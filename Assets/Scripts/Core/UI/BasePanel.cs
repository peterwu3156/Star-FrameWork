using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ������ ���ҵ��Լ�����µ����пؼ�����
/// �ṩ��ʾ�������صĽӿ�
/// </summary>
public class BasePanel : MonoBehaviour
{
    //�����滻ԭ�� ���UI���
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenUIComponents<Image>();
        FindChildrenUIComponents<Text>();
        FindChildrenUIComponents<Button>();
        FindChildrenUIComponents<Slider>();
        FindChildrenUIComponents<Toggle>();
        FindChildrenUIComponents<ScrollRect>();
        FindChildrenUIComponents<InputField>(); 
        FindChildrenUIComponents<ToggleGroup>();
    }

    /// <summary>
    /// ��ʾ���ʱ������߼� ������������д
    /// </summary>
    public virtual void UIComponentOn()
    {

    }

    /// <summary>
    /// �������ʱ������߼� ������������д
    /// </summary>
    public virtual void UIComponentOff()
    {

    }    

    /// <summary>
    /// ��ť����¼� ������������
    /// </summary>
    /// <param name="btnName">��ť������</param>
    protected virtual void OnClick(string btnName)
    {

    }

    /// <summary>
    /// ��ѡ�Ͷ�ѡ�ĵ���¼� �����������Ⲣ��д
    /// </summary>
    /// <param name="toggleName">��ѡ�������</param>
    /// <param name="value">��ѡ���״̬</param>
    protected virtual void OnValueChange(string toggleName, bool value)
    {

    }    

    /// <summary>
    /// �õ���Ӧ���ֵ�UI�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetUIComponent<T>(string name) where T : UIBehaviour
    {
        if(controlDic.ContainsKey(name))
        {
            for (int i = 0; i < controlDic[name].Count; i++)
            {
                if(controlDic[name][i] is T)
                {
                    return controlDic[name][i] as T;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// �ҵ������������µĸ����͵���������ֵ�
    /// </summary>
    /// <typeparam name="T">UI����</typeparam>
    private void FindChildrenUIComponents<T>() where T : UIBehaviour
    {
        T[] components = this.GetComponentsInChildren<T>();
        
        for(int i = 0; i < components.Length; i++)
        {
            string objName = components[i].name;//С�ıհ�

            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(components[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour> { components[i] });
            }

            //��ť�����߼�
            if(components[i] is Button)
            {
                (components[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            //��ѡ���ѡ�����߼�
            else if (components[i] is Toggle)
            {
                (components[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChange(objName, value);
                });
            }
        }
    }
}
