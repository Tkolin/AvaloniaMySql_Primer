using System.Collections.Generic;
using Avalonia.Media;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MySqlConnector;

namespace AvaloniaApplication3.Model;

public static class DataBaseManager
{

    /// Настройки подключения
    public static MySqlConnectionStringBuilder ConnectionString =
            new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "workdb",
                UserID = "root",
                Password = "tkl909"//"nrjkby99"
            };
   
    /// Получение
    public static List<Client> GetClients()
    {
        List<Client> clients = new List<Client>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM clients";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clients.Add(
                            new Client(
                                reader.GetInt32("id"),
                                reader.GetString("firstname"),
                                reader.GetString("lastname"),
                                reader.GetDateTime("dateBirth")));
                    }
                }
            }
            connection.Close();
        }
        return clients;
    }
    public static List<Dish> GetDish()
    {
        List<Dish> dishes = new List<Dish>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM dishes";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dishes.Add(
                            new Dish(
                                reader.GetInt32("id"),
                                reader.GetString("name"),
                                reader.GetFloat("calories"),
                                reader.GetFloat("weight")));
                    }
                }
            }
            connection.Close();
        }
        return dishes;
    }
    public static List<LadingZone> GetLadingZone()
    {
        List<LadingZone> ladingZones = new List<LadingZone>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM ladingZones";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ladingZones.Add(
                            new LadingZone(
                                reader.GetInt32("id"),
                                reader.GetString("name"),
                                reader.GetString("description")));
                    }
                }
            }
            connection.Close();
        }
        return ladingZones;
    }
    public static List<Order> GetOrder()
    {
        List<Order> orders = new List<Order>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM orders";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(
                            new Order(
                                reader.GetInt32("id"),
                                reader.GetInt32("dishe_id"),
                                reader.GetInt32("table_id"),
                                reader.GetInt32("count"),
                                reader.GetDateTime("dateCreate")));
                    }
                }
            }
            connection.Close();
        }
        return orders;
    }
    public static List<Review> GetReview()
    {
        List<Review> reviews = new List<Review>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM reviews";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(
                            new Review(
                                reader.GetInt32("id"),
                                reader.GetInt32("order_id"),
                                reader.GetInt32("client_id"),
                                reader.GetString("comments")));
                    }
                }
            }
            connection.Close();
        }
        return reviews;
    }
    public static List<Table> GetTables()
    {
        List<Table> tables = new List<Table>();

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();

            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "SELECT * FROM tables";

                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                            tables.Add(
                             new Table(
                                 reader.GetInt32("number"),
                                 reader.GetInt32("numberOfSeats"),
                                 reader.GetInt32("landingZone_id")));
                    }
                }
            }
            connection.Close();
        }
        return tables;
    }
    /// Добавление
    public static void AddClients(Client client)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
                {
                    connection.Open();
        
                    using (var comand = connection.CreateCommand())
                    {
                        comand.CommandText = "INSERT INTO clients (firstname, lastname, datebirth) "+
                                             "VALUES (@fName, @lName, @dateBirth);";

                        comand.Parameters.AddWithValue("@fName", client.FirstName);
                        comand.Parameters.AddWithValue("@lName", client.LastName);
                        comand.Parameters.AddWithValue("@dateBirth", client.DateBirth.ToString("dd.MM.yyyy"));

                        var rowsCount = comand.ExecuteNonQuery();

                        if (rowsCount == 0)
                        {
                            MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены",ButtonEnum.Ok).ShowAsync();;
                        }
                    }
                    connection.Close();
                }
    }
    public static void AddOrder(Order order)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO orders (tableid, dishid, count, datecreate) "+
                                     "VALUES (@tableid, @dishid, @count, @datecreate);";

                comand.Parameters.AddWithValue("@tableid", order.Table_id.ToString());
                comand.Parameters.AddWithValue("@dishid", order.Dishe_id.ToString());
                comand.Parameters.AddWithValue("@count", order.Count.ToString());
                comand.Parameters.AddWithValue("@datecreate", order.DateCreate.Date.ToString());
                
                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    public static void AddReviews(Review review)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "INSERT INTO reviews (orderid, clientid, coments) "+
                                     "VALUES (@orderid, @clientid, @coments);";

                comand.Parameters.AddWithValue("@orderid", review.Order_id.ToString());
                comand.Parameters.AddWithValue("@clientid", review.Clietn_id.ToString());
                comand.Parameters.AddWithValue("@coments", review.Comments);

                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не добавлены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    
    /// Удаление
    public static void RemoveClient(Client client)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "DELETE FROM clients " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@id", client.Id.ToString());

                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не удалены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    public static void RemoveOrder(Order order)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "DELETE FROM orders " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@id", order.Id.ToString());

                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не удалены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    public static void RemoveReview(Review review)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "DELETE FROM reviews " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@id", review.Id.ToString());

                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не удалены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    /// Обновление
    public static void UpdateClient(Client client)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "UPDATE clients " +
                                     "SET firstname = @fName, lastname = @lName, datebirth = @dateBirth " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@fName", client.FirstName);
                comand.Parameters.AddWithValue("@lName", client.LastName);
                comand.Parameters.AddWithValue("@dateBirth", client.DateBirth.ToString("dd.MM.yyyy"));
                comand.Parameters.AddWithValue("@id", client.Id.ToString());
                
                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не Обновлены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    public static void UpdateOrder(Order order)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "UPDATE orders "+
                                     "SET table_id = @tableid, dishe_id = @dishid, count = @count, datecreate = @datecreate " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@tableid", order.Table_id.ToString());
                comand.Parameters.AddWithValue("@dishid", order.Dishe_id.ToString());
                comand.Parameters.AddWithValue("@count", order.Count.ToString());
                comand.Parameters.AddWithValue("@datecreate", order.DateCreate.Date.ToString("yyyy-MM-dd"));
                comand.Parameters.AddWithValue("@id", order.Id.ToString());
                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не Обновлены",ButtonEnum.Ok).ShowAsync();;
                }
            }
            connection.Close();
        }
    }
    public static void UpdateReview(Review review)
    {
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
        
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = "UPDATE reviews "+
                                     "SET order_id = @orderid, client_id = @clientid, coments = @coments " +
                                     "WHERE id = @id;";

                comand.Parameters.AddWithValue("@orderid", review.Order_id.ToString());
                comand.Parameters.AddWithValue("@clientid", review.Clietn_id.ToString());
                comand.Parameters.AddWithValue("@coments", review.Comments);
                comand.Parameters.AddWithValue("@id", review.Id.ToString());
                var rowsCount = comand.ExecuteNonQuery();

                if (rowsCount == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Ошибка", "Данные не Обновлены",ButtonEnum.Ok).ShowAsync();
                }
            }
            connection.Close();
        }
    }
}