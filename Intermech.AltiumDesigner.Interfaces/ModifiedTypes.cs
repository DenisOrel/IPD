// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.ModifiedTypes
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>Типы изменения значения</summary>
[Description("Типы изменения значения")]
[Category("Misc")]
public enum ModifiedTypes
{
  /// <summary>Не изменялось</summary>
  [Description("Не изменялось")] None,
  /// <summary>Изменено</summary>
  [Description("Изменено")] Changed,
  /// <summary>Добавлено</summary>
  [Description("Добавлено")] Added,
}
