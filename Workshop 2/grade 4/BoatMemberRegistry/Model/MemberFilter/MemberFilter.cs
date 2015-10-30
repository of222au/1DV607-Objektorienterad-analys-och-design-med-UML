using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{

    public class MemberFilter : IMemberFilter
    {
        public enum MemberFilterField
        {
            Name,
            PersonalNumber,
            UniqueId,
            BoatOfType
        }
        public enum MemberFilterOperator
        {
            Equals,
            NotEquals,
            Contains,
            NotContains,
            LargerThan,
            SmallerThan
        }

        private MemberFilterField m_field;
        private MemberFilterOperator m_operator;
        private string m_searchValue;

        public MemberFilter(MemberFilterField a_field, MemberFilterOperator a_operator, string searchValue)
        {
            m_field = a_field;
            m_operator = a_operator;
            m_searchValue = searchValue;
        }

        public bool CheckFilterOnMember(Member a_member)
        {
            List<string> valuesToCheck = new List<string>();
            switch (m_field) {
                case MemberFilterField.Name:
                    valuesToCheck.Add(a_member.Name);
                    break;
                case MemberFilterField.PersonalNumber:
                    valuesToCheck.Add(a_member.PersonalNumber);
                    break;
                case MemberFilterField.UniqueId:
                    valuesToCheck.Add(a_member.UniqueId);
                    break;
                case MemberFilterField.BoatOfType:
                    foreach (Boat boat in a_member.Boats)
                    {
                        valuesToCheck.Add(boat.TypeAsString);
                    }
                    break;
            }

            foreach (string value in valuesToCheck)
            {
                if (CheckValue(value, m_searchValue))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckValue(string value, string searchValue)
        {
            if (m_operator == MemberFilterOperator.Equals)
            {
                return (value.ToLower() == searchValue.ToLower());
            }
            else if (m_operator == MemberFilterOperator.NotEquals)
            {
                return (value.ToLower() != searchValue.ToLower());
            }
            else if (m_operator == MemberFilterOperator.Contains)
            {
                return (value.ToLower().Contains(searchValue.ToLower()));
            }
            else if (m_operator == MemberFilterOperator.NotContains)
            {
                return (!value.ToLower().Contains(searchValue.ToLower()));
            }
            else if (m_operator == MemberFilterOperator.LargerThan ||
                     m_operator == MemberFilterOperator.SmallerThan)
            {
                double n_value, n_searchValue;
                if (double.TryParse(value, out n_value) && double.TryParse(value, out n_searchValue))
                {
                    if (m_operator == MemberFilterOperator.LargerThan)
                    {
                        return (n_value > n_searchValue);
                    }
                    else if (m_operator == MemberFilterOperator.SmallerThan)
                    {
                        return (n_value < n_searchValue);
                    }
                }
                else
                {
                    //no numeric parsing could be done, no use to compare then
                    return false;
                }
            }

            return false;
        }
    }
}
