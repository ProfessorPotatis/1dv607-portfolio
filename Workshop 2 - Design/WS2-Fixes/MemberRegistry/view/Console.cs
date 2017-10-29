using System;

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
    }
}