
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodeCategoryEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class OrganizerChildNodeCategoryEditor : UITypeEditor
{
  /// <summary>
  /// 
  /// </summary>
  private IWindowsFormsEditorService _svc;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    this._svc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
    int num = -1;
    if (value is OrganizerChildNodeCategoryProxy nodeCategoryProxy1)
      num = nodeCategoryProxy1.ID;
    ListBox listBox = new ListBox();
    listBox.BorderStyle = BorderStyle.None;
    if (ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service)
    {
      Dictionary<int, string> nodesCaption = service.NodesCaption;
      if (nodesCaption != null)
      {
        foreach (KeyValuePair<int, string> keyValuePair in nodesCaption)
        {
          OrganizerChildNodeCategoryProxy nodeCategoryProxy2 = new OrganizerChildNodeCategoryProxy(keyValuePair.Key, keyValuePair.Value);
          listBox.Items.Add((object) nodeCategoryProxy2);
          nodeCategoryProxy1 = num == keyValuePair.Key ? nodeCategoryProxy2 : nodeCategoryProxy1;
        }
      }
    }
    OrganizerChildNodeCategoryProxy nodeCategoryProxy3 = new OrganizerChildNodeCategoryProxy(MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545"), LocalizationHolder.rm.GetString("Organaizer_TaskCaption"));
    listBox.Items.Add((object) nodeCategoryProxy3);
    OrganizerChildNodeCategoryProxy nodeCategoryProxy4 = num == nodeCategoryProxy3.ID ? nodeCategoryProxy3 : nodeCategoryProxy1;
    listBox.SelectedItem = (object) nodeCategoryProxy4;
    listBox.Click += new EventHandler(this.ListBoxClick);
    this._svc.DropDownControl((Control) listBox);
    listBox.Click -= new EventHandler(this.ListBoxClick);
    return listBox.SelectedItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ListBoxClick(object sender, EventArgs e)
  {
    if (this._svc == null)
      return;
    this._svc.CloseDropDown();
  }
}
