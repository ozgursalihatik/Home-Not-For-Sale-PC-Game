using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EqqupableObject : MonoBehaviour
{
    public EqObjectType OnjectsType;
}
public enum EqObjectType
{
    OdunKesmeNoktasi,
    Odun, Kereste, Yumurta,
    Cop, TamirCantasi, GarajCop,
    BahceKapisi, KumesKapisi
}