// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Data.CADLinkTypes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Data;

/// <summary>
/// Описывает разрешенные типы связей в CAD системе между 3D-моделями.
/// </summary>
public enum CADLinkTypes
{
  /// <summary>
  /// Структурная зависимость (по дереву построения сборочной единицы)
  /// </summary>
  Structural,
  /// <summary>Ассоциативная зависимость</summary>
  Associative,
}
