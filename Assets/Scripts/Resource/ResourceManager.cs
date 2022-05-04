using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��Դ����
/// </summary>
public class ResourceManager : BaseManager<ResourceManager>
{
    /// <summary>
    /// ͬ������
    /// </summary>
    /// <param name="name">��Դ·��</param>
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);

        //�����GameObject��ʵ�����ٷ���
        if(res is GameObject)
        {
            return GameObject.Instantiate(res);
        }

        return res;
    }

    /// <summary>
    /// �첽����
    /// </summary>
    /// <typeparam name="T">���ص���Դ����</typeparam>
    /// <param name="name">��Դ·��</param>
    /// <param name="action">���ص���Դ</param>
    public void LoadAsync<T>(string name, UnityAction<T> action) where T : Object
    {
        MonoManager.GetInstance().StartCoroutine(IELoadAsync<T>(name,action));
    }

    /// <summary>
    /// Э���첽����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    private IEnumerator IELoadAsync<T>(string name, UnityAction<T> action) where T : Object
    {
        ResourceRequest r = Resources.LoadAsync<T>(name);
        yield return r;

        if(r.asset is GameObject)
        {
            action(GameObject.Instantiate(r.asset) as T);
        }

        action(r.asset as T);
    }
}
