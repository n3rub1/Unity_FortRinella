using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReputationStatic
{
    private static int reputation = 0;
    private static bool isEntranceGameReady = false;
    private static bool ishiddenItemsGameReady = false;
    private static bool issemaphoreGameReady = false;
    private static bool isengineGameReady = false;
    private static bool ismixingGameReady = false;
    private static bool isbattleshipGameReady = false;


    public static int GetReputation()
    {
        return reputation;
    }

    public static void SetReputationForEntranceGame(int reputationPoints)
    {
        if (!isEntranceGameReady)
        {
            reputation = reputation + reputationPoints;
            Debug.Log(GetReputation());
        }
    }

    public static void SetReputationForHiddenItemsGame(int reputationPoints)
    {
        if (!ishiddenItemsGameReady)
        {
            reputation = reputation + reputationPoints;
        }
    }

    public static void SetReputationForSemaphoreGame(int reputationPoints)
    {
        if (!issemaphoreGameReady)
        {
            reputation = reputation + reputationPoints;
        }
    }

    public static void SetReputationForEngineGame(int reputationPoints)
    {
        if (!isengineGameReady)
        {
            reputation = reputation + reputationPoints;
        }
    }

    public static void SetReputationForMixingGame(int reputationPoints)
    {
        if (!ismixingGameReady)
        {
            reputation = reputation + reputationPoints;
        }
    }

    public static void SetReputationForBattleshipGame(int reputationPoints)
    {
        if (!isbattleshipGameReady)
        {
            reputation = reputation + reputationPoints;
        }
    }

    public static void SetEntranceGameToTrue()
    {
        isEntranceGameReady = true;
    }

    public static void SetHiddenItemsGameToTrue()
    {
        ishiddenItemsGameReady = true;
    }

    public static void SetSemaphoreGameToTrue()
    {
        issemaphoreGameReady = true;
    }

    public static void SetEngineGameToTrue()
    {
        isengineGameReady = true;
    }

    public static void SetMixingGameToTrue()
    {
        ismixingGameReady = true;
    }

    public static void SetBattleshipGameToTrue()
    {
        isbattleshipGameReady = true;
    }

}
