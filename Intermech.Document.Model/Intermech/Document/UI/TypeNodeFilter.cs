// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.TypeNodeFilter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Фильтр узлов по типу</summary>
public class TypeNodeFilter : NodeFilter
{
  /// <summary>Исключать типы</summary>
  public Type[] Exclude;
  /// <summary>Включать типы</summary>
  public Type[] Include;

  /// <summary>Конструктор</summary>
  /// <param name="exclude">Исключать типы</param>
  /// <param name="include">Включать типы</param>
  public TypeNodeFilter(Type[] exclude, Type[] include)
  {
    this.Exclude = exclude;
    this.Include = include;
  }

  /// <summary>Конструктор</summary>
  /// <param name="oneType">Допустимый тип</param>
  public TypeNodeFilter(Type oneType)
  {
    this.Exclude = (Type[]) null;
    this.Include = new Type[1]{ oneType };
  }

  /// <summary>Конструктор</summary>
  public TypeNodeFilter()
  {
  }

  /// <summary>Проверить узел на соответствие фильтру</summary>
  /// <param name="node">Узел документа</param>
  /// <returns>true, если удовлетворяет условиям фильтра</returns>
  public override bool CheckNode(object node)
  {
    return node is DocumentTreeNode documentTreeNode && documentTreeNode.FilterCheck(this.Exclude, this.Include);
  }
}
