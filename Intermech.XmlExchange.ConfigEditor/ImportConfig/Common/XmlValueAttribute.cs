// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.XmlValueAttribute
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[AttributeUsage(AttributeTargets.Field)]
internal class XmlValueAttribute : Attribute
{
  public XmlValueAttribute(string xmlValue) => this.XmlValue = xmlValue;

  public string XmlValue { get; private set; }
}
