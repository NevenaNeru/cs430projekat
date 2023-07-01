using UnityEngine;

public class Wyrm : MonoBehaviour, IReciver
{
    public WyrmStates currentState;
    public WyrmFinalState states;
    public SteeringBehaviour behaviour;

    public Player player;
    public Grid grid;
    public Path path;

    public bool isAttacking;
    public float wyrmSightRadius;
    public bool isPlayerInSight;
    public float playerSightRadius;

    public int mySenderId;

    public Slime slime1;
    public Slime slime2;
    public Slime slime3;

    private void Start()
    {
        currentState = states.PathFollowState();
        currentState.Enter();
    }

    void Awake()
    {
        EntityManager.RegisterEntity(mySenderId, this);
        behaviour = GetComponent<SteeringBehaviour>();
        states = new WyrmFinalState(this);
   

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

        if (distance <= wyrmSightRadius)
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
        Gizmos.DrawWireSphere(transform.position, wyrmSightRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, playerSightRadius);
    }

    public void HandleMessage(Message msg)
    {
        StartCoroutine(currentState.OnMessage(msg));
    }
}
