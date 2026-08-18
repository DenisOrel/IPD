
// Type: Intermech.Interfaces.HashContent
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Класс работы с блобом настройки данных, из которых формируется хэш подписи.
    /// </summary>
    public class HashContent : IHashContent
    {
      private bool compatible;
      private List<AttributeHashContentClass> attributes = new List<AttributeHashContentClass>();
      private List<RelationHashContentClass> relations = new List<RelationHashContentClass>();
      private List<string> files = new List<string>();

      /// <summary>Совместимая подпись</summary>
      public bool Compatible
      {
        get => this.compatible;
        set => this.compatible = value;
      }

      /// <summary>Список атрибутов - для Compatible == false</summary>
      public List<AttributeHashContentClass> Attributes => this.attributes;

      /// <summary>
      /// Список связей и атрибутов к ним - для Compatible == false
      /// </summary>
      public List<RelationHashContentClass> Relations => this.relations;

      /// <summary>
      /// Список имен файлов файлового атрибута "Файл" - для Compatible == true
      /// </summary>
      public List<string> Files => this.files;

      private void Clear() => this.Clear(false);

      public void Clear(bool compatible)
      {
        this.compatible = compatible;
        this.attributes.Clear();
        this.relations.Clear();
        this.files.Clear();
      }

      public void Load(Stream stream)
      {
        XmlDocument xmlDocument = new XmlDocument();
        stream.Position = 0L;
        xmlDocument.Load(stream);
        this.Load(xmlDocument);
      }

      public void Load(XmlDocument xmlDocument) => this.LoadInner(xmlDocument);

      public void Save(Stream stream) => this.Save().Save(stream);

      public XmlDocument Save() => this.SaveInner();

      private bool LoadInner(XmlDocument xmlDocument)
      {
        this.Clear();
        XmlNode xmlNode1 = xmlDocument.SelectSingleNode(HashContentConst.HashSec);
        if (xmlNode1 == null)
          return false;
        this.compatible = xmlNode1.Attributes[HashContentConst.CompatibleVal].Value == "1";
        XmlNode xmlNode2 = xmlNode1.FirstChild;
        if (xmlNode2 != null)
        {
          do
          {
            if (!this.compatible)
            {
              if (xmlNode2.Name == HashContentConst.AttributeSec)
                this.attributes.Add(new AttributeHashContentClass(new Guid(xmlNode2.Attributes[HashContentConst.GuidVal].Value)));
              else if (xmlNode2.Name == HashContentConst.RelationSec)
              {
                RelationHashContentClass hashContentClass = new RelationHashContentClass();
                this.relations.Add(hashContentClass);
                XmlAttribute attribute1 = xmlNode2.Attributes[HashContentConst.GuidVal];
                hashContentClass.Guid = new Guid(attribute1.Value);
                XmlNode xmlNode3 = xmlNode2.FirstChild;
                if (xmlNode3 != null)
                {
                  do
                  {
                    if (xmlNode3.Name == HashContentConst.AttributeSec)
                    {
                      XmlAttribute attribute2 = xmlNode3.Attributes[HashContentConst.GuidVal];
                      hashContentClass.Add(new AttributeHashContentClass(new Guid(attribute2.Value)));
                    }
                    xmlNode3 = xmlNode3.NextSibling;
                  }
                  while (xmlNode3 != null);
                }
              }
            }
            else if (xmlNode2.Name == HashContentConst.FileSec)
              this.files.Add(xmlNode2.Attributes[HashContentConst.NameVal].Value);
            xmlNode2 = xmlNode2.NextSibling;
          }
          while (xmlNode2 != null);
        }
        return true;
      }

      private XmlDocument SaveInner()
      {
        XmlDocument xmlDocument = new XmlDocument();
        XmlNode element1 = (XmlNode) xmlDocument.CreateElement(HashContentConst.HashSec);
        xmlDocument.AppendChild(element1);
        XmlAttribute attribute1 = xmlDocument.CreateAttribute(HashContentConst.HashVerVal);
        attribute1.Value = ((int) HashContentConst.HashContentVersion).ToString();
        element1.Attributes.Append(attribute1);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute(HashContentConst.CompatibleVal);
        attribute2.Value = this.compatible ? "1" : "0";
        element1.Attributes.Append(attribute2);
        if (!this.compatible)
        {
          for (int index = 0; index < this.attributes.Count; ++index)
          {
            XmlNode element2 = (XmlNode) xmlDocument.CreateElement(HashContentConst.AttributeSec);
            XmlAttribute attribute3 = xmlDocument.CreateAttribute(HashContentConst.GuidVal);
            attribute3.Value = this.attributes[index].Guid.ToString();
            element2.Attributes.Append(attribute3);
            element1.AppendChild(element2);
          }
          for (int index1 = 0; index1 < this.relations.Count; ++index1)
          {
            XmlElement element3 = xmlDocument.CreateElement(HashContentConst.RelationSec);
            XmlAttribute attribute4 = xmlDocument.CreateAttribute(HashContentConst.GuidVal);
            attribute4.Value = this.relations[index1].Guid.ToString();
            element3.Attributes.Append(attribute4);
            element1.AppendChild((XmlNode) element3);
            for (int index2 = 0; index2 < this.relations[index1].Count; ++index2)
            {
              XmlElement element4 = xmlDocument.CreateElement(HashContentConst.AttributeSec);
              XmlAttribute attribute5 = xmlDocument.CreateAttribute(HashContentConst.GuidVal);
              attribute5.Value = this.relations[index1][index2].Guid.ToString();
              element4.Attributes.Append(attribute5);
              element3.AppendChild((XmlNode) element4);
            }
          }
        }
        else
        {
          for (int index = 0; index < this.files.Count; ++index)
          {
            XmlElement element5 = xmlDocument.CreateElement(HashContentConst.FileSec);
            XmlAttribute attribute6 = xmlDocument.CreateAttribute(HashContentConst.NameVal);
            attribute6.Value = this.files[index];
            element5.Attributes.Append(attribute6);
            element1.AppendChild((XmlNode) element5);
          }
        }
        return xmlDocument;
      }
    }
}
