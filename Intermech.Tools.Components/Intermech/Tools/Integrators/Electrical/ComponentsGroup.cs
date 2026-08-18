// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ComponentsGroup
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Группа одинаковых компонентов (соотвествует 1 связи)</summary>
public sealed class ComponentsGroup
{
  /// <summary>Наименование компонента</summary>
  public string PartName { get; private set; }

  /// <summary>Список позиционных идентификаторов на схеме</summary>
  public List<Guid> PosGuids { get; set; }

  /// <summary>Группирующий идентификатор, может быть</summary>
  public string GroupID { get; private set; }

  /// <summary>
  /// Список компонентов (позиционное обозначение=список компонентов схемы/платы)
  /// </summary>
  public Dictionary<string, List<IElectricalComponent>> Components { get; set; }

  /// <summary>Вариант состава</summary>
  public CompositionVariants CompositionVariant { get; private set; }

  public ComponentsGroup()
  {
  }

  public ComponentsGroup(
    string partName,
    Guid posGuid,
    string groupID,
    string posDesignation,
    IElectricalComponent component,
    CompositionVariants compositionVariant)
  {
    this.PartName = partName;
    this.PosGuids = new List<Guid>();
    if (posGuid != Guid.Empty)
      this.PosGuids.Add(posGuid);
    this.GroupID = groupID;
    this.Components = new Dictionary<string, List<IElectricalComponent>>()
    {
      {
        posDesignation,
        new List<IElectricalComponent>((IEnumerable<IElectricalComponent>) new IElectricalComponent[1]
        {
          component
        })
      }
    };
    this.CompositionVariant = compositionVariant;
  }

  public ComponentsGroup CreatePrototype(string groupID)
  {
    return new ComponentsGroup()
    {
      CompositionVariant = this.CompositionVariant,
      GroupID = groupID,
      PartName = this.PartName,
      PosGuids = new List<Guid>(),
      Components = new Dictionary<string, List<IElectricalComponent>>()
    };
  }

  /// <summary>
  /// Идентифицирующая группу строка, состоящая из позиционных обозначений компонентов группы.
  /// При String.Empty - ключ не определен.
  /// </summary>
  public string Key
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      List<string> stringList = new List<string>();
      foreach (KeyValuePair<string, List<IElectricalComponent>> component in this.Components)
      {
        if (string.IsNullOrEmpty(component.Key))
          return string.Empty;
        stringList.Add(component.Key.ToLower());
      }
      stringList.Sort();
      foreach (string str in stringList)
        stringBuilder.Append(str);
      return stringBuilder.ToString();
    }
  }
}
