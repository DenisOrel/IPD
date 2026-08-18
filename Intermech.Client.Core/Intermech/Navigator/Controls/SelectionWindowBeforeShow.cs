
// Type: Intermech.Navigator.Controls.SelectionWindowBeforeShow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

/// <summary>
/// Событие генерируется перед отображением окна по выбору объектов
/// </summary>
/// <param name="sender">Отправитель (SelectionWindow)</param>
/// <param name="e">Аргументы события</param>
public delegate void SelectionWindowBeforeShow(object sender, EventArgs e);
