using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    public GameObject uiMenu; // 활성화 시킬 ui

    // uiMenu의 활성화 상태를 토글하는 메서드
    public void ToggleMenu()
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
}
