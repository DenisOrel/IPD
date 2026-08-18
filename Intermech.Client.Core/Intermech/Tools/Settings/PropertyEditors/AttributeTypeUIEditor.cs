
// Type: Intermech.Tools.Settings.PropertyEditors.AttributeTypeUIEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public class AttributeTypeUIEditor : UITypeEditor
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
    GlobalId<int> globalId = (GlobalId<int>) value;
    using (AttributesSelectDlg dlg = new AttributesSelectDlg(false))
    {
      this.BeforeShowDialog(dlg);
      if (globalId.Id != 0)
        dlg.SelectedAttributeIDOnStartup(globalId.Id);
      if (dlg.ShowDialog() != DialogResult.OK || dlg.SelectedAttributesID.Count == 0)
        return value;
      int num = dlg.SelectedAttributesID[0];
      if (num == globalId.Id)
        return value;
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(num);
      return (object) new GlobalId<int>(attributeType.AttributeGuid, num, attributeType.Name);
    }
  }

  protected virtual void BeforeShowDialog(AttributesSelectDlg dlg)
  {
  }
}
