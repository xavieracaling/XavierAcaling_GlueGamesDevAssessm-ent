using UnityEngine;
using System.Collections;
using System;

public class Player : CharacterBase
{
    public Joystick MovementJoystick;
    public Joystick AttackJoystick;
    public float FireRate = 0.5f;  

    private Coroutine shootingCoroutine; 

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if(Dead) return;
        Vector2 moveInput = new Vector2(MovementJoystick.Horizontal, MovementJoystick.Vertical);
        Move(moveInput);
    }

    private void HandleAttack()
    {
        if (AttackJoystick.Horizontal != 0 || AttackJoystick.Vertical != 0)
        {
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootContinuously());
            }
        }
        else
        {
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    private IEnumerator ShootContinuously()
    {
        while (true)
        {
            Attack();
            yield return new WaitForSeconds(FireRate);
        }
    }
    public override void Die(Animator anim)
    {
        Dead = true;
        EffectManager.Instance.PlayDeadEffect(transform.position);
        Debug.Log($"{name} has died.");
        _Animator.SetTrigger("Die");
        StartCoroutine(iDie());
    }
    IEnumerator iDie()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.Result(false);
        Destroy(gameObject);
    }
    public override void Attack()
    {
        Vector2 attackDirection = new Vector2(AttackJoystick.Horizontal, AttackJoystick.Vertical).normalized;
        GameManager.Instance.GetAvailableBullet(attackDirection);
    }

    
}
