using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static Events active;

    // Start is called before the first frame update
    public void Awake()
    {
        active = this;
    }

    // On blueprint collected
    public event Action<Buildable> onBuildingUnlocked;
    public void BuildingUnlocked(Buildable buildable)
    {
        if (onBuildingUnlocked != null)
            onBuildingUnlocked(buildable);
    }

    // On blueprint collected
    public event Action<Enemy> onEnemyDiscovered;
    public void EnemyDiscovered(Enemy enemy)
    {
        if (onEnemyDiscovered != null)
            onEnemyDiscovered(enemy);
    }

    // On blueprint collected
    public event Action<CollectedBlueprint> onBlueprintCollected;
    public void BlueprintCollected(CollectedBlueprint blueprint)
    {
        if (onBlueprintCollected != null)
            onBlueprintCollected(blueprint);
    }

    // On enemy destroyed
    public event Action<BaseEntity> onBuildingHurt;
    public void BuildingHurt(BaseEntity tile)
    {
        if (onBuildingHurt != null)
            onBuildingHurt(tile);
    }

    // On enemy destroyed
    public event Action<BaseEntity> onEnemyHurt;
    public void EnemyHurt(BaseEntity enemy)
    {
        if (onEnemyHurt != null)
            onEnemyHurt(enemy);
    }

    // On enemy destroyed
    public event Action<BaseTile> onBuildingDestroyed;
    public void BuildingDestroyed(BaseTile tile)
    {
        if (onBuildingDestroyed != null)
            onBuildingDestroyed(tile);
    }

    // On enemy destroyed
    public event Action<DefaultEnemy> onEnemyDestroyed;
    public void EnemyDestroyed(DefaultEnemy enemy)
    {
        if (onEnemyDestroyed != null)
            onEnemyDestroyed(enemy);
    }

    // On guardian destroyed
    public event Action<DefaultGuardian> onGuardianSpawned;
    public void GuardianSpawned(DefaultGuardian guardian)
    {
        if (onGuardianSpawned != null)
            onGuardianSpawned(guardian);
    }

    // On guardian destroyed
    public event Action<DefaultGuardian> onGuardianDestroyed;
    public void GuardianDestroyed(DefaultGuardian guardian)
    {
        if (onGuardianDestroyed != null)
            onGuardianDestroyed(guardian);
    }

    // Fires the hub laser
    public event Action fireHubLaser;
    public void FireHubLaser()
    {
        if (fireHubLaser != null)
            fireHubLaser();
    }

    // Invoked when a bullet is fired
    public event Action<DefaultBullet, BaseEntity> onBulletFired;
    public void BulletFired(DefaultBullet bullet, BaseEntity target)
    {
        if (onBulletFired != null)
            onBulletFired(bullet, target);
    }

    // Invoked when a building with a rotating piece is placed
    public event Action<Rotator> onRotatorPlaced;
    public void RotatorPlaced(Rotator rotator)
    {
        if (onRotatorPlaced != null)
            onRotatorPlaced(rotator);
    }

    // Invoked when a building with a collector script is placed
    public event Action<DefaultCollector> onCollectorPlaced;
    public void CollectorPlaced(DefaultCollector collector)
    {
        if (onCollectorPlaced != null)
            onCollectorPlaced(collector);
    }

    // Invoked when a building with a storage script is placed
    public event Action<DefaultStorage> onStoragePlaced;
    public void StoragePlaced(DefaultStorage storage)
    {
        if (onStoragePlaced != null)
            onStoragePlaced(storage);
    }

    // Invoked when a building with a turret script is placed
    public event Action<DefaultTurret> onTurretRegistered;
    public void RegisterTurret(DefaultTurret turret)
    {
        if (onTurretRegistered != null)
            onTurretRegistered(turret);
    }

    // Invoked when a survival save is loaded
    public event Action<SurvivalData> onSurvivalLoaded;
    public void SurvivalLoaded(SurvivalData data)
    {
        if (onSurvivalLoaded != null)
            onSurvivalLoaded(data);
    }

    // Invoked when a hotbar is set
    public event Action<Tile, int> onHotbarSet;
    public void HotbarSet(Tile tile, int slot)
    {
        if (onHotbarSet != null)
            onHotbarSet(tile, slot);
    }

    // Initializes variants
    public event Action<Variant> onVariantLoaded;
    public void VariantLoaded(Variant variant)
    {
        if (onVariantLoaded != null)
            onVariantLoaded(variant);
    }
}