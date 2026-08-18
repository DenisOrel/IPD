// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpSimpleType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public class XmpSimpleType : XmpType
    {
      internal XmpSimpleType(
        XmpMetadata xmp,
        XmlNode parent,
        string prefix,
        string localName,
        string namespaceURI)
        : base(xmp, parent, prefix, localName, namespaceURI)
      {
      }

      protected override void CreateEntity()
      {
        this.EntityParent.AppendChild((XmlNode) this.Xmp.CreateElement(this.EntityPrefix, this.EntityName, this.EntityNamespaceURI));
      }

      protected internal bool GetBool() => XmpUtils.GetBoolValue(this.Value);

      protected internal DateTime GetDateTime() => XmpUtils.GetDateTimeValue(this.Value);

      protected internal int GetInt() => XmpUtils.GetIntValue(this.Value);

      protected internal float GetReal() => XmpUtils.GetRealValue(this.Value);

      protected internal Uri GetUri() => XmpUtils.GetUriValue(this.Value);

      protected internal void SetBool(bool value) => XmpUtils.SetBoolValue(this.XmlData, value);

      protected internal void SetDateTime(DateTime value)
      {
        XmpUtils.SetDateTimeValue(this.XmlData, value);
      }

      protected internal void SetInt(int value) => XmpUtils.SetIntValue(this.XmlData, value);

      protected internal void SetReal(float value) => XmpUtils.SetRealValue(this.XmlData, value);

      protected internal void SetUri(Uri value) => XmpUtils.SetUriValue(this.XmlData, value);

      public string Value
      {
        get => this.XmlData != null ? this.XmlData.InnerXml : string.Empty;
        set
        {
          if (value == null)
            throw new ArgumentNullException(nameof (Value));
          XmpUtils.SetTextValue(this.XmlData, value);
        }
      }
    }
}
