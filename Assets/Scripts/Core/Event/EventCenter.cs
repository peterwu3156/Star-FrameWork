using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface I_EventGeneric
{ 
    
}

public class EventGeneric<T> : I_EventGeneric
{
    public UnityAction<T> actions;

    public EventGeneric(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventGeneric : I_EventGeneric
{
    public UnityAction actions;

    public EventGeneric(UnityAction action)
    {
        actions += action;
    }
}

/// <summary>
/// �¼����� ����
/// �¼�����Ҫ�����¼�����
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    //����¼������� Key���¼��� value�Ǽ����¼���Ӧ��ί�к�����
    private Dictionary<string, I_EventGeneric> eventDic = new Dictionary<string, I_EventGeneric>();

    /// <summary>
    /// ����¼�����
    /// </summary>
    /// <param name="name">�¼���</param>
    /// <param name="action">�����¼���ί�к���</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        //��û�ж�Ӧ���¼�����
        if(eventDic.ContainsKey(name))
        {
            //�о�ֱ�����
            (eventDic[name] as EventGeneric<T>).actions += action;
        }
        else
        {
            //û�о�Ҫ����
            eventDic.Add(name, new EventGeneric<T>(action));
        }
    }

    /// <summary>
    /// �Ƴ��¼�����
    /// </summary>
    /// <param name="name">�Ƴ����¼���</param>
    /// <param name="action">�¼���ӵ�ί�к���</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventGeneric<T>).actions -= action;
        }
        //����ʱ���� OnDestroy()
    }

    /// <summary>
    /// �¼�����
    /// </summary>
    /// <param name="name">�������¼���</param>
    public void EventTrigger<T>(string name, T info)
    {
        if (eventDic.ContainsKey(name) && (eventDic[name] as EventGeneric<T>).actions != null) 
        {
            (eventDic[name] as EventGeneric<T>).actions.Invoke(info);
        }
    }

    /// <summary>
    /// ����¼����� ��ֹ�����л�ʱ���
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
