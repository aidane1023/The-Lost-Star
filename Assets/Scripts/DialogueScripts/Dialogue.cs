using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent, nameComponent;
    public string[] lines, talkingName;
    public bool[] effect, leftIsTalking;
    public GameObject[] leftPortrait, rightPortrait;
    private Image leftImage, rightImage;
    public float textSpeed;
    private int index;

    Color notTalkingColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    Color talkingColor = new Color(1f, 1f, 1f, 1f);
    Color noCharacterColor = new Color(0f, 0f, 0f, 0f);

    public int defaultTextSize;
    public int effectTextSize;

    // Start is called before the first frame update
    public void DialogueTriggered()
    {
        index = 0;
        textComponent.text = string.Empty;
        nameComponent.text = string.Empty;
        leftPortrait[index].SetActive(true);
        rightPortrait[index].SetActive(true);

        foreach (char c in talkingName[index].ToCharArray())
        {
            nameComponent.text += c;
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
    }

    void BeginDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
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

        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
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
            //textComponent.text = string.Empty;
            //index = 0;
            gameObject.SetActive(false);
            leftPortrait[index].SetActive(false);
            rightPortrait[index].SetActive(false);

        }
    }
}
