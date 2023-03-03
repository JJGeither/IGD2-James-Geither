using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    private int score = 5;
    public Text scoreText;
    public GameObject otherObject;
    private BallController ballController;

    // Start is called before the first frame update
    void Start()
    {

        // Get the OtherScript component from the other GameObject
        ballController = otherObject.GetComponent <BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + ballController.score;
    }
}
