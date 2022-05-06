using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGuide : GuideBase
{
    private float r; // 镂空区域圆形的半径

    private float scaleR; //变化之前的半径大小

    private float timer; //计时器
    private float time; // 时间

    //重写引导，没有动画
    public override void Guide(Canvas canvas, RectTransform target)
    {
        base.Guide(canvas, target);
        float width = (targetCorners[3].x - targetCorners[0].x) / 2;
        float height = (targetCorners[1].y - targetCorners[0].y) / 2;
        //计算半径,宽/2的平方 + 高/2的平方，之后开方
        r = Mathf.Sqrt(width * width + height * height);
        // 赋值给材质
        material.SetFloat("_Slider", r);
    }

    //重写引导,有动画
    public override void Guide(Canvas canvas, RectTransform target, float scale, float time)
    {
        //base.Guide(canvas, target, scale, time);不用调用因为是空的
        this.Guide(canvas,target);
        scaleR = r * scale;
        this.material.SetFloat("_Slider", scaleR);
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
            this.material.SetFloat("_Slider", Mathf.Lerp(scaleR, r, timer)); 
            if(timer >= 1)
            {
                timer = 0;
                isScaling = false;
            }
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    Guide(GameObject.Find("Canvas").GetComponent<Canvas>(), GameObject.Find("Button_cir").GetComponent<RectTransform>());
    //}
}
