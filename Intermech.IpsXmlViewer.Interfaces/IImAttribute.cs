// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.IImAttribute
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>Атрибут объекта/связи</summary>
public interface IImAttribute : 
  IImCompositeAttrElement,
  IImCompositeElement,
  IImBaseElement,
  IAssignable,
  ICloneable,
  IDisplayable
{
  /// <summary>Атрибут принадлежит объекту</summary>
  bool IsObjectAttribute { get; set; }

  /// <summary>
  /// Идентификатор версии объекта/связи - владельца атрибута
  /// </summary>
  string OwnerID { get; set; }

  /// <summary>
  /// Имя таблицы SQL, в которой хранится содержимое элемента
  /// </summary>
  string SQLTableName { get; }

  /// <summary>
  /// В свойстве хранится количество элементов, если атрибут является многозначным
  /// (значение меньше 2 - однозначный атрибут)
  /// </summary>
  int MultiValuesCount { get; set; }

  /// <summary>Возвращается минимальное значение F_INLIST_ID*</summary>
  int F_INLIST_ID { get; }

  /// <summary>Возвращается значение F_ATTRIBUTE_ID</summary>
  string F_ATTRIBUTE_ID { get; }

  /// <summary>
  /// Возвращается имя атрибута для хранения в словарике у объекта/связи
  /// </summary>
  string DictAttrKey { get; }

  /// <summary>
  /// Извлечь из словарика атрибутов все значения (многозначные, однозначные) и
  /// сформировать словарик, ключом которого являются значения F_INLIST_ID,
  /// а значениями - значения атрибута
  /// </summary>
  /// <returns>Словарик, ключом которого являются значения F_INLIST_ID,
  /// а значениями - значения атрибута</returns>
  IDictionary<int, IDictionary<string, object>> DeNormalize();

  /// <summary>
  /// Метод осуществляет переименование имён значений атрибута
  /// в соответствии со значением F_INLIST_ID
  /// </summary>
  void Normalize();

  /// <summary>
  /// Метод изучает значения атрибутов и возвращает список всех F_INLIST_ID.*
  /// </summary>
  /// <returns></returns>
  List<int> GetInListIDs();

  /// <summary>
  /// Проверить, можно ли объединиться с указанным атрибутом
  /// (касается многозначных атрибутов)
  /// </summary>
  /// <param name="attribute">Проверяемый атрибут</param>
  /// <returns>true - объединение возможно</returns>
  bool CanMergeWith(IImAttribute attribute);

  /// <summary>
  /// Выполнить объединение значений с указанным атрибутом.
  /// ВНИМАНИЕ!!! АТРИБУТЫ ДОЛЖНЫ БЫТЬ НОРМАЛИЗОВАНЫ!!!
  /// </summary>
  /// <param name="attribute">Атрибут, со значениями которого требуется выполнить объединение</param>
  void MergeWith(IImAttribute attribute);
}
