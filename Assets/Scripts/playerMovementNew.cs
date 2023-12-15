using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Xml.Schema;

public class PlayerMovementNew : MonoBehaviour
{
    // Variables and the like

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;

    private Vector3 moveDirection;
    private Vector3 moveDirectionZ;
    private Vector3 moveDirectionX;
    private Vector3 velocity;

    private float gravityValue = -9.81f;
    private float jumpHeight = 3f;
    public float timesDoubleJumped = 0f;
    public float hoverEnergy = 300f;
    public float currentCheckpoint;
    public float playerHealth = 3;
    private bool hasReachedEnd = false;

    public float secondsCount = 0f;
    public float minuteCount = 0f;
    public float hourCount = 0f;

    public float bestSecondsCount = 0f;
    public float bestMinuteCount = 0f;
    public float bestHourCount = 0f;

    public float currentTime = 0f;
    public float bestTime = 9999999999f;


    public TMP_Text livesText;
    public TMP_Text hoverText;
    public TMP_Text infHoverText;
    public TMP_Text checkPointText;
    public TMP_Text timerText;
    public TMP_Text bestTimeText;

    private CharacterController characterController;

    // Start is called before the first frame update

    void Start()
    {
        // Gets the character controller from the player
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    void Update()
    {
        Move();
        TextManager();
        Timer();
    }

    private void Move()
    {

        if (characterController.isGrounded && velocity.y < 0) //?
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        // Setting Vector3's for horizontal and vertical movement
        moveDirectionZ = new Vector3(0, 0, moveZ);
        moveDirectionX = new Vector3(moveX, 0, 0);
        moveDirection = transform.TransformDirection(moveDirectionZ + moveDirectionX);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) // If you are moving and left shift is not pressed
        {
            Walk();
        }

        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) // If you are moving and left shift is pressed
        {
            Run();
        }

        if (timesDoubleJumped <= 1f && (!Input.GetKey(KeyCode.Mouse4))) // If you haven't double jumped more than once and mouse4 is not pressed
        {

            if (Input.GetButtonDown("Jump")) // If jump key is pressed
            {
                Jump();
            }

            if (moveDirection != Vector3.zero) // If not standing still
            {
                Idle();
            }
        }

        if (characterController.isGrounded) // If you're on the ground
        {
            timesDoubleJumped = 0;

            if (hoverEnergy < 300) // If hoverEngergy is less than 300
            {
                hoverEnergy++; // Continuously add 1 to hoverEnergy
            }
        }

        if (Input.GetKey(KeyCode.Mouse4) && hoverEnergy > 0) // If mouse4 is pressed and hoverenergy is more than 0
        {
            velocity.y = 0; // Set vertical velocity to 0

            if (currentCheckpoint != 4) // If you aren't at checkpoint 4
            {
                hoverEnergy--; // Continuously subtract 1 from hoverEnergy
            }
        }

        else
        {
            velocity.y += gravityValue * Time.deltaTime; // applies gravity
        }

