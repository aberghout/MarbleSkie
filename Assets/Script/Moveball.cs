using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Moveball : MonoBehaviour
{

Moveball controller = null;

[SerializeField] bool lockCursor = true;

    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.5f;
    public float mRunSpeed = 3.0f;
    public float mRotationSpeed = 50.0f;
    public float mGravity = -30.0f;

    [Tooltip("Only useful with Follow and Independent Rotation - third - person camera control")]
    public bool mFollowCameraForward = false;
    public float mTurnRate = 200.0f;
    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    //
    Rigidbody rb;
    public int ballspeed = 0;
    public int jumpspeed = 0;
    private bool istouching = true;

    public Camera mainCam;
    
    //private int counter;
    //public Text cointext;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 200;
        //counter = 15;
       // cointext.text = "COINS: " + counter;
     controller = GetComponent<Moveball>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));


        Vector3 moveball = new Vector3(h, 0.0f, v);

        Quaternion dir = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up); // (0, 1, 0)
       
        moveball = dir * moveball;



        if (istouching == true)
        {
        
            rb.AddForce(moveball * ballspeed);
        }

        if ((Input.GetKey(KeyCode.Space)) && (istouching == true))
        {
            Vector3 balljump = new Vector3(0f, 6f, 0f);

            rb.AddForce(balljump * jumpspeed);
        }

        istouching = false;

        if ((Input.GetKey(KeyCode.Space)) && (istouching == true))
        {
            Vector3 balljump = new Vector3(0f, 6f, 0f);
           
            rb.AddForce(balljump * jumpspeed);
        }

        istouching = false;

        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        mCharacterController.Move(mVelocity * Time.deltaTime);

    }
    
    void OnCollisionStay() 
    {
        istouching = true;
    }


  //  private void OnTriggerEnter(Collider other)
   // {
   //     if (other.gameObject.CompareTag("Coinstag"))
  //      {
   //         other.gameObject.SetActive (false);
  //          counter = counter - 1;
//cointext.text = "COINS: " + counter;
   //         if (counter == 0)
     //       {
           //     SceneManager.LoadScene("EndScene");
        //    }
        //}
    //}




}
