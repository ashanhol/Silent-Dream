using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

#if UNITY_WSA_10_0 && NETFX_CORE
using Windows.UI.ViewManagement;
#endif


public class weakCharacterMovement : MonoBehaviour
{

    public float maxSpeed = 10f;
    bool facingRight = true; //which way character facing horizontal 
    public GameObject joystick;


    void FixedUpdate()
    {
        Vector2 movement;
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        //Arrows for keyboard
        if (Input.GetKey(KeyCode.LeftArrow))
            moveHorizontal = -1;

        else if (Input.GetKey(KeyCode.RightArrow))
            moveHorizontal = 1;

        if (Input.GetKey(KeyCode.UpArrow))
            moveVertical = 1;

        else if (Input.GetKey(KeyCode.DownArrow))
            moveVertical = -1;

        movement = new Vector2(moveHorizontal, moveVertical);

        //Touch Joysticks for mobile
        if (UseJoystick || joystick.GetComponent<Image>().enabled)
        { 
        
            movement = new Vector2(CrossPlatformInputManager.GetAxis("HorizontalRight"), CrossPlatformInputManager.GetAxis("VerticalRight"));

        }

        GetComponent<Rigidbody2D>().velocity = movement * maxSpeed;



        //Flip direction child is facing. 
        if (moveHorizontal > 0 && !facingRight)
        {
            FlipHorizontal();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            FlipHorizontal();
        }
        
    }

    void FlipHorizontal()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    //Determines whether to use joysticks on Windows machines
    public static bool IsWindows10UserInteractionModeTouch
    {
        get
        {
            bool isInTouchMode = false;
#if UNITY_WSA_10_0 && NETFX_CORE
    UnityEngine.WSA.Application.InvokeOnUIThread(() =>
    {
      isInTouchMode =
        UIViewSettings.GetForCurrentView().UserInteractionMode ==
        UserInteractionMode.Touch;
    }, true);        
#endif
            return isInTouchMode;
        }
    }


    public static bool UseJoystick
    {
        get
        {
            return (IsWindowsMobile || IsWindows10UserInteractionModeTouch);
        }
    }


    public static bool IsWindowsMobile
    {
        get
        {
#if UNITY_WSA_10_0 && NETFX_CORE
      return Windows.Foundation.Metadata.ApiInformation.
        IsApiContractPresent ("Windows.Phone.PhoneContract", 1);
#else
            return false;
#endif
        }
    }


}

