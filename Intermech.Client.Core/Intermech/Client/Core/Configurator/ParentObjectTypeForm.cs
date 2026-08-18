
// Type: Intermech.Client.Core.Configurator.ParentObjectTypeForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Map;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core.Configurator;

/// <summary>
/// 
/// </summary>
public class ParentObjectTypeForm : TabPageForm
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private MapView objectTypeMapView;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="aInstGuid"></param>
  public ParentObjectTypeForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.instGuid = aInstGuid;
  }

  /// <summary>Заполняем форму</summary>
  /// <param name="folder"></param>
  public override void FillForm(IFolder folder)
  {
    this.objectTypeMapView.Document.Clear();
    this._folder = folder as CustomFolder;
    List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID((int) this._folder.Id);
    this.objectTypeMapView.Document.StartTransaction();
    try
    {
      int x = 20;
      int y = 20;
      for (int index = objectTypeParentsId.Count - 1; index >= 0; --index)
      {
        ObjectTypeMapNode objectTypeMapNode = new ObjectTypeMapNode(objectTypeParentsId[index]);
        objectTypeMapNode.Position = (PointF) new Point(x, y);
        this.objectTypeMapView.Document.Add((MapObject) objectTypeMapNode);
        y += 40;
        x += 30;
      }
    }
    finally
    {
      this.objectTypeMapView.FinishTransaction("");
    }
  }

  /// <summary>двойной клик по выбранному типу объектов</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void objectTypeMapView_ObjectDoubleClicked(object sender, MapObjectEventArgs e)
  {
    if (e.MapObject == null || !(e.MapObject.ParentNode is ObjectTypeMapNode parentNode))
      return;
    EventsHolder.FireJumpToAttribute4CustomType(sender, this.instGuid, new EventsHolder.JumpToAttribute4CustomTypeArgs(this._folder.ListCategoryValue, parentNode.ObjectTypeID, 0));
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
    this.objectTypeMapView = new MapView();
    this.SuspendLayout();
    this.objectTypeMapView.AllowCopy = false;
    this.objectTypeMapView.AllowDelete = false;
    this.objectTypeMapView.AllowDragOut = false;
    this.objectTypeMapView.AllowEdit = false;
    this.objectTypeMapView.AllowInsert = false;
    this.objectTypeMapView.AllowLink = false;
    this.objectTypeMapView.AllowMove = false;
    this.objectTypeMapView.AllowReshape = false;
    this.objectTypeMapView.AllowResize = false;
    this.objectTypeMapView.BackColor = Color.White;
    this.objectTypeMapView.Border3DStyle = Border3DStyle.Etched;
    this.objectTypeMapView.BorderStyle = BorderStyle.Fixed3D;
    this.objectTypeMapView.Dock = DockStyle.Fill;
    this.objectTypeMapView.GridPenDashStyle = DashStyle.Solid;
    this.objectTypeMapView.GridSnapDrag = MapViewSnapStyle.None;
    this.objectTypeMapView.GridSnapResize = MapViewSnapStyle.None;
    this.objectTypeMapView.GridStyle = MapViewGridStyle.None;
    this.objectTypeMapView.InterpolationMode = InterpolationMode.High;
    this.objectTypeMapView.Location = new Point(0, 0);
    this.objectTypeMapView.Name = "objectTypeMapView";
    this.objectTypeMapView.SecondarySelectionColor = Color.Chartreuse;
    this.objectTypeMapView.ShowHorizontalScrollBar = MapViewScrollBarVisibility.IfNeeded;
    this.objectTypeMapView.ShowVerticalScrollBar = MapViewScrollBarVisibility.IfNeeded;
    this.objectTypeMapView.Size = new Size(1212, 559);
    this.objectTypeMapView.SmoothingMode = SmoothingMode.HighQuality;
    this.objectTypeMapView.TabIndex = 0;
    this.objectTypeMapView.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this.objectTypeMapView.ObjectDoubleClicked += new MapObjectEventHandler(this.objectTypeMapView_ObjectDoubleClicked);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.objectTypeMapView);
    this.Name = nameof (ParentObjectTypeForm);
    this.Size = new Size(1212, 559);
    this.ResumeLayout(false);
  }
}
