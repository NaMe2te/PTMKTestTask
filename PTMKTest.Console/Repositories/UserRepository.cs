using MySql.Data.MySqlClient;
using PTMKTest.Console.Configurations;
using PTMKTest.Console.Enums;
using PTMKTest.Console.Models;

namespace PTMKTest.Console.Repositories;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(AppConfig appConfig)
    {
        _connectionString = appConfig.GetConnectionString();
    }
    
    public async Task CreateDataBaseAsync()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS users ("
                                      + "user_id INT AUTO_INCREMENT PRIMARY KEY,"
                                      + "fio VARCHAR(100),"
                                      + "birth_day DATE,"
                                      + "gender VARCHAR(20))";
            using (MySqlCommand command = new MySqlCommand(createTableQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task CreateAsync(User user)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string createQuery = "INSERT INTO users(fio, birth_day, gender)"
                                 + $"VALUES ('{user.FIO}', '{user.BerthDay}', '{user.Gender.ToString("G")}')";
            using (MySqlCommand command = new MySqlCommand(createQuery, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<string>> GetUniqueUsersAsync()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            var users = new List<string>();
            await connection.OpenAsync();
            string getQuery = "select distinct u.fio, u.birth_day, u.gender, (YEAR(CURRENT_DATE)-YEAR(u.birth_day))-(RIGHT(CURRENT_DATE,5)<RIGHT(u.birth_day, 5)) as 'age' from users as u"
                + " order by u.fio;";
            using (var command = new MySqlCommand(getQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string str = $"{reader[0]} {reader[1]} {reader[2]} {reader[3]}";
                        users.Add(str);
                    }
                }

                return users;
            }
        }
    }

    public async Task<List<string>> GetMalesWithFirstLetterFAsync()
    {
        
        using (var connection = new MySqlConnection(_connectionString))
        {
            var users = new List<string>();
            await connection.OpenAsync();
            string getQuery = "select u.fio, u.gender from users as u"
                              + " where u.fio like 'F%' and u.gender = 'Male';";
            using (var command = new MySqlCommand(getQuery, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string str = $"{reader[0]} {reader[1]}";
                        users.Add(str);
                    }
                }

                return users;
            }
        }
    }

    public async Task CreateIndexAsync()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string createIndexQuery = "create index fio_index on users (fio);";
            using (var createIndexCommand = new MySqlCommand(createIndexQuery, connection))
            {
                createIndexCommand.ExecuteNonQuery();
            }
        }
    }
}