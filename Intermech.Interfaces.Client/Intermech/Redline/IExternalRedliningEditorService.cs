// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.IExternalRedliningEditorService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Redline;

/// <summary>
/// Интерфейс сервиса для взаимодействия в внешним редактором замечаний "ИНТЕРМЕХ".
/// </summary>
public interface IExternalRedliningEditorService
{
  /// <summary>
  /// Сообщает об открытии файла объекта во внешнем приложении. Этот метод предназначен для ведения истории открытия файлов документов,
  /// которая используется редакторов замечаний "ИНТЕРМЕХ".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, чей файл был открыт во внешнем приложении</param>
  /// <param name="isViewAction">Признак, что файл объекта был открыт командой 'Смотреть' или ее аналогом</param>
  void ReportFileOpenAction(long objectId, bool isViewAction);

  /// <summary>
  /// Возвращает список объектов, для которых был выполнен запуск приложений для редактирования или просмотра в текущем сеансе работы.
  /// Следует учитывать, что версии объектов из этого списка перед использованием следует проверять на существование, так как они могут
  /// уже отсутствовать в базе данных.
  /// </summary>
  /// <returns>Список идентификаторов версий объектов</returns>
  List<long> GetFileOpenHistory();

  /// <summary>
  /// Очищает список объектов, для которых был выполнен запуск приложений для редактирования или просмотра в текущем сеансе работы.
  /// </summary>
  void ClearFileOpenHistory();

  /// <summary>
  /// Запускает приложение для снятия скриншотов, если оно установлено.
  /// </summary>
  /// <returns>true - приложение было запущено; false - приложение не было запущено, так как оно не установлено</returns>
  bool LaunchScreenShooter();
}
