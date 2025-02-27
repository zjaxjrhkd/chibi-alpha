using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class ButtonGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject moveButton;
    public GameObject rotateButton;
    public GameObject stayButton;
    public GameObject map;


    public RectTransform parentPanel;
    [HideInInspector] 
    public ObjectFactory createObject;
    [HideInInspector] 
    public UIButton uiButton;

    public List<Sprite> furnitureSprites;
    public List<GameObject> furnitureList;

    private FurnitureBase selectedFurniture;
    private NavMeshDataInstance navMeshDataInstance;
    private List<NavMeshBuildSource> sources = new List<NavMeshBuildSource>();
    private AsyncOperation operation;

    void Start()
    {
        createObject = GetComponent<ObjectFactory>();
        uiButton = GetComponent<UIButton>();

        if (moveButton != null)
            moveButton.GetComponent<Button>().onClick.AddListener(() => MoveSelectedFurniture());
        if (rotateButton != null)
            rotateButton.GetComponent<Button>().onClick.AddListener(() => RotateSelectedFurniture());
        if (stayButton != null)
            stayButton.GetComponent<Button>().onClick.AddListener(() => ConfirmSelectedFurniture());

        if (furnitureSprites.Count > 0 && furnitureSprites.Count == furnitureList.Count)
        {
            for (int i = 0; i < furnitureSprites.Count; i++)
                GenerateButton(furnitureSprites[i], i);
        }
        else
        {
            Debug.LogError("가구 스프라이트 리스트 또는 오브젝트 리스트가 잘못되었습니다.");
        }
    }

    void GenerateButton(Sprite sprite, int index)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, parentPanel);
        Image buttonImage = buttonObj.GetComponent<Image>();

        if (buttonImage != null)
            buttonImage.sprite = sprite;
        else
            Debug.LogError("버튼 이미지 컴포넌트를 찾을 수 없습니다.");

        Button buttonComponent = buttonObj.GetComponent<Button>();
        if (buttonComponent != null)
            buttonComponent.onClick.AddListener(() => ButtonClicked(index));
    }

    void ButtonClicked(int index)
    {
        if (index < furnitureList.Count)
        {
            GameObject furnitureToCreate = furnitureList[index];
            GameObject newFurniture = createObject.CreateFurniture(furnitureToCreate);
            Debug.Log(furnitureList[index].name + " 가구를 생성합니다.");

            selectedFurniture = newFurniture.GetComponent<FurnitureBase>();
            if (selectedFurniture == null)
            {
                Debug.LogError("FurnitureBase 컴포넌트가 없습니다.");
                return;
            }

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

    void MoveSelectedFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.EnableMovement();
            Debug.Log("선택된 가구 이동 활성화");
        }
    }

    void RotateSelectedFurniture()
    {
        if (selectedFurniture != null)
        {
            selectedFurniture.Rotate();
            Debug.Log("선택된 가구 회전 활성화");
        }
    }

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
