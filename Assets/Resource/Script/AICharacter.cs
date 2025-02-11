using System.Collections;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    private enum State 
    { 
    Thinking,
    Thirsty,
    Hungry,
    Bored,
    Tired
    }
    private State currentState = State.Thinking;

    private float increaseInterval = 4f;
    private float thirstTimer = 10f;
    private float hungerTimer = 9f;
    private float boredomTimer = 8f;
    private float fatigueTimer = 7f;

    private float timeSinceLastChange = 0f;
    private UnityEngine.AI.NavMeshAgent agent; 
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(StateManagement());
    }

    IEnumerator StateManagement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeSinceLastChange++;
            Debug.Log("Current State: " + currentState + ", Time since last state change: " + timeSinceLastChange);

            if (currentState == State.Thinking)
            {
                if (timeSinceLastChange >= thirstTimer) SetState(State.Thirsty);
                else if (timeSinceLastChange >= hungerTimer) SetState(State.Hungry);
                else if (timeSinceLastChange >= boredomTimer) SetState(State.Bored);
                else if (timeSinceLastChange >= fatigueTimer) SetState(State.Tired);
            }
        }
    }

    void SetState(State newState)
    {
        if (currentState != State.Thinking) return;
        currentState = newState;
        timeSinceLastChange = 0;
        Debug.Log("Current State: " + currentState);
        MoveToTarget();
    }

    void MoveToTarget()
    {
        string tag = "";
        switch (currentState)
        {
            case State.Thirsty:
                tag = "Water";
                break;
            case State.Hungry:
                tag = "Food";
                break;
            case State.Bored:
                tag = "Entertainment";
                break;
            case State.Tired:
                tag = "Bed";
                break;
        }
        Debug.Log("Moving towards: " + tag);
        GameObject target = FindClosestWithTag(tag);
        if (target)
        {
            Debug.Log("Target found: " + target.name);
            MoveUsingNavMesh(target.transform.position);
        }

    }

    private void MoveUsingNavMesh(Vector3 targetPosition)
    {
        if (agent != null)
        {
            agent.SetDestination(targetPosition); //이동하라는 명령
        }
    }


    GameObject FindClosestWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(currentPosition, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = obj;
            }
        }
        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bed"))
        {
            Debug.Log("왘!");
        }
        if ((currentState == State.Thirsty && other.CompareTag("Water")) ||
            (currentState == State.Hungry && other.CompareTag("Food")) ||
            (currentState == State.Bored && other.CompareTag("Entertainment")) ||
            (currentState == State.Tired && other.CompareTag("Bed")))
        {
            Debug.Log("Reached: " + other.tag + ". Waiting 3 seconds before switching to Thinking state.");
            StartCoroutine(WaitAndSwitchToThinking());
        }
    }

    IEnumerator WaitAndSwitchToThinking()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("State before switching: " + currentState);

        if (currentState == State.Thirsty) thirstTimer += increaseInterval;
        if (currentState == State.Hungry) hungerTimer += increaseInterval;
        if (currentState == State.Bored) boredomTimer += increaseInterval;
        if (currentState == State.Tired) fatigueTimer += increaseInterval;

        currentState = State.Thinking;
        timeSinceLastChange = 0;
        Debug.Log("Switched to Thinking state.");
    }

}
