using System;

class Program
{
    // Стартовая точка для приложения
    static void Main(string[] args)
    {
        Phonebook phonebook = Phonebook.Instance;
        string command;

        do
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Добавить абонента");
            Console.WriteLine("2. Удалить абонента");
            Console.WriteLine("3. Найти абонента по номеру телефона");
            Console.WriteLine("4. Найти номер телефона по имени");
            Console.WriteLine("5. Показать всех абонентов");
            Console.WriteLine("6. Выход");

            command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "1":
                    AddAbonent(phonebook);
                    break;
                case "2":
                    RemoveAbonent(phonebook);
                    break;
                case "3":
                    GetAbonentByPhoneNumber(phonebook);
                    break;
                case "4":
                    GetPhoneNumberByName(phonebook);
                    break;
                case "5":
                    ShowAllAbonents(phonebook);
                    break;
                case "6":
                    Console.WriteLine("Выход из приложения.");
                    break;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
        while (command != "6");
    }

    private static void AddAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона: ");
        string phoneNumber = Console.ReadLine();
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();

        try
        {
            phonebook.AddAbonent(new Abonent(phoneNumber, name));
            Console.WriteLine($"Абонент {name} добавлен.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void RemoveAbonent(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона для удаления: ");
        string phoneNumber = Console.ReadLine();
        phonebook.RemoveAbonent(phoneNumber);
        Console.WriteLine("Абонент удален, если он существовал.");
    }

    private static void GetAbonentByPhoneNumber(Phonebook phonebook)
    {
        Console.Write("Введите номер телефона для поиска: ");
        string phoneNumber = Console.ReadLine();
        var abonent = phonebook.GetAbonentByPhoneNumber(phoneNumber);

        if (abonent != null)
            Console.WriteLine($"Абонент найден: {abonent.Name}");
        else
            Console.WriteLine("Абонент не найден.");
    }

    private static void GetPhoneNumberByName(Phonebook phonebook)
    {
        Console.Write("Введите имя для поиска: ");
        string name = Console.ReadLine();
        string phoneNumber = phonebook.GetPhoneNumberByName(name);

        if (phoneNumber != null)
            Console.WriteLine($"Номер телефона: {phoneNumber}");
        else
            Console.WriteLine("Абонент не найден.");
    }

    private static void ShowAllAbonents(Phonebook phonebook)
    {
        var abonents = phonebook.GetAllAbonents();
        if (abonents.Count == 0)
        {
            Console.WriteLine("Список абонентов пуст.");
            return;
        }

        Console.WriteLine("Список всех абонентов:");
        foreach (var abonent in abonents)
        {
            Console.WriteLine($"Имя: {abonent.Name}, Телефон: {abonent.PhoneNumber}");
        }
    }
}