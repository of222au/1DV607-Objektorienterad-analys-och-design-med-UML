﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry.Model
{
    public interface UserObserver
    {
        void UserChangeMade(User a_user);
    }
}
