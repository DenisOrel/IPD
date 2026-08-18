// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentShowObject
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Document.Model;
using Intermech.Interfaces.Document;
using Intermech.Map;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading;

#nullable disable
namespace Intermech.Document.Client;

[Serializable]
public class ImDocumentShowObject : MapObject, IBackgroundPager, IPager, IMapRelative
{
  private PageData lastPage;
  private ImDocument document;
  private Metafile metafile;
  private float osZoomRatio;
  private Page currentPage;
  private bool aborted;

  public virtual Metafile Metafile
  {
    get => this.metafile;
    set => this.metafile = value;
  }

  public override void Dispose()
  {
    base.Dispose();
    this.currentPage = (Page) null;
    this.lastPage = (PageData) null;
    if (this.document != null)
    {
      this.document.PageUnlocked -= new PageUnlocked_EventHandler(this.document_PageUnlocked);
      this.document.BackgroundThreadsFinished -= new BackgroundThreadsFinished_EventHandler(this.BackgroundLoadFinished);
      this.document.Dispose();
      this.document = (ImDocument) null;
    }
    if (this.metafile == null)
      return;
    this.metafile.Dispose();
    this.metafile = (Metafile) null;
  }

  public ImDocumentShowObject(ImDocument document)
  {
    this.document = document;
    if (document != null)
    {
      document.PageUnlocked += new PageUnlocked_EventHandler(this.document_PageUnlocked);
      document.BackgroundThreadsFinished += new BackgroundThreadsFinished_EventHandler(this.BackgroundLoadFinished);
      if (document.Nodes == null)
        return;
      DocumentEditorPlugin.Instance.UpdateDocumentLinks((DocumentTreeNode) document, true, true, false, false, false);
      this.currentPage = (Page) null;
      int index = 0;
      for (int count = document.Nodes.Count; this.currentPage == null && index < count; ++index)
        this.currentPage = document.Nodes[index] as Page;
      if (this.currentPage != null)
      {
        if (this.metafile != null)
        {
          this.metafile.Dispose();
          this.metafile = (Metafile) null;
        }
        int num = 0;
        for (int millisecondsTimeout = 50; document.pageThreadStatus.StartDistributingPage == 0 && num * millisecondsTimeout / 100 < 30; ++num)
          Thread.Sleep(millisecondsTimeout);
        this.UpdateCache();
      }
      else
        this.Bounds = new RectangleF(0.0f, 0.0f, 297f, 210f);
    }
    this.Selectable = false;
  }

  private void BackgroundLoadFinished(object sender, BackgroundThreadsFinishedArgs e)
  {
    if (this.aborted)
      return;
    PageEventHandler newPageAdded = this.NewPageAdded;
    if (newPageAdded == null)
      return;
    newPageAdded((object) this, new PagerEventArgs((object) null));
  }

  private void document_PageUnlocked(object sender, PageUnlockedArgs e)
  {
    if (this.aborted || e.Page == null || e.IsDistributed)
      return;
    if (this.lastPage != null && this.lastPage.Index != e.Page.Index - 1)
      LogManager.AddLine($"ImDoc. Warning. Страница не добавлена {e.Page}, предыдущая страница {this.lastPage}", true);
    this.lastPage = e.Page;
    if (e.Page.OwnerDocument == null)
      return;
    e.Page.OwnerDocument.UpdatePageNumbers((PageData) null, e.Page.OwnerDocument.StartComplectPageNumber, false, false, false);
    this.OnNewPageAdded(new PagerEventArgs((object) e.Page));
  }

  public void OnNewPageAdded(PagerEventArgs e)
  {
    if ((e.Page is PageData page ? page.OwnerDocument : (ImDocumentData) null) == null)
      return;
    PageEventHandler newPageAdded = this.NewPageAdded;
    if (newPageAdded == null)
      return;
    newPageAdded((object) this, e);
  }

