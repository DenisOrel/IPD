// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcNumerationRules.NumRuleView
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcNumerationRules;

/// <summary>Summary description for NumNodeView.</summary>
[ViewDescriptionProvider(typeof (NumRuleView.NumRuleViewDescriptionProvider))]
public class NumRuleView : NumRuleObjControl, IView
{
  /// <summary>Заголовок закладки</summary>
  protected string _caption = string.Empty;

  /// <summary>
  /// 
  /// </summary>
  protected override void InitializeCustomControls()
  {
    base.InitializeCustomControls();
    this.btnApply.DialogResult = DialogResult.None;
    this.btnCancel.DialogResult = DialogResult.None;
  }

  /// <summary>ImageIndex</summary>
  public int ImageIndex => -1;

  /// <summary>OrderID</summary>
  public int OrderID => 0;

  /// <summary>Caption</summary>
  public string Caption => LocalizationHolder.rm.GetString("TechCard.Client_229");

  /// <summary>Initialize</summary>
  /// <param name="items"></param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectID = 0L;
    if (items == null || items.Count == 0 || !(items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData))
      return;
    this._objectID = itemData.Value;
  }

  /// <summary>Deactivate</summary>
  /// <param name="nextView"></param>
  public void Deactivate(IView nextView)
  {
    if (!this.Modified || MessageBox.Show(LocalizationHolder.rm.GetString(sc_19541.ssp_techcard_19542()), LocalizationHolder.rm.GetString(sc_19541.ssp_techcard_19543()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.DataSave();
  }

  /// <summary>Activate</summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.DataLoad();
  }

  private sealed class NumRuleViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList))
        ServicesManager.GetService(typeof (INamedImageList));
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("TechCard.Client_229"),
        ImageIndex = -1,
        OrderID = 0
      };
    }
  }
}
