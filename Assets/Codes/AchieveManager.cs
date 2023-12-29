using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNoti;

    enum Achieve { UnlockBo, UnlockKitty }
    Achieve[] achieves;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        achieves = Enum.GetValues(typeof(Achieve)) as Achieve[];
        wait = new WaitForSecondsRealtime(5);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }
    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++)
        {
            string achieveName = achieves[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }
    void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }
    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;
        switch (achieve)
        {
            case Achieve.UnlockBo:
                if (GameManager.instance.isAlive)
                    isAchieve = GameManager.instance.kill >= 20;
                break;
            case Achieve.UnlockKitty:
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }
        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);


            for (int i = 0; i < uiNoti.transform.childCount; i++)
            {
                bool isActive = (i == (int)achieve);
                uiNoti.transform.GetChild(i).gameObject.SetActive(isActive);
            }
            StartCoroutine(NotiRoutine());
        }
    }

    IEnumerator NotiRoutine()
    {
        uiNoti.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        yield return wait;
        uiNoti.SetActive(true);
    }
}
