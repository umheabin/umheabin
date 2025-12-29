using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CMainMenu : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button buttonPlay;
    public Button buttonQuit;

    [Header("Main Menu Image")]
    public GameObject mainMenuImage;

    [Header("Audio")]
    public AudioClip backgroundMusic;

    private static CMainMenu instance; // 싱글톤
    private AudioSource audioSource;

    void Awake()
    {
        // 싱글톤 체크: 이미 존재하면 새 오브젝트 삭제
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // 배경음악 유지

        // 버튼 클릭 이벤트
        if (buttonPlay != null)
            buttonPlay.onClick.AddListener(OnPlay);
        if (buttonQuit != null)
            buttonQuit.onClick.AddListener(OnQuit);
    }

    void Start()
    {
        // AudioSource 설정
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        if (backgroundMusic != null && !audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        // 시간 초기화
        Time.timeScale = 1f;

        // 씬 전환 후 카메라 문제 방지
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬 전환 후 UI 비활성화
        if (scene.name == "GameScene")
        {
            if (mainMenuImage != null) mainMenuImage.SetActive(false);
            if (buttonPlay != null) buttonPlay.gameObject.SetActive(false);
            if (buttonQuit != null) buttonQuit.gameObject.SetActive(false);

            // 카메라가 오브젝트를 볼 수 있는지 확인
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                mainCam.cullingMask = -1; // Everything
            }
        }
    }

    public void OnPlay()
    {
        Debug.Log("Play button clicked");

        // 씬 로드
        if (Application.CanStreamedLevelBeLoaded("GameScene"))
        {
            Debug.Log("Loading GameScene...");
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogError("GameScene이 Build Settings에 추가되지 않았거나 이름이 틀립니다!");
        }
    }

    public void OnQuit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
