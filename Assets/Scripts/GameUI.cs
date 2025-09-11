using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameEndText;
    [SerializeField] private GameObject gameEndObj;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text combatMessage;
    [SerializeField] private GameObject combatObj;
    [SerializeField] private GameObject freezingObj;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    private GameObject [] hearts;
    private float combatDuration;
    private float combatTimer;
    private bool combatActive;
    
    private int score;
    private bool gameEnd;
    private bool missionFail;
    private int lifeCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        gameEnd = false;
        missionFail = false;
        gameEndObj.SetActive(false);
        combatObj.SetActive(false);
        freezingObj.SetActive(false);
        lifeCount = 3;
        combatDuration = 3.5f;
        combatActive = false;
        combatTimer = 0;
        hearts = new GameObject[3];
        hearts[0] = heart1;
        hearts[1] = heart2;
        hearts[2] = heart3;
    }

    // Update is called once per frame
    void Update()
    {
        score = gameManager.GettingScore();
        scoreText.text = "Score: "+score;
        gameEnd = gameManager.GettingGameEnd();
        if(gameEnd){
            missionFail = gameManager.GettingMissionFail();
            gameEndObj.SetActive(true);
            if(missionFail)
            {
                gameEndText.text = "Game Over";
            }
            else{
                gameEndText.text = "Game Clear";
            }
        }

        if(combatActive)
        {
            combatTimer -= Time.deltaTime;
            if(combatTimer < 0)
            {
                combatTimer = 0.0f;
                combatActive = false;
                combatObj.SetActive(false);
            }
        }
    }

    public void LoseLife()
    {
        if(lifeCount > 0)
        {
            lifeCount--;
            hearts[lifeCount].SetActive(false);
        }
    }

    public void PopupCombatMessage(bool damaged)
    {
        combatTimer = combatDuration;
        combatActive = true;
        if(damaged)
        {
            combatObj.SetActive(true);
            combatMessage.text = "Damaged";
        }
        else
        {
            combatObj.SetActive(true);
            combatMessage.text = "Attack"; 
        }
    }

    public void ShowFreezingMessage()
    {
        freezingObj.SetActive(true);
    }
    public void HideFreezingMessage()
    {
        freezingObj.SetActive(false);
    }
}
