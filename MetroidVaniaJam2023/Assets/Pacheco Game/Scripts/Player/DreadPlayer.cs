using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadPlayer : MonoBehaviour
{
    [SerializeField][Range(0,100)] int dread = 100;
    
    PlayerMovement player;
    float timeStart;


    private void Start() {
        player = GetComponent<PlayerMovement>();
        timeStart = Time.time;
    }

    private void Update() {
        if(dread >= 1 && Time.time - timeStart > 10) {
            timeStart = Time.time;
            dread--;
        }
    }


    public int GetDreadValue() {
        return dread;
    }

    public void AddDread(int addAmount) {
        dread += addAmount;
    }



}
