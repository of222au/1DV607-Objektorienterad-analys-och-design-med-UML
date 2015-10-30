using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BoatMemberRegistry.Controller
{
    [DataContract]
    class MemberRegistry : Model.MemberObserver, Model.UserObserver
    {
        private Model.MemberListDAL m_memberListDAL;
        private Model.MemberList m_memberList;

        private Model.UserListDAL m_userListDAL;
        private Model.UserList m_userList;

        private View.Console m_view;

        private Model.Member m_addingEditingMember;
        private Model.Boat m_addingEditingBoat;

        private Model.User m_loggedInUser;
        private bool m_isLoggedIn = false;

        private View.ActionEnum m_searchMode;
        private Model.MemberFiltering m_memberFiltering;


        private bool m_isInStartup = false;

        //Constructor
        public MemberRegistry(View.Console a_view)
        {
            m_isInStartup = true;
            m_view = a_view;

            //try to load member list from storage
            m_memberListDAL = new Model.MemberListDAL();
            m_memberList = m_memberListDAL.LoadMemberList();
            if (m_memberList == null)
            {
                m_memberList = new Model.MemberList();
            }

            //subscribe to the MemberList
            m_memberList.AddSubscriber(this);

            //try to load user list from storage
            m_userListDAL = new Model.UserListDAL();
            m_userList = m_userListDAL.LoadUserList();
            if (m_userList == null)
            {
                m_userList = new Model.UserList();
            }

            //subscribe to the UserList
            m_userList.AddSubscriber(this);

            m_addingEditingMember = null;
            m_loggedInUser = null;
            m_memberFiltering = null;

            m_isInStartup = false;
        }

        //Properties
        public Model.MemberList MemberList
        {
            get
            {
                return m_memberList;
            }
        }
        public Model.UserList UserList
        {
            get
            {
                return m_userList;
            }
        }

        private Model.User GetLoggedInUser()
        {
            if (m_loggedInUser != null && m_isLoggedIn)
            {
                return m_loggedInUser;
            }
            return null;
        }

        //Methods
        public bool UseSystem()
        {
            //first display main menu
            m_view.DisplayMainMenu(GetLoggedInUser());

            bool quit = false;
            while (!quit) {

                //get input
                View.InputAction input = m_view.GetInput();

                //do action depending on input
                if (input.Action == View.ActionEnum.Quit)
                {
                    quit = true;
                }
                else if (input.Action == View.ActionEnum.GoToMainMenu)
                {
                    m_view.DisplayMainMenu(GetLoggedInUser());
                }
                else if (input.Action == View.ActionEnum.Login)
                {
                    m_loggedInUser = new Model.User();
                    m_view.DisplayLoginUserAttribute(View.LoginUserAttribute.Name);
                }
                else if (input.Action == View.ActionEnum.MemberListCompact)
                {
                    m_view.DisplayMemberList(m_isLoggedIn, m_memberList, View.DisplayMembersMode.Compact);
                }
                else if (input.Action == View.ActionEnum.MemberListVerbose)
                {
                    m_view.DisplayMemberList(m_isLoggedIn, m_memberList, View.DisplayMembersMode.Verbose);
                }

                else if (input.Action == View.ActionEnum.MemberSelection)
                {
                    m_addingEditingMember = null; //make sure no member is already stored 
                    m_view.DisplaySelectMemberMenu(m_memberList);
                }
                else if (input.Action == View.ActionEnum.MemberSearch)
                {
                    m_memberFiltering = null;
                    m_view.DisplaySearchMemberMenu();
                }
                else if (input.Action == View.ActionEnum.MemberSearchByName ||
                         input.Action == View.ActionEnum.MemberSearchByBoatType)
                {
                    m_searchMode = input.Action;
                    m_view.DisplaySearchEnterInput();
                }
                else if (input.Action == View.ActionEnum.MemberSearched)
                {
                    Model.MemberFilter filter = null;
                    if (m_searchMode == View.ActionEnum.MemberSearchByName)
                    {
                        filter = new Model.MemberFilter(Model.MemberFilter.MemberFilterField.Name, Model.MemberFilter.MemberFilterOperator.Contains, input.Characters);
                    }
                    else if (m_searchMode == View.ActionEnum.MemberSearchByBoatType)
                    {
                        filter = new Model.MemberFilter(Model.MemberFilter.MemberFilterField.BoatOfType, Model.MemberFilter.MemberFilterOperator.Contains, input.Characters);
                    }

                    List<Model.MemberFilter> filters = new List<Model.MemberFilter>();
                    if (filter != null)
                    {
                        filters.Add(filter);
                    }

                    Model.MemberFilters memberFilters = new Model.MemberFilters(filters);
                    m_memberFiltering = new Model.MemberFiltering(memberFilters);
                    List<Model.Member> searchResults = m_memberFiltering.FilterMembers(m_memberList.GetMembers());

                    //display results
                    m_view.DisplaySearchResults(searchResults);
                }
                else if (input.Action == View.ActionEnum.MemberSelected ||
                         input.Action == View.ActionEnum.MemberSelected_ShowMenuAgain)
                {
                    bool allOkay = true;
                    if (input.Action == View.ActionEnum.MemberSelected)
                    {
                        //try to retrieve the selected member by number and store it 
                        allOkay = this.SelectMemberFromInputNumber(input.Characters);
                    }

                    if (allOkay)
                    {
                        m_view.DisplayChangeMemberMenu(m_isLoggedIn, m_addingEditingMember);
                    }
                    else
                    {
                        //error selecting member, show member selection again (with error message)
                        m_view.DisplaySelectMemberMenu(m_memberList, true);
                    }
                }

                else if (input.Action == View.ActionEnum.MemberBoatSelectionEdit ||
                         input.Action == View.ActionEnum.MemberBoatSelectionDelete)
                {
                    bool editNotDelete = (input.Action == View.ActionEnum.MemberBoatSelectionEdit);
                    m_addingEditingBoat = null; //make sure no boat is already stored 
                    m_view.DisplaySelectMemberBoatMenu(m_addingEditingMember, editNotDelete);
                }
                else if (input.Action == View.ActionEnum.MemberBoatSelectedEdit ||
                         input.Action == View.ActionEnum.MemberBoatSelectedDelete)
                {
                    bool editNotDelete = (input.Action == View.ActionEnum.MemberBoatSelectedEdit);

                    //try to retrieve the selected member by number and store it 
                    bool selectResult = this.SelectBoatFromInputNumber(input.Characters);

                    if (selectResult == true)
                    {
                        if (editNotDelete)
                        {
                            m_view.DisplayEditBoatAttribute(View.AddEditBoatAttribute.Type, m_addingEditingMember, m_addingEditingBoat);
                        }
                        else
                        {
                            m_view.DisplayConfirmBoatDelete();
                        }
                    }
                    else
                    {
                        //error selecting boat, show boat selection again (with error message)
                        m_view.DisplaySelectMemberBoatMenu(m_addingEditingMember, editNotDelete, true);
                    }
                }


                else if (input.Action == View.ActionEnum.MemberPresentation ||
                         input.Action == View.ActionEnum.MemberDelete ||
                         input.Action == View.ActionEnum.MemberConfirmDelete ||
                         input.Action == View.ActionEnum.MemberEdit ||
                         input.Action == View.ActionEnum.MemberEditedName ||
                         input.Action == View.ActionEnum.MemberEditedPersonalNumber ||
                         input.Action == View.ActionEnum.MemberAdd ||
                         input.Action == View.ActionEnum.MemberAddingName ||
                         input.Action == View.ActionEnum.MemberAddingPersonalNumber ||
                         input.Action == View.ActionEnum.MemberBoatAdd ||
                         input.Action == View.ActionEnum.MemberBoatAddingType ||
                         input.Action == View.ActionEnum.MemberBoatAddingLength ||
                         input.Action == View.ActionEnum.MemberBoatEditedType ||
                         input.Action == View.ActionEnum.MemberBoatEditedLength ||
                         input.Action == View.ActionEnum.MemberBoatConfirmDelete ||
                         input.Action == View.ActionEnum.LoginSpecifyingName ||
                         input.Action == View.ActionEnum.LoginSpecifyingPassword)
                {
                    bool addingMember = (input.Action == View.ActionEnum.MemberAdd ||
                                        input.Action == View.ActionEnum.MemberAddingName ||
                                        input.Action == View.ActionEnum.MemberAddingPersonalNumber);
                    bool editingMember = (input.Action == View.ActionEnum.MemberEdit ||
                                        input.Action == View.ActionEnum.MemberEditedName ||
                                        input.Action == View.ActionEnum.MemberEditedPersonalNumber);
                    bool addingBoat = (input.Action == View.ActionEnum.MemberBoatAdd ||
                                        input.Action == View.ActionEnum.MemberBoatAddingType ||
                                        input.Action == View.ActionEnum.MemberBoatAddingLength);
                    bool editingBoat = (input.Action == View.ActionEnum.MemberBoatEditedType ||
                                        input.Action == View.ActionEnum.MemberBoatEditedLength);
                    bool deletingBoat = (input.Action == View.ActionEnum.MemberBoatConfirmDelete);
                    bool loggingIn = (input.Action == View.ActionEnum.LoginSpecifyingName ||
                                      input.Action == View.ActionEnum.LoginSpecifyingPassword);

                    if (!loggingIn && !addingMember && m_addingEditingMember == null)
                    {
                        //a selected member is needed, show member selection again (with error message)
                        m_view.DisplaySelectMemberMenu(m_memberList, true);
                    }
                    else if ((editingBoat || deletingBoat) && m_addingEditingBoat == null)
                    {
                        //a selected boat is needed, show boat selection again (with error message)
                        m_view.DisplaySelectMemberBoatMenu(m_addingEditingMember, true, true);
                    }
                    else
                    {
                        if (input.Action == View.ActionEnum.MemberPresentation)
                        {
                            m_view.DisplayDetailedMemberPresentation(m_isLoggedIn, m_addingEditingMember);
                        }
                        else if (input.Action == View.ActionEnum.MemberDelete)
                        {
                            m_view.DisplayConfirmMemberDelete(m_addingEditingMember.Name);
                        }
                        else if (input.Action == View.ActionEnum.MemberConfirmDelete)
                        {
                            //try to remove the member from list
                            if (m_memberList.DeleteMember(m_addingEditingMember.UniqueId))
                            {
                                m_view.ShowSuccessMessage("Member deleted successfully.");
                            }
                            else
                            {
                                m_view.ShowErrorMessage("Could not delete the member.");
                            }

                            m_addingEditingMember = null;
                            m_view.DisplayEnterAnyKeyToContinue();
                        }
                        else if (input.Action == View.ActionEnum.MemberEdit)
                        {
                            m_view.DisplayEditMemberAttribute(View.AddEditMemberAttribute.Name, m_addingEditingMember);
                        }
                        else if (input.Action == View.ActionEnum.MemberAdd)
                        {
                            //create the new member to store details into
                            m_addingEditingMember = new Model.Member();

                            m_view.DisplayAddMemberAttribute(View.AddEditMemberAttribute.Name);
                        }
                        else if (input.Action == View.ActionEnum.MemberBoatAdd)
                        {
                            //create the new boat to store details into
                            m_addingEditingBoat = new Model.Boat();

                            m_view.DisplayAddBoatAttribute(View.AddEditBoatAttribute.Type, m_addingEditingMember);
                        }
                        else if (input.Action == View.ActionEnum.MemberBoatConfirmDelete)
                        {
                            //try to remove the boat from list
                            if (m_addingEditingMember.DeleteBoat(m_addingEditingBoat))
                            {
                                m_view.ShowSuccessMessage("Boat deleted successfully.");
                            }
                            else
                            {
                                m_view.ShowErrorMessage("Could not delete the boat.");
                            }

                            m_addingEditingMember = null;
                            m_addingEditingBoat = null;
                            m_view.DisplayEnterAnyKeyToContinue();
                        }
                        else //Input of some kind (adding/editing member/boat)
                        {
                            //try to set the attribute in stored member/boat
                            bool success = false;
                            string errorMessage = null;
                            try
                            {
                                if ((addingMember || addingBoat || loggingIn) && input.Characters == "")
                                {
                                    throw new Exception("Can't leave this field empty.");
                                }

                                if (input.Characters != "") //if nothing is supplied, user didn't want to change anything
                                {
                                    //Member
                                    if (input.Action == View.ActionEnum.MemberEditedName ||
                                        input.Action == View.ActionEnum.MemberAddingName)
                                    {
                                        m_addingEditingMember.Name = input.Characters;
                                    }
                                    else if (input.Action == View.ActionEnum.MemberEditedPersonalNumber ||
                                             input.Action == View.ActionEnum.MemberAddingPersonalNumber)
                                    {
                                        if (m_memberList.IsPersonalNumberUnique(input.Characters, m_addingEditingMember))
                                        {
                                            m_addingEditingMember.PersonalNumber = input.Characters;
                                        }
                                        else
                                        {
                                            //error
                                            errorMessage = "Personal number already exists in another member.";
                                        }
                                    }

                                    //Boat
                                    else if (input.Action == View.ActionEnum.MemberBoatAddingType ||
                                            input.Action == View.ActionEnum.MemberBoatEditedType)
                                    {
                                        m_addingEditingBoat.Type = (Model.Boat.BoatType)int.Parse(input.Characters);
                                    }
                                    else if (input.Action == View.ActionEnum.MemberBoatAddingLength ||
                                             input.Action == View.ActionEnum.MemberBoatEditedLength)
                                    {
                                        m_addingEditingBoat.Length = double.Parse(input.Characters);
                                    }

                                    //Login
                                    else if (input.Action == View.ActionEnum.LoginSpecifyingName)
                                    {
                                        m_loggedInUser.Name = input.Characters;
                                    }
                                    else if (input.Action == View.ActionEnum.LoginSpecifyingPassword)
                                    {
                                        m_loggedInUser.Password = input.Characters;
                                    }
                                }

                                if (errorMessage == null)
                                {
                                    success = true;
                                }
                            }
                            catch (Exception e)
                            {
                                errorMessage = e.Message;
                            }

                            if (success)
                            {
                                //Member
                                if (input.Action == View.ActionEnum.MemberEditedName)
                                {
                                    //go on to edit personal number
                                    m_view.DisplayEditMemberAttribute(View.AddEditMemberAttribute.Personal_Number, m_addingEditingMember);
                                }
                                else if (input.Action == View.ActionEnum.MemberEditedPersonalNumber)
                                {
                                    //successfully edited member, show message
                                    m_view.ShowSuccessMessage("Member successfully edited.", true);

                                    //display change member menu
                                    m_view.DisplayChangeMemberMenu(m_isLoggedIn, m_addingEditingMember, false);
                                }
                                else if (input.Action == View.ActionEnum.MemberAddingName)
                                {
                                    //go on to add personal number
                                    m_view.DisplayAddMemberAttribute(View.AddEditMemberAttribute.Personal_Number);
                                }
                                else if (input.Action == View.ActionEnum.MemberAddingPersonalNumber)
                                {
                                    //all attributes added, now try to save the member to list
                                    if (this.MemberList.AddMember(m_addingEditingMember, true))
                                    {
                                        //successfully added to the member list, show message
                                        m_view.ShowSuccessMessage("Member successfully added.", true);

                                        //display members list
                                        m_view.DisplaySelectMemberMenu(m_memberList, false, false);
                                    }
                                    else
                                    {
                                        //could not add the member for some reason.. show error message (should never happen)
                                        m_view.ShowErrorMessage("Error: could not add the member to the list...");
                                    }
                                }

                                //Boat
                                else if (input.Action == View.ActionEnum.MemberBoatEditedType)
                                {
                                    //go on to edit length
                                    m_view.DisplayEditBoatAttribute(View.AddEditBoatAttribute.Length, m_addingEditingMember, m_addingEditingBoat);
                                }
                                else if (input.Action == View.ActionEnum.MemberBoatEditedLength)
                                {
                                    //successfully edited boat, show message
                                    m_view.ShowSuccessMessage("Boat successfully edited.", true);

                                    //display change member menu
                                    m_view.DisplayChangeMemberMenu(m_isLoggedIn, m_addingEditingMember, false);
                                }
                                else if (input.Action == View.ActionEnum.MemberBoatAddingType)
                                {
                                    //go on to add length
                                    m_view.DisplayAddBoatAttribute(View.AddEditBoatAttribute.Length, m_addingEditingMember);
                                }
                                else if (input.Action == View.ActionEnum.MemberBoatAddingLength)
                                {
                                    //all attributes added, now try to save the member to list
                                    if (m_addingEditingMember.AddBoat(m_addingEditingBoat))
                                    {
                                        //successfully added to the member list, show message
                                        m_view.ShowSuccessMessage("Boat successfully added.", true);

                                        //display change member menu
                                        m_view.DisplayChangeMemberMenu(m_isLoggedIn, m_addingEditingMember, false);
                                    }
                                    else
                                    {
                                        //could not add the member for some reason.. show error message (should never happen)
                                        m_view.ShowErrorMessage("Error: could not add the boat...");
                                    }
                                }

                                //Login
                                else if (input.Action == View.ActionEnum.LoginSpecifyingName)
                                {
                                    m_view.DisplayLoginUserAttribute(View.LoginUserAttribute.Password);
                                }
                                else if (input.Action == View.ActionEnum.LoginSpecifyingPassword)
                                {
                                    //try to login
                                    if (m_loggedInUser.HasCorrectDetails &&
                                        m_userList.CheckUserCredentials(m_loggedInUser))
                                    {
                                        //show main menu and that login succeeded
                                        m_isLoggedIn = true;
                                        m_view.DisplayMainMenu(GetLoggedInUser(), true);
                                    }
                                    else
                                    {
                                        //show login again with error
                                        m_loggedInUser = new Model.User();
                                        m_view.DisplayLoginUserAttribute(View.LoginUserAttribute.Name, true);
                                    }
                                }
                            }
                            else
                            {
                                //show error message
                                m_view.ShowErrorMessage(errorMessage, true);

                                //let the loop continue so the user can try again
                            }
                        }
                    }
                }
                else
                {
                    //should never occur (if all actions are implemented above)
                    throw new Exception("The action " + input.Action.ToString() + " has not been implemented yet..");
                }
            }
            return false;
        }

        private bool SelectMemberFromInputNumber(string input)
        {
            m_addingEditingMember = null;
            int memberNumber = -1;
            try
            {
                //parse the member number and try to retrieve it from the member list
                memberNumber = int.Parse(input);
                m_addingEditingMember = m_memberList.GetMember(memberNumber - 1);
            }
            catch { }

            if (m_addingEditingMember != null)
            {
                return true;
            }
            return false;
        }
        private bool SelectBoatFromInputNumber(string input)
        {
            m_addingEditingBoat = null;
            int boatNumber = -1;
            try
            {
                //parse the boat number and try to retrieve it from the boat list
                boatNumber = int.Parse(input);
                m_addingEditingBoat = m_addingEditingMember.GetBoat(boatNumber - 1);
            }
            catch { }

            if (m_addingEditingBoat != null)
            {
                return true;
            }
            return false;
        }


        //MemberObserver method(s)
        public void MemberChangeMade(Model.Member a_member)
        {
            if (!m_isInStartup)
            {
                //some change made, save to storage
                m_memberListDAL.SaveMemberList(m_memberList);
            }
        }

        //UserObserver method(s)
        public void UserChangeMade(Model.User a_user)
        {
            if (!m_isInStartup)
            {
                //some change made, save to storage
                m_userListDAL.SaveUserList(m_userList);
            }
        }
    }
}
