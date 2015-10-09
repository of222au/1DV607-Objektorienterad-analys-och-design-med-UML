using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.View
{
    public enum DisplayMembersMode
    {
        Compact,
        Verbose
    }
    public enum AddEditMemberOrBoatMode
    {
        Add_Member,
        Edit_Member,
        Add_Boat,
        Edit_Boat
    }
    public enum AddEditMemberAttribute
    {
        Name,
        Personal_Number,
        None
    }
    public enum AddEditBoatAttribute
    {
        Type,
        Length,
        None
    }
}
