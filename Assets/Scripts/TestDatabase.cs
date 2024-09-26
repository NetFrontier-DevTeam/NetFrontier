using System;
using System.IO;
using MySql.Data.MySqlClient;
using UnityEngine;

public class TestDatabase : MonoBehaviour
{
    public DatabaseManager databaseManager;

    void Start()
    {
        databaseManager = FindObjectOfType<DatabaseManager>();

        // Test user registration
        databaseManager.RegisterUser("testUser", "testPassword");

        // Test user login
        bool loginSuccess = databaseManager.LoginUser("testUser", "testPassword");
        Debug.Log("Login successful: " + loginSuccess);
    }
}
