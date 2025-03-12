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
                Console.WriteLine("__________________________________");
                Console.WriteLine("Хотите удалить папку? ");
                Console.WriteLine("1 = yes | 2 = no");
                int delFolder = int.Parse(Console.ReadLine());

                if (delFolder == 1)
                {   
                    Console.WriteLine("Какую?");
                    Console.WriteLine("1. test folder 1");
                    Console.WriteLine("2. test folder 2");

                    int question = int.Parse(Console.ReadLine());

                    if (question == 1)
                    {
                        Directory.Delete(@"C:\Users\rilUnreal\Documents\file is project\test folder 1", true);
                        Console.WriteLine("Вы изничтожили папку: test folder 1");
                    }
                    else if (question == 2)
                    {
                        Directory.Delete(@"C:\Users\rilUnreal\Documents\file is project\test folder 2");
                        Console.WriteLine("Вы изничтожили папку: test folder 2");
                    }
                    else
                    {
                        Console.WriteLine("Такой папки нема :(");
                    }
                }
                else
                {
                    Console.WriteLine("Ваша папка жива и здорова");
                }
                
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

// У тоби е эбупрофен? Голова болить