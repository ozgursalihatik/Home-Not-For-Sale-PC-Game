using System.Collections;

using UnityEngine;

public class PrefsManager : MonoBehaviour
{
    public static PrefsManager Instance;
    public static bool isInitialized = false;

    public Prefs prefs;

    private void Awake ( )
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
        InitializePrefs( );
        isInitialized = PlayerPrefs.HasKey("PosX");
    }
    public static void AutoSave ( )
    {
        Instance.InitializePrefs( );
        isInitialized = PlayerPrefs.HasKey("PosX");
    }
    public static void Load ( )
    {
        Instance.ApplyPrefs( );
    }
    private void InitializePrefs ( )
    {
        if ( PlayerPrefs.HasKey("GameSession") )
        {
            prefs.CurrentSession = 0;
            PlayerPrefs.SetInt("GameSession", 0);
        }
        else
        {
            prefs.CurrentSession = (GameSessions)PlayerPrefs.GetInt("GameSession");
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
    }
    private void ApplyPrefs ( )
    {
        PlayerMovement.Position = prefs.lastPos;
        PlayerMovement.Rotation = prefs.lastRot;
        EventManager.CurrentSession = prefs.CurrentSession;
    }
    private void Update ( )
    {
        prefs.lastPos = PlayerMovement.Position;
        prefs.lastRot = PlayerMovement.Rotation;
    }
}

public class Prefs
{
    public GameSessions CurrentSession;
    public Vector3 lastPos;
    public Quaternion lastRot;
}