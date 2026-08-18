// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.EmptyModelParametersWriteTargetStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class EmptyModelParametersWriteTargetStrategy : ModelParametersWriteTargetStrategy
{
  /// <summary>
  /// Возвращает признак, что у документа включен режим независимого обозначения.
  /// Иначе, у документа действует режим совместного обозначения с основным исполнением изделия.
  /// </summary>
  /// <param name="document">Документ CAD-системы</param>
  /// <param name="documentProperties">Значения свойств документа</param>
  /// <returns>true - включен режим независимого обозначения документа</returns>
  public override bool IsIndependentDesignationMode(
    IValueBagContainer documentContainer,
    ValueBag documentParameters)
  {
    return true;
  }

  /// <summary>
  /// Возвращает конфигурацию документа, которая соответствует основному исполнению изделия.
  /// Метод используется только в случае режима общего обозначения у документа и у основного исполнения изделия.
  /// </summary>
  /// <param name="document">Документ CAD-системы</param>
  /// <param name="documentProperties">Контейнер с параметрами документа, прочитанными из файла документа</param>
  /// <returns>Конфигурация документа, которая соответствует основному исполнению изделия</returns>
  public override IValueBagContainer GetBasicArticleContainer(
    IValueBagContainer documentContainer,
    ValueBag documentParameters)
  {
    throw new NotSupportedException();
  }
}
