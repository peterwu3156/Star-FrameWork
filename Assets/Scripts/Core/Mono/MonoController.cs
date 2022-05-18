using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Mono�Ĺ����� ���ܱ�ֱ��ʹ��
/// </summary>
public class MonoController : MonoBehaviour
{

    private event UnityAction updateEvent;

    void Start()
    {
        DontDestroyOnLoad(this);
    }


    void Update()
    {
        if(updateEvent != null)
        {
            updateEvent.Invoke();
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// ���֡�����¼�
    /// </summary>
    public void AddUpdateListener(UnityAction fun)
    {
        updateEvent += fun;
    }

    /// <summary>
    /// �Ƴ�֡�����¼�
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdateListener(UnityAction fun)
    {
        updateEvent -= fun;
    }
}
