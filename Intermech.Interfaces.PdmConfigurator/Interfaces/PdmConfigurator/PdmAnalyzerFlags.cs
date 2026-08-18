// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.PdmAnalyzerFlags
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

using System;

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>Режимы работы анализатора опций объектов</summary>
[Flags]
[Serializable]
public enum PdmAnalyzerFlags
{
  /// <summary>Выполнять поиск опций среди указанных объектов</summary>
  Default = 0,
  /// <summary>
  /// Выполнять поиск опций в составах первого уровня указанных объектов
  /// </summary>
  InCompositions = 1,
  /// <summary>
  /// Выполнять поиск опций в развёрнутых составах указанных объектов
  /// </summary>
  InCompositionsRecursive = 2,
  /// <summary>Игнорировать опции, отмеченные как "устаревшие"</summary>
  IgnoreObsoleteOptions = 268435456, // 0x10000000
}
