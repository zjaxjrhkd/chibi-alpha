using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    public GameObject CreateFurniture(GameObject furniturePrefab)
    {
        Vector3 spawnPosition = new Vector3(0, 1, 0); // 원하는 위치
        GameObject newFurniture = Instantiate(furniturePrefab, spawnPosition, Quaternion.identity);
        return newFurniture; // 생성된 오브젝트 반환
    }

}
