using UnityEngine;
using System.Collections;


public class backgoundMusic : MonoBehaviour {

	public AudioClip[] bgMusic;
	public float volume;
	public bool loopOneMusic = false;
	public bool randomPlay = false;
	
	
	private int musicIndex = 0;
	private AudioSource backgroundMusicSource;
	
	private AudioSource CreateAudioSource (AudioClip clip, bool loop, float volume) {
		GameObject go = new GameObject("audio");
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.AddComponent(typeof(AudioSource));
		go.GetComponent<AudioSource>().clip = clip;
		go.GetComponent<AudioSource>().loop = loop;
		go.GetComponent<AudioSource>().volume = volume;
		go.GetComponent<AudioSource>().Play();
		return go.GetComponent<AudioSource>();
	}
	
	void Start () {
		
		if (bgMusic.Length !=0){
			
			if (randomPlay)
				musicIndex = Random.Range(0,bgMusic.Length);
			backgroundMusicSource = CreateAudioSource(bgMusic[musicIndex], loopOneMusic , volume);
			musicIndex++;
		}
	}
	
	void Update()
	{
		if (backgroundMusicSource)
		{
			backgroundMusicSource.loop = loopOneMusic;
			if (!backgroundMusicSource.isPlaying){
				if (bgMusic.Length !=0){
					if (musicIndex >= bgMusic.Length) musicIndex =0;
					if (randomPlay)
						musicIndex = Random.Range(0,bgMusic.Length);
					
					backgroundMusicSource.GetComponent<AudioSource>().clip = bgMusic[musicIndex];
					musicIndex++; 
					backgroundMusicSource.Play();
				}
			}
		}
	}
	
}
