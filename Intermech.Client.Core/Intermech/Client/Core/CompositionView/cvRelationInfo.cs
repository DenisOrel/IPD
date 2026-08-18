
// Type: Intermech.Client.Core.CompositionView.cvRelationInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Client.Core.CompositionView;

/// <summary>Структура для хранения данных о связи</summary>
/// <summary>Конструктор</summary>
/// <param name="relationTypeID">идентификатор типа связи</param>
/// <param name="hasSortAttribute">имеет ли связь атрибут сортировка</param>
public struct cvRelationInfo(int relationTypeID, bool hasSortAttribute)
{
  /// <summary>Идентификатор типа связи</summary>
  public int RelationTypeID = relationTypeID;
  /// <summary>Имеет ли связь атрибут сортировка</summary>
  public bool HasSortAttribute = hasSortAttribute;

  /// <summary>Пустое значение</summary>
  public static cvRelationInfo Empty => new cvRelationInfo(-1, false);

  /// <summary>Базовый</summary>
  /// <returns></returns>
  public override int GetHashCode() => base.GetHashCode();

  /// <summary>Сравнение</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case cvRelationInfo cvRelationInfo:
        return this.RelationTypeID.Equals(cvRelationInfo.RelationTypeID);
      case int _:
        return this.RelationTypeID.Equals(Convert.ToInt32(obj));
      default:
        return base.Equals(obj);
    }
  }
}
