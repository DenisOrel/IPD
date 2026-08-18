// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.IArticlePhysicalPropertiesService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Необязательный сервис для работы с физическими свойствами изделия.
/// </summary>
public interface IArticlePhysicalPropertiesService
{
  /// <summary>
  /// Вычисляет и возвращает массу изделия. Метод может вернуть null, если вычисление массы не поддерживается.
  /// </summary>
  /// <param name="articleItem">Рабочий элемент изделия</param>
  /// <returns>Масса изделия или null</returns>
  MeasuredValue CalculateMass(SectionEntity articleItem);
}