        moveDirection += velocity;
        characterController.Move(moveDirection * Time.deltaTime); // Makes sure movement speed is not based off FPS

    }

    private void OnTriggerEnter(Collider collision) // When entering a trigger
    {
        // If trigger's tag is "tpTrigger or tpTriggerOptional and your current checkpoint is not 4
        if (collision.gameObject.CompareTag("tpTrigger") || collision.gameObject.CompareTag("tpTriggerOptional") && currentCheckpoint != 4)
        {

            if (currentCheckpoint == 1 && playerHealth > 0) // If current checkpoint is 1 and player health is higher than 0
            {
                TeleportPlayer(new Vector3(23, 31, 136)); // Teleport player to given position
            }

            else if (currentCheckpoint == 2 && playerHealth > 0) // If current checkpoint is 2 and player health is higher than 0
            {
                TeleportPlayer(new Vector3(25, 68, 107)); // Teleport player to given position
            }

            else if (currentCheckpoint == 3 && playerHealth > 0) // If current checkpoint is 3 and player health is higher than 0
            {
                TeleportPlayer(new Vector3(25.2199993f, 93, 49.493f)); // Teleport player to given position
            }

            else if (currentCheckpoint == 4) // If current checkpoint is 4
            {
                TeleportPlayer(new Vector3(24, 82.41f, -100)); // Teleport player to given position
            }

            else // If current checkpoint is anything other than 1-4
            {
                TeleportPlayer(new Vector3(0, 1.40999997f, -7.23000002f)); // Teleport player to given position
                if (playerHealth <= 0) // If player health is less than or equal to 0
                {
                    playerHealth++; // Add 1 to player health
                }
            }

            if (!hasReachedEnd) // If end hasn't been reached
            {
                playerHealth--; // Remove 1 from player health
            }
        }

        else if (collision.gameObject.CompareTag("checkPoint1") && currentCheckpoint == 0) // If trigger's tag is "checkPoint1" and your current checkpoint is 0
        {
            currentCheckpoint = 1; // Set current checkpoint to subsequent value
        }

        else if (collision.gameObject.CompareTag("checkPoint2") && currentCheckpoint == 1) // If trigger's tag is "checkPoint2" and your current checkpoint is 1
        {
            currentCheckpoint = 2; // Set current checkpoint to subsequent value
        }

        else if (collision.gameObject.CompareTag("checkPoint3") && currentCheckpoint == 2) // If trigger's tag is "checkPoint3" and your current checkpoint is 2
        {
            currentCheckpoint = 3; // Set current checkpoint to subsequent value
        }

        else if (collision.gameObject.CompareTag("checkPoint4") && currentCheckpoint == 3) // If trigger's tag is "checkPoint4" and your current checkpoint is 3
        {
            currentCheckpoint = 4; // Set current checkpoint to subsequent value
            hasReachedEnd = true; 

            if (currentTime < bestTime) // If your current time is lower than your best time
            {
                bestTime = currentTime; // Set current time as your best
                                        // Sets current times values to best timer values
                bestSecondsCount = secondsCount;
                bestMinuteCount = minuteCount;
                bestHourCount = hourCount;
            }
        }

        else if (collision.gameObject.CompareTag("timeReset")) // If trigger's tag is "timeReset"
        {
            // Set all current timer values to 0
            currentTime = 0;
            secondsCount = 0;
            minuteCount = 0;
            hourCount = 0;

            currentCheckpoint = 0; // Reset current checkpoint

            TeleportPlayer(new Vector3(0, 1.40999997f, -7.23000002f)); // Teleport player to given position
        }
    }

    private void Walk()
    {
        moveDirection *= walkSpeed; // Set move direction to go at walkspeed
    }

    private void Run()
    {
        moveDirection *= runSpeed; // Set move direction to go at runspeed
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityValue); // ?
        if (currentCheckpoint != 4) // If current checkpoint is not equal to 4
        {
            timesDoubleJumped++; // Add 1 to time double jumped
        }
    }

    private void Idle() { } // Placeholder in case of wanting to add something to being idle

    private void TeleportPlayer(Vector3 location)
    {
        characterController.enabled = false; // Turn off character controller (It used to override teleport command)
        transform.position = location; // Set player position to set value
        characterController.enabled = true; // Turn on character controller
    }

    private void TextManager()
    {
        if (currentCheckpoint != 4) // If current checkpoint is not 4
        { 
            hoverText.text = "Hover Left: " + hoverEnergy / 100; // Think deviding the number by 100 looks better
            timerText.text = hourCount + "h:" + minuteCount + "m:" + secondsCount + "s";
            infHoverText.text = ""; // Made this field empty because otherwise it would keep infinite hover text from last run if time is restarted
        }

        else // If current checkpoint is anything other than not 4
        {
            hoverText.text = ""; // Makes this field empty because otherwise it would overlap
            infHoverText.text = "Infinite Hover";
            timerText.text = "Your final time is: " + hourCount + "h:" + minuteCount + "m:" + secondsCount + "s" + "!";
        }

        if (!hasReachedEnd) // If end hasn't been reached
        {
            livesText.text = "Lives: " + playerHealth;
        }

        else // Otherwise
        {
            livesText.text = "Lives: Infinite";
        }

        bestTimeText.text = "Best Time: " + bestHourCount + "h:" + bestMinuteCount + "m:" + bestSecondsCount + "s";
        checkPointText.text = "Current Checkpoint: " + currentCheckpoint;
    }

    private void Timer()
    {
        if (currentCheckpoint != 4) // If current checkpoint is not 4
        {
            secondsCount += Time.deltaTime; // Sets secondsCount to increase linearly, in actual seconds instead of having it be frame dependant
            currentTime += Time.deltaTime; // Same but for currentTime value, chose to add another unseen value that just adds up seconds because it makes comparing scores a lot easier
            if (secondsCount >= 60) // If seconds count it higher than or equal to 60
            {
                minuteCount++; // Increase minute count by 1
                secondsCount = 0; // Set second count to 0
            }
            else if (minuteCount >= 60) // If minute count is equal to or above 60
            {
                hourCount++; // Increase hour count by 1
                minuteCount = 0; // Set minute count to 0
            }
        }
    }

}
