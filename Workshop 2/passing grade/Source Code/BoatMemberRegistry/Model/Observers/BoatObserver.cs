using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{
    public interface BoatObserver
    {
        void ChangeMade(Boat a_boat);
    }
}
