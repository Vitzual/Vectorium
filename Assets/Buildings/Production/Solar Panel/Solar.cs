﻿using UnityEngine;

public class Solar : TileClass
{
    public void Start()
    {
        GameObject.Find("Survival").GetComponent<Survival>().increaseAvailablePower(250);
    }

    // Kill defense
    public override void DestroyTile()
    {
        GameObject.Find("Survival").GetComponent<Survival>().decreaseAvailablePower(250);
        Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
