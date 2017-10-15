using System;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Application
    {
        public bool Run(view.Console a_view)
        {
            try
            {
                a_view.DisplayInstructions();

                controller.Member memberController = new controller.Member();
                controller.Boat boatController = new controller.Boat();

                // Based on user input -> call function
                view.Event e;
                e = a_view.GetEvent();

                switch (e)
                {
                    case view.Event.Quit:
                        return false;
                    case view.Event.ShowInstructions:
                        a_view.ShowInstructions();
                        break;
                    case view.Event.CreateMember:
                        memberController.CreateMember();
                        break;
                    case view.Event.CompactMembersList:
                        memberController.ListMembersCompact();
                        break;
                    case view.Event.VerboseMembersList:
                        memberController.ListMembersVerbose();
                        break;
                    case view.Event.DeleteMember:
                        memberController.DeleteMember();
                        break;
                    case view.Event.ChangeMemberInfo:
                        memberController.ChangeMemberInfo();
                        break;
                    case view.Event.LookMemberInfo:
                        memberController.ListMemberInfo();
                        break;
                    case view.Event.RegisterBoat:
                        boatController.RegisterBoat();
                        break;
                    case view.Event.DeleteBoat:
                        boatController.DeleteBoat();
                        break;
                    case view.Event.ChangeBoatInfo:
                        boatController.ChangeBoatInfo();
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                a_view.WriteException(ex);
                return true;
            }
            finally
            {
                //Console.WriteLine("Finally, we are done!");
            }
            
        }
    }
}