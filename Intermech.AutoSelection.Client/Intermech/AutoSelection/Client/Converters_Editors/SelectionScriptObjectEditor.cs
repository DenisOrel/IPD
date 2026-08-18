// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Converters_Editors.SelectionScriptObjectEditor
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Converters_Editors;

internal class SelectionScriptObjectEditor : UITypeEditor
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
    if (!(value is AS_Guid) || context == null || context.Instance == null || !(context.Instance is AutoSelectionNodeBase instance))
      return value;
    if (instance.Rule == null)
    {
      int num = (int) MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString(sc_701.ssp_automatch_702()), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_38"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return value;
    }
    if (!(SelectionWindow.Select(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_45"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(AutoSelectionConsts.objTypeScriptID), typeof (IDBObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect) is IDBObjectID[] dbObjectIdArray) || dbObjectIdArray.Length == 0)
      return value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(dbObjectIdArray[0].Value);
      return objectInfo.Empty ? value : (object) new AS_Guid(objectInfo.VersionGuid);
    }
  }
}
