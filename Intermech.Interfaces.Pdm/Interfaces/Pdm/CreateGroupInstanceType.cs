// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CreateGroupInstanceType
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Что создается</summary>
public enum CreateGroupInstanceType
{
  /// <summary>не наше</summary>
  None,
  /// <summary>Версия исполнения группового изделия</summary>
  ArticleVersion,
  /// <summary>Спецификация</summary>
  Specification,
}
