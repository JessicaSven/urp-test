using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlnigBallSpawner : MonoBehaviour
{
    public Animator arrowAnim;
    public GameObject[] bowlingBalls;
    public float speed;

    public float arrowSpeed = 0.2f;
    private const float MAX_XPOS = 4.2f;

    private const string arrowAnimBallThrown = "WasBallThrown";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SPEED: " + speed);
    }

    // Update is called once per frame
    void Update()
    {
        //Transform arrowChildTransform = arrowAnim.transform.GetChild(0).transform;

        //arrowChildTransform.localPosition = new Vector3(Mathf.Clamp(arrowChildTransform.localPosition.x + horizontalMovement, -MAX_XPOS, MAX_XPOS), arrowChildTransform.localPosition.y, arrowChildTransform.localPosition.z);

        float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime;

        if (horizontalMovement != 0.0f)
        {
            arrowAnim.SetBool(arrowAnimBallThrown, false);

        }

        arrowAnim.transform.Translate(arrowSpeed * Time.deltaTime * horizontalMovement, 0.0f, 0.0f);

        //Vector3 localpos = arrowAnim.transform.localPosition;

        //float localPosX = localpos.x;

        //float ClampedLocalPosX = Mathf.Clamp(localPosX, -4.2f, 4.2f);

        //Vector3 clampedPos = new Vector3 (ClampedLocalPosX, localpos.y, localpos.z);

        //arrowAnim.transform.localPosition = clampedPos;

        //arrowAnim.transform.localPosition = new Vector3(Mathf.Clamp(arrowAnim.transform.localPosition.x, -4.2f, 4.2f), arrowAnim.transform.localPosition.y, arrowAnim.transform.localPosition.z);
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            arrowAnim.SetBool(arrowAnimBallThrown, false);

            arrowAnim.SetBool("Aiming", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            arrowAnim.SetBool("Aiming", false);

            arrowAnim.SetBool("WasFallThrown", true);


            int randomNum = Random.Range(0, 8);

            Debug.Log("RANDOM NUM: " + randomNum);

            GameObject ball = Instantiate(bowlingBalls[randomNum], arrowAnim.transform.position, transform.rotation);

            if (ball.GetComponent<Rigidbody>() != null)
            {
                ball.GetComponent<Rigidbody>().AddForce(arrowAnim.transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            }

        }
    }
}
