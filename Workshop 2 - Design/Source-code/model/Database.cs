using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Database
    {
        public bool MemberExist(string name, string pNum)
        {
            string json = ReadJsonFile();
            JArray members = ConvertJsonToJArray(json);

            for (int i = 0; i < members.Count; i++)
            {
                if ((string)members[i]["pNum"] == pNum)
                {
                    return true;
                }
            }

            string uMemberId = GenerateUniqueMemberId(members.Count);

            for (int i = 0; i < members.Count; i++)
            {
                if ((string)members[i]["uMemberId"] == uMemberId)
                {
                    uMemberId = GenerateUniqueMemberId(members.Count);
                }
            }

            CreateNewMember(name, pNum, uMemberId, json);
            return false;
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

        private string GenerateUniqueMemberId(int memberCount)
        {
            Random rnd = new Random();
            int num1 = rnd.Next(1000);
            int num2 = rnd.Next(100);

            string memberId = "Member" + num1 + num2;

            return memberId;
        }

        private void CreateNewMember(string name, string pNum, string uMemberId, string json)
        {
            string newMember = "{\"name\": \"" + name + "\", \"pNum\": \"" + pNum + "\", \"uMemberId\": \"" + uMemberId + "\"}";
            JObject member = JObject.Parse(newMember);
            JObject memberJsonObj = JObject.Parse(json);
            
            memberJsonObj["members"][0].AddAfterSelf(
                new JObject(
                new JProperty("name", member["name"]),
                new JProperty("pNum", member["pNum"]),
                new JProperty("uMemberId", member["uMemberId"])
            ));

            WriteToJsonFile(memberJsonObj);
        }

        private void WriteToJsonFile(JObject memberJsonObj)
        {
            using (FileStream file = File.OpenWrite("model/MemberRegistry.json"))
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(memberJsonObj);
            }
        }
    }
}