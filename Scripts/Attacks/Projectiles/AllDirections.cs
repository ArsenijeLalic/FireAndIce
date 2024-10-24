using UnityEngine;

public class AllDirections : AttackScript
{
    [SerializeField] private GameObject[] projectilePrefabs;
    [SerializeField] private int numOfProjectiles;
    [SerializeField] private float aimOffSet;

    public override void Attack()
    {
        if (!onCooldown)
        {
            AttackAnimation();
            StartCoroutine(CooldownCoroutine());
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector2 direction = (target.transform.position - gameObject.transform.position).normalized;
        
        for(int i = 0; i < numOfProjectiles; i++)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            int index = Random.Range(0, projectilePrefabs.Length);            
            GameObject projInstance = Instantiate(projectilePrefabs[index], (Vector2)transform.position + direction.normalized * aimOffSet, rotation);
            ProjectileController pc = projInstance.GetComponent<ProjectileController>();
            pc.setEnemyTag(enemyTag);
            direction = RotateVector(direction, (2 * Mathf.PI)/numOfProjectiles);
        }
    }

    private Vector2 RotateVector(Vector2 vector, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        // Rotation matrix calculation
        float xPrime = vector.x * cos - vector.y * sin;
        float yPrime = vector.x * sin + vector.y * cos;

        return new Vector2(xPrime, yPrime);
    }
}
