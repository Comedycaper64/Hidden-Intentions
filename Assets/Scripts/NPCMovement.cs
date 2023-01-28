using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private GameObject variousMovePoints;
    private Transform currentMovePoint;
    private int nextMovePoint;
    public bool shouldMove = false;
    private Animator animator;
    [SerializeField] private float movingTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentMovePoint = movePoints[0];
        nextMovePoint = 1;
        for (int i = 0; i < movePoints.Length; i++)
        {
            movePoints[i].SetParent(variousMovePoints.transform);
        }
        StartCoroutine(MoveAround(movingTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentMovePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, currentMovePoint.position) <= .05f)
            {
                currentMovePoint = movePoints[nextMovePoint];
                if (nextMovePoint < movePoints.Length - 1)
                {
                    nextMovePoint++;
                }
                else
                {
                    nextMovePoint = 0;
                }
                shouldMove = false;   
            }


            if ((currentMovePoint.position.x - transform.position.x) > 0.1f)
            {
                animator.SetFloat("Horizontal", 1f);
            }
            else if ((currentMovePoint.position.x - transform.position.x) < -0.1f)
            {
                animator.SetFloat("Horizontal", -1f);
            }
            else
            {
                animator.SetFloat("Horizontal", 0);
            }

            if ((currentMovePoint.position.y - transform.position.y) > 0.1f)
            {
                animator.SetFloat("Vertical", 1f);
            }
            else if ((currentMovePoint.position.y - transform.position.y) < -0.1f)
            {
                animator.SetFloat("Vertical", -1f);
            }
            else
            {
                animator.SetFloat("Vertical", 0);
            }

            animator.SetFloat("speed", 1f);
        }
        else
        {
            animator.SetFloat("speed", 0f);
        }
    }

    private IEnumerator MoveAround(float timeBetweenMoving)
    {
        yield return new WaitForSeconds(timeBetweenMoving);
        shouldMove = true;
        StartCoroutine(MoveAround(timeBetweenMoving));
    }
}
