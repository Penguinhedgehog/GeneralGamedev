using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New TextBox", menuName = "Textbox")]
public class TextBox : ScriptableObject
{
    [Header("Display")]
    [SerializeField] string speaker;
    [SerializeField] string dialogue;


    [Header("Settings")]
    [SerializeField] TextBox nextDialogue;
    [SerializeField] bool endOfDialogue;

    public string GetSpeaker() {
        return speaker;
    }

    public string GetDialogue() {
        return dialogue;
    }


}
