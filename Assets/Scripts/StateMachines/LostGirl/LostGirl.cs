using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LostGirl : MonoBehaviour
{
    public LostGirlState currentState;
    public LostGirlFinalStateMachine states;
    public SteeringBehaviour behaviour;

    public Grid grid;
    public Path path;

    public Player player;
    public GameObject house;

    public bool isPlayerInSight;
    public float playerSightRadius;

    public bool isHouseInSight;
    public float houseSightRadius;

    public bool houseCollision;

    private void Awake()
    {
        isPlayerInSight = false;
        isHouseInSight = false;
        houseCollision = false;

        behaviour = GetComponent<SteeringBehaviour>();
        states = new LostGirlFinalStateMachine(this);
        currentState = states.WaitState();
        currentState.Enter();
    }

    private void OnDrawGizmos() { 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerSightRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, houseSightRadius);
    }


    private void Update()
    {
        currentState.Execute();

        float distancePlayer = Vector3.Distance(player.transform.position, this.transform.position);
        float distanceHouse = Vector3.Distance(house.transform.position, this.transform.position);

        if (distancePlayer <= playerSightRadius)
        {
            isPlayerInSight = true;
        }
        else
        {
            isPlayerInSight = false;
        }

        if (distanceHouse <= houseSightRadius)
        {
            isHouseInSight = true;
        }
        else
        {
            isHouseInSight = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "House")
        {
            houseCollision = true;
        }
    }

}
