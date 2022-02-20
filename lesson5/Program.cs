using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace lesson5
{
    [Serializable]
    class Program
    {
        static void Main(string[] args)
        {
            //Задание 1
            Console.WriteLine("Введите произвольный набор данных: ");
            string str = Console.ReadLine();
            string filename = "startup.txt";
            File.WriteAllText(filename, str);
            string check = File.ReadAllText(filename);
            Console.WriteLine($"Содержимое файла {filename}: {check}\n");
            
            //Задание 2
            DateTime data = DateTime.Now;
            File.AppendAllText(filename, $" Внимание! В файл была добавлена текущая дата: {data}\n");
            check = File.ReadAllText(filename);
            Console.WriteLine(check);

            //Задание 3
            Console.WriteLine("Введите произвольный набор чисел от 0 до 255 через пробел: ");
            var input = Console.ReadLine().Split(' ');
            var array = new byte[input.Length];
            for (int i = 0; i < array.Length; i++)
            {
                if(!byte.TryParse(input[i],out byte numb))
                {
                    Console.WriteLine("Вы ввели неверные данные!");
                    return;
                }
                array[i] = numb;
            }
            string filename2 = "exs3.bin";

            //Создаем объект BinaryWriter для записи массива в файл
            using(BinaryWriter writer=new BinaryWriter(File.Open(filename2,FileMode.Create)))
            {
                //записываем каждый элемент массива в файл exs3.bin
                for (int i = 0; i < array.Length; i++)
                {
                    writer.Write(array[i]);
                }
            }

            //Создаем объект  BinaryReader для считывания элементов массива из файла
            byte[] retArray;
            using (BinaryReader reader = new BinaryReader(File.Open(filename2, FileMode.Open)))
            {
                retArray = reader.ReadBytes(array.Length);
            }
            Console.WriteLine($"Содержимое файла {filename2}: ");
            //Вывод сдержимого файла exs3.bin на консоль
            for (int i = 0; i < retArray.Length; i++)
            {
                Console.Write(retArray[i]+" ");
            }
            Console.ReadKey();

        }
    }
}
