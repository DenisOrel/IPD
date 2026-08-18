// Decompiled with JetBrains decompiler
// Type: Syncfusion.XPS.SignatureDefinitionType
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Syncfusion.XPS
{
    [DebuggerStepThrough]
    [GeneratedCode("xsd", "2.0.50727.3038")]
    [XmlType(Namespace = "http://schemas.microsoft.com/xps/2005/06/signature-definitions")]
    [DesignerCategory("code")]
    [Serializable]
    public class SignatureDefinitionType
    {
      private string intentField;
      private string langField;
      private DateTime signByField;
      private bool signByFieldSpecified;
      private string signerNameField;
      private string signingLocationField;
      private string spotIDField;
      private SpotLocationType spotLocationField;

      public string Intent
      {
        get => this.intentField;
        set => this.intentField = value;
      }

      [XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
      public string lang
      {
        get => this.langField;
        set => this.langField = value;
      }

      public DateTime SignBy
      {
        get => this.signByField;
        set => this.signByField = value;
      }

      [XmlIgnore]
      public bool SignBySpecified
      {
        get => this.signByFieldSpecified;
        set => this.signByFieldSpecified = value;
      }

      [XmlAttribute]
      public string SignerName
      {
        get => this.signerNameField;
        set => this.signerNameField = value;
      }

      public string SigningLocation
      {
        get => this.signingLocationField;
        set => this.signingLocationField = value;
      }

      [XmlAttribute(DataType = "ID")]
      public string SpotID
      {
        get => this.spotIDField;
        set => this.spotIDField = value;
      }

      public SpotLocationType SpotLocation
      {
        get => this.spotLocationField;
        set => this.spotLocationField = value;
      }
    }
}
