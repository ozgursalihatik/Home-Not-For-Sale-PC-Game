using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Settings, InventoryEnv;
    public GameObject InvSlot1, InvSlot2, InvSlot3;
    public TMP_Text InvSlot1Counter, InvSlot2Counter, InvSlot3Counter;

    private CursorLockMode tempLock;
    private bool tempLive;
    private bool tempMove;

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
