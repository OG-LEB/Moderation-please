using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    //Wins
    [Header("Links")]
    [SerializeField] private GameController gameController;
    [Header("ui elements")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject PlayWin;
    [SerializeField] private GameObject GameOverUI;
    [Space]
    [Header("ui elements")]
    [SerializeField] private TextMeshProUGUI FullScoreText;
    [SerializeField] private TextMeshProUGUI NoScoreText;
    [SerializeField] private TextMeshProUGUI YesScoreText;
    [SerializeField] private TextMeshProUGUI GameOverFullScoreText;
    [SerializeField] private TextMeshProUGUI GameOverNoScoreText;
    [SerializeField] private TextMeshProUGUI GameOverYesScoreText;
    [SerializeField] private GameObject MakeGameSign;
    [SerializeField] private GameObject MakingGameObjects;
    [SerializeField] private Image MakingGameBar;
    private bool isMakingGameBarActive = false;
    [SerializeField] private float barValue = 0;
    //[Space]
    //[Header("ui elements")]
    //[SerializeField] private GameObject FullScoreLeaderBoard;
    //[SerializeField] private GameObject YesScoreLeaderBoard;
    //[SerializeField] private GameObject NoScoreLeaderBoard;

    private void Start()
    {
        OpenMainMenu();
        MakingGameObjects.SetActive(false);
    }
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        PlayWin.SetActive(false);
        GameOverUI.SetActive(false);
        isMakingGameBarActive = false;
        barValue = 0;

    }
    public void OpenPlayUI()
    {
        MainMenu.SetActive(false);
        PlayWin.SetActive(true);
    }
    public void GameOver()
    {
        MainMenu.SetActive(false);
        PlayWin.SetActive(false);
        GameOverUI.SetActive(true);
    }
    public void UpdateScore(int value)
    {
        FullScoreText.text = $"Отправлено игр: {value}";
    }
    public void UpdateNoScore(int value)
    {
        NoScoreText.text = $"Отклонено: {value}";
    }
    public void UpdateYesScore(int value)
    {
        YesScoreText.text = $"Одобрено: {value}";
    }
    public void UpdateGameOverScore()
    {
        GameOverFullScoreText.text = $"Отправлено игр: {gameController.GetFullScore().ToString()}";
        GameOverNoScoreText.text = $"Отклонено: {gameController.GetNoScore().ToString()}";
        GameOverYesScoreText.text = $"Одобрено: {gameController.GetYesScore().ToString()}";
    }
    public void TurnOnMakeGameSign() { MakeGameSign.SetActive(true); }
    public void TurnOffMakeGameSign() { MakeGameSign.SetActive(false); }
    public void StartMakingGameBar()
    {
        TurnOffMakeGameSign();
        MakingGameObjects.SetActive(true);
        MakingGameBar.fillAmount = 0;
        barValue = 0;
        isMakingGameBarActive = true;
    }
    private void Update()
    {
        if (isMakingGameBarActive)
        {
            //barValue = Mathf.Lerp(barValue, 1, 1f);
            barValue += Time.deltaTime;
            MakingGameBar.fillAmount = barValue;
            if (barValue >= 1)
            {
                isMakingGameBarActive = false;
                gameController.GameIsReady();
                MakingGameObjects.SetActive(false);
            }
        }
    }
}
