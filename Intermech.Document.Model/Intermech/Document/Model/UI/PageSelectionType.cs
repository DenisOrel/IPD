// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.PageSelectionType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Model.UI;

/// <summary>Режимы выборки страниц</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum PageSelectionType
{
  /// <summary>Не выбрано</summary>
  [Description("Не выбрано")] None,
  /// <summary>Текущая</summary>
  [Description("Текущая страница")] ActivePage,
  /// <summary>Все в текущем интервале</summary>
  [Description("Все страницы в интервале")] CurrentRange,
  /// <summary>Все в документе</summary>
  [Description("Все страницы в документе")] All,
}
