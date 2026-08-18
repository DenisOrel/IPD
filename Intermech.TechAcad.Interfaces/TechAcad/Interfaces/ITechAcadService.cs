// Decompiled with JetBrains decompiler
// Type: Intermech.TechAcad.Interfaces.ITechAcadService
// Assembly: Intermech.TechAcad.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 512FF008-192B-42A6-A8D1-B0B0A687059D
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.TechAcad.Interfaces.xml

#nullable disable
namespace Intermech.TechAcad.Interfaces;

/// <summary>Интерфейс для работы с редактором эскизов</summary>
public interface ITechAcadService
{
  /// <summary>
  /// Возвращает абсолютный путь к рабочей папке в файловом хранилище, где размещаются файлы эскизов Cadmech-T.
  /// </summary>
  /// <returns></returns>
  string GetWorkingDirPath();

  /// <summary>Загрузка редактора</summary>
  /// <returns></returns>
  bool LoadAcad(TechAcadLoadMode loadMode);

  /// <summary>Выгрузка редактора</summary>
  /// <param name="askForUnload">Подтверждение выгрузки</param>
  /// <returns></returns>
  bool UnloadAcad(bool askForUnload);

  /// <summary>Создание нового объекта эскиза</summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool CreatePicture(long objId);

  /// <summary>ЗагрузкаВыгрузка эскиза в файловое хранилище</summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool OpenPicture(long objId);

  /// <summary>Сохранение/запись файла в объект эскиза</summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool SaveOnlyPicture(long objId);

  /// <summary>
  /// Сохранение/запись файла в объект эскиза и последующее удаление файла из хранилища
  /// </summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool SaveAndUnloadPicture(long objId);

  /// <summary>
  /// Закрытие эскиза в редакторе и удаление файла из хранилища
  /// </summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool ClosePicture(long objId);

  /// <summary>Загрузка файла из хранилища</summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  bool UnloadPicture(long objId);

  /// <summary>Загрузка файла из хранилища</summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  bool UnloadPicture(string fileName);

  /// <summary>Получение текста с эскиза</summary>
  /// <returns></returns>
  string GetAcadText();

  /// <summary>Дополнительная информация с эскиза...</summary>
  /// <param name="objId"></param>
  /// <returns></returns>
  string GetTechDop(long objId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  void SetInterfaceObject(object obj);

  /// <summary>
  /// Проверяет публикацию объекта эскиза в рабочей области файлового хранилища и
  /// возвращает абсолютный путь к файлу эскиза. Если эскиз не был опубликован,
  /// либо файл на диске отсутствует, то метод вернет null.
  /// </summary>
  /// <param name="draftId">Идентификатор версии объекта эскиза</param>
  /// <returns>Абсолютный путь к файлу эскиза на диске</returns>
  string GetPictureLocalPath(long draftId);

  /// <summary>
  /// Публикует объект эскиза в рабочей области и извлекает его файл на диск.
  /// </summary>
  /// <param name="objId">Идентификатор версии объекта эскиза</param>
  /// <returns>Абсолютный путь к файлу эскиза</returns>
  string ExtractPicture(long objId);

  /// <summary>
  /// Находит объект эскиза по абсолютному пути к файлу эскиза.
  /// </summary>
  /// <param name="draftLocalPath">Абсолютный путь к файлу эскиза</param>
  /// <returns>Идентификатор версии объекта эскиза или Intermech.Consts.UnknownObjectId</returns>
  long GetPictureObject(string draftLocalPath);

  /// <summary>
  /// Проверяет возможность внесения изменений в файл эскиза, находящийся в рабочей области файлового хранилища. Если
  /// объект эскиза не был опубликован, то метод вернет false. При этом метод не проверяет существования файла на диске.
  /// </summary>
  /// <param name="objId">Идентификатор версии объекта эскиза</param>
  /// <returns>Признак возможности внесения изменений</returns>
  bool IsPictureEditable(long objId);

  /// <summary>Показать окно редактора</summary>
  void ShowAcadWindow(WindowMode mode);
}
