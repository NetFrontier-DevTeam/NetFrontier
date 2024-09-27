using System;
using System.IO;
using System.Net.NetworkInformation; // Using System's Ping
using MySql.Data.MySqlClient;
using UnityEngine;
using UnityEngine.UI;

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

    public Text loginMessageText; // Text component for displaying login message
    public Text pingMessageText; // Text component for displaying ping messages

    void Start()
    {
        try
        {
            LoadDatabaseConfig(); // Load configuration on start
            OpenConnection();
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                Debug.Log("Database connection successful!");
                StartPing("127.0.0.1"); // Replace with your server IP
            }
            else
            {
                Debug.LogError("Database connection failed to open.");
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

    // Method for user registration
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

    // Method for user login
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

            if (count > 0)
            {
                Debug.Log("User logged in successfully: " + username);
                PrintLoginMessage(username); // Print user login message
                return true;
            }
            else
            {
                Debug.LogWarning("Login failed: Incorrect username or password.");
                return false;
            }
        }
    }

    // Method to display the login message
    private void PrintLoginMessage(string username)
    {
        if (loginMessageText != null)
        {
            loginMessageText.text = "Logged in as: " + username; // Display the login message
        }
        else
        {
            Debug.LogError("LoginMessageText is not assigned.");
        }
    }

    private void StartPing(string ipAddress)
    {
        StartCoroutine(PingServer(ipAddress));
    }

    private System.Collections.IEnumerator PingServer(string ipAddress)
    {
        while (true)
        {
            using (System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping())
            {
                PingReply reply = ping.Send(ipAddress);
                string pingMessage = $"Ping to {ipAddress}: bytes=32 time={reply.RoundtripTime}ms TTL=128"; // Format ping message
                UpdatePingDisplay(pingMessage); // Update the ping message display
            }

            yield return new WaitForSeconds(1); // Adjust the interval as needed
        }
    }

    private void UpdatePingDisplay(string message)
    {
        if (pingMessageText != null)
        {
            pingMessageText.text += message + "\n"; // Append ping message with newline
        }
        else
        {
            Debug.LogError("PingMessageText is not assigned.");
        }
    }
}
