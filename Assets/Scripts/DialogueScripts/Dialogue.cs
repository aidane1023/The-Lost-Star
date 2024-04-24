using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent, nameComponent;
    public TextMeshProUGUI[] buttonComponent;
    public GameObject[] dialogueButton;
    public int dialogueButtonCount;
    public bool hasDialogueOptions;
    public string[] buttonText, talkingName, lines;
    public bool[] effect, leftIsTalking; 
    public GameObject[] leftPortrait, rightPortrait;
    private Image leftImage, rightImage;
    public float textSpeed;
    private int index, buttonIndex;

    Color notTalkingColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    Color talkingColor = new Color(1f, 1f, 1f, 1f);
    Color noCharacterColor = new Color(0f, 0f, 0f, 0f);

    public int defaultTextSize;
    public int effectTextSize;
    public bool dialogueEnded = false;

    public AudioSource typewriterSource;

    // Start is called before the first frame update
    public void DialogueTriggered()
    {
        index = 0;
        dialogueEnded = false;
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        leftPortrait[index].SetActive(true);
        rightPortrait[index].SetActive(true);

        foreach (char c in talkingName[index].ToCharArray())
        {
            nameComponent.text += c;
        }
        
        if (hasDialogueOptions == true)
        {
            for (int i = 0; i < dialogueButtonCount; i++)
            {
                dialogueButton[i].SetActive(false);
                buttonComponent[i].text = string.Empty;
                buttonComponent[i].text = buttonText[i];
            }
        }

        BeginDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

        if (textComponent.text == lines[index])
        {
            typewriterSource.Stop();
        }
    }

    void BeginDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        typewriterSource.Play();

        Debug.Log(index);

        //Effect Code
        if (effect[index] == true)
        {
            textComponent.fontSize = effectTextSize;
        }
        else
        {
            textComponent.fontSize = defaultTextSize;
        }
        //Character Portrait Code
        if (leftIsTalking[index] == true) 
        {
            leftImage = leftPortrait[index].GetComponent<Image>();
            leftImage.color = talkingColor;
            rightImage = rightPortrait[index].GetComponent<Image>();
            rightImage.color = notTalkingColor;
        }
        else
        {
            rightImage = rightPortrait[index].GetComponent<Image>();
            rightImage.color = talkingColor;
            leftImage = leftPortrait[index].GetComponent<Image>();
            leftImage.color = notTalkingColor;
        }
       foreach (char c in lines[index].ToCharArray())
       {
           textComponent.text += c;
           yield return new WaitForSeconds(textSpeed);
       }
       if (textComponent.text == lines[index] && hasDialogueOptions == true)
       {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                for (int i = 0; i < dialogueButtonCount; i++)
                {
                    dialogueButton[i].SetActive(true);
                }
                
            }
       }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            
            leftPortrait[index].SetActive(false);
            rightPortrait[index].SetActive(false);
            index++;
            leftPortrait[index].SetActive(true);
            rightPortrait[index].SetActive(true);

            textComponent.text = string.Empty;
            nameComponent.text = string.Empty;

            foreach (char c in talkingName[index].ToCharArray())
            {
                nameComponent.text += c;
            }

            StartCoroutine(TypeLine());
        }
        else
        {
            if (hasDialogueOptions == true)
            {
                for (int i = 0; i < dialogueButtonCount; i++)
                {
                    dialogueButton[i].SetActive(true);
                    buttonComponent[i].text = string.Empty;
                    buttonComponent[i].text = buttonText[i];
                    Debug.Log("Dialogue " + i + " Active");
                }
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(dialogueButton[0]);
            }
            else
            {
                //textComponent.text = string.Empty;
                //index = 0;
                gameObject.SetActive(false);
                leftPortrait[index].SetActive(false);
                rightPortrait[index].SetActive(false);
            }

        }
    }

    public void EndDialogue()
    {
        dialogueEnded = true;
        gameObject.SetActive(false);
        leftPortrait[index].SetActive(false);
        rightPortrait[index].SetActive(false);
    }
}
