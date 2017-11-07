using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Database
    {
        public string ReadJsonFile()
        {
            string json;
            string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "/model/MemberRegistry.json";

            using (FileStream file = File.OpenRead(fileName))
            using (StreamReader reader = new StreamReader(file))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        public JArray ConvertJsonToJArray(string json)
        {
            JObject o = JObject.Parse(json);
            JArray membersArray = (JArray)o["members"];
            
            return membersArray; 
        }

        public void WriteToJsonFile(JObject memberJsonObj)
        {
            string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "/model/MemberRegistry.json";

            using (FileStream file = File.Create(fileName))
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(memberJsonObj);
            }
        }

        public JObject GetMemberJsonObject()
        {
            string json = ReadJsonFile();
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