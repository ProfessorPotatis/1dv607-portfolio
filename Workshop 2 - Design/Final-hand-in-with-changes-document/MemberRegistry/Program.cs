using System;

namespace MemberRegistry
{
    class Program
    {
        /* Starting point of the application */
        static void Main(string[] args)
        {
            view.Console v = new view.Console();
            controller.Application c = new controller.Application();

            // Keep running the application until user wants to quit.
            while (c.Run(v)) ;
        }
    }
}
