using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePanel : MonoBehaviour
{
    public GuideController guideController = null;
    public Canvas canvas;


    [UnityEngine.SerializeField]
    private Button m_btnLeft;
    [UnityEngine.SerializeField]
    private Button m_btnRight;
    private void Awake()
    {
        //canvas = GetComponentInParent<Canvas>();
        //guideController = GetComponent<GuideController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //guideController.Guide(canvas, GameObject.Find("Button").GetComponent<RectTransform>(),GuideType.Rect);
        guideController.Guide(canvas, GameObject.Find("Button").GetComponent<RectTransform>(), GuideType.Rect, 10, 1.5f);

         m_btnLeft.onClick.AddListener(() => {
            test();
        });

         m_btnRight.onClick.AddListener(() => {
            guideController.StopGuide();
        });
    }

    void test()
    {
        //guideController.Guide(canvas, GameObject.Find("Button_cir").GetComponent<RectTransform>(), GuideType.Circle);
        guideController.Guide(canvas, GameObject.Find("Button_cir").GetComponent<RectTransform>(), GuideType.Circle, 3, 1.0f);
    }
}