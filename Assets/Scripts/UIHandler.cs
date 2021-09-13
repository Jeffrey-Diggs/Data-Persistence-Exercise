using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public InputField nameField;
    
    void Start()
    {
        nameField = GetComponentInChildren<InputField>();
    }

    
    public void StartNewGame()
    {
        NameAndScoreManager.Instance.playerName = nameField.text; // Passer le texte de l'inputField dans le singleton NameManager pour le retrieve sur la scène suivante. Cf MainManager l-68
        SceneManager.LoadScene(1);
    }
}
