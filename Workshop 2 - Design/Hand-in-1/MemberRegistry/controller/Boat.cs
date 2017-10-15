using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Boat
    {
        private view.Console _view;
        private model.Database _database;

        public Boat(view.Console a_view, model.Database a_database)
        {
            View = a_view;
            Database = a_database;
        }

        public view.Console View
        {
            get { return _view; }
            set
            {
                _view = value;
            }
        }

        public model.Database Database
        {
            get { return _database; }
            set
            {
                _database = value;
            }
        }

        public void RegisterBoat()
        {
            string memberPNum = View.WantsToRegisterBoat();
                    
            if (Database.MemberExist(memberPNum))
            {
                string[] boatToBeRegistered = View.GetNewBoatInfo();
                string newType = boatToBeRegistered[0];
                string newLength = boatToBeRegistered[1];

                string type = GetBoatType(newType);

                Database.RegisterBoat(memberPNum, type, newLength);
                View.BoatRegistered();
            } 
            else
            {
                View.MemberDoesNotExist();
            }
        }

        private string GetBoatType(string newBoatType)
        {
            view.BoatType b;
            b = View.GetBoatType(newBoatType);

            string type = "";

            switch (b)
            {
                case view.BoatType.Sailboat:
                    type = "Sailboat";
                    break;
                case view.BoatType.Motorsailer:
                    type = "Motorsailer";
                    break;
                case view.BoatType.KayakOrCanoe:
                    type = "Kayak/Canoe";
                    break;
                case view.BoatType.Other:
                    type = "Other";
                    break;
                case view.BoatType.None:
                    type = "None";
                    break;
                default:
                    break;
            }

            return type;
        }

        public void DeleteBoat()
        {
            string memberPNum = View.WantsToDeleteBoat();

            if (Database.MemberExist(memberPNum))
            {
                int i;
                JArray boatOwner = Database.GetMember(memberPNum);
                int numberOfBoats = boatOwner[0]["boats"].Count();
                View.ListMemberInfo(boatOwner);
                string boatToBeDeleted = View.GetDeleteBoatInfo();

                if (int.TryParse(boatToBeDeleted, out i))
                {
                    int boatToBeDeletedInt = Convert.ToInt32(boatToBeDeleted);
                    if (boatToBeDeletedInt > numberOfBoats || boatToBeDeletedInt == 0)
                    {
                        throw new IndexOutOfRangeException("ERROR: Boat index is out of range.");
                    } 
                    else
                    {
                        Database.DeleteBoat(memberPNum, boatToBeDeletedInt);
                        View.BoatDeleted();
                    }
                } 
                else
                {
                    throw new IndexOutOfRangeException("ERROR: Boat index has to be a number.");
                }

                
            } 
            else
            {
                View.MemberDoesNotExist();
            }
        }

        public void ChangeBoatInfo()
        {
            string memberPNum = View.WantsToChangeBoatInfo();

            if (Database.MemberExist(memberPNum))
            {
                int i;
                JArray boatOwner = Database.GetMember(memberPNum);
                int numberOfBoats = boatOwner[0]["boats"].Count();
                View.ListMemberInfo(boatOwner);

                string[] updatedBoatInfo = View.GetUpdatedBoatInfo();
                string boatIndex = updatedBoatInfo[0];
                string newBoatType = updatedBoatInfo[1];
                string newBoatLength = updatedBoatInfo[2];

                if (int.TryParse(boatIndex, out i))
                {
                    int boatIndexInt = Convert.ToInt32(boatIndex);
                    if (boatIndexInt > numberOfBoats || boatIndexInt == 0)
                    {
                        throw new IndexOutOfRangeException("ERROR: Boat index is out of range.");
                    } 
                    else
                    {
                        string type = GetBoatType(newBoatType);

                        Database.ChangeBoatInfo(memberPNum, boatIndexInt, type, newBoatLength);
                        View.BoatInfoUpdated();
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("ERROR: Boat index has to be a number.");
                }
            }
            else
            {
                View.MemberDoesNotExist();
            }
        }
    }
}