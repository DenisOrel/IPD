
// Type: Intermech.PropertyEditors.FilteredTreeView
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// TreeView с поддержкой интерфейса фильтрования.
/// ноды TreeView при добавлении к дереву должны обращаться к методам интерфейса ISelectorFilter
/// </summary>
public class FilteredTreeView : TreeView, ISelectorFilter
{
  private ISelectorFilter selectorFilter;

  public ISelectorFilter SelectorFilter
  {
    get => this.selectorFilter;
    set => this.selectorFilter = value;
  }

  public bool IsInFilter(int category, object id)
  {
    return this.selectorFilter == null || this.selectorFilter.IsInFilter(category, id);
  }
}
