using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float lifetime;
    

    private void Awake() {
        Destroy(gameObject, lifetime);
    }

    public float GetLifeTime() {
        return lifetime;
    }


}
