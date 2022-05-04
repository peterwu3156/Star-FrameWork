using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITest : BasePanel
{
    protected override void Awake()
    {
        base.Awake();//һ��Ҫ������� ����ȷ���ؼ������Ĵ���
    }

    void Start()
    {
        UIManager.AddCustomEventListener(GetUIComponent<Button>("Start"), EventTriggerType.PointerEnter, (data) =>
        {
            Debug.Log("�����밴ť");
        });

        UIManager.AddCustomEventListener(GetUIComponent<Button>("Start"), EventTriggerType.PointerExit, (data) =>
        {
            Debug.Log("����뿪��ť");
        });
    }

    public override void UIComponentOn()
    {
        base.UIComponentOn();
        //����д�������ʾʱҪִ�е��߼�
    }

    public override void UIComponentOff()
    {
        base.UIComponentOff();
        //����д���������ʱҪִ�е��߼�
    }

    protected override void OnClick(string btnName)
    {
        switch(btnName)
        {
            case "Start":
                Debug.Log("��Ϸ��ʼ");
                break;
            case "End":
                Debug.Log("��Ϸ����");
                break;
        }
        
    }

    public void InitPanel()
    {
        Debug.Log("����Ѿ����� ��������߼�");
        Debug.Log(this.gameObject.name);
        //Invoke("DelayHide", 5f);
    }

    public void DelayHide()
    {
        UIManager.GetInstance().HidePanel(this.name);
    }

}
