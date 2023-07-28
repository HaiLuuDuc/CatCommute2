using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolCharacterModel : Singleton<PoolCharacterModel>
{
    [SerializeField] private GameObject prefab;
    public List<GameObject> pool = new List<GameObject>();
    public int size;

    public void OnInit()
    {
        prefab = UpgradeManager.ins.characterDatas[DataManager.ins.playerData.characterLevel - 1].model.gameObject;
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(this.transform);
            obj.gameObject.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetModel()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // If we get here, all objects are in use
        GameObject model = Instantiate(prefab);
        pool.Add(model);
        return model;
    }

    public void ReturnToPool(GameObject model)
    {
        model.transform.SetParent(this.transform);
        model.gameObject.SetActive(false);
    }

    public void ChangePrefab()
    {
        foreach(GameObject obj in pool)
        {
            Destroy(obj);
        }
        pool.Clear();
        OnInit();
    }

    public void CollectAll()
    {
        foreach(GameObject obj in pool)
        {
            ReturnToPool(obj);
        }
    }
}

