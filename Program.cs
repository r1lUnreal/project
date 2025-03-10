﻿using System;

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

                //? Размер каталога
                long folderSize = CalculateFolderSize(directoryInfo);
                Console.WriteLine($"Размер каталога: {folderSize} байт");

                //? Список файлов в каталоге
                FileInfo[] files = directoryInfo.GetFiles();
                Console.WriteLine($"Файлы в каталоге: {files.Length}");
                foreach (var file in files)
                {
                    Console.WriteLine($"\tФайл: {file.Name}, Размер: {file.Length} байт");
                }

                //? Список папок в каталоге
                DirectoryInfo[] folders = directoryInfo.GetDirectories();
                Console.WriteLine($"Папки в каталоге: {folders.Length}");
                foreach (var dir in folders)
                {
                    Console.WriteLine($"\tПапка: {dir.Name}");
                }

                //? Получение спика каталогов
                DirectoryInfo[] subDirectories = directoryInfo.GetDirectories();
                Console.WriteLine($"Подкаталоги: {directoryInfo.Name}");

                //? Удаление файлов
                //*Directory.Delete(@"C:\Users\rilUnreal\Documents\file is project" + @"\test folder 1", true);
            }
            else
            {
                Console.WriteLine("Такого файла не существует");
            }
        }

        //? Функция для расчёта размера каталога
        public static long CalculateFolderSize(DirectoryInfo directory)
        {
            long size = 0;

            //? Суммирование всех файлов в каталоге
            foreach (FileInfo file in directory.GetFiles())
            {
                size += file.Length;
            }

            //? Рекурсивное суммирование всех подкаталогов
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                size += CalculateFolderSize(dir);
            }

            return size;
        }
    }
}