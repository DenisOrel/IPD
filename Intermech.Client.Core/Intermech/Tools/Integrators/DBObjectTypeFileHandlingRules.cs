
// Type: Intermech.Tools.Integrators.DBObjectTypeFileHandlingRules
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Files;
using System;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Содержит сведения о типе объектов IPS и правилах работы с атрибутом "Файл" для этого типа.
/// </summary>
[Serializable]
public sealed class DBObjectTypeFileHandlingRules
{
  /// <summary>Создает объект.</summary>
  /// <param name="objectTypeId">Идентификатор типа объектов IPS</param>
  /// <param name="integratorRef">Именованная ссылка на интегратор с приложением. Значение может быть null, если интегратора для этого типа объектов нет</param>
  /// <param name="fileAttributeEditMode">Режим использования атрибута "Файл". Значение может бы null, если такого атрибута нет</param>
  public DBObjectTypeFileHandlingRules(
    int objectTypeId,
    IntegratorObject integratorRef,
    FileAttributeEditMode? fileAttributeEditMode)
  {
    this.ObjectTypeId = objectTypeId != -1 ? objectTypeId : throw new ArgumentException("Не задан идентификатор типа объектов IPS.", nameof (objectTypeId));
    this.IntegratorRef = integratorRef;
    this.FileEditMode = fileAttributeEditMode;
  }

  /// <summary>Возвращает идентификатор типа объектов IPS.</summary>
  public int ObjectTypeId { get; }

  /// <summary>
  /// Возвращает именованную ссылку на интегратор с приложением.
  /// Значение свойства может быть равно null, интегратора для этого типа объектов нет.
  /// </summary>
  public IntegratorObject IntegratorRef { get; }

  /// <summary>
  /// Возвращает режим использования атрибута "Файл" у объектов IPS,
  /// обрабатываемых этим интегратором. Значение свойства может быть равно null,
  /// если атрибута "Файл" нет.
  /// </summary>
  public FileAttributeEditMode? FileEditMode { get; }

  /// <summary>
  /// Возвращает признак, что атрибут "Файл" следует обрабатывать по общим правилам - через
  /// извлечение его содержимого в рабочую область файлового хранилища пользователя.
  /// </summary>
  public bool RequireNormalEditMode
  {
    get
    {
      return (!this.FileEditMode.HasValue ? 0 : (this.FileEditMode.Value == FileAttributeEditMode.Internal ? 1 : 0)) == 0;
    }
  }
}
