using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.GetInstance().loadfromtitle == true)
        {
            SoundManager.GetInstance().loadfromtitle = false;
            int tempindex = SoundManager.GetInstance().loadfromtitleindex;
            SoundManager.GetInstance().loadfromtitleindex = -1;

            ProgressManager.GetInstance().LoadfromFile(tempindex);
        }
        else
        {
            SceneManager.LoadScene("Beginning");
            Debug.Log("NewGame");
        }
        
    }


}
