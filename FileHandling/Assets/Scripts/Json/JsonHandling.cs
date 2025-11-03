using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class JsonHandling : MonoBehaviour
{
    private string json = null;

    private void Start()
    {
        //SaveJson();
        LoadJson();
        //StartCoroutine(LOAD());
    }

    IEnumerator LOAD()
    {
        yield return 0;
        LoadJson();
    }

    private void SaveJson()
    {
        List<MyData> data = new List<MyData>();
        data.Add(new MyData("111", 123, new List<string>() { "qqq", "www" }));
        data.Add(new MyData("222", 456, new List<string>() { "zzz", "www" }));
        data.Add(new MyData("333", 789, new List<string>() { "qqq", "ggg" }));
        json = JsonHelper.ToJson(data);
        Debug.Log(json);
        FileStream fs = new FileStream(Application.streamingAssetsPath+"/jsontest.json",FileMode.Create);
        //存储时时二进制,所以这里需要把我们的字符串转成二进制
        byte[] bytes = new UTF8Encoding().GetBytes(json);
        fs.Write(bytes, 0, bytes.Length);
        //每次读取文件后都要记得关闭文件
        fs.Close();
        print("写入完成");
    }

    private void LoadJson()
    {
        byte[] bytes = new byte[1024];
        //StreamReader streamreader = new StreamReader(Application.streamingAssetsPath + "/jsontest.json");//读取数据，转换成数据流
        using (FileStream fs = new FileStream(Application.streamingAssetsPath + "/jsontest.json", FileMode.Open))
        {
            int count = (int)fs.Length;
            fs.Read(bytes, 0, count);
            //将读取到的二进制转换成字符串
            string s = new UTF8Encoding().GetString(bytes);
            Debug.LogError(s);
            var data= JsonHelper.FromJson<MyData>(s);
            //var data = JsonUtility.FromJson<List<JsonData>>(json);
            for (int i = 0; i < data.Length; i++)
            {
                Debug.Log(data[i].name);
            }
        };
    }

}

[Serializable]
public class MyData
{
    public string name;
    public int age;
    public List<string> message;

    public MyData()
    {

    }

    public MyData(string name, int age, List<string> m = null)
    {
        this.name = name;
        this.age = age;
        message = m;
    }
}
