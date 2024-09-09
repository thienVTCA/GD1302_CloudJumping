using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManagerInstance;
    [SerializeField]
    Text textHealth, textEnergy, textScore;
    // Start is called before the first frame update
    void Start()
    {
        uiManagerInstance = this;
    }

    public void ShowHealth(int currentHealth, int maxHealth)
    {
        textHealth.text = "Health : " + currentHealth + "/" + maxHealth;
    }

    public void ShowEnergy(int currentEnergy, int maxEnergy)
    {
        textEnergy.text = "Energy : " + currentEnergy + "/" + maxEnergy;
    }

    public void ShowScore(int scoreNumber)
    {
        textScore.text = "Score: " + scoreNumber;
    }    

}
