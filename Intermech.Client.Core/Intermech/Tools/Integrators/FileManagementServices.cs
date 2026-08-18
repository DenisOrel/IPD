
// Type: Intermech.Tools.Integrators.FileManagementServices
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Files;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

public static class FileManagementServices
{
  private static readonly ApplicationServiceRef<IFileAttributeEditorService> fileAttributeEditorService = new ApplicationServiceRef<IFileAttributeEditorService>();

  /// <summary>
  /// Проверяет, имеется ли у объектов указанного типа атрибут "Файл".
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>true, если атрибут "Файл" имеется или может быть, false - если такого атрибута нет</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  [Obsolete("Use the method IFileProcessingOptionsService.HasFileAttribute instead of this.", true)]
  public static bool HasFiles(int objectType)
  {
    return FileManagementServices.fileAttributeEditorService.Value.HasFileAttribute(objectType);
  }

  /// <summary>
  /// Проверяет, следует ли обрабатывать объекты указанного типа по общим правилам работы с атрибутом "Файл".
  /// </summary>
  /// <param name="objectType">Идентификатор типа объекта</param>
  /// <returns>true, если объекты указанного типа обрабатываются по общим правилам, false - если требуется специальная обработка</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа объекта</exception>
  [Obsolete("Use the method IFileProcessingOptionsService.GetFileAttributeEditMode instead of this.", true)]
  public static bool RequiresCommonFileRules(int objectType)
  {
    FileAttributeEditMode? attributeEditMode = FileManagementServices.fileAttributeEditorService.Value.GetFileAttributeEditMode(objectType);
    return !attributeEditMode.HasValue || attributeEditMode.Value == FileAttributeEditMode.Normal;
  }

  /// <summary>
  /// Не все типы объектов, имеющие атрибут "Файл", следует обрабатывать по общим правилам. Как правило, для них требуется
  /// специальная обработка (например, как в случае с AVS). Этот метод и возвращает список таких типов объектов.
  /// </summary>
  /// <returns>Список идентификаторов типов объектов с атрибутом "Файл", обрабатываемых не по общим правилам</returns>
  [Obsolete("Use the method IFileProcessingOptionsService.GetObjectTypesWithInternalEditMode instead of this.", true)]
  public static List<int> GetObjectTypesWithSpecialFileRules()
  {
    return new List<int>((IEnumerable<int>) FileManagementServices.fileAttributeEditorService.Value.GetObjectTypesWithInternalEditMode());
  }
}
