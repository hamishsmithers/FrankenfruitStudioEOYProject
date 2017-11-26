using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool m_SharedInstance;
    private List<GameObject> m_lstPooledObjects;
    //---------------
    // Object to Pool
    //---------------
    [LabelOverride("Object To Pool")]
    [Tooltip("This stores the object that will be used by the object pool.")]
    public GameObject m_goObjectToPool;
    //---------------
    // Amount to Pool
    //---------------
    [LabelOverride("Amount to Pool")]
    [Tooltip("An int to choose how many object you want in the pool.")]
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
            GameObject goObj = Instantiate(m_goObjectToPool);
            goObj.SetActive(false);
            m_lstPooledObjects.Add(goObj);
            //Debug.Log("spawned false");
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
                m_lstPooledObjects[i].SetActive(true);
                //Debug.Log("object pool activated");
                return m_lstPooledObjects[i];
            }
        }
        //3   
        return null;
    }
}
