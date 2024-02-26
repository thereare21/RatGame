using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitUntilMenu());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitUntilMenu() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TitleScreen");
    }

}
