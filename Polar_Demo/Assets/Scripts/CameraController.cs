using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform farBackground, middleBackground;

    //private float lastXPos; //last x position
    private Vector2 lastPos;

    public float minHeight, maxHeight;



    // Start is called before the first frame update
    void Start()
    {
        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        /*
        //x만 target 따라 움직임. y, z는 그대로.
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        */
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);


        //float amountToMoveX = transform.position.x - lastXPos; //플레이어와 exactly same speed로 움직임
        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);


        farBackground.position = farBackground.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;

        //lastXPos = transform.position.x;
        lastPos = transform.position;
    }
}
