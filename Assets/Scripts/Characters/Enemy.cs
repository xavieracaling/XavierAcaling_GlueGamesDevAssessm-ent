using UnityEngine;
using System.Collections;

public class Enemy : CharacterBase
{
    public int Damage = 10;                
    public float DamageCooldown = 1.0f; 
    public Transform Player;              
    
    private Coroutine damageCoroutine;    

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine == null)    
            {
                damageCoroutine = StartCoroutine(DamagePlayer(other.gameObject.GetComponent<IDamagable>()));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;   
            }
        }
    }

    private IEnumerator DamagePlayer(IDamagable player)
    {
        while(true)
        {
            player.TakeDamage(Damage);  
            yield return new WaitForSeconds(DamageCooldown); 
        }
            
    }
    public override void Attack()
    {
    }
    private void MoveTowardPlayer()
    {
        if (Player != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;

            transform.position = Vector2.MoveTowards(transform.position, Player.position, MoveSpeed * Time.deltaTime);
        }
    }
}

