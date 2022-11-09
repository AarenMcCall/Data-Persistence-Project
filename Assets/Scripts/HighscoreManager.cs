using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class HighscoreManager : MonoBehaviour
{
    public static HighscoreManager Instance;

    public string currentPlayer; // new name variable declared
    public int currentScore; // new score variable declared

    public string highscoreName; // new name variable declared
    public int highscore; // new score variable declared

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    //data persistence between sessions
    [System.Serializable]
    class PlayerData 
    {
        public string playerName;
        public int playerScore;
    }

    //creates player class instance and saves values as JSON
    public void SaveScore()
    {
        PlayerData playerData = new PlayerData(); //new instance of class PlayerData
        
        playerData.playerName = currentPlayer;
        playerData.playerScore = currentScore;

        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    
    //reads JSON file and fills player class instance with its values
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            highscoreName = playerData.playerName;
            highscore = playerData.playerScore;
        }
    }

}
