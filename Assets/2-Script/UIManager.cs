using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject Settings;

    private CursorLockMode tempLock;
    private bool tempLive;
    private bool tempMove;

    private void Awake ( )
    {
        Instance = this;
    }
    private void Update ( )
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
    public void SettingsTrigger ( )
    {
        Cursor.lockState = tempLock;
        Settings.SetActive(false);
        EventManager.GameIsLive = tempLive;
        PlayerMovement.Instance.canMove = tempMove;
    }
    public void SaveGame ( )
    {
        PrefsManager.AutoSave( );
    }
    public void LoadGame ( )
    {
        PrefsManager.Load( );
    }
    public void QuitGame ( )
    {
        Application.Quit( );
    }
}
