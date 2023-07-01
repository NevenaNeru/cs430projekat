using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 6.5f;
    [SerializeField] private GameInput input;
    [SerializeField] private HealthBar healthBar;

    private bool isFacingRight = true;

    public int currentHealth;
    private int maxHealth;

    private void Start()
    {
        maxHealth = 500;
        currentHealth = maxHealth;
    }
    private void Update()
    {
        Vector2 inputVector = input.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y);
        transform.position += moveDirection * speed * Time.deltaTime;

        if (isFacingRight && inputVector.x < 0f)
        {
            Flip();
        } else if(!isFacingRight && inputVector.x > 0f) {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0) 
        {
            //Debug.Log("dead");
        }
    }

}
