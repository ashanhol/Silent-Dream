using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
#if UNITY_WSA_10_0 && NETFX_CORE
using Microsoft.UnityPlugins;
#endif

/*Note, this script although controls the game over condition, goes on the child*/
public class gameOver : MonoBehaviour {

    bool canDamage = true;

    public AudioClip hit;
    private AudioSource source;
    private Animator animator;                  

    private GameController gameController;

    //Initialize hit animation and GameController
    void Start () {

        source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();


        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
	

    //Check for Child/Enemy collision. Lose health if collided.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (canDamage) {
                animator.SetTrigger("ChildHit");

                gameController.SubtractHealth();
                StartCoroutine(damageTimeout());

                if (gameController.getHealth() <= 0)
                {
                    Time.timeScale = 0;
                    gameController.over = true;
                    StartCoroutine(ChangeLight());
                }
                
            }
        }
    }

    //Makes the background grow redder if you lose. Enter lose screen.
    IEnumerator ChangeLight()
    {
#if UNITY_WSA_10_0 && NETFX_CORE
       Toasts.ShowToast(ToastTemplateType.ToastImageAndText01, new string[] { "Try again! Your score was: " + gameController.getScore() }, "Assets/StoreLogo.scale-400.png");
#endif
        GameObject light = GameObject.FindGameObjectWithTag("Light");
        Light backgroundLight = light.GetComponent<Light>();
        while(backgroundLight.color.b > 0 && backgroundLight.color.g > 0) { 
            backgroundLight.color = new Color(backgroundLight.color.r, backgroundLight.color.b - .10f, backgroundLight.color.g - .10f);
            yield return new WaitForSeconds(.05f);

        }
        gameController.done = true;
        gameController.over = false;
        gameController.Reset();
        SceneManager.LoadScene("Game_Over");

    }

    //Makes sure child has recovery period.
    IEnumerator damageTimeout()
    {
        canDamage = false;
        //play sound
        source.PlayOneShot(hit, 1);
               
        yield return new WaitForSeconds(1f);


        canDamage = true;
    }
}
