
// Type: Intermech.PropertyEditors.MemoEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Редактор Memo полей</summary>
internal class MemoEditor : UITypeEditor
{
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider sp,
    object value)
  {
    if (value == null)
      return value;
    MemoForm memoForm = new MemoForm();
    int aMaxMemoSize;
    if (value is MemoPropertyClass)
    {
      aMaxMemoSize = ((MemoPropertyClass) value).MaxMemoSize;
      memoForm.MaxMemoSize = aMaxMemoSize;
      memoForm.Memo = ((MemoPropertyClass) value).Memo;
    }
    else
    {
      aMaxMemoSize = Consts.MaxStringSize;
      memoForm.MaxMemoSize = aMaxMemoSize;
      memoForm.Memo = value.ToString();
    }
    bool flag1 = false;
    bool flag2 = false;
    if (context != null && context.PropertyDescriptor is PropDescriptor)
    {
      flag1 = ((PropDescriptor) context.PropertyDescriptor).DisableManualEdit;
      flag2 = context.PropertyDescriptor.IsReadOnly;
    }
    memoForm.DisableManualEdit = flag1;
    memoForm.ReadonlyFlag = flag2;
    return memoForm.ShowDialog() == DialogResult.OK ? (object) new MemoPropertyClass(memoForm.Memo, ((MemoPropertyClass) value).IsNull, aMaxMemoSize) : value;
  }
}
