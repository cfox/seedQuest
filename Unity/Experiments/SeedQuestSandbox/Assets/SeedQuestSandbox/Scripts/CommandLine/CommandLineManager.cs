﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLineManager
{

    // Initialize the dictionary 
    public static Dictionary<string, Func<string, string>> commands =
        new Dictionary<string, Func<string, string>>
    {
        {"help", help},
        {"print", print},
        {"gamestate", setGameState}
    };

    // Dictionary containing information on various topics for the user
    public static Dictionary<string, string> helpInformation = new Dictionary<string, string>
    {
        {"seedquest", "SeedQuest is a mnemonic application to help a user recover a private key"},
        {"rehearse", "Rehearsal mode is a practice mode to help a user remember the mnemonic path to recover their private key. Also called Learn mode"},
        {"learn", "Learn mode is a practice mode to help a user remember the mnemonic path to recover their private key. Also called rehearsal mode"},
        {"recall", "Recall mode is the mode used to recover a private key. Also called Recover mode"},
        {"recover", "Recover mode is the mode used to recover a private key. Also called Recall mode"},

    };

    // Displays some information to the user. If parameter string isn't found in helpInformation,
    //  then prints out a list of available commands
    public static string help(string input)
    {
        if (helpInformation.ContainsKey(input))
            return helpInformation[input];
        else
        {
            privatePrint("Available command line commands: ");
            foreach (string key in commands.Keys)
                privatePrint(key);
        }
        return "";
    }

    // Just used for displaying information to the user
    public static string print(string input)
    {
        return input;
    }

    // This prints to the debug log for now, but should display text to the front end terminal eventually
    private static void privatePrint(string input)
    {
        Debug.Log(input);
    }

    // Set the gamestate. string.StartsWith() is used so that the user input doesn't need to be
    //  perfectly correct to set some states (ex: 'rehears' will work with either 'rehearsal' 
    //  or 'rehearse' as the user input.
    public static string setGameState(string input)
    {
        if (input.StartsWith("recal") || input.StartsWith("recover"))
        {
            //GameManager.State = GameState.Recall;
            return "Game state set to Recall.";
        }

        if (input.StartsWith("rehears") || input.StartsWith("learn"))
        {
            GameManager.State = GameState.Rehearsal;
            return "Game state set to Rehearsal.";
        }

        if (input.StartsWith("prev"))
        {
            GameManager.State = GameManager.PrevState;
            return "Game state set to previous state.";
        }

        if (input.StartsWith("gamest") || input.StartsWith("start"))
        {
            //GameManager.State = GameState.GameStart;
            return "Game state set to Recall.";
        }

        if (input.StartsWith("gameend") || input.StartsWith("end"))
        {
            //GameManager.State = GameState.GameEnd;
            return "Game state set to GameEnd.";
        }

        if (input.StartsWith("pause"))
        {
            GameManager.State = GameState.Pause;
            return "Game state set to Pause.";
        }

        if (input.StartsWith("inter"))
        {
            GameManager.State = GameState.Interact;
            return "Game state set to Interact.";
        }

        if (input.StartsWith("loadingrecall"))
        {
            //GameManager.State = GameState.LoadingRecall;
            return "Game state set to Loading Recall.";
        }

        if (input.StartsWith("loadingrehearse"))
        {
            //GameManager.State = GameState.LoadingRehersal;
            return "Game state set to Loading Rehearsal.";
        }

        return "Game state by name of '" + input + "' not found.";
    }

}
