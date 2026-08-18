
// Type: Intermech.Redline.ExternalRedliningEditorService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;


namespace Intermech.Redline;

/// <summary>
/// Реализует сервис для взаимодействия в внешним редактором замечаний "ИНТЕРМЕХ".
/// </summary>
internal sealed class ExternalRedliningEditorService : IExternalRedliningEditorService
{
  private readonly OrderedList<long> fileOpenHistory;

  /// <summary>Создает объект.</summary>
  public ExternalRedliningEditorService()
  {
    this.fileOpenHistory = new OrderedList<long>(128 /*0x80*/);
  }

  /// <summary>
  /// Сообщает об открытии файла объекта во внешнем приложении. Этот метод предназначен для ведения истории открытия файлов документов,
  /// которая используется редакторов замечаний "ИНТЕРМЕХ".
  /// </summary>
  /// <param name="objectId">Идентификатор версии объекта, чей файл был открыт во внешнем приложении</param>
  /// <param name="isViewAction">Признак, что файл объекта был открыт командой 'Смотреть' или ее аналогом</param>
  public void ReportFileOpenAction(long objectId, bool isViewAction)
  {
    if (objectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (objectId));
    this.fileOpenHistory.Add(objectId);
    if (!isViewAction || !(bool) (ValueCell<bool>) RedliningSettings.CommonSettings.LaunchScreenShooter)
      return;
    this.LaunchScreenShooter();
  }

  /// <summary>
  /// Запускает приложение для снятия скриншотов, если оно установлено.
  /// </summary>
  /// <returns>true - приложение было запущено; false - приложение не было запущено, так как оно не установлено</returns>
  public bool LaunchScreenShooter()
  {
    string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scrshooter.exe");
    if (!File.Exists(str))
      return false;
    Process.Start(str)?.Dispose();
    return true;
  }

  /// <summary>
  /// Возвращает список объектов, для которых был выполнен запуск приложений для редактирования или просмотра в текущем сеансе работы.
  /// Следует учитывать, что версии объектов из этого списка перед использованием следует проверять на существование, так как они могут
  /// уже отсутствовать в базе данных.
  /// </summary>
  /// <returns>Список идентификаторов версий объектов</returns>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public List<long> GetFileOpenHistory()
  {
    return new List<long>((IEnumerable<long>) this.fileOpenHistory);
  }

  /// <summary>
  /// Очищает список объектов, для которых был выполнен запуск приложений для редактирования или просмотра в текущем сеансе работы.
  /// </summary>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public void ClearFileOpenHistory() => this.fileOpenHistory.Clear();
}
