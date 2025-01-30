using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform rotationPoint;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float angle = 0f;

    private Transform target;

    public void SetTarget(Transform target) {
        this.target = target;
    }

    private void FixedUpdate() {
        if (!target) {
            Destroy(gameObject);
        } else {
            if (rotationPoint != null) {
                RotateTowardsTarget();
                MoveInDirection();
            } else {
                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + this.angle;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = targetRotation;
    }

    private void MoveInDirection() {
        Vector2 direction = rotationPoint.up.normalized; // Esto usa la dirección de la rotación
        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.transform == target) {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if (collision.gameObject.transform == target) {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
