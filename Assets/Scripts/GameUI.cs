using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text gameEndText;
    [SerializeField] private GameObject gameEndObj;
    [SerializeField] private TMP_Text combatMessage;
    [SerializeField] private GameObject combatObj;
    [SerializeField] private GameObject freezingObj;
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;
    private GameObject [] hearts;

    
    private int lifeCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gameEndObj.SetActive(false);
        combatObj.SetActive(false);
        freezingObj.SetActive(false);
        lifeCount = 3;
        hearts = new GameObject[3];
        hearts[0] = heart1;
        hearts[1] = heart2;
        hearts[2] = heart3;
    }

    private void OnEnable(){
        GameManager.GameEnd +=ShowGameEndText;
        GameManager.GameOver += ShowGameOverText;
        GameManager.FreezingEventStart += ShowFreezingText;
        GameManager.FreezingEventEnd += UnShowFreezingText;
        GameManager.AttackEventStart += ShowAttackText;
        GameManager.AttackEventEnd += UnShowAttackText;
        GameManager.DamageEventStart += ShowDamageText;
        GameManager.DamageEventEnd += UnShowDamageText;
        GameManager.GetScoreEvent += UpdatedGetScore;

    }
    private void OnDisable(){
        GameManager.GameEnd -=ShowGameEndText;
        GameManager.GameOver -= ShowGameOverText;
        GameManager.FreezingEventStart -= ShowFreezingText;
        GameManager.FreezingEventEnd -= UnShowFreezingText;
        GameManager.AttackEventStart -= ShowAttackText;
        GameManager.AttackEventEnd -= UnShowAttackText;
        GameManager.DamageEventStart -= ShowDamageText;
        GameManager.DamageEventEnd -= UnShowDamageText;
        GameManager.GetScoreEvent -= UpdatedGetScore;
    }

    private void ShowGameOverText(object c){
        gameEndObj.SetActive(true);
        gameEndText.text = "Game Over";
    }

    private void ShowGameEndText(object c){
        gameEndObj.SetActive(true);
        gameEndText.text = "Game Clear";
    }

    private void ShowFreezingText(object c){
        freezingObj.SetActive(true);
    }

    private void UnShowFreezingText(object c){
        freezingObj.SetActive(false);
    }
    private void ShowAttackText(object c){
        combatObj.SetActive(true);
        combatMessage.text = "Attack";
    }
    private void UnShowAttackText(object c){
        combatObj.SetActive(false);
    }
    private void ShowDamageText(object c){
        combatObj.SetActive(true);
        combatMessage.text = "Damaged";
        if(lifeCount > 0)
        {
            lifeCount--;
            hearts[lifeCount].SetActive(false);
        }

    }
    private void UnShowDamageText(object c){
        combatObj.SetActive(false);
    }

    private void UpdatedGetScore(object c, int score){
        scoreText.text = "Score: "+score;
    }


}
