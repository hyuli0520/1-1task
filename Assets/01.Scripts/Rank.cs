using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public struct bestPlayer
    {
        public string bestName;
        public int bestScore;

        public bestPlayer(string _bestName, int _bestScore)
        {
            bestName = _bestName;
            bestScore = _bestScore;
        }
    };

    public static List<bestPlayer> ranks = new List<bestPlayer>()
    {
        new bestPlayer("-",0),
        new bestPlayer("-",0),
        new bestPlayer("-",0),
        new bestPlayer("-",0),
        new bestPlayer("-",0)
    };

    public Text[] nameText;
    public Text[] scoreText;

    public InputField playerNameInput;
    private string playerName;
    private int playerScore;

    public Text curplayerScore;

    public Player player;

    private void Awake()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerName = playerNameInput.GetComponent<InputField>().text;
        for (int i = 0; i < 5; i++)
        {
            scoreText[i].text = ranks[i].bestScore.ToString();
            nameText[i].text = ranks[i].bestName.ToString();
        }
        curplayerScore.text = playerLogic.score.ToString();
    }

    public void InputName()
    {
        Player playerLogic = player.GetComponent<Player>();
        playerName = playerNameInput.text;
        playerScore = playerLogic.score;
        //PlayerPrefs.SetString("curPlayerName", playerName);
        //PlayerPrefs.SetInt("curPlayerScore", playerScore);

        //for (int i = 0; i < 5; i++)
        //{
        //    bestPlayer bestplayer = new bestPlayer();
        //    bestplayer.bestName = PlayerPrefs.GetString(i + "BestName");
        //    bestplayer.bestScore = PlayerPrefs.GetInt(i + "BestScore");
        //    ranks.Add(bestplayer);
        //}

        bestPlayer Bestplayer = new bestPlayer();
        Bestplayer.bestName = playerName;
        Bestplayer.bestScore = playerScore;
        ranks.Add(Bestplayer);

        ranks.Sort((a, b) => { return b.bestScore - a.bestScore; });

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(ranks[i].bestScore);
            Debug.Log(ranks[i].bestName);
        }

        //for (int i = 0; i < 5; i++)
        //{
        //    PlayerPrefs.SetInt(i + "BestScore", ranks[i].bestScore);
        //    PlayerPrefs.SetString(i.ToString() + "BestName", ranks[i].bestName);
        //}

        for (int i = 0; i < 5; i++)
        {
            scoreText[i].text = ranks[i].bestScore.ToString();
            nameText[i].text = ranks[i].bestName.ToString();
        }
    }
}
