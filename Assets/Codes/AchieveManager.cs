/* Name: #20
 Mai Nguyen Hoang - ITITIU21208
 Purpose: A vampire survivors clone that implements DSA.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

//* Manages the achievements and unlocking of characters in the game *//
public class AchieveManager : MonoBehaviour
{
    enum Achieve { UnlockBo, UnlockKitty }
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNoti;
    Achieve[] achieves;
    WaitForSecondsRealtime wait;

    //* Unity Function *//

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        achieves = Enum.GetValues(typeof(Achieve)) as Achieve[];
        wait = new WaitForSecondsRealtime(5);

        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    // Start is called before the first frame update.
    void Start()
    {
        UnlockCharacter();
    }

    // LateUpdate is called once per frame, after all Update functions have been called.
    void LateUpdate()
    {
        foreach (Achieve achieve in achieves)
        {
            CheckAchieve(achieve);
        }
    }

    // Initializes the player's data and achievements.
    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (Achieve achieve in achieves)
        {
            PlayerPrefs.SetInt(achieve.ToString(), 0);
        }
    }

    //* Custom Function *//
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

    void CheckAchieve(Achieve achieve)
    {
        bool isAchieve = false;
        switch (achieve)
        {
            case Achieve.UnlockBo:
                if (GameManager.instance.isAlive)
                    isAchieve = GameManager.instance.kill >= 30;
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
        uiNoti.SetActive(false);
    }
}
