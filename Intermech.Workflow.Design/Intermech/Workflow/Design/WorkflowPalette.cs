// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.WorkflowPalette
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for WorkflowPalette.</summary>
public class WorkflowPalette : MapPalette
{
  private System.Windows.Forms.Button _scrollUpButton;
  private System.Windows.Forms.Button _scrollDownButton;
  private Timer _scrollTimer;
  private static Dictionary<ScrollBarArrowButtonState, Image> _scrollbarImages = new Dictionary<ScrollBarArrowButtonState, Image>();
  private int _lastScrollIncrement;
  private const int ScrollButtonSpace = 5;

  private Image GetScrollbarImage(ScrollBarArrowButtonState state)
  {
    Image image = (Image) null;
    if (!WorkflowPalette._scrollbarImages.TryGetValue(state, out image))
    {
      image = (Image) new Bitmap(SystemInformation.VerticalScrollBarWidth, SystemInformation.VerticalScrollBarArrowHeight);
      using (Graphics g = Graphics.FromImage(image))
        ScrollBarRendererEx.DrawArrowButton(g, new Rectangle(0, 0, image.Width, image.Height), state);
      WorkflowPalette._scrollbarImages.Add(state, image);
    }
    return image;
  }

  private System.Windows.Forms.Button MakeScrollButton(int increment)
  {
    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
    button.Text = "";
    button.Width = 25;
    button.Parent = (Control) this;
    button.FlatStyle = FlatStyle.Flat;
    button.FlatAppearance.BorderSize = 0;
    button.MouseDown += new MouseEventHandler(this.ScrollButton_Down);
    button.MouseUp += new MouseEventHandler(this.ScrollButton_MouseUp);
    button.Tag = (object) increment;
    if (increment > 0)
      button.Image = this.GetScrollbarImage(ScrollBarArrowButtonState.DownNormal);
    else
      button.Image = this.GetScrollbarImage(ScrollBarArrowButtonState.UpNormal);
    button.BackColor = Color.Transparent;
    return button;
  }

  public void UpdateScrollButtons()
  {
    PointF docPosition = this.DocPosition;
    int num1 = this._scrollUpButton.Visible ? 1 : 0;
    bool visible = this._scrollDownButton.Visible;
    this._scrollUpButton.Visible = (double) docPosition.Y > 0.0;
    System.Windows.Forms.Button scrollDownButton = this._scrollDownButton;
    SizeF sizeF = this.DocumentSize;
    double height = (double) sizeF.Height;
    sizeF = this.DocExtentSize;
    double num2 = (double) sizeF.Height + (double) docPosition.Y;
    int num3 = height > num2 ? 1 : 0;
    scrollDownButton.Visible = num3 != 0;
    int num4 = this._scrollUpButton.Visible ? 1 : 0;
    if (num1 == num4 && visible == this._scrollDownButton.Visible)
      return;
    this._scrollTimer.Stop();
  }

  public WorkflowPalette()
  {
    this._scrollTimer = new Timer();
    this._scrollTimer.Interval = 50;
    this._scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
    this._scrollUpButton = this.MakeScrollButton(-1);
    this._scrollDownButton = this.MakeScrollButton(1);
    this.PropertyChanged += new PropertyChangedEventHandler(this.WorkflowPalette_PropertyChanged);
  }

  private void WorkflowPalette_PropertyChanged(object sender, PropertyChangedEventArgs e)
  {
    if (!(e.PropertyName == "DocPosition"))
      return;
    this.UpdateScrollButtons();
  }

  private void ScrollTimer_Tick(object sender, EventArgs e)
  {
    this.ScrollLine(0.0f, (float) this._lastScrollIncrement);
  }

  private void ScrollButton_Down(object sender, MouseEventArgs e)
  {
    this._lastScrollIncrement = Convert.ToInt32(((Control) sender).Tag);
    this.ScrollLine(0.0f, (float) this._lastScrollIncrement);
    this._scrollTimer.Start();
  }

