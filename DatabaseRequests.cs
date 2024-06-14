using Npgsql;
using System;
using System.Data;

namespace datebook
{
    public class DatabaseRequests
    {

        public static void AddUser(User user)
        {
            using var cmd =
                new NpgsqlCommand("INSERT INTO Users (Username, Password) VALUES (@Username, @Password)", DatabaseServer.GetSqlConnection());
            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.Parameters.AddWithValue("password", user.Password);

            cmd.ExecuteNonQuery();
            bool isAccess = AccessUser(user);
        }

        public static bool AccessUser(User user)
        {
            using var cmd =
                new NpgsqlCommand( "SELECT user_id FROM Users WHERE username = @username AND password = @password", DatabaseServer.GetSqlConnection());
            cmd.Parameters.AddWithValue("username", user.Username);
            cmd.Parameters.AddWithValue("password", user.Password); 
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                user.UserID = (int)reader[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddTask(Tasks task)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd =
                new NpgsqlCommand("INSERT INTO Tasks (TaskName, Description, DueDate, UserID) " +
                                  "VALUES (@TaskName, @Description, @DueDate, @UserID)", connection);
            cmd.Parameters.AddWithValue("Task", task.TaskName);
            cmd.Parameters.AddWithValue("Description", task.Description);
            cmd.Parameters.AddWithValue("DueDate", task.DueData);
            cmd.Parameters.AddWithValue("UserId", task.user.UserID);
            cmd.ExecuteNonQuery();
        }

        public static void DeleteTask(Tasks task)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd = new NpgsqlCommand("DELETE FROM Tasks WHERE TaskId = @TaskID", connection);
            cmd.Parameters.AddWithValue("TaskId", task.TaskID);
            cmd.ExecuteNonQuery();
        }
        

        public static void EditTask(Tasks task)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd =
                new NpgsqlCommand("UPDATE Tasks SET TaskName = @TaskName, Description = @Description, DueDate = @DueDate " +
                                  "WHERE TaskId = @TaskID", connection);
            cmd.Parameters.AddWithValue("TaskId", task.TaskID);
            cmd.Parameters.AddWithValue("Task", task.TaskName);
            cmd.Parameters.AddWithValue("Description", task.Description);
            cmd.Parameters.AddWithValue("DueDate", task.DueData);
            cmd.ExecuteNonQuery();
        }

        public static void GetTasks(Tasks task, User u)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd = new NpgsqlCommand("SELECT * FROM Tasks WHERE UserID = @UserID", connection);
            cmd.Parameters.AddWithValue("UserId", task.user.UserID);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["TaskId"]}, Название: {reader["Task"]}, Описание: {reader["Description"]}, " +
                    $"Дата: {reader["DueDate"]}");
            }
        }

        public static void GetTasksForToday(Tasks task, User user)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd =
                new NpgsqlCommand("SELECT * FROM Tasks WHERE UserId = @UserID AND DueDate = CURRENT_DATE", connection);
            cmd.Parameters.AddWithValue("UserId", task.user.UserID);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["TaskId"]}, Название: {reader["Task"]}, Описание: {reader["Description"]}, " +
                    $"Дата: {reader["DueDate"]}");
            }
        }

        public static void GetTasksForTomorrow(Tasks task, User user)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd =
                new NpgsqlCommand(
                    "SELECT * FROM Tasks WHERE UserId = @UserID AND DueDate = CURRENT_DATE + INTERVAL '1 day'",
                    connection);
            cmd.Parameters.AddWithValue("UserId", task.user.UserID);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["TaskId"]}, Название: {reader["Task"]}, Описание: {reader["Description"]}, " +
                    $"Дата: {reader["DueDate"]}");
            }
        }

        public static void GetTasksForThisWeek(Tasks task, User user)
        {
            using var connection = DatabaseServer.GetSqlConnection();
            using var cmd =
                new NpgsqlCommand("SELECT * FROM Tasks WHERE UserId = @UserID AND DueDate >= CURRENT_DATE " +
                                  "AND DueDate <= CURRENT_DATE + INTERVAL '7 days'", connection);
            cmd.Parameters.AddWithValue("UserId", task.user.UserID);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["TaskId"]}, Название: {reader["Task"]}, Описание: {reader["Description"]}, " +
                    $"Дата: {reader["DueDate"]}");
            }
        }
    }

}
