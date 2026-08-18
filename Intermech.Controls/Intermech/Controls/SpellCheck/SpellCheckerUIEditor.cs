
// Type: Intermech.Controls.SpellCheck.SpellCheckerUIEditor
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Controls.SpellCheck;

public class SpellCheckerUIEditor : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    Hashtable hashtable = value as Hashtable;
    SpellChecker.Instance.Dict.UserFileLoadDB(hashtable);
    if (new SpellCheckOptionsForm(hashtable).ShowDialog() == DialogResult.OK)
      SpellChecker.Instance.Dict.UserFileSave(hashtable, true);
    return base.EditValue(context, provider, value);
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }
}
