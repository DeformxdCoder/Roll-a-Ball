using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public Text donotpickupText;

    private Rigidbody rb;
    private int count;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;              //Initialize Score, and score text to 0
        SetCountText ();
        winText.text = "";
        donotpickupText.text = "Ouch! -1";          //Initialize Bad Pickup message, invisible at start
	Color temp = donotpickupText.color;
	temp.a = 0.0f;
	donotpickupText.color = temp;
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");        //Accept movement commands
        float moveVertical = Input.GetAxis ("Vertical");
	    float jump;

	    if (Input.GetKeyDown(KeyCode.Space) && rb.transform.position.y <= 1.5f)          //Accept jump input if on/near ground
	        jump = 20f;
	    else
	        jump = 0;

        Vector3 movement = new Vector3 (moveHorizontal, jump, moveVertical);

        rb.AddForce (movement * speed);                             //Update velocity to incorporate any new valid inputs
	    Color temp = donotpickupText.color;
	    if (temp.a > 0)                                             //Fade bad pickup text when visible
	    {
	        temp.a -= 2.55f;
		    donotpickupText.color = temp;
	    }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Pick Up"))               //Increase score and destroy collectibles on collision
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
	    if (other.gameObject.CompareTag ( "DoNotPick Up"))          //Decrease score, destroy bad collectibles, and display bad pickup message on collision
	    {
            other.gameObject.SetActive (false);
            count = count - 1;
            SetCountText ();
            Color temp = donotpickupText.color;
	        temp.a = 255.0f;
	        donotpickupText.color = temp;
	    }
    }

    void SetCountText ()                                            //Check for win conditions to be met
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 10)
        {
            winText.text = "You Win!";
        }
    }
}