using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject textBox;

    private int index;
    void Start()
    {
        instance = this;
        textBox.SetActive(false);
        textComponent.text = String.Empty;
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void StartDialogue()
    {
       
        textBox.SetActive(true);
        //index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            //index++;
            //textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
