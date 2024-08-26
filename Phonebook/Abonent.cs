using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Класс, представляющий абонента телефонной книги.
/// </summary>
public class Abonent
{
    public string PhoneNumber { get; }
    public string Name { get; }

    public Abonent(string phoneNumber, string name)
    {
        PhoneNumber = phoneNumber;
        Name = name;
    }
}

