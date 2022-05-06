using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectGuide : GuideBase
{
    
    private float width;        // 镂空区域的宽
    private float height;       // 镂空区域的高
    private float scaleW; //变化之前的宽
    private float scaleH; //变化之前的高

    private float timer; //计时器
    private float time; // 时间
   

    // 引导
    public override void Guide(Canvas canvas, RectTransform target)
    {
        //调用下base,中的方法
        base.Guide(canvas,target);
        // 中心点的计算在base.Guide(canvas,target)有了，
        // 计算宽高
        width = (targetCorners[3].x - targetCorners[0].x)/2;
        height = (targetCorners[1].y - targetCorners[0].y)/2;
        //设置材质的宽高
        material.SetFloat("_SliderX", width);
        material.SetFloat("_SliderY", height);

    }
    //重写引导,有动画
    public override void Guide(Canvas canvas, RectTransform target, float scale, float time)
    {
        //base.Guide(canvas, target, scale, time);不用调用因为是空的
        this.Guide(canvas, target);
        scaleW = width * scale;
        scaleH = height * scale;
        this.material.SetFloat("_SliderX", scaleW);
        this.material.SetFloat("_SliderY", scaleH);
        this.time = time;
        isScaling = true;
        timer = 0;

    }
    private void Update()
    {
        if (isScaling)
        {
            // 每秒变化1/time,时间越短变化的就得越快
            //deltaTime时间增量，保证不同帧率移动的是一样的
            timer += Time.deltaTime * 1 / time;
            this.material.SetFloat("_SliderX", Mathf.Lerp(scaleW, width, timer));
            this.material.SetFloat("_SliderY", Mathf.Lerp(scaleH, height, timer));
            if (timer >= 1)
            {
                timer = 0;
                isScaling = false;
            }
        }
    }

    
    //private void Update()
    //{
    //    Guide(GameObject.Find("Canvas").GetComponent<Canvas>(), GameObject.Find("Button").GetComponent<RectTransform>());
    //}
}
