// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IFileAttributeEditorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Сервис для определения способа редактирования атрибута "Файл" у объектов IPS.
/// </summary>
/// <remarks>
/// У большинства объектов IPS для редактирования атрибута "Файл" требуется извлечь файлы на локальный диск,
/// передать их внешнему приложению, а затем сохранить изменения в базу данных IPS. Но есть небольшая группа
/// документов, которая редактируется встроенными в IPS средствами без извлечения файлов на диск.
/// Данный сервис позволяет определить требуемый режим редактирования атрибута "Файл" для указанного типа
/// объектов IPS.
/// </remarks>
public interface IFileAttributeEditorService
{
  /// <summary>
  /// Проверяет, имеется ли у указанного типа объектов IPS атрибут "Файл".
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <returns>true, если атрибут "Файл" имеется или может быть, false - если такого атрибута нет</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объектов IPS</exception>
  bool HasFileAttribute(int objectTypeId);

  /// <summary>
  /// Возвращает для указанного типа объектов IPS способ редактирования атрибута "Файл".
  /// </summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <returns>Способ редактирования атрибута "Файл" у объектов указанного типа</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объектов IPS</exception>
  FileAttributeEditMode? GetFileAttributeEditMode(int objectTypeId);

  /// <summary>
  /// Возвращает коллекцию идентификаторов типов объектов IPS, у которых атрибут "Файл" должен редактироваться в оперативной памяти
  /// без извлечения на диск в рабочую область файлового хранилища пользователя.
  /// </summary>
  /// <returns>Коллекция идентификаторов типов объектов IPS</returns>
  ICollection<int> GetObjectTypesWithInternalEditMode();
}