  public override RectangleF Bounds
  {
    get => base.Bounds;
    set
    {
      if (!(this.Bounds != value))
        return;
      base.Bounds = value;
      EventHandler refit = this.Refit;
      if (refit == null)
        return;
      refit((object) this, new EventArgs());
    }
  }

  private void UpdateCache()
  {
    Graphics graphics = Graphics.FromImage((Image) new Bitmap(1, 1));
    if (this.metafile != null)
    {
      this.metafile.Dispose();
      this.metafile = (Metafile) null;
    }
    SizeF size = new SizeF(210f, 297f);
    if (this.currentPage != null)
    {
      size = this.currentPage.Size;
      this.metafile = this.currentPage.CreatePageMetafile(new PointF(graphics.DpiX, graphics.DpiY));
      this.osZoomRatio = (float) this.metafile.Height / (graphics.DpiY / MapView.MillimetersPerInch) / size.Height;
    }
    this.Bounds = new RectangleF(PointF.Empty, size);
    EventHandler refresh = this.Refresh;
    if (refresh == null)
      return;
    refresh((object) this, new EventArgs());
  }

  public override void Paint(Graphics g, MapView view)
  {
    view.UseBuffer = false;
    if (this.metafile == null)
      return;
    if (Math.Round((double) this.osZoomRatio, 2) != 1.0)
    {
      GraphicsState gstate = g.Save();
      Graphics graphics = g;
      graphics.ScaleTransform(this.osZoomRatio, this.osZoomRatio);
      graphics.DrawImage((Image) this.metafile, this.Bounds);
      g.Restore(gstate);
    }
    else
      g.DrawImage((Image) this.metafile, this.Bounds);
  }

  public void First()
  {
    if (this.document == null)
      return;
    for (int index = 0; index < this.document.Nodes.Count; ++index)
    {
      if (this.document.Nodes[index] is Page node)
      {
        this.Current = (object) node;
        break;
      }
    }
  }

  public void Next()
  {
    if (this.document == null)
      return;
    int num = -1;
    if (this.currentPage != null)
      num = this.currentPage.Index;
    for (int index = num + 1; index < this.document.Nodes.Count; ++index)
    {
      if (this.document.Nodes[index] is Page node)
      {
        this.Current = (object) node;
        break;
      }
    }
  }

  public void Prev()
  {
    if (this.document == null)
      return;
    int num = -1;
    if (this.currentPage != null)
      num = this.currentPage.Index;
    for (int index = num - 1; index >= 0; --index)
    {
      if (this.document.Nodes[index] is Page node)
      {
        this.Current = (object) node;
        break;
      }
    }
  }

  public void Last()
  {
    if (this.document == null)
      return;
    for (int index = this.document.Nodes.Count - 1; index >= 0; --index)
    {
      if (this.document.Nodes[index] is Page node && !node.IsLockedForLoad)
      {
        this.Current = (object) node;
        break;
      }
    }
  }

  public object[] Pages
  {
    get
    {
      if (this.document != null)
      {
        ArrayList arrayList = new ArrayList(this.document.Nodes.Count);
        lock (this.document.Nodes)
        {
          for (int index = 0; index < this.document.Nodes.Count; ++index)
          {
            if (this.document.Nodes[index] is Page node)
            {
              if (!node.IsLockedForLoad)
              {
                node.WaitForLayout(200);
                arrayList.Add((object) node);
              }
              else
                break;
            }
          }
        }
        if (arrayList.Count > 0)
          return arrayList.ToArray();
      }
      return new object[0];
    }
  }

  public object Current
  {
    [DebuggerStepThrough] get => (object) this.currentPage;
    set
    {
      if (this.currentPage == value)
        return;
      this.currentPage = value as Page;
      if (this.metafile != null)
      {
        this.metafile.Dispose();
        this.metafile = (Metafile) null;
      }
      this.UpdateCache();
      EventHandler pageChanged = this.PageChanged;
      if (pageChanged == null)
        return;
      pageChanged((object) this, new EventArgs());
    }
  }

  public event EventHandler Refit;

  public event EventHandler Refresh;

