using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRecycle : MonoBehaviour
{

    public IEnumerator Recycle()
    {
        Debug.Log("���տ�ʼ");
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Debug.Log("���ս���");
        PoolManager.GetInstance().PushObj(this.name, this.gameObject);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
