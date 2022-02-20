using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace lesson5_exs4x
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Пользователи\Core_i5\testDir";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if(!dirInfo.Exists)//если директория не существует, то создать его
            {
                dirInfo.Create();
            }
            dirInfo.CreateSubdirectory(@"dir2\dir2");
            dirInfo.CreateSubdirectory(@"dir1\dir1.1");

            string tree = @"C:\Пользователи\Core_i5\testDir\dir1\dir1.1\test.txt";
            Console.WriteLine($"Путь к txt файлу: {tree} ");
            //запись в файл
            using (FileStream fstream = new FileStream(tree,FileMode.OpenOrCreate))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(tree);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Путь записан в файл.");
            }
            //чтение из файла
            using (FileStream fstream = File.OpenRead(tree))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                string fromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine($"Содержимое файла test.txt: {fromFile}");
            }

            Console.ReadKey();
        }
    }
}
