using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{
    public class MemberFiltering
    {
        private MemberFilters m_filters;

        public MemberFiltering(MemberFilters filters)
        {
            m_filters = filters;
        }

        public List<Member> FilterMembers(IEnumerable<Member> members)
        {
            return m_filters.FilterMembers(members);
        }
    }
}
