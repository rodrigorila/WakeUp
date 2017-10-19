using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreenManager : MonoBehaviour {

    public void OnPlayLevelClick (string level) {
		// load the specified level
        SceneManager.LoadScene(level);
	}

}
