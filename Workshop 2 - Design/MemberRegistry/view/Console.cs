using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.view
{
    class Console
    {
        public void DisplayInstructions()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Welcome to the BoatClub member registry!");
            System.Console.WriteLine("help - to show instructions");
            System.Console.WriteLine();
        }

        public Event GetEvent()
        {
            Event result;

            string command = System.Console.ReadLine();

            switch (command)
            {
                case "cm":
                    result = Event.CreateMember;
                    break;
                case "lc":
                    result = Event.CompactMembersList;
                    break;
                case "lv":
                    result = Event.VerboseMembersList;
                    break;
                case "dm":
                    result = Event.DeleteMember;
                    break;
                case "ci":
                    result = Event.ChangeMemberInfo;
                    break;
                case "lm":
                   result = Event.LookMemberInfo;
                    break;
                case "rb":
                    result = Event.RegisterBoat;
                    break;
                case "db":
                    result = Event.DeleteBoat;
                    break;
                case "bi":
                    result = Event.ChangeBoatInfo;
                    break;
                case "help":
                    result = Event.ShowInstructions;
                    break;
                case "q":
                    result = Event.Quit;
                    break;
                default:
                    result = Event.None;
                    break;
            }

            return result;
        }

        public BoatType GetBoatType(string type)
        {
            BoatType result;

            switch (type)
            {
                case "s":
                    result = BoatType.Sailboat;
                    break;
                case "m":
                    result = BoatType.Motorsailer;
                    break;
                case "k":
                    result = BoatType.KayakOrCanoe;
                    break;
                case "o":
                    result = BoatType.Other;
                    break;
                default:
                    result = BoatType.None;
                    break;
            }

            return result;
        }

        public void ShowInstructions()
        {
            System.Console.WriteLine("cm - to create a new member");
            System.Console.WriteLine("lc - for a compact members list");
            System.Console.WriteLine("lv - for a verbose members list");
            System.Console.WriteLine("dm - to delete a member");
            System.Console.WriteLine("ci - to change members info");
            System.Console.WriteLine("lm - to look at specific members info");
            System.Console.WriteLine("rb - to register a new boat");
            System.Console.WriteLine("db - to delete a boat");
            System.Console.WriteLine("bi - to change boats info");
            System.Console.WriteLine("q - to quit");
        }

        public string[] WantsToCreateNewMember()
        {
            System.Console.WriteLine("Create new member");
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("Full name: ");
            string name = System.Console.ReadLine();
            string pNum = GetPersonalNumber();

            if (CheckInputFieldForContent(name))
            {
                string[] memberInfo = {name, pNum};
                return memberInfo;
            } 
            else
            {
                throw new IndexOutOfRangeException("ERROR: Name has to be filled in.");
            }

            
        }

        private bool CheckInputFieldForContent(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

        private string GetPersonalNumber()
        {
            System.Console.WriteLine("Personal number (YYMMDD-xxxx): ");
            string pNum = System.Console.ReadLine();

            if (CheckInputFieldForContent(pNum))
            {
                return pNum;
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: Personal number has to be filled in.");
            }
        }

        public void MemberAlreadyExist()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Sorry. A member with that personal number already exists.");
        }

        public void NewMemberCreated()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Member was successfully saved to the database.");
        }

        public void ListMembersCompact(JArray members)
        {
            for (int i = 0; i < members.Count; i++)
            {
                System.Console.WriteLine("\nName: " + members[i]["name"] + "\nMemberId: " + members[i]["uMemberId"] + "\nNumber of boats: " + members[i]["boats"].Count());
            }
        }

        public void ListMembersVerbose(JArray members)
        {
            for (int i = 0; i < members.Count; i++)
            {
                System.Console.WriteLine("\nName: " + members[i]["name"] + "\nPersonal number: " + members[i]["pNum"] + "\nMemberId: " + members[i]["uMemberId"]);
                System.Console.WriteLine("Boats: ");
                for (int x = 0; x < members[i]["boats"].Count(); x++)
                {
                    System.Console.WriteLine("  " + members[i]["boats"][x]["type"] + ", " + members[i]["boats"][x]["length"]);
                }
            }
        }

        public string WantsToDeleteMember()
        {
            System.Console.WriteLine("Delete member");
            System.Console.WriteLine("-------------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public void MemberDeleted()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Member was successfully deleted.");
        }

        public void MemberDoesNotExist()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Sorry. Member does not exist.");
        }

        public string WantsToChangeMemberInfo()
        {
            System.Console.WriteLine("Change member info");
            System.Console.WriteLine("------------------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public string[] GetUpdatedMemberInfo()
        {
            System.Console.WriteLine("New full name: ");
            string newName = System.Console.ReadLine();
            string newPNum = GetPersonalNumber();

            if (CheckInputFieldForContent(newName))
            {
                string[] memberInfo = {newName, newPNum};
                return memberInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: A new name has to be filled in.");
            }
        }

        public string WantsToLookAtMemberInfo()
        {
            System.Console.WriteLine("Look at specific member info");
            System.Console.WriteLine("----------------------------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public void ListMemberInfo(JArray member)
        {
            ListMembersVerbose(member);
        }

        public string WantsToRegisterBoat()
        {
            System.Console.WriteLine("Register boat");
            System.Console.WriteLine("-------------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public string[] GetNewBoatInfo()
        {
            System.Console.WriteLine("\nBoat type: ");
            System.Console.WriteLine("(s for Sailboat, m for Motorsailer, k for Kayak/Canoe, o for Other)");
            string newType = System.Console.ReadLine();
            System.Console.WriteLine("\nBoat length: ");
            System.Console.WriteLine("(in meters)");
            string newLength = System.Console.ReadLine();

            if (CheckInputFieldForContent(newType) && CheckInputFieldForContent(newLength))
            {
                string[] boatInfo = {newType, newLength};
                return boatInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: A new boat type and length has to be filled in.");
            }
        }

        public void BoatRegistered()
        {
            System.Console.WriteLine("\nBoat was successfully registered.");
        }

        public string WantsToDeleteBoat()
        {
            System.Console.WriteLine("Delete boat");
            System.Console.WriteLine("-----------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public string GetDeleteBoatInfo()
        {
            System.Console.WriteLine("\nBoat index: ");
            System.Console.WriteLine("(ex. 2 if it is the second boat in the list.)");
            string boatIndex = System.Console.ReadLine();

            if (CheckInputFieldForContent(boatIndex))
            {
                return boatIndex;
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: A boat index has to be filled in.");
            }
        }

        public void BoatDeleted()
        {
            System.Console.WriteLine("\nBoat was successfully deleted.");
        }

        public string WantsToChangeBoatInfo()
        {
            System.Console.WriteLine("Change boat info");
            System.Console.WriteLine("----------------");
            string pNum = GetPersonalNumber();

            return pNum;
        }

        public string[] GetUpdatedBoatInfo()
        {
            System.Console.WriteLine("\nBoat index: ");
            System.Console.WriteLine("(ex. 2 if it is the second boat in the list.)");
            string boatIndex = System.Console.ReadLine();
            System.Console.WriteLine("\nNew boat type: ");
            System.Console.WriteLine("(s for Sailboat, m for Motorsailer, k for Kayak/Canoe, o for Other)");
            string newBoatType = System.Console.ReadLine();
            System.Console.WriteLine("\nNew boat length: ");
            System.Console.WriteLine("(in meters)");
            string newBoatLength = System.Console.ReadLine();

            if (CheckInputFieldForContent(boatIndex) && CheckInputFieldForContent(newBoatType) && CheckInputFieldForContent(newBoatLength))
            {
                string[] boatInfo = {boatIndex, newBoatType, newBoatLength};
                return boatInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: Boat index, boat type and length has to be filled in.");
            }
        }

        public void BoatInfoUpdated()
        {
            System.Console.WriteLine("\nBoat info was successfully updated.");
        }

        public void WriteException(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }
}