using System.Collections.Generic;
using UnityEngine;

public static class ItemDescriptions
{
    public static readonly Dictionary<string, string> Descriptions = new Dictionary<string, string>
    {
        { "FLAME_MATERIAL_000", "샐러맨더의 발톱.\n 화산 속에 사는 불꽃 도마뱀 샐라맨더의 발톱. \n 상대를 불태우는 불속성 무기의 재료" },
        { "FLAME_MATERIAL_001", "샐러맨더의 비늘.\n 화산 속에 사는 불꽃 도마뱀 샐라맨더의 비늘. \n 불속성에 강한 내성을 부여하는 장비의 재료" },
    };

    public static string GetDescription(string itemId)
    {
        if (Descriptions.TryGetValue(itemId, out string description))
        {
            return description;
        }
        return "알 수 없는 아이템입니다.";
    }
}
