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
            //target�� x���� ���󰡰�, y�� z�� ����״�� ����.
            targetPosition = new Vector3(target.transform.position.x, transform.position.y,
                                                transform.position.z);

            if (target.transform.localScale.x > 0f)  //target�� ������ ���ϰ� ���� �� (�¿����X)
            {
                targetPosition = new Vector3(targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
                // x���� float ���� ���ؼ�, �÷��̾��� ��ġ�� ���ʿ� �����ϵ��� ������. 

            }
            else // tartget�� ������ ���ϰ� ���� �� (scale x�� ����, �¿����O)
            {
                targetPosition = new Vector3(targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
                // x������ float ���� ����, �÷��̾ �������� ������ ���� ��ġ�� ���ʿ� �����ϵ��� ������. 

            }

            //transform.position = targetPosition;
            //ī�޶��� ��ġ�� targetPosition���� ����

            //���� ��ġ, �̵��� ��ġ, ��ġ �̵��� �Ҹ��ϴ� �ð�
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
         


    }
}
