using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능 사용을 위해 추가

public class GameManager : MonoBehaviour
{
    public GameObject CharacterPrefab; // 캐릭터 프리팹 참조를 위한 변수
    public GameObject ButtonInstance; // 이미 생성되어 있는 메뉴 버튼의 인스턴스 참조
    public GameObject ButtonInstance1; // 옷 변경 버튼 1 -> 나중에 List써서 의상 추가 가능하게 합치고 수정하기
    public GameObject ButtonInstance2; // 옷 변경 버튼 2
    public GameObject ButtonInstance3; // 옷 변경 버튼 3
    
    [HideInInspector]
    public GameObject CharacterInstance;
    [HideInInspector]
    public ChangeClothes changeClothesScript;//옷 변경 스크립트
    [HideInInspector]
    public Vector2 screenPosition; //화면좌표
    [HideInInspector]
    public RectTransform rectTransform;//월드좌표전환

    private RaycastHit hit;

    void Start()
    {
        // 캐릭터 프리팹을 씬에 인스턴스화
        CharacterInstance = Instantiate(CharacterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        ButtonInstance.SetActive(false); // 초기에 버튼을 비활성화

        // 각 버튼에 클릭 이벤트 리스너 추가 or 인스펙터창에서 추가
        /*
        ButtonInstance1.GetComponent<Button>().onClick.AddListener(CallChangeClothesOne);
        ButtonInstance2.GetComponent<Button>().onClick.AddListener(CallChangeClothesTwo);
        ButtonInstance3.GetComponent<Button>().onClick.AddListener(CallChangeClothesThree);
        */
        //옷 변경 스크립트
        changeClothesScript = CharacterInstance.GetComponent<ChangeClothes>();
        //월드 좌표
        rectTransform = ButtonInstance.GetComponent<RectTransform>();

    }

    void Update()
    {
        MenuClick();
    }

    void MenuClick()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 감지
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == CharacterInstance) // 클릭된 오브젝트가 CharacterInstance인지 확인
                {
                    Menu(hit.point);
                }
            }
        }
    }
    

    void Menu(Vector3 position)
    {
        Debug.Log("메뉴테스트");
        screenPosition = Camera.main.WorldToScreenPoint(position); //Menu가 호출될 때 position값을 가져와야함
        if (rectTransform != null)
        {
            rectTransform.position = screenPosition;
            ButtonInstance.SetActive(true); // 버튼을 활성화
        }
        else
        {
            Debug.LogError("RectTransform is missing on the button instance!");
        }
    }

    public void CallChangeClothesOne()
    {
        if (changeClothesScript != null)
        {
            changeClothesScript.ChangeClothesOne(); // 첫 번째 옷 변경
        }
        else
        {
            Debug.LogError("ChangeClothes script is not found on the CharacterInstance!");
        }
        ButtonInstance.SetActive(false); // 버튼을 비활성화

    }

    public void CallChangeClothesTwo()
    {
        if (changeClothesScript != null)
        {
            changeClothesScript.ChangeClothesTwo(); // 두 번째 옷 변경
        }
        else
        {
            Debug.LogError("ChangeClothes script is not found on the CharacterInstance!");
        }
        ButtonInstance.SetActive(false); // 버튼을 비활성화
    }

    public void CallChangeClothesThree()
    {
        if (changeClothesScript != null)
        {
            changeClothesScript.ChangeClothesThree(); // 세 번째 옷 변경
        }
        else
        {
            Debug.LogError("ChangeClothes script is not found on the CharacterInstance!");
        }
        ButtonInstance.SetActive(false); // 버튼을 비활성화
    }
}
