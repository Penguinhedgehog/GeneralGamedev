using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    
    [SerializeField] int damage = 2;
    [SerializeField] bool isProjectile;
    [SerializeField] int dreadDamage = 0;
    [SerializeField] AudioClip impactSound;

    AudioPlayer audioPlayer;

    private void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public int GetDamage() {
        return damage;
    }

    public int GetDreadDamage() {
        return dreadDamage;
    }


    //Destroys damage projectile
    public void Hit() {
        if(!isProjectile) { return; }
        if(impactSound != null) {
            audioPlayer.PlayAudioClip(impactSound);
        }
        Destroy(gameObject);
    }







}
