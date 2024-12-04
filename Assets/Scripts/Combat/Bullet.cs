using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 300;      
    private Vector2 direction;      

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        RotateBullet(direction);
        Destroy(gameObject, 5f); 
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
            Destroy(gameObject);           
        }
    }
}