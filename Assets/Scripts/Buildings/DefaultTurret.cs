using System.Collections.Generic;
using UnityEngine;

[HideInInspector]
public class DefaultTurret : DefaultBuilding, IAudible
{
    // IAudible interface variables
    public AudioClip sound { get; set; }

    // Barrel thing
    public ActiveTurret barrel;

    // Sets stats and registers itself under the turret handler
    public void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();

        if (collider != null)
            collider.radius = barrel.turret.range;
        else Debug.LogError("Turret does not have a circle collider!");

        Events.active.TurretPlaced(this, barrel);
        SetStats();
    }

    // IAudible sound method
    public void PlaySound()
    {
        float audioScale = CameraScroll.getZoom() / 1400f;
        AudioSource.PlayClipAtPoint(sound, gameObject.transform.position, Settings.soundVolume - audioScale);
    }
}
