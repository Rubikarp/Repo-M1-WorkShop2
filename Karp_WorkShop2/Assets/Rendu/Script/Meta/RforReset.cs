using UnityEngine;
using UnityEngine.SceneManagement;

public class RforReset : MonoBehaviour
{
    public InputHandler input;
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
