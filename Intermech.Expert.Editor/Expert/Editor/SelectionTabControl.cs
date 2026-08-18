// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.SelectionTabControl
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Expert.Editor;

internal class SelectionTabControl : ListSelectionTab, ISelectionDialogTab
{
  public SelectionTabControl()
    : base(LocalizationHolder.rm.GetString("Expert.Editor_600"), 1, new List<int>((IEnumerable<int>) new int[1]
    {
      MetaDataHelper.GetObjectTypeID(ExpertObjGUIDs.DocScript)
    }), LocalizationHolder.rm.GetString("Expert.Editor_601"), LocalizationHolder.rm.GetString("Expert.Editor_602"))
  {
  }

  protected override int ObjectTypesInSelectDialog
  {
    get => MetaDataHelper.GetObjectTypeID(ExpertObjGUIDs.DocScript);
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
