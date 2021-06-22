using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EqqupableObject : MonoBehaviour
{
    public EqObjectType OnjectsType;

    private void OnDisable( )
    {
        if ( GameManager.Instance != null )
            if ( OnjectsType == EqObjectType.Odun )
                GameManager.Instance.StartCoroutine("WoodDelay");
            else return;
        else return;
    }
}
public enum EqObjectType
{
    OdunKesmeNoktasi,
    Odun, Kereste, Yumurta,
    Cop, TamirCantasi, GarajCop,
    BahceKapisi, KumesKapisi, Balta
}