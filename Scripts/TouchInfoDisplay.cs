using UnityEngine;
using UnityEngine.UI;

public class TouchInfoDisplay : MonoBehaviour
{
    [SerializeField] Camera arCamera;            // ARSessionOrigin 하위 AR 카메라
    [SerializeField] CanvasGroup infoPanelCg;    // InfoPanel(GameObject) 의 CanvasGroup
    [SerializeField] Image infoImage;          // InfoImage 컴포넌트

    [SerializeField] Sprite spriteFor201;
    [SerializeField] Sprite spriteFor210;
    private string selectedKey;

    private void Start()
    {
        // 1) PlayerPrefs에서 선택된 도착지 키 읽기
        selectedKey = PlayerPrefs.GetString("SelectedDestination", "");

        // 2) 시작할 때 InfoPanel 숨기기
        infoPanelCg.alpha = 0f;
        infoPanelCg.blocksRaycasts = false;
    }

    void Update()
    {
        // 모바일 터치 or 에디터 클릭
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector2 screenPos = Input.touchCount > 0
                              ? Input.GetTouch(0).position
                              : (Vector2)Input.mousePosition;

            // Raycast
            Ray ray = arCamera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                var target = hit.collider.GetComponent<NavigationTarget>();
                if (target != null && target.infoSprite != null)
                {
                    ShowInfoByKey(target.infoSprite);
                }
            }
        }
    }

    private void ShowInfoByKey(Sprite fallback)
    {
        Sprite toShow = fallback;

        if (selectedKey == "201" && spriteFor201 != null)
            toShow = spriteFor201;
        else if (selectedKey == "210" && spriteFor210 != null)
            toShow = spriteFor210;

        // 3) 패널에 스프라이트 할당 후 보이게 설정
        infoImage.sprite = toShow;
        infoPanelCg.alpha = 1f;
        infoPanelCg.blocksRaycasts = true;
    }

    
    public void HideInfo()
    {
        infoPanelCg.alpha = 0f;
        infoPanelCg.blocksRaycasts = false;
    }
}