  /// <summary>Событие перехода на другую страницу</summary>
  public event EventHandler PageChanged;

  /// <summary>получить по точке в документе найти ID элемента состовляющего документ</summary>
  /// <param name="point">по точке в документе </param>
  /// <returns>ID элемента в документе на который указывает точка</returns>
  public string GetId(PointF point)
  {
    if (this.currentPage == null)
      return (string) null;
    int layer = -1;
    VisualNode pageElementAtPoint = this.currentPage.FindPageElementAtPoint(point, ref layer, false);
    if (pageElementAtPoint == null)
      return this.currentPage.Id;
    return pageElementAtPoint is TextBoxElement textBoxElement ? $"{textBoxElement.FindFirstCell().Id}!!{textBoxElement.StartCharIndex}" : pageElementAtPoint.Id;
  }

  /// <summary>получить ID текущей страницы в документе</summary>
  /// <returns>ID текущей страницы в документе</returns>
  public string GetCurrentPageId()
  {
    return this.currentPage != null ? this.currentPage.Id : (string) null;
  }

  /// <summary>получить базовую точку элемента </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>базовая точка</returns>
  public PointF GetBasePoint(string id)
  {
    if (this.document != null && !string.IsNullOrWhiteSpace(id))
    {
      int result = -1;
      if (id.Contains("!!"))
      {
        string[] strArray = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries);
        id = strArray[0];
        if (strArray.Length > 1)
          int.TryParse(strArray[1], out result);
      }
      switch (this.document.FindNode(id))
      {
        case RectangleElement rectangleElement:
          if (rectangleElement is TextBoxElement textBoxElement)
          {
            while (textBoxElement.NextCell is TextBoxElement nextCell && nextCell.StartCharIndex < result)
              textBoxElement = nextCell;
            rectangleElement = (RectangleElement) textBoxElement;
          }
          return rectangleElement.Location;
        case Polyline polyline:
          PointF[] pathPoints = polyline.PathPoints;
          if (pathPoints != null && pathPoints.Length != 0)
            return pathPoints[0];
          break;
        case Page page:
          return page.Location;
      }
    }
    return PointF.Empty;
  }

  /// <summary> видим ли графику к указанному элементу </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент видим</returns>
  public bool GetVisible(string id) => this.GetPage(id) == this.currentPage;

  /// <summary> проверить сущетвование элемента в документе</summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент существует</returns>
  public bool CheckElementId(string id)
  {
    if (id.Contains("!!"))
      id = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries)[0];
    return this.document?.FindNode(id) != null;
  }

  /// <summary>
  /// Получение страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public object GetPage(string id)
  {
    if (this.document != null)
    {
      int result = -1;
      if (id.Contains("!!"))
      {
        string[] strArray = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries);
        id = strArray[0];
        if (strArray.Length > 1)
          int.TryParse(strArray[1], out result);
      }
      DocumentTreeNode documentTreeNode = this.document.FindNode(id);
      if (documentTreeNode != null)
      {
        if (documentTreeNode is Page page)
          return (object) page;
        if (documentTreeNode is TextBoxElement textBoxElement)
        {
          while (textBoxElement.NextCell is TextBoxElement nextCell && nextCell.StartCharIndex < result)
            textBoxElement = nextCell;
          documentTreeNode = (DocumentTreeNode) textBoxElement;
        }
        if (documentTreeNode is PageElementNode pageElementNode)
          return (object) pageElementNode.Page;
      }
    }
    return (object) null;
  }

  /// <summary>
  /// Получение ID страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns></returns>
  public object GetPageId(string id)
  {
    return !(this.GetPage(id) is Page page) ? (object) string.Empty : (object) page.Id;
  }

  public event PageEventHandler NewPageAdded;

  public void Abort()
  {
    try
    {
      this.aborted = true;
      if (this.document == null)
        return;
      this.document.AbortBackgroundThreads();
    }
    finally
    {
      this.aborted = false;
    }
  }
}
