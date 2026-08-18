
// Type: Intermech.Navigator.Controls.ChildrenViewSearchComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Search.SearchHistory;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewSearchComponent : Component
{
  private CancellationTokenSource _cancellationTokenSource;
  private GlobalIndexSearchValue _globalIndexSearchValue = GlobalIndexSearchValue.Empty;
  private ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState _searchState;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ChildrenViewSearchComponent() => this.InitializeComponent();

  public ChildrenViewSearchComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  public event EventHandler SearchStateChanged;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenView ChildrenView { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState SearchState
  {
    get => this._searchState;
    set
    {
      if (this._searchState == value)
        return;
      this._searchState = value;
      EventHandler searchStateChanged = this.SearchStateChanged;
      if (searchStateChanged == null)
        return;
      searchStateChanged((object) this, EventArgs.Empty);
    }
  }

  public void Attach(ChildrenView childrenView)
  {
    this.ChildrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
    this.ChildrenView.Activated += new EventHandler(this.ChildrenView_Activated);
    this.ChildrenView.DisableFiltrationChanged += new EventHandler(this.ChildrenView_DisableFiltrationChanged);
    this.ChildrenView.SearchComboBoxItem.ComboBox.KeyUp += new KeyEventHandler(this.SearchComboBox_KeyUp);
    this.ChildrenView.SearchComboBoxItem.ComboBox.TextChanged += new EventHandler(this.SearchComboBox_TextChanged);
    this.ChildrenView.SearchButtonItem.Click += new EventHandler(this.SearchButtonItem_Click);
    this.ChildrenView.CancelSearchButtonItem.Click += new EventHandler(this.CancelSearchButtonItem_Click);
    this.ChildrenView.ClearSearchResultsButtonItem.Click += new EventHandler(this.ClearSearchResultsButtonItem_Click);
    this.ChildrenView.ChangeSearchSettingsButtonItem.Click += new EventHandler(this.ChangeSearchSettingsButtonItem_Click);
    this.SetGlobalIndexSearchValue();
  }

  public GlobalIndexSearchValue GetGlobalIndexSearchValue()
  {
    return (GlobalIndexSearchValue) this._globalIndexSearchValue.Clone();
  }

  private void ChildrenView_Activated(object sender, EventArgs e)
  {
    this.UpdateControls();
    this.UpdateSearchComboBoxItem();
  }

  private void ChildrenView_DisableFiltrationChanged(object sender, EventArgs e)
  {
    this.UpdateControls();
  }

  private void SearchComboBox_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Return)
      return;
    this.Search();
  }

  private void SearchComboBox_TextChanged(object sender, EventArgs e) => this.UpdateControls();

  private void SearchButtonItem_Click(object sender, EventArgs e) => this.Search();

  private void CancelSearchButtonItem_Click(object sender, EventArgs e) => this.CancelSearch();

  private void ClearSearchResultsButtonItem_Click(object sender, EventArgs e)
  {
    this.ClearSearchResults();
  }

  private void ChangeSearchSettingsButtonItem_Click(object sender, EventArgs e)
  {
    Intermech.Bars.ToolBar toolBar = this.ChildrenView.SearchButtonItem.ToolBar;
    Rectangle buttonBounds = this.ChildrenView.SearchButtonItem.ButtonBounds;
    int x1 = buttonBounds.X;
    buttonBounds = this.ChildrenView.SearchButtonItem.ButtonBounds;
    int y1 = buttonBounds.Y;
    buttonBounds = this.ChildrenView.SearchButtonItem.ButtonBounds;
    int height1 = buttonBounds.Height;
    int y2 = y1 + height1 + 5;
    Point p = new Point(x1, y2);
    Point screen = toolBar.PointToScreen(p);
    int width1 = 400;
    int height2 = 200;
    int num1 = screen.X + width1;
    Rectangle workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int x2 = workingArea1.X;
    workingArea1 = Screen.PrimaryScreen.WorkingArea;
    int width2 = workingArea1.Width;
    int num2 = x2 + width2;
    Rectangle workingArea2;
    if (num1 > num2)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int x3 = workingArea2.X;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int width3 = workingArea2.Width;
      int num3 = x3 + width3 - width1;
      local.X = num3;
    }
    int x4 = screen.X;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int x5 = workingArea2.X;
    if (x4 < x5)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int x6 = workingArea2.X;
      local.X = x6;
    }
    int num4 = screen.Y + height2;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y3 = workingArea2.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int height3 = workingArea2.Height;
    int num5 = y3 + height3;
    if (num4 > num5)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y4 = workingArea2.Y;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int height4 = workingArea2.Height;
      int num6 = y4 + height4 - height2;
      local.Y = num6;
    }
    int y5 = screen.Y;
    workingArea2 = Screen.PrimaryScreen.WorkingArea;
    int y6 = workingArea2.Y;
    if (y5 < y6)
    {
      ref Point local = ref screen;
      workingArea2 = Screen.PrimaryScreen.WorkingArea;
      int y7 = workingArea2.Y;
      local.Y = y7;
    }
    if (IndexSearchOptionsForm.Execute(ref this._globalIndexSearchValue.SearchOptions, new Rectangle(screen.X, screen.Y, width1, height2)) != DialogResult.OK)
      return;
    this.Search();
  }

  private void AddGlobalIndexSearchValueToServicesManager()
  {
    if (ServicesManager.GetService(typeof (GlobalIndexSearchValue)) != null)
      ServicesManager.RemoveService(typeof (GlobalIndexSearchValue));
    ServicesManager.AddService(typeof (GlobalIndexSearchValue), (object) new GlobalIndexSearchValue(this._globalIndexSearchValue.Value, this._globalIndexSearchValue.SearchOptions, this._globalIndexSearchValue.History));
  }

  private void CancelSearch()
  {
    if (this._cancellationTokenSource != null)
      this._cancellationTokenSource.Cancel();
    this.ChildrenView.DataAdapter.ClearPreloadedData();
    this.SearchState = ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.None;
    this.UpdateControls();
  }

  private void ClearSearchResults()
  {
    this.SearchState = ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.None;
    this.UpdateControls();
    this.ChildrenView.ReloadItems();
  }

  private void SetGlobalIndexSearchValue()
  {
    if (DesignerServices.IsInDesignMode((Component) this, true))
      return;
    if (!(ServicesManager.GetService(typeof (GlobalIndexSearchValue)) is GlobalIndexSearchValue indexSearchValue))
      indexSearchValue = GlobalIndexSearchValueClientHelpers.GetDefaultGlobalIndexSearchValue();
    this._globalIndexSearchValue = indexSearchValue;
  }

  private void UpdateControls()
  {
    if (this.ChildrenView.SearchComboBoxItem.ComboBox == null)
      return;
    bool flag = this.ChildrenView.IsFiltrationEnabled();
    this.ChildrenView.SearchComboBoxItem.Enabled = this.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.ChildrenView.SearchComboBoxItem.Visible = flag;
    this.ChildrenView.SearchButtonItem.Enabled = this.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.ChildrenView.SearchButtonItem.Visible = flag && this.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.ChildrenView.CancelSearchButtonItem.Visible = flag && this.SearchState == ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.ChildrenView.ClearSearchResultsButtonItem.Visible = flag && this.SearchState == ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loaded;
    this.ChildrenView.ChangeSearchSettingsButtonItem.Enabled = this.SearchState != ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.ChildrenView.ChangeSearchSettingsButtonItem.Visible = flag;
  }

  private void UpdateSearchComboBoxItem()
  {
    if (!this.ChildrenView.IsFiltrationEnabled() || this._globalIndexSearchValue.History == null || this.ChildrenView.SearchComboBoxItem.ComboBox == null)
      return;
    this.ChildrenView.SearchComboBoxItem.ComboBox.BeginUpdate();
    try
    {
      this.ChildrenView.SearchComboBoxItem.ComboBox.Items.Clear();
      this.ChildrenView.SearchComboBoxItem.ComboBox.Items.AddRange((object[]) this._globalIndexSearchValue.History.ToArray());
      this.AddSearchComboBoxItemsFromGlobalIndex(30);
    }
    finally
    {
      this.ChildrenView.SearchComboBoxItem.ComboBox.EndUpdate();
    }
  }

  /// <summary>
  /// Добавляет в выпадающий список поисковые запросы, текущего пользователя
  /// за последние несколько дней. Если в настройках установлено, сохранение поисковых запросов
  /// </summary>
  /// <param name="countDaysAgo">Количество дней за которое найти поисковые запросы</param>
  private void AddSearchComboBoxItemsFromGlobalIndex(int countDaysAgo)
  {
    if (countDaysAgo <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexSettings)) is IGlobalIndexSettings customService1) && !customService1.IsSaveSearchQueryHistory)
        return;
      IGlobalIndexHelper customService2 = (IGlobalIndexHelper) sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexHelper));
      ICurrentUserAndRole service = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
      if (customService2 == null || service == null)
        return;
      DataTable queriesHistory = customService2.GetQueriesHistory(sessionKeeper.Session.SessionGUID, service.UserID, DateTime.Today - TimeSpan.FromDays((double) countDaysAgo), DateTime.Now);
      if (queriesHistory == null)
        return;
      List<string> source = new List<string>();
      foreach (DataRow row in (InternalDataCollectionBase) queriesHistory.Rows)
      {
        SearchHistoryItem searchHistoryItem = new SearchHistoryItem()
        {
          SearchString = DataSetProcessor.GetStringValue(row, "F_QUERY_STR", (string) null)
        };
        source.Add(searchHistoryItem.SearchString);
      }
      foreach (string str in source.GroupBy<string, string>((System.Func<string, string>) (i => i)).OrderByDescending<IGrouping<string, string>, int>((System.Func<IGrouping<string, string>, int>) (i => i.Count<string>())).Select<IGrouping<string, string>, string>((System.Func<IGrouping<string, string>, string>) (i => i.Key)).ToList<string>())
      {
        if (!this.ChildrenView.SearchComboBoxItem.ComboBox.Items.Contains((object) str))
          this.ChildrenView.SearchComboBoxItem.ComboBox.Items.Add((object) str);
      }
    }
  }

  public async Task ReloadItemsAsync()
  {
    await this.Preload();
    if (!this.ChildrenView.DataAdapter.HasPreloadedData)
      return;
    this.ChildrenView.ReloadItems();
    this.SearchState = ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loaded;
    this.UpdateControls();
  }

  private async Task Preload()
  {
    if (this._cancellationTokenSource != null)
      this._cancellationTokenSource.Cancel();
    this._cancellationTokenSource = new CancellationTokenSource();
    await Task.Run((Action) (() => this.ChildrenView.DataAdapter.Preload()), this._cancellationTokenSource.Token);
  }

  private async void Search()
  {
    if (string.IsNullOrEmpty(this.ChildrenView.SearchComboBoxItem.ComboBox.Text))
      return;
    this._globalIndexSearchValue.Value = this.ChildrenView.SearchComboBoxItem.ComboBox.Text;
    this._globalIndexSearchValue.AddToHistory(this.ChildrenView.SearchComboBoxItem.ComboBox.Text);
    this.UpdateSearchComboBoxItem();
    this.AddGlobalIndexSearchValueToServicesManager();
    this.SearchState = ChildrenViewSearchComponent.ChildrenViewSearchComponentSearchState.Loading;
    this.UpdateControls();
    try
    {
      await this.ReloadItemsAsync();
    }
    catch
    {
      this.CancelSearch();
      this.ClearSearchResults();
      this.ChildrenView.FiltersComboBoxItem.ComboBox.Focus();
      throw;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

  public enum ChildrenViewSearchComponentSearchState
  {
    None,
    Loading,
    Loaded,
  }
}
