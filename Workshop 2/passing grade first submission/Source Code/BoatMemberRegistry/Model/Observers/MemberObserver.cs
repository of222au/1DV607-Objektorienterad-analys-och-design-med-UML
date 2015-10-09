using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{
    public interface MemberObserver
    {
        void ChangeMade(Member a_member);
    }
}
