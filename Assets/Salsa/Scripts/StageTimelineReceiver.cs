using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTimelineReceiver : MonoBehaviour
{
    public string NextSceneName;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(NextSceneName);
    }
}
