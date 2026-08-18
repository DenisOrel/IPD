// Decompiled with JetBrains decompiler
// Type: Intermech.Redline.IRedliningAPI
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Redline;

/// <summary>
/// Интерфейс COM-объекта, используемого для интеграции с редактором замечаний ИНТЕРМЕХ.
/// </summary>
[ComVisible(true)]
[Guid("2C1F7287-D372-4975-A0B9-5539E774DF6D")]
[InterfaceType(ComInterfaceType.InterfaceIsDual)]
public interface IRedliningAPI
{
  /// <summary>
  /// Возвращает или задает расширение для файла замечаний. По умолчанию содержит .rxml.
  /// </summary>
  string FileExtension { get; set; }

  /// <summary>
  /// Позволяет проверить, завершена ли загрузка приложения.
  /// </summary>
  /// <returns>Возвращает true, если пользователь аутентифицировался, и загрузка приложения завершена, иначе - false</returns>
  bool IsReady { get; }

  /// <summary>
  /// Возвращает идентификатор пользователя, вошедшего в IPS.
  /// </summary>
  long CurrentUserID { get; }

  /// <summary>
  /// Возвращает глобальный идентификатор пользователя, вошедшего в IPS.
  /// </summary>
  string CurrentUserGuid { get; }

  /// <summary>Позволяет дождаться полной загрузки приложения.</summary>
  /// <param name="timeout">Таймаут ожидания в миллисекундах. Значение меньшее или равное 0 может быть использовано для задания бесконечного таймаута</param>
  /// <returns>Возвращает true, если пользователь аутентифицировался, и загрузка приложения завершена. Возвращает false, если приложение не завершило загрузку за указанное время.</returns>
  bool WaitReady(int timeout);

  /// <summary>
  /// Возвращает полный список граф для всех должностей, которыми обладает текущий пользователь.
  /// Список разделён переводами строк.
  /// </summary>
  /// <returns>Список граф для подписи, разделенный переводами строк</returns>
  string GetCurrentUserRanks();

  /// <summary>
  /// Возвращает идентификатор пользователя по его глобальному идентификатору. Если пользователь с указанным глобальным идентификатором не существует в базе IPS,
  /// то метод вернет Intermech.Consts.UnknownObjectId.
  /// </summary>
  /// <param name="userGuid">Глобальный идентификатор пользователя</param>
  /// <returns>Идентификатор пользователя или Intermech.Consts.UnknownObjectId</returns>
  /// <exception cref="T:System.ArgumentException">Глобальный идентификатор пользователя не может быть пустым</exception>
  /// <exception cref="T:System.FormatException">Указанное значение не является глобальным идентификатором</exception>
  long GetUserIDFromGuid(string userGuid);

  /// <summary>Возвращает выводимое имя указанного пользователя.</summary>
  /// <param name="userId">Идентификатор пользователя</param>
  /// <returns>Выводимое имя пользователя</returns>
  /// <exception cref="T:System.ArgumentException">Идентификатор пользователя не может быть пустым</exception>
  /// <exception cref="T:System.Exception">Не удалось найти пользователя с указанным идентификатором</exception>
  string GetUserFullName(long userId);

  /// <summary>
  /// Показывает диалог выбора документа из тех документов, для которых были вызваны команды просмотра, редактирования или печати в этом сеансе работы приложения.
  /// </summary>
  /// <returns>Идентификатор выбранного документа, или 0, если пользователь отказался выбирать документ, или -1, если нет доступных для выбора документов</returns>
  long SelectLastViewedDocument();

  /// <summary>
  /// Показывает диалог выбора любого документа PDM-системы.
  /// </summary>
  /// <returns>Идентификатор выбранного документа или 0, если пользователь отказался выбирать документ</returns>
  long SelectDocument();

  /// <summary>
  /// Возвращает идентификатор документа, которому принадлежит указанный файл. Если файл никому не принадлежит, то метод вернет 0.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Идентификатор документа или 0</returns>
  /// <exception cref="T:System.ArgumentException">Путь к файлу не задан, либо задан не в абсолютной форме</exception>
  /// <exception cref="T:System.InvalidOperationException">Указанный файл находится вне рабочей области файлового хранилища пользователя</exception>
  long FindDocumentByFilePath(string filePath);

  /// <summary>
  /// Делает указанную версию документа текущей. Это значит, что остальные функции данного интерфейса работают с этим текущим документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  void OpenDocument(long documentId);

  /// <summary>
  /// Возвращает значение для указанного атрибута текущего документа.
  /// </summary>
  /// <param name="attributeName">Имя атрибута документа</param>
  /// <returns>Значение атрибута или null, если такого атрибута у документа нет</returns>
  /// <exception cref="T:System.ArgumentException">Имя атрибута документа не задано</exception>
  object GetDocumentAttribute(string attributeName);

  /// <summary>
  /// Возвращает абсолютный путь к мастер-файлу текущего документа.
  /// </summary>
  /// <returns>Абсолютный путь к мастер-файлу текущего документа</returns>
  string GetDocumentFilePath();

  /// <summary>
  /// Возвращает список граф, в которых текущий пользователь может подписать текущий документ. Список разделён переводами строк.
  /// </summary>
  /// <returns>Список граф для подписи, разделенный переводами строк</returns>
  string GetDocumentRanks();

  /// <summary>
  /// Показывает диалог выбора графы подписи, в которой будет создано замечание.
  /// </summary>
  /// <returns>Имя графы или пустая строка, если пользователь отказался от выбора, либо нет доступных для пользователя граф</returns>
  string SelectDocumentRank();

  /// <summary>
  /// Возвращает информацию об участии текущего документа в документообороте.
  /// </summary>
  /// <param name="processName">Имя процесса документооборота</param>
  /// <param name="activityName">Имя шага процесса документооборота</param>
  /// <returns>true, если указанный документ учавствует в данный момент в документообороте</returns>
  bool GetDocumentWorkflowInfo(out string processName, out string activityName);

  /// <summary>Вызывает команду просмотра для текущего документа.</summary>
  /// <param name="copyToDiskOnly">Признак, что нужно только извлечь файлы документа на диск, а приложение запускать не требуется</param>
  /// <returns>Абсолютный путь к мастер-файлу извлеченного текущего документа. Может быть пустым, если команда просмотра не извлекает файлы на диск, а открывает их непосредственно из базы IPS</returns>
  string ViewDocument(bool copyToDiskOnly);

  /// <summary>
  /// Добавляет или заменяет файл замечаний для текущего документа.
  /// </summary>
  /// <param name="redliningFilePath">Абсолютный путь к файлу замечаний</param>
  /// <exception cref="T:System.ArgumentException">Путь к файлу не задан, либо задан не в абсолютной форме</exception>
  /// <exception cref="T:System.IO.FileNotFoundException">Указанный файл не найден на диске</exception>
  void UpdateRedliningFile(string redliningFilePath);

  /// <summary>
  /// Копирует файл замечаний текущего документа в указанную папку.
  /// </summary>
  /// <param name="dirPath">Путь к папке, куда следует скопировать файл замечаний</param>
  /// <returns>Абсолютный путь к извлеченному файлу замечаний или null, если у документа нет файла замечаний</returns>
  /// <exception cref="T:System.ArgumentException">Путь к папке не задан, либо задан не в абсолютной форме</exception>
  string GetRedliningFile(string dirPath);
}
