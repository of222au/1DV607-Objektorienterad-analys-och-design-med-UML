using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.View
{
    class MemberView
    {
        private static string newLine = System.Environment.NewLine;

        //Constructor
        public MemberView()
        {
        }


        //Methods

        public string GetMemberPresentation(Model.Member a_member, DisplayMembersMode mode, bool useIndentation = false)
        {
            string output = "";
            if (mode == DisplayMembersMode.Compact)
            {
                output = (useIndentation ? " - " : "") + a_member.Name + " (" + a_member.UniqueId + ") " + a_member.BoatCount + " boat(s)";
            }
            else //verbose mode
            {
                //member's info
                output = (useIndentation ? " - " : "") + a_member.Name + " " + a_member.PersonalNumber + " (" + a_member.UniqueId + ") ";

                //and boat info
                int boatCount = a_member.BoatCount;
                if (boatCount > 0)
                {
                    //output += newLine;
                    output += boatCount + " boat(s)" + newLine; //(useIndentation ? "     " : "") +
                    foreach (Model.Boat boat in a_member.Boats)
                    {
                        output += this.GetBoatPresentation(boat, false, useIndentation, true) + newLine;
                    }
                }
                else
                {
                    output += "No boat(s)" + newLine;
                }
            }

            return output;
        }

        public string GetDetailedMemberPresentation(Model.Member a_member)
        {
            int boatCount = a_member.BoatCount;

            string output = "";
            output += "   Name: " + a_member.Name + newLine;
            output += "   Personal no: " + a_member.PersonalNumber + newLine;
            output += "   Member id: " + a_member.UniqueId + newLine;
            output += newLine;
            output += "   " + (boatCount > 0 ? boatCount.ToString() : "No") + " boat" + (boatCount > 1 ? "s" : "") + (boatCount > 0 ? ":" : "") + newLine;
            if (boatCount > 0)
            {
                for (int i = 0; i < a_member.BoatCount; i++)
                {
                    //output += "     " + (i + 1) + ": " + newLine;
                    output += this.GetBoatPresentation(a_member.Boats[i], true, true);
                    output += newLine;
                }
            }

            return output;
        }

        public string GetBoatPresentation(Model.Boat a_boat, bool detailed = false, bool useIndentation = false, bool useListCharacter = false)
        {
            string output = "";
            if (detailed)
            {
                output += (useIndentation ? "     " : "") + "Type: " + a_boat.TypeAsString + newLine;
                output += (useIndentation ? "     " : "") + "Length: " + a_boat.Length + " meters" + newLine;
            }
            else
            {
                output += (useIndentation ? "     " : "") + (useListCharacter ? "- " : "") + a_boat.TypeAsString + " " + a_boat.Length + " meters";
            }

            return output;
        }
    }
}
