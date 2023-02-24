using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    ThirdPersonMovement thirdPersonMoveScript;

    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonMoveScript = playerObj.GetComponent<ThirdPersonMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if(thirdPersonMoveScript.direction != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, thirdPersonMoveScript.direction.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
