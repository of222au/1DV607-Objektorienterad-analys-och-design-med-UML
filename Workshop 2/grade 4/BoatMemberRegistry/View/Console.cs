using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.View
{
    class Console
    {
        private List<InputAction> m_possibleInputActions = null; //stores the current actions the user can do with the current shown menu
        private MemberView m_memberView;
        private bool m_isLoggedIn = false;

        //Constructor
        public Console()
        {
            m_memberView = new MemberView();
        }

        //Methods

        #region Input method(s)

        /// <summary>
        /// Reads input from the console
        /// </summary>
        /// <returns>an InputAction instance</returns>
        public InputAction GetInput()
        {
            if (m_possibleInputActions != null)
            {
                while (true)
                {
                    //Get input from user
                    string input = System.Console.ReadLine();

                    //loop and try to match with the currently possible input
                    foreach (InputAction possibleInput in m_possibleInputActions)
                    {
                        if (possibleInput.CheckAgainstInput(input) == true)
                        {
                            //use this input
                            if (possibleInput.Characters != "")
                            {
                                //return the input
                                return possibleInput;
                            }
                            else
                            {
                                //return the custom input with the action
                                return new InputAction(input, possibleInput.Action);
                            }
                        }
                    }

                    //not correct input
                    System.Console.Write("Wrong input, try again: ");
                }
            }

            //should never happen
            return null;
        }

        /// <summary>
        /// Sets the current possible input actions
        /// </summary>
        /// <param name="specificActions">The specific input actions to set (excluding action for to go back, main menu and quit)</param>
        /// <param name="backAction">The go back action (main menu by default)</param>
        private void setPossibleInputActions(List<InputAction> specificActions, View.ActionEnum backAction = View.ActionEnum.GoToMainMenu)
        {
            List<InputAction> possibleActions = specificActions;

            //insert the general (always used) actions
            InputAction inputActionMainMenu = new InputAction("m", View.ActionEnum.GoToMainMenu); //main menu
            possibleActions.Insert(0, inputActionMainMenu);
            InputAction inputActionQuit = new InputAction("q", View.ActionEnum.Quit); //quit
            possibleActions.Insert(1, inputActionQuit);

            //back action
            possibleActions.Insert(0, new InputAction("b", backAction));

            //set the possible input actions
            m_possibleInputActions = possibleActions;
        }

        #endregion


        #region Display methods

        //Public methods
        public void DisplayMainMenu(Model.User loggedInUser, bool showLoginSuccess = false)
        {
            System.Console.Clear();
            
            System.Console.WriteLine("");
            System.Console.WriteLine("Welcome to the Boat member registry");
            System.Console.WriteLine("************************************");
            if (showLoginSuccess)
            {
                ShowSuccessMessage("You are now logged in!");
            }
            if (loggedInUser != null)
            {
                System.Console.WriteLine("Logged in as: " + loggedInUser.Name);
            }
            System.Console.WriteLine("");
            System.Console.WriteLine("Main menu:");
            System.Console.WriteLine("");
            this.displayInputCharacters("Enter ", "l", " to list all members info in a compact list.");
            this.displayInputCharacters("Enter ", "v", " to list all members info in a verbose list.");
            this.displayInputCharacters("Enter ", "s", " to search for members.");
            this.displayInputCharacters("Enter ", "e", " to " + (m_isLoggedIn ? "edit/" : "") + "view an existing member and it's boats.");
            if (loggedInUser != null)
            {
                this.displayInputCharacters("Enter ", "a", " to add a new member.");
            }
            else
            {
                this.displayInputCharacters("Enter ", "u", " to login.");
            }
            System.Console.WriteLine("");
            this.displayInputCharacters("Please also note that you can at any time enter ", "b", " to go back", false);
            this.displayInputCharacters(", or ", "q", " to quit.");
            System.Console.WriteLine("");
            System.Console.Write("What would you like to do? ");

            //set the current possible input
            List<InputAction> inputActions = new List<InputAction>();
            if (loggedInUser != null)
            {
                inputActions.Add(new InputAction("a", View.ActionEnum.MemberAdd));
            }
            else 
            {
                inputActions.Add(new InputAction("u", View.ActionEnum.Login));
            }
            inputActions.Add(new InputAction("l", View.ActionEnum.MemberListCompact));
            inputActions.Add(new InputAction("v", View.ActionEnum.MemberListVerbose));
            inputActions.Add(new InputAction("s", View.ActionEnum.MemberSearch));
            inputActions.Add(new InputAction("e", View.ActionEnum.MemberSelection));
            this.setPossibleInputActions(inputActions);
        }
        public void DisplayMemberList(bool isLoggedIn, Model.MemberList memberList, DisplayMembersMode mode = DisplayMembersMode.Compact)
        {
            if (mode == DisplayMembersMode.Verbose)
            {
                System.Console.Clear();
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("List of all members");

            if (mode == DisplayMembersMode.Verbose)
            {
                System.Console.WriteLine("****************************");
                System.Console.WriteLine("");
            }

            //loop the members
            foreach (Model.Member member in memberList.GetMembers()) 
            {
                //display member's info
                string memberInfo = m_memberView.GetMemberPresentation(member, mode, true);
                System.Console.WriteLine(memberInfo);
            }

            if (mode == DisplayMembersMode.Verbose)
            {
                System.Console.WriteLine("****************************");
            }
            System.Console.WriteLine("");

            if (isLoggedIn)
            {
                this.displayInputCharacters("Press ", "a", " to add a new member or any other key to go back to main menu. ", false);

                //set the current possible input
                this.setPossibleInputActions(new List<InputAction>() { new InputAction("a", View.ActionEnum.MemberAdd),
                                                                     new InputAction("", View.ActionEnum.GoToMainMenu)});
            }
            else
            {
                System.Console.Write("Press any key to go back to main menu. ");

                //set the current possible input
                this.setPossibleInputActions(new List<InputAction>() { new InputAction("", View.ActionEnum.GoToMainMenu)});
            }

        }
        public void DisplayDetailedMemberPresentation(bool isLoggedIn, Model.Member member, bool clearView = true)
        {
            if (clearView)
            {
                System.Console.Clear();
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("Member presentation");
            System.Console.WriteLine("*********************************");
            System.Console.WriteLine("");

            //display detailed presentation
            string memberInfo = m_memberView.GetDetailedMemberPresentation(member);
            System.Console.WriteLine(memberInfo);

            System.Console.WriteLine("*********************************");
            System.Console.WriteLine("");

            if (isLoggedIn)
            {
                this.displayInputCharacters("Enter ", "e", " to edit this member.");
                this.displayInputCharacters("Enter ", "d", " to delete this member.");
                System.Console.WriteLine("");
            }
            this.displayLineOfGeneralOptions(false, true);
            System.Console.WriteLine("");
            System.Console.Write("What would you like to do now? ");

            //set the current possible input
            List<InputAction> inputActions = new List<InputAction>();
            if (isLoggedIn)
            {
                inputActions.Add(new InputAction("a", View.ActionEnum.MemberAdd));
                inputActions.Add(new InputAction("e", View.ActionEnum.MemberEdit));
                inputActions.Add(new InputAction("d", View.ActionEnum.MemberDelete));
            }
            else
            {
                inputActions.Add(new InputAction("", View.ActionEnum.MemberSelected_ShowMenuAgain));
            }
            this.setPossibleInputActions(inputActions, View.ActionEnum.MemberSelected_ShowMenuAgain);
        }
        public void DisplaySelectMemberMenu(Model.MemberList memberList, bool showSelectMemberError = false, bool clear = true)
        {
            if (clear)
            {
                System.Console.Clear();
            }

            this.displayMembersEditorHeader();

            string keyToAddMember = "a";
            List<InputAction> possibleInput = new List<InputAction>() { new InputAction(keyToAddMember, View.ActionEnum.MemberAdd) };

            if (memberList.MemberCount > 0)
            {
                //loop the members
                int counter = 1;
                foreach (Model.Member member in memberList.GetMembers())
                {
                    //display each member
                    this.displayInputCharacters("", counter.ToString(), ". " + m_memberView.GetMemberPresentation(member, DisplayMembersMode.Compact));
                    counter++;
                }
                System.Console.WriteLine("");

                if (showSelectMemberError)
                {
                    this.ShowErrorMessage("Could not select the member...", true);
                }

                this.displayInputCharacters("Choose a member by it's number (or ", keyToAddMember, " to add a new member): ", false);

                //add all the member numbers to possible input
                for (int i = 1; i <= memberList.MemberCount; i++)
                {
                    possibleInput.Add(new InputAction(i.ToString(), View.ActionEnum.MemberSelected));
                }
            }
            else
            {
                //no members
                System.Console.WriteLine("No members yet..");
                System.Console.WriteLine("");
                this.displayInputCharacters("Enter ", keyToAddMember, " to add a new member. ");

                //also display the general input options
                this.displayLineOfGeneralOptions(false, true);
            }

            //set the current possible input
            this.setPossibleInputActions(possibleInput);
        }
        public void DisplaySearchMemberMenu(bool showSelectMemberError = false, bool clear = true)
        {
            if (clear)
            {
                System.Console.Clear();
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("Search member");
            System.Console.WriteLine("**************");
            
            displayInputCharacters("Press ", "n", " to search by name", true);
            displayInputCharacters("Press ", "t", " to search by boat type", true);

            List<InputAction> possibleInput = new List<InputAction>() { new InputAction("n", View.ActionEnum.MemberSearchByName),
                                                                        new InputAction("t", View.ActionEnum.MemberSearchByBoatType) };


            //set the current possible input
            this.setPossibleInputActions(possibleInput);  
        }
        public void DisplaySearchEnterInput()
        {
            System.Console.Write("Enter search value: ");

            //set the current possible input
            List<InputAction> possibleInput = new List<InputAction>() { new InputAction("", View.ActionEnum.MemberSearched) };
            this.setPossibleInputActions(possibleInput);
        }
        public void DisplaySearchResults(List<Model.Member> members)
        {
            System.Console.Clear();

            System.Console.WriteLine("");
            System.Console.WriteLine("Search member results");
            System.Console.WriteLine("**********************");

            if (members != null && members.Count > 0)
            {
                //loop the members
                foreach (Model.Member member in members)
                {
                    System.Console.WriteLine(m_memberView.GetMemberPresentation(member, DisplayMembersMode.Compact));
                }
            }
            else
            {
                //no members
                System.Console.WriteLine("No members found..");
            }
            System.Console.WriteLine("");
            displayInputCharacters("Press ", "s", " to search again", false);
            displayInputCharacters(" or ", "m", " to go back to main menu: ", false);

            //set the current possible input
            List<InputAction> possibleInput = new List<InputAction>() { new InputAction("s", View.ActionEnum.MemberSearch) };
            this.setPossibleInputActions(possibleInput);
        }
        public void DisplayChangeMemberMenu(bool isLoggedIn, Model.Member selectedMember, bool clear = true)
        {
            if (clear)
            {
                System.Console.Clear();
            }

            this.displayMembersEditorHeader();

            System.Console.WriteLine("-> " + m_memberView.GetMemberPresentation(selectedMember, DisplayMembersMode.Compact));

            System.Console.WriteLine("");
            this.displayInputCharacters("Enter ", "v", " to view this member's info.");
            if (isLoggedIn)
            {
                this.displayInputCharacters("Enter ", "e", " to edit this member's personal info.");
                this.displayInputCharacters("Enter ", "g", " to add a boat to this member.");
                if (selectedMember.BoatCount > 0) { this.displayInputCharacters("Enter ", "h", " to edit a boat."); }
                if (selectedMember.BoatCount > 0) { this.displayInputCharacters("Enter ", "j", " to delete a boat."); }
                this.displayInputCharacters("Enter ", "d", " to delete this member.");
            }
            System.Console.WriteLine("");
            this.displayLineOfGeneralOptions(false, true);
            System.Console.WriteLine("");
            System.Console.Write("What do you want to do with the selected member? ");
            

            //set the current possible input
            List<InputAction> inputActions = new List<InputAction>();
            if (isLoggedIn)
            {
                inputActions.Add(new InputAction("a", View.ActionEnum.MemberAdd));
                inputActions.Add(new InputAction("e", View.ActionEnum.MemberEdit));
                inputActions.Add(new InputAction("g", View.ActionEnum.MemberBoatAdd));
                inputActions.Add(new InputAction("d", View.ActionEnum.MemberDelete));
                if (selectedMember.BoatCount > 0)
                {
                    inputActions.Add(new InputAction("h", View.ActionEnum.MemberBoatSelectionEdit));
                    inputActions.Add(new InputAction("j", View.ActionEnum.MemberBoatSelectionDelete));
                }
            }
            inputActions.Add(new InputAction("v", View.ActionEnum.MemberPresentation));

            this.setPossibleInputActions(inputActions, View.ActionEnum.MemberSelection);
        }
        public void DisplaySelectMemberBoatMenu(Model.Member selectedMember, bool selectToEditNotDelete, bool showSelectBoatError = false)
        {
            System.Console.Clear();

            this.displayMembersEditorHeader();

            System.Console.WriteLine("-> " + m_memberView.GetMemberPresentation(selectedMember, DisplayMembersMode.Compact));
            System.Console.WriteLine("");

            string keyToAddBoat = "a";
            List<InputAction> possibleInput = new List<InputAction>() { new InputAction(keyToAddBoat, View.ActionEnum.MemberBoatAdd) };

            if (selectedMember.BoatCount > 0)
            {
                //loop the members
                int counter = 1;
                foreach (Model.Boat boat in selectedMember.Boats)
                {
                    //display each boat
                    this.displayInputCharacters("  ", counter.ToString(), ". " + m_memberView.GetBoatPresentation(boat));
                    counter++;
                }
                System.Console.WriteLine("");

                if (showSelectBoatError)
                {
                    this.ShowErrorMessage("Could not select the boat...", true);
                }

                this.displayInputCharacters("Choose a boat " + (selectToEditNotDelete ? "to edit" : "to delete") + " by it's number (or ", keyToAddBoat, " to add a new boat): ", false);

                //add all the member numbers to possible input
                for (int i = 1; i <= selectedMember.BoatCount; i++)
                {
                    View.ActionEnum action = (selectToEditNotDelete ? View.ActionEnum.MemberBoatSelectedEdit : View.ActionEnum.MemberBoatSelectedDelete);
                    possibleInput.Add(new InputAction(i.ToString(), action));
                }
            }
            else //no boats
            {
                System.Console.WriteLine("No boats yet..");
                System.Console.WriteLine("");
                this.displayInputCharacters("Enter ", keyToAddBoat, " to add a new boat. ");

                //also display the general input options
                this.displayLineOfGeneralOptions(false, true);
            }

            //set the current possible input
            this.setPossibleInputActions(possibleInput, 
                                         View.ActionEnum.MemberSelected_ShowMenuAgain);
        }
        public void DisplayEnterAnyKeyToContinue()
        {
            System.Console.Write("Enter any key to continue to main menu.. ");

            this.setPossibleInputActions(new List<InputAction>() { new InputAction("", View.ActionEnum.GoToMainMenu) });
        }

        //these two methods calls displayConfirmMessage with different parameters
        public void DisplayConfirmMemberDelete(string memberName)
        {
            this.displayConfirmMessage("Are you sure you want to delete " + memberName + "? This can not be undone.", View.ActionEnum.MemberConfirmDelete);
        }
        public void DisplayConfirmBoatDelete()
        {
            this.displayConfirmMessage("Are you sure you want to delete this boat? This can not be undone.", View.ActionEnum.MemberBoatConfirmDelete);
        }

        //these four methods all calls displayAddEditMemberOrBoat with different parameters
        public void DisplayEditMemberAttribute(AddEditMemberAttribute attribute, Model.Member selectedMember)
        {
            this.displayAddEditMemberOrBoat(true, AddEditMemberOrBoatMode.Edit_Member, attribute, selectedMember);
        }
        public void DisplayAddMemberAttribute(AddEditMemberAttribute attribute)
        {
            this.displayAddEditMemberOrBoat(true, AddEditMemberOrBoatMode.Add_Member, attribute);
        }
        public void DisplayEditBoatAttribute(AddEditBoatAttribute attribute, Model.Member selectedMember, Model.Boat selectedBoat)
        {
            this.displayAddEditMemberOrBoat(true, AddEditMemberOrBoatMode.Edit_Boat, AddEditMemberAttribute.None, selectedMember, attribute, selectedBoat);
        }
        public void DisplayAddBoatAttribute(AddEditBoatAttribute attribute, Model.Member selectedMember)
        {
            this.displayAddEditMemberOrBoat(true, AddEditMemberOrBoatMode.Add_Boat, AddEditMemberAttribute.None, selectedMember, attribute);
        }


        public void DisplayLoginUserAttribute(LoginUserAttribute attribute, bool showErrorLoggingIn = false)
        {
            this.displayLoginUser(attribute, showErrorLoggingIn);
        }


        public void ShowErrorMessage(string message, bool showTryAgain = false)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;

            System.Console.WriteLine("");
            System.Console.WriteLine(message);

            System.Console.ResetColor();

            if (showTryAgain)
            {
                System.Console.Write("Please try again: ");
            }
        }
        public void ShowSuccessMessage(string message, bool clear = false)
        {
            if (clear)
            {
                System.Console.Clear();
            }

            System.Console.ForegroundColor = ConsoleColor.Green;

            System.Console.WriteLine("");
            System.Console.WriteLine(message);
            System.Console.WriteLine("");

            System.Console.ResetColor();
        }
        public void ShowNeedToLoginMessage()
        {
            System.Console.ForegroundColor = ConsoleColor.Red;

            System.Console.WriteLine("");
            System.Console.WriteLine("You need to login to access this future");

            System.Console.ResetColor();
        }


        //Private methods
        private void displayLoginUser(LoginUserAttribute loginAttribute, bool showErrorLoggingIn = false)
        {
            //The collected function for login user (retrieve login credentials)

            if (loginAttribute == LoginUserAttribute.Name)
            {
                System.Console.Clear();

                System.Console.WriteLine("");
                System.Console.WriteLine("Login user");
                System.Console.WriteLine("*************");;
            }
            if (showErrorLoggingIn)
            {
                ShowErrorMessage("Sorry could not log in, please try again.");
            }
            System.Console.WriteLine("");

            View.ActionEnum backAction;

            View.ActionEnum anyInputEnteredAction = View.ActionEnum.NotSpecified;
            List<InputAction> specificInputs = new List<InputAction>();

            System.Console.Write("Please enter " + loginAttribute.ToString().ToLower() + ": ");

            backAction = View.ActionEnum.GoToMainMenu;
            if (loginAttribute == LoginUserAttribute.Name)
            {
                anyInputEnteredAction = View.ActionEnum.LoginSpecifyingName;
            }
            else if (loginAttribute == LoginUserAttribute.Password)
            {
                anyInputEnteredAction = View.ActionEnum.LoginSpecifyingPassword;
            }
            if (anyInputEnteredAction == View.ActionEnum.NotSpecified)
            {
                //should never happen (as long as no new attributes are added without taking care of them here)
                throw new Exception("No login action specified for attribute " + loginAttribute);
            }

            //add the any input action
            specificInputs.Add(new InputAction("", anyInputEnteredAction));

            //set the current possible input
            this.setPossibleInputActions(specificInputs, backAction);
        }
        
        private void displayMembersEditorHeader()
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Existing members editor");
            System.Console.WriteLine("******************************");
            System.Console.WriteLine("");
        }
        private void displayConfirmMessage(string message, View.ActionEnum confirmAction, View.ActionEnum anyOtherKeyAction = View.ActionEnum.GoToMainMenu)
        {
            System.Console.WriteLine("");

            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ResetColor();

            this.displayInputCharacters("Enter ", "y", " to confirm, or any other key to cancel: ", false);

            //set the current possible input
            this.setPossibleInputActions(new List<InputAction>() { new InputAction("y", confirmAction),
                                                                     new InputAction("", anyOtherKeyAction)});
        }
        private void displayAddEditMemberOrBoat(bool clearAndRedrawView, AddEditMemberOrBoatMode addEditMode, AddEditMemberAttribute memberAttribute = AddEditMemberAttribute.None, Model.Member ifEditing_selectedMember = null, AddEditBoatAttribute boatAttribute = AddEditBoatAttribute.None, Model.Boat ifEditingBoat_selectedBoat = null)
        {
            //The collected function for adding/editing member or boat

            bool memberNotBoat = (addEditMode == AddEditMemberOrBoatMode.Add_Member || addEditMode == AddEditMemberOrBoatMode.Edit_Member);
            bool addNotEdit = (addEditMode == AddEditMemberOrBoatMode.Add_Boat || addEditMode == AddEditMemberOrBoatMode.Add_Member);

            if (clearAndRedrawView)
            {
                System.Console.Clear();

                System.Console.WriteLine("");
                System.Console.WriteLine((addNotEdit ? "Add a new " : "Edit existing ") + (memberNotBoat ? "member" : "boat"));
                System.Console.WriteLine("************************");
                System.Console.WriteLine("");
                if (!addNotEdit) //if editing, or adding/editing boat
                {
                    System.Console.WriteLine("-> " + m_memberView.GetMemberPresentation(ifEditing_selectedMember, DisplayMembersMode.Compact));

                    if (addEditMode == AddEditMemberOrBoatMode.Edit_Boat)
                    {
                        System.Console.WriteLine("  -> " + m_memberView.GetBoatPresentation(ifEditingBoat_selectedBoat));
                    }

                    System.Console.WriteLine("");
                }
            }
            else
            {
                System.Console.WriteLine("");
            }

            View.ActionEnum backAction;

            View.ActionEnum anyInputEnteredAction = View.ActionEnum.NotSpecified;
            List<InputAction> specificInputs = new List<InputAction>();

            string attributeName = (memberNotBoat ? memberAttribute.ToString() : boatAttribute.ToString()).ToLower().Replace("_", " ");
            System.Console.Write("Please enter a " + (!addNotEdit ? "new " : "") + attributeName + (addNotEdit ? " for the new " + (memberNotBoat ? "member" : "boat") : ""));

            System.Console.Write(" (");

            //if adding/editing boat type, display the boat types with a number and add that number as possible input
            if (!memberNotBoat && boatAttribute == AddEditBoatAttribute.Type)
            {
                View.ActionEnum action = (addNotEdit ? View.ActionEnum.MemberBoatAddingType : View.ActionEnum.MemberBoatEditedType);

                foreach (Model.Boat.BoatType type in Enum.GetValues(typeof(Model.Boat.BoatType)))
                {
                    if (type != Model.Boat.BoatType.Undefined)
                    {
                        this.displayInputCharacters("", ((int)type).ToString(), " " + type.ToString().Replace("__", "/") + ", ", false);

                        //add input action to list
                        specificInputs.Add(new InputAction(((int)type).ToString(), action));
                    }
                }
            }
            if (!addNotEdit)
            {
                System.Console.Write("empty to leave unchanged, ");
            }
            this.displayInputCharacters("or ", "b", " to go back): ", false);

            //set the back action and the "any input" action depending on adding/editing member/boat
            if (memberNotBoat) //if member
            {
                if (addNotEdit) //if add mode
                {
                    backAction = View.ActionEnum.GoToMainMenu;
                    if (memberAttribute == AddEditMemberAttribute.Name)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberAddingName;
                    }
                    else if (memberAttribute == AddEditMemberAttribute.Personal_Number)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberAddingPersonalNumber;
                    }
                }
                else //if edit mode
                {
                    backAction = View.ActionEnum.MemberSelected_ShowMenuAgain;
                    if (memberAttribute == AddEditMemberAttribute.Name)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberEditedName;
                    }
                    else if (memberAttribute == AddEditMemberAttribute.Personal_Number)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberEditedPersonalNumber;
                    }
                }
            }
            else //if boat
            {
                backAction = View.ActionEnum.MemberSelected_ShowMenuAgain;

                if (addNotEdit) //if add mode
                {
                    if (boatAttribute == AddEditBoatAttribute.Type)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberBoatAddingType;
                    }
                    else if (boatAttribute == AddEditBoatAttribute.Length)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberBoatAddingLength;
                    }
                }
                else //if edit mode
                {
                    if (boatAttribute == AddEditBoatAttribute.Type)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberBoatEditedType;
                    }
                    else if (boatAttribute == AddEditBoatAttribute.Length)
                    {
                        anyInputEnteredAction = View.ActionEnum.MemberBoatEditedLength;
                    }
                }
            }
            if (anyInputEnteredAction == View.ActionEnum.NotSpecified)
            {
                //should never happen (as long as no new attributes are added without taking care of them here)
                throw new Exception("No action specified for attribute " + attributeName);
            }

            //add the any input action
            specificInputs.Add(new InputAction("", anyInputEnteredAction));

            //set the current possible input
            this.setPossibleInputActions(specificInputs, backAction);
        }
        private void displayLineOfGeneralOptions(bool isTheMainOptions, bool includeGoBack = true)
        {
            System.Console.Write(isTheMainOptions ? "Enter " : "Or ");
            if (includeGoBack)
            {
                this.displayInputCharacters("", "b", " to go back, ", false);
            }
            this.displayInputCharacters("", "m", " to main menu ", false);
            this.displayInputCharacters("or ", "q", " to quit" + (isTheMainOptions ? ": " : ""), (isTheMainOptions ? false : true));
        }
        private void displayInputCharacters(string textBefore, string inputCharacters, string textAfter, bool finishWithLine = true)
        {
            //this displays some text before, and then the input characters in another color, and text after. 

            System.Console.Write(textBefore);

            System.Console.ForegroundColor = ConsoleColor.Magenta;
            System.Console.Write(inputCharacters);
            System.Console.ResetColor();

            if (finishWithLine)
            {
                System.Console.WriteLine(textAfter);
            }
            else
            {
                System.Console.Write(textAfter);
            }
        }


        #endregion

    }
}
