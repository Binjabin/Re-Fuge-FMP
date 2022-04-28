using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] float blasterDamage;
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<HealthBar>().Damage(blasterDamage);
        }
    }
}
