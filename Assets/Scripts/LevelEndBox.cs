using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelEndBox : MonoBehaviour
{
    public Collider collider;
    public float Bound => collider.bounds.size.x;

    public float health;
    public TextMeshProUGUI healthText;

    private void OnValidate()
    {
        healthText.text = ((int)health).ToString();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthText.text = ((int)health).ToString();
        if (health<=0)
        {
            EventManager.LevelEndBoxDestroyed(this);
            gameObject.SetActive(false);
        }
    }
}
