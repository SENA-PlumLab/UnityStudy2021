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
        // Time.deltaTime = 1프레임이 재생될 때 소모되는 시간.  (fps에 따라 다름)
        // 항상 같지 않지만 대략적인 평균 값을 읽을 수 있음
        lifetime = lifetime - Time.deltaTime;

        if(lifetime <= 0f)
        {
            Destroy(gameObject); //delete this object from the world
        }
        
    }
}
