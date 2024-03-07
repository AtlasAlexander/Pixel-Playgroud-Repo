using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class tutorialController : MonoBehaviour
{
    TextMeshProUGUI tutorialText;
    GameObject Player;
    GameObject CheckpointController;
    Animator Anim;

    List<string> keyboardInstructions = new List<string>();
    List<string> controllerInstructions = new List<string>();

    int currentInstruction = 0;

    // Start is called before the first frame update
    void Start()
    {
        keyboardInstructions.Add("Use WASD to move");
        keyboardInstructions.Add("Use 1 and 2 to change weapon");
        keyboardInstructions.Add("Use left mouse click to shoot");
        keyboardInstructions.Add("Look at a cube and press E to pickup");
        keyboardInstructions.Add(""); //Gap Between Rooms
        keyboardInstructions.Add("Use right mouse click to zoom in");

        Player = GameObject.Find("Player");
        tutorialText = GetComponentInChildren<TextMeshProUGUI>();
        CheckpointController = GameObject.Find("Checkpoints");
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
            Controller();
        else
            KeyboardMouse();
    }

    void KeyboardMouse()
    {
        if (currentInstruction == 0 && Player.GetComponent<FirstPersonController>().isWalking)
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 1 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 2 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 3 && Player.GetComponent<NewGrabbing>().grab)
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (CheckpointController.GetComponent<CheckpointController>().GetCheckP() > 0 && currentInstruction < 5)
        {
            currentInstruction = 5;
            StartCoroutine(Transition());

            // tutorialText.text = keyboardInstructions[currentInstruction];
        }
        else if (currentInstruction == 5 && Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
    }

    void Controller()
    {
        if (currentInstruction == 0 && Player.GetComponent<FirstPersonController>().isWalking)
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 1 && Input.GetKeyDown("joystick button 4"))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 2 && Input.GetKeyDown("joystick button 33"))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (currentInstruction == 3 && Player.GetComponent<NewGrabbing>().grab)
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
        else if (CheckpointController.GetComponent<CheckpointController>().GetCheckP() > 0 && currentInstruction < 5)
        {
            currentInstruction = 5;
            StartCoroutine(Transition());

            // tutorialText.text = keyboardInstructions[currentInstruction];
        }
        else if (currentInstruction == 5 && Input.GetKeyDown("joystick button 32"))
        {
            currentInstruction++;
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        if(currentInstruction != 5)
        {
            tutorialText.color = Color.green;
            Anim.Play("Fade Out");
        }

        yield return new WaitForSeconds(1f);

        if (currentInstruction == 6)
            gameObject.SetActive(false);
        else
            tutorialText.text = keyboardInstructions[currentInstruction];

        tutorialText.color = Color.white;
        Anim.Play("Fade In");

        yield return new WaitForSeconds(1f);

        Anim.Play("Fade Idle");
    }
}