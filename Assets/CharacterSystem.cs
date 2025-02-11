using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public Transform parentTransform; // A ������Ʈ
    public Transform targetTransform; // C ������Ʈ
    public Transform objectToActivate; // B ������Ʈ (�ν����Ϳ��� ����)
    public GameObject targetObject; // �ν����Ϳ��� �Ҵ��� ������Ʈ
    public float distanceFromParent = 2.0f; // A�κ��� C ���������� �Ÿ�
    public float distanceBetweenBAndC = 2.0f; // B�� C ������ �Ÿ�

    private Vector3 originalTargetPosition; // C ������Ʈ�� ���� ��ġ ����
    private Quaternion originalTargetRotation; // C ������Ʈ�� ���� ȸ�� ����

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
            // A�� C ������ ���� ���� ���
            Vector3 directionToTarget = (targetTransform.position - parentTransform.position).normalized;

            // B ������Ʈ�� ��ġ�� A�κ��� C�� �������� ������ �Ÿ���ŭ ����
            objectToActivate.position = parentTransform.position + directionToTarget * distanceFromParent;

            // B ������Ʈ�� C ������Ʈ�� �ٶ󺸰� ����
            objectToActivate.LookAt(targetTransform);
            objectToActivate.rotation = Quaternion.Euler(objectToActivate.rotation.eulerAngles.x + 50f,
                                    objectToActivate.rotation.eulerAngles.y,
                                    objectToActivate.rotation.eulerAngles.z);

            // B ������Ʈ Ȱ��ȭ
            objectToActivate.gameObject.SetActive(true);
        }
    }

    private void AdjustTargetPosition()
    {
        if (targetTransform != null && objectToActivate != null)
        {
            // C ������Ʈ�� B ������Ʈ�� �ٶ󺸵��� ����
            targetTransform.LookAt(objectToActivate);

            // C ������Ʈ�� B ������Ʈ �������� �̵��Ͽ� ������ �Ÿ� ����
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

