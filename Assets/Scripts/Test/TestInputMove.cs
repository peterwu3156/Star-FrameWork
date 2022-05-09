using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInputMove : MonoBehaviour
{
    void Start()
    {
        InputManager.GetInstance().CheckInput(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyDown", CheckInputDown);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyUp", CheckInputUp);
        EventCenter.GetInstance().AddEventListener<float>("Horizontal", HorizontalMove);
        EventCenter.GetInstance().AddEventListener<float>("Vertical", VerticalMove);
        EventCenter.GetInstance().AddEventListener<int>("MouseDown", CheckMouseDown);
    }

    private void CheckInputDown(KeyCode key)
    {
        Debug.Log("����");
    }

    private void CheckInputUp(KeyCode key)
    {
        Debug.Log("̧��");
    }

    private void CheckMouseDown(int button)
    {
        switch (button)
        {
            case 0:
                Debug.Log("�������");
                break;
            case 1:
                Debug.Log("�Ҽ�����");
                break;
        }
    }

    private void HorizontalMove(float direction)
    {
        float speed = direction * 10;
        this.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void VerticalMove(float direction)
    {
        float speed = direction * 10;
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
