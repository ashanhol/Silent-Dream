using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

    public GameObject goal;
    public float speed;
    private GameController gameController;

    //Initialize child and GameController
    void Start () {
        goal = GameObject.Find("child");

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
	

	//Enemies move towards child every frame
	void Update () {

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, goal.transform.position, step);
        if (goal.transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if (goal.transform.position.x >= transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            
        }

    }

    //Makes the enemies fade yellow when hit by doll and add to the score
    IEnumerator ChangeColorOnDeath()
    {
    
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, .5f);
        yield return new WaitForSeconds(.09f);
       
        Destroy(gameObject);
        gameController.AddScore(50);
        gameController.EnemyCountIncrease();

    }

    //Runs ChangeColorOnDeath() when enemy and doll collide
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ChangeColorOnDeath());
            
        }
    }
}
