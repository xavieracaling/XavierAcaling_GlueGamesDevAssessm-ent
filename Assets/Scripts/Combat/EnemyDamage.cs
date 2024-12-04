using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int Damage = 10;                
    public float DamageCooldown = 1.0f; 
    private Coroutine damageCoroutine;    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine == null)    
            {
                damageCoroutine = StartCoroutine(DamagePlayer(other.gameObject.GetComponent<IDamagable>(),other.gameObject.transform));
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

    private IEnumerator DamagePlayer(IDamagable player, Transform playerPos)
    {
        while(true)
        {
            player.TakeDamage(Damage, ()=> {
                EffectManager.Instance.PlayPlayerHitEffect(playerPos.position);
            });  
            yield return new WaitForSeconds(DamageCooldown); 
        }
    }
}
