using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter
{
    [Header("Character Settings")]
    public float Health = 100f;
    public float MoveSpeed = 5f;

    public virtual void Move(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, direction.y, 0).normalized;
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }
    
    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    public abstract void Attack();

    public virtual void Die()
    {
        Debug.Log($"{name} has died.");
        Destroy(gameObject);
    }
}