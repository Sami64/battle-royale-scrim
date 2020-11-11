using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject playerModel;
    Quaternion targetRotation;
    [Header("Equipment")]
    public Sword sword;
    public Bow bow;
    public GameObject bombPrefab;
    public float throwingSpeed = 200f;
    [SerializeField]
    int bombAmount = 5;
    [SerializeField]
    int arrowAmount = 15;
    [Header("Movement")]
    public float rotatingSpeed = 3f;
    public float jumpVelocity = 5f;
    public float movingVelocity = 4f;
    bool canJump = false;
    Rigidbody playerRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, targetRotation, Time.deltaTime * rotatingSpeed);
        PlayerMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    void PlayerMovement()
    {
        /*
         * Consider setting velocity to zero
         */
        playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
        if (Input.GetKey("right"))
        {
            playerRigidbody.velocity = new Vector3(movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            targetRotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey("left"))
        {
            playerRigidbody.velocity = new Vector3(-movingVelocity, playerRigidbody.velocity.y, playerRigidbody.velocity.z);
            targetRotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKey("up"))
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, movingVelocity);
            targetRotation = Quaternion.Euler(0, 0, 0);

        }
        if (Input.GetKey("down"))
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y, -movingVelocity);
            targetRotation = Quaternion.Euler(0, 180, 0);
        }
        if(canJump && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpVelocity, playerRigidbody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            sword.SwordAttack();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowBomb();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(arrowAmount > 0)
            {
                bow.Shoot();
                arrowAmount--;
            }
        }
    }

    void ThrowBomb()
    {
        if (bombAmount <= 0)
        {
            Debug.Log("Out of bombs");
            return;
        }
        GameObject bombObject = Instantiate(bombPrefab);
        bombObject.transform.position = transform.position + playerModel.transform.forward;
        Vector3 throwingDirection = (playerModel.transform.forward + Vector3.up).normalized;
        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);
        bombAmount--;
    }
}
