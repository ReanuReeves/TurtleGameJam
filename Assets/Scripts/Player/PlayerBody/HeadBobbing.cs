using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float baseBobbingSpeed = 10f;
    public float runningBobbingSpeed = 20f;
    float currentBobbingSpeed;

    public float bobbingAmount = 0.05f;
    
    public PlayerMovement controller;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isRunning) {
            currentBobbingSpeed = runningBobbingSpeed;
        }
        else {
            currentBobbingSpeed = baseBobbingSpeed;
        }

        if(Mathf.Abs(controller.moveDirection.x) > 0.1f && controller.enabled || Mathf.Abs(controller.moveDirection.z) > 0.1f && controller.enabled)
        {
            //Player is moving
            timer += Time.deltaTime * currentBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * currentBobbingSpeed), transform.localPosition.z);
        }
    }
}