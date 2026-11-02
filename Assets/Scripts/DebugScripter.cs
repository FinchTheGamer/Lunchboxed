using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic; //needed for coroutines I believe
public class DebugScripter : MonoBehaviour
{
    private const string THEPROMISE = "I promise that this project contains no AI generated code whatsoever. AI slop is the scourge of the internet, and it is up to humanity to fight back";
    //^ necessary for proving that I AM A LEGEND
    GameObject DEBUG;
    WorkingStateOfCode workingStateOfCode; 
    private int hoursWithoutSleep = 0;
    public bool shouldActivateRandomGoodMessageGenerator = false;

    [SerializeField] private Text hoursButtonText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        DEBUG = GameObject.FindGameObjectWithTag("TEMP");
        workingStateOfCode = WorkingStateOfCode.Warnings;
        DEBUG.GetComponent<Text>().text = DevNotes + "\n-State Of Code: " + workingStateOfCode;
        Debug.Log("Debugger says: " + AssembleRandomKeyPhrase("", randomKeyWordList.Length));
        StartCoroutine(PasswordSolver());
        StartCoroutine(GenerateBonusGoodMessages());
    } //what's going on?
    
    //bruh

    //private string[] ListOfCallableCommands = {"/setPlayerHealth", };
    //should this be like... a struct?
    private string DevNotes = //can be added to like a notepad!
        "Fix EVERYTHING";
    public enum WorkingStateOfCode //this is an enum! Kinda fun; they are sort of like custom value types that have specific answers to them
        // I'm so worried I won't pass. My assets aren't... great
    {
        Slop,
        SpaghettiCode,
        Errors,
        Warnings,
        Compiles,
        Works,
        Polished,
    } //enums are really just lists under the hood, I believe
    private string[] randomKeyWordList = //this is a list of inspirations. I think I'll need it. My motivation can be none existant sometimes
    { 
        "Cool",
        "Strong",
        "Wise",
        "Indepedent",
        "Strong",
        "Powerful",
        "Efficient",
        "Hard-working",
        "Passionate",
        "Determined",
        "Resilient",
        "Mango",
        "Hopeful",
        "Intelligent",
        "Smart",
        "Brave",
        "Wise",
        "Alive",
        "Proud",
        "Happy",
        "Clever",
        "Cunning",
        "Energetic",
        "Kind",
        "Immortal",
        "Legendary"
        
        
    //should we grab a random starter too
    };//would this be a good enum? I don't think so. It needs to print.
    private string[] letters = //why is it always the vowels?
    {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", //English lowercase
        "A", "B", "C", "D", "D", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", //English uppercase
        "ä", "ö ", "ü", "ß", "Ä", "Ö", "Ü", //that'll be Deutsch..
        "ñ", "á", "é", "í", "ó", "ú", "Ñ", "Á", "É", "Í", "Ó", "Ú", //that'll be Español..
        "à", "è", 
    }; //remember that arrays, like string[], and lists, like List<string>, are not the same in C#
    private string AssembleRandomKeyPhrase(string starterSeed = "", int amountOfTimesToLoop = 5) //so my idea for this is that this could be useful for... say... on the fly keys!
    {
        string tempAssembler = starterSeed;
        for (int i = 0; i < amountOfTimesToLoop-1; i++)
        {
            tempAssembler += randomKeyWordList[Random.Range(0, randomKeyWordList.Length)]; //first value is inclusive, last is exclusive //it tried to call one out of range
            tempAssembler += "-";
        }
        tempAssembler += randomKeyWordList[Random.Range(0, randomKeyWordList.Length)];
        return tempAssembler;
    }
    private string AssembleRandomKeyWord(string starterSeed = "", int amountOfTimesToLoop = 5) //so my idea for this is that this could be useful for... say... on the fly keys!
    {
        string tempAssembler = starterSeed;
        for (int i = 0; i < amountOfTimesToLoop - 1; i++)
        {
            tempAssembler += letters[Random.Range(0, letters.Length)]; //first value is inclusive, last is exclusive //it tried to call one out of range
            
        }
        tempAssembler += letters[Random.Range(0, letters.Length)];
        return tempAssembler;
    }
    public void DisplayWord()
    {
        Debug.Log(AssembleRandomKeyWord());
    }
    private IEnumerator PasswordSolver() //it is going  to try to solve its own password //i have this idea for a door where you have to collect clues for the password
    {
        string passwordToSolve = AssembleRandomKeyPhrase();
        string guess = "";
        bool hasntBeenSolved = true;
        //begin assembling new strings

        while(hasntBeenSolved)
        {
            guess = AssembleRandomKeyPhrase();
            Debug.Log("Guess: " + guess + " Password: " + passwordToSolve); // this prints the guess and then the password
            if (guess == passwordToSolve)
            {
                hasntBeenSolved = false;
                Debug.Log("I solved it!");
                break;

            }
            yield return null; //so this ensures we don't get suck in a loop
        }
        
    }
    //this is essentially a sort of NotePad!
    //I guess its kinda weird to make an entire notepad
    //at least it exists now?
    //this takes forever
    //I think the above code is kind of expensive

    public void UpdateHoursWithoutSleep()
    {
        hoursWithoutSleep++;
        if(Random.Range(0,2) == 0)
         {
            hoursButtonText.text = "Hours without sleep: " + hoursWithoutSleep;
        }
        else
        {
            hoursButtonText.text = (inspirationalMessages[Random.Range(0, inspirationalMessages.Count)]);
        }

    }
    private List<string> inspirationalMessages = new List<string>//oh
    {
        "You got this!",
        "I believe in you!",
        "Remember who you are!",
        "If the Turks can win against 5 colonizers, YOU can do another hour",
        "Keep pushing! For the laptop!",
        "Think of all the people who said you couldn't do it!",
        "You are the singularity! Complete your dream!",
        "Just one more hour!",
        "You are stronger than the bug!",
        "Swat those bugs!",
        "Watch your memory!",
        "If quitting is your power, what are you without it?",
        "What are you made of?",
        "Are you tired of the task or tired of being a loser",
        "Work for money!",
        "Work for pride!",
        "Work to live a happy life!",
        "Work to be a \"main character\"!",
        "Work to not be caught in a dangerous accident!",
        "Work to someday find out the meaning of happiness!",
        "Work to someday find out the meaning of love!",
        "Work to someday find out the meaning of life!",
        "Work to someday find out the meaning of death!",
        "Work to someday find out the meaning of the before",
        "Work to someday find out the meaning of the beyond!",
        "What's out there? It is up to you to find it.",
        "The Soviet Union could reunite! A new Cold War could break! You can't miss that!",
        "Remember, be happy, because being sad doesn't make you happy.",
        "Remember, take showers.",
        "Remember, take breaks.",
        "Remember, don't be stupid!",
        "Remember, don't succumb to Dunning-Kruger!",
        "Remember, withdraw when wise, but never surrender the war effort",
        "Remember, eating is pretty useful sometimes",
        "Remember, don't eat too much"
    };
    private IEnumerator GenerateBonusGoodMessages() //I don't remember the rest of the words to the song
    {
        int iterations = 0;
        while(iterations < 10)
        {
            inspirationalMessages.Add(AssembleRandomKeyPhrase());
            iterations++;
            yield return null;
        }
        
    }

    //I'm so tired... so so tired... I can't keep doing this
    
    //let's have itt try to make an inspirational message too;
}
