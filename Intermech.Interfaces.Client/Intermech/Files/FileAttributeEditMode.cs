// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileAttributeEditMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>Способ редактирования атрибута "Файл" у объекта IPS</summary>
public enum FileAttributeEditMode
{
  /// <summary>
  /// Для редактирования атрибута "Файл" используется внешнее приложение.
  /// Это обычный режим, используемый для большинства объектов IPS.
  /// Содержимое атрибута "Файл" должно быть извлечено в рабочую область файлового хранилища пользователя,
  /// а позже изменения в файлах должны быть сохранены в базу данных
  /// </summary>
  Normal,
  /// <summary>
  /// Для редактирования атрибута "Файл" используется внутренний редактор, являющийхся частью IPS.
  /// Это специальный режим, используемый лишь для некоторых объектов IPS.
  /// Содержимое атрибута "Файл" не будет извлекаться в рабочую область файлового хранилища пользователя,
  /// все изменения должны быть сделаны в оперативной памяти.
  /// </summary>
  Internal,
}
