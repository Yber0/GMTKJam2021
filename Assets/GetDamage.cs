using System;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public ParticleSystem _particleSystem;
    public SpriteRenderer player;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Die animation
        _particleSystem.Emit(30);

        player.enabled = false;
        
        // Reload current scene
        SceneLoader.instance.ReloadScene();
    }
}
