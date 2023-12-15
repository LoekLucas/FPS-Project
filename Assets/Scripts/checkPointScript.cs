using Unity.VisualScripting;
using UnityEngine;

public class checkPointScript : MonoBehaviour
{
    public string currentCheckpoint;
    public GameObject Player;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("tpTrigger"))
        {

            if (currentCheckpoint == "checkPoint1")
            {
                transform.position = new Vector3(23f, 31f, 136f);
            }

            else if (currentCheckpoint == "checkPoint2")
            {
                transform.position = new Vector3(25, 68, 107);
            }

            else if (currentCheckpoint == "checkPoint3")
            {
                transform.position = new Vector3(25.2199993f, 93, 49.493f);
            }
        }

        else if (collision.gameObject.CompareTag("checkPoint1"))
        {
            currentCheckpoint = "checkPoint1";

        }

        else if (collision.gameObject.CompareTag("checkPoint2"))
        {
            currentCheckpoint = "checkPoint2";
        }

        else if (collision.gameObject.CompareTag("checkPoint3"))
        {
            currentCheckpoint = "checkPoint3";
        }
    }


}
