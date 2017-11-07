using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.view
{
    class Boat
    {
        private view.Console _console;

        public Boat()
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
                    result = BoatType.Other;
                    break;
            }

            return result;
        }

        public string WantsToRegisterBoat()
        {
            System.Console.WriteLine("Register boat");
            System.Console.WriteLine("-------------");
            string pNum = Console.GetPersonalNumber();

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

            if (Console.CheckInputFieldForContent(newType) && Console.CheckInputFieldForContent(newLength))
            {
                string[] boatInfo = {newType, newLength};
                return boatInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("\nERROR: A new boat type and length has to be filled in.");
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
            string pNum = Console.GetPersonalNumber();

            return pNum;
        }

        public string GetDeleteBoatInfo()
        {
            System.Console.WriteLine("\nBoat index: ");
            System.Console.WriteLine("(ex. 2 if it is the second boat in the list.)");
            string boatIndex = System.Console.ReadLine();

            if (Console.CheckInputFieldForContent(boatIndex))
            {
                return boatIndex;
            }
            else
            {
                throw new IndexOutOfRangeException("\nERROR: A boat index has to be filled in and be a number.");
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
            string pNum = Console.GetPersonalNumber();

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

            if (Console.CheckInputFieldForContent(boatIndex) && Console.CheckInputFieldForContent(newBoatType) && Console.CheckInputFieldForContent(newBoatLength))
            {
                string[] boatInfo = {boatIndex, newBoatType, newBoatLength};
                return boatInfo;
            }
            else
            {
                throw new IndexOutOfRangeException("\nERROR: Boat index, boat type and length has to be filled in.");
            }
        }

        public void BoatInfoUpdated()
        {
            System.Console.WriteLine("\nBoat info was successfully updated.");
        }

        public void MemberDoesNotExist()
        {
            Console.MemberDoesNotExist();
        }

        public void ListMemberInfo(JArray member)
        {
            Console.ListMemberInfo(member);
        }
    }
}