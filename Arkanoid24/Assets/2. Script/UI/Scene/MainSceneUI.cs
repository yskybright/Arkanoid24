using UnityEngine;
using TMPro;

public class MainSceneUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject[] lifes;

    [Header("Time Attack")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI bestTimerText;

    [Header("Score Board")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI[] rankTexts;

    [Header("Popup")]
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private GameObject nextStagePopup;
    [SerializeField] private GameObject rankingPopup;
    [SerializeField] private GameObject timeAttackPopup;

    #region Set UI

    public void SetScoreUI(float score)
    {
        scoreText.text = $"점수 : {score}";
    }

    public void SetLifeUI(bool isDown, int num)
    {
        if (isDown)
        {
            lifes[num].SetActive(false);
        }
        else
        {
            lifes[num].SetActive(true);
        }
    }

    #endregion

    #region Show Popup

    public void ShowGameOver()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.gameover);
        gameOverPopup.SetActive(true);
    }

    public void ShowNextStage()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.nextstage);
        nextStagePopup.SetActive(true);
    }

    public void ShowRanking()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.nextstage);
        rankingPopup.SetActive(true);
    }

    public void ShowTimeAttack()
    {
        SFX.Instance.PlayOneShot(SFX.Instance.nextstage);
        timeAttackPopup.SetActive(true);
    }

    #endregion

    #region Mode Method

    /// <summary>
    /// 모드에 따라 UI 변경
    /// </summary>
    public void StartModeUI(GameMode mode)
    {
        switch (mode) 
        {
            case GameMode.Main:
                scoreText.gameObject.SetActive(true); 
                break;

            case GameMode.Infinity:
                scoreText.gameObject.SetActive(true);
                bestScoreText.gameObject.SetActive(true);
                break;

            case GameMode.TimeAttack:
                timerText.gameObject.SetActive(true);
                bestTimerText.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// [타임 어택] 타이머 UI 세팅
    /// </summary>
    /// <param name="remainTime">남은 시간</param>
    public void SetTimerUI(float remainTime)
    {
        timerText.text = string.Format(remainTime.ToString("F2"));
    }

    /// <summary>
    /// [무한 모드] 현재 라이프 UI 세팅
    /// </summary>
    /// <param name="num"></param>
    public void SetCurrentLifeUI(int num)
    {
        for (int i = 0; i < lifes.Length; i++)
        {
            if (i < num) lifes[i].SetActive(true);
            else lifes[i].SetActive(false);
        }
    }

    /// <summary>
    /// [무한 모드] 최고 점수 세팅
    /// </summary>
    /// <param name="bestScore"></param>
    public void SetBestScoreUI(float bestScore)
    {
        bestScoreText.text = $"최고 기록 : {bestScore}";
    }

    public void SetRankingUI()
    {
        for (int i = 0; i < rankTexts.Length; i++)
        {
            rankTexts[i].text = $"{i + 1}등  {Managers.Game.RankScores[i]}점";
        }
    }

    #endregion
}
