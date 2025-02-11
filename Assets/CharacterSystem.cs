using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public Transform parentTransform; // A 오브젝트
    public Transform targetTransform; // C 오브젝트
    public Transform objectToActivate; // B 오브젝트 (인스펙터에서 받음)
    public GameObject targetObject; // 인스펙터에서 할당할 오브젝트
    public float distanceFromParent = 2.0f; // A로부터 C 방향으로의 거리
    public float distanceBetweenBAndC = 2.0f; // B와 C 사이의 거리

    private Vector3 originalTargetPosition; // C 오브젝트의 원래 위치 저장
    private Quaternion originalTargetRotation; // C 오브젝트의 원래 회전 저장

    private void OnMouseDown()
    {
        if (targetObject.activeSelf)
        {
            objectToActivate.gameObject.SetActive(false);
            ReturnToOriginalPositionAndRotation();
        }
        else
        {
            SaveOriginalPositionAndRotation();
            AlignAndActivate();
            AdjustTargetPosition();
        }
    }

    private void AlignAndActivate()
    {
        if (targetTransform != null && parentTransform != null)
        {
            // A와 C 사이의 방향 벡터 계산
            Vector3 directionToTarget = (targetTransform.position - parentTransform.position).normalized;

            // B 오브젝트의 위치를 A로부터 C의 방향으로 지정된 거리만큼 설정
            objectToActivate.position = parentTransform.position + directionToTarget * distanceFromParent;

            // B 오브젝트가 C 오브젝트를 바라보게 설정
            objectToActivate.LookAt(targetTransform);
            objectToActivate.rotation = Quaternion.Euler(objectToActivate.rotation.eulerAngles.x + 50f,
                                    objectToActivate.rotation.eulerAngles.y,
                                    objectToActivate.rotation.eulerAngles.z);

            // B 오브젝트 활성화
            objectToActivate.gameObject.SetActive(true);
        }
    }

    private void AdjustTargetPosition()
    {
        if (targetTransform != null && objectToActivate != null)
        {
            // C 오브젝트가 B 오브젝트를 바라보도록 설정
            targetTransform.LookAt(objectToActivate);

            // C 오브젝트를 B 오브젝트 방향으로 이동하여 지정된 거리 유지
            Vector3 moveDirection = (objectToActivate.position - targetTransform.position).normalized;
            targetTransform.position = objectToActivate.position - moveDirection * distanceBetweenBAndC;
        }
    }

    private void SaveOriginalPositionAndRotation()
    {
        if (targetTransform != null)
        {
            originalTargetPosition = targetTransform.position;
            originalTargetRotation = targetTransform.rotation;
        }
    }

    private void ReturnToOriginalPositionAndRotation()
    {
        if (targetTransform != null)
        {
            targetTransform.position = originalTargetPosition;
            targetTransform.rotation = originalTargetRotation;
        }
    }
}

