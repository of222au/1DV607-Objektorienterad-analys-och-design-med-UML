using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.View
{
    class InputAction
    {

        private string m_characters;
        private View.ActionEnum m_action;

        //Constructor
        public InputAction(string characters, View.ActionEnum action)
        {
            m_characters = characters;
            m_action = action;
        }

        //Properties
        public string Characters
        {
            get
            {
                return m_characters;
            }
        }
        public View.ActionEnum Action
        {
            get
            {
                return m_action;
            }
        }

        //Methods
        public bool CheckAgainstInput(string input)
        {
            //if the input characters matches the accepted possible input
            if (input.ToLower() == this.Characters.ToLower() ||
                this.Characters == "")
            {
                return true;
            }

            return false;
        }
    }
}
