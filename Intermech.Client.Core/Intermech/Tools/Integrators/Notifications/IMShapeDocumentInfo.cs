
// Type: Intermech.Tools.Integrators.Notifications.IMShapeDocumentInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Tools.Integrators.Notifications;

/// <summary>
/// Контейнер сведений о документе IPS для интеграции с IMShape.
/// </summary>
/// <remarks>Реализация является immutable и thread safe.</remarks>
/// <summary>Создает объект.</summary>
/// <param name="objectId">Идентификатор версии документа</param>
/// <param name="objectTypeId">Идентификатор типа документа</param>
/// <param name="filePath">Абсолютный путь к файлу документа</param>
public sealed class IMShapeDocumentInfo(long objectId, int objectTypeId, string filePath) : 
  FileDocumentInfo(objectId, objectTypeId, filePath)
{
}
