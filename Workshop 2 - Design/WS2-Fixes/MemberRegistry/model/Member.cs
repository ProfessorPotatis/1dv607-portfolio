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

        public bool MemberExist(string pNum)
        {
            JArray members = GetAllMembers();

            for (int i = 0; i < members.Count; i++)
            {
                if ((string)members[i]["pNum"] == pNum)
                {
                    return true;
                }
            }
            return false;
        }

        public JArray GetAllMembers()
        {
            string json = Database.ReadJsonFile();
            JArray members = Database.ConvertJsonToJArray(json);

            return members;
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
            if (MemberExist(pNum))
            {
                JObject memberJsonObj = GetMemberJsonObject();
                JToken memberToBeDeleted = SelectedMember(pNum, memberJsonObj);

                memberToBeDeleted.Remove();
                Database.WriteToJsonFile(memberJsonObj);
                return true;
            }
            return false;
        }

        public JObject GetMemberJsonObject()
        {
            string json = Database.ReadJsonFile();
            JObject memberJsonObj = JObject.Parse(json);

            return memberJsonObj;
        }

        public JToken SelectedMember(string pNum, JObject memberJsonObj)
        {
            JToken member = null;

            for (int i = 0; i < memberJsonObj["members"].Count(); i++)
            {
                if ((string)memberJsonObj["members"][i]["pNum"] == pNum)
                {
                    member = memberJsonObj["members"][i];
                }
            }

            return member;
        }

        public void ChangeMemberInfo(string pNum, string newName, string newPNum)
        {
            JObject memberJsonObj = GetMemberJsonObject();
            JToken memberToBeUpdated = SelectedMember(pNum, memberJsonObj);

            memberToBeUpdated["name"] = newName;
            memberToBeUpdated["pNum"] = newPNum;
            Database.WriteToJsonFile(memberJsonObj);
        }

        public JArray GetMember(string pNum)
        {
            JArray members = GetAllMembers();
            JArray specificMember = new JArray();

            for (int i = 0; i < members.Count; i++)
            {
                if ((string)members[i]["pNum"] == pNum)
                {
                    specificMember.Add(members[i]);
                }
            }
            return specificMember;
        }
    }
}