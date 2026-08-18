// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionImbaseFolder
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.Forms;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Protection;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionImbaseFolder : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is AS_Long) || context == null || context.Instance == null)
      return value;
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (!(context.Instance is AutoSelectionNodeItemImbase instance))
      return value;
    if (instance.ObjTypeGuid.Value == Guid.Empty)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_685.ssp_automatch_686()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    AutoSelectionImbaseObjSelectForm form = new AutoSelectionImbaseObjSelectForm(MetaDataHelper.GetObjectTypeID(instance.ObjTypeGuid.Value.ToString()), instance.ImbaseObjectID.Value);
    if (form.ShowTopDialog() != DialogResult.OK || form.ImbaseObjID == 0L)
      return value;
    instance.ImbaseCatalogID = new AS_Long(form.ImbaseCatalogID);
    return (object) new AS_Long(form.ImbaseObjID);
  }
}
