
// Type: Intermech.PropertyEditors.ExEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing.Design;


namespace Intermech.PropertyEditors;

internal class ExEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider sp,
    object value)
  {
    TestForm testForm = new TestForm();
    testForm.ExText = value == null ? "" : ((ExPropertyClass) value).Id;
    int num = (int) testForm.ShowDialog();
    return (object) new ExPropertyClass(testForm.ExText);
  }
}
