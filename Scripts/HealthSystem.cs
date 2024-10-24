using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    SpriteRenderer spRend;
    GameManager gm;
    public int health;
    [SerializeField] private Slider mySlider;

    public bool dead = false;
    [SerializeField] private Color colorOnHurt;
    [SerializeField] private Color colorOnHeal;
    [SerializeField] private Color colorOnFreeze;
    private float timeInvincible = 0.2f;
    private bool invincible = false;
    [SerializeField] private bool playerCharacter;

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        mySlider.maxValue = health;
        mySlider.value = health;
    }
    private void Update()
    {
        mySlider.value = health;
    }

    public void UpdateHealth(int hp, string element)
    {
        if (gm.gameIsOn)
        {
            if (hp < 0)
            {
                if (!invincible)
                {
                    if (gameObject.CompareTag("Door"))
                    {
                        health--;
                    }
                    else
                    {
                        health += hp;
                        StartCoroutine(ColorCoroutine(colorOnHurt));
                        if (playerCharacter)
                            StartCoroutine(InvincibleCoroutine());
                    }
                }
                CheckHealth();
            }
            else
            {
                StartCoroutine(ColorCoroutine(colorOnHeal));
                health += hp;
            }
            
            
            if (element.Equals("ice"))
            {
                if (!gameObject.CompareTag("Door"))
                {
                    Unit enemy = gameObject.GetComponent<Unit>();
                    StartCoroutine(enemy.BlockMove(0.5f, 1));

                    if (gameObject.CompareTag("Player"))
                    {
                        StartCoroutine(ColorCoroutine(colorOnFreeze));
                    }
                }
            }
        }
    }

    IEnumerator ColorCoroutine(Color color)
    {
        spRend.color = color;
        yield return new WaitForSeconds(0.2f);
        spRend.color = Color.white;
    }



    private void CheckHealth()
    {
        if (!gameObject.CompareTag("Door"))
        {
            gameObject.GetComponent<Unit>().Damaged();
            if (health <= 0)
            {
                gameObject.GetComponent<Unit>().Die();
            }
        }
        else
        {
            if (health <= 0)
            {
                Destroy(gameObject,0.3f);
            }
        }
    }

    private IEnumerator InvincibleCoroutine()
    {
        invincible = true;
        yield return new WaitForSeconds(timeInvincible);
        invincible = false;
    }
}
