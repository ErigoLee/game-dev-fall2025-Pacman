using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip freezingEffect;
    [SerializeField] private AudioClip attackEffect;
    [SerializeField] private AudioClip damageEffect;
    [SerializeField] private AudioClip getScoreEffect;

    
    private void OnEnable(){
        GameManager.GameEnd += PlaySoundGameEnd;
        GameManager.GameOver += PlaySoundGameOver;
        GameManager.FreezingEventStart += PlaySoundFreezing;
        GameManager.AttackEventStart += PlaySoundAttack;
        GameManager.DamageEventStart += PlaySoundDamage;
        GameManager.GetScoreEvent += PlaySoundGetScore;

    }
    private void OnDisable(){
        GameManager.GameEnd -=PlaySoundGameEnd;
        GameManager.GameOver -= PlaySoundGameOver;
        GameManager.FreezingEventStart -= PlaySoundFreezing;
        GameManager.AttackEventStart -= PlaySoundAttack;
        GameManager.DamageEventStart -= PlaySoundDamage;
        GameManager.GetScoreEvent -= PlaySoundGetScore;
    }

    private void PlaySoundGameEnd(object c){

    }

    private void PlaySoundGameOver(object c){

    }

    private void PlaySoundFreezing(object c){
        GameObject freezingObj = new GameObject("Freezing Effect Object");
        freezingObj.transform.position = transform.position;
        freezingObj.transform.rotation = transform.rotation;
        AudioSource freezingSound = freezingObj.AddComponent<AudioSource>();
        freezingSound.clip = freezingEffect;
        freezingSound.Play();

        Destroy(freezingObj, 1.0f);
    }

    private void PlaySoundAttack(object c){
        GameObject attackObj = new GameObject("Attack Effect Object");
        attackObj.transform.position = transform.position;
        attackObj.transform.rotation = transform.rotation;
        AudioSource attackSound = attackObj.AddComponent<AudioSource>();
        attackSound.clip = attackEffect;
        attackSound.Play();

        Destroy(attackObj, 1.0f);
    }

    private void PlaySoundDamage(object c){
        GameObject damageObj = new GameObject("Damage Effect Object");
        damageObj.transform.position = transform.position;
        damageObj.transform.rotation = transform.rotation;
        AudioSource damageSound = damageObj.AddComponent<AudioSource>();
        damageSound.clip = damageEffect;
        damageSound.Play();

        Destroy(damageObj, 1.0f);
    }

    private void PlaySoundGetScore(object c, int score){
        GameObject scoreObj = new GameObject("Get Score Effect Object");
        scoreObj.transform.position = transform.position;
        scoreObj.transform.rotation = transform.rotation;
        AudioSource scoreSound = scoreObj.AddComponent<AudioSource>();
        scoreSound.clip = getScoreEffect;
        scoreSound.Play();

        Destroy(scoreObj, 1.0f);
    }
}
