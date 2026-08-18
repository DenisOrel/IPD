// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.IImDocumentManager
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Интерфейс для стандартного взаимодействия с объектом управляющим окнами документов</summary>
public interface IImDocumentManager
{
  /// <summary>Менеджер команд</summary>
  ICommandManager CommandManager { get; }

  /// <summary>Включен режим выбора элементов. Если имеет значение true,
  /// то IsElementCreating не может иметь значение true</summary>
  bool IsElementSelecting { get; set; }

  /// <summary>Включен режим создания элементов. Если имеет значение true,
  /// то IsElementSelecting не может иметь значение true</summary>
  bool IsElementCreating { get; set; }

  /// <summary>Объект управляющий созданием элемента</summary>
  PageElementCreator SelectedElementCreator { get; set; }

  /// <summary>Обновить отображаемую информацию о выбранном элементе</summary>
  void UpdateSelectedElementInfo();

  /// <summary>Среагировать на изменение выделения</summary>
  void SelectionChanged();

  /// <summary>Установить сообщение в статусной строке</summary>
  /// <param name="text">Текст сообщения</param>
  void SetMessageText(string text);

  /// <summary>Обновить информацию о количестве страниц и текущей странице</summary>
  void UpdatePagesInfo();

  /// <summary>Диалог сохранения документа в файл</summary>
  SaveFileDialog SaveToFileDialog { get; }

  /// <summary>Последнее путь использовавшийся при сохранении как</summary>
  string RecentlySaveAsPath { get; set; }

  /// <summary>Обновить меню и инструменты форматирования</summary>
  void UpdateFormatCommands();

  /// <summary>Отобразить информацию о возникшей исключительной ситуации (Exception)</summary>
  /// <param name="e">Возникшее исключение</param>
  /// <returns>Тип нажатой в окне кнопки</returns>
  void ShowExceptionDialog(Exception e);
}
