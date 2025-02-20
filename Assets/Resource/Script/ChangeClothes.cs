using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{
    public List<GameObject> clothesGroup;

    public void ChangeClothesOne()
    {
        Debug.Log("첫번쨰");
        // 첫 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 0);
        }
    }

    public void ChangeClothesTwo()
    {
        Debug.Log("두번쨰");
        // 두 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 1);
        }
    }

    public void ChangeClothesThree()
    {
        Debug.Log("세번쨰");
        // 세 번째 옷을 활성화하고 나머지는 비활성화
        for (int i = 0; i < clothesGroup.Count; i++)
        {
            clothesGroup[i].SetActive(i == 2);
        }
    }
}
