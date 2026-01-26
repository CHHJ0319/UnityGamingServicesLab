using UnityEngine;
using UnityEngine.UI;

public class PopuoPanel : MonoBehaviour
{
    public Button confirmButton;

    private void Start()
    {
        confirmButton.onClick.AddListener(Close);
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

}
