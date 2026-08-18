// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IApplicationFileTypes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора, позволяющий определять файлы, созданные в приложении, с который осуществляется интеграция.
/// </summary>
public interface IApplicationFileTypes : IIntegratorService
{
  /// <summary>
  /// Позволяет определить по имени файла, является ли он документом приложения.
  /// </summary>
  /// <remarks>
  /// Как правило, другие сервисы интегратора вызывают этот метод перед открытием документа из базы IPS в приложении, чтобы проверить,
  /// является ли мастер-файл документом приложения. Такая проверка нужна из-за того, что пользователь вручную может изменить список
  /// файлов любого документа в базе IPS.
  /// </remarks>
  /// <param name="fileName">Имя и путь к файлу, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  bool IsApplicationFile(string fileName);

  /// <summary>
  /// Позволяет определить по имени и содержимому файла, является ли он документом приложения. Сначала выполняется проверка по имени, и, если она
  /// успешно пройдена, то выполняется проверка содержимого файла.
  /// </summary>
  /// <remarks>
  /// Как правило, этот метод используется тогда, когда нужно найти интегратор, ответственный за обработку еще не зарегистрированного в IPS файла.
  /// Использовать расширение файла нельзя, так как файлы разных приложений могут использовать одинаковые расширения.
  /// </remarks>
  /// <param name="fileInfo">Сведения о файле</param>
  /// <param name="fileContent">Поток с содержимым файла, указатель положения установлен в начало потока</param>
  /// <returns>true, если это файл приложения, с которым осуществляется интеграция</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на аргумент метода не может быть null</exception>
  bool IsApplicationFile(FileInfo fileInfo, Stream fileContent);
}
