using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    enum State { Move, Stay, Rotate }
    State currentState = State.Move;

    private Vector3 screenPoint;
    private Vector3 offset;

    // 이 함수는 현재 GameObject가 다른 콜라이더에 처음 부딪힐 때 호출됩니다.
    void OnCollisionEnter(Collision collision)
    {
        if (currentState == State.Move || currentState == State.Rotate)
        {
        // "안되요" 메시지를 출력합니다.
        Debug.Log("안되요");
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
