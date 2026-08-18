
// Type: Intermech.Controls.Grid.CustomCollectionEditor
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.ComponentModel.Design;


namespace Intermech.Controls.Grid;

/// <summary>
/// Class created so we can force an invalidation/update on the control when the column editor returns
/// </summary>
/// <summary>
/// Default Constructor for custom column collection editor
/// </summary>
/// <param name="type"></param>
internal class CustomCollectionEditor(Type type) : CollectionEditor(type)
{
  private int m_nUnique = 1;

  /// <summary>Called to edit a value in collection editor</summary>
  /// <param name="context"></param>
  /// <param name="isp"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider isp,
    object value)
  {
    ListGrid instance = (ListGrid) context.Instance;
    object obj = base.EditValue(context, isp, value);
    instance.Refresh();
    return obj;
  }

  /// <summary>
  /// Creates a new instance of a column for custom collection
  /// </summary>
  /// <param name="itemType"></param>
  /// <returns></returns>
  protected override object CreateInstance(Type itemType)
  {
    string editValue;
    object[] items;
    do
    {
      editValue = "Column" + this.m_nUnique.ToString();
      items = this.GetItems((object) editValue);
      ++this.m_nUnique;
    }
    while (items.Length != 0);
    object instance;
    ((ListColumn) (instance = base.CreateInstance(itemType))).Name = editValue;
    return instance;
  }
}
