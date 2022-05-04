using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �������ģ��
/// </summary>
public class InputManager : BaseManager<InputManager>
{
    private bool Open = false;

    /// <summary>
    /// ���캯�� ��� update����
    /// </summary>
    public InputManager()
    {
        MonoManager.GetInstance().AddUpdateListener(InputUpdate);
    }

    //������رռ��
    public void CheckInput(bool isOpen)
    {
        Open = isOpen;
    }

    /// <summary>
    /// ����ģ���update
    /// </summary>
    private void InputUpdate()
    {
        if (!Open)
        {
            return;
        }
        CheckKeyCode(KeyCode.Escape);
        CheckMouseButton(0);
        CheckMouseButton(1);
        CheckMouseButton(2);

        InputHorizontal();
        InputVertical();

        MouseX();
        MouseY();
        MouseScroll();
    }

    /// <summary>
    /// ��ⰴ��̧���� �ַ��¼�
    /// </summary>
    /// <param name="key">����</param>
    private void CheckKeyCode(KeyCode key)
    {
        //����
        if (Input.GetKeyDown(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyDown", key);
        }
        //̧��
        if (Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyUp", key);
        }
    }

    /// <summary>
    /// ����ˮƽ������
    /// </summary>
    private void InputHorizontal()
    {
        EventCenter.GetInstance().EventTrigger("Horizontal", Input.GetAxis("Horizontal"));
        
    }

    /// <summary>
    /// ������ֱ������
    /// </summary>
    private void InputVertical()
    {
        EventCenter.GetInstance().EventTrigger("Vertical", Input.GetAxis("Vertical"));
    }

    /// <summary>
    /// ������ ̧�� ���� ���� �ַ��¼�
    /// </summary>
    /// <param name="button">����λ</param>
    private void CheckMouseButton(int button)
    {
        if (Input.GetMouseButtonDown(button))
        {
            EventCenter.GetInstance().EventTrigger("MouseDown", button);
        }

        if (Input.GetMouseButtonUp(button))
        {
            EventCenter.GetInstance().EventTrigger("MouseUp", button);
        }

        if(Input.GetMouseButton(button))
        {
            EventCenter.GetInstance().EventTrigger("MouseOn", button);
        }
    }

    /// <summary>
    /// ���ˮƽ����
    /// </summary>
    private void MouseX()
    {
        EventCenter.GetInstance().EventTrigger("Mouse X", Input.GetAxis("Mouse X"));
    }

    /// <summary>
    /// ��괹ֱ����
    /// </summary>
    private void MouseY()
    {
        EventCenter.GetInstance().EventTrigger("Mouse Y", Input.GetAxis("Mouse Y"));
    }

    /// <summary>
    /// �������ƶ�
    /// </summary>
    private void MouseScroll()
    {
        EventCenter.GetInstance().EventTrigger("MouseScroll", Input.mouseScrollDelta);
    }

}
