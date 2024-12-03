using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int Damage = 10;                
    public float DamageCooldown = 1.0f; 
    public float MoveSpeed = 3f;          
    public Transform Player;              

    private bool isPlayerInRange = false;  
    private Coroutine damageCoroutine;    

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player").transform;  
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            MoveTowardPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
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
            isPlayerInRange = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;   
            }
        }
    }

    private IEnumerator DamagePlayer(IDamagable player)
    {
        while (isPlayerInRange)
        {
            player.TakeDamage(Damage);  
            yield return new WaitForSeconds(DamageCooldown);     
        }
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

