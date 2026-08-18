// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISelectionDialogTab
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Дополнительная закладка для формы выборки</summary>
public interface ISelectionDialogTab
{
  /// <summary>Заголовок</summary>
  string Caption { get; }

  /// <summary>
  /// Индекс в списке закладок
  /// 0 - Табличные отчеты
  /// 1 - Скрипты генерации документов
  /// </summary>
  int Index { get; }

  /// <summary>Контрол для закладки</summary>
  Control TabControl { get; }

  /// <summary>Загрузка данных в контрол в закладке</summary>
  void Initialize(IUserSession session, long selectionID, bool isPersonal);

  /// <summary>Сохранение результатов</summary>
  /// <param name="session"></param>
  /// <param name="selectionID"></param>
  void Save(IUserSession session, long selectionID);

  /// <summary>Событие, генерируется закладкой при изменениях</summary>
  event EventHandler OnChanged;
}
