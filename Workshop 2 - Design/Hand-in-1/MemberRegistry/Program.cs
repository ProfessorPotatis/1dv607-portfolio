using System;

namespace MemberRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            model.Database m = new model.Database();
            view.Console v = new view.Console();
            controller.Application c = new controller.Application();

            while (c.Run(v, m)) ;
        }
    }
}
