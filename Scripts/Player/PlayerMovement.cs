using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    [HideInInspector]
    public float hInput;
    [HideInInspector]
    public float vInput;
    public FixedJoystick fixedJoystick;


    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;


    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
#if UNITY_STANDALONE || UNITY_WEBGL

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);

#else
        Move(hInput, vInput);
        Turning();
        Animating(hInput, vInput);


#endif


    }

    void Move(float h, float v)
    {
        movement.Set(h, 0.0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {

            #if UNITY_STANDALONE || UNITY_WEBGL

            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0.0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);


            #else
            Vector3 playerToMouse = new Vector3(fixedJoystick.inputVector.x, 0.0f, fixedJoystick.inputVector.y);


            if (playerToMouse != Vector3.zero)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidBody.MoveRotation(newRotation);

            }

            #endif
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h!=0f||v!=0f;
        anim.SetBool("IsWalking", walking);
    }
}
