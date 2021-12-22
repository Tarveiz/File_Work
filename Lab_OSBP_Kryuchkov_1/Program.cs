using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Linq;
using System.Xml.Serialization;
using System.IO.Compression;
using System.Text;

namespace Lab_OSBP_Kryuchkov_1
{
    class MainMenu
    {
        public void menu()
        {
            
            Console.WriteLine("Выберите нужную вам операцию: \n");
            Console.WriteLine("Введите 1, чтобы вывести системную информацию\n" +
                "Введите 2, чтобы вывести работу с файлами\n" +
                "Введите 3, чтобы вывести работу с форматом JSON\n" +
                "Введите 4, чтобы вывести работу с форматом XML\n" +
                "Введите 5, чтобы вывести работу с архивом zip\n" +
                "Введите 6, чтобы выйти из программы\n");
            int userChoise = Convert.ToInt32(Console.ReadLine());
            switch (userChoise)
            {
                case 1:
                    Console.WriteLine("-------------------------------------");
                    new SystemInfo().Show();
                    Console.WriteLine("\n-------------------------------------");
                    menu();
                    break;
                case 2:
                    Console.WriteLine("-------------------------------------");
                    new FileWork().FileCode();
                    Console.WriteLine("\n-------------------------------------");
                    menu();
                    break;
                case 3:
                    Console.WriteLine("-------------------------------------");
                    new JsonWork().JsonCode();
                    Console.WriteLine("\n-------------------------------------");
                    menu();
                    break;
                case 4:
                    Console.WriteLine("-------------------------------------");
                    new XmlWork().XmlCode();
                    Console.WriteLine("\n-------------------------------------");
                    menu();
                    break;
                case 5:
                    Console.WriteLine("-------------------------------------");
                    new ZipWork().ZipCode();
                    Console.WriteLine("\n-------------------------------------");
                    menu();
                    break;
                case 6:
                    break;
            }
        }
    }

