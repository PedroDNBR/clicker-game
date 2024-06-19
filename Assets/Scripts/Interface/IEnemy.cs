using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public int GetHealth();
    public void SetHealth(int health);
    public void Heal(int heal);

    public void TakeDamage(int damage);
}
