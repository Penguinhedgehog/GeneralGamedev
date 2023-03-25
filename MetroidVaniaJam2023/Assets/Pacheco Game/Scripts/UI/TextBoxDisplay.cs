using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBoxDisplay : MonoBehaviour
{
    //Attach the Scriptable Object set up here
    [SerializeField] TextBox textBox;

    [SerializeField] TMP_Text speaker;
    [SerializeField] TMP_Text dialogue;
    [SerializeField] Image textBoxImage; //May not be needed as a constant


    void Start()
    {
        speaker.text = textBox.GetSpeaker();
        dialogue.text = textBox.GetDialogue();
    }

}
