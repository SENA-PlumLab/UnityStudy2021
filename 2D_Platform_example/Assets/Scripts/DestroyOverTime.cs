using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifetime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime = 1�������� ����� �� �Ҹ�Ǵ� �ð�.  (fps�� ���� �ٸ�)
        // �׻� ���� ������ �뷫���� ��� ���� ���� �� ����
        lifetime = lifetime - Time.deltaTime;

        if(lifetime <= 0f)
        {
            Destroy(gameObject); //delete this object from the world
        }
        
    }
}
