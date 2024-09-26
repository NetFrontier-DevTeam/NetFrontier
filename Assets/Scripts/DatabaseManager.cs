using System;
using System.IO;
using MySql.Data.MySqlClient;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private MySqlConnection connection;

    // Database configuration structure
    [System.Serializable]
    public class DatabaseSettings
    {
        public string Server;
        public string DatabaseName; // Ensure this matches your JSON structure
        public string UserId;
        public string Password;
    }

    [System.Serializable]
    public class DatabaseConfig
    {
        public DatabaseSettings Database;
    }

    void Start()
    {
        try
        {
            LoadDatabaseConfig(); // Load configuration on start
            if (connection != null)
            {
                OpenConnection();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    Debug.Log("Database connection successful!");
                }
                else
                {
                    Debug.LogError("Database connection failed to open.");
                }
            }
            else
            {
                Debug.LogError("Database connection is not initialized.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Database connection failed: " + ex.Message);
        }
    }

    private void LoadDatabaseConfig()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "config.json");

        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            DatabaseConfig config = JsonUtility.FromJson<DatabaseConfig>(jsonString);
            // Build the connection string from config
            string connectionString = $"Server={config.Database.Server};Database={config.Database.DatabaseName};User ID={config.Database.UserId};Password={config.Database.Password};";
            connection = new MySqlConnection(connectionString);
        }
        else
        {
            Debug.LogError("Config file not found at: " + path);
        }
    }

    private void OpenConnection()
    {
        if (connection == null)
        {
            Debug.LogError("Connection object is null.");
            return;
        }

        try
        {
            connection.Open();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Failed to open database connection: " + ex.Message);
        }
    }

    void OnApplicationQuit()
    {
        if (connection != null)
        {
            connection.Close();
            Debug.Log("Database connection closed.");
        }
    }

    // Example: Method for user registration
    public void RegisterUser(string username, string password)
    {
        if (connection == null || connection.State != System.Data.ConnectionState.Open)
        {
            Debug.LogError("Database connection is not open. Cannot register user.");
            return;
        }

        string query = "INSERT INTO users (username, password) VALUES (@username, @password)";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            Debug.Log("User registered: " + username);
        }
    }

    // Example: Method for user login
    public bool LoginUser(string username, string password)
    {
        if (connection == null || connection.State != System.Data.ConnectionState.Open)
        {
            Debug.LogError("Database connection is not open. Cannot login.");
            return false;
        }

        string query = "SELECT COUNT(*) FROM users WHERE username=@username AND password=@password";

        using (MySqlCommand cmd = new MySqlCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }
}
