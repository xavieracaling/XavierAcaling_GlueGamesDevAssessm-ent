using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter
{
    [Header("Character Settings")]
    public float Health = 100f;
    public float MoveSpeed = 5f;
    public Animator _Animator;
    void Awake ()
    {
        _Animator = GetComponent<Animator>();
    }
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
            Die(_Animator);
        }
    }

    public abstract void Attack();

    public abstract void Die(Animator anim);
    
}