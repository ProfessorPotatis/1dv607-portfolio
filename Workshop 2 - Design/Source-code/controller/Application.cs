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
                    if (a_database.MemberExist(pNum))
                    {
                        a_view.MemberAlreadyExist();
                    } else
                    {
                        string uMemberId = a_database.GenerateUniqueMemberId();
                        a_database.CreateNewMember(name, pNum, uMemberId);
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
                case view.Console.Event.DeleteMember:
                    string memberPNum = a_view.WantsToDeleteMember();
                    if (a_database.DeleteMember(memberPNum))
                    {
                        a_view.MemberDeleted();
                    } else
                    {
                        a_view.MemberDoesNotExist();
                    }
                    break;
                case view.Console.Event.ChangeMemberInfo:
                    string memberPNum2 = a_view.WantsToChangeMemberInfo();
                    if (a_database.MemberExist(memberPNum2))
                    {
                        string[] updatedMemberInfo = a_view.GetUpdatedMemberInfo();
                        string newName = updatedMemberInfo[0];
                        string newPNum = updatedMemberInfo[1];
                        a_database.ChangeMemberInfo(memberPNum2, newName, newPNum);
                    } else
                    {

                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}