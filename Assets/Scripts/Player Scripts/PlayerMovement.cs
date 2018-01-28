using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed=5f;
	public float jumpPower=10f;
	public float secondJumpPower = 10f;
	public Transform groundCheckPosition;
	//To check if player is on the ground or not
	//Side note.. In Unity we have can have class and a data type with the same name. EG Transform and transform...
	//In such cases the one with the lower case letter normally refers to the class used to find the details of the oject the script is attached to
	public float radius=0.3f;
	public LayerMask LayerGround;
	//This represents a layer. Bookmarked on Udemy
	public GameObject smokePosition;

	private Rigidbody myBody;
	private bool isGrounded;
	private bool playerJumped;
	private bool canDoubleJump;

	private PlayerAnnimation playerAnim;

	private bool gameStarted;

	private BGScroller bgScroller;

    private PlayerHealthDamageShoot playerShoot;
    private Button jumpBtn;

	void Awake () {
		myBody = GetComponent<Rigidbody> ();
		playerAnim = GetComponent<PlayerAnnimation> ();
		bgScroller = GameObject.Find (Tags.Backgrund_GameObjTag).GetComponent<BGScroller> ();
        playerShoot = GetComponent<PlayerHealthDamageShoot>();

        jumpBtn = GameObject.Find(Tags.Jump_Button_Obj).GetComponent<Button>();
        jumpBtn.onClick.AddListener(() => Jump());
	}

	void Start(){
		StartCoroutine (StartGame ());
	}

	void FixedUpdate (){
		if (gameStarted) {
			PlayerMove();
			PlayerGrounded ();
			PlayerJump ();
		}
	}

	void PlayerMove(){
		myBody.velocity = new Vector3 (movementSpeed, myBody.velocity.y, 0f);
		//if myBody.velocity.y wasnt present, the player will fall in slow motion. Try by replacing it with 0f
	}

	void PlayerGrounded(){
		isGrounded = Physics.OverlapSphere (groundCheckPosition.position, radius, LayerGround).Length > 0;
		//This basically checks if the player is on the ground or not. Since physics.overlapsSphere can retern an array of collisions,
		//check its length will let us know if collision takes place at a radius radius from the object attached to GroundCheckPosition 
		//with LayerGround.

	}

	void PlayerJump(){

		if (Input.GetKeyDown (KeyCode.Space) && !isGrounded && canDoubleJump) {
			canDoubleJump = false;
			myBody.AddForce (new Vector3 (0, secondJumpPower , 0));
		} else if (Input.GetKeyUp (KeyCode.Space) && isGrounded) {
			playerAnim.didJump ();
			myBody.AddForce (new Vector3 (0, jumpPower, 0));
			playerJumped = true;
			canDoubleJump = true;
		}

	}

    public void Jump()
    {
        if (!isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            myBody.AddForce(new Vector3(0, secondJumpPower, 0));
        }
        else if (isGrounded)
        {
            playerAnim.didJump();
            myBody.AddForce(new Vector3(0, jumpPower, 0));
            playerJumped = true;
            canDoubleJump = true;
        }
    }


	IEnumerator StartGame(){
		yield return new WaitForSeconds (2f);
		gameStarted = true;
		bgScroller.canScroll = true;
        playerShoot.canShoot = true;
        GameplayController.instance.canCountScore = true;
		smokePosition.SetActive (true);
		playerAnim.PlayerRun ();
	}
		
	void OnCollisionEnter(Collision target){
		if (target.gameObject.tag == Tags.Platform_Tag) {
            if (playerJumped)
            {
                playerJumped = false;
                playerAnim.DidLand();
            }
        }
	}

}
