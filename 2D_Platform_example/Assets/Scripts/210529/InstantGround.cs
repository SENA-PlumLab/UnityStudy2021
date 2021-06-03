using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantGround : MonoBehaviour
{
    public float waitToDestroy;
    public int maxGroundable;
    public int groundedCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (groundedCount >= maxGroundable)
        {
            DestroyInstantGround();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            groundedCount++;

        }
    }

    public void DestroyInstantGround()
    {
        StartCoroutine("DestroyCo");
    }
    public IEnumerator DestroyCo()
    {
        //yield 게임 잠시 멈추기... 
        yield return new WaitForSeconds(waitToDestroy);
        groundedCount = 0;
        gameObject.SetActive(false);
    }
}
