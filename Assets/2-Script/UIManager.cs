using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Settings, InventoryEnv, QuestInfo;
    public GameObject InvSlot1, InvSlot2, InvSlot3;
    public TMP_Text InvSlot1Counter, InvSlot2Counter, InvSlot3Counter,
        QuestInfoText;
    public int RequiredWoods, RequiredPlanks, RequiredEggs, RequiredTrashes;

    private CursorLockMode tempLock;
    private bool tempLive;
    private bool tempMove;
    private GameObject tempObject;

    private void Awake( )
    {
        Instance = this;
    }
    private void Update( )
    {
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
        }
        if ( tempObject != null )
        {
            if ( Input.GetKeyDown(KeyCode.E) )
            {
                tempObject.SetActive(false);
            }
        }
    }

    public void ReleaseInfo( )
    {
        switch ( EventManager.SessionNumber )
        {
            case 0:
                QuestInfoText.text = "G�rev: Deden ile selamla� ve tatilin ba�las�n!";
                break;
            case 1:
                QuestInfoText.text = "G�rev: Dedenin istedi�i Tamir �antas�n� bul ve dedene g�t�r.";
                break;
            case 2:
                QuestInfoText.text = "G�rev: Balta'y� evin arkas�ndaki odun kesme alan�na g�t�r.";
                break;
            case 3:
                QuestInfoText.text = "G�rev: Garaja git ve at�lacak �eyleri at.";
                break;
            case 4:
                QuestInfoText.text = "G�rev: Bah�e kap�s�n� tamir etmek i�in Odun ve Kereste bul! \n" +
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

    public void EqquipItem( GameObject item )
    {
        tempObject = item;
    }
    public void ItemIsFar( )
    {
        tempObject = null;
    }

    public void SettingsTrigger( )
    {
        Cursor.lockState = tempLock;
        Settings.SetActive(false);
        EventManager.GameIsLive = tempLive;
        PlayerMovement.Instance.canMove = tempMove;
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
        }
    }

    /// <summary>
    /// It changes the activity value of the given slot. 
    /// </summary>
    /// <param name="index">Changing values index</param>
    /// <param name="value">Value to change</param>
    public static void SetActiveInventory( int index, bool value )
    {
        switch ( index )
        {
            case 0:
                Instance.InvSlot1.SetActive(value);
                break;
            case 1:
                Instance.InvSlot2.SetActive(value);
                break;
            case 2:
                Instance.InvSlot3.SetActive(value);
                break;
        }
        Debug.LogError("Slots Indexes cannot be less than 0 and greater than 2.");
        return;
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
}
