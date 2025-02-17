using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // 버튼 프리팹
    public RectTransform parentPanel; // 버튼을 생성할 부모 패널
    public List<GameObject> furnitureList; // 가구 객체 리스트

    void Start()
    {
        Debug.Log("Furniture List의 개수: " + furnitureList.Count);

        foreach (GameObject furniture in furnitureList)
        {
            GenerateButton(furniture);
        }
    }

    void GenerateButton(GameObject furniture)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, parentPanel);

        // TextMeshProUGUI를 사용하여 버튼 텍스트 설정
        TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = furniture.name;
        }
        else
        {
            Debug.LogError("버튼 텍스트를 찾을 수 없습니다.");
        }

        // 버튼 클릭 이벤트 추가
        Button buttonComponent = buttonObj.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => ButtonClicked(furniture.name));
        }
    }

    void ButtonClicked(string furnitureName)
    {
        Debug.Log(furnitureName + " clicked!");
    }
}
