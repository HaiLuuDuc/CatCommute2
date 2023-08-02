using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public enum ModelType
{
    Banana = 0,
    Avocado = 1,
    Carrot = 2,
    Lemon = 3,
    Apple = 4,
    Pear = 5,
    Kiwi = 6,
    Grape = 7,
    Pineapple = 8,
    Pumpkin = 9
}
[Serializable]
public struct Pool
{
    public List<GameObject> pool;
}
public class PoolCharacterModel : Singleton<PoolCharacterModel>
{
    public Dictionary<ModelType, Pool> dict = new Dictionary<ModelType, Pool>();
    [SerializeField] private GameObject prefab;
    public List<GameObject> mainPool = new List<GameObject>();
    public int size = 10;

    public void InitAllPools()
    {
        dict = new Dictionary<ModelType, Pool>();
        for (int i = 0; i < 9; i++)
        {
            ModelType type = (ModelType)i;
            Pool poolData = new Pool();
            poolData.pool = new List<GameObject>();
            for (int j = 0; j < size; j++)
            {
                GameObject obj = Instantiate(UpgradeManager.ins.characterDatas[i].model);
                obj.transform.SetParent(this.transform);
                obj.gameObject.SetActive(false);
                poolData.pool.Add(obj);
            }
            dict.Add(type, poolData); // Add the modified Pool instance to the dictionary
        }
    }

    /*public void OnInit()
    {
        prefab = UpgradeManager.ins.characterDatas[DataManager.ins.playerData.characterLevel - 1].model.gameObject;
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(this.transform);
            obj.gameObject.SetActive(false);
            mainPool.Add(obj);
        }
    }*/
    public void InitMainPool()
    {
        mainPool = dict[(ModelType)DataManager.ins.playerData.characterLevel - 1].pool;
    }

    public GameObject GetModel()
    {
        foreach (GameObject obj in mainPool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        // If we get here, all objects are in use
        GameObject model = Instantiate(prefab);
        mainPool.Add(model);
        return model;
    }

    public void ReturnToPool(GameObject model)
    {
        model.transform.SetParent(this.transform);
        model.gameObject.SetActive(false);
    }

    /*public void ChangePrefab()
    {
        foreach (GameObject obj in mainPool)
        {
            Destroy(obj);
        }
        mainPool.Clear();
        OnInit();
    }*/
    public void ChangePrefab()
    {
        foreach(GameObject gameObject in mainPool)
        {
            ReturnToPool(gameObject);
        }
        InitMainPool();
    }

    public void CollectAll()
    {
        foreach(GameObject obj in mainPool)
        {
            ReturnToPool(obj);
        }
    }
}

