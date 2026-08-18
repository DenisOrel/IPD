// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecArticleType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace Intermech.AVS;

/// <summary>Класс с типом специфицируемого изделия</summary>
internal class SpecArticleType : IComparable<SpecArticleType>
{
  /// <summary>Тип специфицируемого объекта</summary>
  internal int ObjectType;
  /// <summary>Название типа специфицируемого объекта</summary>
  internal string ObjTypeName;
  /// <summary>Разрешено ли выбирать указанный тип объекта</summary>
  internal bool Enabled;
  /// <summary>Значок для указанного типа</summary>
  internal Icon Icon;
  /// <summary>Идентификтор классификатора</summary>
  internal long ClassifierID;
  /// <summary>Коллекция дочерних элементов</summary>
  internal List<SpecArticleType> Items = new List<SpecArticleType>();

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="objectType">Тип специфицируемого объекта</param>
  /// <param name="objTypeName">Название типа специфицируемого объекта</param>
  /// <param name="enabled">Разрешено ли выбирать указанный тип объекта</param>
  /// <param name="icon">Значок для указанного типа</param>
  public SpecArticleType(int objectType, string objTypeName, bool enabled, Icon icon)
  {
    this.ObjectType = objectType;
    this.ObjTypeName = objTypeName;
    this.Enabled = enabled;
    this.Icon = icon;
  }

  /// <summary>Сравнить с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj) => this.CompareTo(obj as SpecArticleType) == 0;

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => this.ObjectType.GetHashCode();

  /// <summary>Представить объект в виде строки</summary>
  /// <returns>Объект в виде строки</returns>
  public override string ToString()
  {
    return $"[{(this.Enabled ? (object) "x" : (object) " ")}] [{this.ObjectType}] {this.ObjTypeName}";
  }

  /// <summary>Сравнить с указанным экземпляром класса</summary>
  /// <param name="other">Объект для сравнения</param>
  /// <returns>-1, 0, 1</returns>
  public int CompareTo(SpecArticleType other)
  {
    return other == null ? 1 : this.ObjTypeName.ToUpperInvariant().CompareTo(other.ObjTypeName.ToUpperInvariant());
  }

  /// <summary>Найти элемент соответсвующий типу</summary>
  /// <param name="objectType">Искомый тип</param>
  /// <returns></returns>
  public SpecArticleType FindItem(int objectType)
  {
    SpecArticleType specArticleType = (SpecArticleType) null;
    if (this.ObjectType == objectType)
      specArticleType = this;
    for (int index = 0; specArticleType == null && index < this.Items.Count; ++index)
      specArticleType = this.Items[index].FindItem(objectType);
    return specArticleType;
  }
}
