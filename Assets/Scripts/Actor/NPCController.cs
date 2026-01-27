using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class NPCController : MonoBehaviour
{
    public PlayerPanel playerPanel;
    public MarketPanel marketPanel;

    void Update()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            HandleClick();
        }
    }

    void HandleClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                playerPanel.gameObject.SetActive(true);
                marketPanel.gameObject.SetActive(true);
            }
        }
    }
}
