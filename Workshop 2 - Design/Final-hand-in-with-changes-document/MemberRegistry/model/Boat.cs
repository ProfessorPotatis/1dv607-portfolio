using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MemberRegistry.model
{
    class Boat : Member
    {
        public void RegisterBoat(string pNum, string boatType, string boatLength)
        {
            JObject memberJsonObj = GetMemberJsonObject();
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
            JObject memberJsonObj = GetMemberJsonObject();
            JToken memberToDeleteBoatFrom = SelectedMember(pNum, memberJsonObj);
 
            memberToDeleteBoatFrom["boats"][boatIndex - 1].Remove();
            WriteToJsonFile(memberJsonObj);
        }

        public void ChangeBoatInfo(string memberPNum, int boatIndex, string newBoatType, string newBoatLength)
        {
            JObject memberJsonObj = GetMemberJsonObject();
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