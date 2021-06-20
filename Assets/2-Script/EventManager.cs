using System.Collections;

using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public static bool GameIsLive;
    private static int _SessionNumber;

    public static int SessionNumber
    {
        get => _SessionNumber;
        set
        {
            if ( value == 0 )
            {
            }
            else if ( value == 1 )
            {
                GameManager.Instance.DedeRotationChange(value);
                GameManager.Instance.HalaPositionChange(0);
            }
            else if ( value == 2 )
            {
            }
            else if ( value == 3 )
            {
                GameManager.Instance.GarajKapisiAcilsin( );
                GameManager.Instance.HalaPositionChange(1);
            }
            else if ( value == 4 )
            {
                GameManager.Instance.BahceKapisiAcilsin( );
            }
            else if ( value == 5 )
            {
                GameManager.Instance.KumesKapisiAcilsin( );
            }
            _SessionNumber = value;
        }
    }

    private void Awake( )
    {
        Instance = this;
        GameIsLive = true;
    }
}
