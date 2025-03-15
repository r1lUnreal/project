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
            var directories = Directory.GetDirectories(way);
            Console.WriteLine(string.Join("\n", directories));
            Console.WriteLine(string.Join("\n", ways));

            Console.WriteLine("Внизу основная Директория"); // Обозначение для пользователя

            //? Размер каталога
            long folderSize = CalculateFolderSize(_directoryInfo);
            Console.WriteLine($"Размер каталога: {folderSize / 1024.0:F2} кб"); //? Преобразование из Байт в КилоБайт

            //? Список файлов в каталоге и их размер
            FileInfo[] files = _directoryInfo.GetFiles();
            Console.WriteLine($"Файлы в каталоге: {files.Length}");
            foreach (var file in files)
            {
                Console.WriteLine($"\tФайл: {file.Name}, Размер: {file.Length / 1024.0:F2} кб"); //? Преобразование из Байт в КилоБайт
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
        public void CopyFile(string sourceFileName, string destinationPath)
        {
            //? Полный путь к исходному файлу
            string sourceFilePath = Path.Combine(_directoryInfo.FullName, sourceFileName);

            //? Проверка существования исходного файла
            if (!File.Exists(sourceFilePath))
            {
                Console.WriteLine("Файл не найден в текущей директории.");
                return;
            }

            //? Проверка, является ли destinationPath директорией или полным путем к файлу
            string destinationFilePath;
            if (Directory.Exists(destinationPath))
            {
                //? Если destinationPath — это директория, создаем полный путь к файлу
                destinationFilePath = Path.Combine(destinationPath, sourceFileName);
            }
            else
            {
                //? Если destinationPath — это полный путь к файлу, используем его
                destinationFilePath = destinationPath;
            }

            try
            {
                //? Копирование файла
                File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                Console.WriteLine($"Файл успешно скопирован в: {destinationFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при копировании файла: {ex.Message}");
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
            string folderPath = @"C:\Users\rilUnreal\Documents\file is project"; //? Дефолтная папка

            try
            {
                FileManager fileManager = new FileManager(folderPath);

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("========================================");
                    Console.WriteLine("          Менеджер файлов");
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("1. Показать файлы в папке");
                    Console.WriteLine("2. Скопировать файл");
                    Console.WriteLine("3. Удалить файл");
                    Console.WriteLine("4. Выйти");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("========================================");
                    Console.ResetColor();

                    Console.Write("Выберите действие: ");
                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверно! Выбери от 1 до 4");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }

                    if (choice == 1)
                    {
                        Console.Write("Введите путь к папке: ");
                        string way = Console.ReadLine()!;
                        fileManager.ShowFilesInFolder(way);
                    }
                    else if (choice == 2)
                    {
                        Console.Write("Что скопировать: ");
                        string fileNameToCopy = Console.ReadLine()!;
                        Console.Write("Куда скопировать: ");
                        string destinationPath = Console.ReadLine()!;
                        fileManager.CopyFile(fileNameToCopy, destinationPath);
                    }
                    else if (choice == 3)
                    {
                        Console.Write("Что удалить: ");
                        string fileNameToDelete = Console.ReadLine()!;
                        fileManager.DeleteFile(fileNameToDelete);
                    }
                    else if (choice == 4)
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Неверно! Выбери от 1 до 4");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}