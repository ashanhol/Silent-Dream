using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinScreen : MonoBehaviour
{

    public GameObject eyes;
    public Button again;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WinGame());
        again.GetComponent<Image>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;

    }

    IEnumerator WinGame()
    {
        //Make it seem like the child is opening and closing their eyes by enabling and disabling the closed eye sprite. 
        //NOTE: This is really cheap animation :)
        eyes.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        eyes.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        eyes.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        eyes.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.5f);
        eyes.GetComponent<SpriteRenderer>().enabled = false;
        again.GetComponent<Image>().enabled = true;

    }
}
