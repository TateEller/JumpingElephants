using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrophyRoomManager : MonoBehaviour
{
    public void NextLevelButton()
    {
        SceneManager.LoadScene(1);
    }
    public void GambleButton()
    {
        SceneManager.LoadScene("GambleScene");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
