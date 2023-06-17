using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndBox : MonoBehaviour
{
    public Collider collider;
    public float Bound => collider.bounds.size.x;

    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health<=0)
        {
            EventManager.LevelEndBoxDestroyed(this);
            Destroy(gameObject);
        }
    }
}
