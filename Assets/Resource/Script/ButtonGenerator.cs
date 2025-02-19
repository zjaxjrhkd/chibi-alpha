using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab; // 버튼 프리팹
    public GameObject moveButton;   // 가구 move 버튼
    public GameObject rotateButton; // 가구 rotate 버튼
    public GameObject stayButton;   // 가구 stay 버튼

    public RectTransform parentPanel; // 버튼을 생성할 부모 패널
    [HideInInspector]
    public ObjectFactory createObject; // ObjectFactory 참조
    [HideInInspector]
    public UIButton uiButton; // UI 메뉴 토글 버튼

    public List<Sprite> furnitureSprites; // 가구의 스프라이트 리스트
    public List<GameObject> furnitureList; // 실제 생성할 가구 오브젝트 리스트

    private FurnitureBase selectedFurniture; // 현재 선택된 가구를 저장할 변수

    void Start()
    {
        createObject = GetComponent<ObjectFactory>();
        uiButton = GetComponent<UIButton>();

        // 버튼에 기본 이벤트 연결 (초기에는 가구 없음)
        if (moveButton != null)
        {
            moveButton.GetComponent<Button>().onClick.AddListener(() => MoveSelectedFurniture());
        }
        if (rotateButton != null)
        {
            rotateButton.GetComponent<Button>().onClick.AddListener(() => RotateSelectedFurniture());
        }
        if (stayButton != null)
        {
            stayButton.GetComponent<Button>().onClick.AddListener(() => ConfirmSelectedFurniture());
        }

        if (furnitureSprites.Count > 0 && furnitureSprites.Count == furnitureList.Count)
        {
            for (int i = 0; i < furnitureSprites.Count; i++)
            {
                GenerateButton(furnitureSprites[i], i);
            }
        }
        else
        {
            Debug.LogError("가구 스프라이트 리스트 또는 가구 오브젝트 리스트가 비어 있거나 일치하지 않습니다.");
        }
    }

    void GenerateButton(Sprite sprite, int index)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, parentPanel);

        // 버튼의 이미지 설정
        Image buttonImage = buttonObj.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("버튼 이미지 컴포넌트를 찾을 수 없습니다.");
        }

        // 버튼 클릭 이벤트 추가
        Button buttonComponent = buttonObj.GetComponent<Button>();
        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => ButtonClicked(index));
        }
    }

    void ButtonClicked(int index)
    {
        if (index < furnitureList.Count)
        {
            GameObject furnitureToCreate = furnitureList[index];
            GameObject newFurniture = createObject.CreateFurniture(furnitureToCreate); // 생성된 가구 반환

            Debug.Log(furnitureList[index].name + " 가구를 생성합니다.");

            // 새로 생성된 가구를 현재 선택된 가구로 설정
            selectedFurniture = newFurniture.GetComponent<FurnitureBase>();

            if (selectedFurniture == null)
            {
                Debug.LogError("FurnitureBase 스크립트가 생성된 가구에 없습니다.");
                return;
            }

            // UI 메뉴 토글
            if (uiButton != null)
            {
                uiButton.ToggleUiMenu();
                uiButton.ToggleFurnitureMenu();
            }
        }
        else
        {
            Debug.LogError("인덱스가 가구 리스트 범위를 벗어났습니다.");
        }
    }

    // 현재 선택된 가구의 이동 활성화
    void MoveSelectedFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.EnableMovement();
            Debug.Log("선택된 가구 이동 활성화");
        }
    }

    // 현재 선택된 가구 회전 활성화
    void RotateSelectedFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.Rotate();
            Debug.Log("선택된 가구 회전 활성화");
        }
    }

    // 현재 선택된 가구 위치 확정
    void ConfirmSelectedFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.ConfirmPosition();
            uiButton.ToggleFurnitureMenu();
            Debug.Log("선택된 가구 위치 확정");
        }
    }
}
