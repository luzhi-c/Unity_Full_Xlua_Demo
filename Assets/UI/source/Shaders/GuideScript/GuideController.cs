using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 枚举，引导的类型
public enum GuideType
{
    Rect,
    Circle
}
//组件:需要的组件将会自动被添加到game object(游戏物体)。上
[RequireComponent(typeof(CircleGuide))]
[RequireComponent(typeof(RectGuide))]
public class GuideController : MonoBehaviour,ICanvasRaycastFilter
{
    //
    private CircleGuide circleGuide;
    private RectGuide rectGuide;
    //材质不一样，所以创建两个材质
    [SerializeField]
    private Material rectMat;
    [SerializeField]
    private Material circleMat;

    //需要image,
    private Image mask;

    private RectTransform target;
    // 在

    private GuideBase curGuide;
    
    private void Awake()
    {
        // 获取组件
        mask = transform.GetComponent<Image>();
        circleGuide = transform.GetComponent<CircleGuide>();
        rectGuide = transform.GetComponent<RectGuide>();
        if (rectMat==null || circleMat==null)
        {
            throw new System.Exception("材质未赋值");
        }

       
        
    }

    // 引导方法 参数：画布 目标 引导类型
    public void Guide(Canvas canvas, RectTransform target, GuideType guideType)
    {
        gameObject.SetActive(true);
        //引导的时候，将传入的target赋值
        this.target = target;
        // TODO
        switch (guideType)
        {
            case GuideType.Rect:
                mask.material = rectMat;
                rectGuide.Guide(canvas, target);
                curGuide = rectGuide;
                break;
            case GuideType.Circle:
                mask.material = circleMat;
                circleGuide.Guide(canvas, target);
                curGuide = circleGuide;
                break;

        }
    }
    // 引导方法 参数：画布 目标 引导类型 缩放  时间
    public void Guide(Canvas canvas, RectTransform target, GuideType guideType, float scale, float time)
    {
        gameObject.SetActive(true);
        //引导的时候，将传入的target赋值
        this.target = target;
        // TODO
        switch (guideType)
        {
            case GuideType.Rect:
                mask.material = rectMat;
                rectGuide.Guide(canvas, target, scale, time);
                curGuide = rectGuide;
                break;
            case GuideType.Circle:
                mask.material = circleMat;
                circleGuide.Guide(canvas, target, scale, time);
                curGuide = circleGuide;
                break;

        }

    }

    public void StopGuide() {
        gameObject.SetActive(false);
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp">鼠标点的点（屏幕坐标），看看在不在镂空区域，在就把事件渗透，不在拦截</param>
    /// <param name="eventCamera"></param>
    /// <returns></returns>
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (curGuide != null && !curGuide.IsGuideFinish()) {
            return true;
        }
        if (target==null) { return true; }//点击不了
        // 看看鼠标点击在不在镂空区域
        return !RectTransformUtility.RectangleContainsScreenPoint(target, sp);
    }
}
