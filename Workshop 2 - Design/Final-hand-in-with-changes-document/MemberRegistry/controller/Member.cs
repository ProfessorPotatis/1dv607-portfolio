using System;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.controller
{
    class Member
    {
        private view.Member _view;
        private model.Member _database;

        public Member()
        {
            View = new view.Member();
            Database = new model.Member();
        }

        public view.Member View
        {
            get { return _view; }
            set
            {
                _view = value;
            }
        }

        public model.Member Database
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
                View.MemberInfoUpdated();
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