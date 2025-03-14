﻿using System;
using System.IO;

namespace ConsoleApp
{
    //? ООП
    public class FileManager
    {
        private readonly DirectoryInfo _directoryInfo;

        //? Конструктор принимающий путь к папке
        public FileManager(string folderPath)
        {
            _directoryInfo = new DirectoryInfo(folderPath);

            if (!_directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException("Такой папки у меня нету");
            }
        }

        //? Вывод всех файлов и папок
        public void ShowFilesInFolder(string way)
        {
            //? Принятие пути до директории от пользователя
            var ways = Directory.GetFiles(way);
            var directorys = Directory.GetDirectories(way);
            Console.WriteLine(string.Join ("\n", directorys));
            Console.WriteLine(string.Join ("\n", ways));

            Console.WriteLine("Внизу основная Директория"); // Обозначение для пользователя

            //? Размер каталога
            long folderSize = CalculateFolderSize(_directoryInfo);
            Console.WriteLine($"Размер каталога: {folderSize} байт");
            
            //? Список файлов в каталоге и их размер
            FileInfo[] files = _directoryInfo.GetFiles();
            Console.WriteLine($"Файлы в каталоге: {files.Length}");
            foreach (var file in files)
            {
                Console.WriteLine($"\tФайл: {file.Name}, Размер: {file.Length} байт");
            }

            //? Список папок в каталоге
            DirectoryInfo[] folders = _directoryInfo.GetDirectories();
            Console.WriteLine($"Папки в каталоге: {folders.Length}");
            foreach (var dir in folders)
            {
                Console.WriteLine($"\tПапка: {dir.Name}");
            }
        }

        //? Копирование файлов
        //! Требует исправлений
        public void CopyFile(string fileName, string destinationPath)
        {
            FileInfo fileToCopy = new FileInfo(fileName);
            if (!fileToCopy.Exists)
            {
                Console.WriteLine("Такого файла у меня нету");
                return;
            }

            try
            {
                fileToCopy.CopyTo(destinationPath, true);
                Console.WriteLine("Ты скопировал файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //? Удаление файлов
        public void DeleteFile(string fileName)
        {
            FileInfo fileToDelete = new FileInfo(Path.Combine(_directoryInfo.FullName, fileName));
            if (!fileToDelete.Exists)
            {
                Console.WriteLine("Такого файла у меня нету");
                return;
            }

            try
            {
                fileToDelete.Delete();
                Console.WriteLine("Ты удалил файл");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //? Размер каталога
        private static long CalculateFolderSize(DirectoryInfo directory)
        {
            long size = 0;

            //? Суммирование всех файлов
            foreach (FileInfo file in directory.GetFiles())
            {
                size += file.Length;
            }

            //? Суммирование всех подкаталогов
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                size += CalculateFolderSize(dir);
            }

            return size;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            string folderPath = @"C:\Users\rilUnreal\Documents\file is project";

            try
            {
                FileManager fileManager = new FileManager(folderPath);

                while (true)
                {
                    Console.WriteLine("____________-_-____________________");
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Показать файлы в папке");
                    Console.WriteLine("2. Скопировать файл");
                    Console.WriteLine("3. Удалить файл");
                    Console.WriteLine("4. Выйти");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Неверно! Выбери от 1 до 4");
                        continue;
                    }

                    if (choice == 1)
                    {
                        Console.WriteLine("Введите путь к папке:");
                        string way = Console.ReadLine();
                        fileManager.ShowFilesInFolder(way);
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("Что скопировать:");
                        string fileNameToCopy = Console.ReadLine();
                        Console.WriteLine("Куда скопировать:");
                        string destinationPath = Console.ReadLine();
                        fileManager.CopyFile(fileNameToCopy, destinationPath);
                    }
                    else if (choice == 3)
                    {
                        Console.WriteLine("Что удалить:");
                        string fileNameToDelete = Console.ReadLine();
                        fileManager.DeleteFile(fileNameToDelete);
                    }
                    else if (choice == 4)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Неверно! Выбери от 1 до 4");
                    }
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}


//TODO Version 1.5