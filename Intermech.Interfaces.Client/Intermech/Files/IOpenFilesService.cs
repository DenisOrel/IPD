// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IOpenFilesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Сервис открытых файлов. Позволяет определить, какие файлы объектов IPS открыты в различных
/// приложениях, какие из этих файлов имеют несохраненные изменения, а также позволяет
/// закрыть и переоткрыть определенные файлы объектов. Все методы сервиса являются thread-safe.
/// </summary>
public interface IOpenFilesService : IOpenFiles
{
  /// <summary>Регистрирует расширение сервиса.</summary>
  /// <param name="extension">Объект расширения</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект расширения не может быть null</exception>
  /// <exception cref="T:System.ArgumentException">Повторная регистрация расширения недопустима</exception>
  void RegisterExtension(IOpenFilesServiceExtension @extension);

  /// <summary>Отменяет регистрацию расширения сервиса.</summary>
  /// <param name="extension">Объект расширения</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект расширения не может быть null</exception>
  void UnregisterExtension(IOpenFilesServiceExtension @extension);
}
