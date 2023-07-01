using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IReciver
{
    public SlimeState currentState;
    public SlimeFinalState states;
    public SteeringBehaviour behavior;

    public Player player;
    public Grid grid;
    public Path path;
    public GameObject water;
    public Wyrm wyrm;

    public bool isAttacking;
    public float enemySightRadius;

    public bool isPlayerInSight;
    public float playerSightRadius;

    public bool isWaterInSight;

    public int myReciverId;

    private void Awake()
    {
        EntityManager.RegisterEntity(myReciverId, this);

        isPlayerInSight = false;
        isWaterInSight = false;
        behavior = GetComponent<SteeringBehaviour>();
        states = new SlimeFinalState(this);
        currentState = states.WanderState();
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Execute();

        float distance = Vector3.Distance(player.transform.position, this.transform.position);

        if (distance <= playerSightRadius)
        {
            isPlayerInSight = true;
        }
        else
        {
            isPlayerInSight = false;
        }

        if (distance <= enemySightRadius)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemySightRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, playerSightRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            water = collision.gameObject;
            isWaterInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            isWaterInSight = false;
        }
    }

    public void HandleMessage(Message msg)
    {
        StartCoroutine(currentState.OnMessage(msg));
    }
}
