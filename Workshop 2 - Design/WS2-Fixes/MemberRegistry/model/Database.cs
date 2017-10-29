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
    }
}