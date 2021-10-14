using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool active = true;
    private SpriteRenderer _SpriteRenderer;

    void Start(){
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _SpriteRenderer.enabled = active;
    }

    // Knockback works on things tagges as enemy
    // But not for the enemies we have
    // Might be because of the AI?
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && active)
        {   
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            enemy.AddForce(new Vector2(-enemy.velocity.x + 1, Mathf.Abs(enemy.velocity.y) + 1), ForceMode2D.Impulse);
            active = false;
        }
    }
}
