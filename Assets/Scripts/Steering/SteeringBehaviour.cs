using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    public float maxSpeed = 4f;
    public float maxAcceleration = 7f;
    public float speed = 2f;
    public float panicDistance = 3f;
    public float targetTime = 0.1f;
    public float targetRadius = 0.1f;
    public float slowdonRadisu = 1f;

    public float wanderOffset = 2f;
    public float wanderRadius = 5f;
    public float wanderRate = 1f;
    public float wanderJitter = 20f;

    public float maxPrediction = 2f;

    Rigidbody2D rb2D;
    Vector2 wanderTarget;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        float value = Random.value * 2 * Mathf.PI;
        wanderTarget = new Vector2(wanderRadius * Mathf.Cos(value), wanderRadius * Mathf.Sin(value));
    }

    public void Steer(Vector2 acceleration)
    {
        rb2D.velocity += acceleration * Time.deltaTime;
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
    }

    public Vector2 Seek(Vector2 targetPos)
    {
        Vector2 velocity = targetPos - new Vector2(transform.position.x, transform.position.y);
        velocity *= maxAcceleration;
        return velocity;
    }

    public Vector2 Flee(Vector2 target)
    {
        Vector2 acceleration = new Vector2(transform.position.x, transform.position.y) - target;

        if (acceleration.magnitude > panicDistance)
        {
            if (rb2D.velocity.magnitude > 0.1f)
            {
                acceleration = -rb2D.velocity / targetTime;

                if (acceleration.magnitude > maxAcceleration)
                {

                    acceleration.Normalize();
                    acceleration *= maxAcceleration;

                }
                return acceleration;
            }
            else
            {
                rb2D.velocity = Vector2.zero;
                return Vector2.zero;
            }
        }
        acceleration.Normalize();
        acceleration *= maxAcceleration;
        return acceleration;

    }

    public Vector2 Arrive(Vector2 target)
    {
        Vector2 targetVelocity = new Vector2(target.x - transform.position.x, target.y - transform.position.y);

        if (targetVelocity.magnitude < targetRadius)
        {
            rb2D.velocity = Vector2.zero;
            return Vector2.zero;
        }

        float targetSpeed = 0;

        if (targetVelocity.magnitude > slowdonRadisu)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * (targetVelocity.magnitude / slowdonRadisu);
        }

        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector2 acceleration = targetVelocity - rb2D.velocity;
        acceleration *= 1 / targetTime;

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= maxAcceleration;
        }

        return acceleration;
    }

    public Vector2 Wander()
    {
        float jitter = wanderJitter * Time.deltaTime;
        wanderTarget = new Vector3(Random.Range(-1, 1) * jitter, Random.Range(-1, 1) * jitter, 0);

        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y) + 
            new Vector2(transform.right.x, transform.right.y) * wanderOffset + wanderTarget;

        return Seek(targetPos);
    }

    public Vector2 Pursuit(Rigidbody2D target)
    {
        Vector2 displacment = target.transform.position - transform.position;

        float distance = displacment.magnitude;

        float speed = rb2D.velocity.magnitude;

        float predection = 0;
        if (speed <= distance / maxPrediction)
        {
            predection = maxPrediction;
        }
        else
        {
            predection = distance / speed;
        }

        Vector2 victim = new Vector2(target.transform.position.x, target.transform.position.y) + target.velocity * predection;
        return Seek(victim);
    }

    public Vector2 Evade(Rigidbody2D target)
    {
        Vector2 displacment = target.transform.position - transform.position;
        float distance = displacment.magnitude;

        float speed = target.velocity.magnitude;

        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
            prediction *= 0.9f;
        }

        Vector2 killer = new Vector2(target.transform.position.x, target.transform.position.y) + target.velocity * prediction;
        return Flee(killer);
    }

    public Vector2 PathFollow(Path path)
    {
        float distance = Vector2.Distance(new Vector2(path.pathPoint[path.currentPoint].pos.x, 
            path.pathPoint[path.currentPoint].pos.y), transform.position);
        Vector2 acc = Seek(new Vector2(path.pathPoint[path.currentPoint].pos.x, 
            path.pathPoint[path.currentPoint].pos.y));

        if (distance < path.distance && path.currentPoint < path.pathPoint.Count)
        {
            path.currentPoint++;
        }
        if (path.currentPoint >= path.pathPoint.Count)
        {
            path.currentPoint = path.pathPoint.Count - 1;

        }
        return acc;
    }
}
