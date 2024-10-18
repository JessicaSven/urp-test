using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBallSpawner : MonoBehaviour
{
    public SphereScript mySphereScript;
    public Animator arrowAnim;
    public GameObject[] bowlingBalls;
    public float speed;

    public float arrowSpeed = 1.0f;

    private const string arrowAnimBallThrown = "WasBallThrown";

    public int numBallsThrownInCurrentFrame = 0;

    public GameManager gameManager;

    private void Awake()
    {

    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //move arrow based on horizontal axis input
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (horizontalMovement != 0.0f)
        {
            arrowAnim.SetBool(arrowAnimBallThrown, false);
        }

        arrowAnim.transform.Translate(arrowSpeed * Time.deltaTime * horizontalMovement, 0.0f, 0.0f);

        Vector3 localpos = arrowAnim.transform.localPosition;

        float localPosX = localpos.x;


        arrowAnim.transform.localPosition = new Vector3(arrowAnim.transform.localPosition.x,
            arrowAnim.transform.localPosition.y, arrowAnim.transform.localPosition.z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            arrowAnim.SetBool(arrowAnimBallThrown, false);


            arrowAnim.SetBool("Aiming", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            arrowAnim.SetBool("Aiming", false);
            arrowAnim.SetBool(arrowAnimBallThrown, true);


            int randomNum = Random.Range(0, 5);

            Debug.Log("RANDOM NUM: " + randomNum);

            GameObject ball = Instantiate(bowlingBalls[0], arrowAnim.transform.position, transform.rotation);
            if (ball.GetComponent<Rigidbody>() != null)
            {
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Impulse);
            }

            numBallsThrownInCurrentFrame++;


        }
    }


}