
// Type: Intermech.Navigator.SelectionDialogTabsService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator;

/// <summary>Сервис дополнительных закладок для формы выборки</summary>
internal class SelectionDialogTabsService : ISelectionDialogTabsService
{
  public ISelectionDialogTab[] Tabs
  {
    get
    {
      if (this.SelectionDialogTabEvent == null)
        return (ISelectionDialogTab[]) null;
      Delegate[] invocationList = this.SelectionDialogTabEvent.GetInvocationList();
      SortedDictionary<int, ISelectionDialogTab> sortedDictionary = new SortedDictionary<int, ISelectionDialogTab>();
      for (int index = 0; index < invocationList.Length; ++index)
      {
        ISelectionDialogTab selectionDialogTab = ((SelectionDialogTabCreateHandler) invocationList[index])((object) this, new SelectionDialogTabEventArgs());
        sortedDictionary.Add(selectionDialogTab.Index, selectionDialogTab);
      }
      List<ISelectionDialogTab> selectionDialogTabList = new List<ISelectionDialogTab>(sortedDictionary.Count);
      foreach (KeyValuePair<int, ISelectionDialogTab> keyValuePair in sortedDictionary)
        selectionDialogTabList.Add(keyValuePair.Value);
      return selectionDialogTabList.ToArray();
    }
  }

  public event SelectionDialogTabCreateHandler SelectionDialogTabEvent;
}
