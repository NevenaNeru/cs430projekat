using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public ChickenState currentState;
    public ChickenFinalStateMachine states;
    public SteeringBehaviour behavior;

    public Grid grid;
    public Path path;

    public Slime slime1;
    public Slime slime2;
    public Slime slime3;
    public Wyrm wyrm1;

    public bool isEnemyInSight;
    public float enemySightRadius;

    public GameObject house;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemySightRadius);
    }

    private void Awake()
    {
        isEnemyInSight = false;
        behavior = GetComponent<SteeringBehaviour>();
        states = new ChickenFinalStateMachine(this);
        currentState = states.WanderState();
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Execute();

        float distanceSlime1 = Vector3.Distance(slime1.transform.position, this.transform.position);
        float distanceSlime2 = Vector3.Distance(slime2.transform.position, this.transform.position);
        float distanceSlime3 = Vector3.Distance(slime3.transform.position, this.transform.position);
        float distanceWyrm1 = Vector3.Distance(wyrm1.transform.position, this.transform.position);

        if (distanceSlime1 <= enemySightRadius || distanceSlime2 <= enemySightRadius
            || distanceSlime3 <= enemySightRadius || distanceWyrm1 <= enemySightRadius)
        {
            isEnemyInSight = true;
        }
        else
        {
            isEnemyInSight = false;
        }
    }
}
