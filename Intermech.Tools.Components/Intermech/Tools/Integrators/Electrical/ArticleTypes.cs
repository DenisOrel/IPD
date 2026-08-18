// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ArticleTypes
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Типы электрических изделий</summary>
public enum ArticleTypes
{
  /// <summary>Компонент схемы (прочее изделие)</summary>
  Component,
  Assembly,
  /// <summary>
  /// Виртуальная сборка, добавляется для группировки плат в проекте,
  /// свойства для нее берутся от документа проекта
  /// </summary>
  VirtualAssembly,
}
