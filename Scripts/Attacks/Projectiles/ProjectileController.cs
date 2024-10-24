using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private string element;
    private Rigidbody2D projectileRB;
    private string enemyTag;
    bool collided = false;
    [SerializeField] private int damage;
    [SerializeField] GameObject residuePrefab;
    
    private AudioSource aSource;
    [SerializeField] private AudioClip spellFly;
    [SerializeField] private AudioClip spellHit;
    private Animator animator;

    void Awake()
    {
        aSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        projectileRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        aSource.PlayOneShot(spellFly);
    }

    void Update()
    {
        if (!collided)
            projectileRB.velocity = speed * Time.fixedDeltaTime * transform.right;
    }

    public string GetElement()
    {
        return element;
    }

    public void setEnemyTag(string tag)
    {
        enemyTag = tag;
    }

    public void setDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag(enemyTag) || collision.gameObject.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            collided = true;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            if (collision.gameObject.CompareTag(enemyTag) || collision.gameObject.CompareTag("Door"))
            {
                DamageEnemy(collision.gameObject);
            }
            if(residuePrefab!=null) Instantiate(residuePrefab, transform.position, residuePrefab.transform.rotation);
            projectileRB.velocity = Vector2.zero;
            aSource.PlayOneShot(spellHit);
            animator.SetTrigger("hit_trig");
            Destroy(gameObject, 0.4f);
        }
        if(collision.gameObject.CompareTag("Candle") && element == "fire")
        {
            collision.gameObject.GetComponent<Light2D>().intensity = 2;
        }
    }

    private void DamageEnemy(GameObject enemy)
    {
        HealthSystem hs = enemy.GetComponent<HealthSystem>();
        hs.UpdateHealth(-damage, element);
    }
}
