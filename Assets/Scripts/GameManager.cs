using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm;

	public GameObject player;
	public GameObject playerShooter;

	public GameObject ZSpawner;

	public GameObject VictoryText;
	public GameObject YouLooseText;
	public GameObject MenuButton;

	public List<GameObject> Z = new List<GameObject>();

	public Material MaterialZCollected;

    // for testing only
    public bool Infinite = false;

    private int m_ZsCollected = 0;
    private bool m_playing = false;


    public bool Playing {
        get {
            return m_playing;
        }
    }

	public void IncrementZsCollected() {
        if (m_ZsCollected >= Z.Count)
            return;
        
		Z [m_ZsCollected].GetComponent<ZChangeToCollected> ().enabled = true;

		m_ZsCollected++;

        if (m_ZsCollected == Z.Count)
            EndGame (true);
	}

    public void EndGame (bool wonLevel) {
        if (!m_playing)
            return;

        if (Infinite)
            return;

        m_playing = false;

		if (wonLevel)
			VictoryText.SetActive (true);
		else
			YouLooseText.SetActive (true);

		ZSpawner.SetActive (false);

		MenuButton.SetActive (true);

		player.GetComponent<AlckyController> ().enabled = false;
		playerShooter.SetActive (false);
	}

	void Start () {
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

        m_playing = true;
	}

	public void OnMenuButtonClick () {
		// load the specified level
		SceneManager.LoadScene("MainScreen");
	}
}
