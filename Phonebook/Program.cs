
class Program
{
    static void Main(string[] args)
    {
        Phonebook phonebook = Phonebook.Instance;

        // Загружаем абонентов из файла при старте программы
        phonebook.LoadFromFile("phonebook.txt");

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить абонента");
            Console.WriteLine("2. Удалить абонента");
            Console.WriteLine("3. Найти абонента по номеру телефона");
            Console.WriteLine("4. Найти номер телефона по имени");
            Console.WriteLine("5. Показать всех абонентов");
            Console.WriteLine("6. Выход");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddAbonent(phonebook);
                    break;
                case "2":
                    RemoveAbonent(phonebook);
                    break;
                case "3":
                    FindAbonentByPhone(phonebook);
                    break;
                case "4":
                    FindPhoneByAbonentName(phonebook);
                    break;
                case "5":
                    ShowAllAbonents(phonebook);
                    break;
                case "6":
                    phonebook.SaveToFile("phonebook.txt");
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }

    static void AddAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();

        Console.Write("Введите имя абонента: ");
        string name = Console.ReadLine();

        bool added = phonebook.AddAbonent(new Abonent(phone, name));
        if (added)
        {
            Console.WriteLine("Абонент добавлен.");
        }
        else
        {
            Console.WriteLine("Абонент с таким номером уже существует.");
        }
    }

    static void RemoveAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона для удаления: ");
        string phone = Console.ReadLine();
        bool removed = phonebook.RemoveAbonent(phone);
        if (removed)
        {
            Console.WriteLine("Абонент удален.");
        }
        else
        {
            Console.WriteLine("Абонент с таким номером не найден.");
        }
    }

    static void FindAbonentByPhone(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона: ");
        string phone = Console.ReadLine();
        var abonent = phonebook.GetAbonentByPhone(phone);

        if (abonent != null)
        {
            Console.WriteLine($"Найден абонент: {abonent.Name}");
        }
        else
        {
            Console.WriteLine("Абонент с таким номером не найден.");
        }
    }

    static void FindPhoneByAbonentName(Phonebook phonebook)
    {
        Console.Write("Введите имя абонента: ");
        string name = Console.ReadLine();
        var phone = phonebook.GetPhoneByName(name);

        if (phone != null)
        {
            Console.WriteLine($"Номер телефона: {phone}");
        }
        else
        {
            Console.WriteLine("Абонент с таким именем не найден.");
        }
    }

    static void ShowAllAbonents(Phonebook phonebook)
    {
        foreach (var abonent in phonebook.GetAllAbonents())
        {
            Console.WriteLine($"Имя: {abonent.Name}, Телефон: {abonent.Phone}");
        }
    }
}

public class Abonent
{
    public string Phone { get; }
    public string Name { get; }

    public Abonent(string phone, string name)
    {
        Phone = phone;
        Name = name;
    }
}
public class Phonebook
{
    private static readonly Phonebook _instance = new Phonebook();
    private List<Abonent> _abonents = new List<Abonent>();

    private Phonebook() { }

    public static Phonebook Instance => _instance;

    public bool AddAbonent(Abonent abonent)
    {
        if (_abonents.Any(a => a.Phone == abonent.Phone))
        {
            return false; // Абонент с таким номером уже существует
        }
        _abonents.Add(abonent);
        return true;
    }

    public bool RemoveAbonent(string phone)
    {
        var abonentToRemove = _abonents.SingleOrDefault(a => a.Phone == phone);
        if (abonentToRemove != null)
        {
            _abonents.Remove(abonentToRemove);
            return true;
        }
        return false;
    }

    public Abonent GetAbonentByPhone(string phone)
    {
        return _abonents.SingleOrDefault(a => a.Phone == phone);
    }

    public string GetPhoneByName(string name)
    {
        var abonent = _abonents.SingleOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return abonent?.Phone;
    }

    public IEnumerable<Abonent> GetAllAbonents()
    {
        return _abonents;
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var abonent in _abonents)
            {
                writer.WriteLine($"{abonent.Phone},{abonent.Name}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        if (File.Exists(filename))
        {
            var lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 2)
                {
                    AddAbonent(new Abonent(parts[0], parts[1]));
                }
            }
        }
    }
}