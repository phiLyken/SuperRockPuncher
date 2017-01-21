using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSounds : MonoBehaviour {

    AudioSource m_audio;

    PunchMeter m_punchMeter;

    public List<AudioClip> WalkSounds;

    public AudioClip PunchSmall;
    public AudioClip PunchBig;
    public AudioClip BoothMove;

    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        m_punchMeter = GetComponent<PunchMeter>();
        Player.OnAction += PlayActionSound;
    }
	
    void PlayActionSound(Player.PlayerActions action)
    {
        if (!BackGroundMusic.GetEnabled())
            return;

        switch (action)
        {
            case Player.PlayerActions.move_forward:
                m_audio.PlayOneShot(WalkSounds.GetRandom());
                break;

            case Player.PlayerActions.punch:
                m_audio.PlayOneShot(m_punchMeter.GetCurrentFill() > 0.5f ? PunchBig : PunchSmall);
                break;

            case Player.PlayerActions.move_in:
            case Player.PlayerActions.move_out:
                m_audio.PlayOneShot(BoothMove);
                break;

        }
    }

    void OnDestroy()
    {
        Player.OnAction -= PlayActionSound;
    }
}
