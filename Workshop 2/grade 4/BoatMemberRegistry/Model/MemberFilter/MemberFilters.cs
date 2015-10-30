using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{
    public class MemberFilters : IMemberFilter
    {
        public enum MemberFiltersConditionalOperator 
        {
            AND,
            OR
        }

        private List<MemberFilter> m_filters;
        private List<MemberFilters> m_nestedFilters;
        private MemberFiltersConditionalOperator m_conditionalOperator;

        public MemberFilters(List<MemberFilter> filters, MemberFiltersConditionalOperator a_conditionalOperator = MemberFiltersConditionalOperator.AND, List<MemberFilters> nestedFilters = null)
        {
            SetFilters(filters, a_conditionalOperator, nestedFilters);
        }

        public void SetFilters(List<MemberFilter> filters, MemberFiltersConditionalOperator a_conditionalOperator = MemberFiltersConditionalOperator.AND, List<MemberFilters> nestedFilters = null)
        {
            m_filters = filters;
            m_nestedFilters = (nestedFilters != null ? nestedFilters : new List<MemberFilters>());
            m_conditionalOperator = a_conditionalOperator;
        }

        private List<IMemberFilter> GetAllFilters()
        {
            List<IMemberFilter> filters = new List<IMemberFilter>();

            //loop MemberFilter
            foreach (MemberFilter filter in m_filters)
            {
                filters.Add(filter);
            }
            //loop MemberFilters
            foreach (MemberFilters nestedFilter in m_nestedFilters)
            {
                filters.Add(nestedFilter);
            }

            return filters;
        }

        public List<Member> FilterMembers(IEnumerable<Member> members)
        {
            List<Member> membersFiltered = new List<Member>();

            foreach (Member member in members)
            {
                //if success
                if (CheckFilterOnMember(member) == true)
                {
                    membersFiltered.Add(member);
                }
            }

            return membersFiltered;
        }


        public bool CheckFilterOnMember(Member member)
        {
            bool? memberResult = null;

            //loop filters
            foreach (IMemberFilter filter in GetAllFilters())
            {
                bool result = filter.CheckFilterOnMember(member);
                if (result == true && m_conditionalOperator == MemberFiltersConditionalOperator.OR)
                {
                    memberResult = true;
                }
                else if (result == false && m_conditionalOperator == MemberFiltersConditionalOperator.AND)
                {
                    memberResult = false;
                }

                if (memberResult != null)
                {
                    break;
                }
            }
           
            //if AND operator and no failure, set success
            if (m_conditionalOperator == MemberFiltersConditionalOperator.AND && memberResult != false)
            {
                memberResult = true;
            }

            return (memberResult == true);
        }
    }
}
