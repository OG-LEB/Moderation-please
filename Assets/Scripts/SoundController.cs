using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource MainSountrack;
    [SerializeField] AudioSource GameOverSound;
    [SerializeField] AudioSource OmNomSound;
    [SerializeField] AudioSource Bonksound;
    [SerializeField] AudioSource CodingSound;
    [SerializeField] AudioSource ReadySound;
    [SerializeField] AudioSource GameAcceptSound;
    [SerializeField] AudioSource GameDeniedSound;

    public void PlayOmnomSound()
    {
        OmNomSound.Play();
    }
    public void StartSoundTrack()
    {
        MainSountrack.Play();
    }
    public void StopSoundTrack() 
    {
        MainSountrack.Stop();
    }
    public void OmnomSound()
    {
        OmNomSound.Play();
    }
    public void BonkSoundPlay()
    {
        Bonksound.Play();
    }
    public void GameOverSoundPlay() 
    {
        GameOverSound.Play();
    }
    public void PlayCodingSound()
    {
        CodingSound.Play();
    }
    public void PlayReadySound()
    {
        ReadySound.Play();
    }
    public void StopGameOverSound() 
    {
        GameOverSound?.Stop();
    }
    public void PlayGameAcceptSound() 
    {
        GameAcceptSound.Play();
    }
    public void PlayGameDeniedSound()
    {
        GameDeniedSound.Play();
    }
}
