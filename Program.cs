//Додаємо бiблiотеку для роботи с файлом .json
using Newtonsoft.Json;


class Diary
{
    // Оголошуємо змiнну Records
    static List<Record> Records = new List<Record>();
    // Оголошуємо змiнну та привласнюємо слово Menu
    //static string filePath = "Menu";

    // Запускаємо програму i викликаємо метод завантаження записiв та метод меню
    static void Main()
    {
        LoadRecords();
        DisplayMenu();
    }

    static void LoadRecords()
    {
        // Перевiряємо, чи iснує файл "Records.json" в поточному каталозi
        if (File.Exists("Records.json"))
        {
            // Якщо файл iснує, зчитуємо вмiст файлу у змiнну jsonData
            string jsonData = File.ReadAllText("Records.json");
            // Якщо файл порожнiй, метод DeserializeObject поверне значення null
            Records = JsonConvert.DeserializeObject<List<Record>>(jsonData) ?? new List<Record>();
        }
    }

    static void DisplayMenu()
    {
        while (true) 
        { 
            Console.WriteLine("Щоденник\n\n 1. Створити запис\n 2. Переглянути записи\n 3. Вийти з програми\n");
    
            Console.Write("Виберiть функцiю: ");
            string? choice = Console.ReadLine();
    
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Створення запису");
                    CreateRecords();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Список записiв");
                    ViewRecords();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
/*                case "4":
                    Console.Clear();
                    CreateRecordsWithItems();
                    break;
*/
                default:
                    Console.WriteLine("Невiрний вибiр. Спробуйте ще раз.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
    }

  /*
    static void ClearConsoleAndAddMenu()
    {
        Console.WriteLine("\nНатиснiть будь-яку клавiшу для переходу в меню");
        Console.ReadKey();
        Console.Clear();
        // Перевiряє змiнну filePath для виведення потрiбного меню
        //if (filePath == "Menu")
        //{
            DisplayMenu();
        //}
        //else if (filePath == "RecordsMenu")
        //{
        //    ViewRecords();
        //}
    }
  */

    static void CreateRecords()
    {
        while (true) 
        { 
            Console.Clear();
            Console.Write("Введiть назву запису: ");
            string recordsTitle = Console.ReadLine();
            Console.Write("Введiть вмiст запису: ");
            string recordsContent = Console.ReadLine();
    
            // Перевiряє, чи назва та вмiст запису не є пустими рядками
            if (!string.IsNullOrEmpty(recordsTitle) && !string.IsNullOrEmpty(recordsContent))
            {
                while (true)
                { 

                   Console.Clear();

                   // Якщо назва та вмiст не є пустими, додає новий запис до списку Records
                   Records.Add(new Record { Title = recordsTitle, Content = recordsContent });

                   Console.WriteLine("Назву запису: " + recordsTitle);
                   Console.WriteLine("Вмiст запису: " + recordsContent);
                   Console.WriteLine("\n1. Зберегти запис");
                   Console.WriteLine("2. Не зберiгати запис");
                   Console.Write("Виберiть опцiю: ");
                   string choice = Console.ReadLine();
                   switch (choice)
                   {
                       case "1":
                           Console.WriteLine("\nЗапис успiшно збереженно");    
                           SaveRecords();
                           Console.ReadLine();
                           Console.Clear();
                           DisplayMenu();
                           break;
                       case "2":
                           // Видаляє останнiй доданий запис зi списку перед виходом у головне меню
                           Records.RemoveAt(Records.Count - 1);
                           Console.Clear();
                           DisplayMenu();
                           break;
                       default:
                           Console.WriteLine("\nНевiрний вибiр");
                           // Видаляє останнiй доданий запис зi списку перед виходом у головне меню
                           Records.RemoveAt(Records.Count - 1);
                           Console.ReadLine();
                           break;
                   }
                }
            }
            else
            {
                Console.WriteLine("Назва та/або вмiст не можуть бути пустими!");
                Console.ReadLine();
            }
        }
    }
        
    static void SaveRecords()
    {
        // Створює рядок JSON, який мiстить iнформацiю про всi записи у списку Records
        string jsonData = JsonConvert.SerializeObject(Records);
        // Записує рядок у файл "Records.json"
        File.WriteAllText("Records.json", jsonData);
    }

    static void SortRecordsByName()
    {
        // Використовує LINQ для сортування записiв за назвою у алфавiтному порядку
        Records = Records.OrderBy(f => f.Title).ToList();
    }

    static void ViewRecords()
    {
        Console.Clear();
        Console.WriteLine("Список записiв:");

        // Перевiряє, чи є записи у списку
        if (Records.Count == 0)
        {
            Console.WriteLine("\nСписок порожнiй\n\n\nНатиснiть будь-яку клавiшу для переходу в меню");
            Console.ReadLine();
            Console.Clear();
        }
        else
        {
            int index = 1;
            foreach (var record in Records)
            {
                Console.WriteLine($"{index}. {record.Title}");
                index++;
            }

            Console.WriteLine("\nСписок функцiй:");
            Console.WriteLine("01 | Фiльтрацiя А до Я");
            Console.WriteLine("02 | Пошук запису");
            Console.WriteLine("03 | Повернутись назад");
            Console.WriteLine("04 | Введення номера запису для перегляду");

            Console.Write("\nВибiр функцiї: ");
            string choice = Console.ReadLine();

            if (choice == "04")
            {
                // Якщо користувач обрав перегляд конкретного запису, запитує номер запису
                Console.Write("Введiть номер запису для перегляду: ");
                string recordChoice = Console.ReadLine();
                if (int.TryParse(recordChoice, out int displayIndex) && displayIndex > 0 && displayIndex <= Records.Count)
                {
                    // Викликає метод перегляду запису з вiдповiдним iндексом
                    ViewRecord(displayIndex - 1);
                }
                else
                {
                    Console.WriteLine("Невiрний номер запису");
                    Console.ReadLine();
                    ViewRecords();
                }
            }
            else
            {
                switch (choice)
                {
                    case "01":
                        SortRecordsByName();
                        ViewRecords();
                        break;
                    case "02":
                        SearchRecordByTitle();
                        ViewRecords();
                        break;
                    case "03":
                        Console.Clear();
                        DisplayMenu();
                        break;
                    default:
                        Console.WriteLine("Невiрний вибiр");
                        Console.ReadLine();
                        ViewRecords();
                        break;
                }
            }


        }
    }

    static void ViewRecord(int index)
    {
        while (true) 
        { 
            var selectedRecord = Records[index]; // Вибирає запис за вказаним iндексом
            Console.Clear();
            Console.WriteLine($"Назва: {selectedRecord.Title}");
            Console.WriteLine($"Вмiст: {selectedRecord.Content}");
    
            Console.Write("\nСписок функцiй:\n 1. Редагувати запис\n 2. Видалити запис\n 3. Повернутись до списку записiв\n\nВибiр функцiї: ");
            string choice = Console.ReadLine();
    
            switch (choice)
            {
                case "1":
                    EditRecords(index);
                    ViewRecords();
                    break;
                case "2":
                    DeleteRecords(index);
                    ViewRecords();
                    break;
                case "3":
                    ViewRecords();
                    break;
                default:
                    Console.WriteLine("Невiрний вибiр");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void EditRecords(int index)
    {
        Console.Write("Введiть нову назву запису: ");
        string? newrecordsTitle = Console.ReadLine(); // Зчитує нову назву
        Console.Write("Введiть новий змiст запису: "); // Зчитує новий змiст
        string? newrecordsContent = Console.ReadLine();

        // Перевiряє, чи назва та вмiст запису не є пустими рядками
        if (!string.IsNullOrEmpty(newrecordsTitle) && !string.IsNullOrEmpty(newrecordsContent))
        { 
            // Змiнюємо запис у списку за вказаним iндексом на новий
            Records[index] = new Record { Title = newrecordsTitle, Content = newrecordsContent };
            SaveRecords();
            Console.WriteLine("Запис успiшно вiдредагований!");
            Console.ReadLine();
            Console.Clear();
        }

        else
        {
            Console.WriteLine("Назва та/або вмiст не можуть бути пустими!");
            Console.ReadLine();
        }

    }

    static void DeleteRecords(int index) 
    {
        Records.RemoveAt(index); // Видаляє запис iз списку за вказаним iндексом
        SaveRecords();
        Console.WriteLine("Запис успiшно видалена!");
    }

    static void SearchRecordByTitle()
    {
        Console.Write("Введiть назву запису для пошуку: ");
        string? searchQuery = Console.ReadLine()?.ToLower(); // Перетворює введений запит у нижній регістр для ігнорування регістру

        if (!string.IsNullOrEmpty(searchQuery))
        {
            List<Record> foundRecords = Records.Where(record => record.Title.ToLower().Contains(searchQuery)).ToList(); // Використовуємо LINQ для швидкого пошуку

            if (foundRecords.Any()) // Перевіряє, чи є знайдені записи
            {
                Console.Clear();
                Console.WriteLine("Список знайденних записiв:");

                for (int i = 0; i < foundRecords.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {foundRecords[i].Title}"); // Виводе знайдені записи на екран
                }

                Console.Write("\nВведiть номер запису для перегляду: ");
                if (int.TryParse(Console.ReadLine(), out int displayIndex) && displayIndex > 0 && displayIndex <= foundRecords.Count)
                {
                    ViewRecord(Records.IndexOf(foundRecords[displayIndex - 1])); // Переглядаємо обраний запис
                }
                else
                {
                    Console.WriteLine("\nНеправильний номер запису.\nНатиснiть на будь-яку клавiшу для повернення у список записiв");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Записiв з такою назвою не знайдено.");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("Назва не може бути пустою!");
            Console.ReadLine();
        }
    }


    /*
        static void CreateRecordsWithItems()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введiть назву запису: ");
                string recordsTitle = Console.ReadLine();
                Console.WriteLine("Напишiть \"1\" для закiнчення написання вмiсту запису.");
                Console.WriteLine("Вмiст запису:\n");

                // Створення списку для зберiгання елементiв запису
                List<string> items = new List<string>();

                while (true)
                {
                    Console.Write("> ");
                    string input = Console.ReadLine();

                    // Якщо користувач ввiв "1", закiнчити ввiд елементiв
                    if (input == "end")
                    {
                        break;
                    }

                    // Додати введений елемент до списку
                    items.Add(input);
                }

                // Перетворити список елементiв в рядок з роздiльником ";"
                string recordsContent = string.Join("\n- ", items);

                // Виведення назви та вмiсту запису
                Console.WriteLine("\nНазва запису: " + recordsTitle);
                Console.WriteLine("Вмiст запису: ");
                Console.WriteLine(recordsContent);

                Console.WriteLine("\n1. Зберегти запис");
                Console.WriteLine("2. Не зберiгати та вийти в головне меню");
                Console.Write("Виберiть опцiю: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nЗапис успiшно збереженно");
                        Records.Add(new Record { Title = recordsTitle, Content = recordsContent });
                        SaveRecords();
                        Console.ReadLine();
                        Console.Clear();
                        DisplayMenu();
                        break;
                    case "2":
                        Console.Clear();
                        DisplayMenu();
                        break;
                    default:
                        Console.WriteLine("Невiрний вибiр");
                        Console.ReadLine();
                        break;
                }
            }
        }
    */
}

class Record
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }

