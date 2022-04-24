
using System.Collections;
using System;
using UnityEngine;
public class ResourceManager : SingletenBase<ResourceManager> {
    public override void Init()
    {
        base.Init();
    }

    public GameObject LoadUI(string url) {
        var go = Resources.Load<GameObject>(url);
        return GameObject.Instantiate(go);
    }

    public GameObject LoadPrefab(string url) {
        var go = Resources.Load<GameObject>(url);
        return go;
    }

    public void LoadPrefabAsync(string url, Action<GameObject> action) {
        StartCoroutine(StartLoadAsync(url, action));
    }

    IEnumerator StartLoadAsync(string url, Action<GameObject> action) {
        var load =  Resources.LoadAsync<GameObject>(url);
        while(!load.isDone) {
            Debug.Log("加载中");
            yield return null;
        }
        action?.Invoke(load.asset as GameObject);
    }
}