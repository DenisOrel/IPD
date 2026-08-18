// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.ICadProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoCAD.Proxies;

public interface ICadProxy
{
  /// <summary>Проверяет валидность подключения к CAD-системе.</summary>
  /// <exception cref="T:System.Exception">Подключение к CAD-системе нарушено и должно быть переустановлено</exception>
  void KnockKnock();

  /// <summary>
  /// Проверяет готовность CAD-системы к взаимодействию через COM API.
  /// </summary>
  /// <returns>true, если CAD-система готова к взаимодействию через COM API</returns>
  bool IsReady();

  /// <summary>Создает новый документ.</summary>
  /// <returns>Объект документа</returns>
  ICadDocumentProxy CreateDocument();

  ICadDocumentProxy OpenDocument(string fullName);

  ICadDocumentProxy FindOpenDocument(string fullName);

  /// <summary>Возвращает список документов, открытых в CAD-системе.</summary>
  /// <param name="includeNew">Флаг, позволяющий включить в список еще не сохраненные на диск документы</param>
  /// <returns>Список документов, открытых в CAD-системе</returns>
  List<ICadDocumentProxy> GetOpenDocuments(bool includeNew = true);

  /// <summary>
  /// Возвращает активный документ CAD-системы, если таковой имеется.
  /// Активного документа может не быть, если в CAD-системе нет открытых окон документов.
  /// </summary>
  /// <returns>Объект документа или null</returns>
  ICadDocumentProxy TryGetActiveDocument();

  object SaveVisualState(CadVisualStateFlags flags);

  void RestoreVisualState(object state);

  void ShowWindow();

  /// <summary>Выполняет переключение на приложение AutoCAD.</summary>
  /// <returns>HWND активного окна до переключения</returns>
  IntPtr SwitchToApp();

  /// <summary>
  /// Возвращает исходный необернутый COM-объект CAD-системы.
  /// Это свойство должно использоваться только в тех случаях, когда
  /// COM-объект требуется передать в другое приложение.
  /// Внутри IPS должен использоваться только прокси-объект.
  /// </summary>
  object RawObject { get; }

  /// <summary>
  /// Возвращает имя приложения, которое можно использовать в сообщениях и диалоговых окнах.
  /// </summary>
  string ApplicationName { get; }

  /// <summary>Возвращает или задает имя активного профиля AutoCAD.</summary>
  string ActiveProfile { get; set; }

  string WorkspacePath { get; set; }

  /// <summary>
  /// Возвращает признак, что загрузка внешних ссылок на другие DWG-файлы является "блокирующией".
  /// Если значение свойства равно true, то CAD-система не дает редактировать DWG-файлы после
  /// их косвенного открытия в качестве external reference (xref) из другого документа.
  /// </summary>
  bool XRefLoadingIsBlocking { get; }
}
