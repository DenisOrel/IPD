
// Type: Intermech.Navigator.DBObjects.ObjectListFiltration
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

public sealed class ObjectListFiltration : IObjectListFiltration
{
  private ChildrenView _childrenView;

  public ObjectListFiltration(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
  }

  public bool FilterByCurrentVersionsRule => this._childrenView.FilterByCurrentVersionsRule;

  public bool IsGlobalIndexSearchActived
  {
    get
    {
      return this._childrenView.SearchComponent.SearchState == ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading || this._childrenView.SearchComponent.SearchState == ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loaded;
    }
  }

  public Guid SelectedFilterGuid => this._childrenView.SelectedFilterGuid;

  public GlobalIndexSearchValue GlobalIndexSearchValue
  {
    get => this._childrenView.SearchComponent.GetGlobalIndexSearchValue();
  }
}
