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

        if ( Physics.Raycast(ray, out hit, 30f) )
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
                            if ( PlayerMovement.Instance.AxeIsGotten )
                                GameManager.Instance.BaltaPositionChange( );
                            break;
                        case EqObjectType.Odun:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(0);
                            InventoryManagement.Instance.Slot1Count++;
                            break;
                        case EqObjectType.Kereste:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(1);
                            InventoryManagement.Instance.Slot2Count++;
                            break;
                        case EqObjectType.Yumurta:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(2);
                            InventoryManagement.Instance.Slot3Count++;
                            break;
                        case EqObjectType.Cop:
                            item.SetActive(false);
                            InventoryManagement.Instance.SetActiveSlot(3);
                            InventoryManagement.Instance.Slot4Count++;
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
                                item.SetActive(false);
                            break;
                        case EqObjectType.KumesKapisi:
                            if ( UIManager.WoodsOK( ) && UIManager.PlanksOK( ) )
                                item.SetActive(false);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
