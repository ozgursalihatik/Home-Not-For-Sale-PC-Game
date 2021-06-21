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
                GameManager.Instance.DedeRotationChange(0);
            }
            else if ( value == 1 )
            {
                GameManager.Instance.Session2Trigger.SetActive(false);
                GameManager.Instance.garageTrigger.SetActive(true);
            }
            else if ( value == 2 )
            {
                GameManager.Instance.DedeRotationChange(1);
                GameManager.Instance.HalaPositionChange(0);
                GameManager.Instance.Session2Trigger.SetActive(true);
                GameManager.Instance.garageTrigger.SetActive(false);
            }
            else if ( value == 3 )
            {
                GameManager.Instance.Session2Trigger.SetActive(false);
            }
            else if ( value == 4 )
            {
                UIManager.Instance.RequiredPlanks = GameManager.Instance.Planks1.transform.childCount;
                GameManager.Instance.HalaPositionChange(1);
                GameManager.Instance.GarajKapisiAcilsin( );
            }
            else if ( value == 5 )
            {
                UIManager.Instance.RequiredPlanks = GameManager.Instance.Planks2.transform.childCount;
                UIManager.Instance.GottenPlanks = 0;
                UIManager.Instance.GottenWoods = 0;
            }
            else if ( value == 6 )
            {
                GameManager.Instance.garageTrigger.SetActive(false);
                GameManager.Instance.Session2Trigger.SetActive(false);
                GameManager.Instance.baltaTrigger.SetActive(false);
                UIManager.Instance.RequiredTrashes = GameManager.Instance.Trashes.transform.childCount;
                UIManager.Instance.RequiredEggs = GameManager.Instance.Eggs.transform.childCount;
                UIManager.Instance.GottenEggs = 0;
                UIManager.Instance.GottenTrashes = 0;
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
