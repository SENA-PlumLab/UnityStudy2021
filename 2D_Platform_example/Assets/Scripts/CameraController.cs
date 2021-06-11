using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject target;
    public float followAhead;

    private Vector3 targetPosition;

    public float smoothing;

    public bool followtarget;

    // Start is called before the first frame update
    void Start()
    {
        followtarget = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (followtarget)
        {
            //target의 x값을 따라가고, y와 z는 현행그대로 유지.
            targetPosition = new Vector3(target.transform.position.x, transform.position.y,
                                                transform.position.z);

            if (target.transform.localScale.x > 0f)  //target이 우측을 향하고 있을 때 (좌우반전X)
            {
                targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
                // x값에 float 값을 더해서, 플레이어의 위치를 왼쪽에 고정하도록 수정함. 

            }
            else // tartget이 좌측을 향하고 있을 때 (scale x가 음수, 좌우반전O)
            {
                targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
                // x값에서 float 값을 빼서, 플레이어가 왼쪽으로 움직일 때도 위치를 왼쪽에 고정하도록 수정함. 

            }

            //transform.position = targetPosition;
            //카메라의 위치를 targetPosition으로 설정

            //원래 위치, 이동할 위치, 위치 이동에 소모하는 시간
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
         


    }
}
