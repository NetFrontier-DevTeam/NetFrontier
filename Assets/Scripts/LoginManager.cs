using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public DatabaseManager databaseManager; // Reference to your DatabaseManager
    public InputField usernameInput; // Reference to the Username input field
    public InputField passwordInput; // Reference to the Password input field
    public Text feedbackText; // Reference to the feedback text
    public Button logoutButton; // Reference to the logout button

    private void Start()
    {
        feedbackText.text = ""; // Clear feedback text on start
        logoutButton.gameObject.SetActive(false); // Hide logout button on start
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (databaseManager.LoginUser(username, password))
        {
            feedbackText.text = "Login successful!";
            logoutButton.gameObject.SetActive(true); // Show logout button upon successful login
        }
        else
        {
            feedbackText.text = "Login failed! Please check your username and password.";
        }
    }

    public void OnLogoutButtonClicked()
    {
        // Optionally, log the user out in the database
        string username = usernameInput.text;
        // databaseManager.LogUserLogout(username); // Uncomment if you want to log out in the database

        feedbackText.text = "Logged out successfully!";
        logoutButton.gameObject.SetActive(false); // Hide logout button after logout

        // Close the application
        Application.Quit();

        // If running in the editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
