
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class Frame
    {
        public int frameNum = 1;
        public bool isStrike = false;
        public bool isSpare = false;
        public int frameScore = 0;
    }
    public int totalScore;

    public int currentFrame = 1;

    public BowlingBallSpawner bowlingBallSpawner;

    public Transform[] pinTransforms;

    public Vector3[] initialPinPositions;

    public Frame[] frames;

    // Start is called before the first frame update
    void Start()
    {
        initialPinPositions = new Vector3[pinTransforms.Length];

        for (int i = 0; i < initialPinPositions.Length; i++)
        {
            initialPinPositions[i] = pinTransforms[i].localPosition;
        }

        frames = new Frame[10];

        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame()
            {
                frameNum = i + 1
            };
            // frames[i].frameNum = i + 1;
        }
    }

    public void AddScoreToFrame(int scoreToAdd)
    {
        Debug.Log("ADD " + scoreToAdd + " TO THE SCORE");

        CheckFrame(scoreToAdd);
    }

    public void AddFrameToTotalScore()
    {
        totalScore += frames[currentFrame].frameScore;
    }

    public void NextFrame()
    {
        bowlingBallSpawner.numBallsThrownInCurrentFrame = 0;
        ResetPins();
        AddFrameToTotalScore();
        currentFrame++;
    }

    public void EndGame()
    {

    }

    public void CheckFrame(int scoreToAdd)
    {
        frames[currentFrame - 1].frameScore += scoreToAdd;

        //if only one ball was thrown
        if (bowlingBallSpawner.numBallsThrownInCurrentFrame == 1)
        {
            if (currentFrame > 1)
            {
                if (frames[currentFrame - 2].isSpare)
                {
                    frames[currentFrame - 2].frameScore += scoreToAdd;
                }
            }
            if (frames[currentFrame - 1].frameScore == 10)
            {

                if (currentFrame == 10)
                {
                    //throw another ball
                }
                else
                {
                    frames[currentFrame - 1].isStrike = true;
                    NextFrame();
                }
            }
        }
        //if 2nd ball was thrown
        else if (bowlingBallSpawner.numBallsThrownInCurrentFrame == 2)
        {
            if (currentFrame > 1)
            {
                if (frames[currentFrame - 2].isStrike)
                {
                    frames[currentFrame - 2].frameScore += frames[currentFrame - 1].frameScore;
                }
            }

            if (frames[currentFrame - 1].frameScore == 10)
            {
                if (currentFrame == 10)
                {
                    //throw a 3rd ball
                }
                else
                {
                    frames[currentFrame - 1].isSpare = true;
                    NextFrame();
                }
            }
            else
            {
                if (currentFrame == 10)
                {
                    CalculateTotalFrameScores();
                }
                else
                {
                    NextFrame();
                }
            }



            //if we are not in the last frame
            /*  if (currentFrame < 10)
              {
                  NextFrame();
                  bowlingBallSpawner.numBallsThrownInCurrentFrame = 0;
              }
              //if we are in the last frame
              else if (currentFrame == 10)
              {
                  //if 2 balls have been thrown and we haven't scored 10, or if we have thrown 3 balls
                  //then end the game
                  if ((bowlingBallSpawner.numBallsThrownInCurrentFrame == 2 && frameScore < 10) ||
                      (bowlingBallSpawner.numBallsThrownInCurrentFrame == 3))
                  {
                      EndGame();
                  }
              }*/
        }
        //if 3rd ball was thrown
        else if (bowlingBallSpawner.numBallsThrownInCurrentFrame == 3)
        {
            CalculateTotalFrameScores();
        }

        for (int i = 0; i < frames.Length; i++)
        {
            Debug.Log("FRAME NUM: " + frames[i].frameNum + "   FRAME SCORE: " + frames[i].frameScore);
        }

    }

    public void CalculateTotalFrameScores()
    {
        totalScore += frames[currentFrame - 1].frameScore;
        EndGame();
    }

    public void CheckPins()
    {
        int score = 0;
        for (int i = 0; i < pinTransforms.Length; i++)
        {
            if (Vector3.Dot(pinTransforms[i].up, Vector3.up) < 0.999f && pinTransforms[i].gameObject.activeSelf)
            {
                pinTransforms[i].gameObject.SetActive(false);
                // Debug.Log("PIN NAME: " + pinTransforms[i].gameObject.name + "   HAS FALLEN");
                score++;
            }
        }

        AddScoreToFrame(score);

    }

    public void ResetPins()
    {
        for (int i = 0; i < pinTransforms.Length; i++)
        {
            pinTransforms[i].gameObject.SetActive(true);
            pinTransforms[i].localPosition = initialPinPositions[i];
            pinTransforms[i].rotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
