using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// �����л�
/// </summary>
public class ScenesManager : BaseManager<ScenesManager>
{
    
    /// <summary>
    /// ͬ������
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="action">actionί��</param>
    public void LoadScene(string name, UnityAction action)
    {
        SceneManager.LoadScene(name);
        action();
    }

    /// <summary>
    /// ͬ�������޺����߼�
    /// </summary>
    /// <param name="name">������</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// �첽����
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="action">actionί��</param>
    public void LoadSceneAsync(string name, UnityAction action)
    {
        MonoManager.GetInstance().StartCoroutine(IELoadSceneAsync(name, action));    
    }

    /// <summary>
    /// �첽�����޺����߼�
    /// </summary>
    /// <param name="name"></param>
    public void LoadSceneAsync(string name)
    {
        MonoManager.GetInstance().StartCoroutine(IELoadSceneAsync(name));
    }

    /// <summary>
    /// Э���첽����
    /// </summary>
    /// <param name="name">������</param>
    /// <param name="action">actionί��</param>
    /// <returns></returns>
    IEnumerator IELoadSceneAsync(string name, UnityAction action)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        while(!ao.isDone)
        {
            //��������½����� �¼�������Ӵ��� �������þ���
            EventCenter.GetInstance().EventTrigger("Loading", ao.progress);
            yield return ao.progress;
        }

        //������ɺ�Ż�ȥִ��action
        action();
    }

    /// <summary>
    /// Э���첽���� �޺����߼�
    /// </summary>
    /// <param name="name">������</param>
    /// <returns></returns>
    IEnumerator IELoadSceneAsync(string name)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        while (!ao.isDone)
        {
            //��������½����� �¼�������Ӵ��� �������þ���
            EventCenter.GetInstance().EventTrigger("Loading", ao.progress);
            yield return ao.progress;
        }
    }
}
