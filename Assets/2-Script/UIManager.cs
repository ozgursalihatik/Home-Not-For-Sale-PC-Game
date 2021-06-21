using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Settings, InventoryEnv, QuestInfo;
    public GameObject InvSlot1, InvSlot2, InvSlot3, InvSlot4;
    public TMP_Text InvSlot1Counter, InvSlot2Counter, InvSlot3Counter, InvSlot4Counter,
        QuestInfoText;
    public TMP_Text InfoText;
    public int RequiredWoods, RequiredPlanks, RequiredEggs, RequiredTrashes;
    public int GottenWoods, GottenPlanks, GottenEggs, GottenTrashes;

    private CursorLockMode tempLock;
    private bool tempLive;
    private bool tempMove;
    private bool infotext;
    private GameObject tempObject;

    public static void SendMessageBox( string message )
    {
        Instance.InfoText.gameObject.SetActive(true);
        Instance.infotext = true;
        Instance.InfoText.text = message;
        Instance.StartCoroutine(Instance.infoDelay(1));
    }
    public static bool WoodsOK( )
    {
        return Instance.RequiredWoods == Instance.GottenWoods;
    }
    public static bool PlanksOK( )
    {
        return Instance.RequiredWoods == Instance.GottenPlanks;
    }
    public static bool EggsOK( )
    {
        return Instance.RequiredEggs == Instance.GottenEggs;
    }
    public static bool TrashesOK( )
    {
        return Instance.RequiredTrashes == Instance.GottenTrashes;
    }
    /// <summary>
    /// Sets active or deactive Invetory Slots in UIManager.
    /// </summary>
    /// <param name="Inventories">In it, wood, plank and eggs values are searched dictionary variable. </param>
    public static void SetInventory( Dictionary<string, int> Inventories )
    {
        foreach ( var item in Inventories.Keys )
        {
            if ( item.Equals("Wood") )
            {
                Instance.SetSlot1(Inventories[item]);
            }
            else if ( item.Equals("Plank") )
            {
                Instance.SetSlot2(Inventories[item]);
            }
            else if ( item.Equals("Eggs") )
            {
                Instance.SetSlot3(Inventories[item]);
            }
            else if ( item.Equals("Trashes") )
            {
                Instance.SetSlot4(Inventories[item]);
            }
        }
    }
    public static void ShowObjectInfo( EqqupableObject eqObject )
    {
        if ( PlayerMovement.Instance.canMove )
        {
            Instance.InfoText.gameObject.SetActive(true);
            switch ( eqObject.OnjectsType )
            {
                case EqObjectType.OdunKesmeNoktasi:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Baltay� b�rakmak i�in 'E'";
                    return;
                case EqObjectType.Odun:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Odunu almak i�in 'E'";
                    return;
                case EqObjectType.Kereste:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Keresteyi almak i�in 'E'";
                    return;
                case EqObjectType.Yumurta:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Yumurtay� almak i�in 'E'";
                    return;
                case EqObjectType.Cop:
                    Instance.infotext = true;
                    Instance.InfoText.text = "��p� yerden kald�rmak i�in 'E'";
                    return;
                case EqObjectType.TamirCantasi:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Tamir �antas�n� almak i�in 'E'";
                    return;
                case EqObjectType.GarajCop:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Da��n�kl��� temizlemek i�in 'E'";
                    return;
                case EqObjectType.BahceKapisi:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Toplad���n e�yalar� b�rakmak i�in 'E'";
                    return;
                case EqObjectType.KumesKapisi:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Toplad���n e�yalar� b�rakmak i�in 'E'";
                    return;
                case EqObjectType.Balta:
                    Instance.infotext = true;
                    Instance.InfoText.text = "Baltay� almak i�in 'E'";
                    return;
            }
        }
    }

    private void Awake( )
    {
        Instance = this;
    }
    private void Update( )
    {
        Instance.infotext = false;
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            tempLock = new CursorLockMode( );
            tempLock = Cursor.lockState;
            Cursor.lockState = CursorLockMode.None;
            Settings.SetActive(true);
            tempLive = EventManager.GameIsLive;
            EventManager.GameIsLive = false;
            tempMove = PlayerMovement.Instance.canMove;
            PlayerMovement.Instance.canMove = false;
            InventoryEnv.SetActive(false);
            QuestInfo.SetActive(false);
        }

        if ( !infotext )
            InfoText.gameObject.SetActive(false);

        ReleaseInfo(EventManager.SessionNumber);
    }
    public void ReleaseInfo( int i )
    {
        if ( PlayerMovement.Instance.canMove )
            QuestInfo.SetActive(true);
        switch ( i )
        {
            case 0:
                QuestInfoText.text = "G�rev: Deden ile selamla� ve tatilin ba�las�n!";
                break;
            case 1:
                QuestInfoText.text = "G�rev: Dedenin istedi�i Tamir �antas�n� bul ve dedene g�t�r.";
                break;
            case 2:
                break;
            case 3:
                QuestInfoText.text = "G�rev: Balta'y� evin arkas�ndaki odun kesme alan�na g�t�r.";
                break;
            case 4:
                QuestInfoText.text = "G�rev: Garaja git ve at�lacak �eyleri at. \n" +
                    " G�rev: Bah�e kap�s�n� tamir etmek i�in Odun ve Kereste bul! \n" +
                    "Toplanacak Odun: " + ( RequiredWoods - InventoryManagement.Instance.Slot1Count ) + "\n" +
                    "Toplanacak Kereste: " + ( RequiredPlanks - InventoryManagement.Instance.Slot2Count );
                break;
            case 5:
                QuestInfoText.text = "G�rev: K�mesin kap�s�n� tamir etmek i�in Odun ve Kereste bul! \n" +
                    "Toplanacak Odun: " + ( RequiredWoods - InventoryManagement.Instance.Slot1Count ) + "\n" +
                    "Toplanacak Keresste: " + ( RequiredPlanks - InventoryManagement.Instance.Slot2Count );
                break;
            case 6:
                QuestInfoText.text = "G�rev: T�m ��pleri ve da��lm�� yumurtalar� topla ki Deden bu i�le me�gul olmak zorunda kalmas�n! \n" +
                    "Toplanacak Yumurta: " + ( RequiredEggs - InventoryManagement.Instance.Slot3Count ) + "\n" +
                    "Toplanacak ��p: " + ( RequiredTrashes - InventoryManagement.Instance.Slot4Count );
                break;
        }
    }
    public void SettingsTrigger( )
    {
        Cursor.lockState = tempLock;
        Settings.SetActive(false);
        EventManager.GameIsLive = tempLive;
        PlayerMovement.Instance.canMove = tempMove;
        InventoryEnv.SetActive(true);
        QuestInfo.SetActive(true);
    }

    public void SetSlot1( int value )
    {
        if ( value > 0 )
        {
            InvSlot1Counter.text = value.ToString( );
            InventoryManagement.Instance.Slot1Count = value;
        }
        else
        {
            InvSlot1Counter.text = string.Empty;
            InventoryManagement.Instance.Slot1Count = 0;
        }
    }
    public void SetSlot2( int value )
    {
        if ( value > 0 )
        {
            InvSlot2Counter.text = value.ToString( );
            InventoryManagement.Instance.Slot2Count = value;
        }
        else
        {
            InvSlot2Counter.text = string.Empty;
            InventoryManagement.Instance.Slot2Count = 0;
        }
    }
    public void SetSlot3( int value )
    {
        if ( value > 0 )
        {
            InvSlot3Counter.text = value.ToString( );
            InventoryManagement.Instance.Slot3Count = value;
        }
        else
        {
            InvSlot3Counter.text = string.Empty;
            InventoryManagement.Instance.Slot3Count = 0;
        }
    }
    public void SetSlot4( int value )
    {
        if ( value > 0 )
        {
            InvSlot4Counter.text = value.ToString( );
            InventoryManagement.Instance.Slot4Count = value;
        }
        else
        {
            InvSlot4Counter.text = string.Empty;
            InventoryManagement.Instance.Slot4Count = 0;
        }
    }
    public void SaveGame( )
    {
        PrefsManager.AutoSave( );
    }
    public void LoadGame( )
    {
        PrefsManager.Load( );
    }
    public void QuitGame( )
    {
        Application.Quit( );
    }
    private IEnumerator infoDelay( int delay )
    {
        yield return new WaitForSeconds(delay);
        infotext = false;
    }
}
