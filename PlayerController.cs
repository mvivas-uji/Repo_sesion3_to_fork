using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables para controlar el salto del personaje mediante fuerzas aplicadas a su RigidBody.
    public Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool doubleJumpUsed = false;
    public float doubleJumpForce;

    //Variables para controlar el dash del personaje.
    public bool doubleSpeed = false;
    
    //Variables de chequeo
    public bool isGrounded; //Para comprobar que el personaje esté en el suelo y permitir el salto.
    public bool gameOver = false; //Para comprobar si el juego ha terminado;

    //Variables de referencia a componentes y otros objetos
    private Animator playerAnim; //Para referenciar el Animator del player.
    public ParticleSystem explosionParticle; //Variable para referenciar la explosión hija del player, lo referenciamos desde el editor de Unity.
    public ParticleSystem dirtParticle; //Variable para referenciar el polvo que sale al correr, referenciado desde Unity.
    public AudioClip jumpSound; //Variable para añadir sonido de salto, referenciado en el editor de Unity.
    public AudioClip crashSound; //Variable para añadir sonido de choque, referenciado en el editor de Unity.
    public AudioSource playerAudio; //Variable para referenciar el Audio Source del player (en el método Start).

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier; //Permite multiplicar el valor de la gravedad de toda la escena por el valor de la variable creada por nosotros.
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Dash();
    }

    //Método que determina el salto del personaje pulsando espacio.
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void Dash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            doubleSpeed = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (doubleSpeed)
        {
            doubleSpeed = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }
    }

    //Método de detecciones para comprobar si el personaje está en el suelo y si se entra en Game Over.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
