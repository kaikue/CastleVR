using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static SoundManagerScript S;

    public GameObject bellSoundObj;
    public GameObject explosionSoundObj;
    public GameObject loseSoundObj;
    public GameObject battleSoundObj;
    public GameObject playroomSoundObj;
    public GameObject enemyDeathSoundObj;
    public GameObject wilhelmSoundObj;
    public GameObject winSoundObj;
    public GameObject thwackSoundObj;

    private AudioSource bellSound;
    private AudioSource explosionSound;
    private AudioSource loseSound;
    private AudioSource battleSound;
    private AudioSource playroomSound;
    private AudioSource enemyDeathSound;
    private AudioSource wilhelmSound;
    private AudioSource winSound;
    private AudioSource thwackSound;


    // Start is called before the first frame update
    void Start()
    {
        S = this;

        bellSound = bellSoundObj.GetComponent<AudioSource>();
        explosionSound = explosionSoundObj.GetComponent<AudioSource>();
        loseSound = loseSoundObj.GetComponent<AudioSource>();
        battleSound = battleSoundObj.GetComponent<AudioSource>();
        playroomSound = playroomSoundObj.GetComponent<AudioSource>();
        enemyDeathSound = enemyDeathSoundObj.GetComponent<AudioSource>();
        wilhelmSound = wilhelmSoundObj.GetComponent<AudioSource>();
        winSound = winSoundObj.GetComponent<AudioSource>();
        thwackSound = thwackSoundObj.GetComponent<AudioSource>();
    }

    //bell sound
    public void MakeBellSound()
    {
        bellSound.Play();
    }
    public void StopBellSound()
    {
        bellSound.Stop();
    }
    //explosion sound
    public void MakeExplosionSound()
    {
        explosionSound.Play();
    }
    public void StopExplosionSound()
    {
        explosionSound.Stop();
    }
    //lose sound
    public void MakeLoseSound()
    {
        loseSound.Play();
    }
    public void StopLoseSound()
    {
        loseSound.Stop();
    }
    // battle sound
    public void MakeBattleSound()
    {
        battleSound.Play();
    }
    public void StopBattleSound()
    {
        battleSound.Stop();
    }
    //playroom sound
    public void MakePlayroomSound()
    {
        playroomSound.Play();
    }
    public void StopPlayroomSound()
    {
        playroomSound.Stop();
    }
    // enemy death sound
    public void MakeEnemyDeathSound()
    {
        enemyDeathSound.Play();
    }
    public void StopEnemyDeathSound()
    {
        enemyDeathSound.Stop();
    }
    //wilhelm sound
    public void MakeWilhelmSound()
    {
        wilhelmSound.Play();
    }
    public void StopWilhelmSound()
    {
        wilhelmSound.Stop();
    }
    //win sound
    public void MakeWinSound()
    {
        winSound.Play();
    }
    public void StopWinSound()
    {
        winSound.Stop();
    }
    //thwack sound
    public void MakeThwackSound()
    {
        thwackSound.Play();
    }
    public void StopThwackSound()
    {
        thwackSound.Stop();
    }
}
