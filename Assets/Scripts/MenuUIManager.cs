using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;
public class MenuUIManager : MonoBehaviour
{
    public InputField playerNameInput;

    public void StartGame()
    {
        if (playerNameInput.text != "") //checks if the user entered a name
        {
            SceneManager.LoadScene(1);
        }
        else Debug.Log("Please enter a name");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void NewName()
    {
        string newName = playerNameInput.text;
        HighscoreManager.Instance.currentPlayer = newName;
    }
}
