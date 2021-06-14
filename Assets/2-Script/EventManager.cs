using System.Collections;

using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public static bool GameIsLive;
    private static int _SessionNumber;

    public static int SessionNumber { get => _SessionNumber; set => _SessionNumber = value; }

    private void Awake ( )
    {
        Instance = this;
        GameIsLive = true;
    }
}
