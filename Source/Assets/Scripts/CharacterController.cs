using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    float speed = 10;
    int energyCount;
    bool stopPlayer;

    Rigidbody playerRb;
    public GameObject timerText;
    public GameObject scoreText;
    public Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(energyCount >= 50)
        {
            SceneManager.LoadScene("WinScene");
        }
        if (energyCount < 0)
        {
            SceneManager.LoadScene("LoseScene");
        }
        if (GameManager.gmInstance.levelTime <= 0)
        {
            stopPlayer = true;
            SceneManager.LoadScene("LoseScene");
        }

        if (!stopPlayer)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                playerAnim.SetBool("isWalk", true);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0, 270, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                playerAnim.SetBool("isWalk", true);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                playerAnim.SetBool("isWalk", true);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                playerAnim.SetBool("isWalk", true);
            }

            if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                playerAnim.SetBool("isWalk", false);
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                playerAnim.SetBool("isWalk", false);
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                playerAnim.SetBool("isWalk", false);
            }
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerAnim.SetBool("isWalk", false);
            }
        }

        //UI displaying of Timer and Energy

        timerText.GetComponent<Text>().text = "Timer: " + (int)GameManager.gmInstance.levelTime;
        scoreText.GetComponent<Text>().text = "Energy: " + energyCount;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("AddEnergy"))
        {
            Destroy(collision.collider.gameObject);
            energyCount += 5;
            GameManager.gmInstance.levelTime += 1;
            GameManager.gmInstance.AddMoreEnergy();
        }

        if (collision.collider.gameObject.CompareTag("MinusEnergy"))
        {
            Destroy(collision.collider.gameObject);
            energyCount -= 25;
        }
    }
}
