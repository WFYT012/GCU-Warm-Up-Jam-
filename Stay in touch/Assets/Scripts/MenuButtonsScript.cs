using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuButtonsScript : MonoBehaviour
{
    public GameObject playButton;
    public GameObject creditsButton;
    public GameObject quitButton;
    public Text credits;

    public DeathTransition dt;

    [SerializeField] AudioResource menuSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ButtonIntro());
        credits.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SoundManagerScript.instance.PlaySoundClip(menuSound);
        dt.Death();
        MusicManagerScript.instance.FadeTrack();
    }
    public void StopButton()
    {
        SoundManagerScript.instance.PlaySoundClip(menuSound);
        Application.Quit();

    }
    public void CreditsButton()
    {
        SoundManagerScript.instance.PlaySoundClip(menuSound);
        StartCoroutine(FlashCredits());
        // add credits
    }
    IEnumerator ButtonIntro()
    {
        yield return new WaitForSeconds(1f);
        playButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -89), 1.5f).SetEase(Ease.OutBounce);
        creditsButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -232), 1.5f).SetEase(Ease.OutBounce);
        quitButton.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -377), 1.5f).SetEase(Ease.OutBounce);
    }
    IEnumerator FlashCredits()
    {
        credits.enabled = true;
        yield return new WaitForSeconds(1.5f);
        credits.enabled = false;
    }
}
