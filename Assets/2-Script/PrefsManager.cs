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
    }
    private void Start ( )
    {
        InitializePrefs( );
        isInitialized = PlayerPrefs.HasKey("GameSession");
        Load( );

    }
    public static void AutoSave ( )
    {
        Instance.Save( );
        isInitialized = PlayerPrefs.HasKey("PosX");
    }
    public static void Load ( )
    {
        Instance.ApplyPrefs( );
    }
    private void Save ( )
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
    }
    private void InitializePrefs ( )
    {
        prefs = new Prefs( );
        if ( !PlayerPrefs.HasKey("GameSession") )
        {
            prefs.CurrentSession = 0;
            PlayerPrefs.SetInt("GameSession", 0);
        }
        else
        {
            prefs.CurrentSession = PlayerPrefs.GetInt("GameSession");
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
        EventManager.SessionNumber = prefs.CurrentSession;
    }
}

public class Prefs
{
    public int CurrentSession;
    public Vector3 lastPos;
    public Quaternion lastRot;
}