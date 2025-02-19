using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public GameObject uiMenu; // 활성화할 UI 메뉴
    public GameObject furnitureMenu; // 활성화할 가구 메뉴

    // uiMenu의 활성화 상태를 토글하는 메서드
    public void ToggleUiMenu()
    {
        if (uiMenu != null)
        {
            // uiMenu의 활성화 상태를 반전시킵니다.
            uiMenu.SetActive(!uiMenu.activeSelf);
        }
        else
        {
            Debug.LogError("UIButton: UI Menu GameObject is not assigned.");
        }
    }

    // furnitureMenu의 활성화 상태를 토글하는 메서드
    public void ToggleFurnitureMenu()
    {
        if (furnitureMenu != null)
        {
            // furnitureMenu의 활성화 상태를 반전시킵니다.
            furnitureMenu.SetActive(!furnitureMenu.activeSelf);
        }
        else
        {
            Debug.LogError("UIButton: Furniture Menu GameObject is not assigned.");
        }
    }
}
