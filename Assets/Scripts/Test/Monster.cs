using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    void Start()
    {
        Die();
    }

    public void Die()
    {
        Debug.Log("��������");
        //�����¼�
        EventCenter.GetInstance().EventTrigger("MonsterDie", this);
    }

}
