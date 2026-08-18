// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionTypeSelectionForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionType;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionTypeSelectionForm : Form
{
  private AutoSelectionNodeCommon _selNode;
  private IContainer components;

  public AutoSelectionTypeSelectionForm() => this.InitializeComponent();

  public AutoSelectionNodeCommon SelectionNode
  {
    get => this._selNode;
    set => this._selNode = value;
  }

  public static AutoSelectionNodeType SelectSelectionItemType(AutoSelectionNodeCommon node)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString(sc_746.ssp_automatch_747()), "", (IDescriptor) new AutoSelectionTypesDescriptor(), typeof (AutoSelectionTypeRec), SelectionOptions.HideViewsToolbar | SelectionOptions.HideViewsGroupingBox | SelectionOptions.SelectOtherNodes | SelectionOptions.DisableMultiselect);
    return objArray == null || objArray.Length != 1 || !(objArray[0] is AutoSelectionTypeRec) ? AutoSelectionNodeType.None : ((AutoSelectionTypeRec) objArray[0]).Type;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionTypeSelectionForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (AutoSelectionTypeSelectionForm);
    this.ResumeLayout(false);
  }
}
