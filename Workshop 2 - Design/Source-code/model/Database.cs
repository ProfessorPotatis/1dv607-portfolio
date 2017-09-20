using System;

namespace MemberRegistry.model
{
    class Database
    {
        public bool MemberExist(string name, string pNum)
        {
            System.Console.WriteLine("Name: " + name + " Personal number: " + pNum);
            return false;
        }
    }
}