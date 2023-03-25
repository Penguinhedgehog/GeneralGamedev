using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeCatchers : MonoBehaviour
{
    
    [SerializeField] List<GameObject> eyeCatchList;
    [SerializeField] int frame = 0;


    public void GetFrame(int frameIndex) {
        frame = frameIndex;
    }

    public void ActivateFrame() {
        eyeCatchList[frame].SetActive(true);
    }

    public void DeactivateFrame() {
        eyeCatchList[frame].SetActive(false);
    }


}
