// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADDocumentLoaderFlags
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Флаги, уточняющие загрузку документов в CAD-систему.</summary>
[Flags]
public enum CADDocumentLoaderFlags
{
  /// <summary>Все флаги сброшены</summary>
  None = 0,
  /// <summary>Документ должен быть открыт в окне</summary>
  OpenVisible = 1,
  /// <summary>
  /// После загрузки документа необходимо переключиться на окно CAD-системы. Этот флаг используется
  /// при открытии документов на редактирование или просмотр
  /// </summary>
  SwitchToApp = 2,
}
