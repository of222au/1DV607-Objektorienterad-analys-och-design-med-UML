using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    class Program
    {
        const bool c_runConsoleBased = false; //need to change output type in application (project) settings as well

        static void Main(string[] args)
        {
            model.rules.IRulesFactory rulesFactory = new model.rules.AmericanRulesFactory();
            model.Game g = new model.Game(rulesFactory);

            if (c_runConsoleBased) 
            {
                view.IView v = new view.SwedishView(); // new view.SwedishView();
                controller.PlayGame ctrl = new controller.PlayGame(g, v);
                while (ctrl.Play());
            }
            else
            {
                BlackJack.view.FormView form = new BlackJack.view.FormView();
                view.IView v = form; // new view.SwedishView();
                controller.PlayGame ctrl = new controller.PlayGame(g, v);
                ctrl.Play();

                Application.Run(form);
            }
        }
    }
}
