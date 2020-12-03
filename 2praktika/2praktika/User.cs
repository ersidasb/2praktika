using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2praktika
{
    public class User
    {
        private string name;
        private string surname;
        private string username;
        private string password;
        private int groupId;
        public User(string name, string surname, string username, string password, int groupId)
        {
            this.name = name;
            this.surname = surname;
            this.username = username;
            this.password = password;
            this.groupId = groupId;
        }

        public string GetName()
        {
            return name;
        }
        public string GetSurname()
        {
            return surname;
        }
        public string GetUsername()
        {
            return username;
        }
        public string GetPassword()
        {
            return password;
        }
        public int GetGroupId()
        {
            return groupId;
        }
    }
}
