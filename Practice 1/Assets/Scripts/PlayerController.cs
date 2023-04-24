using UnityEngine;


// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour {
	
	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public GameObject looseTextObject;
	public AudioClip pickUpEffect;

    private float movementX;
    private float movementY;
	private float heigth;

	private Rigidbody rb;
	private AudioSource playerSound;
	private int count;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();
		playerSound = GetComponent<AudioSource>();
		// Set the count to zero 
		count = 0;
		heigth = 0;
		SetCountText ();

                // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
                winTextObject.SetActive(false);
				looseTextObject.SetActive(false);
	}

	void FixedUpdate ()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
		rb.AddForce (movement * speed);

		Vector3 bar = transform.position;
		heigth = bar.y;
		
		if(heigth<-10){
				looseTextObject.SetActive(true);
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();

			//Reproduce the sound
			playerSound.PlayOneShot(pickUpEffect, 1.0f);
		}
	}

        void OnMove(InputValue value)
        {
        	Vector2 v = value.Get<Vector2>();

        	movementX = v.x;
        	movementY = v.y;
			
        }

        void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 13) 
		{
                    // Set the text value of your 'winText'
                    winTextObject.SetActive(true);
		}
	}
}
