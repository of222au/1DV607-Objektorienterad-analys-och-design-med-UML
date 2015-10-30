using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.view
{
    interface InputActionObserver
    {
        void Input(InputAction inputAction);
    }
}
