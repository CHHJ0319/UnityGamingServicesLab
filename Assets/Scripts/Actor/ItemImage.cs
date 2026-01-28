using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemImage : MonoBehaviour
{
    [Header("UI References")]
    public Image targetImage;

    [Header("Settings")]
    public List<Sprite> imageList = new List<Sprite>();

    public void SetImageByIndex(int index)
    {
        if (imageList == null || imageList.Count == 0)
        {
            Debug.LogWarning("이미지 리스트가 비어 있습니다.");
            return;
        }
        int safeIndex = Mathf.Clamp(index, 0, imageList.Count - 1);

        if (targetImage != null)
        {
            targetImage.sprite = imageList[safeIndex];
        }
        else
        {
        }
    }
}
