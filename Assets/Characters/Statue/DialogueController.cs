using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public GameObject contButton;
    public GameObject skipButton;
    public GameObject controlsPanel;
    private PlayerController player;

    public string[] dialogue = new string[18] {
        "Hello, hero, I am th-",
        "Oh.",
        "You have no clothes on.",
        "...",
        "Sorry, I didn't mean to do that.",
        "Anyway... I am the goddess of this realm, and this is my temple.",
        "Dark forces are threatening to take over this land. This temple is the last bastion",
        "of light.", 
        "I have created you, my champion, to help defend it.", 
        "This statue is the conduit of my power. Through it, I can put up a barrier to keep out", 
        "beings of evil.",
        "But I need time to charge my power.",
        "Defend my statue from the beings that would destroy it and make this place a",
        "sanctuary for the good and righteous.",
        "Their numbers are great, but as a being of good, they cannot hurt you.",
        "However, be warned hero. You are only an avatar made from my light. If you leave my",
        "reach, you will cease to exist and the temple will be lost.",
        "Be vigilant and strike true, hero! And may victory be ours!"
    };
    private int index;
    public float wordSpeed = 0.04f;
    private Coroutine typingCoroutine;
    public bool playerIsClose;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<PlayerController>();
        dialoguePanel.SetActive(false);
        controlsPanel.SetActive(true);
        zeroText();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose){
            // start dialogue if player is close and presses E
            controlsPanel.SetActive(false);
            dialoguePanel.SetActive(true);
            player.canMove = false;
            player.canAttack = false;
            StartDialogue();
        } else if (dialoguePanel.activeInHierarchy) {
            // check for input to advance dialogue if panel is active
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
                NextLine();
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                EndDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsClose = false;
        }
    }

    public void StartDialogue() {
        index = 0;
        dialoguePanel.SetActive(true);
        typingCoroutine = StartCoroutine(Typing());
    }

    public void EndDialogue() {
        StopAllCoroutines();
        zeroText();
        dialoguePanel.SetActive(false);
        player.canMove = true;
        player.canAttack = true;
        SceneManager.LoadScene("SampleScene"); // start game
    }

    public void NextLine() {
        if (typingCoroutine != null) {
            // complete current line if player tries to go next line while typing
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogue[index]; 
            typingCoroutine = null;
        } else if (index < dialogue.Length - 1) {
            // go to next line
            index++;
            dialogueText.text = "";
            typingCoroutine = StartCoroutine(Typing());
        } else { 
            EndDialogue();
        }
    }

    public void zeroText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing(){
        foreach (char letter in dialogue[index].ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        typingCoroutine = null;
    }    
}
