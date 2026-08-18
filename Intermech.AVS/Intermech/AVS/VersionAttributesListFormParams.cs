// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.VersionAttributesListFormParams
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>
/// Параметры, с которыми работает форма "Редактор списка атрибутов"
/// </summary>
[Serializable]
public class VersionAttributesListFormParams : ICloneable
{
  /// <summary>Список выбранных атрибутов</summary>
  public List<VersionAttribute> Items;

  /// <summary>Создать пустой экземпляр класса</summary>
  public VersionAttributesListFormParams()
    : this(new List<VersionAttribute>())
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Список атрибутов, которые отображаются в примечаниях спецификаций</param>
  public VersionAttributesListFormParams(List<VersionAttribute> items) => this.Items = items;

  /// <summary>Создать экземпляр класса по прототипу</summary>
  /// <param name="template">Прототип</param>
  public VersionAttributesListFormParams(VersionAttributesListFormParams template)
  {
    this.Assign(template);
  }

  /// <summary>Скопировать все данные из указанного источника</summary>
  /// <param name="source">Источник данных</param>
  public virtual void Assign(VersionAttributesListFormParams source)
  {
    this.Items = new List<VersionAttribute>();
    if (source == null)
      return;
    VersionAttributesListFormParams.CopyTo(source.Items, this.Items);
  }

  /// <summary>
  /// Скопировать элементы из коллекции source в коллекцию dest
  /// </summary>
  /// <param name="source">Коллекция-источник</param>
  /// <param name="dest">Коллекция-назначение</param>
  public static void CopyTo(List<VersionAttribute> source, List<VersionAttribute> dest)
  {
    if (source == null || dest == null)
      return;
    dest.Clear();
    for (int index = 0; index < source.Count; ++index)
      dest.Add(source[index].Clone() as VersionAttribute);
  }

  /// <summary>Создать точную копию экземпляра класса</summary>
  /// <returns>Точная копия экземпляра класса</returns>
  public object Clone() => (object) new VersionAttributesListFormParams(this);
}
