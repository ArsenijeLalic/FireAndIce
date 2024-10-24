using UnityEngine;

[System.Serializable]
public class ShootProjectile : AttackScript
{
    [SerializeField] protected GameObject[] projectilePrefabs;
    [SerializeField] protected float aimOffSet;
    // Start is called before the first frame update

    protected virtual void Shoot(Vector2 target)
    {
        int index = Random.Range(0, projectilePrefabs.Length);
        Vector2 aimFromShooter = target - (Vector2)transform.position;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(aimFromShooter.y, aimFromShooter.x);
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject projInstance = Instantiate(projectilePrefabs[index], (Vector2)transform.position + aimFromShooter.normalized * aimOffSet, rotation);
        ProjectileController pc = projInstance.GetComponent<ProjectileController>();
        pc.setEnemyTag(enemyTag);
        if(enemyTag != "Player")
        {
            pc.setDamage(damage);
        }
    }

    public override void Attack()
    {
        if (!onCooldown)
        {
            AttackAnimation();
            Shoot(takeAim());
            StartCoroutine(CooldownCoroutine());
        }
    }
}
