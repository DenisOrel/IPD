
// Type: Intermech.PropertyEditors.PossibleValuesEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class PossibleValuesEditor : UITypeEditor
{
  private PossibleValuesForm possibleValuesForm;
  private EventsHolder.GetListDelegate getList;

  public PossibleValuesEditor()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public PossibleValuesEditor(EventsHolder.GetListDelegate getListDelegate)
  {
    this.getList = getListDelegate;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return context.PropertyDescriptor.IsReadOnly ? UITypeEditorEditStyle.None : UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    ArrayList aObjTypes = (ArrayList) null;
    if ((((PossibleValuesPropertyClass) value).FieldType == FieldTypes.ftObjectLink || ((PossibleValuesPropertyClass) value).FieldType == FieldTypes.ftObjectLinkByID) && this.getList != null)
      aObjTypes = this.getList((object) this);
    if (this.possibleValuesForm == null)
      this.possibleValuesForm = new PossibleValuesForm();
    this.possibleValuesForm.SetData(((PossibleValuesPropertyClass) value).PossibleValues, ((PossibleValuesPropertyClass) value).FieldType, aObjTypes);
    int num = (int) this.possibleValuesForm.ShowDialog();
    return this.possibleValuesForm.DialogResult == DialogResult.OK ? (object) new PossibleValuesPropertyClass(this.possibleValuesForm.GetData(), ((PossibleValuesPropertyClass) value).FieldType) : value;
  }
}
