// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public abstract class XmpType : XmpEntityBase
    {
      private XmpMetadata m_xmp;

      internal XmpType(
        XmpMetadata xmp,
        XmlNode parent,
        string prefix,
        string localName,
        string namespaceURI)
        : base(parent, prefix, localName, namespaceURI)
      {
        this.m_xmp = xmp != null ? xmp : throw new ArgumentNullException(nameof (xmp));
        this.Initialize();
      }

      protected override void CreateEntity()
      {
        this.EntityParent.AppendChild((XmlNode) this.Xmp.CreateElement(this.EntityPrefix, this.EntityName, this.EntityNamespaceURI));
      }

      protected override XmlElement GetEntityXml()
      {
        XmlNode entityXml = (XmlNode) null;
        if (this.m_xmp.isLoadedDocument)
        {
          if (this.EntityParent.InnerText != "" || this.EntityParent.InnerXml != "")
            entityXml = this.EntityParent.SelectSingleNode($"./{this.EntityPrefix}:{this.EntityName}", this.Xmp.NamespaceManager);
        }
        else
          entityXml = this.EntityParent.SelectSingleNode($"./{this.EntityPrefix}:{this.EntityName}", this.Xmp.NamespaceManager);
        return entityXml as XmlElement;
      }

      protected internal XmpMetadata Xmp => this.m_xmp;
    }
}
