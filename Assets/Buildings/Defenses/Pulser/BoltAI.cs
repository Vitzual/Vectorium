﻿using UnityEngine;

public class BoltAI : TurretClass
{
    // Targetting system
    void Update()
    {
        // If animation is playing, wait
        if (animPlaying)
        {
            PlayAnim();
            return;
        }

        if (isRotating)
            RotateTowardNearestEnemy();

        // If a target exists, shoot at it
        if (target != null && !isRotating)
        {
            // Unflag hasTarget
            hasTarget = false;
                
            // Call shoot function
            Shoot(Bullet, FirePoints[0]);
        } else {
            // Unflag hasTarget when target is null
            hasTarget = false;
        }
    }

    // Kill defense
    public override void DestroyTile()
    {
        Survival srv = GameObject.Find("Survival").GetComponent<Survival>();
        srv.decreasePowerConsumption(power);
        TurretHandler.buildings.Remove(transform);
        GameObject.Find("Spawner").GetComponent<WaveSpawner>().decreaseHeat(heat);
        Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
