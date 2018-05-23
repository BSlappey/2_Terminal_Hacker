using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game configuration data
    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "money", "government", "banks", "treasury", "important" };

    //Game state
    string greeting = "Greetings criminal.";
    string password;
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Use this for initialization
    void Start ()
    {
        ShowMainMenu(greeting);
	}

    void ShowMainMenu(string greeting)
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine(greeting);
        Terminal.WriteLine("Which terminal would you like to hack  into?\n");
        Terminal.WriteLine("1. Library");
        Terminal.WriteLine("2. Police");
        Terminal.WriteLine("3. Fort Knox\n");
        Terminal.WriteLine("Enter your selection: ");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu(greeting);
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            AskForPassword();
        }
        else if (input == "007")
        {
            Terminal.WriteLine("Please select a level Mr Bond.");
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    void AskForPassword()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SetRandomPassword();
        Terminal.WriteLine("Type 'menu' to return to the main menu.");
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }

    private void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                break;
        }
    }

    void CheckPassword(string input)
    {
        if (input == password)
        {
            Terminal.WriteLine("Congratulations!");
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine("Type 'menu' to return to the main menu.");
    }

    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /______//
(______(/
");
                break;
            case 2:
                Terminal.WriteLine("You're free to go...");
                Terminal.WriteLine(@"
    ____
   /    \
  /  /\  \________________
 /  <  >                  \
 \   \/   _____   ___   __/
  \      /     |_|   |_|
   \____/
");
                break;
            case 3:
                Terminal.WriteLine("You're rich!");
                Terminal.WriteLine(@"
$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
");
                break;
            default:
                Debug.LogError("Invalid level reached");
                break;
        }
    }
}
