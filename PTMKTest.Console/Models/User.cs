using PTMKTest.Console.Enums;

namespace PTMKTest.Console.Models;

public class User
{
    public User(string fio, DateOnly berthDay, Gender gender)
    {
        FIO = fio;
        BerthDay = berthDay.ToString("yyyy-MM-dd");
        Gender = gender;
    }

    public string FIO { get; init; }
    public string BerthDay { get; init; }
    public Gender Gender { get; init; }
}