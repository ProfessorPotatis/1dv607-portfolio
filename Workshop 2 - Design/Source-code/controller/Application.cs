using System;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Application
    {
        public bool Run(view.Console a_view, model.Database a_database)
        {
            a_view.DisplayInstructions();

            view.Console.Event e;
            e = a_view.GetEvent();

            switch (e)
            {
                case view.Console.Event.Quit:
                    return false;
                case view.Console.Event.ShowInstructions:
                    a_view.ShowInstructions();
                    break;
                case view.Console.Event.CreateMember:
                    string[] memberInfo = a_view.WantsToCreateNewMember();
                    string name = memberInfo[0];
                    string pNum = memberInfo[1];
                    if (a_database.MemberExist(name, pNum))
                    {
                        a_view.MemberAlreadyExist();
                    } else
                    {
                        a_view.NewMemberCreated();
                    }
                    break;
                case view.Console.Event.CompactMembersList:
                    JArray members = a_database.GetAllMembers();
                    a_view.ListMembersCompact(members);
                    break;
                case view.Console.Event.VerboseMembersList:
                    JArray members2 = a_database.GetAllMembers();
                    a_view.ListMembersVerbose(members2);
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}