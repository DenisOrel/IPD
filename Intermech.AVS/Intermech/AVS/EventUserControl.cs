// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.EventUserControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Панель свойств выбранных объектов или документов в редакторе спецификаций </summary>
public class EventUserControl : DockControl
{
  public static Guid DockGuid = new Guid("{B942EE32-1F77-4344-92C8-40DA92F641F5}");
  private AvsRowEventMessageViewer eventsHelper;
  private AVSWindow _avsWindow;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ListBox listBox1;
  private ToolStrip toolStrip1;
  private ToolStripButton bClear;

  /// <summary> Конструктор по-умолчанию </summary>
  public EventUserControl(AVSWindow avsWindow, AvsRowEventMessageViewer events)
  {
    this._avsWindow = avsWindow;
    this.InitializeComponent();
    this.Collapsible = false;
    this.HideOnClose = true;
    this.EventsHelper = events;
    this.Manager = avsWindow.DockManager;
    this.PersistState = false;
    this.Guid = EventUserControl.DockGuid;
  }

  /// <summary>Очистить ошибки</summary>
  public void Clear()
  {
    this.EventsHelper.Clear();
    this.UpdateRows();
  }

  internal void UpdateRows()
  {
    this.listBox1.Items.Clear();
    if (this.EventsHelper == null)
      return;
    foreach (KeyValuePair<AVSRow, List<AvsRowEventMessage>> keyValuePair in this.EventsHelper.Events)
    {
      for (int index = 0; index < keyValuePair.Value.Count; ++index)
      {
        string message = keyValuePair.Value[index].Message;
        string str = "";
        switch (keyValuePair.Value[index].EventType)
        {
          case AVSEventType.ChangeRow:
            str = "Изменили строку ";
            break;
          case AVSEventType.AddRow:
            str = "Добавили строку ";
            break;
          case AVSEventType.RemoveRow:
            str = "Удалили строку ";
            break;
        }
        this.listBox1.Items.Add((object) new EventUserControl.RowWrapper()
        {
          Row = keyValuePair.Key,
          Text = $"{str}: {keyValuePair.Key.ObjCaption}. {message}",
          Message = keyValuePair.Value[index]
        });
      }
    }
  }

  public AvsRowEventMessageViewer EventsHelper
  {
    get => this.eventsHelper;
    set
    {
      this.eventsHelper = value;
      this.UpdateRows();
    }
  }

  public AVSWindow AVSWindow => this._avsWindow;

  private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (!(this.listBox1.SelectedItem is EventUserControl.RowWrapper selectedItem))
      return;
    this.AVSWindow.Activate();
    this.AVSWindow.Activated();
    if (selectedItem.Message.AttrInfo != null)
      this.AVSWindow.AVSDocument.SetFocusTo(selectedItem.Row, selectedItem.Message.AttrInfo, selectedItem.Message.ProductIndex);
    else
      this.AVSWindow.RestoreSelection(new List<AVSRow>((IEnumerable<AVSRow>) new AVSRow[1]
      {
        selectedItem.Row
      }), selectedItem.Row);
  }

  private void bClear_Click(object sender, EventArgs e) => this.Clear();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    this.listBox1.Items.Clear();
    this._avsWindow = (AVSWindow) null;
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventUserControl));
    this.listBox1 = new ListBox();
    this.toolStrip1 = new ToolStrip();
    this.bClear = new ToolStripButton();
    this.toolStrip1.SuspendLayout();
    this.SuspendLayout();
    this.listBox1.Dock = DockStyle.Fill;
    this.listBox1.FormattingEnabled = true;
    this.listBox1.Location = new Point(0, 25);
    this.listBox1.Name = "listBox1";
    this.listBox1.Size = new Size(605, 232);
    this.listBox1.TabIndex = 20;
    this.listBox1.MouseDoubleClick += new MouseEventHandler(this.listBox1_MouseDoubleClick);
    this.toolStrip1.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.bClear
    });
    this.toolStrip1.Location = new Point(0, 0);
    this.toolStrip1.Name = "toolStrip1";
    this.toolStrip1.RenderMode = ToolStripRenderMode.System;
    this.toolStrip1.Size = new Size(605, 25);
    this.toolStrip1.TabIndex = 21;
    this.toolStrip1.Text = "toolStrip1";
    this.bClear.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.bClear.Image = (Image) componentResourceManager.GetObject("bClear.Image");
    this.bClear.ImageTransparentColor = Color.Magenta;
    this.bClear.Name = "bClear";
    this.bClear.Size = new Size(63 /*0x3F*/, 22);
    this.bClear.Text = "Очистить";
    this.bClear.Click += new EventHandler(this.bClear_Click);
    this.Controls.Add((Control) this.listBox1);
    this.Controls.Add((Control) this.toolStrip1);
    this.Name = nameof (EventUserControl);
    this.Size = new Size(605, 257);
    this.Text = "Список событий спецификации";
    this.toolStrip1.ResumeLayout(false);
    this.toolStrip1.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class RowWrapper
  {
    private AVSRow row;
    private string text;
    private AvsRowEventMessage message;

    public AVSRow Row
    {
      get => this.row;
      set => this.row = value;
    }

    public string Text
    {
      get => this.text;
      set => this.text = value;
    }

    public AvsRowEventMessage Message
    {
      get => this.message;
      set => this.message = value;
    }

    public override string ToString() => this.Text;
  }
}
