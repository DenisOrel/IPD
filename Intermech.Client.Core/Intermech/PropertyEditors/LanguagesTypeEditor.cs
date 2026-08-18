
// Type: Intermech.PropertyEditors.LanguagesTypeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.PropertyEditors;

public class LanguagesTypeEditor : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (provider != null)
    {
      IWindowsFormsEditorService service = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
      if (service != null)
      {
        CheckedListBox listBox = new CheckedListBox();
        listBox.BorderStyle = BorderStyle.None;
        listBox.CheckOnClick = true;
        if (!(value is string str1))
          str1 = string.Empty;
        listBox.Items.Add((object) DataHolders.LanguagesHolder.GetNamebyID(""), str1.Length == 0);
        foreach (DataRow row in (InternalDataCollectionBase) DataHolders.LanguagesHolder.DataTable.Rows)
        {
          string str2 = row["F_LANGUAGE_ID"].ToString();
          string str3 = row["F_LANGUAGE_NAME"].ToString();
          listBox.Items.Add((object) str3, str1.IndexOf(str2[0]) != -1);
        }
        using (CheckedListBoxHelper checkedListBoxHelper = new CheckedListBoxHelper(listBox, true))
        {
          service.DropDownControl((Control) listBox);
          if (checkedListBoxHelper.Break)
            return (object) str1;
        }
        string empty = string.Empty;
        if (listBox.GetItemChecked(0))
          return (object) empty;
        int count = listBox.Items.Count;
        StringBuilder stringBuilder = new StringBuilder(count);
        for (int index = 1; index < count; ++index)
        {
          if (listBox.GetItemChecked(index))
            stringBuilder.Append(DataHolders.LanguagesHolder.GetIDbyName(listBox.GetItemText(listBox.Items[index])));
        }
        return (object) stringBuilder.ToString();
      }
    }
    return value;
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }
}
