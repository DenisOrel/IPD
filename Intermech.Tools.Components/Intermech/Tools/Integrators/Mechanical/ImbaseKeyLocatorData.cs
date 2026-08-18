// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ImbaseKeyLocatorData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.Data;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Реализует декодер исходных данных для алгоритма поиска изделия по ключу Imbase.
/// </summary>
public sealed class ImbaseKeyLocatorData : IImbaseKeyLocatorData
{
  private readonly SectionEntity articleItem;

  /// <summary>Создает объект.</summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <exception cref="T:ArgumentNullException">articleItem</exception>
  public ImbaseKeyLocatorData(SectionEntity articleItem)
  {
    this.articleItem = articleItem != null ? articleItem : throw new ArgumentNullException(nameof (articleItem));
  }

  /// <summary>Возвращает ключ Imbase.</summary>
  /// <returns>Значение ключа Imbase, может быть равно null или пустой строке</returns>
  public string GetImbaseKey()
  {
    return this.articleItem.Sections.Get<AttributesSection>().WorkingSet.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null);
  }
}
