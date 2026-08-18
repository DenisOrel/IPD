// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PolylineCreator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Document.Model.Undo;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Вспомогательный класс,
/// обеспечивает интерфейс пользователя при создании полилинии</summary>
public class PolylineCreator : PageElementCreator
{
  private static Image image;
  /// <summary>Создание первой точки</summary>
  protected bool IsFirstPointSelected;
  /// <summary>Предыдущая выбранная точка</summary>
  private Point prevPoint = Point.Empty;
  /// <summary>Следующая выбранная точка</summary>
  protected Point nextPoint = Point.Empty;
  /// <summary>Новая полилиния</summary>
  protected Polyline newPolyline;
  protected Polyline newPolyline1;

  /// <summary>Иконка для кнопки статическая версия</summary>
  public new static Image Icon
  {
    get
    {
      if (PolylineCreator.image == null)
        PolylineCreator.image = PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.Polyline.png");
      return PolylineCreator.image;
    }
  }

  /// <summary>Иконка для кнопки</summary>
  public override Image Image
  {
    [DebuggerStepThrough] get
    {
      if (PolylineCreator.image == null)
        PolylineCreator.image = this.LoadImageFromResurces("Intermech.Document.Model.Resources.Polyline.png");
      return PolylineCreator.image;
    }
  }

  /// <summary>Имя элемента</summary>
  public override string Name
  {
    [DebuggerStepThrough] get => Polyline.ElementTypeName;
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseDown(MouseEventArgs e)
  {
  }

  /// <summary>Вызвает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseMove(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    if (this.ShowingContextMenu)
    {
      this.ShowingContextMenu = false;
    }
    else
    {
      Page pageAtPoint = this.PageControl.GetPageAtPoint(e.Location);
      if (this.HostPage == null || pageAtPoint != this.HostPage)
        return;
      this.nextPoint = new Point(e.X, e.Y);
      if (!this.IsFirstPointSelected)
        return;
      PointF world = this.HostPage.PageUI.ConvertPixelToWorld(this.nextPoint);
      if (this.newPolyline == null || this.newPolyline.PathPoints == null || this.newPolyline.PathPoints.Length == 0)
      {
        PointF startPoint = this.HostPage.PageUI.SnapPoint(this.HostPage.PageUI.ConvertPixelToWorld(this.prevPoint), (VisualNode) this.newPolyline);
        this.nextPoint = this.HostPage.PageUI.ConvertWorldToPixel((Control.ModifierKeys & Keys.Shift) != Keys.None ? this.HostPage.PageUI.SnapPointOrtho(world, startPoint, (VisualNode) this.newPolyline) : this.HostPage.PageUI.SnapPoint(world, (VisualNode) this.newPolyline));
      }
      else
      {
        PointF point;
        if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
        {
          point = this.HostPage.PageUI.SnapPoint(world, (VisualNode) this.newPolyline);
        }
        else
        {
          PointF[] pathPoints = this.newPolyline.PathPoints;
          PointF startPoint = pathPoints[pathPoints.Length - 1];
          point = this.HostPage.PageUI.SnapPointOrtho(world, startPoint, (VisualNode) this.newPolyline);
        }
        this.nextPoint = this.HostPage.PageUI.ConvertWorldToPixel(point);
      }
      if (this.DocumentControl.PageControl != null)
        this.nextPoint = this.HostPage.PageUI.SnapPixelToWorldGrid(this.nextPoint, (VisualNode) null);
      this.HostPage.RefreshUI();
    }
  }

  public override Page HostPage
  {
    get => base.HostPage;
    set
    {
      if (this.IsFirstPointSelected)
        return;
      base.HostPage = value;
    }
  }

  /// <summary>Вызвает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnMouseUp(MouseEventArgs e)
  {
    if (this.PageControl == null)
      return;
    Page pageAtPoint = this.PageControl.GetPageAtPoint(e.Location);
    if (this.HostPage == null || e.Button != MouseButtons.Left || pageAtPoint != this.HostPage || this.HostPage.PageUI == null)
      return;
    if (!this.IsFirstPointSelected)
    {
      this.prevPoint = this.HostPage.PageUI.SnapPixelToWorldGrid(new Point(e.X, e.Y), (VisualNode) this.newPolyline);
      this.IsFirstPointSelected = true;
    }
    else
    {
      this.nextPoint = new Point(e.X, e.Y);
      if (this.newPolyline == null)
        this.newPolyline = new Polyline();
      PointF world = this.HostPage.PageUI.ConvertPixelToWorld(this.nextPoint);
      if (this.newPolyline.PathPoints.Length == 0)
      {
        PointF pointF1 = this.HostPage.PageUI.SnapPoint(this.HostPage.PageUI.ConvertPixelToWorld(this.prevPoint), (VisualNode) this.newPolyline);
        PointF pointF2 = (Control.ModifierKeys & Keys.Shift) != Keys.None ? this.HostPage.PageUI.SnapPointOrtho(world, pointF1, (VisualNode) this.newPolyline) : this.HostPage.PageUI.SnapPoint(world, (VisualNode) this.newPolyline);
        this.nextPoint = this.HostPage.PageUI.ConvertWorldToPixel(pointF2);
        this.newPolyline.AddLine(pointF1, pointF2);
        this.newPolyline.SetParent((DocumentTreeNode) this.HostPage, true, false);
      }
      else
      {
        PointF pointF;
        if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
        {
          pointF = this.HostPage.PageUI.SnapPoint(world, (VisualNode) this.newPolyline);
        }
        else
        {
          PointF[] pathPoints = this.newPolyline.PathPoints;
          PointF startPoint = pathPoints[pathPoints.Length - 1];
          pointF = this.HostPage.PageUI.SnapPointOrtho(world, startPoint, (VisualNode) this.newPolyline);
        }
        this.nextPoint = this.HostPage.PageUI.ConvertWorldToPixel(pointF);
        this.newPolyline.AddLine(pointF);
      }
      this.prevPoint = this.nextPoint;
    }
  }

