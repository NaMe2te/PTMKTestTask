using MySql.Data.MySqlClient;
using PTMKTest.Console.Configurations;
using PTMKTest.Console.Exseptions;
using PTMKTest.Console.Models;
using PTMKTest.Console.Repositories;
using PTMKTest.Console.Services;

namespace PTMKTest.Console;

public class Menu
{
    private readonly UserService _service;
    private long _lastRequestExecutionTime;
    
    public Menu()
    {
        _service = new UserService(new UserRepository(new AppConfig()));
        StartMessage();
    }

    public void StartMessage()
    { 
        System.Console.WriteLine("Commands (without \"-\"):");
        System.Console.WriteLine("-1- Create data base");
        System.Console.WriteLine("-2 {FIO} {Birth Day} {Gender}- Creating a table entry (without \"{\" and \"}\")");
        System.Console.WriteLine("-3- Get Unique with fio and birth day Users");
        System.Console.WriteLine("-4- Fill The Table With Million Values");
        System.Console.WriteLine("-5- Get Males With First Letter F");
        System.Console.WriteLine("-6- Get Males With First Letter F after created index");
        
        var choose = System.Console.ReadLine();
        string[] chooseParameters = choose.Split(' ');
        int chooseNumber = Convert.ToInt32(choose[0].ToString());
        
        switch (chooseNumber)
        {
            case 1:
                CreateDataBaseMenu();
                break;
            case 2:
                CreatingTableEntryMenu(chooseParameters);
                break;
            case 3:
                GetUniqueUsersMenu();
                break;
            case 432334242:
                FillTheTableWithMillionValuesMenu();
                break;
            case 5:
                GetMalesWithFirstLetterFMenu();
                break;
            case 6:
                GetMalesWithFirstLetterFSecondMenu();
                break;
            default:
                System.Console.WriteLine("Invalid input");
                StartMessage();
                break;
        }
    }

    public async void CreateDataBaseMenu()
    {
        await _service.CreateDataBaseAsync();
        System.Console.WriteLine("Table \"users\" was created successful");
        StartMessage();
    }

    public async void CreatingTableEntryMenu(string[] user)
    {
        try
        {
            User newUser = await _service.CreateUserAsync(user);
            System.Console.WriteLine($"User with FIO \"{newUser.FIO}\" was successfully created");
        }
        catch (InvalidUserParametersException e)
        {
            System.Console.WriteLine(e);
        }
        catch (ArgumentException e)
        {
            System.Console.WriteLine("Data entered incorrectly");
        }
        catch (MySqlException e)
        {
            System.Console.WriteLine(e);
        }
        finally
        {
            StartMessage();
        }
    }

    public async void GetUniqueUsersMenu()
    {
        List<string> users = await _service.GetUniqueUsersAsync();
        int count = 0;
        foreach (var user in users)
        {
            System.Console.WriteLine($"{++count}) {user}");
        }
        StartMessage();
    }

    public async void FillTheTableWithMillionValuesMenu()
    {
        await _service.FillTheTableWithMillionValues();
        StartMessage();
    }
    
    public async void GetMalesWithFirstLetterFMenu()
    {
        TimeSpan timeSpan = await _service.GetMalesWithFirstLetterF();
        System.Console.WriteLine($"Milliseconds is {timeSpan.Milliseconds}");
        _lastRequestExecutionTime = timeSpan.Milliseconds;
        StartMessage();
    }

    public async void CreateIndexMenu()
    {
        await _service.CreateDataBaseIndex();
        System.Console.WriteLine("Ok");
        StartMessage();
    }

    public async void GetMalesWithFirstLetterFSecondMenu()
    {
        TimeSpan timeSpan = await _service.GetMalesWithFirstLetterF();
        System.Console.WriteLine($"new result with indexes: {timeSpan.Milliseconds}, last result without indexes: {_lastRequestExecutionTime}");
        StartMessage();
    }
}