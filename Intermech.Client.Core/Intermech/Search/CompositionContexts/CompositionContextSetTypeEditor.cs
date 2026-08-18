
// Type: Intermech.Search.CompositionContexts.CompositionContextSetTypeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Search.CompositionContexts;

public sealed class CompositionContextSetTypeEditor : UITypeEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (!(value is CompositionContextSet compositionContextSet1))
      compositionContextSet1 = CompositionContextSet.Empty;
    CompositionContextSet compositionContextSet2 = compositionContextSet1;
    if (provider == null || !(provider.GetService(typeof (IWindowsFormsEditorService)) is IWindowsFormsEditorService service))
      return (object) compositionContextSet2;
    using (CheckedListBox checkedListBox = new CheckedListBox())
    {
      checkedListBox.CheckOnClick = true;
      ((ListControl) checkedListBox).DisplayMember = "Item2";
      ((ListControl) checkedListBox).ValueMember = "Item1";
      foreach (CompositionContext allContext in CompositionContextClientHelper.AllContexts)
        checkedListBox.Items.Add((object) new Tuple<long, string>(allContext.Value, allContext.Description));
      foreach (CompositionContext compositionContext1 in compositionContextSet2.CompositionContexts)
      {
        CompositionContext compositionContext = compositionContext1;
        Tuple<long, string> tuple = checkedListBox.Items.Cast<Tuple<long, string>>().FirstOrDefault<Tuple<long, string>>((Func<Tuple<long, string>, bool>) (o => o.Item1 == compositionContext.Value));
        if (tuple != null)
          checkedListBox.SetItemChecked(checkedListBox.Items.IndexOf((object) tuple), true);
      }
      service.DropDownControl((Control) checkedListBox);
      List<CompositionContext> compositionContextList = new List<CompositionContext>();
      foreach (Tuple<long, string> tuple in checkedListBox.CheckedItems.Cast<Tuple<long, string>>())
        compositionContextList.Add(new CompositionContext(tuple.Item1, tuple.Item2));
      return (object) new CompositionContextSet(compositionContextList.ToArray());
    }
  }

  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.DropDown;
  }
}
