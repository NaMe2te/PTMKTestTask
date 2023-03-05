using System.Diagnostics;
using MySql.Data.MySqlClient;
using PTMKTest.Console.Configurations;
using PTMKTest.Console.Enums;
using PTMKTest.Console.Exseptions;
using PTMKTest.Console.Models;
using PTMKTest.Console.Repositories;

namespace PTMKTest.Console.Services;

public class UserService
{
    private readonly UserRepository _repository;
    
    public UserService(UserRepository userRepository)
    {
        _repository = userRepository;
    }

    public async Task CreateDataBaseAsync()
    {
         await _repository.CreateDataBaseAsync();
    }

    public async Task<User> CreateUserAsync(string[] user)
    {
        if (user.Length != 6)
            throw new InvalidUserParametersException();
        string fio = GetFio(user);
        var birthDay = DateOnly.Parse(user[4]);
        var gender = Enum.Parse<Gender>(user[5]);
        
        User newUser = new User(fio, birthDay, gender);
        await _repository.CreateAsync(newUser);

        return newUser;
    }

    public async Task<List<string>> GetUniqueUsersAsync()
    {
        return await _repository.GetUniqueUsersAsync();
    }

    public async Task FillTheTableWithMillionValues()
    {
        string[] fios =
        {
            "Ivanov Nikita Igorevich", "Pinch Maks Stena", "Volvo Zidan Pula", "Wishna Witalina Valerevna",
            "Orimov Oreshka Antonovna", "Mustaeva Ksu Ruslanovna", "Firsova Dasha Petrova", "Telephonov Arseni Antonov",
            "Puplin Peta Petrov", "Shishkin Alex Viktorovich"
        };

        Gender[] genders = new[] {Gender.Female, Gender.Male};
        DateOnly dateOnly = new DateOnly(1990, 10, 1);
        Random random = new Random();
        for (int i = 0; i < 350_000; i++)
        {
            dateOnly = dateOnly.AddDays(1);
            await _repository.CreateAsync(new User(fios[random.Next(0, 9)], dateOnly, genders[i % 2]));
        }

        for (int i = 0; i < 100; i++)
        {
            dateOnly = dateOnly.AddDays(1);
            await _repository.CreateAsync(new User("Fioletov Nikita Fedorov", dateOnly, genders[1]));
        }
    }

    public async Task CreateDataBaseIndex()
    {
        await _repository.CreateIndexAsync();
    }
    
    public async Task<TimeSpan> GetMalesWithFirstLetterF()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        await _repository.GetMalesWithFirstLetterFAsync();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    private string GetFio(string[] userValues)
    {
        return $"{userValues[1]} {userValues[2]} {userValues[3]}";
    }
    
}