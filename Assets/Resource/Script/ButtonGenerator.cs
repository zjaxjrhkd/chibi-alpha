using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // 버튼 프리팹
    public RectTransform parentPanel; // 버튼을 생성할 부모 패널
    public List<Sprite> furnitureSprites; // 가구의 스프라이트 리스트

    void Start()
    {
        if (furnitureSprites.Count != 0)
        {
            foreach (Sprite sprite in furnitureSprites)
            {
                GenerateButton(sprite);
            }
        }
        else
        {
            Debug.LogError("가구 스프라이트 리스트가 비어 있습니다.");
        }
    }

    void GenerateButton(Sprite sprite)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, parentPanel);

        // 버튼의 이미지 컴포넌트 찾기
        Image buttonImage = buttonObj.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = sprite; // 스프라이트 설정
        }
        else
        {
            Debug.LogError("버튼 이미지 컴포넌트를 찾을 수 없습니다.");
        }

        // 버튼 클릭 이벤트 추가
        Button buttonComponent = buttonObj.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => ButtonClicked(sprite.name));
        }
    }

    void ButtonClicked(string spriteName)
    {
        Debug.Log(spriteName + " 가구 구매");
    }
}
