using UnityEngine;
using System.Collections;
using System;

public class Enemy : CharacterBase
{
    public int Damage = 10;                
    public float DamageCooldown = 1.0f; 
    public Transform Player;              
    public float StopDistance = 1f; 
    public GameObject DamageGObj;
    private void Start()
    {
        MoveSpeed = 1f;
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").transform;  
        }
    }

    private void Update()
    {
        MoveTowardPlayer();
    }
    public override void Die(Animator anim)
    {
        GameManager.Instance.UpdateKillCount();
        EffectManager.Instance.PlayDeadEffect(transform.position);
        Dead = true;
        Debug.Log($"{name} has died.");
        _Animator.SetTrigger("Die");
        StartCoroutine(iDie());
    }
    IEnumerator iDie()
    {
        Destroy(DamageGObj);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public override void Attack()
    {
    }
    private void MoveTowardPlayer()
    {
        if(Dead || GameManager.Instance.EndGame) return;

        if (Player != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;

            float distanceToPlayer = Vector2.Distance(transform.position, Player.position);

            if (distanceToPlayer > StopDistance)
                transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
            transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,0);
        }
    }
}

