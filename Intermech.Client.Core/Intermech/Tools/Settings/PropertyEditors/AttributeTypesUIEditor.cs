
// Type: Intermech.Tools.Settings.PropertyEditors.AttributeTypesUIEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Tools.Settings.PropertyEditors;

public sealed class AttributeTypesUIEditor : UITypeEditor
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
    ChangeTrackingListAdapter<GlobalId<int>> trackingListAdapter = ((ChangeTrackingListAdapter<GlobalId<int>>) value).Clone();
    return new AttributeTypeListEditorForm()
    {
      AttributeTypes = trackingListAdapter.Items
    }.ShowDialog() != DialogResult.OK ? value : (object) trackingListAdapter;
  }
}
