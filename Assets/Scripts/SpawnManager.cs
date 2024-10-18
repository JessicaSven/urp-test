using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ballPrefab;

    private Camera mainCam;

    private const float MAX_FORCE = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            Vector3 targetPos = mainCam.ScreenToWorldPoint(mousePos);
            Debug.Log("TARGET POS: " + targetPos);

            GameObject ball = Instantiate(ballPrefab, targetPos, Quaternion.identity);

            float randomX = Random.Range(-MAX_FORCE, MAX_FORCE);
            float randomY = Random.Range(-MAX_FORCE, MAX_FORCE);
            float randomZ = Random.Range(-MAX_FORCE, MAX_FORCE);

            Vector3 randomDirection = new Vector3(randomX, randomY, randomZ);

            ball.GetComponent<Rigidbody>().AddForce(randomDirection, ForceMode.Impulse);

        }
    }
}