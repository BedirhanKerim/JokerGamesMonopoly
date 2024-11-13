using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerMovement.Instance.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, playerTransform.position.x + .5f, 10 * Time.deltaTime),
            transform.position.y,
            transform.position.z);
        //  transform.position = new Vector3(playerTransform.position.x+.5f, transform.position.y, transform.position.z);
    }
}
