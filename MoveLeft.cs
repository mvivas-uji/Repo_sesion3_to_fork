using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 30; //Variable que controla la velocidad de desplazamiento.
    private PlayerController playercScript; //Variable de referencia para script externo.
    private float leftBound = -15; //Variable para marcar el límite en el eje -x en el que los obstáculos se destruyen.

    // Start is called before the first frame update
    void Start()
    {
        playercScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercScript.gameOver == false)
        {
            if (playercScript.doubleSpeed)
            {
                transform.Translate(Vector3.left * (speed * 2) * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
