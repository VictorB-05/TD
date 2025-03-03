using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Turret : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float hps = 1f; // hits per second
    [SerializeField] private float angle = 0f;

    private Transform target;
    private float timeUntilFire;
    private void Update() {
        if (target == null) {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange()) {
            target = null;
        }else {

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / hps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab,firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(
        transform.position, // El punto desde donde se lanza el "círculo".
        targetingRange,     // El radio del círculo que define el rango de detección.
        (Vector2)transform.position, // La dirección del lanzamiento.
        0f,                 // La distancia del círculo proyectado (en este caso, 0 significa que el círculo no se mueve).
        enemyMask           // La capa (layer mask) que filtra qué objetos detectar.
        );

        if (hits.Length > 0) { // Si hay al menos un objeto detectado...
            target = hits[FindEndTarget(hits)].transform; // Guardamos el transform del primer objeto detectado como el objetivo.
        }
    }

    private int FindEndTarget(RaycastHit2D[] hits) {
        int mayor = 0;
        int result = 0;
        for (int i = 0; i < hits.Length; i++) {
            int aux = hits[i].transform.GetComponent<EnemyMovement>().getPathIndex();
            if (aux > mayor) {
                mayor = aux;
                result = i;
            }else if (aux == mayor) {
                Transform punto = LevelManager.main.path[result];
                float distance1 = Vector2.Distance(punto.transform.position, hits[i].transform.position);
                float distance2 = Vector2.Distance(punto.transform.position, hits[result].transform.position);
                if (distance1 < distance2) {
                    result = i;
                }
            }
        }
        return result;
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + this.angle;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
