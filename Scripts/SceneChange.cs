using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void SelectA() => SelectDestination("201");
    public void SelectB() => SelectDestination("210");

    private void SelectDestination(string destKey)
    {
        // 선택 정보 저장 (예: PlayerPrefs, Static 변수 등)
        PlayerPrefs.SetString("SelectedDestination", destKey);
        // AR 내비 씬으로 전환
        SceneManager.LoadScene("SampleScene");
    }
}
