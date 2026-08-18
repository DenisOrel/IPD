// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.InheritanceSettingsLevel
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Уровень наследования настроек документов</summary>
public enum InheritanceSettingsLevel
{
  /// <summary>Документ</summary>
  Document = 1,
  /// <summary>Шаблон документа</summary>
  Template = 2,
  /// <summary>Общий шаблон для данного типа документов</summary>
  CommonTemplate = 3,
}
