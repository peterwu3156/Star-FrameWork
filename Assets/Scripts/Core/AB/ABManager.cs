using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABManager : SingletonAutoMono<ABManager>
{
    //AB�������ظ����� �����������ֵ����洢���ع���AB��
    private Dictionary<string, AssetBundle> ABDic = new Dictionary<string, AssetBundle>();

    //����
    private AssetBundle mainAB = null;
    private AssetBundleManifest manifest = null;

    /// <summary>
    /// AB�����·�� ��������޸�
    /// </summary>
    private string Path_Url
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    /// <summary>
    /// ������ �����ƽ̨�����仯
    /// </summary>
    private string MainAB_Name
    {
        get
        {
            #if UNITY_IOS
                return "IOS";
            #elif UNITY_ANDROID
                return "Android"
            #else
                return "PC";
            #endif
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="ABName">Ŀ�����</param>
    public void LoadDependencies(string ABName)
    {
        AssetBundle ab = null;

        //��������
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(Path_Url + MainAB_Name);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //����������
        string[] strs = manifest.GetAllDependencies(ABName);
        for (int i = 0; i < strs.Length; i++)
        {
            if (!ABDic.ContainsKey(strs[i]))
            {
                AssetBundle abDepend = AssetBundle.LoadFromFile(Path_Url + strs[i]);
                ABDic.Add(strs[i], abDepend);
            }
        }

        //����Ŀ����Դ��
        if (!ABDic.ContainsKey(ABName))
        {
            ab = AssetBundle.LoadFromFile(Path_Url + ABName);
            ABDic.Add(ABName, ab);
        }
    }

    /// <summary>
    /// ͬ������AB����Դ
    /// </summary>
    /// <param name="ABName">AB����</param>
    /// <param name="resName">��Դ��</param>
    public object LoadRes(string ABName, string resName)
    {
        LoadDependencies(ABName);
        //������Դ
        Object obj = ABDic[ABName].LoadAsset(resName);
        if(obj == gameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

    /// <summary>
    /// ͬ������ ����ָ������
    /// </summary>
    /// <param name="ABName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public object LoadRes(string ABName, string resName, System.Type type)
    {
        LoadDependencies(ABName);
        //������Դ
        Object obj = ABDic[ABName].LoadAsset(resName, type);
        if (obj == gameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

    public T LoadRes<T>(string ABName, string resName)
    {
        LoadDependencies(ABName);
        //������Դ
        return null;
    }

    /// <summary>
    /// ����ж��
    /// </summary>
    /// <param name="ABName">Ҫж�صİ���</param>
    public void UnLoad(string ABName)
    {

    }

    /// <summary>
    /// ������м��ص�AB��
    /// </summary>
    public void ClearAB()
    {

    }
}
