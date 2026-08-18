// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionObjectEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Protection;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionObjectEditor : UITypeEditor
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
    byte[] numArray1 = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray1.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray1;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (!(context.Instance is AutoSelectionNodeItemObject instance))
      return value;
    if (instance.ObjTypeGuid.Value == Guid.Empty)
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_688.ssp_automatch_689()), LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    int objectTypeId = MetaDataHelper.GetObjectTypeID(instance.ObjTypeGuid.Value.ToString());
    long[] numArray2 = SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("AutoSelection.Client_39"), "", objectTypeId, SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
    if (numArray2 == null || numArray2.Length == 0)
      return value;
    long num3 = numArray2[0];
    if (num3 < 0L)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("AutoSelection.Client_83"), LocalizationHolder.rm.GetString("AutoSelection.Client_84"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) != DialogResult.OK)
        return value;
      num3 = -num3;
    }
    return (object) new AS_Long(num3);
  }
}
