using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ButtonIntro());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Level1");
        
    }
    public void StopButton()
    {
        Application.Quit();
    }
    public void CreditsButton()
    {
        // add credits
    }
    IEnumerator ButtonIntro()
    {
        yield return new WaitForSeconds(1f);
        playButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -89), 1.5f).SetEase(Ease.OutBounce);
        creditsButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -232), 1.5f).SetEase(Ease.OutBounce);
        quitButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -377), 1.5f).SetEase(Ease.OutBounce);
    }

}
