
// Type: Intermech.Search.SearchHistory.SearchHistoryView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search.SearchHistory;

public sealed class SearchHistoryView : UserControl, IView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SearchHistoryControl _searchHistoryControl;

  public SearchHistoryView() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
  }

  public void Activate(IView previousView)
  {
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => "История поисковых запросов";

  public int ImageIndex => -1;

  public int OrderID => int.MaxValue;

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
  private void InitializeComponent()
  {
    this._searchHistoryControl = new SearchHistoryControl();
    this._searchHistoryControl.BeginInit();
    this.SuspendLayout();
    this._searchHistoryControl.Dock = DockStyle.Fill;
    this._searchHistoryControl.Location = new Point(0, 0);
    this._searchHistoryControl.Name = "_searchHistoryControl";
    this._searchHistoryControl.Size = new Size(613, 309);
    this._searchHistoryControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._searchHistoryControl);
    this.Name = nameof (SearchHistoryView);
    this.Size = new Size(613, 309);
    this._searchHistoryControl.EndInit();
    this.ResumeLayout(false);
  }
}
