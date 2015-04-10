using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	public AudioClip failHorn;
	public AudioClip dingDing;
    public AudioClip explosion;

	public void playClip(EnumScript.CustomAudioClips audioClip)
	{
		switch(audioClip)
		{
			case EnumScript.CustomAudioClips.failHorn:
				audio.PlayOneShot(failHorn);
				break;
			case EnumScript.CustomAudioClips.dingDing:
				audio.PlayOneShot(dingDing);
				break;
            case EnumScript.CustomAudioClips.explosion:
                audio.PlayOneShot(explosion);
                break;
			default:
				break;
		}
	}
}