  private void ScrollButton_MouseUp(object sender, MouseEventArgs e) => this._scrollTimer.Stop();

  public int ClientWidth
  {
    get
    {
      int width = this.Width;
      if (this.VerticalScrollBar.Visible)
        width -= this.VerticalScrollBar.Width;
      return width;
    }
  }

  public int ClientHeight
  {
    get
    {
      int height = this.Height;
      if (this.HorizontalScrollBar.Visible)
        height -= this.HorizontalScrollBar.Height;
      return height;
    }
  }

  protected override void OnSizeChanged(EventArgs evt) => base.OnSizeChanged(evt);

  public override void LayoutItems()
  {
    if (!this.AutomaticLayout)
      return;
    bool flag = this.Orientation == Orientation.Vertical;
    if (flag)
    {
      this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
      this.ShowVerticalScrollBar = MapViewScrollBarVisibility.IfNeeded;
    }
    else
    {
      this.ShowHorizontalScrollBar = MapViewScrollBarVisibility.IfNeeded;
      this.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
    }
    this.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
    ICollection collection = (ICollection) this.Document;
    if (this.Sorting != SortOrder.None && this.Comparer != null)
    {
      MapObject[] mapObjectArray = this.Document.CopyArray();
      Array.Sort((Array) mapObjectArray, 0, mapObjectArray.Length, this.Comparer);
      if (this.Sorting == SortOrder.Descending)
        Array.Reverse((Array) mapObjectArray, 0, mapObjectArray.Length);
      collection = (ICollection) mapObjectArray;
    }
    float x = (float) this.ClientWidth / 2f;
    float y = 5f;
    if (!flag)
    {
      x = 15f;
      y = -1f;
    }
    foreach (MapObject mapObject in (IEnumerable) collection)
    {
      if (mapObject.Visible)
      {
        if (flag)
        {
          mapObject.Position = new PointF(x - mapObject.Width / 2f, y);
          y += 5f + mapObject.Height;
        }
        else
        {
          if ((double) y == -1.0)
          {
            y = (float) this.ClientHeight / 2f;
            if (mapObject is WorkflowNode workflowNode)
              y = (float) ((double) y - (double) workflowNode.Icon.Height / 2.0 - 15.0);
          }
          mapObject.Position = new PointF(x, y);
          x += 5f + mapObject.Width;
        }
      }
    }
    RectangleF documentBounds = this.ComputeDocumentBounds();
    this.Document.Size = new SizeF(documentBounds.Width, documentBounds.Height);
    this.Document.TopLeft = new PointF(documentBounds.X, documentBounds.Y);
    this._scrollUpButton.Left = this.ClientWidth - this._scrollUpButton.Width - 5;
    this._scrollUpButton.Top = 5;
    this._scrollDownButton.Left = this._scrollUpButton.Left;
    this._scrollDownButton.Top = this.Height - this._scrollDownButton.Height - 5;
    this.UpdateScrollButtons();
  }

  public void Fill(long ProcessID)
  {
    this.Document.StartTransaction();
    for (int index = 0; index < ActivityInfos.Items.Count; ++index)
    {
      Intermech.Workflow.ActivityInfo activityInfo = ActivityInfos.Items[index];
      if (activityInfo.Kind != ActivityKind.LCStep)
      {
        WorkflowNode workflowNode = new WorkflowNode(ProcessID, activityInfo.Type);
        workflowNode.Initialize(ClientActivityInfos.ImageList, activityInfo.ImageIndex, activityInfo.ObjectName);
        workflowNode.ToolTipText = activityInfo.TypeName;
        this.Document.Add((MapObject) workflowNode);
      }
    }
    this.Document.FinishTransaction("pallette fill");
  }

  public bool ShowStart
  {
    get => this.Document.FindNode(ActivityKind.Start) != null;
    set
    {
      WorkflowNode node = this.Document.FindNode(ActivityKind.Start);
      if (node == null)
        return;
      node.Visible = value;
      this.LayoutItems();
    }
  }
}
