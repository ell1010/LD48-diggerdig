using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager instance
	{
		get{return gm;}
	}
	private static GameManager gm = null;
	[SerializeField]
	float duration;
	[SerializeField]
	Text cdown;
	int score;
	[SerializeField]
	Text tscore;
	float totalTime;
	public bool triggerAdd = false;
	public GameObject player;
	public GameObject gover;

    // Start is called before the first frame update
    void Start()
    {
		if (gm != null)
		{
			DestroyImmediate(gameObject);
			return;
		}
		//DontDestroyOnLoad(gameObject);
		gm = this;
		score = 0;
		tscore.text = score.ToString();
		StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update()
    {
		if (triggerAdd)
		{
			addcoins();
			triggerAdd = false;
		}
    }

	public void addcoins()
	{
		//add score
		//update ui
		//add time to countdown
		//5 seconds?
		score += 10;
		tscore.text = score.ToString();
		totalTime += 2f;

	}

	IEnumerator countdown()
	{
		totalTime = duration;
		while (totalTime > 0)
		{
			totalTime -= Time.deltaTime;
			cdown.text = System.Math.Round(totalTime , 2).ToString();

			yield return null;

		}
		print("timer finished");
		endtimer();
		yield break;
	}

	void endtimer()
	{
		player.GetComponent<PlayerController>().enabled = false;
		gover.SetActive(true);
		//Time.timeScale = 0;
	}
}
