using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutSceneScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject wasd;
    public GameObject arrows;
    private bool endScenePressed = false;
    public Animator animator;
    public GameObject fader;

    private void Start()
    {
        wasd.transform.DOMove(new Vector2(-2.5f, -2), 2);
        arrows.transform.DOMove(new Vector2(2.5f, -2), 2);
        fader.transform.localScale = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !endScenePressed)
        {
            StartCoroutine(EndCutScene());
        }
    }
    
    
    
    IEnumerator EndCutScene()
    {
        endScenePressed = true;
        print("hello");
        player1.transform.DOMove(new Vector2(-0.5f, 0), 1.5f);
        player2.transform.DOMove(new Vector2(0.5f, 0), 1.5f);
        yield return new WaitForSeconds(1);
        fader.transform.DOScale(new Vector2(1, 1), 1.8f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");
        
    }
}
