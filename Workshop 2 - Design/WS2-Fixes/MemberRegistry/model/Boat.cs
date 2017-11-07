using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Boat
    {
        private model.Database _database;

        public Boat()
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
            return Database.MemberExist(pNum);
        }

        public JArray GetMember(string pNum)
        {
            return Database.GetMember(pNum);
        }

        public void RegisterBoat(string pNum, string boatType, string boatLength)
        {
            JObject memberJsonObj = Database.GetMemberJsonObject();
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

            Database.WriteToJsonFile(memberJsonObj);
        }

        public void DeleteBoat(string pNum, int boatIndex)
        {
            JObject memberJsonObj = Database.GetMemberJsonObject();
            JToken memberToDeleteBoatFrom = Database.SelectedMember(pNum, memberJsonObj);
 
            memberToDeleteBoatFrom["boats"][boatIndex - 1].Remove();
            Database.WriteToJsonFile(memberJsonObj);
        }

        public void ChangeBoatInfo(string memberPNum, int boatIndex, string newBoatType, string newBoatLength)
        {
            JObject memberJsonObj = Database.GetMemberJsonObject();
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
            Database.WriteToJsonFile(memberJsonObj);
        }
    }
}