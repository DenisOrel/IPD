// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.FilterObject
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public class FilterObject
{
  /// <summary>Фильтрующие объекты</summary>
  public ListItem Filter { get; set; }

  /// <summary>Корневой объект</summary>
  public ListItem RootObject { get; set; }

  /// <summary>Является ли схемой данных</summary>
  public bool IsSheme { get; set; }

  public override string ToString()
  {
    return $"{this.RootObject.Caption}(Для фильтрующего: {this.Filter.Caption})";
  }
}
