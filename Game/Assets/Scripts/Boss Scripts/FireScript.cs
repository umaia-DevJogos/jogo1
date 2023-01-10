using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    [SerializeField] private int projectileLimit = 10;
    [SerializeField] private float minAngle = 0f;
    [SerializeField] private float maxAngle = 360f;
    private float waveCooldown = 4f;
    [SerializeField] private float waveCooldownOut;
    [SerializeField] private BossScript bossScript;

    //Sprites
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite firingSprite;
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite rageFiringSprite;
    [SerializeField] private Sprite rageNormalSprite;

    public bool rageMode = false;

    private void Update()
    {
        waveCooldown += Time.deltaTime; // Increment Cooldown
        
        if (waveCooldown > waveCooldownOut)
        {
            if(bossScript.hp <= bossScript.starthp / 2)
            {
                rageMode = true;
            }
            waveCooldown = 0f;
            StartCoroutine("FireModes");
        }
    }
    private void Fire(float p_projectileLimit)
    {
        float angleStep = (maxAngle - minAngle) / p_projectileLimit; //Divide degree range by the number of possible projectiles
        float angle = minAngle;

        for (int i = 0; i < p_projectileLimit + 1; i++)
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
    IEnumerator FireModes()
    {
        if (rageMode) //Enraged mode
        {
            spriteRenderer.sprite = rageFiringSprite;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = rageNormalSprite;
            Fire(projectileLimit * 2);
        }
        else //Normal Mode
        {
            spriteRenderer.sprite = firingSprite;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = normalSprite;
            Fire(projectileLimit);
        }
    }
}
