using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;
    void Start( )
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update( )
    {
        if ( !EventManager.GameIsLive )
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if ( Physics.Raycast(ray, out hit, 10f) )
        {
            GameObject item = hit.collider.gameObject;
            if ( item.TryGetComponent(out EqqupableObject EqObject) )
            {
                UIManager.ShowObjectInfo(EqObject);
                if ( Input.GetKey(KeyCode.E) )
                {
                    switch ( EqObject.OnjectsType )
                    {
                        case EqObjectType.OdunKesmeNoktasi:
                            if ( !GameManager.Instance.AxeIsPlaced )
                            {
                                if ( PlayerMovement.Instance.AxeIsGotten )
                                    GameManager.Instance.BaltaPositionChange( );
                                else
                                    UIManager.SendMessageBox("Baltayý almazsan buraya býrakamazsýn.");
                            }
                            else
                            {
                                InventoryManagement.Instance.SetActiveSlot(0);
                                InventoryManagement.Instance.Slot1Count++;
                                UIManager.Instance.GottenWoods++;
                                Dictionary<string, int> dict1 = new Dictionary<string, int>( );
                                dict1.Add("Wood", UIManager.Instance.GottenWoods);
                                UIManager.SetInventory(dict1);
                            }
                            break;
                        case EqObjectType.Odun:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(0);
                            InventoryManagement.Instance.Slot1Count++;
                            UIManager.Instance.GottenWoods++;
                            Dictionary<string, int> dict2 = new Dictionary<string, int>( );
                            dict2.Add("Wood", UIManager.Instance.GottenWoods);
                            UIManager.SetInventory(dict2);
                            break;
                        case EqObjectType.Kereste:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(1);
                            InventoryManagement.Instance.Slot2Count++;
                            UIManager.Instance.GottenPlanks++; Dictionary<string, int> dict3 = new Dictionary<string, int>( );
                            dict3.Add("Plank", UIManager.Instance.GottenPlanks);
                            UIManager.SetInventory(dict3);
                            break;
                        case EqObjectType.Yumurta:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(2);
                            InventoryManagement.Instance.Slot3Count++;
                            UIManager.Instance.GottenEggs++;
                            Dictionary<string, int> dict4 = new Dictionary<string, int>( );
                            dict4.Add("Eggs", UIManager.Instance.GottenEggs);
                            UIManager.SetInventory(dict4);
                            break;
                        case EqObjectType.Cop:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(3);
                            InventoryManagement.Instance.Slot4Count++;
                            UIManager.Instance.GottenTrashes++;
                            Dictionary<string, int> dict = new Dictionary<string, int>( );
                            dict.Add("Trashes", UIManager.Instance.GottenTrashes);
                            UIManager.SetInventory(dict);
                            break;
                        case EqObjectType.TamirCantasi:
                            item.SetActive(false);
                            PlayerMovement.Instance.FixItBoxGotten = true;
                            break;
                        case EqObjectType.GarajCop:
                            item.SetActive(false);
                            break;
                        case EqObjectType.BahceKapisi:
                            if ( UIManager.WoodsOK( ) && UIManager.PlanksOK( ) )
                            {
                                GameManager.Instance.BahceKapisiAcilsin( );
                                UIManager.Instance.GottenWoods = 0;
                                UIManager.Instance.GottenPlanks = 0;
                                Dictionary<string, int> dict5 = new Dictionary<string, int>( );
                                dict5.Add("Wood", 0);
                                dict5.Add("Plank", 0);
                                UIManager.SetInventory(dict5);
                            }
                            break;
                        case EqObjectType.KumesKapisi:
                            if ( UIManager.WoodsOK( ) && UIManager.PlanksOK( ) )
                            {
                                Dictionary<string, int> dict6 = new Dictionary<string, int>( );
                                dict6.Add("Wood", 0);
                                dict6.Add("Plank", 0);
                                dict6.Add("Eggs", 0);
                                dict6.Add("Trashes", 0);
                                UIManager.SetInventory(dict6);
                            }
                            break;
                        case EqObjectType.Balta:
                            GameManager.Instance.Balta.SetActive(false);
                            PlayerMovement.Instance.AxeIsGotten = true;
                            break;
                    }
                }
            }
        }
    }
}
