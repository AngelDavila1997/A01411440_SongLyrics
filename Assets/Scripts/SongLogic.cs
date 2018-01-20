using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLogic : MonoBehaviour {
    //Variables and constants that will help us play our game
    //menuHint will tell the user that they can type menu whenever they want

    const string menuHint = "You can type menu at any time.";

    //Class attributes
    //These arrays will hold the song lyrics and the name of each song in order 
    //To look for them in the position they are in

    string[] level1Lyrics = {
    "She's just a girl who claims that I am the one. But the kid is not my son",
    "Since you've gone I been lost without a trace. I dream at night I can only see your face",
    "Today's another day to find you. Shying away. I'll be coming for your love, okay?",
    "You better run, you better do what you can. Don't want to see no blood, don't be a macho man",
    "I made it through the wilderness. Somehow I made it through. Didn't know how lost I was until I found you",
    "Rising up, back on the street. Did my time, took my chances"
    };
    string[] level1Songs = {"Billie Jean", "Every Breath You Take", "Take On Me", "Beat It", "Like A Virgin", "Eye Of The Tiger"};

    string[] level2Lyrics = {
    "With the lights out, it's less dangerous. Here we are now, entertain us",
    "If I should stay. I would only be in your way",
    "My loneliness is killing me (and I) I must confess, I still believe (still believe)",
    "Yo, I'll tell you what I want, what I really, really want",
    "You float like a feather. In a beautiful world",
    "Backbeat, the word was on the street. That the fire in your heart is out"
    };
    string[] level2Songs = { "Smells Like Teen Spirit", "I Will Always Love You", "Baby One More Time", "Wannabe", "Creep", "Wonderwall"};

    string[] level3Lyrics = {
    "When you leave, I'm begging you not to go. Call your name two or three times in a row",
    "When the sun shines, we shine together. Told you I'll be here forever",
    "That tonight's gonna be a good night. That tonight's gonna be a good, good night",
    "Fold 'em, let 'em, hit me, raise it baby stay with me (I love it)",
    "I fly like paper, get high like planes. If you catch me at the border I got visas in my name",
    "All the commotion. The kiddie like play"
    };
    string[] level3Songs = { "Crazy In Love", "Umbrella", "I Gotta Feeling", "Poker Face", "Paper Planes", "Sex On Fire" };


    int level; //To know which level the user wants to join
    string answer; //Stores the answer to compare it with the input
    bool play = false; //This variable is for not letting the player to get another lyric until he goes back to the main menu

    //Here I declare an enumerated type to represent the different game states
    //and I declare a variable to hold the current game state
    enum GameState { MainMenu, Password, Win }; //Refactor
    GameState currentScreen = GameState.MainMenu;

    string lyric; //Will hold the lyric of the song to be guessed

    // Use this for initialization 
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ShowMainMenu()
    {
        //Only works with WM2000
        //We clear the screen
        Terminal.ClearScreen();

        //We show the menu
        Terminal.WriteLine("Choose a music era:");
        Terminal.WriteLine("");
        Terminal.WriteLine("1. 80's Songs");
        Terminal.WriteLine("2. 90's Songs");
        Terminal.WriteLine("3. 00's Songs");
        Terminal.WriteLine("");
        Terminal.WriteLine("To exit write 'close', 'quit' or");
        Terminal.WriteLine("'exit' at any time.");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection.");
        
        //We set our current screen state as the main menu
        currentScreen = GameState.MainMenu;
    }

    void OnUserInput(string input)
    {
        if (currentScreen == GameState.Win)
        {
            //If user inputs the "menu" keyword, then we call the ShowMainMenu() method once more
            if (input == "menu")
            {
                ShowMainMenu();
            }
        }
        else if (input == "menu")
        {
            ShowMainMenu();
        }
        //However if the user types quit, close or exit, then we try to close the game. If the game is played on a Web browser, then 
        //we ask the user to close the tab
        else if (input == "quit" || input == "close" || input == "exit")
        {
            Terminal.WriteLine("Please, close the browser's tab");
            //To end a game
            Application.Quit();
        }
        //If the user inputs anything that is not menu, quit, close or exit, then we are going to handle that input depending on the game state
        //If the game state is still MainMenu, then we call the RunMainMenu()
        else if (currentScreen == GameState.MainMenu)
        {
            RunMainMenu(input);
        }
        //But if the current state is password, then we call the CheckPassword() Method
        else if (currentScreen == GameState.Password)
        {
            CheckLyric(input);
        }
    }

    void CheckLyric(string input)
    {
        if (input == answer)//Here it checks if the lyric matches the song answered by the player
        {
            DisplayWinScreen();
        }
        else
        {
            AskForSong();
        }
    }

    void DisplayWinScreen()
    {
        play = false; //Play becommes false to play again
        Terminal.ClearScreen(); //Clears the screen
        Terminal.WriteLine(menuHint); //And tells him that he can go back at the menu
        Terminal.WriteLine("");
        currentScreen = GameState.Win;
        ShowLevelReward(); //Shows the reward
    }

    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Have a centipede..");
                Terminal.WriteLine(@"
....\...../
.....╚⊙ ⊙╝
..╚═(███)═╝
.╚═(███)═╝
╚═(███)═╝
╚═(███)═╝
.╚═(███)═╝");
                break;
            case 2:
                Terminal.WriteLine("Have a pentagram..");
                Terminal.WriteLine(@"
 ___|)_________|___________
|___/___|______|____
|__/|___|-.__,-.________
|_/(|,\_|/___`-'______
|_\_|_/__________
    | 
  (_|                      ");
                break;
            case 3:
                Terminal.WriteLine("Have a piano..");
                Terminal.WriteLine(@"
  o    _______________
 /\_  _|             |
_\__`[_______________|  
] [ \, ][         ][         
");
                break;
            default:
                Debug.LogError("Invalid level reached.");
                break;
        }
    }

    void RunMainMenu(string input)
    {
        //We first check that the input is a valid input
        bool isValidInput = (input == "1") || (input == "2") || (input == "3");

        // If the user iputs a valid level, we convert that input to an int value and then we call the AskForPassword() methos
        if (isValidInput)
        {
            level = int.Parse(input); //Change value from string to int
            AskForSong();
        }

        //However if the user did not enter a valid input, then we validate for our Easter Egg
        else if (input == "KPOP")
        {
            Terminal.WriteLine("유효한 수준 입력");
        }
        else
        {
            Terminal.WriteLine("Enter a valid level.");
        }
    }

    void AskForSong()
    {
        //We set our current game state as Password
        currentScreen = GameState.Password;

        //We clear our terminal screen
        Terminal.ClearScreen();

        //We call the SetRandomPassword() method
        if (!play)
        {
            SetRandomLyric();
            play = true;
        }

        Terminal.WriteLine("Enter your guess. Hint: \n " + lyric);
        Terminal.WriteLine("");
        Terminal.WriteLine("Remember that the first letter of each word should be UPPERCASE.");
        Terminal.WriteLine(menuHint);
        Terminal.WriteLine("");

    }

    void SetRandomLyric()
    {
        switch (level)
        {
            case 1:
                lyric = level1Lyrics[UnityEngine.Random.Range(0, level1Lyrics.Length - 1)];
                answer = level1Songs[SearchPosition(lyric, level1Lyrics)];
                break;
            case 2:
                lyric = level2Lyrics[UnityEngine.Random.Range(0, level2Lyrics.Length - 1)];
                answer = level2Songs[SearchPosition(lyric, level2Lyrics)];
                break;
            case 3:
                lyric = level3Lyrics[UnityEngine.Random.Range(0, level3Lyrics.Length - 1)];
                answer = level3Songs[SearchPosition(lyric, level3Lyrics)];
                break;
            default:
                Debug.LogError("Invalid level. How did you manage that?");
                break;
        }
    }

    private int SearchPosition(string song, string[] levelLyrics)
    {
        for (int i = 0; i < levelLyrics.Length; i++)
        { //Start a for cycle
            if (levelLyrics[i] == song)
            {//If the value inside the position is equal to the value sent
                return i;//It will return the position
            }
        }
        Terminal.WriteLine("Error.");
        return -1;//If not it will return -1 (position that does not exist)
    }
}
