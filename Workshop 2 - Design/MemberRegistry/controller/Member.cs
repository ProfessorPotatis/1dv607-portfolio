using System;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Member
    {
        private view.Console _view;
        private model.Database _database;

        public Member(view.Console a_view, model.Database a_database)
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

        public void CreateMember()
        {
            string[] memberInfo = View.WantsToCreateNewMember();
            string name = memberInfo[0];
            string pNum = memberInfo[1];

            if (Database.MemberExist(pNum))
            {
                View.MemberAlreadyExist();
            }
            else
            {
                string uMemberId = Database.GenerateUniqueMemberId();
                Database.CreateNewMember(name, pNum, uMemberId);
                View.NewMemberCreated();
            }
        }

        public void ListMembersCompact()
        {
            JArray members = Database.GetAllMembers();
            View.ListMembersCompact(members);
        }

        public void ListMembersVerbose()
        {
            JArray members = Database.GetAllMembers();
            View.ListMembersVerbose(members);
        }

        public void DeleteMember()
        {
            string memberPNum = View.WantsToDeleteMember();
            if (Database.DeleteMember(memberPNum))
            {
                View.MemberDeleted();
            }
            else
            {
                View.MemberDoesNotExist();
            }
        }

        public void ChangeMemberInfo()
        {
            string memberPNum = View.WantsToChangeMemberInfo();
            if (Database.MemberExist(memberPNum))
            {
                string[] updatedMemberInfo = View.GetUpdatedMemberInfo();
                string newName = updatedMemberInfo[0];
                string newPNum = updatedMemberInfo[1];
                Database.ChangeMemberInfo(memberPNum, newName, newPNum);
            }
            else
            {
                View.MemberDoesNotExist();
            }
        }

        public void ListMemberInfo()
        {
            string memberPNum = View.WantsToLookAtMemberInfo();
            JArray choosenMember = Database.GetMember(memberPNum);

            if (choosenMember.Count == 0)
            {
                View.MemberDoesNotExist();
            }
            else
            {
                View.ListMemberInfo(choosenMember);
            }
        }
    }
}