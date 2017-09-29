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
                        a_view.MemberDoesNotExist();
                    }
                    break;
                case view.Console.Event.LookMemberInfo:
                    string memberPNum3 = a_view.WantsToLookAtMemberInfo();
                    JArray choosenMember = a_database.GetMember(memberPNum3);

                    if (choosenMember.Count == 0)
                    {
                        a_view.MemberDoesNotExist();
                    } else
                    {
                        a_view.ListMemberInfo(choosenMember);
                    }
                    break;
                case view.Console.Event.RegisterBoat:
                    string memberPNum4 = a_view.WantsToRegisterBoat();
                    
                    if (a_database.MemberExist(memberPNum4))
                    {
                        string[] boatToBeRegistered = a_view.GetNewBoatInfo();
                        string newType = boatToBeRegistered[0];
                        string newLength = boatToBeRegistered[1];

                        view.Console.BoatType b;
                        b = a_view.GetBoatType(newType);

                        string type = "";

                        switch (b)
                        {
                            case view.Console.BoatType.Sailboat:
                                type = "Sailboat";
                                break;
                            case view.Console.BoatType.Motorsailer:
                                type = "Motorsailer";
                                break;
                            case view.Console.BoatType.KayakOrCanoe:
                                type = "Kayak/Canoe";
                                break;
                            case view.Console.BoatType.Other:
                                type = "Other";
                                break;
                            case view.Console.BoatType.None:
                                type = "None";
                                break;
                            default:
                                break;
                        }

                        a_database.RegisterBoat(memberPNum4, type, newLength);

                        a_view.BoatRegistered();
                    } else
                    {
                        a_view.MemberDoesNotExist();
                    }
                    break;
                case view.Console.Event.DeleteBoat:
                    string memberPNum5 = a_view.WantsToDeleteBoat();
                    if (a_database.MemberExist(memberPNum5))
                    {
                        JArray boatOwner = a_database.GetMember(memberPNum5);
                        a_view.ListMemberInfo(boatOwner);
                        int boatToBeDeleted = a_view.GetDeleteBoatInfo();
                        a_database.DeleteBoat(memberPNum5, boatToBeDeleted);
                        a_view.BoatDeleted();
                    } else
                    {
                        a_view.MemberDoesNotExist();
                    }
                    break;
                case view.Console.Event.ChangeBoatInfo:
                    string memberPNum6 = a_view.WantsToChangeBoatInfo();
                    if (a_database.MemberExist(memberPNum6))
                    {
                        JArray boatOwner2 = a_database.GetMember(memberPNum6);
                        a_view.ListMemberInfo(boatOwner2);

                        string[] updatedBoatInfo = a_view.GetUpdatedBoatInfo();
                        int boatIndex = Convert.ToInt32(updatedBoatInfo[0]);
                        string newBoatType = updatedBoatInfo[1];
                        string newBoatLength = updatedBoatInfo[2];

                        view.Console.BoatType b;
                        b = a_view.GetBoatType(newBoatType);

                        string type = "";

                        switch (b)
                        {
                            case view.Console.BoatType.Sailboat:
                                type = "Sailboat";
                                break;
                            case view.Console.BoatType.Motorsailer:
                                type = "Motorsailer";
                                break;
                            case view.Console.BoatType.KayakOrCanoe:
                                type = "Kayak/Canoe";
                                break;
                            case view.Console.BoatType.Other:
                                type = "Other";
                                break;
                            case view.Console.BoatType.None:
                                type = "None";
                                break;
                            default:
                                break;
                        }

                        a_database.ChangeBoatInfo(memberPNum6, boatIndex, type, newBoatLength);
                        a_view.BoatInfoUpdated();
                    } else
                    {
                        a_view.MemberDoesNotExist();
                    }
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}