using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool projectilePoolInstance;
    [SerializeField] GameObject pooledProjectile;
    [SerializeField] bool notEnoughProjectiles = true;
    private List<GameObject> projectiles;

    private void Awake()
    {
        projectilePoolInstance = this; // "this" refers to this gameObject
    }
    void Start()
    {
        projectiles = new List<GameObject>();
    }

    void Update()
    {
        
    }

    public GameObject getProjectile()
    {
        if (projectiles.Count > 0)
        {
            for (int i = 0; i < projectiles.Count; ++i)
            {
                if (!projectiles[i].activeInHierarchy)
                {
                    return projectiles[i];
                }
            }
        }

        if (notEnoughProjectiles)
        {
            GameObject new_projectile = Instantiate(pooledProjectile);
            new_projectile.SetActive(false);
            projectiles.Add(new_projectile);
            return new_projectile;
        }
        return null;
    }
}
