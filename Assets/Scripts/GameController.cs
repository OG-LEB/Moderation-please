using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using YG;

public class GameController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private UIController _UIController;
    [SerializeField] private ModeratorMovement _ModeratorMovement;
    [SerializeField] private SoundController _SoundController;
    [SerializeField] private SavingSystem _savingSystem;
    private bool isPause;
    [Header("Location objects")]
    [SerializeField] private GameObject ZipObject;
    [SerializeField] private Transform SpawnPosition;
    [Space]
    [Header("Settings")]
    [SerializeField] private float ThrowForce;
    private int fullScore = 0;
    private int hightScore = 0;
    private int yesScore = 0;
    private int noScore = 0;
    private bool isCheeseBallThown;
    private bool isGameReady = false;
    private bool makingGame = false;
    [Space]
    [Header("Games")]
    [SerializeField] private string[] GameNames;
    [SerializeField] private TextMeshProUGUI gameNameText;
    [Space]
    [Header("Moderator")]
    [SerializeField] private GameObject ModeratorTextObject;
    [SerializeField] private TextMeshProUGUI ModeratorText;
    [SerializeField] private string[] AllowTexts;
    [SerializeField] private string[] DeniedTexts;


    private void Start()
    {
        GameAwake();
        hightScore = _savingSystem.LoadHightScore();
    }
    public void GameAwake()
    {
        isPause = true;
        ZipObject.GetComponent<Rigidbody>().isKinematic = true;
        ZipObject.transform.position = SpawnPosition.position;
        _UIController.OpenMainMenu();
        ZipObject.SetActive(false);
        _UIController.TurnOnMakeGameSign();
        _ModeratorMovement.StopMovement();
        _ModeratorMovement.ResetPosition();
        fullScore = 0;
        yesScore = 0;
        noScore = 0;
        _ModeratorMovement.Restart();
        isCheeseBallThown = false;
        isGameReady = false;
        makingGame = false;
        _UIController.UpdateScore(fullScore);
        _UIController.UpdateYesScore(yesScore);
        _UIController.UpdateNoScore(noScore);
        _SoundController.StartSoundTrack();
        ModeratorTextObject.SetActive(false);
    }
    public void PlayButton()
    {
        _UIController.OpenPlayUI();
        isPause = false;
        _ModeratorMovement.StartMovement();
    }
    public void GameOver()
    {
        _ModeratorMovement.StopMovement();
        isPause = true;
        _UIController.GameOver();
        _UIController.UpdateGameOverScore();
        _SoundController.GameOverSoundPlay();
        _SoundController.StopSoundTrack();

        if (fullScore > hightScore)
        {
            hightScore = fullScore;
            _savingSystem.SaveHightScore(hightScore);
            YandexGame.NewLeaderboardScores("FullScore", hightScore);
        }
    }
    private void Update()
    {
        if (!isPause && !isCheeseBallThown)
        {
            if (Input.GetMouseButtonDown(0) && !makingGame)
            {
                if (!isGameReady)
                {
                    MakeAGame();
                }
                else
                {
                    ThrowGame();
                }

            }
        }
    }
    private void ThrowGame()
    {
        ZipObject.GetComponent<Rigidbody>().isKinematic = false;
        ZipObject.GetComponent<Rigidbody>().AddForce(SpawnPosition.forward * ThrowForce, ForceMode.Impulse);
        isCheeseBallThown = true;
        _SoundController.BonkSoundPlay();
    }
    private void MakeAGame()
    {
        makingGame = true;
        _UIController.StartMakingGameBar();
        _SoundController.PlayCodingSound();
    }
    public void GameIsReady()
    {
        makingGame = false;
        isGameReady = true;
        RespawnZip();
        ZipObject.SetActive(true);
        _SoundController.PlayReadySound();

    }
    public void HitTarget()
    {
        fullScore++;
        _UIController.UpdateScore(fullScore);
        StartCoroutine(ModeratorThink());
        _SoundController.OmnomSound();
        ZipObject.SetActive(false);
        _UIController.TurnOnMakeGameSign();
        isGameReady = false;
        isCheeseBallThown = false;

    }
    public void RespawnZip()
    {
        UpdateGameName();
        ZipObject.GetComponent<Rigidbody>().isKinematic = true;
        ZipObject.transform.position = SpawnPosition.position;
    }
    public void GameRestart()
    {
        GameAwake();
        _SoundController.StopGameOverSound();
        YandexGame.FullscreenShow();
    }
    public int GetFullScore() { return fullScore; }
    public int GetYesScore() { return yesScore; }
    public int GetNoScore() { return noScore; }
    private void UpdateGameName()
    {
        gameNameText.text = $"{GameNames[UnityEngine.Random.Range(0, GameNames.Length)]}.zip";
    }
    private void ModeratorSay(bool yes)
    {
        if (yes)
        {
            ModeratorText.text = AllowTexts[UnityEngine.Random.Range(0, AllowTexts.Length)];
        }
        else
        {
            ModeratorText.text = DeniedTexts[UnityEngine.Random.Range(0, DeniedTexts.Length)];
        }
        StartCoroutine(ShowModeratorDialog());
    }
    IEnumerator ShowModeratorDialog()
    {
        ModeratorTextObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        ModeratorTextObject.SetActive(false);
    }
    IEnumerator ModeratorThink()
    {
        yield return new WaitForSeconds(1f);
        int veroyatnost = UnityEngine.Random.Range(1, 100);
        if (veroyatnost <= 10)
        {
            yesScore++;
            _UIController.UpdateYesScore(yesScore);
            ModeratorSay(true);
            _SoundController.PlayGameAcceptSound();
        }
        else
        {
            noScore++;
            _UIController.UpdateNoScore(noScore);
            ModeratorSay(false);
            _SoundController.PlayGameDeniedSound();
        }
        _ModeratorMovement.SpeedUp();
    }
}
