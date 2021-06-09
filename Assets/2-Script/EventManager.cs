using System.Collections;

using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public static bool GameIsLive;
    public static GameSessions CurrentSession
    {
        get
        {
            return Instance._currentSession;
        }
        set
        {
            Instance._currentSession = value;
        }
    }
    public GameSessions _currentSession;
    private void Awake ( )
    {
        Instance = this;
        GameIsLive = true;
    }
}
