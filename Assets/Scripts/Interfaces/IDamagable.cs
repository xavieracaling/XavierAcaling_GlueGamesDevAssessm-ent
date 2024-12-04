using System;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage, Action hitFX);
    void Die(Animator anim);
}