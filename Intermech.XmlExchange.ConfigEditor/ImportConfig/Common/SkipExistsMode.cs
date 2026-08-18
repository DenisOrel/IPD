// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.Common.SkipExistsMode
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;

[Flags]
[TypeConverter(typeof (EnumDescConverter))]
public enum SkipExistsMode
{
  [Description("Действие не задано")] None = 0,
  [Description("Атрибуты объекта не импортируются")] ExcludeAttributes = 1,
  [Description("Состав объекта не импортируется")] ExcludeComposition = 2,
}
