using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloadManager : MonoBehaviour
{
    public Camera SceneCamera;
    private float InitialCameraZoom;
    public float CameraTargetSize = 2.0f;

    public bool CameraMoving = false;
    private Vector3 CameraStartPosition;
    private Vector3 CameraTargetPosition;
    private GameObject Player;
    private Vector3 PlayerStartPosition;
    private Vector3 PlayerTargetPosition;
    private float ReloadCalledTime;

    public void ReloadScene(GameObject InPlayer, Vector3 TargetPosition)
    {
        if (CameraMoving)
        {
            return;
        }

        Player = InPlayer;
        CameraMoving = true;
        ReloadCalledTime = Time.time;
        InitialCameraZoom = SceneCamera.orthographicSize;
        CameraStartPosition = SceneCamera.transform.position;

        PlayerStartPosition = InPlayer.transform.position;
        PlayerTargetPosition = new Vector3 (InPlayer.transform.position.x, InPlayer.transform.position.y + 0.5f, InPlayer.transform.position.z);


        CameraTargetPosition = TargetPosition;
        CameraTargetPosition.z = CameraStartPosition.z;

    }

    float EaseInOutQuint(float time)
    {
        time = Mathf.Clamp(time, 0.0f, 1.0f);
        return time < 0.5 ? 16 * time * time * time * time * time : 1 - Mathf.Pow(-2 * time + 2, 5) / 2;
    }

    void Start()
    {
        //this
    }

    void Update()
    {
        if (!CameraMoving)
        {
            return;
        }

        float time = Time.time - ReloadCalledTime;
        float progress = EaseInOutQuint(time);

        Debug.Log("Progress: " + progress);

        SceneCamera.orthographicSize = Mathf.Lerp(InitialCameraZoom, CameraTargetSize, progress);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Lerp(0, -10.0f, progress));
        transform.position = Vector3.Lerp(CameraStartPosition, CameraTargetPosition, progress);

        float deathTime = Time.time - ReloadCalledTime - 1.25f;
        float deathProgress = EaseInOutQuint(deathTime);

        Player.transform.position = Vector3.Lerp(PlayerStartPosition, PlayerTargetPosition, progress) + new Vector3 (Mathf.Clamp(deathTime, 0.0f, 1.0f) * 3, Mathf.Sin(Mathf.PI * Mathf.Clamp(deathTime, 0.0f, 1.0f)) * 2, 0.0f);
        if (deathTime < 0)
        {
            Player.transform.position += new Vector3(Random.Range(0, time / 5), Random.Range(0, time / 5));
        }
        Player.transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Clamp(deathTime, 0.0f, 1.0f) * 720.0f);

        if (time >= 3.0f)
        {
            //This is where the reload starts, should animate then be called

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
