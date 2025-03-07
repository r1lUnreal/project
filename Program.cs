namespace project;

class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\XboxGames";
        
        //? Вывод всех файлов в каталоге
        var files = Directory.GetFiles(path);
        foreach (string file in files) Console.WriteLine(file);

        //? Вывод всех папок в каталоге 
        var dirs = Directory.GetDirectories(path);
        foreach (string dir in dirs) Console.WriteLine(dir);

        //? Копирование файла
        //* File.Copy(path_from, path_to);

        //? Создание пустой папки 
        //*Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"/new_folder" + @"/test_folder");

        //? Удаление файла
        //*Directory.Delete(Directory.GetCurrentDirectory() + @"/new_folder", true);

        //? Возвращает текущие местоположение
        //*Console.WriteLine(Directory.GetCurrentDirectory());




    }
}


//! До 14.03.2025