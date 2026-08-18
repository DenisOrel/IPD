
// Type: Intermech.Navigator.Controls.ChildrenViewAutoCompleteSearchComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public class ChildrenViewAutoCompleteSearchComponent : Component
{
  private static readonly TimeSpan AutoCompleteDelay = new TimeSpan(100L);
  private const int AutoCompleteItemsCount = 500;
  private const int MinTextLengthForStartAutoComplete = 2;
  public static readonly char[] SpecialCaseMarks = new char[3]
  {
    '?',
    'n',
    'N'
  };
  private ChildrenView _childrenView;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Timer _timer;

  public ChildrenViewAutoCompleteSearchComponent() => this.InitializeComponent();

  public ChildrenViewAutoCompleteSearchComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  public void Attach(ChildrenView childrenView)
  {
    this._childrenView = childrenView != null ? childrenView : throw new ArgumentNullException(nameof (childrenView));
    this._childrenView.SearchComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this._childrenView.SearchComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
    this._childrenView.SearchComboBox.TextChanged += new EventHandler(this.SearchComboBox_TextChanged);
    this._childrenView.SearchComboBox.KeyUp += new KeyEventHandler(this.SearchComboBox_KeyUp);
  }

  private void Timer_Tick(object sender, EventArgs e)
  {
    this._timer.Stop();
    string comboBoxText = this._childrenView.SearchComboBox.Text;
    if (string.IsNullOrEmpty(comboBoxText) || comboBoxText.Length <= 2 || ((IEnumerable<char>) ChildrenViewAutoCompleteSearchComponent.SpecialCaseMarks).Contains<char>(comboBoxText[0]))
      return;
    Task.Run((Action) (() =>
    {
      try
      {
        string[] searchComboBoxAutoCompleteItems = (string[]) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          searchComboBoxAutoCompleteItems = ((IGlobalIndexHelper) sessionKeeper.Session.GetCustomService(typeof (IGlobalIndexHelper))).GetSimilarQueries(sessionKeeper.Session.SessionGUID, comboBoxText, 500);
        this._childrenView.Invoke((Delegate) (() =>
        {
          if (searchComboBoxAutoCompleteItems == null || searchComboBoxAutoCompleteItems.Length == 0)
            return;
          searchComboBoxAutoCompleteItems = ((IEnumerable<string>) searchComboBoxAutoCompleteItems).Where<string>((Func<string, bool>) (o => !this._childrenView.SearchComboBox.AutoCompleteCustomSource.Contains(o))).ToArray<string>();
          if (searchComboBoxAutoCompleteItems.Length == 0)
            return;
          this._childrenView.SearchComboBox.AutoCompleteCustomSource.AddRange(searchComboBoxAutoCompleteItems);
        }));
      }
      catch
      {
      }
    }));
  }

  private void SearchComboBox_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return && e.KeyCode != Keys.Return || this._childrenView.SearchComboBox.AutoCompleteCustomSource.Contains(this._childrenView.SearchComboBox.Text))
      return;
    this._childrenView.SearchComboBox.AutoCompleteCustomSource.Add(this._childrenView.SearchComboBox.Text);
  }

  private void SearchComboBox_TextChanged(object sender, EventArgs e)
  {
    this._timer.Stop();
    this._timer.Start();
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
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._timer = new Timer(this.components);
    this._timer.Tick += new EventHandler(this.Timer_Tick);
  }
}
