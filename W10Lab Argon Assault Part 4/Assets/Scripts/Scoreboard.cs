using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    int score;

    public void IncreaseScore(int ammtToIncrease)
    {
        score += ammtToIncrease;
        Debug.Log($"The new score is, {score}");
    }
}
