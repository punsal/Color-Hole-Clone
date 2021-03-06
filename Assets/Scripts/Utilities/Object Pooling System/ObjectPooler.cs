﻿using System.Collections.Generic;
using UnityEngine;
using Utilities.Object_Pooler_System;

namespace Utilities.Object_Pooling_System
{
  public class ObjectPooler : MonoBehaviour {

    // ReSharper disable once InconsistentNaming
    public static ObjectPooler SharedInstance;
    
    [SerializeField] private List<ObjectPoolItem> itemsToPool = new List<ObjectPoolItem>();
    private List<GameObject> pooledObjects;

    void Awake() {
      SharedInstance = this;
    }

    // Use this for initialization
    void Start () {
      pooledObjects = new List<GameObject>();
      foreach (ObjectPoolItem item in itemsToPool) {
        for (int i = 0; i < item.amountToPool; i++) {
          GameObject obj = (GameObject)Instantiate(item.objectToPool);
          obj.SetActive(false);
          pooledObjects.Add(obj);
        }
      }
    }
	
    public GameObject GetPooledObject(string itemTag) {
      for (int i = 0; i < pooledObjects.Count; i++) {
        if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].CompareTag(itemTag)) {
          return pooledObjects[i];
        }
      }
      foreach (ObjectPoolItem item in itemsToPool) {
        if (item.objectToPool.CompareTag(itemTag)) {
          if (item.poolItemType != PoolItemType.Expandable) continue;
          GameObject obj = (GameObject)Instantiate(item.objectToPool);
          obj.SetActive(false);
          pooledObjects.Add(obj);
          return obj;
        }
      }
      return null;
    }
  }
}