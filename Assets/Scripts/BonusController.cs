using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    public int bonusType = 0;//0-health, 1-coin, 2-energy 
    // Start is called before the first frame update
    void Start()
    {
        bonusType = Random.Range(0, 3);
    }
}
