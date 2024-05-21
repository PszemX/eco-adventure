using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class endgames : MonoBehaviour
{
    public Canvas inGameCanvas;
    public TMP_Text scoreText;
    public TMP_Text enemyText;
    public TMP_Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        float timer = GameManager.instance.getTimer();
        timerText.text = string.Format("{0:00}:{1:00}", ((timer - 30) / 60), (timer % 60));
        scoreText.text = GameManager.instance.getScore().ToString();
        enemyText.text = GameManager.instance.getEnemies().ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
