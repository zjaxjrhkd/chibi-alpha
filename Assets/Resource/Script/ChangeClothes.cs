using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능 사용을 위해 추가

public class ChangeClothes : MonoBehaviour
{
    public List<GameObject> clothesGroup;


    void Start()
    {

    }

    public void ChangeClothesOne()
    {
        Debug.Log("첫번째");
        // 첫 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 0);
        }
    }

    public void ChangeClothesTwo()
    {
        Debug.Log("두번째");
        // 두 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 1);
        }
    }

    public void ChangeClothesThree()
    {
        Debug.Log("세번째");
        // 세 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 2);
        }
    }
}
