using System;
using System.Data;
using System.Collections.Generic;
using System.IO;


public class User
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Tasks
{
    public User user{ get; set; }
    public int TaskID { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public DateTime DueData { get; set; }
}

namespace datebook
{
    class Program
    {
        
        static DateTime AddUserTime(string input)
        {
            DateTime DueData;
            while (!DateTime.TryParse(input, out DueData))
            {
                Console.WriteLine();
                input = Console.ReadLine();
            }

            return DueData;
        }
        public static void Main(string[] args)
        {
            
            User user = new User();
            Tasks task = new Tasks();
            bool exit = false;
            
            while (exit == false)
            {
                Console.WriteLine("..........................................");
                Console.WriteLine("Выберете действие:");
                Console.WriteLine("1. Регистрация.");
                Console.WriteLine("2. Вход в акаунт.");
                Console.WriteLine("..........................................");
                int num = Convert.ToInt32(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        Console.WriteLine("Введите логин: ");
                        user.Username = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        user.Password = Console.ReadLine();
                        DatabaseRequests.AddUser(user);
                        break;
                    case 2:
                        Console.WriteLine("Введите логин: ");
                        user.Username = Console.ReadLine();
                        Console.WriteLine("Введите пароль: ");
                        user.Password = Console.ReadLine();
                       
                        exit = DatabaseRequests.AccessUser(user);
                        
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор действия...");
                        break;
                    
                }
                if (exit)
                {
                    Console.WriteLine("Вы вошли...");
                }
                else
                {
                    Console.WriteLine("Не удалось войти...");
                }

                
            }
            while (true)
            {
                    Console.WriteLine("..........................................");
                    Console.WriteLine("Выбирите действие:");
                    Console.WriteLine("1. Создать задачу.");
                    Console.WriteLine("2. Удалить задачу.");
                    Console.WriteLine("3. Редактировать задачу.");
                    Console.WriteLine("4. Просмотреть все задачи задачи.");
                    Console.WriteLine("5. Просмотреть задачи на сегодня.");
                    Console.WriteLine("6. Просмотреть задачи на завтра.");
                    Console.WriteLine("7. Просмотреть задачи на неделю.");
                    Console.WriteLine("7. Просмотр предстоящих задач. ");
                    Console.WriteLine("7. Выйти из ежедневника. ");
                    Console.WriteLine("7. Выйти из ежедневника. ");
                    Console.WriteLine("..........................................");
                    int num = Convert.ToInt32(Console.ReadLine());
                    
                    switch (num)
                    {
                        case 1 :
                            Console.WriteLine("Название задачи: ");
                            task.TaskName = Console.ReadLine();
                            Console.WriteLine("Содержание задачи: ");
                            task.Description = Console.ReadLine();
                            Console.WriteLine("Дата задачи: ");
                            task.DueData = AddUserTime(Console.ReadLine());
                            DatabaseRequests.AddTask(task);
                            break;
                        case 2 :
                            Console.WriteLine("Номер задачи, которую будете удалять: ");
                            task.TaskID = Convert.ToInt32(Console.ReadLine());
                            DatabaseRequests.DeleteTask(task);
                            break;
                        case 3:
                            Console.WriteLine("Введите номер задачи, которую будете исправлять: ");
                            task.TaskID = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введите название задачи, которую будете исправлять: ");
                            task.TaskName = Console.ReadLine();
                            Console.WriteLine("Содержание задачи: ");
                            task.Description = Console.ReadLine();
                            Console.WriteLine("Дата задачи: ");
                            task.DueData = AddUserTime(Console.ReadLine());;
                            DatabaseRequests.EditTask(task);
                            break;
                        case 4:
                            Console.WriteLine("Все задачи:");
                            DatabaseRequests.GetTasks(task, user);
                            break;
                        case 5:
                            Console.WriteLine("Задачи на сегодня:");
                            DatabaseRequests.GetTasksForToday(task, user);
                            break;
                        case 6:
                            Console.WriteLine("Задачи на завтра: ");
                            DatabaseRequests.GetTasksForTomorrow(task, user);
                            break;
                        case 7:
                            Console.WriteLine("Задачи на неделю:");
                            DatabaseRequests.GetTasksForThisWeek(task, user);
                            break;
                        case 0: 
                            Console.WriteLine("Вы вышли...");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Некорректный выбор действия...");
                            break;
                        
                    }

                }

        }
    }
}
