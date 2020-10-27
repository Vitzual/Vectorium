﻿using UnityEngine;

public class ShotgunAI : TurretClass
{
    public int level = 1;
    public int cost = 10;

    // On start, assign weapon variables
    void Start()
    {
        fireRate = .5f;
        bulletForce = 50f;
        bulletSpread = .45f;
        bulletAmount = 8;
        rotationSpeed = .5f;
        range = 25;
        health = 8;
    }

    // Targetting system
    void Update()
    {
        RotateTowardNearestEnemy();

        // If a target exists, shoot at it
        if (target != null)
        {            
            // If turret is pointing at target, fire at it
            if ((gunRotation - enemyAngle) <= 1 && (gunRotation - enemyAngle) >= -1)
            {
                // Unflag hasTarget
                hasTarget = false;
                
                // Call shoot function
                Shoot(Bullet, Point);
            }
        } else {
            // Unflag hasTarget when target is null
            hasTarget = false;
        }
    }

    // Kill defense
    public override void DestroyTile()
    {
        Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override int GetCost()
    {
        return cost;
    }

    public override int GetLevel()
    {
        return level;
    }
}
