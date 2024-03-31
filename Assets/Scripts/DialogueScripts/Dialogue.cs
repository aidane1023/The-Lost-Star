using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public bool[] effect;
    public float textSpeed;
    private int index;

    public int defaultTextSize;
    public int effectTextSize;

    // Start is called before the first frame update
    public void DialogueTriggered()
    {
        textComponent.text = string.Empty;
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
        if (effect[index] == true)
        {
            textComponent.fontSize = effectTextSize;
        }
        else
        {
            textComponent.fontSize = defaultTextSize;
        }
        Debug.Log(index);
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
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            //textComponent.text = string.Empty;
            //index = 0;
            gameObject.SetActive(false);
        }
    }
}
