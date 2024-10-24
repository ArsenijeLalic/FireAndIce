using UnityEngine;

public class PlayerController : Unit
{
    void Update()
    {
        if(gm.gameIsOn && alive)
        {
            //Movement section
            float vInput = Input.GetAxis("Vertical");
            float hInput = Input.GetAxis("Horizontal");
            Vector2 direction = new Vector2(hInput, vInput);
            rb.velocity = currSpeed * Time.fixedDeltaTime * direction;

            animator.SetFloat("speed_f", rb.velocity.magnitude);

            //Attacking
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                attacks[0].Attack();
            }
            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                attacks[1].Attack();
            }
            RotateSprite(rb.velocity.x);
        }
    }



    protected override void AfterDeath()
    {
        gm.GameOver();
    }
}
