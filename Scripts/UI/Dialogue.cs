using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent, textPressE;

    [TextArea(7,7)]
    [SerializeField] string[] lines;

    [SerializeField] float textSpeed;
    AudioSource dialogueSound;
    int index;

    void OnEnable() {
        textPressE.gameObject.SetActive(true);
        dialogueSound = GameObject.Find("DialogueSound").GetComponent<AudioSource>();
        //StartDialogue();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E)){
            if (textComponent.text == lines[index]){
                NextLine();
            } else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }*/
    }

    public void StartDialogue(){
        dialogueSound.Play();
        textComponent.text = string.Empty;
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine(){
        if (index < lines.Length - 1){
            dialogueSound.Play();
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else{
            textPressE.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            if(textComponent.text == lines[index]){
                Invoke("NextLine", 1.75f);
            }
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
