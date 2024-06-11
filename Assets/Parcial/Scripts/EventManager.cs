using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void StartGameDelegate();
    public static StartGameDelegate onStartGame;

    public static StartGameDelegate onPlayerDeath;

    public delegate void TakeDamageDelegate(float amt);
    public static TakeDamageDelegate onTakeDamage;

    public delegate void PauseGameDelegate();
    public static PauseGameDelegate onPauseGame;

    public delegate void BoostChangeDelegate(float amt);
    public static BoostChangeDelegate onBoostChange;

    public delegate void EndGameDelegate();
    public static EndGameDelegate onEndGame;

    public static void TakeDamage(float percent)
    {
        Debug.Log("Damage: " + percent);
        onTakeDamage?.Invoke(percent);
    }

    public static void PauseGame()
    {
        Debug.Log("Game Paused");
        onPauseGame?.Invoke();
    }

    public static void PlayerDeath()
    {
        Debug.Log("Player Died");
        onPlayerDeath?.Invoke();
        EndGame();
    }

    public static void BoostChange(float percent)
    {
        Debug.Log("Boost: " + percent);
        onBoostChange?.Invoke(percent);
    }

    public static void EndGame()
    {
        Debug.Log("Game Ended");
        onEndGame?.Invoke();
    }
}
