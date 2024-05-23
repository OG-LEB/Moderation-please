using UnityEngine;
using YG;

public class SavingSystem : MonoBehaviour
{
    public int LoadHightScore()
    {
        return YandexGame.savesData.HightScore;
    }
    public void SaveHightScore(int hightScore) 
    {
        YandexGame.savesData.HightScore = hightScore;
        YandexGame.SaveProgress();
    }
}
