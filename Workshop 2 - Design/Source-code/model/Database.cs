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

            member["boats"] = new JArray();
            
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

        public bool DeleteMember(string pNum)
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
                return true;
            }
            return false;
        }

        public void ChangeMemberInfo(string memberPNum, string newName, string newPNum)
        {
            string json = ReadJsonFile();
            JObject memberJsonObj = JObject.Parse(json);
            JToken memberToBeUpdated = null;

            for (int i = 0; i < memberJsonObj["members"].Count(); i++)
            {
                if ((string)memberJsonObj["members"][i]["pNum"] == memberPNum)
                {
                    memberToBeUpdated = memberJsonObj["members"][i];
                }
            }

            memberToBeUpdated["name"] = newName;
            memberToBeUpdated["pNum"] = newPNum;
            WriteToJsonFile(memberJsonObj);
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

        public void RegisterBoat(string pNum, string boatType, string boatLength)
        {
            string json = ReadJsonFile();
            JObject memberJsonObj = JObject.Parse(json);
            JArray memberToAddBoatTo = new JArray();

            for (int i = 0; i < memberJsonObj["members"].Count(); i++)
            {
                if ((string)memberJsonObj["members"][i]["pNum"] == pNum)
                {
                    memberToAddBoatTo = memberJsonObj["members"][i]["boats"] as JArray;
                }
            }

            memberToAddBoatTo.Add(
                new JObject(
                new JProperty("type", boatType),
                new JProperty("length", boatLength + " meters")
            ));

            WriteToJsonFile(memberJsonObj);
        }

        public void DeleteBoat(string pNum, int boatIndex)
        {
            string json = ReadJsonFile();
            JObject memberJsonObj = JObject.Parse(json);
            JToken memberToDeleteBoatFrom = null;

            for (int i = 0; i < memberJsonObj["members"].Count(); i++)
            {
                if ((string)memberJsonObj["members"][i]["pNum"] == pNum)
                {
                    memberToDeleteBoatFrom = memberJsonObj["members"][i];
                }
            }
            memberToDeleteBoatFrom["boats"][boatIndex - 1].Remove();
            WriteToJsonFile(memberJsonObj);
        }

        public void ChangeBoatInfo(string memberPNum, int boatIndex, string newBoatType, string newBoatLength)
        {
            string json = ReadJsonFile();
            JObject memberJsonObj = JObject.Parse(json);
            JToken boatToBeUpdated = null;

            for (int i = 0; i < memberJsonObj["members"].Count(); i++)
            {
                if ((string)memberJsonObj["members"][i]["pNum"] == memberPNum)
                {
                    boatToBeUpdated = memberJsonObj["members"][i]["boats"][boatIndex - 1];
                }
            }

            boatToBeUpdated["type"] = newBoatType;
            boatToBeUpdated["length"] = newBoatLength + " meters";
            WriteToJsonFile(memberJsonObj);
        }
    }
}