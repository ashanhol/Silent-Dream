using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    

    //Makes the sign shake when you mouse over it.
    void OnMouseOver()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2f, 0));
        GetComponent<SpriteRenderer>().color = Color.gray;
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

    }

}
