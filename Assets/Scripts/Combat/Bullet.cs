using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 300;      
    private Vector2 direction;      

    Coroutine Cdisappear;

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        RotateBullet(direction);
        if(Cdisappear != null)
            StopCoroutine(Cdisappear);
        Cdisappear = StartCoroutine(disappear());
    }
    IEnumerator disappear()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }

    private void RotateBullet(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EffectManager.Instance.PlayBulletEffect(transform.position);
            collision.transform.parent.GetComponent<IDamagable>().TakeDamage(Random.Range(8,10),null);
            if(Cdisappear != null)
                StopCoroutine(Cdisappear);
            gameObject.SetActive(false);
        }
    }
}