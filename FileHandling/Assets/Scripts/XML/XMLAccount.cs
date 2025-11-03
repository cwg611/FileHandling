using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


[Serializable]
[XmlRoot("root")]
public class XMLAccount
{
    public string username;

    public string password;

    public List<Person> Person;

    public XMLAccount() { }

    public XMLAccount(string username, string password, List<Person> Person)
    {
        this.username = username;
        this.password = password;
        this.Person = Person;
    }
}

[Serializable]
public class Person
{
    [XmlAttribute]
    public string name;

    [XmlAttribute]
    public int age;

    public Person() { }

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}
