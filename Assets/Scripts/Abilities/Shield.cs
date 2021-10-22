using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static bool active = true;
    private SpriteRenderer _SpriteRenderer;
    public AudioClip ShieldSound;
    public AudioSource AudioSource;

    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _SpriteRenderer.enabled = active;
    }

    // Knockback works on things tagges as enemy
    // But not for the enemies we have
    // Might be because of the AI? It immediately moves it after knockback
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && active)
        {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            enemy.AddForce((enemy.position - (Vector2)transform.position).normalized * 50, ForceMode2D.Impulse);
            active = false;
            if (AudioSource != null) AudioSource.PlayOneShot(ShieldSound);
        }
    }
}
