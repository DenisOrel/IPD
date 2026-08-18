// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Base.Var
// Assembly: Intermech.Workflow.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 43DB3E33-56C8-49B7-85B7-A2947193D068
// Assembly location: D:\IPS\Client\Intermech.Workflow.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Base.xml

using System;
using System.Xml.Serialization;


namespace Intermech.Workflow.Base
{
    [Serializable]
    public class Var
    {
      [XmlElement("ID")]
      public int VariableID { get; set; }

      public string Value { get; set; }
    }
}
