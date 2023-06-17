using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    
    public Collider collider;
    public float Bound => collider.bounds.size.x;

    public int damage;
    
}
