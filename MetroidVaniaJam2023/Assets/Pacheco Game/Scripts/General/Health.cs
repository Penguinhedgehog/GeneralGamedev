using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] bool isPlayer;
    [SerializeField] int maxHealth = 100;
    [SerializeField][Range(0,100)] int health = 100;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip injuredSoundEffect;
    [SerializeField] AudioClip deathSoundEffect;


    PlayerMovement player;
    SceneManagement sceneManagement;
    AudioPlayer audioPlayer;
    Renderer render;
    Color transparency;
    DreadPlayer dreadScript;
    bool damageable = true;


    Vector3 initialPosition;
    float shakeDuration = .5f;
    float shakeMagnitude = .5f;



    private void Awake() {
        if(isPlayer) {
            player = GetComponent<PlayerMovement>();
            render = GetComponent<Renderer>();
            transparency = render.material.color;
            sceneManagement = FindObjectOfType<SceneManagement>();
            dreadScript  = GetComponent<DreadPlayer>();
        }
        
        audioPlayer = FindObjectOfType<AudioPlayer>();

    }
    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == gameObject.tag) {return;}
        if(!damageable) { return; }
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null) {
            PlayHitEffect();
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();

            if(isPlayer) {
                TakeDreadDamage(damageDealer.GetDreadDamage());
                StartCoroutine(player.PlayerKick(other.transform.position));

                StartCoroutine(PlayerInvincibility());                
            }
        }
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    public void HealHealth(int healAmount) {
        if(healAmount + health >= maxHealth) {
            health = maxHealth;
        } else {
            health += healAmount;
        }
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if(health <= 0) {
            Die();
            audioPlayer.PlayCustomAudioClip(deathSoundEffect, gameObject.transform.position);
        }
        if(health > 0) {
            audioPlayer.PlayCustomAudioClip(injuredSoundEffect, gameObject.transform.position);
        }

    }

    public void TakeDreadDamage(int dreadDamage) {
        dreadScript.AddDread(dreadDamage);
    }


    private void PlayHitEffect() {
        if(hitEffect != null) {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            //Audio Code would go here for on hit sounds
        }
        if (!isPlayer) {
            StartCoroutine(DamageShake());
        }
    }

    
    private IEnumerator DamageShake() {
        SpriteRenderer enemySprite = GetComponent<SpriteRenderer>();
        Color defaultColor = new Color(1f, 1f, 1f);
        enemySprite.color = new Color(1f, .5f, .5f);
        gameObject.transform.localScale = new Vector3(.8f, 1f, 1f);
        

        initialPosition = transform.position;
        float elapsedTime = 0f;
        while(elapsedTime < shakeDuration) {
            transform.position = initialPosition + (Vector3)UnityEngine.Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        enemySprite.color = defaultColor;
        transform.position = initialPosition;
    }


    private IEnumerator PlayerInvincibility() {
        transparency.a = 0.3f;
        render.material.color = transparency;

        damageable = false;
		yield return new WaitForSeconds (2f);
		damageable = true;

        transparency.a = 1f;
        render.material.color = transparency;

        if(player.GetIsDead()) {
            sceneManagement.GameOverUI();
        }
    }

    private void Die()
    {
        if(isPlayer) {
            player.PlayerDied();
            //StartCoroutine(audioPlayer.FadeOutMusic());
            audioPlayer.PlayMusicTrack(8);
            Debug.Log("Player DIED!");
            //Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
        //If Enemies drop stuff we would implement a portion here
    }




}
