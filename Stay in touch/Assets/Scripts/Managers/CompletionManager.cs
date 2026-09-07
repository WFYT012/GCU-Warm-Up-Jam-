using DG.Tweening;
using NUnit.Framework.Internal.Filters;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

//[RequireComponent(typeof(Animator))]
public class CompletionManager : MonoBehaviour
{
	public Camera SceneCamera;
	public Transform Player1;
	public Transform Player2;
	public float CompletionDistance = 1.5f;
    private float InitialCameraZoom;
    public float CompletionCameraZoom = 2.0f;

    public float CompletionHoldingDistance = 3.0f;

    //public VideoClip handshakeClip;
    public VideoPlayer videoPlayer;

    //[SerializeField] private AnimationClip CompletionZoomAnimation;
    //[SerializeField] private Animator animator;

    //private PlayableGraph graph;

    private Coroutine completionTImerCoroutine;
    private bool GameComplete = false;
    private bool VideoPlaying = false;

    private Vector3 cameraStartPosition;
    private Vector3 cameraTargetPosition;

    private Vector3 player1StartPosition;
    private Vector3 player1TargetPosition;

    private Vector3 player2StartPosition;
    private Vector3 player2TargetPosition;

    private float completionTime;

    //[SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private SceneReloadManager reloadManager;

    public GameObject completeSprite;

    public Animator animator;

    void Start()
	{
        completionTImerCoroutine = StartCoroutine(CheckForCompletion());

        completeSprite.transform.localScale = Vector3.zero;
        completeSprite.SetActive(false);
        //CameraAnimator.clip = CompletionZoomAnimation;
    }

	IEnumerator CheckForCompletion()
	{
		while (!GameComplete)
		{
			yield return new WaitForSeconds(0.1f);

			float distance = Vector3.Distance(Player1.position, Player2.position);
			//Debug.Log("Distance: " + distance);

			if (distance <= CompletionDistance && !reloadManager.CameraMoving)
			{
				Debug.Log("Game Completion Triggered");
				GameComplete = true;

				//animator.SetTrigger("OnCompletion");

                completionTime = Time.time;

                StartCoroutine(LevelComplete());

                reloadManager.enabled = false;

                cameraStartPosition = transform.position;
                cameraTargetPosition = Vector3.Lerp(Player1.position, Player2.position, 0.5f);
                cameraTargetPosition.z = cameraStartPosition.z;

                player1StartPosition = Player1.position;
                player2StartPosition = Player2.position;

                player1TargetPosition.y = cameraTargetPosition.y;
                player2TargetPosition.y = cameraTargetPosition.y;

                player1TargetPosition.x = cameraTargetPosition.x + CompletionHoldingDistance / 2;
                player2TargetPosition.x = cameraTargetPosition.x + -CompletionHoldingDistance / 2;


                InitialCameraZoom = SceneCamera.orthographicSize;
            }
		}
	}

    void Update()
    {
        if (!GameComplete)
        {
            return;
        }

        float time = Time.time - completionTime;
        float progress = EaseInOutQuint(time);

        Debug.Log("Progress: " + progress);

        SceneCamera.orthographicSize = Mathf.Lerp(InitialCameraZoom, CompletionCameraZoom, progress);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(0, -10.0f, progress));
        transform.position = Vector3.Lerp(cameraStartPosition, cameraTargetPosition, progress);

        Player1.position = Vector3.Lerp(player1StartPosition, player1TargetPosition, progress);
        Player2.position = Vector3.Lerp(player2StartPosition, player2TargetPosition, progress);

        //if (progress >= 1)
        //{
        //    GameComplete = false;
        //}
        if (!VideoPlaying && time > 0.8f)
        {
            VideoPlaying = true;
            videoPlayer.transform.position = new Vector3(cameraTargetPosition.x, cameraTargetPosition.y, 0.0f);

            videoPlayer.enabled = true;
            videoPlayer.Play();
        }

        //VideoPlayer player = Instantiate<VideoPlayer>();
    }


    float EaseInOutQuint(float time)
    {
        time = Mathf.Clamp(time, 0.0f, 1.0f);
        return time < 0.5 ? 16 * time * time * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 5) / 2;
    }


    //public void PlayCompletionZoom()
    //{
    //       graph = PlayableGraph.Create("PlayAnimationClip");
    //       graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

    //       var output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());

    //       // Wrap the clip in a playable.
    //       var clipPlayable = AnimationClipPlayable.Create(graph, CompletionZoomAnimation);

    //       // Connect the Playable to an output.
    //       output.SetSourcePlayable(clipPlayable);

    //       // Plays the Graph.
    //       graph.Play();
    //   }

    //private void OnDestroy()
    //{
    //	if (graph.IsValid())
    //		graph.Destroy();
    //}

    IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(0.5f);
        completeSprite.SetActive(true);
        completeSprite.transform.DOScale(new Vector3(3.9f, 4.5f, 1), 0.15f)
            .OnComplete(() => completeSprite.transform.DOScale(Vector3.one *3, 0.1f));
        yield return new WaitForSeconds(1);
        animator.SetTrigger("SwipeLeft");
    }
}