  /// <summary>Вызвает событие DoubleClick</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnDoubleClick(EventArgs e) => this.EndCreation();

  /// <summary>Вызвает событие Paint</summary>
  /// <param name="e">Аргументы события</param>
  public override void OnPaint(PaintEventArgs e)
  {
    if (!this.IsFirstPointSelected)
      return;
    e.Graphics.DrawLine(new Pen(Color.Black)
    {
      DashStyle = DashStyle.Dash
    }, this.prevPoint, this.nextPoint);
  }

  /// <summary>Отменить создание элемента</summary>
  public override void CancelCreation(object sender, EventArgs e)
  {
    if (this.newPolyline != null)
    {
      this.newPolyline.Remove(true, true);
      this.newPolyline = (Polyline) null;
      this.newPolyline1 = (Polyline) null;
    }
    base.CancelCreation(sender, e);
  }

  /// <summary>Получить контекстное меню режима создания элемента</summary>
  /// <param name="contextMenuItems">Пункты контекстного меню</param>
  public override void GetContextMenu(List<ToolbarItemBase> contextMenuItems)
  {
    base.GetContextMenu(contextMenuItems);
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Model_77"));
    menuButtonItem1.CommandName = LocalizationHolder.rm.GetString("Document.Model_78");
    menuButtonItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_79");
    menuButtonItem1.Click += new EventHandler(((PageElementCreator) this).CompleteCreation);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem1);
    MenuButtonItem menuButtonItem2 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Model_80"));
    menuButtonItem2.CommandName = "ClosePolyline";
    menuButtonItem2.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_81");
    menuButtonItem2.Enabled = this.newPolyline != null && this.newPolyline.PathPoints.Length > 2;
    menuButtonItem2.Click += new EventHandler(this.ClosePolyline);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem2);
  }

  /// <summary>Закончить создание элемента</summary>
  public override void CompleteCreation(object sender, EventArgs e) => this.EndCreation();

  /// <summary>Создание элемента завершено</summary>
  public void EndCreation()
  {
    if (this.newPolyline == null && this.newPolyline1 != null)
      this.newPolyline = this.newPolyline1;
    if (this.newPolyline != null && this.HostPage != null && this.HostPage.OwnerDocument != null && this.HostPage.OwnerDocument.UndoManager != null)
      this.HostPage.OwnerDocument.UndoManager.CreateUndo((IUndoAction) new UndoAddAction(this.HostPage.OwnerDocument.UndoManager, (DocumentTreeNode) this.HostPage, (DocumentTreeNode) this.newPolyline), true);
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl != null && documentControl.DocumentManager != null)
    {
      documentControl.DocumentManager.IsElementCreating = false;
      this.DocumentControl.Document.RefreshUI();
    }
    this.newPolyline = (Polyline) null;
    this.newPolyline1 = (Polyline) null;
  }

  /// <summary>Сбросить режим создания элемента</summary>
  public override void Reset()
  {
    this.IsFirstPointSelected = false;
    this.newPolyline1 = this.newPolyline;
    this.newPolyline = (Polyline) null;
    base.Reset();
  }

  /// <summary>Замкнуть полилинию и закончить создание</summary>
  public virtual void ClosePolyline(object sender, EventArgs e)
  {
    if (this.newPolyline == null && this.newPolyline1 != null)
      this.newPolyline = this.newPolyline1;
    if (this.newPolyline != null)
    {
      PointF[] pathPoints = this.newPolyline.PathPoints;
      if (pathPoints.Length > 2)
        this.newPolyline.AddLine(pathPoints[0]);
    }
    this.EndCreation();
  }

  public Point PrevPoint
  {
    get => this.prevPoint;
    set => this.prevPoint = value;
  }
}
