using System.Collections;

using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

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

    private void Start ( )
    {

    }
}
public enum GameSessions
{
    First = 0,
    Grandpa = 1,
    Coop = 2,
    Garage = 3,
    End = 4
}