using System;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string folder = @"C:\Users\rilUnreal\Documents\file is project";

            DirectoryInfo directoryInfo = new DirectoryInfo(folder);

            if (directoryInfo.Exists)
            {
                Console.WriteLine($"Имя директории: {directoryInfo.Name}");

                //? Список файлов в каталоге
                FileInfo[] files = directoryInfo.GetFiles();
                Console.WriteLine($"Файлы в каталоге: {files.Length}");
                foreach (var file in files)
                {
                    Console.WriteLine($"\tФайл: {file.Name}, Размер: {file.Length} байт");
                }

                //? Получение спика каталогов
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
                Console.WriteLine($"Подкаталоги: {directoryInfo.Name}");

                //? Удаление файлов
                Directory.Delete(@"C:\Users\rilUnreal\Documents\file is project" + @"\test folder 1", true);
            }
            else
            {
                Console.WriteLine("Такого файла не существует");
            }
        }
    }
}