    class SystemInfo
    {
        public void Show()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine($"Название: {drive.Name}");
                Console.WriteLine($"Тип: {drive.DriveType}");
                if (drive.IsReady)
                {
                    Console.WriteLine($"Объем диска: {drive.TotalSize}");
                    Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Метка: {drive.VolumeLabel}");
                }
                Console.WriteLine();
            }
        }
    }

    class FileWork
    {
        public void FileCode()
        {
            try
            {
                string path = @"C:\Users\Misha\Desktop\exampleFile.txt";
                Console.WriteLine("Введите строку: ");
                string text = Console.ReadLine();
                using StreamWriter file = new(path, append: true);
                Console.WriteLine("Данные в файле:");
                Console.WriteLine("___________________________________________");
                file.WriteLine(text);
                file.Close();
                using StreamReader st = new StreamReader(path);
                while (!st.EndOfStream)
                {
                    Console.WriteLine(st.ReadLine());
                }
                st.Close();
                Console.WriteLine("___________________________________________");
                Console.WriteLine("Файл создался и заполнился, находится на рабочем столе. Для продолжения нажмите ENTER");
                Console.ReadLine();
                File.Delete(path);
            }
            catch (Exception i)
            {
                Console.WriteLine(i);
            }
        }
    }

    class JsonWork
    {
        public void JsonCode()
        {
            string path = @"C:\Users\Misha\Desktop\json.json";
            List<model> models = new List<model>();
            Console.WriteLine("Введите количество записей: ");
            string num = Console.ReadLine();
            int number = 0;
            try { number = Convert.ToInt32(num); }
            catch 
            {
                Console.WriteLine("Ошибка, введите пожалуйста целочисленное значение");
            }
            for (int i = 0; i < number; i++)
            {

                Console.WriteLine("Введите имя: ");
                string name_1 = Console.ReadLine();
                int nu;
                bool bol = int.TryParse(name_1, out nu);
                if (bol)
                {
                    Console.WriteLine("Ошибка 1. Введите корректное имя");
                    i--;
                    continue;
                }
                Console.WriteLine("Введите фамилию: ");
                string lastname_1 = Console.ReadLine();
                bol = int.TryParse(lastname_1, out nu);

                if (bol)
                {
                    Console.WriteLine("Ошибка 2. Введите корректную фамилию");
                    i--;
                    continue;
                }
                Console.WriteLine("Введите возраст: ");
                string age_1 = Console.ReadLine();
                bol = int.TryParse(age_1, out nu);
                if (!bol)
                {
                    Console.WriteLine("Ошибка 3. Введите корректный возраст");
                    i--;
                    continue;
                }
                models.Add(new model()
                {
                    name = name_1,
                    lastname = lastname_1,
                    age = nu
                });
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(models, options);
            File.WriteAllText(path, json);
            Console.WriteLine("\nВ файле содержится: \n");
            Console.WriteLine("___________________________________________");
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("___________________________________________");
            File.Delete(path);
        }
    }

    class XmlWork
    {
        public void XmlCode()
        {
            string path = @"C:\Users\Misha\Desktop\xml.xml";
            List<model> lst = new List<model>();
            Console.WriteLine("Введите количество записей: ");
            string num = Console.ReadLine();
            int number = 0;
            try { number = Convert.ToInt32(num); }
            catch
            {
                Console.WriteLine("Ошибка, введите пожалуйста целочисленное значение");
            }
            

            for (int i = 0; i < number; i++)
            {

                Console.WriteLine("Введите имя: ");
                string name_1 = Console.ReadLine();
                int nu;
                bool bol = int.TryParse(name_1, out nu);
                if (bol)
                {
                    Console.WriteLine("Ошибка 1. Введите корректное имя");
                    i--;
                    continue;
                }
                Console.WriteLine("Введите фамилию: ");
                string lastname_1 = Console.ReadLine();
                bol = int.TryParse(lastname_1, out nu);

                if (bol)
                {
                    Console.WriteLine("Ошибка 2. Введите корректную фамилию");
                    i--;
                    continue;
                }
                Console.WriteLine("Введите возраст: ");
                string age_1 = Console.ReadLine();
                bol = int.TryParse(age_1, out nu);
                if (!bol)
                {
                    Console.WriteLine("Ошибка 3. Введите корректный возраст");
                    i--;
                    continue;
                }
                lst.Add(new model()
                {
                    name = name_1,
                    lastname = lastname_1,
                    age = nu
                });

            }
            XmlSerializer formatter = new XmlSerializer(typeof(List<model>));

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, lst);
            }

            Console.WriteLine("\nВ файле содержится: \n");
            Console.WriteLine("___________________________________________");
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine("___________________________________________");
            File.Delete(path);
        }
    }

    class ZipWork
    {
        public void ZipCode()
        {
            string path = @"C:\Users\Misha\Desktop\archive";
            string path1 = @"C:\Users\Misha\Desktop\archive2";
            string zip = @"C:\Users\Misha\Desktop\archive.zip";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] allfiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            if (allfiles.Length == 0)
            {
                using (File.Create(path + "\\1.txt"))
                using (File.Create(path + "\\2.docx"))
                using (File.Create(path + "\\3.xlsx")) ;
            }
            allfiles = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            int i = 0;
            foreach (string filename in allfiles)
            {
                Console.WriteLine(i + ": " + filename);
                i++;
            }
            Console.WriteLine("");
            List<string> LIST = new List<string>();
            while (true)
            {
                Console.WriteLine("Выберите файлы, для завершения введите 'end': ");
                string files = Console.ReadLine();
                if (files == "end") { break; }
                try {
                    int N = Convert.ToInt32(files);
                    LIST.Add(files);
                }
                catch
                {
                    Console.WriteLine("\nОшибка. Введите числовые значения файлов архива.\n");
                }
            }
            i = 0;
            List<string> LIST2 = new List<string>();
            foreach (string filename in allfiles)
            {
                foreach (string num in LIST)
                {
                    int value = Convert.ToInt32(num);
                    if (value == null)
                    {
                        Console.WriteLine("ERROR");
                        return;
                    }
                    if (i == value)
                    {
                        bool bl = false;
                        foreach (string el in LIST2)
                        {
                            if (el == filename)
                            {
                                bl = true;
                            }
                        }
                        if (bl == false)
                        {
                            LIST2.Add(filename);
                        }
                    }
                }
                i++;
            }
            Directory.CreateDirectory(path1);
            int k = 0;
            foreach (string str in LIST2)
            {

                Console.WriteLine(str);
                string str1 = Path.GetFileName(str);
                File.Copy(str, path1 + "\\" + str1);
                k++;

            }
            ZipFile.CreateFromDirectory(path1, zip);
            
            foreach (string str in LIST2)
            {
                string str1 = Path.GetFileName(str);
                File.Delete(path1 + "\\" + str1);
            }
            Directory.Delete(path1);
            Console.WriteLine("\nСоздан архив, в него добавлены выбранные пользователем файлы. Для продолжения нажмите ENTER.\n");
            Console.ReadLine();
            ZipFile.ExtractToDirectory(zip, path1);
            Console.WriteLine("\nАрхив разархивирован, далее архив будет удален. Для продолжения нажмите ENTER.\n");
            Console.ReadLine();
            File.Delete(zip);
            Console.WriteLine("\nДалее будут удалены файлы из папки.\n");
            Console.ReadLine();
            foreach (string str in LIST2)
            {
                string str1 = Path.GetFileName(str);
                File.Delete(path1 + "\\" + str1);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            new MainMenu().menu();
        }

    }

    public class model
    {
        public model()
        {

        }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }
}
