// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.SelectionTabControl
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Reports;

internal class SelectionTabControl : ListSelectionTab, ISelectionDialogTab
{
  public SelectionTabControl()
    : base(LocalizationHolder.rm.GetString("Document.Client_135"), 0, new List<int>((IEnumerable<int>) new int[2]
    {
      MetaDataHelper.GetObjectTypeID("cad00289-306c-11d8-b4e9-00304f19f545"),
      MetaDataHelper.GetObjectTypeID("cad0028a-306c-11d8-b4e9-00304f19f545")
    }), LocalizationHolder.rm.GetString("Document.Client_136"), LocalizationHolder.rm.GetString("Document.Client_137"))
  {
  }

  protected override int ObjectTypesInSelectDialog
  {
    get
    {
      return !this.isPersonal ? MetaDataHelper.GetObjectTypeID("cad00289-306c-11d8-b4e9-00304f19f545") : MetaDataHelper.GetObjectTypeID("cad00288-306c-11d8-b4e9-00304f19f545");
    }
  }

  [SpecialName]
  string ISelectionDialogTab.get_Caption() => this.Caption;

  [SpecialName]
  int ISelectionDialogTab.get_Index() => this.Index;

  [SpecialName]
  Control ISelectionDialogTab.get_TabControl() => this.TabControl;

  void ISelectionDialogTab.Initialize(IUserSession session, long selectionID, bool isPersonal)
  {
    this.Initialize(session, selectionID, isPersonal);
  }

  void ISelectionDialogTab.Save(IUserSession session, long selectionID)
  {
    this.Save(session, selectionID);
  }

  [SpecialName]
  void ISelectionDialogTab.add_OnChanged(EventHandler value) => this.OnChanged += value;

  [SpecialName]
  void ISelectionDialogTab.remove_OnChanged(EventHandler value) => this.OnChanged -= value;
}
