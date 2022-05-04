using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test
{ 
    public Test()
    {
        MonoManager.GetInstance().StartCoroutine(TestCroutine());
        //MonoManager.GetInstance().StopCoroutine("TestCroutine");
        //������ �����Ǳ���������µ�Э�̲ſ�ͨ�����뺯��������
    }

    public void Update()
    {
        Debug.Log("�Ǽ̳�Mono��Update");
    }

    IEnumerator TestCroutine()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("��Mono��Э�̲���");
    }

}

public class UnMonoUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test t = new Test();
        MonoManager.GetInstance().AddUpdateListener(t.Update);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
