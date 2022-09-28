using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Variables para determinar la puntuación y referenciar al jugador para determinar si estamos en Game Over o no.
    public float score;
    private PlayerController playerCScript;

    //Variables para determinar la animación de inicio.
    public Transform startingPoint;
    public float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerCScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        playerCScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCounter();
    }

    void ScoreCounter()
    {
        if (!playerCScript.gameOver)
        {
            if (playerCScript.doubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log("Score: " + score);
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerCScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerCScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerCScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        playerCScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerCScript.gameOver = false;
    }
}
