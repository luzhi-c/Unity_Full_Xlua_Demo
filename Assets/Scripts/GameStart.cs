using System;
using UnityEngine;
using XLua;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor; // 这个文件在手机上没有，需要使用条件编译
#endif
using DG.Tweening;

public class GameStart : MonoBehaviour
{

    public Image testImage;
    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    // Start is called before the first frame update

    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable scriptEnv;

    private void Awake()
    {


        
        luaEnv.AddLoader(CustomLoader);


        scriptEnv = luaEnv.NewTable();

        // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);

        luaEnv.DoString("require 'GameStart'", "GameStart", scriptEnv);

        Action luaAwake = scriptEnv.Get<Action>("awake");
        scriptEnv.Get("start", out luaStart);
        scriptEnv.Get("update", out luaUpdate);
        scriptEnv.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null)
        {
            luaAwake();
        }
        // testImage.transform.DOLocalMoveY(100, 0.5f).OnComplete(() => 
        // {
        //     Debug.Log("完成");
        // });
        // transform.localPosition = Vector3.zero
    }

    private byte[] CustomLoader(ref string filepath)
    {
        TextAsset luaAsset;
       #if UNITY_EDITOR
        var fullPath = "Assets/Lua/" + filepath.Replace(".", "/")  + ".lua.txt";
        luaAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(fullPath);
       #else
        luaAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(fullPath);
       #endif
       return luaAsset?.bytes;
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
        if (Time.time - GameStart.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            GameStart.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        scriptEnv.Dispose();
    }
}
