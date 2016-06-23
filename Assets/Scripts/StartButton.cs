using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

/*This script controls the animation at the beginning*/
public class StartButton : MonoBehaviour
{

    public GameObject firstBackground;
    public GameObject secondBackground;
    public GameObject thirdBackground;
    public GameObject instructions;


    public Button Skip;
    public Button exit;

    public GameObject squidnightmare;
    public GameObject ghostnightmare;
    public GameObject batnightmare;

    public GameObject creepything1;
    public GameObject creepything2;

    public AudioSource menutheme;
    public AudioSource firstVerse;
    public AudioSource secondVerse;
    public AudioSource thirdVerse;
    public AudioSource fourthVerse;


    // Use this for initialization
    void Start()
    {
        Skip.GetComponent<Image>().enabled = false;
        Skip.GetComponentInChildren<Text>().enabled = false;
        Skip.GetComponent<Button>().enabled = false;
      
    }

    public void ChangeScene()
    {
     
            SceneManager.LoadScene("Main Scene");
        
    }
    public void Exit()
    {

        Application.Quit();

    }



    void OnMouseDown()
    {
        StartCoroutine(FirstCutscene());

        //SceneManager.LoadScene("Main Scene");

    }

    IEnumerator FirstCutscene()
    {
        Skip.GetComponent<Image>().enabled = true;
        Skip.GetComponentInChildren<Text>().enabled = true;
        Skip.GetComponent<Button>().enabled = true;

        exit.GetComponent<Image>().enabled = false;
        exit.GetComponent<Button>().enabled = false;

        GetComponent<BoxCollider2D>().enabled = false; // make it so you can't press the button anymore.
        instructions.GetComponent<BoxCollider2D>().enabled = false;

        menutheme.volume = .2f;
        firstVerse.Play();

        while (firstBackground.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            firstBackground.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, firstBackground.GetComponent<SpriteRenderer>().color.a + .02f);

            yield return new WaitForSeconds(.2f);

        }
        while(firstVerse.isPlaying)
            yield return new WaitForSeconds(.5f);


        secondVerse.Play();
        while (secondBackground.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            secondBackground.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, secondBackground.GetComponent<SpriteRenderer>().color.a + .02f);

            yield return new WaitForSeconds(.2f);
        }
        while (secondVerse.isPlaying)
            yield return new WaitForSeconds(.5f);

        thirdVerse.Play();
        while (thirdBackground.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            thirdBackground.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, thirdBackground.GetComponent<SpriteRenderer>().color.a + .02f);

            yield return new WaitForSeconds(.2f);
        }
        while (thirdVerse.isPlaying)
            yield return new WaitForSeconds(.5f);

        fourthVerse.Play();
        StartCoroutine(StopBGMusic());  
        yield return new WaitForSeconds(1f);
        while (ghostnightmare.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            ghostnightmare.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, ghostnightmare.GetComponent<SpriteRenderer>().color.a + .05f);
            batnightmare.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, batnightmare.GetComponent<SpriteRenderer>().color.a + .05f);
            squidnightmare.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, squidnightmare.GetComponent<SpriteRenderer>().color.a + .05f);

            yield return new WaitForSeconds(.2f);
        }
        yield return new WaitForSeconds(2f);
        while (creepything1.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            creepything1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, creepything1.GetComponent<SpriteRenderer>().color.a + .05f);
            creepything2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, creepything2.GetComponent<SpriteRenderer>().color.a + .05f);

            yield return new WaitForSeconds(.2f);
        }

        while (fourthVerse.isPlaying)
            yield return new WaitForSeconds(.5f);

        SceneManager.LoadScene("Main Scene");

    }

    IEnumerator StopBGMusic()
    {
        yield return new WaitForSeconds(fourthVerse.clip.length - 6.5f);
        menutheme.Pause();


    }



}