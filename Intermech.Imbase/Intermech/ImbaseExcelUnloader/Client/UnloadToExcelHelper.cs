// Decompiled with JetBrains decompiler
// Type: Intermech.ImbaseExcelUnloader.Client.UnloadToExcelHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ImbaseExcelUnloader.Client;

internal static class UnloadToExcelHelper
{
  internal static void Unload(
    ISelectedItems items,
    System.IServiceProvider viewServices,
    object additionalInfo)
  {
    if (items == null || items.Count == 0)
      return;
    HashSet<int> objTypes = new HashSet<int>();
    List<string> values = new List<string>();
    HashSet<IDBTypedObjectID> dbTypedObjectIdSet = new HashSet<IDBTypedObjectID>();
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = items.GetItemData(index, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
      dbTypedObjectIdSet.Add(itemData);
      objTypes.Add(itemData.ObjectType);
      values.Add(itemData.Caption);
    }
    using (OptionsForm optionsForm = new OptionsForm(objTypes, dbTypedObjectIdSet))
    {
      if (optionsForm.ShowDialog() != DialogResult.OK)
        return;
      List<Guid> guidLst = optionsForm._GuidLst;
      UnloadFlags flags = optionsForm.Flags;
      SaveFileDialog saveFileDialog1 = new SaveFileDialog();
      saveFileDialog1.DefaultExt = "xlsx";
      saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
      saveFileDialog1.FileName = string.Join(" ,", (IEnumerable<string>) values);
      saveFileDialog1.RestoreDirectory = true;
      SaveFileDialog saveFileDialog2 = saveFileDialog1;
      if (saveFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      ImbaseUnloadBackGroundTask task = new ImbaseUnloadBackGroundTask(saveFileDialog2.FileName, dbTypedObjectIdSet, guidLst, flags);
      ServiceHolder.BackgroundTaskView.AddTask((IBackgroundTask) task);
      task.Resume();
    }
  }
}
