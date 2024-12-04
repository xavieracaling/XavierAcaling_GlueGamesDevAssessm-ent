using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, ICharacter
{
    [Header("Character Settings")]
    public float Health = 100f;
    public float MoveSpeed = 5f;
    public bool Dead;
    public Animator _Animator;
    public ProgressBar HealthBar;
    void Awake ()
    {
        _Animator = GetComponent<Animator>();
    }
    public virtual void Move(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, direction.y, 0).normalized;
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }
    
    public virtual void TakeDamage(float damage, Action hitFX)
    {
        if(Dead) return;
        if(hitFX != null)
            hitFX?.Invoke();
        Health -= damage;
        HealthBar.BarValue = Health;
        if (Health <= 0)
        {
            Die(_Animator);
        }
    }

    public abstract void Attack();

    public abstract void Die(Animator anim);
    
}