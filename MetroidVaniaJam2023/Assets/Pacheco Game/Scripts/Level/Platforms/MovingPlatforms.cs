using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] bool isActive = false;
    [Tooltip("Platform moves once then stops")]
    [SerializeField] bool stopAtEndPoints = true;
    [SerializeField] List<Vector2> positions = new List<Vector2>();
    [Tooltip("Changes the movement type from default")]
    [SerializeField] bool isLoop = false;
    

    int positionIndex = 0;
    bool oscillateHitEnd = false;
    bool stopFlag = false;


    [Header("LoopSettings (Optional)")]
    [SerializeField] bool stopMidLoop = false;
    [SerializeField] List<int> stoppingPoints;
    
    private void Awake() {
        positions.Insert(0, transform.position);

        if(stoppingPoints.Contains(-1) && isLoop) { isActive = true;} //Bug Band-aid
        if(stopAtEndPoints) { isActive = true;} //Bug band-aid
    }

    private void Update() {
        if(!isActive) { return; }
        if(isLoop) {
            LoopPositions();
        } else {
            OscillatePositions();
        }
    }

    public void SwitchActive() {
        isActive = !isActive;
    }




    private void LoopPositions() {

        Vector3 targetPosition = positions[positionIndex];
        float delta = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

        if(transform.position == targetPosition) {
            if(stopMidLoop && stoppingPoints.Contains(positionIndex-1)) {
                Debug.Log(positionIndex);
                isActive = false;
            }
            if(positionIndex == positions.Count-1 ) {
                positionIndex = 0;
            } else {
                positionIndex++;
            }

        }
    }

    
    private void OscillatePositions() {
        Vector3 targetPosition = positions[positionIndex];
        float delta = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);

        if(positionIndex == positions.Count-1 ) {
            oscillateHitEnd = true;
            if(stopAtEndPoints) { stopFlag = true; }
        } else if (positionIndex == 0) {
            oscillateHitEnd = false;
            if(stopAtEndPoints) { stopFlag = true; }
        }

        if(transform.position == targetPosition) {

            if(!oscillateHitEnd) {
                positionIndex++;
            } else if(oscillateHitEnd) {
                positionIndex--;
            }   
            if(stopFlag) {
                isActive = false;
                stopFlag = false;
            }
        }
    }


}
