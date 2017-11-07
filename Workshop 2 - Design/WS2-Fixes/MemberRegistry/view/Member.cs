using System;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.view
{
    class Member
    {
        private view.Console _console;

        public Member()
        {
            Console = new view.Console();
        }

        public view.Console Console
        {
            get { return _console; }
            set
            {
                _console = value;
            }
        }

        public string[] WantsToCreateNewMember()
        {
            System.Console.WriteLine("Create new member");
            System.Console.WriteLine("-----------------");
            System.Console.WriteLine("Full name: ");
            string name = System.Console.ReadLine();
            string pNum = Console.GetPersonalNumber();

            if (Console.CheckInputFieldForContent(name))
            {
                string[] memberInfo = {name, pNum};
                return memberInfo;
            } 
            else
            {
                throw new IndexOutOfRangeException("\nERROR: Name has to be filled in.");
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
            System.Console.WriteLine("Compact member list");
            System.Console.WriteLine("-------------------");
            for (int i = 0; i < members.Count; i++)
            {
                System.Console.WriteLine("\nName: " + members[i]["name"] + "\nMember ID: " + members[i]["uMemberId"] + "\nNumber of boats: " + members[i]["boats"].Count());
            }
        }

        public void ListMembersVerbose(JArray members)
        {
            System.Console.WriteLine("Verbose member list");
            System.Console.WriteLine("-------------------");
            for (int i = 0; i < members.Count; i++)
            {
                System.Console.WriteLine("\nName: " + members[i]["name"] + "\nPersonal number: " + members[i]["pNum"] + "\nMember ID: " + members[i]["uMemberId"]);
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
            string pNum = Console.GetPersonalNumber();

            return pNum;
        }

        public void MemberDeleted()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Member was successfully deleted.");
        }

        public void MemberDoesNotExist()
        {
            Console.MemberDoesNotExist();
        }

        public string WantsToChangeMemberInfo()
        {
            System.Console.WriteLine("Change member info");
            System.Console.WriteLine("------------------");
            string pNum = Console.GetPersonalNumber();

            return pNum;
        }

        public string[] GetUpdatedMemberInfo()
        {
            System.Console.WriteLine("New full name: ");
            string newName = System.Console.ReadLine();
            string newPNum = Console.GetPersonalNumber();

            if (Console.CheckInputFieldForContent(newName))
            {
                string[] memberInfo = {newName, newPNum};
                return memberInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("\nERROR: A new name has to be filled in.");
            }
        }

        public void MemberInfoUpdated()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Member info was successfully updated.");
        }

        public string WantsToLookAtMemberInfo()
        {
            System.Console.WriteLine("Look at specific member info");
            System.Console.WriteLine("----------------------------");
            string pNum = Console.GetPersonalNumber();

            return pNum;
        }

        public void ListMemberInfo(JArray member)
        {
            Console.ListMemberInfo(member);
        }

    }
}