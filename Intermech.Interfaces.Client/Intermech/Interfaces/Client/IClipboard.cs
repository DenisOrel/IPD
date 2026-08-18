// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IClipboard
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс внутреннего буфера обмена клиентского приложения IPS
/// </summary>
public interface IClipboard
{
  /// <summary>Возвращает текущий выделенный объект</summary>
  /// <returns>Текущий выделенный объект</returns>
  object GetDataObject();

  /// <summary>Возвращает список объектов</summary>
  /// <returns>Список объектов</returns>
  object[] GetDataObjects();

  /// <summary>Возвращает список объектов заданного типа</summary>
  /// <param name="needType">Требуемый тип объектов</param>
  /// <returns>Список объектов требуемого типа</returns>
  object[] GetDataObjects(Type needType);

  /// <summary>Добавить информацию в буфер обмена</summary>
  /// <param name="clipboardObject">Объект, добавляемый в буфер обмена</param>
  void SetDataObject(object clipboardObject);

  /// <summary>Добавить информацию в буфер обмена</summary>
  /// <param name="clipboardObject">Объект, добавляемый в буфер обмена</param>
  /// <param name="title">Заголовок объекта</param>
  void SetDataObject(object clipboardObject, string title);

  /// <summary>Удалить из буфера обмена текущий объект</summary>
  void RemoveCurrentDataObject();

  /// <summary>Обновляет значок у выделенного объекта</summary>
  void RefreshImage();

  /// <summary>Сохраняет набор объектов во временном массиве</summary>
  void Push();

  /// <summary>
  /// Восстанавливает ранее сохраненный набор объектов из временного массива
  /// </summary>
  void Pop();

  /// <summary>
  /// Событие возникает при изменении содержимого буфера обмена
  /// </summary>
  event EventHandler Changed;

  /// <summary>
  /// Событие возникает при изменении текущего объекта в буфере обмена
  /// </summary>
  event EventHandler ContextChanged;

  /// <summary>Удаляет из буфера обмена объекты указанного типа</summary>
  /// <param name="type">Тип данных для удаления</param>
  void RemoveDataObjects(Type type);
}
