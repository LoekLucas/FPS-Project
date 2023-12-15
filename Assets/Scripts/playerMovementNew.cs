using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Xml.Schema;

public class playerMovementNew : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2;
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

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        moveDirectionZ = new Vector3(0, 0, moveZ);
        moveDirectionX = new Vector3(moveX, 0, 0);
        moveDirection = transform.TransformDirection(moveDirectionZ + moveDirectionX);

        if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }

        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }

        if (timesDoubleJumped <= 1f && (!Input.GetKey(KeyCode.Mouse4)))
        {

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            if (moveDirection != Vector3.zero)
            {
               Idle();
            }
        }

        if (characterController.isGrounded)
        {
            timesDoubleJumped = 0;

            if (hoverEnergy < 300)
            {
                hoverEnergy++;
            }
        }

        //characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        if(Input.GetKey(KeyCode.Mouse4) && hoverEnergy > 0)
        {
            velocity.y = 0;

            if (currentCheckpoint != 4)
            {
                hoverEnergy--;
            }
        } 
        
        else
        {
            velocity.y += gravityValue * Time.deltaTime; // applies gravity
        }

        moveDirection += velocity;
        characterController.Move(moveDirection * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("tpTrigger") || collision.gameObject.CompareTag("tpTriggerOptional") && currentCheckpoint != 4)
        {

            if (currentCheckpoint == 1 && playerHealth > 0)
            {
                TeleportPlayer(new Vector3(23, 31, 136));
            }

            else if (currentCheckpoint == 2 && playerHealth > 0)
            {
                TeleportPlayer(new Vector3(25, 68, 107));
            }

            else if (currentCheckpoint == 3 && playerHealth > 0)
            {
                TeleportPlayer(new Vector3(25.2199993f, 93, 49.493f));
            }

            else if (currentCheckpoint == 4)
            {
                TeleportPlayer(new Vector3(24, 82.41f, -100));
                playerHealth++;

                if (currentTime < bestTime)
                {
                    bestTime = currentTime;
                    bestSecondsCount = secondsCount;
                    bestMinuteCount = minuteCount;
                    bestHourCount = hourCount;
                }
            }

            else
            {
                TeleportPlayer(new Vector3(0, 1.40999997f, -7.23000002f));
                if (playerHealth <= 0)
                {
                    playerHealth++;
                }
            }

            playerHealth--;

        }

        else if (collision.gameObject.CompareTag("checkPoint1") && currentCheckpoint == 0)
        {
            currentCheckpoint = 1;
        }

        else if (collision.gameObject.CompareTag("checkPoint2") && currentCheckpoint == 1)
        {
            currentCheckpoint = 2;
        }

        else if (collision.gameObject.CompareTag("checkPoint3") && currentCheckpoint == 2)
        {
            currentCheckpoint = 3;
        }

        else if (collision.gameObject.CompareTag("checkPoint4") && currentCheckpoint == 3)
        {
            currentCheckpoint = 4;
        }

        else if (collision.gameObject.CompareTag("timeReset"))
        {
            currentTime = 0;
            secondsCount = 0;
            minuteCount = 0;
            hourCount = 0;

            currentCheckpoint = 0;
        }
    }

    private void Walk()
    {
        moveDirection *= walkSpeed;
    }

    private void Run()
    {
        moveDirection *= runSpeed;
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityValue);
        timesDoubleJumped++;
    }

    private void Idle() { }

    private void TeleportPlayer(Vector3 location)
    {
        characterController.enabled = false;
        transform.position = location;
        characterController.enabled = true;
    }

    private void TextManager()
    {
        if (currentCheckpoint != 4)
        {
            livesText.text = "Lives: " + playerHealth;
            hoverText.text = "Hover Left: " + hoverEnergy / 100;
            timerText.text = hourCount + "h:" + minuteCount + "m:" + secondsCount + "s";
            infHoverText.text = "";
        }

        else
        {
            livesText.text = "Lives: Infinite";
            hoverText.text = "";
            infHoverText.text = "Infinite Hover";
            timerText.text = "Your final time is: " + hourCount + "h:" + minuteCount + "m:" + secondsCount + "s" + "!";
        }

        bestTimeText.text = "Best Time: " + bestHourCount + "h:" + bestMinuteCount + "m:" + bestSecondsCount + "s";
        checkPointText.text = "Current Checkpoint: " + currentCheckpoint;
    }

    private void Timer()
    {
        if (currentCheckpoint != 4)
        {
            secondsCount += Time.deltaTime;
            currentTime += Time.deltaTime;
            if (secondsCount >= 60)
            {
                minuteCount++;
                secondsCount = 0;
            }
            else if (minuteCount >= 60)
            {
                hourCount++;
                minuteCount = 0;
            }
        }
    }

}
