using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("HUD temps réel")]
    public Text timerText;
    public Text collisionText;

    [Header("Panel résultat final")]
    public GameObject resultPanel;
    public Text finalTimeText;
    public Text finalCollisionsText;
    public Text finalScoreText;
    public Text finalMessageText;

    private float elapsedTime;
    private int collisionCount;
    private bool isRunning;
    private bool isFinished;

    [HideInInspector] public bool cube1Done;
    [HideInInspector] public bool cube2Done;
    [HideInInspector] public bool cube3Done;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isRunning && !isFinished && Input.anyKeyDown)
            isRunning = true;

        if (!isRunning || isFinished) return;

        elapsedTime += Time.deltaTime;

        if (timerText != null)
            timerText.text = "Temps : " + elapsedTime.ToString("F1") + "s";
        if (collisionText != null)
            collisionText.text = "Collisions : " + collisionCount;
    }

    public void AddCollision()
    {
        if (!isRunning || isFinished) return;
        collisionCount++;
    }

    public bool AllCubesDone() => cube1Done && cube2Done && cube3Done;

    public void EndGame()
    {
        if (isFinished) return;
        isFinished = true;
        isRunning = false;
        ShowResult();
    }

    void ShowResult()
    {
        if (resultPanel != null)
            resultPanel.SetActive(true);

        int score = 0;
        if (elapsedTime < 60f) score += 10;
        else if (elapsedTime < 120f) score += 5;

        if (collisionCount < 5) score += 6;
        else if (collisionCount < 10) score += 3;

        if (AllCubesDone()) score += 4;

        string msg = score >= 16 ? "Mission accomplie !" :
                     score >= 10 ? "Pas mal, continuez !" :
                     "Essayez encore !";

        if (finalTimeText != null)
            finalTimeText.text = "Temps : " + elapsedTime.ToString("F1") + " s";
        if (finalCollisionsText != null)
            finalCollisionsText.text = "Collisions : " + collisionCount;
        if (finalScoreText != null)
            finalScoreText.text = "Score : " + score + " / 20";
        if (finalMessageText != null)
            finalMessageText.text = msg;

        // Désactiver FPS controller + ObjectClick pour libérer la souris
        var fpsGO = GameObject.Find("FPSController");
        if (fpsGO != null)
        {
            var fpsCtrl = fpsGO.GetComponent("FirstPersonController") as MonoBehaviour;
            if (fpsCtrl != null) fpsCtrl.enabled = false;
        }

        var interactGO = GameObject.Find("InteractManager");
        if (interactGO != null)
        {
            var oc = interactGO.GetComponent<ObjectClick>();
            if (oc != null) oc.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
