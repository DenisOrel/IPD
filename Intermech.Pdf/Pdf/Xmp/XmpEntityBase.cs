// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpEntityBase
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public abstract class XmpEntityBase
    {
      private string m_entityPrefix;
      private string m_localName;
      private string m_namespaceURI;
      private XmlNode m_xmlParent;

      protected internal XmpEntityBase(
        XmlNode parent,
        string prefix,
        string localName,
        string namespaceURI)
      {
        if (parent == null)
          throw new ArgumentNullException(nameof (parent));
        if (localName == null)
          throw new ArgumentNullException(nameof (localName));
        this.m_xmlParent = parent;
        this.m_entityPrefix = prefix;
        this.m_localName = localName;
        this.m_namespaceURI = namespaceURI;
      }

      protected virtual bool CheckIfExists() => this.GetEntityXml() != null;

      protected abstract void CreateEntity();

      protected abstract XmlElement GetEntityXml();

      protected virtual bool GetSuspend() => false;

      protected virtual void Initialize()
      {
        if (this.SuspendInitialization || this.Exists)
          return;
        this.CreateEntity();
      }

      internal void SetXmlParent(XmlNode parent)
      {
        this.m_xmlParent = parent != null ? parent : throw new ArgumentNullException(nameof (parent));
      }

      protected internal string EntityName => this.m_localName;

      protected internal string EntityNamespaceURI => this.m_namespaceURI;

      protected internal XmlNode EntityParent => this.m_xmlParent;

      protected internal string EntityPrefix => this.m_entityPrefix;

      protected internal bool Exists => this.CheckIfExists();

      protected bool SuspendInitialization => this.GetSuspend();

      public XmlElement XmlData => this.GetEntityXml();
    }
}
