using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    private int _ratsKilled;

    public int ratsKilled {
        get => _ratsKilled;
        set
        {
            _ratsKilled = value;
            PlayIncrementAnimation();
        }
    }

    public GameObject scoreUI;

    // Start is called before the first frame update
    void Start()
    {
        ratsKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI scoreText = scoreUI.GetComponentInChildren<TextMeshProUGUI>();
        scoreText.text = ratsKilled.ToString();
    }

    void PlayIncrementAnimation()
    {
        StartCoroutine(ScaleAnimate());
    }

    IEnumerator ScaleAnimate()
    {

        scoreUI.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        yield return new WaitForSeconds(0.2f);
        scoreUI.transform.localScale = new Vector3(1f, 1f, 1f);

        yield return null;
    }

}
