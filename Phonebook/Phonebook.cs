using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/// <summary>
/// Класс, представляющий телефонную книгу.
/// </summary>
public class Phonebook
{
    #region Singleton Implementation

    private static readonly Phonebook _instance = new Phonebook();
    private readonly List<Abonent> _abonents = new List<Abonent>();
    private const string FilePath = "phonebook.txt";

    /// <summary>
    /// Получает экземпляр телефонной книги.
    /// </summary>
    public static Phonebook Instance => _instance;

    /// <summary>
    /// Приватный конструктор для реализации Singleton.
    /// </summary>
    private Phonebook()
    {
        LoadPhonebook();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Добавляет абонента в телефонную книгу.
    /// </summary>
    /// <param name="abonent">Абонент для добавления.</param>
    /// <exception cref="InvalidOperationException">Если абонент уже существует.</exception>
    public void AddAbonent(Abonent abonent)
    {
        if (_abonents.Any(a => a.PhoneNumber == abonent.PhoneNumber))
        {
            throw new InvalidOperationException("Абонент с таким номером телефона уже существует.");
        }

        _abonents.Add(abonent);
        SavePhonebook();
    }

    /// <summary>
    /// Удаляет абонента из телефонной книги по номеру телефона.
    /// </summary>
    /// <param name="phoneNumber">Номер телефона абонента для удаления.</param>
    public void RemoveAbonent(string phoneNumber)
    {
        var abonent = _abonents.FirstOrDefault(a => a.PhoneNumber == phoneNumber);
        if (abonent != null)
        {
            _abonents.Remove(abonent);
            SavePhonebook();
        }
    }

    /// <summary>
    /// Получает абонента по номеру телефона.
    /// </summary>
    /// <param name="phoneNumber">Номер телефона абонента.</param>
    /// <returns>Абонент или null, если не найден.</returns>
    public Abonent GetAbonentByPhoneNumber(string phoneNumber)
    {
        return _abonents.FirstOrDefault(a => a.PhoneNumber == phoneNumber);
    }

    /// <summary>
    /// Получает номер телефона по имени.
    /// </summary>
    /// <param name="name">Имя абонента.</param>
    /// <returns>Номер телефона или null, если не найден.</returns>
    public string GetPhoneNumberByName(string name)
    {
        var abonent = _abonents.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        return abonent?.PhoneNumber;
    }

    /// <summary>
    /// Получает всех абонентов.
    /// </summary>
    /// <returns>Список абонентов.</returns>
    public List<Abonent> GetAllAbonents()
    {
        return _abonents.ToList();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Загружает телефонную книгу из файла.
    /// </summary>
    private void LoadPhonebook()
    {
        if (File.Exists(FilePath))
        {
            var lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 2)
                {
                    _abonents.Add(new Abonent(parts[0], parts[1]));
                }
            }
        }
    }

    /// <summary>
    /// Сохраняет телефонную книгу в файл.
    /// </summary>
    private void SavePhonebook()
    {
        using (var writer = new StreamWriter(FilePath))
        {
            foreach (var abonent in _abonents)
            {
                writer.WriteLine($"{abonent.PhoneNumber};{abonent.Name}");
            }
        }
    }

    #endregion
}