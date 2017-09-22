using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Database
    {
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
            string json = ReadJsonFile();
            JArray members = ConvertJsonToJArray(json);

            return members;
        }

        private string ReadJsonFile()
        {
            string json;

            using (FileStream file = File.OpenRead("model/MemberRegistry.json"))
            using (StreamReader reader = new StreamReader(file))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        private JArray ConvertJsonToJArray(string json)
        {
            JObject o = JObject.Parse(json);
            JArray membersArray = (JArray)o["members"];
            
            return membersArray; 
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
            string json = ReadJsonFile();
            string newMember = "{\"name\": \"" + name + "\", \"pNum\": \"" + pNum + "\", \"uMemberId\": \"" + uMemberId + "\"}";
            JObject member = JObject.Parse(newMember);
            JObject memberJsonObj = JObject.Parse(json);
            
            memberJsonObj["members"][0].AddAfterSelf(
                new JObject(
                new JProperty("name", member["name"]),
                new JProperty("pNum", member["pNum"]),
                new JProperty("uMemberId", member["uMemberId"]),
                new JProperty("boats", member["boats"])
            ));

            WriteToJsonFile(memberJsonObj);
        }

        private void WriteToJsonFile(JObject memberJsonObj)
        {
            using (FileStream file = File.Create("model/MemberRegistry.json"))
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(memberJsonObj);
            }
        }

        public void DeleteMember(string pNum)
        {
            if (MemberExist(pNum))
            {
                string json = ReadJsonFile();
                JObject memberJsonObj = JObject.Parse(json);
                JToken memberToBeDeleted = null;

                for (int i = 0; i < memberJsonObj["members"].Count(); i++)
                {
                    if ((string)memberJsonObj["members"][i]["pNum"] == pNum)
                    {
                        memberToBeDeleted = memberJsonObj["members"][i];
                    }
                }
                memberToBeDeleted.Remove();
                WriteToJsonFile(memberJsonObj);
            }
        }
    }
}