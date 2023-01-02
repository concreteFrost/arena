using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public GameEventSO playerDeath;
    public float levelTimer ;
    public int hostagesCount;

    public TMP_Text hostagesCountUI;
    public TMP_Text timeLeftUI;
    // Start is called before the first frame update
    void Start()
    {
        hostagesCount = FindObjectsOfType<Rescue>().Length;
       
        levelTimer = 600f;
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = (int)levelTimer / 60;
        int seconds = (int)levelTimer % 60;

        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        levelTimer -= Time.deltaTime;
        timeLeftUI.text = "Time left :" + minutes + ":" + seconds;
        hostagesCountUI.text = "Hostages left : " + hostagesCount.ToString();

    }

    public void PlayerDeath()
    {
        StartCoroutine(ReloadScene());
    }

    public void HostageSaved()
    {
        hostagesCount--;
        if(hostagesCount <=0)
        {
            Debug.Log("Level complete");
        }
        levelTimer += 120f;
    }

    public void HostageDead()
    {
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
