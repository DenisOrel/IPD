// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.ArticleWithDocForm.IPageControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs.ArticleWithDocForm;

/// <summary>Закладка</summary>
internal interface IPageControl
{
  /// <summary>Изменились атрибуты объекта/связи закладки</summary>
  event EventHandler Changed;

  /// <summary>Событие об том, что необходимо перечитать контролы</summary>
  event EventHandler ReloadData;

  /// <summary>Произведена классификация</summary>
  event ClassificatedEventHandler ClassificatedEvent;

  /// <summary>Получена команда отображения формы редактора атрибута</summary>
  event GetEditorDelegate GetEditorEvent;

  /// <summary>Интерфейс на общие данные</summary>
  IFormCommonData CommonData { set; }

  /// <summary>Обновление данных в закладке</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="mode">Режим открытия закладки</param>
  void Reload(IUserSession session, OpenModes mode);

  /// <summary>Изменились общие данные</summary>
  /// <param name="type">Тип измененных общих данных</param>
  void CommonDataChanged(CommonDataType type);

  /// <summary>Сохранение данных в закладке</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="mode">Режим открытия закладки</param>
  /// <param name="pair">Ссылка на созданную пару объектов (для самостоятельного заполнения закладками)</param>
  void Save(IUserSession session, OpenModes mode, CreatedPair pair);

  /// <summary>Устанорвка контрола на котором будет лежать закладка</summary>
  /// <param name="parent"></param>
  void SetParent(Control parent);

  /// <summary>
  /// Флаг устанавливающий автоматическую нотификацию изменений в контродах
  /// </summary>
  bool AutoNotifications { set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="oc"></param>
  /// <returns></returns>
  void OnSetClassifyAttributes(IObjectClassificator oc, long clasifID);
}
