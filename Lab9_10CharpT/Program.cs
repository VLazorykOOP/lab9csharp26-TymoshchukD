using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n===== Лабораторна робота №9 =====");
                Console.WriteLine("1 - Завдання 1.8 Stack: обчислення формули");
                Console.WriteLine("2 - Завдання 2.8 Queue: співробітники");
                Console.WriteLine("3 - Завдання 3 ArrayList: задачі 1 і 2 через ArrayList");
                Console.WriteLine("4 - Завдання 4 Hashtable: каталог музичних дисків");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Lab9T1 lab9task1 = new Lab9T1();
                        lab9task1.Run();
                        break;

                    case "2":
                        Lab9T2 lab9task2 = new Lab9T2();
                        lab9task2.Run();
                        break;

                    case "3":
                        Lab9T3 lab9task3 = new Lab9T3();
                        lab9task3.Run();
                        break;

                    case "4":
                        Lab9T4 lab9task4 = new Lab9T4();
                        lab9task4.Run();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }
    }

    // ============================================================
    // Завдання 1.8
    // Stack: обчислення формули виду M(...,...) або m(...,...)
    // ============================================================
    internal class Lab9T1
    {
        private const string FileName = "formula.txt";

        public void Run()
        {
            CreateExampleFileIfNotExists();

            string formula = File.ReadAllText(FileName).Trim();

            Console.WriteLine("\n--- Завдання 1.8 Stack ---");
            Console.WriteLine("Формула з файлу:");
            Console.WriteLine(formula);

            int result = CalculateFormulaWithStack(formula);

            Console.WriteLine("Результат = " + result);
        }

        private void CreateExampleFileIfNotExists()
        {
            if (!File.Exists(FileName))
            {
                File.WriteAllText(FileName, "M(m(3,5),M(1,2))");
            }
        }

        private int CalculateFormulaWithStack(string formula)
        {
            Stack<object> stack = new Stack<object>();

            foreach (char ch in formula)
            {
                if (char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                }
                else if (ch == 'M' || ch == 'm')
                {
                    stack.Push(ch);
                }
                else if (ch == ')')
                {
                    int second = (int)stack.Pop();
                    int first = (int)stack.Pop();
                    char operation = (char)stack.Pop();

                    int result;

                    if (operation == 'M')
                    {
                        result = Math.Max(first, second);
                    }
                    else
                    {
                        result = Math.Min(first, second);
                    }

                    stack.Push(result);
                }
            }

            return (int)stack.Pop();
        }
    }

    // ============================================================
    // Завдання 2.8
    // Queue: файл зі співробітниками
    // Формат рядка:
    // Прізвище;Ім'я;По батькові;Стать;Вік;Зарплата
    // ============================================================
    internal class Lab9T2
    {
        private const string FileName = "employees.txt";

        public void Run()
        {
            CreateExampleFileIfNotExists();

            Queue<Employee> employeesQueue = new Queue<Employee>();

            Console.WriteLine("\n--- Завдання 2.8 Queue ---");
            Console.WriteLine("Зчитування співробітників з файлу...");

            foreach (string line in File.ReadLines(FileName))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                Employee employee = ParseEmployee(line);

                if (employee != null)
                {
                    employeesQueue.Enqueue(employee);
                }
            }

            Console.WriteLine("\nСпівробітники фірми:");

            while (employeesQueue.Count > 0)
            {
                Employee employee = employeesQueue.Dequeue();
                employee.Show();
            }
        }

        private void CreateExampleFileIfNotExists()
        {
            if (!File.Exists(FileName))
            {
                string[] lines =
                {
                    "Петренко;Іван;Олегович;чоловіча;25;18000",
                    "Шевченко;Марія;Іванівна;жіноча;30;22000",
                    "Коваль;Олег;Петрович;чоловіча;41;26000",
                    "Мельник;Анна;Сергіївна;жіноча;28;21000"
                };

                File.WriteAllLines(FileName, lines);
            }
        }

        private Employee ParseEmployee(string line)
        {
            string[] parts = line.Split(';');

            if (parts.Length != 6)
            {
                Console.WriteLine("Помилка у рядку: " + line);
                return null;
            }

            string surname = parts[0];
            string name = parts[1];
            string patronymic = parts[2];
            string gender = parts[3];

            int age = int.Parse(parts[4]);
            decimal salary = decimal.Parse(parts[5]);

            return new Employee(surname, name, patronymic, gender, age, salary);
        }
    }

    // ============================================================
    // Завдання 3
    // Розв'язати задачі 1 і 2 через ArrayList
    // ============================================================
    internal class Lab9T3
    {
        private const string FormulaFileName = "formula.txt";
        private const string EmployeesFileName = "employees.txt";

        public void Run()
        {
            Console.WriteLine("\n--- Завдання 3 ArrayList ---");
            Console.WriteLine("1 - Формула через ArrayList");
            Console.WriteLine("2 - Співробітники через ArrayList");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RunFormulaArrayList();
            }
            else if (choice == "2")
            {
                RunEmployeesArrayList();
            }
            else
            {
                Console.WriteLine("Невірний вибір!");
            }
        }

        private void RunFormulaArrayList()
        {
            if (!File.Exists(FormulaFileName))
            {
                File.WriteAllText(FormulaFileName, "M(m(3,5),M(1,2))");
            }

            string formula = File.ReadAllText(FormulaFileName).Trim();

            Console.WriteLine("\nФормула:");
            Console.WriteLine(formula);

            int result = CalculateFormulaWithArrayList(formula);

            Console.WriteLine("Результат = " + result);
        }

        private int CalculateFormulaWithArrayList(string formula)
        {
            ArrayList stack = new ArrayList();

            foreach (char ch in formula)
            {
                if (char.IsDigit(ch))
                {
                    stack.Add(ch - '0');
                }
                else if (ch == 'M' || ch == 'm')
                {
                    stack.Add(ch);
                }
                else if (ch == ')')
                {
                    int second = (int)stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);

                    int first = (int)stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);

                    char operation = (char)stack[stack.Count - 1];
                    stack.RemoveAt(stack.Count - 1);

                    int result;

                    if (operation == 'M')
                    {
                        result = Math.Max(first, second);
                    }
                    else
                    {
                        result = Math.Min(first, second);
                    }

                    stack.Add(result);
                }
            }

            return (int)stack[stack.Count - 1];
        }

        private void RunEmployeesArrayList()
        {
            if (!File.Exists(EmployeesFileName))
            {
                string[] lines =
                {
                    "Петренко;Іван;Олегович;чоловіча;25;18000",
                    "Шевченко;Марія;Іванівна;жіноча;30;22000",
                    "Коваль;Олег;Петрович;чоловіча;41;26000",
                    "Мельник;Анна;Сергіївна;жіноча;28;21000"
                };

                File.WriteAllLines(EmployeesFileName, lines);
            }

            ArrayList employees = new ArrayList();

            foreach (string line in File.ReadLines(EmployeesFileName))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                Employee employee = ParseEmployee(line);

                if (employee != null)
                {
                    employees.Add(employee);
                }
            }

            Console.WriteLine("\nСпівробітники через ArrayList:");

            foreach (Employee employee in employees)
            {
                employee.Show();
            }
        }

        private Employee ParseEmployee(string line)
        {
            string[] parts = line.Split(';');

            if (parts.Length != 6)
            {
                Console.WriteLine("Помилка у рядку: " + line);
                return null;
            }

            return new Employee(
                parts[0],
                parts[1],
                parts[2],
                parts[3],
                int.Parse(parts[4]),
                decimal.Parse(parts[5])
            );
        }
    }

    // ============================================================
    // Завдання 4
    // Hashtable: каталог музичних компакт-дисків
    // ============================================================
    internal class Lab9T4
    {
        private Hashtable catalog = new Hashtable();

        public void Run()
        {
            CreateExampleCatalog();

            while (true)
            {
                Console.WriteLine("\n--- Завдання 4 Hashtable ---");
                Console.WriteLine("1 - Додати диск");
                Console.WriteLine("2 - Видалити диск");
                Console.WriteLine("3 - Додати пісню до диска");
                Console.WriteLine("4 - Видалити пісню з диска");
                Console.WriteLine("5 - Переглянути весь каталог");
                Console.WriteLine("6 - Переглянути окремий диск");
                Console.WriteLine("7 - Пошук пісень за виконавцем");
                Console.WriteLine("0 - Назад");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddDisk();
                        break;

                    case "2":
                        RemoveDisk();
                        break;

                    case "3":
                        AddSong();
                        break;

                    case "4":
                        RemoveSong();
                        break;

                    case "5":
                        ShowCatalog();
                        break;

                    case "6":
                        ShowDisk();
                        break;

                    case "7":
                        SearchByArtist();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }
            }
        }

        private void CreateExampleCatalog()
        {
            ArrayList disk1 = new ArrayList();
            disk1.Add(new Song("Imagine Dragons", "Believer"));
            disk1.Add(new Song("Imagine Dragons", "Thunder"));

            ArrayList disk2 = new ArrayList();
            disk2.Add(new Song("The Weeknd", "Blinding Lights"));
            disk2.Add(new Song("The Weeknd", "Starboy"));

            catalog["Pop Hits"] = disk1;
            catalog["Best Music"] = disk2;
        }

        private void AddDisk()
        {
            Console.Write("Введіть назву диска: ");
            string diskName = Console.ReadLine();

            if (catalog.ContainsKey(diskName))
            {
                Console.WriteLine("Такий диск вже існує!");
                return;
            }

            catalog.Add(diskName, new ArrayList());
            Console.WriteLine("Диск додано.");
        }

        private void RemoveDisk()
        {
            Console.Write("Введіть назву диска для видалення: ");
            string diskName = Console.ReadLine();

            if (!catalog.ContainsKey(diskName))
            {
                Console.WriteLine("Диск не знайдено!");
                return;
            }

            catalog.Remove(diskName);
            Console.WriteLine("Диск видалено.");
        }

        private void AddSong()
        {
            Console.Write("Введіть назву диска: ");
            string diskName = Console.ReadLine();

            if (!catalog.ContainsKey(diskName))
            {
                Console.WriteLine("Диск не знайдено!");
                return;
            }

            Console.Write("Введіть виконавця: ");
            string artist = Console.ReadLine();

            Console.Write("Введіть назву пісні: ");
            string title = Console.ReadLine();

            ArrayList songs = (ArrayList)catalog[diskName];
            songs.Add(new Song(artist, title));

            Console.WriteLine("Пісню додано.");
        }

        private void RemoveSong()
        {
            Console.Write("Введіть назву диска: ");
            string diskName = Console.ReadLine();

            if (!catalog.ContainsKey(diskName))
            {
                Console.WriteLine("Диск не знайдено!");
                return;
            }

            Console.Write("Введіть назву пісні для видалення: ");
            string title = Console.ReadLine();

            ArrayList songs = (ArrayList)catalog[diskName];

            for (int i = 0; i < songs.Count; i++)
            {
                Song song = (Song)songs[i];

                if (song.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    songs.RemoveAt(i);
                    Console.WriteLine("Пісню видалено.");
                    return;
                }
            }

            Console.WriteLine("Пісню не знайдено.");
        }

        private void ShowCatalog()
        {
            Console.WriteLine("\nВесь каталог:");

            foreach (DictionaryEntry entry in catalog)
            {
                Console.WriteLine("\nДиск: " + entry.Key);

                ArrayList songs = (ArrayList)entry.Value;

                if (songs.Count == 0)
                {
                    Console.WriteLine("  Пісень немає.");
                }
                else
                {
                    foreach (Song song in songs)
                    {
                        song.Show();
                    }
                }
            }
        }

        private void ShowDisk()
        {
            Console.Write("Введіть назву диска: ");
            string diskName = Console.ReadLine();

            if (!catalog.ContainsKey(diskName))
            {
                Console.WriteLine("Диск не знайдено!");
                return;
            }

            Console.WriteLine("\nДиск: " + diskName);

            ArrayList songs = (ArrayList)catalog[diskName];

            if (songs.Count == 0)
            {
                Console.WriteLine("Пісень немає.");
            }
            else
            {
                foreach (Song song in songs)
                {
                    song.Show();
                }
            }
        }

        private void SearchByArtist()
        {
            Console.Write("Введіть виконавця: ");
            string artist = Console.ReadLine();

            bool found = false;

            Console.WriteLine("\nРезультати пошуку:");

            foreach (DictionaryEntry entry in catalog)
            {
                string diskName = (string)entry.Key;
                ArrayList songs = (ArrayList)entry.Value;

                foreach (Song song in songs)
                {
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Диск: " + diskName);
                        song.Show();
                        found = true;
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("Пісень цього виконавця не знайдено.");
            }
        }
    }

    // ============================================================
    // Допоміжний клас Employee
    // ============================================================
    internal class Employee
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public Employee(string surname, string name, string patronymic, string gender, int age, decimal salary)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Gender = gender;
            Age = age;
            Salary = salary;
        }

        public void Show()
        {
            Console.WriteLine($"{Surname} {Name} {Patronymic}, стать: {Gender}, вік: {Age}, зарплата: {Salary}");
        }
    }

    // ============================================================
    // Допоміжний клас Song
    // ============================================================
    internal class Song
    {
        public string Artist { get; set; }
        public string Title { get; set; }

        public Song(string artist, string title)
        {
            Artist = artist;
            Title = title;
        }

        public void Show()
        {
            Console.WriteLine($"  Виконавець: {Artist}, пісня: {Title}");
        }
    }
}