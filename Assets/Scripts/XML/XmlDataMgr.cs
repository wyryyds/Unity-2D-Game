using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XmlDataMgr
{
    private static XmlDataMgr instance = new XmlDataMgr();
    public static XmlDataMgr Instance => instance;

    private XmlDataMgr() { }

    public void SaveData(object data, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        // 存储文件
        using (StreamWriter writer = new StreamWriter(path))
        {
            // 序列化数据
            XmlSerializer s = new XmlSerializer(data.GetType());
            s.Serialize(writer, data);
        }
    }

    public object LoadData(Type type, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".xml";

        // 如果文件不在
        if (!File.Exists(path))
        {
            // 则先看streamingAssetsPath文件夹中是否有该文件
            path = Application.streamingAssetsPath + "/" + fileName + ".xml";
            if (!File.Exists(path))
            {
                // 如果还是没有 就创建一个新的该类型的对象返回(只是默认值)
                if (type == typeof(MusicData))
                {
                    MusicData musicData = new MusicData();
                    musicData.bkVol = 1f;
                    musicData.sound = 1f;
                    return musicData;
                }
                return Activator.CreateInstance(type);
            }
        }

        // 存储文件
        using (StreamReader reader = new StreamReader(path))
        {
            // 反序列化数据
            XmlSerializer s = new XmlSerializer(type);
            // 得到目的数据则返回
            return s.Deserialize(reader);
        }
    }
}
