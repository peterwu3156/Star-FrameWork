using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    /// <summary>
    /// ͬ������ ���ݷ���ָ������
    /// </summary>
    /// <typeparam name="T">����</typeparam>
    /// <param name="ABName">����</param>
    /// <param name="resName">��Դ��</param>
    /// <returns></returns>
    public T LoadRes<T>(string ABName, string resName) where T: Object
    {
        LoadDependencies(ABName);
        //������Դ
        T obj = ABDic[ABName].LoadAsset<T>(resName);
        if (obj == gameObject)
        {
            return Instantiate(obj);
        }
        else
        {
            return obj;
        }
    }

    /// <summary>
    /// �첽����AB����Դ
    /// </summary>
    /// <param name="ABName">����</param>
    /// <param name="resName">��Դ��</param>
    /// <param name="callBack">�ص�����</param>
    public void LoadResAsync(string ABName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(ABName, resName, callBack));
    }


    public void LoadResAsync(string ABName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(ABName, resName, type, callBack));
    }

    public void LoadResAsync<T>(string ABName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(ABName, resName, callBack));
    }

    private IEnumerator ReallyLoadResAsync(string ABName, string resName, UnityAction<Object> callBack)
    {
        LoadDependencies(ABName);
  
        AssetBundleRequest abRequest = ABDic[ABName].LoadAssetAsync(resName);
        yield return abRequest;

        if (abRequest.asset == gameObject)
        {
            callBack(Instantiate(abRequest.asset));
        }
        else
        {
            callBack(abRequest.asset);
        }
    }

    private IEnumerator ReallyLoadResAsync(string ABName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadDependencies(ABName);

        AssetBundleRequest abRequest = ABDic[ABName].LoadAssetAsync(resName, type);
        yield return abRequest;

        if (abRequest.asset == gameObject)
        {
            callBack(Instantiate(abRequest.asset));
        }
        else
        {
            callBack(abRequest.asset);
        }
    }

    private IEnumerator ReallyLoadResAsync<T>(string ABName, string resName, UnityAction<T> callBack) where T: Object
    {
        LoadDependencies(ABName);

        AssetBundleRequest abRequest = ABDic[ABName].LoadAssetAsync<T>(resName);
        yield return abRequest;

        if (abRequest.asset == gameObject)
        {
            callBack(Instantiate(abRequest.asset) as T);
        }
        else
        {
            callBack(abRequest.asset as T);
        }
    }

    /// <summary>
    /// ����ж��
    /// </summary>
    /// <param name="ABName">Ҫж�صİ���</param>
    public void UnLoad(string ABName)
    {
        if(ABDic.ContainsKey(ABName))
        {
            ABDic[ABName].Unload(false);
            ABDic.Remove(ABName);
        }
    }

    /// <summary>
    /// ������м��ص�AB��
    /// </summary>
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        ABDic.Clear();
        mainAB = null;
        manifest = null;
    }
}
