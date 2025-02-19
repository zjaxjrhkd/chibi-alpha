using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureBase : MonoBehaviour
{
    enum State { Move, Stay, Rotate }
    State currentState = State.Move;

    private Vector3 screenPoint;
    private Vector3 offset;
    private MeshRenderer meshRenderer;
    private Color originalColor;

    void Start()
    {
        currentState = State.Move;

        // MeshRenderer 가져오기
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.Log("없음");
        }
        else if (meshRenderer.materials.Length > 0)
        {
            originalColor = meshRenderer.materials[0].color;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("부딫쳐요");
        if (currentState == State.Move || currentState == State.Rotate)
        {
            Debug.Log("안되요");
            if (meshRenderer != null && meshRenderer.materials.Length > 0)
            {
                meshRenderer.materials[0].color = Color.red;
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (meshRenderer != null && meshRenderer.materials.Length > 0)
        {
            meshRenderer.materials[0].color = originalColor;
        }
    }

    void OnMouseDown()
    {
        if (currentState == State.Move)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (currentState == State.Move)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            transform.position = new Vector3(cursorPosition.x, transform.position.y, cursorPosition.z);
        }
    }

    public void ConfirmPosition()
    {
        currentState = State.Stay;
    }

    public void EnableMovement()
    {
        currentState = State.Move;
    }

    public void Rotate()
    {
        currentState = State.Rotate;
    }
}
