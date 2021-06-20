using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public static InventoryManagement Instance;

    public GameObject Slot1, Slot2, Slot3, Slot4;
    public int Slot1Count, Slot2Count, Slot3Count, Slot4Count;

    private void Awake( )
    {
        Instance = this;
    }
    public bool GetSlotsActivation( int SlotIndex )
    {
        switch ( SlotIndex )
        {
            case 0:
                return Slot1.activeInHierarchy;
            case 1:
                return Slot2.activeInHierarchy;
            case 2:
                return Slot3.activeInHierarchy;
            case 3:
                return Slot4.activeInHierarchy;
        }
        Debug.LogError("Slots Index cannot be less than 0 and greater than 3.");
        return false;
    }
    public void SetActiveSlot( int SlotIndex )
    {
        switch ( SlotIndex )
        {
            case 0:
                Slot1.SetActive(true);
                break;
            case 1:
                Slot2.SetActive(true);
                break;
            case 2:
                Slot3.SetActive(true);
                break;
            case 3:
                Slot4.SetActive(true);
                break;
        }
    }
}
