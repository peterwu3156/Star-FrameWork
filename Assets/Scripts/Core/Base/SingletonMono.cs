using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        //�ص㣺�����Լ�new ��ͨ��u3d��ʵ���� Ҳ�����������ں���
        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;
        //���� ��дAwake��������
        //��� ��ɱ������� �麯��
        //������д ����Ҫ����base.Awake();
    }

    //ȱ�� ���ض�� ����ģʽ�ͱ��ƻ���
    //ֻ��������һ�� 
}
