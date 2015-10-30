using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.View
{
    public enum ActionEnum
    {
        GoToMainMenu,
        Quit,

        MemberListCompact,
        MemberListVerbose,

        MemberSelection,
        MemberSelected,
        MemberSelected_ShowMenuAgain,

        MemberSearch,
        MemberSearchByName,
        MemberSearchByBoatType,
        MemberSearched,

        MemberPresentation,

        MemberEdit,
        MemberEditedName,
        MemberEditedPersonalNumber,

        MemberAdd,
        MemberAddingName,
        MemberAddingPersonalNumber,

        MemberDelete,
        MemberConfirmDelete,

        MemberBoatSelectionEdit,
        MemberBoatSelectedEdit,

        MemberBoatEditedType,
        MemberBoatEditedLength,

        MemberBoatAdd,
        MemberBoatAddingType,
        MemberBoatAddingLength,

        MemberBoatSelectionDelete,
        MemberBoatSelectedDelete,
        MemberBoatConfirmDelete,

        Login,
        LoginSpecifyingName,
        LoginSpecifyingPassword,

        NotSpecified
    }
}
