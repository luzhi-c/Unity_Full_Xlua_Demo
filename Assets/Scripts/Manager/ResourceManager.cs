
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
}