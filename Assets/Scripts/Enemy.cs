using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{this.name} I'm hit by {other.gameObject.name}");
        Destroy(gameObject);
    }
}
