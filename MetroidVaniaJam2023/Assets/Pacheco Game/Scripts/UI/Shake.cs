using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    Vector3 initialPosition;


    private void Start() {
        
    }
    

    public IEnumerator DamageShake(float shakeDuration, float shakeMagnitude) {
        initialPosition = transform.position;
        float elapsedTime = 0f;
        while(elapsedTime < shakeDuration) {
            transform.position = initialPosition + (Vector3)UnityEngine.Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }



}
