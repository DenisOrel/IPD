// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.CaseSensitiveMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[TypeConverter(typeof (EnumDescConverter))]
public enum CaseSensitiveMode
{
  [Description("Операция не чувствительна к регистру при поиске объектов в базе по указанному атрибуту")] No,
  [Description("Операция чувствительна к регистру при поиске объектов в базе по указанному атрибуту")] Yes,
}
