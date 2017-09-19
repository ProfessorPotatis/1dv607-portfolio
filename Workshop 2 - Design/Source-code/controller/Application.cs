using System;

namespace MemberRegistry.controller
{
    class Application
    {
        public bool Run(view.Console a_view)
        {
            a_view.PresentInstructions();

            view.Console.Event e;

            e = a_view.GetEvent();
            if (e == view.Console.Event.Quit)
			{
				return false;
            }
            return true;
        }
    }
}