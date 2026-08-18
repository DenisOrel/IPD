// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PageElementCreator
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Document.Model;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Абстрактный впомогательный класс для обеспечения интерфейса пользователя
/// при создании новых элементов страницы</summary>
public abstract class PageElementCreator
{
  /// <summary>Загрузить рисунок из ресурсов статическая версия</summary>
  /// <param name="resourceName">Имя ресурса</param>
  /// <returns>Рисунок</returns>
  public static Image LoadImageFromResurcesStatic(string resourceName)
  {
    Bitmap bitmap = (Bitmap) null;
    Stream manifestResourceStream = typeof (PageElementCreator).Assembly.GetManifestResourceStream(resourceName);
    if (manifestResourceStream != null)
    {
      bitmap = new Bitmap(manifestResourceStream);
      bitmap.MakeTransparent();
    }
    return (Image) bitmap;
  }

  /// <summary>Загрузить рисунок из ресурсов</summary>
  /// <param name="resourceName">Имя ресурса</param>
  /// <returns>Рисунок</returns>
  protected Image LoadImageFromResurces(string resourceName)
  {
    Bitmap bitmap = (Bitmap) null;
    Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream(resourceName);
    if (manifestResourceStream != null)
    {
      bitmap = new Bitmap(manifestResourceStream);
      if (bitmap.RawFormat.Equals((object) ImageFormat.Bmp))
        bitmap.MakeTransparent();
    }
    return (Image) bitmap;
  }

  /// <summary>Иконка для кнопки, статическая версия</summary>
  public static Image Icon
  {
    [DebuggerStepThrough] get => (Image) null;
  }

  /// <summary>Иконка для кнопки</summary>
  public virtual Image Image
  {
    [DebuggerStepThrough] get => (Image) null;
  }

  /// <summary>Курсор</summary>
  public virtual Cursor Cursor { [DebuggerStepThrough] get; set; } = Cursors.Cross;

  /// <summary>Имя элемента</summary>
  public abstract string Name { get; }

  /// <summary>Страница на которой будет размещаться элемент</summary>
  public virtual Page HostPage { [DebuggerStepThrough] get; set; }

  /// <summary>В данный момент показывается контекстное меню</summary>
  public bool ShowingContextMenu { get; set; }

  /// <summary>Контрол страницы</summary>
  public PageControl PageControl
  {
    [DebuggerStepThrough] get => this.HostPage?.PageControl;
  }

  /// <summary>Контрол документа</summary>
  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.PageControl?.DocumentControl;
  }

  /// <summary>Сбросить режим создания элемента</summary>
  public virtual void Reset()
  {
    if (this.HostPage == null || this.DocumentControl == null)
      return;
    this.DocumentControl.PageControl.IsElementCreating = false;
    this.DocumentControl.Document.RefreshUI();
  }

  /// <summary>Вызвает событие Paint</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnPaint(PaintEventArgs e)
  {
  }

  /// <summary>Вызвает событие MouseDown</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnMouseDown(MouseEventArgs e)
  {
  }

  /// <summary>Вызвает событие MouseMove</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnMouseMove(MouseEventArgs e)
  {
  }

  /// <summary>Вызвает событие MouseUp</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnMouseUp(MouseEventArgs e)
  {
  }

  /// <summary>Вызвает событие Click</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnClick(EventArgs e)
  {
  }

  /// <summary>Вызвает событие DoubleClick</summary>
  /// <param name="e">Аргументы события</param>
  public virtual void OnDoubleClick(EventArgs e)
  {
  }

  /// <summary>Получить контекстное меню режима создания элемента</summary>
  /// <param name="contextMenuItems">Пункты контекстного меню</param>
  public virtual void GetContextMenu(List<ToolbarItemBase> contextMenuItems)
  {
    MenuButtonItem menuButtonItem1 = new MenuButtonItem(LocalizationHolder.rm.GetString("Document.Model_75"));
    menuButtonItem1.CommandName = "CancelElementCreation";
    menuButtonItem1.ToolTipText = LocalizationHolder.rm.GetString("Document.Model_76");
    MenuButtonItem menuButtonItem2 = menuButtonItem1;
    menuButtonItem2.Click += new EventHandler(this.CancelCreation);
    contextMenuItems.Add((ToolbarItemBase) menuButtonItem2);
  }

  /// <summary>Отменить создание элемента</summary>
  public virtual void CompleteCreation(object sender, EventArgs e)
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl == null || documentControl.DocumentManager == null)
      return;
    documentControl.DocumentManager.IsElementCreating = false;
  }

  /// <summary>Отменить создание элемента</summary>
  public virtual void CancelCreation(object sender, EventArgs e)
  {
    DocumentControl documentControl = this.DocumentControl;
    if (documentControl?.DocumentManager == null)
      return;
    documentControl.DocumentManager.IsElementCreating = false;
  }
}
