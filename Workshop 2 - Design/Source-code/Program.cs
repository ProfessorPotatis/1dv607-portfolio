using System;

namespace MemberRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            view.Console v = new view.Console();
            controller.Application c = new controller.Application();

            while (c.Run(v)) ;
        }
    }
}
