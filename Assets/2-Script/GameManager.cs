using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform Hala, Dede;
    public GameObject Balta, BahceKapi, GarajKapi, KumesKapi, Planks1, Planks2, Eggs;

    public Vector3 BaltaTargetPos;
    public Vector3 DedeFirsPos, DedeFirstRot, DedeSecondRot;
    public Vector3 HalaFirstPos, HalaFirstRot;
    public Vector3 HalaSecondPos, HalaSecondRot;

    private void Start( )
    {

    }
    public void BaltaPositionChange( )
    {
        Balta.transform.position = new Vector3(176.173f, 2.078f, 140.788f);
        Balta.transform.rotation = Quaternion.Euler(new Vector3(2.719f, -80.502f, -24.943f));
    }
    public void DedeRotationChange( int index )
    {
        if ( index == 0 )
        {
            Dede.rotation = Quaternion.Euler(DedeFirstRot);
        }
        else if ( index == 1 )
        {
            Dede.rotation = Quaternion.Euler(DedeSecondRot);
        }
    }
    public void HalaPositionChange( int index )
    {
        if ( index == 0 )
        {
            Hala.gameObject.SetActive(true);
            Hala.position = HalaFirstPos;
            Hala.rotation = Quaternion.Euler(HalaFirstRot);
        }
        else if ( index == 1 )
        {
            Hala.position = HalaSecondPos;
            Hala.rotation = Quaternion.Euler(HalaSecondRot);
        }
    }
    public void BahceKapisiAcilsin( )
    {
        BahceKapi.SetActive(false);
    }
    public void GarajKapisiAcilsin( )
    {
        GarajKapi.SetActive(false);
    }
    public void KumesKapisiAcilsin( )
    {
        KumesKapi.SetActive(false);
        Eggs.SetActive(true);
    }
}
