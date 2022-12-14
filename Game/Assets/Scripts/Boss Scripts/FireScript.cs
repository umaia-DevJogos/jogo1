using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [SerializeField] private int projectileLimit = 10;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 360f;
    private float waveCooldown;
    [SerializeField] private float waveCooldownOut;
    private Vector2 projectileDirection;
    void Start()
    {
        //waveCooldown = 4f;
        InvokeRepeating("Fire", 0f, 4f);
    }
    /*void Update()
    {
        waveCooldownOut -= Time.deltaTime;

        if(waveCooldown >= 0)
        {
            Fire();
            waveCooldown = waveCooldownOut;
        }
    }*/
    private void Fire()
    {
        float angleStep = (maxAngle - minAngle) / projectileLimit; //Divide degree range by the number of possible projectiles
        float angle = minAngle;

        for (int i = 0; i < projectileLimit + 1; i++)
        {
            float projectileDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 directions = new Vector2(projectileDirectionX - transform.position.x, projectileDirectionY - transform.position.y).normalized;

            GameObject projectile = ProjectilePool.projectilePoolInstance.getProjectile(); // Call getProjectile method in ProjectilePool class
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.SetActive(true); // Enable Projectile
            projectile.GetComponent<ProjectileScript>().setDirection(directions); // Send direction to setDirection() on ProjectileScript class

            angle += angleStep; // Increment angle
        }
    }
}
