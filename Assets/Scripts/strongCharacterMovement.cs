using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

#if UNITY_WSA_10_0 && NETFX_CORE
using Windows.UI.ViewManagement;
#endif


public class strongCharacterMovement : MonoBehaviour
{

    public float maxSpeed = 10f;
    public GameObject joystick; 

    void Start()
    {
        //make sure the child and doll don't collide with each other
        GameObject child = GameObject.FindGameObjectWithTag("Child");
        Collider2D[] childColliders = child.GetComponents<Collider2D>();
        Collider2D[] dollColliders = GetComponents<Collider2D>();
        for (int i = 0; i < childColliders.Length; i++)
        {
            for (int j = 0; j < dollColliders.Length; j++)
            {
                Physics2D.IgnoreCollision(childColliders[i], dollColliders[j]);
            }
        }
    }



    void FixedUpdate()
    {
        Vector2 movement; 

        //WASD for keyboard
        float moveHorizontal = 0f;
        if (Input.GetKey(KeyCode.A))
            moveHorizontal = -1;

        else if (Input.GetKey(KeyCode.D))
            moveHorizontal = 1;



        float moveVertical = 0f;
        if (Input.GetKey(KeyCode.W))
            moveVertical = 1;

        else if (Input.GetKey(KeyCode.S))
            moveVertical = -1;

        movement = new Vector2(moveHorizontal, moveVertical);

        //Touch Joysticks for mobile
        if (UseJoystick || joystick.GetComponent<Image>().enabled)
        {
            
            movement = new Vector2(CrossPlatformInputManager.GetAxis("HorizontalLeft"), CrossPlatformInputManager.GetAxis("VerticalLeft"));

        }

        

        GetComponent<Rigidbody2D>().velocity = movement * maxSpeed;
        


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
