using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoatMemberRegistry
{
    class Program
    {

        static void Main(string[] args)
        {
            View.Console view = new View.Console(); //memberRegistry.MemberList);
            Controller.MemberRegistry memberRegistry = new Controller.MemberRegistry(view);

            memberRegistry.UserList.AddUser(new Model.User("admin", "password"));

            memberRegistry.UseSystem();

        }
    }
}
