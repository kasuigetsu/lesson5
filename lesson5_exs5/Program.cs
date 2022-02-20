using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace lesson5_exs5
{
    class Program
    {
        /*Попробовала сделать 5ю задачу, не совсем получилось, так так коряво сделан вывод выполненных задач из списка, 
         * думаю там нужно было как то использовать поле isdone из класса toDo, но я пока не разобралась как именно, 
         * хотела попросить у Вас небольшую подсказочку))
        */
        static void Main(string[] args)
        {
            //Создаем список задач
            var myList = ToCreateList();

            //Вывод списка задач на консоль через бинарный файл
            var new_list = listfromFile(myList);
            Console.WriteLine("Ваш список задач: ");
            for (int i = 0; i < new_list.Count; i++)
            {
                new_list[i] = $"{i+1}) " + new_list[i];
                Console.WriteLine(new_list[i]);
            }

            //Вызов метода, позволяющего отметить выполненные задачи
            var isDone_list = isDone(new_list);
            listfromFile(isDone_list);
            
            //Вывод измененного списка задач
            Console.WriteLine("Обновленный список: ");
            foreach(string task in isDone_list)
            {
                Console.WriteLine(task);
            }
            Console.ReadLine();
        }
        static List<string> isDone(List<string>mylist)
        {
            bool flag = true;
            while(flag)
            {
                Console.WriteLine("Введите порядковый номер выполненной задачи: ");
                int is_done = Convert.ToInt32(Console.ReadLine());
                if (is_done > mylist.Count || is_done < 1)
                {
                    Console.WriteLine("Такой задачи не существует, попробуйте еще раз.");
                }
                else
                {
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        if (is_done == i + 1)
                        {
                            mylist[i] = "[x]" + mylist[i];
                        } 
                        
                    }
                    
                    Console.WriteLine("Хотите дополнить список выполненных задач?(+/-)");
                    string answer = Convert.ToString(Console.ReadLine());
                    if(answer!="+"&&answer!="-")
                    {
                        Console.WriteLine("Вы ввели неверное значение!");
                    }
                    else
                    {
                        if (answer == "-")
                        {
                            flag = false;
                        }
                    }
                }
            }
            return mylist;
        }
        static List<string> ToCreateList()
        {
            bool flag = true;
            var mylist = new List<string>(){ };
            while (flag)
            {
                ToDo list = new ToDo();
                Console.WriteLine("Введите задачу: ");
                list.Title = Console.ReadLine();
                mylist.Add(list.Title);
                Console.WriteLine("Хотите дополнить список задач?(+/-)");
                string answer = Convert.ToString(Console.ReadLine());
                if (answer != "+" && answer != "-")
                {
                    Console.WriteLine("Вы ввели неверное значение!");
                }
                else
                {
                    if (answer == "-")
                    {
                        flag = false;
                    }
                }
            }
            return mylist;
        }
        static List<string> listfromFile(List<string> mylist)
        {
            string filename = "mylist.bin";
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream fs = new FileStream(filename,FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, mylist);
            }
            List<string> new_list = new List<string>() { };
            using(FileStream fs =File.Open(filename,FileMode.Open))
            {
                return (List<string>)formatter.Deserialize(fs);
            }
        }
        
        class ToDo
        {
            private string task = string.Empty;
           
            public string Title
            {
                get
                {
                    return task;
                }
                set
                {
                    task = value;
                }
            }
            public string IsDone
            {
                get;set;
                
            }
        } 
    }
}
