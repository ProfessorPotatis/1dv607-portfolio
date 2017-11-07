using System;
using System.Text.RegularExpressions;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.view
{
    class Console
    {
        public void DisplayInstructions()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("-----------------------------");
            System.Console.WriteLine("The Boat Club Member Registry");
            System.Console.WriteLine("help - to show menu");
            System.Console.WriteLine("-----------------------------");
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

        public void ShowInstructions()
        {
            System.Console.WriteLine("Menu");
            System.Console.WriteLine("----");
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

        public void WriteException(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }

        public string GetPersonalNumber()
        {
            System.Console.WriteLine("Personal number (YYMMDD-xxxx): ");
            string pNum = System.Console.ReadLine();

            if (CheckPersonalNumber(pNum))
            {
                return pNum;
            }
            else
            {
                throw new IndexOutOfRangeException("\nERROR: Personal number has to match YYMMDD-xxxx.");
            }
        }

        private bool CheckPersonalNumber(string pNum) {
            Regex personalNumber = new Regex(@"^[0-9]{6}-[0-9]{4}$");
            Match match = personalNumber.Match(pNum);
            
            if (CheckInputFieldForContent(pNum) && match.Success)
            {
                return true;
            }
            return false;
        }

        public bool CheckInputFieldForContent(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            return false;
        }

        public void MemberDoesNotExist()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Sorry. Member does not exist.");
        }

        public void ListMemberInfo(JArray member)
        {
            System.Console.WriteLine("Member");
            System.Console.WriteLine("------");

            System.Console.WriteLine("\nName: " + member[0]["name"] + "\nPersonal number: " + member[0]["pNum"] + "\nMember ID: " + member[0]["uMemberId"]);
            System.Console.WriteLine("Boats: ");
            for (int x = 0; x < member[0]["boats"].Count(); x++)
            {
                System.Console.WriteLine("  " + member[0]["boats"][x]["type"] + ", " + member[0]["boats"][x]["length"]);
            }
        }
    }
}