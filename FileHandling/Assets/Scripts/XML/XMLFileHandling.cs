using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Text;

public class XMLFileHandling : MonoBehaviour
{
    private string filename = "test.xml";

    // Start is called before the first frame update
    private void Start()
    {
        //DeserializeXML();

        LoadData();


        // CreatAxmlFile();
    }

    private void CreatMyxml()
    {
        Person one = new Person("11111", 11111);
        Person two = new Person("22222", 22222);
        Person three = new Person("33333", 33333);
        List<Person> people = new List<Person>();
        people.Add(one);
        people.Add(two);
        people.Add(three);
        XMLAccount xMLAccount = new XMLAccount("TestAccount", "123", people);
        using (FileStream fileStream = new FileStream(Application.streamingAssetsPath + "/test.xml", FileMode.Create))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLAccount));
            xmlSerializer.Serialize(fileStream, xMLAccount);
        }
    }

    private void LoadData()
    {
        string data = Resources.Load(filename.Split('.')[0]).ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);
        XmlNodeList nodeList = xmlDoc.SelectSingleNode("root").ChildNodes;//获取根节点信息
        for (int i = 0; i < nodeList.Count; i++)
        {
            Debug.Log(nodeList[i].Name);
            if (nodeList[i].Name == "Person")  //其中一个子节点的信息
            {
                foreach (XmlElement xe in nodeList)
                {
                    string a = xe.GetAttribute("name");
                    string b = xe.GetAttribute("age");
                    //int b =int.Parse(xe.GetAttribute("age"));

                    Debug.Log(a+b);
                }
            }

        }
    }

    private void DeserializeXML()
    {
        FileStream fs = new FileStream(Application.streamingAssetsPath + "/test.xml", FileMode.Open, FileAccess.Read);

        XmlSerializer serializer = new XmlSerializer(typeof(XMLAccount)); //指定反序列化的类型

        XMLAccount baseInfo = (XMLAccount)serializer.Deserialize(fs);

        Debug.LogError(baseInfo.password);

        fs.Close();
    }

    public void CreatAxmlFile()
    {
        XmlDocument doc = new XmlDocument(); //创建xml文件对象

        XmlNode xmldct = doc.CreateXmlDeclaration("1.0", "utf - 8", null);  //创建xml头

        doc.AppendChild(xmldct);  //添加xml头

        XmlNode root = doc.CreateElement("users");  //创建xml根节点（元素属于节点）users

        doc.AppendChild(root); //添加Xml根节点

        XmlNode xn_element = doc.CreateNode(XmlNodeType.Element, "name", null);  //创建子节点

        xn_element.InnerText = "Albert"; //设置子节点的值

        XmlAttribute xa = doc.CreateAttribute("no");  //创建属性

        xa.Value = "1234";  //设置属性值

        XmlDocument xd = xn_element.OwnerDocument; //获取元素的document

        xn_element.Attributes.SetNamedItem(xa); //设置元素属性

        root.AppendChild(xn_element); //添加子节点到root节点

        doc.Save(Application.dataPath + "/test.xml"); //保存xml
    }
}
