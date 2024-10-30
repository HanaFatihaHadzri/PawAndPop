using System.Collections;
using UnityEngine;
using Pathfinding; // import pathfinding namespace

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public Transform targetObject;
    public Animator _animator;
    public float minDistance = 1f;

    private Rigidbody2D rb;

    public float waitTime = 2f;

    //A* declaration
    private AIDestinationSetter aIDestinationSetter; // reference to AIDestinatioSetter script
    private IAstarAI ai; //reference to A* Pathfinding AI


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
    }
    void FindRandomCat()
    {
        if (targetObject == null)// if the cat is destroyed
        {
            targetObject = GameObject.FindWithTag("TargetObject")?.GetComponent<TargetObjectSpawner>()?.GetRandTargetObject()?.transform;

            if (targetObject == null) // if noy found cat in scene
            {
                //patrol
                PatrolEnemy patrolEnemy = gameObject.GetComponent<PatrolEnemy>();
                PatrolManager patrolManager = FindAnyObjectByType<PatrolManager>();

                if(patrolManager != null && patrolEnemy != null)
                {
                    patrolEnemy.SetPatrolPoints(patrolManager.patrolPoints);
                }
            }
            else
            {
                aIDestinationSetter.target = targetObject; //set new target for AIDestinationSetter
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindRandomCat();
        HandleMovementAndAnimation();
    }

    void HandleMovementAndAnimation()
    {
        //check if ai is close enough to target to stop move
        if (targetObject != null && ai != null)
        {
            float distance = Vector2.Distance(transform.position, targetObject.position);

            if (distance == minDistance)
            {
                ai.destination = transform.position; //stop ai movement when close to target
                _animator.SetFloat("Horizontal", 0); //stop move animation
            }
            else
            {
                _animator.SetFloat("Horizontal", Mathf.Abs(ai.velocity.x)); //animate movement based on velocity
            }

            // Check if horizontal movement is above a small threshold to flip the sprite
            float velocityThreshold = 0.1f; // small value to prevent flipping when almost stopped

            //flip sprite based on horizontal movement direction
            if (Mathf.Abs(ai.velocity.x) > velocityThreshold)
            {
                transform.localScale = new Vector3(Mathf.Sign(ai.velocity.x) *
                    Mathf.Abs(transform.localScale.x), transform.localScale.y,
                    transform.localScale.z);
            }
            else if(ai.velocity.x == 0)
            {
                _animator.SetFloat("Horizontal", 0); //stop move animation
            }
        }
    }
}