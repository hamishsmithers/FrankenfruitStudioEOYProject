using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool m_SharedInstance;
    public List<GameObject> m_lstPooledObjects;
    public GameObject m_goObjectToPool;
    public int m_nAmountToPool;

    // Use this for initialization
    void Awake()
    {
        m_SharedInstance = this;
    }

    private void Start()
    {
        m_lstPooledObjects = new List<GameObject>();

        for (int i = 0; i < m_nAmountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_goObjectToPool);
            obj.SetActive(false);
            m_lstPooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetPooledObject()
    {
        //1
        for (int i = 0; i < m_lstPooledObjects.Count; i++)
        {
            //2
            if (!m_lstPooledObjects[i].activeInHierarchy)
            {
                return m_lstPooledObjects[i];
            }
        }
        //3   
        return null;
    }
}
