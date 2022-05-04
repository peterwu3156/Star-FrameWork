using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�Ѷ���ر���Ͷ������ķ���ֿ���д
//�������ķ������ 
//����صĲ���

/// <summary>
/// ��������ĳһ����
/// </summary>
public class PoolData
{
    //����������صĸ��ڵ� ������unity������ʾ�л����ֲ㼶
    public GameObject rootObj;
    //����ķ�������
    public List<GameObject> poolList;

    /// <summary>
    /// ���캯��  
    /// </summary>
    /// <param name="obj">������������</param>
    /// <param name="poolObj">����ĸ��ڵ㣨������pool��</param>
    public PoolData(GameObject obj, GameObject poolObj)
    {
        //�¹� �� ����
        //pool�·��������ļ���
        rootObj = new GameObject(obj.name);
        rootObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        PushObj(obj);
    }

    /// <summary>
    /// ��������������
    /// </summary>
    /// <param name="obj">�������Ķ���</param>
    public void PushObj(GameObject obj)
    {
        obj.SetActive(false);//����
        poolList.Add(obj);
        obj.transform.parent = rootObj.transform;
    }

    /// <summary>
    /// �ӷ�����ȡ������
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        GameObject obj = null;

        obj = poolList[0];
        poolList.RemoveAt(0);


        obj.SetActive(true);//�ǵ�һ��Ҫ����
        obj.transform.parent = null;//����ʾ��pool��ķ����� ������ʾ������

        return obj;
    }
}


public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;

    /// <summary>
    /// �ӷ������첽���ض���
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="action">actionί��</param>
    public void GetObj(string name, UnityAction<GameObject> action)
    {
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0) 
        {
            action(poolDic[name].GetObj());
        }
        else
        {
            ResourceManager.GetInstance().LoadAsync<GameObject>(name, (obj) =>
            {
                obj.name = name;
                action(obj);
            });
        }
    }

    /// <summary>
    /// ������󵽶�Ӧ������
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="obj">����Ķ���</param>
    public void PushObj(string name, GameObject obj)
    {
        if(poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }

        if(poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            //û�ж�Ӧ�ķ���
            poolDic.Add(name, new PoolData(obj,poolObj));
        }
    }

    /// <summary>
    /// ��ջ���� ��ֹ�ڳ����л���ʱ�����
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}
