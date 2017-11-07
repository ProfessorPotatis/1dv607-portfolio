using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Member
    {
        private model.Database _database;

        public Member()
        {
            Database = new model.Database();
        }

        public model.Database Database
        {
            get { return _database; }
            set
            {
                _database = value;
            }
        }

        public string GenerateUniqueMemberId()
        {
            Random rnd = new Random();
            int num1 = rnd.Next(1000);
            int num2 = rnd.Next(100);

            string memberId = "Member" + num1 + num2;

            return memberId;
        }

        public void CreateNewMember(string name, string pNum, string uMemberId)
        {
            string json = Database.ReadJsonFile();
            string newMember = "{\"name\": \"" + name + "\", \"pNum\": \"" + pNum + "\", \"uMemberId\": \"" + uMemberId + "\"}";
            JObject member = JObject.Parse(newMember);
            JObject memberJsonObj = JObject.Parse(json);

            member["boats"] = new JArray();
            
            memberJsonObj["members"][0].AddAfterSelf(
                new JObject(
                new JProperty("name", member["name"]),
                new JProperty("pNum", member["pNum"]),
                new JProperty("uMemberId", member["uMemberId"]),
                new JProperty("boats", member["boats"])
            ));

            Database.WriteToJsonFile(memberJsonObj);
        }

        public bool DeleteMember(string pNum)
        {
            if (Database.MemberExist(pNum))
            {
                JObject memberJsonObj = Database.GetMemberJsonObject();
                JToken memberToBeDeleted = Database.SelectedMember(pNum, memberJsonObj);

                memberToBeDeleted.Remove();
                Database.WriteToJsonFile(memberJsonObj);
                return true;
            }
            return false;
        }

        public void ChangeMemberInfo(string pNum, string newName, string newPNum)
        {
            JObject memberJsonObj = Database.GetMemberJsonObject();
            JToken memberToBeUpdated = Database.SelectedMember(pNum, memberJsonObj);

            memberToBeUpdated["name"] = newName;
            memberToBeUpdated["pNum"] = newPNum;
            Database.WriteToJsonFile(memberJsonObj);
        }

        public bool MemberExist(string pNum)
        {
            return Database.MemberExist(pNum);
        }

        public JArray GetMember(string pNum)
        {
            return Database.GetMember(pNum);
        }

        public JArray GetAllMembers()
        {
            return Database.GetAllMembers();
        }        
    }
}