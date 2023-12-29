using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AchieveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;

    enum Achieve { UnlockBo, UnlockKitty }
    Achieve[] achieves;

    void Awake()
    {
        achieves = Enum.GetValues(typeof(Achieve)) as Achieve[];

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
                isAchieve = GameManager.instance.kill >= 10;
                break;
            case Achieve.UnlockKitty:
                isAchieve = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }
        if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 1);
        }
    }
}
