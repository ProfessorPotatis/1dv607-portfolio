using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Boat
    {
        private view.Boat _view;
        private view.Member _memberView;
        private model.Boat _database;
        private model.Member _memberModel;

        public Boat()
        {
            View = new view.Boat();
            MemberView = new view.Member();
            Database = new model.Boat();
            MemberModel = new model.Member();
        }

        public view.Boat View
        {
            get { return _view; }
            set
            {
                _view = value;
            }
        }

        public view.Member MemberView
        {
            get { return _memberView; }
            set
            {
                _memberView = value;
            }
        }

        public model.Boat Database
        {
            get { return _database; }
            set
            {
                _database = value;
            }
        }

        public model.Member MemberModel
        {
            get { return _memberModel; }
            set
            {
                _memberModel = value;
            }
        }

        public void RegisterBoat()
        {
            string memberPNum = View.WantsToRegisterBoat();
                    
            if (MemberModel.MemberExist(memberPNum))
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
                MemberView.MemberDoesNotExist();
            }
        }

        private string GetBoatType(string newBoatType)
        {
            // Based on user input -> set boat type
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
                default:
                    type = "Other";
                    break;
            }

            return type;
        }

        public void DeleteBoat()
        {
            string memberPNum = View.WantsToDeleteBoat();

            if (MemberModel.MemberExist(memberPNum))
            {
                int numberOfBoatsOwned = PresentMemberBoats(memberPNum);
                string boatToBeDeleted = View.GetDeleteBoatInfo();
                int boatIndex = ValidateBoatIndex(boatToBeDeleted);
                bool boatExist = CheckIfBoatExist(numberOfBoatsOwned, boatIndex);

                if (boatExist) {
                    Database.DeleteBoat(memberPNum, boatIndex);
                    View.BoatDeleted();
                }
            } 
            else
            {
                MemberView.MemberDoesNotExist();
            }
        }

        private int PresentMemberBoats(string memberPNum)
        {
            JArray boatOwner = MemberModel.GetMember(memberPNum);
            int numberOfBoatsOwned = boatOwner[0]["boats"].Count();
            MemberView.ListMemberInfo(boatOwner);

            return numberOfBoatsOwned;
        }

        private int ValidateBoatIndex(string boatToBeDeleted)
        {
            int i;

            if (int.TryParse(boatToBeDeleted, out i))
            {
                return Convert.ToInt32(boatToBeDeleted);
            }
            else
            {
                throw new IndexOutOfRangeException("ERROR: Boat index has to be a number.");
            }
        }

        private bool CheckIfBoatExist(int numberOfBoatsOwned, int boatToBeDeleted)
        {
            if (boatToBeDeleted > numberOfBoatsOwned || boatToBeDeleted == 0)
            {
                throw new IndexOutOfRangeException("\nERROR: Boat index is out of range.");
            } 
            else
            {
                return true;
            }
        }

        public void ChangeBoatInfo()
        {
            string memberPNum = View.WantsToChangeBoatInfo();

            if (MemberModel.MemberExist(memberPNum))
            {
                int numberOfBoatsOwned = PresentMemberBoats(memberPNum);

                string[] updatedBoatInfo = View.GetUpdatedBoatInfo();
                string boatIndex = updatedBoatInfo[0];
                string newBoatType = updatedBoatInfo[1];
                string newBoatLength = updatedBoatInfo[2];

                int boatIndexInt = ValidateBoatIndex(boatIndex);

                bool boatExist = CheckIfBoatExist(numberOfBoatsOwned, boatIndexInt);

                if (boatExist) {
                    string type = GetBoatType(newBoatType);

                    Database.ChangeBoatInfo(memberPNum, boatIndexInt, type, newBoatLength);
                    View.BoatInfoUpdated();
                }
            }
            else
            {
                MemberView.MemberDoesNotExist();
            }
        }
    }
}