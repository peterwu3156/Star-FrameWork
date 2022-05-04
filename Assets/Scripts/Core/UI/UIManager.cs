using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UI_Layer
{
    Bottom,
    Middle,
    Top,
    System
}

/// <summary>
/// UI������
/// ����������ʾ�����
/// �ṩ���ⲿ��ʾ�����صȽӿ�
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform bottom;
    private Transform middle;
    private Transform top;
    private Transform system;

    //��¼UI canvas������ �����Ժ��ⲿʹ��
    public RectTransform canvas;

    public UIManager()
    {
        //��̬����Canvas ȷ�������������Ƴ�
        GameObject obj = ResourceManager.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        //�ҵ�����
        bottom = canvas.Find("Bottom");
        middle = canvas.Find("Middle");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        //��̬����EventSystem ȷ�������������Ƴ�
        GameObject eventSystem = ResourceManager.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(eventSystem);
    }

    /// <summary>
    /// �õ���Ӧ�㼶�ĸ�����
    /// </summary>
    /// <param name="layer">�㼶</param>
    /// <returns></returns>
    public Transform GetLayerRoot(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bottom: 
                return bottom;
            case E_UI_Layer.Middle:
                return middle;
            case E_UI_Layer.Top:
                return top;
            case E_UI_Layer.System:
                return system;
        }
        return null;
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    /// <typeparam name="T">��屾��</typeparam>
    /// <param name="name">�����</param>
    /// <param name="layer">��ʾ�㼶</param>
    /// <param name="callBack">��崴��������߼� ���ÿ�</param>
    public void ShowPanel<T>(string name, E_UI_Layer layer = E_UI_Layer.Middle, UnityAction<T> callBack = null) where T : BasePanel
    {
        ResourceManager.GetInstance().LoadAsync<GameObject>("UI/" + name, (panel) =>
         {
             if (panelDic.ContainsKey(name)) 
             {
                 panelDic[name].UIComponentOn();

                 //�ظ�����ֱ�������첽���� ��ִ�лص�����
                 if (callBack != null)
                 {
                     callBack(panelDic[name] as T);
                 }

                 return;
             }

             //��ΪCanvas�ĸ��㼶��ĳһ����Ӷ��� ���������λ��
             Transform root = bottom;
             switch (layer)
             {
                case E_UI_Layer.Middle:
                     root = middle;
                     break;
                case E_UI_Layer.Top:
                     root = top;
                     break;
                case E_UI_Layer.System:
                     root = system;
                     break;
             }

             //��ʼ��λ�úʹ�С
             panel.name = name;
             panel.transform.SetParent(root);
             panel.transform.localPosition = Vector3.zero;
             panel.transform.localScale = Vector3.one;
             (panel.transform as RectTransform).offsetMax = Vector2.zero;
             (panel.transform as RectTransform).offsetMin = Vector2.zero;

             //�õ�Ԥ�������ϵ����ű�
             T panelScript = panel.GetComponent<T>();
             //������崴����ɺ���߼� Ȼ�������
             if(callBack != null)
             {
                 callBack(panelScript);
             }

             panelDic.Add(name, panelScript);

             //�����ʾʱ������߼�
             panelDic[name].UIComponentOn();
         });
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="name">�����</param>
    public void HidePanel(string name)
    {
        if(panelDic.ContainsKey(name))
        {
            //�������ʱ������߼�
            panelDic[name].UIComponentOff();
            GameObject.Destroy(panelDic[name].gameObject);
            panelDic.Remove(name);
        }
    }

    /// <summary>
    /// �õ���ʾ�����
    /// </summary>
    /// <typeparam name="T">������</typeparam>
    /// <param name="name">�����</param>
    /// <returns></returns>
    public T GetPanel<T>(string name) where T : BasePanel
    {
        if(panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        return null;
    }

    /// <summary>
    /// ����Զ������
    /// </summary>
    /// <param name="uiComponent">UI�ؼ�</param>
    /// <param name="type">��������</param>
    /// <param name="action">�ص�����</param>
    public static void AddCustomEventListener(UIBehaviour uiComponent, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = uiComponent.GetComponent<EventTrigger>();
        if(trigger == null)
        {
            trigger = uiComponent.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(action);

        trigger.triggers.Add(entry);
    }
}
