
// Type: Intermech.Navigator.Conditions.AttributeConditionControls.DescriberButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client.PropertyEditors;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.ComponentModel.Design;
using System.Drawing.Design;


namespace Intermech.Navigator.Conditions.AttributeConditionControls;

internal class DescriberButtonDialog : ButtonDialog
{
  private IAttributePropertyDescriber _describer;

  public bool Handled { get; private set; }

  public DescriberButtonDialog(
    IAttributePropertyDescriber describer,
    IConditionDataProvider dataProvider,
    int attributeID,
    object value)
    : base(dataProvider, attributeID, value)
  {
    this._describer = describer;
  }

  public override bool OnOpenDialog(bool multiselect)
  {
    UITypeEditor descriptorEditor = this._describer.GetPropDescriptorEditor(this.attributeID) as UITypeEditor;
    if (descriptorEditor.GetEditStyle() != UITypeEditorEditStyle.Modal)
      return false;
    this.Handled = true;
    using (ServiceContainer provider = new ServiceContainer())
    {
      provider.AddService(typeof (IEditorDialogStyle), (object) new EditorDialogStyle(false));
      object attributeValue = this._describer.GetAttributeValue((IElementInfo) null, this.attributeID, descriptorEditor.EditValue((IServiceProvider) provider, this.Value));
      if (object.Equals(attributeValue, this.Value))
        return false;
      this.Value = attributeValue;
      object propDescriptorValue = this._describer.GetPropDescriptorValue((IElementInfo) null, this.attributeID, attributeValue);
      this.Text = propDescriptorValue != null ? propDescriptorValue.ToString() : attributeValue.ToString();
      return true;
    }
  }
}
