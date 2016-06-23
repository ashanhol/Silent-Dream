using UnityEngine;
using System.Collections;

/*This script controlls the sweet dream collision*/
public class sweetDream : MonoBehaviour {

    private GameController gameController;

    // Use this for initialization
    void Start () {
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Child")
        {
            gameController.AddSweetDream();
            Destroy(gameObject);
        }
    }
}
