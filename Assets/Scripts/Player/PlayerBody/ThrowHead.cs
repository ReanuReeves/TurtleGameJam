using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHead : MonoBehaviour
{
    public GameObject headPrefab;
    public Transform headSpawnPoint;
    bool isHeadAlive = false;
    // add a public float for the charge time
    float chargeTime = 0.0f;
    public float maxChargeTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the left mouse button is pressed down the player will charge up a throw
        if (Input.GetMouseButton(0) && !isHeadAlive)
        {   
            chargeTime += Time.deltaTime;
            // if the charge time is greater than 1.0f, set it to 1.0f
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;
            }
        }

        // if the left mouse button is released the player will throw the head
        if (Input.GetMouseButtonUp(0) && chargeTime > 0.2f && !isHeadAlive)
        {
            isHeadAlive = true;
            // instantiate the head
            GameObject newHead = Instantiate(headPrefab, headSpawnPoint.position, transform.rotation);
            // add force to the head
            newHead.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * chargeTime * 1000);
            // reset the charge time
            chargeTime = 0.0f;
        }
        else if (Input.GetMouseButtonUp(0) && chargeTime <= 0.2f && !isHeadAlive)
        {
            chargeTime = 0.0f;
        }
    }


    public void SetHeadFalse()
    {
        isHeadAlive = false;
    }
}
