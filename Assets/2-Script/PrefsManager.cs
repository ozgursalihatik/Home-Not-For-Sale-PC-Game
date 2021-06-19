using System.Collections;

using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static PrefsManager Instance;
    public static bool isInitialized = false;

    public Prefs prefs;

    public delegate void PrefsGate( );
    public static PrefsGate OnLoaded;
    public static PrefsGate OnSaved;
    public static PrefsGate OnStart;

    private void Awake( )
    {
        if ( Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        StartUp( );
    }
    public void StartUp( )
    {
        InitializePrefs( );
        ApplyPrefs( );

        isInitialized = PlayerPrefs.HasKey("GameSession");

        if ( OnStart != null )
            OnStart( );
    }
    public static void AutoSave( )
    {
        Instance.Save( );
        isInitialized = PlayerPrefs.HasKey("PosX");
    }
    public static void Load( )
    {
        Instance.ApplyPrefs( );
    }
    private void Save( )
    {
        PlayerPrefs.SetInt("GameSession", EventManager.SessionNumber);
        prefs.CurrentSession = PlayerPrefs.GetInt("GameSession");

        PlayerPrefs.SetFloat("PosX", PlayerMovement.Position.x);
        PlayerPrefs.SetFloat("PosY", PlayerMovement.Position.y);
        PlayerPrefs.SetFloat("PosZ", PlayerMovement.Position.z);

        prefs.lastPos = new Vector3(PlayerPrefs.GetFloat("PosX"),
                                    PlayerPrefs.GetFloat("PosY"),
                                    PlayerPrefs.GetFloat("PosZ"));

        PlayerPrefs.SetFloat("RotX", PlayerMovement.Rotation.x);
        PlayerPrefs.SetFloat("RotY", PlayerMovement.Rotation.y);
        PlayerPrefs.SetFloat("RotZ", PlayerMovement.Rotation.z);
        PlayerPrefs.SetFloat("RotW", PlayerMovement.Rotation.w);

        prefs.lastRot = new Quaternion(PlayerPrefs.GetFloat("RotX"),
                                       PlayerPrefs.GetFloat("RotY"),
                                       PlayerPrefs.GetFloat("RotZ"),
                                       PlayerPrefs.GetFloat("RotW"));

        PlayerPrefs.SetInt("Slot1", InventoryManagement.Instance.Slot1Count);
        prefs.InvSlot1 = PlayerPrefs.GetInt("Slot1");

        PlayerPrefs.SetInt("Slot2", InventoryManagement.Instance.Slot2Count);
        prefs.InvSlot2 = PlayerPrefs.GetInt("Slot2");

        PlayerPrefs.SetInt("Slot3", InventoryManagement.Instance.Slot3Count);
        prefs.InvSlot3 = PlayerPrefs.GetInt("Slot3");

        PlayerPrefs.Save( );

        if ( OnSaved != null )
            OnSaved( );
    }
    private void InitializePrefs( )
    {
        prefs = new Prefs( );
        if ( PlayerPrefs.HasKey("GameSession") )
        {
            prefs.CurrentSession = PlayerPrefs.GetInt("GameSession");
        }
        else
        {
            prefs.CurrentSession = 0;
            PlayerPrefs.SetInt("GameSession", 0);
        }

        if ( PlayerPrefs.HasKey("PosX") )
        {
            prefs.lastPos = new Vector3(PlayerPrefs.GetFloat("PosX"),
                                        PlayerPrefs.GetFloat("PosY"),
                                        PlayerPrefs.GetFloat("PosZ"));
        }
        else
        {
            PlayerPrefs.SetFloat("PosX", PlayerMovement.Position.x);
            PlayerPrefs.SetFloat("PosY", PlayerMovement.Position.y);
            PlayerPrefs.SetFloat("PosZ", PlayerMovement.Position.z);

            prefs.lastPos = new Vector3(PlayerPrefs.GetFloat("PosX"),
                                        PlayerPrefs.GetFloat("PosY"),
                                        PlayerPrefs.GetFloat("PosZ"));
        }

        if ( PlayerPrefs.HasKey("RotX") )
        {
            prefs.lastRot = new Quaternion(PlayerPrefs.GetFloat("RotX"),
                                           PlayerPrefs.GetFloat("RotY"),
                                           PlayerPrefs.GetFloat("RotZ"),
                                           PlayerPrefs.GetFloat("RotW"));
        }
        else
        {
            PlayerPrefs.SetFloat("RotX", PlayerMovement.Rotation.x);
            PlayerPrefs.SetFloat("RotY", PlayerMovement.Rotation.y);
            PlayerPrefs.SetFloat("RotZ", PlayerMovement.Rotation.z);
            PlayerPrefs.SetFloat("RotW", PlayerMovement.Rotation.w);

            prefs.lastRot = new Quaternion(PlayerPrefs.GetFloat("RotX"),
                                           PlayerPrefs.GetFloat("RotY"),
                                           PlayerPrefs.GetFloat("RotZ"),
                                           PlayerPrefs.GetFloat("RotW"));
        }

        if ( PlayerPrefs.HasKey("Slot1") )
        {
            prefs.InvSlot1 = PlayerPrefs.GetInt("Slot1");
        }
        else
        {
            PlayerPrefs.SetInt("Slot1", 0);
            prefs.InvSlot1 = 0;
        }

        if ( PlayerPrefs.HasKey("Slot2") )
        {
            prefs.InvSlot2 = PlayerPrefs.GetInt("Slot2");
        }
        else
        {
            PlayerPrefs.SetInt("Slot2", 0);
            prefs.InvSlot2 = 0;
        }

        if ( PlayerPrefs.HasKey("Slot3") )
        {
            prefs.InvSlot3 = PlayerPrefs.GetInt("Slot3");
        }
        else
        {
            PlayerPrefs.SetInt("Slot3", 0);
            prefs.InvSlot3 = 0;
        }
    }
    private void ApplyPrefs( )
    {
        PlayerMovement.Position = prefs.lastPos;
        PlayerMovement.Rotation = prefs.lastRot;
        EventManager.SessionNumber = prefs.CurrentSession;
        InventoryManagement.Instance.Slot1Count = prefs.InvSlot1;
        InventoryManagement.Instance.Slot2Count = prefs.InvSlot2;
        InventoryManagement.Instance.Slot3Count = prefs.InvSlot3;
        if ( OnLoaded != null )
            OnLoaded( );
    }
}

public class Prefs
{
    public int CurrentSession;
    public Vector3 lastPos;
    public Quaternion lastRot;
    public int InvSlot1, InvSlot2, InvSlot3;
}