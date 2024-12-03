using UnityEngine;

public class Player : CharacterBase
{
    public Joystick MovementJoystick;  
    public Joystick AttackJoystick;   
    public GameObject BulletPrefab;    
    public Transform BulletSpawnPoint; 

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        Vector2 moveInput = new Vector2(MovementJoystick.Horizontal, MovementJoystick.Vertical);
        if (moveInput.magnitude > 0.1f)
        {
            Move(moveInput);
        }
    }

    private void HandleAttack()
    {
        if (AttackJoystick.Horizontal != 0 || AttackJoystick.Vertical != 0)
        {
            Attack();
        }
    }

    public override void Attack()
    {
        Vector2 attackDirection = new Vector2(AttackJoystick.Horizontal, AttackJoystick.Vertical).normalized;
        ShootBullet(attackDirection);
    }

    private void ShootBullet(Vector2 direction)
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity);

        // bullet.GetComponent<Bullet>().Initialize(direction);

        Debug.Log($"Bullet shot in direction: {direction}");
    }
}
