using UnityEngine;

public interface ICharacter : IDamagable, IMovable
{
   void Attack();
}