using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    [SerializeField] GameObject dialogoPrimero, enemigoDch, imagenFinal;
    [SerializeField] GameObject creditos;
    [SerializeField] AudioSource victorySound, bossMusic;

    bool creditsActive;

    public bool endActivated;
    public bool gunPicked;

    void Awake(){
        if (sharedInstance == null){
            sharedInstance = this;
        }
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
    }

    private void Start() {
        dialogoPrimero.SetActive(true);
        dialogoPrimero.GetComponent<Dialogue>().StartDialogue();
    }

    public void ExitGame(){
        Application.Quit();
    }

    private void Update() {
        if (creditsActive && creditos.GetComponent<VideoPlayer>().isPaused){
            Invoke("MainMenu", 3f);
        }
        if (enemigoDch.transform.childCount == 0 && !endActivated){
            bossMusic.Stop();
            imagenFinal.SetActive(true);
            victorySound.Play();
            endActivated = true;
            print("matados");
            Invoke("PlayCredits", 3f);
        }
    }

    public void RestartScene(){
        SceneManager.LoadScene("PrototypeScene");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayCredits(){
        creditsActive = true;
        imagenFinal.SetActive(false);
        creditos.SetActive(true);
        creditos.GetComponent<VideoPlayer>().Play();
    }
}
