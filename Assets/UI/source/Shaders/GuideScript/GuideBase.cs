using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GuideBase : MonoBehaviour
{
    // 将私有的变量，变成protect，这样才可以继承
    protected Material material;  // 材质
    protected Vector3 center;     // 镂空区域的中心


    protected RectTransform target;// 要显示的目标,通过目标计算镂空区域的中心，宽高
    protected Vector3[] targetCorners = new Vector3[4];//存储要镂空组件的四个角的数组


    protected bool isScaling; // 是否正在变化

    //protected virtual void Start()
    //{
    //    // 获取材质
    //    material = transform.GetComponent<Image>().material;
    //    //如果没有获取到材质，就抛出异常
    //    if (material == null)
    //    {
    //        throw new System.Exception("为获取到材质");
    //    }
    //}
    // 引导
    public virtual void Guide(Canvas canvas, RectTransform target)
    {
        //获取材质
        material = transform.GetComponent<Image>().material;
        this.target = target;  // 将传进来的目标组件赋值给target
        

        // 获取中心点
        // GetWorldCorners:在世界空间中得到计算的矩形的角。参数角的数组
        target.GetWorldCorners(targetCorners);

        // 讲四个角的世界坐标转为局部坐标坐标
        for (int i = 0; i < targetCorners.Length; i++)
        {
            targetCorners[i] = WorldToScreenPoint(canvas, targetCorners[i]);
        }

        //计算中心点
        center.x = targetCorners[0].x + (targetCorners[3].x - targetCorners[0].x) / 2;
        center.y = targetCorners[0].y + (targetCorners[1].y - targetCorners[0].y) / 2;
        //设置材质的中心点
        material.SetVector("_Center", center);

    }

    //写个重载函数
    public virtual void Guide(Canvas canvas, RectTransform target,float scale,float time)
    {

    }

    // 功能性的方法，不需要重写，所以不需要创建成虚方法
    public Vector2 WorldToScreenPoint(Canvas canvas, Vector3 world)
    {
        //把世界坐标转化为屏幕坐标
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);

        // 屏幕坐标转换为局部坐标
        //out的是vector2类型，事先声明
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                                            screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }

    public bool IsGuideFinish() {
        return !isScaling;
    }
  
}
