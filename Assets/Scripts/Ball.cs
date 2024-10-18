using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;

    private int score = 0;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PitFloor")
        {
            //Pin[] pins = FindObjectsOfType<Pin>();

            StopAllCoroutines();
            StartCoroutine(CheckPinsAfterDelay());
        }

    }

    IEnumerator CheckPinsAfterDelay()
    {
        yield return new WaitForSeconds(3.0f);
        gameManager.CheckPins();
    }

    // Update is called once per frame
    void Update()
    {

    }
}