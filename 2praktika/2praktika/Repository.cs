using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;

namespace _2praktika
{
    public class Repository
    {
        private SqlConnection conn;

        public Repository()
        {
            conn = new SqlConnection(@"Server=.;Database=2praktika;Trusted_Connection=true;");
        }
        public User GetUser(string username, string password)
        {
            try
            {
                conn.Close();
                string sql = "select groupid, name, surname, username from [user] " +
                    "where username=@username and password=@password";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int groupId = Convert.ToInt32(reader["groupid"].ToString());
                        string name = reader["name"].ToString();
                        string surname = reader["surname"].ToString();
                        string usrname = reader["username"].ToString();
                        conn.Close();
                        return new User(name, surname , usrname, password, groupId);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("Neteisingi vartotojo duomenys");
        }
        public User GetUser(int userId)
        {
            try
            {
                conn.Close();
                string sql = "select groupid, name, surname, username from [user] " +
                    "where id=@userId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int groupId = Convert.ToInt32(reader["groupid"].ToString());
                        string name = reader["name"].ToString();
                        string surname = reader["surname"].ToString();
                        string usrname = reader["username"].ToString();
                        conn.Close();
                        return new User(name, surname, usrname, "", groupId);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get user with id error");
        }

        public List<string> GetGroupsList()
        {
            try
            {
                conn.Close();
                List<string> groupsList = new List<string>();
                string sql = "select groupName from [group] where id <> 1 and id <> 2";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string groupName = reader["groupName"].ToString();
                        groupsList.Add(groupName);
                    }
                }
                conn.Close();
                groupsList = groupsList.OrderBy(q => q).ToList();
                return groupsList;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get groups list error");
        }

        public int GetGroupId(string groupName)
        {
            try
            {
                conn.Close();
                string sql = "select id from [group] where groupName=@groupName";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupName", groupName);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int groupId = Convert.ToInt32(reader["id"]);
                        return groupId;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return 0;
        }

        public string GetGroupName(int groupId)
        {
            try
            {
                conn.Close();
                string sql = "select groupName from [group] where id=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string groupName = reader["groupName"].ToString();
                        return groupName;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get group name error");
        }

        public void RemoveGroup(int groupId)
        {
            try
            {
                conn.Close();
                string sql = "delete from [user] where groupid=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                sql = "delete from [subjectgroup] where groupid=@groupId";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                sql = "delete from [group] where id=@groupId";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }


        public void AddGroup(string groupName)
        {
            try
            {
                conn.Close();
                string sql = "insert into [group](groupName) " +
                    "values(@groupName)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupName", groupName);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public List<User> GetUserList(int groupId)
        {
            try
            {
                conn.Close();
                List<User> usersList = new List<User>();
                string sql = "select name, surname, username from [user] "+
                    "where groupid=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["name"].ToString();
                        string surname = reader["surname"].ToString();
                        string usrname = reader["username"].ToString();
                        usersList.Add(new User(name, surname, usrname, "", groupId));
                    }
                }
                conn.Close();
                return usersList;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get users list error");
        }
        
        public int GetUserId(string username)
        {
            try
            {
                conn.Close();
                string sql = "select id from [user] where username=@username";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["id"]);
                        return userId;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return 0;
        }

        public int GetUserId(string subject, int temp)
        {
            try
            {
                conn.Close();
                string sql = "select userid from [subject] where subject=@subject";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subject", subject);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["userid"]!=DBNull.Value)
                        {
                            int userId = Convert.ToInt32(reader["userid"]);
                            return userId;
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return 0;
        }

        public void RemoveUser(int userId)
        {
            try
            {
                conn.Close();
                string sql = "delete from [grade] where userid=@userId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                sql = "delete from [user] where id=@userId";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void AddUser(User user)
        {
            try
            {
                conn.Close();
                string sql = "insert into [user](name, surname, username, password, groupid) " +
                    "values(@name, @surname, @username, @password, @groupId)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", user.GetName());
                cmd.Parameters.AddWithValue("@surname", user.GetSurname());
                cmd.Parameters.AddWithValue("@username", user.GetUsername());
                cmd.Parameters.AddWithValue("@password", user.GetPassword());
                cmd.Parameters.AddWithValue("@groupId", user.GetGroupId());
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public List<string> GetSubjectsList(int userId)
        {
            try
            {
                conn.Close();
                List<string> subjectsList = new List<string>();
                string sql;
                if (userId == 0)//id 0 = all subjects without teacher
                {
                    sql = "select subject from [subject] " +
                        "where userid is null";
                }
                else if(userId == -1)//id -1 = all subjects
                {
                    sql = "select subject from [subject]";
                }
                else
                {
                    sql = "select subject from [subject] " +
                         "where userid=@userId";
                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (userId != 0 && userId != -1)
                    cmd.Parameters.AddWithValue("@userId", userId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string subjectName = reader["subject"].ToString();
                        subjectsList.Add(subjectName);
                    }
                }
                conn.Close();
                subjectsList = subjectsList.OrderBy(q => q).ToList();
                return subjectsList;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get subjects list by userId error");
        }

        public List<string> GetSubjectsList(int groupId, int temp)
        {
            try
            {
                conn.Close();
                List<int> subjectsIdList = new List<int>();
                string sql = "select subjectid from [subjectgroup] " +
                        "where groupid=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int subjectId = Convert.ToInt32(reader["subjectid"]);
                        subjectsIdList.Add(subjectId);
                    }
                }
                conn.Close();
                List<string> subjectsList = new List<string>();
                foreach (int id in subjectsIdList)
                {
                    subjectsList.Add(GetSubjectName(id));
                }
                subjectsList = subjectsList.OrderBy(q => q).ToList();
                return subjectsList;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get subjects list by groupId error");
        }

        public string GetSubjectName(int subjectId)
        {
            try
            {
                conn.Close();
                string sql = "select subject from [subject] where id=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string subjectName = reader["subject"].ToString();
                        return subjectName;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get subject name error");
        }

        public void UpdateSubjectTeacher(int userId, string subjectName)
        {
            try
            {
                conn.Close();
                string sql;
                if (userId != 0)
                {
                    sql = "update [subject] " +
                        "set userId=@userId " +
                        "where subject=@subjectName";
                }
                else
                {
                    sql = "update [subject] " +
                        "set userId=null " +
                        "where subject=@subjectName";
                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd = new SqlCommand(sql, conn);
                if(userId!=0)
                    cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@subjectName", subjectName);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public int GetSubjectId(string subjectName)
        {
            try
            {
                conn.Close();
                string sql = "select id from [subject] where subject=@subjectName";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectName", subjectName);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int subjectId = Convert.ToInt32(reader["id"]);
                        return subjectId;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return 0;
        }

        public void AddSubject(string subjectName)
        {
            try
            {
                conn.Close();
                string sql = "insert into [subject](subject) " +
                    "values(@subjectName)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectName", subjectName);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public List<string> GetSubjectGroupsList(int subjectId)
        {
            try
            {
                conn.Close();
                List<string> groupsList = new List<string>();
                List<int> groupIdsList = new List<int>();
                string sql = "select groupid from [subjectgroup] " +
                         "where subjectid=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int groupId = Convert.ToInt32(reader["groupid"]);
                        groupIdsList.Add(groupId);
                    }
                }
                conn.Close();
                foreach(int id in groupIdsList)
                {
                    groupsList.Add(GetGroupName(id));
                }
                groupsList = groupsList.OrderBy(q => q).ToList();
                return groupsList;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            throw new Exception("get subject groups list error");
        }

        public void RemoveSubject(int subjectId)
        {
            try
            {
                conn.Close();
                string sql = "delete from [grade] where subjectid=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                sql = "delete from [subjectgroup] where subjectid=@subjectId";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                sql = "delete from [subject] where id=@subjectId";
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void AddSubjectGroup(int subjectId, int groupId)
        {
            try
            {
                conn.Close();
                string sql = "insert into [subjectgroup](subjectid, groupid) " +
                    "values(@subjectId, @groupId)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public int GetSubjectGroupId(int subjectId, int groupId)
        {
            try
            {
                conn.Close();
                string sql = "select id from [subjectgroup] where subjectid=@subjectId and groupid=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int subjectGroupId = Convert.ToInt32(reader["id"]);
                        return subjectGroupId;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return 0;
        }

        public void RemoveSubjectGroup(int subjectId, int groupId)
        {
            try
            {
                conn.Close();
                string sql = "delete from [subjectgroup] where subjectid=@subjectId and groupid=@groupId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                cmd.Parameters.AddWithValue("@groupId", groupId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public int GetGrade(int userId, int subjectId)
        {
            try
            {
                conn.Close();
                string sql = "select grade from [grade] where userid=@userId and subjectid=@subjectId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@subjectId", subjectId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int grade = Convert.ToInt32(reader["grade"]);
                        return grade;
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            return -1;//grade not found
        }

        public void AddGrade(int userId, int subjectId, int grade)
        {
            try
            {
                if (GetGrade(userId, subjectId) == -1)
                {
                    conn.Close();
                    string sql = "insert into [grade](userid, subjectid, grade) " +
                        "values(@userId, @subjectId, @grade)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@subjectId", subjectId);
                    cmd.Parameters.AddWithValue("@grade", grade);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    try
                    {
                        conn.Close();
                        string sql = "update [grade] " +
                                "set grade=@grade " +
                                "where userid=@userId and subjectid=@subjectId";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@grade", grade);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@subjectId", subjectId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
