using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PolicemanAI : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentPointId;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetPos(0);
    }

    private void Update()
    {
        if (Vector3.Distance(points[currentPointId].position, transform.position) < 0.3f)
            NextPos();

        animator.SetFloat("Speed", navMeshAgent.speed);
    }

    private void NextPos()
    {
        int nextPosId = currentPointId + 1;

        if (nextPosId >= points.Length)
            nextPosId = 0;

        if (nextPosId == 2 || nextPosId == 0)
            StartCoroutine(SetSpeed(maxSpeed));
        else
            StartCoroutine(SetSpeed(minSpeed));

        SetPos(nextPosId);
    }

    private void SetPos(int pointId)
    {
        currentPointId = pointId;
        navMeshAgent.SetDestination(points[currentPointId].position);
    }

    private IEnumerator SetSpeed(float newSpeed)
    {
        float currentSpeed = navMeshAgent.speed;
        float waitTimeLerp = 1f;
        float elapsedTimeLerp = 0;

        while (elapsedTimeLerp < waitTimeLerp)
        {
            navMeshAgent.speed = Mathf.Lerp(currentSpeed, newSpeed, elapsedTimeLerp / waitTimeLerp);
            elapsedTimeLerp += Time.deltaTime;
            yield return null;
        }    
    }
}
