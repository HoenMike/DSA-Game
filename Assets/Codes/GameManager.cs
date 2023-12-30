/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//* Singleton GameManager Class to store Universal game Data and control Game Logic *//
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Manager")]
    public bool isAlive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 }; //using queue

    [Header("# Game Objects")]

    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    //* Unity's Functions *//
    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameWin();
        }
    }

    //* Game State Handler *//
    public void GameStart(int playerId)
    {
        this.playerId = playerId;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }
    public void GameWin()
    {
        StartCoroutine(GameWinRoutine());
    }
    IEnumerator GameWinRoutine()
    {
        isAlive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isAlive = false;
        yield return new WaitForSeconds(.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    public void GetExp()
    {
        if (!isAlive)
            return;

        exp++;
        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
    public void Stop()
    {
        isAlive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isAlive = true;
        Time.timeScale = 1;
    }
}