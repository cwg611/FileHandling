using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="CreatConfig",fileName ="MyConfig")]
public class AssetConfig : ScriptableObject
{
    public List<ConfigData> configDatas = new List<ConfigData>();
}

[Serializable]
public class ConfigData
{
    public string name;
    public string message;
}
