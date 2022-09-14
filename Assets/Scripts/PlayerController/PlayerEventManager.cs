using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlayerEventManager
{
    public static event Action onGameStarted;
    public static event Action onGameStopped;

    public static event Action onPlayerStartCollect;
    public static event Action onPlayerStopCollect;

    public static void FireOn_StartCollect()
    {
        onPlayerStartCollect?.Invoke();
    }
    public static void FireOn_StopCollect()
    {
        onPlayerStopCollect?.Invoke();
    }
    public static void FireOn_GameStarted()
    {
        onGameStarted?.Invoke();
    }
    public static void FireOn_GameStopped()
    {
        onGameStopped?.Invoke();
    }
}
