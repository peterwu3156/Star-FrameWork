using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�̳������Զ������ĵ���ģʽ����
//����Ҫ�����ֶ�ȥ�ϻ���api����
//ֱ��GetInstance
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            //�Զ�����һ���ն��� ��һ������ģʽ�ű�

            //�л����� ����ᱻɾ�� ���Դ�������
            //���Թ����� Ҫȷ�������Ƴ�
            //����ģʽ���������Ǵ������������������ڵ�
            GameObject.DontDestroyOnLoad(obj);

            instance = obj.AddComponent<T>();
        }
        return instance;
    }

}
