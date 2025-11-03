using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class ProtoBufTest : MonoBehaviour
{
    string path = Application.streamingAssetsPath + "/StudentData.proto";
    // Start is called before the first frame update
    void Start()
    {
        //List<Student> data = new List<Student>();
        //data.Add(new Student("111", 123, new List<string>() { "qqq", "www" }));
        //data.Add(new Student("222", 456, new List<string>() { "zzz", "www" }));
        //data.Add(new Student("333", 789, new List<string>() { "qqq", "ggg" }));

        ////ProtoSerialize(path,data);

        //var dataLoaded = ProtoDeSerialize<List<Student>>(path);

        //for (int i = 0; i < dataLoaded.Count; i++)
        //{
        //    Debug.Log(dataLoaded[i].age);
        //}

        //SerializeData();
        List<MyData> myDatas = BinaryDeSerilize<List<MyData>>(Application.streamingAssetsPath + "/test.txt");
        for (int i = 0; i < myDatas.Count; i++)
        {
            Debug.Log(myDatas[i].name);
        }
    }

    private void SerializeData()
    {
        List<MyData> data = new List<MyData>();
        data.Add(new MyData("111", 123, new List<string>() { "qqq", "www" }));
        data.Add(new MyData("222", 456, new List<string>() { "zzz", "www" }));
        data.Add(new MyData("333", 789, new List<string>() { "qqq", "ggg" }));

        BinarySerilize(Application.streamingAssetsPath + "/test.txt", data);
    }


    /// <summary>
    /// C#类转换成二进制
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool BinarySerilize<T>(string path,T data) where T:class
    {
        try
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, data);
            }

            return true;
        }
        catch (IOException e)
        {
            Debug.LogError("此类无法转换成二进制 " + data.GetType() + "," + e);
        }

        return false;
    }


    /// <summary>
    /// 反序列化
    /// </summary>
    public T BinaryDeSerilize<T>(string path) where T : class
    {
        try
        {
            using (FileStream loadFile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(loadFile) as T;
            }
        }
        catch (IOException e)
        {
            Debug.LogError("反序列化错误，" + path + "," + e);
        }

        return null;
    }
    //
    public bool ProtoSerialize(string path,UnityEngine.Object obj)
    {
        try
        {
            using (Stream file = File.Create(path))
            {
                Serializer.Serialize(file, obj);
                return true;
            }
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            return false;
        }
    }

    //反序列化proto文件
    public static T ProtoDeSerialize<T>(string path) where T : class
    {
        try
        {
            using (FileStream file = File.OpenRead(path))
            {
                return Serializer.Deserialize<T>(file);
            }
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            return null;
        }
    }

    //序列化保存数据为proto
    public static bool ProtoSerialize<T>(string path,T data) where T : class
    {
        try
        {
            using (FileStream stream = File.OpenWrite(path))
            {
                //序列化后的数据存入文件
                ProtoBuf.Serializer.Serialize(stream, data);
                Debug.Log("Proto写入");
                return true;
            }
        }
        catch (IOException e)
        {
            Debug.LogError(e);
            return false;
        }
    }


    [ProtoBuf.ProtoContract]
    public class Student
    {
        [ProtoBuf.ProtoMember(1)]
        public string name;
        [ProtoBuf.ProtoMember(2)]
        public int age;
        [ProtoBuf.ProtoMember(3)]
        public List<string> message;

        public Student() { }

        public Student(string name, int age, List<string> m = null)
        {
            this.name = name;
            this.age = age;
            message = m;
        }
    }
}
