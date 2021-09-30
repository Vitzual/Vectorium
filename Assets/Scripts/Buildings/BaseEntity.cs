using UnityEngine;
using Mirror;
using System.Collections.Generic;


public class BaseEntity : NetworkBehaviour, IDamageable
{
    // IDamageable interface variables
    public float health { get; set; }
    public float maxHealth { get; set; }

    protected ParticleSystem particle;
    [HideInInspector] public Material material;

    public virtual void Setup()
    {
        Debug.LogError("This object has a BaseEntity script attached to it!\n" +
            "Please use a default script that inherits from BaseEntity instead.");
    }

    // Damages the entity (IDamageable interface method)
    public virtual void DamageEntity(float dmg)
    {
        health -= dmg;

        if (health <= 0)
            DestroyEntity();
    }

    // Destroys the entity (IDamageable interface method)
    public virtual void DestroyEntity()
    {
        if (particle != null)
            Instantiate(particle, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    // Heals the entity (IDamageable interface method)
    public virtual void HealEntity(float amount)
    {
        health += amount;
        if (health > maxHealth) health = maxHealth;
    }
}
