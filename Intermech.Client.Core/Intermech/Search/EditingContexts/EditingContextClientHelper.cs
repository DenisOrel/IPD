
// Type: Intermech.Search.EditingContexts.EditingContextClientHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Contexts;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Search.EditingContexts;

public static class EditingContextClientHelper
{
  public static DialogResult ShowSelectAddObjectsToEditingContextTypeDialog()
  {
    return MessageBox.Show($"Разворачивать состав рекурсивно на все уровни?{Environment.NewLine}Внимание! Операция может выполняться длительное время.", "Intermech Professional Solution", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
  }

  public static void ShowAddObjectsToEditingContextResultDialog(
    AddObjectsToEditingContextResult addObjectsToEditingContextResult)
  {
    if (addObjectsToEditingContextResult == null)
      throw new ArgumentNullException(nameof (addObjectsToEditingContextResult));
    if (addObjectsToEditingContextResult == null || addObjectsToEditingContextResult.EditingContextLogEnties.Count == 0 && addObjectsToEditingContextResult.AddedObjectsCount > 0)
    {
      int num = (int) MessageBox.Show("Добавление объектов в состав контекста редактирования успешно завершено", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      if (MessageBox.Show($"Добавление объектов в состав контекста редактирования завершено. Добавлено {addObjectsToEditingContextResult.AddedObjectsCount}, пропущено {addObjectsToEditingContextResult.SkippedObjectsCount} объектов.{Environment.NewLine}Показать журнал добавления?", "Intermech Professional Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
        return;
      EditingContextsLog log = new EditingContextsLog();
      log.AddRange((IEnumerable<EditingContextsLogEntry>) addObjectsToEditingContextResult.EditingContextLogEnties);
      EditingContextsEventLogForm.Execute(log);
    }
  }
}
