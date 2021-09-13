using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject bestScoreText; // Var ajoutée pour les besoins de l'exo

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;


    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        if(NameAndScoreManager.Instance.bestScore == 0)
            bestScoreText.GetComponent<Text>().text = $"Best Sore : {NameAndScoreManager.Instance.playerName} : {NameAndScoreManager.Instance.bestScore}";

        else if (NameAndScoreManager.Instance.bestScore > 0)
            bestScoreText.GetComponent<Text>().text = $"Best Sore : {NameAndScoreManager.Instance.bestPlayer} : {NameAndScoreManager.Instance.bestScore}";

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(0);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {NameAndScoreManager.Instance.playerName} : {m_Points}"; //On va chercher la valeur de playerName contenue dans le singleton
    }

    public void GameOver()
    {
        if (m_Points > NameAndScoreManager.Instance.bestScore) //Si les points cumulés de la partie actuelle sont supérieurs à ceux enregistrés dans le int du singleton...
        {
            NameAndScoreManager.Instance.bestScore = m_Points;//...On update le int du singleton pour lui attribuer une nouvelle valeur
            NameAndScoreManager.Instance.bestPlayer = NameAndScoreManager.Instance.playerName;
            NameAndScoreManager.Instance.SaveNameAndBestScore();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
