// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.RedlineDocControlWrapper
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Client.Core.Visualizers;
using Intermech.Document.Model;
using Intermech.Document.Model.UI;
using Intermech.Document.UI;
using Intermech.Interfaces.Document;
using Intermech.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

#nullable disable
namespace Intermech.Document.Client;

public class RedlineDocControlWrapper : IBackgroundPager, IPager, IMapRelative
{
  private DocumentControl _documentControl;
  private Page currentPage;
  private bool aborted;

  public RedlineDocControlWrapper(DocumentControl documentControl)
  {
    this._documentControl = documentControl;
    this._documentControl.ActivePageChanged += (ActivePageChanged_EventHandler) ((s, e) => this.Current = (object) this._documentControl.ActivePage);
  }

  public void First()
  {
    if (this._documentControl?.Document == null)
      return;
    int index = 0;
    while (true)
    {
      int num = index;
      int? count = this._documentControl?.Document.Nodes.Count;
      int valueOrDefault = count.GetValueOrDefault();
      if (num < valueOrDefault & count.HasValue)
      {
        if (!(this._documentControl?.Document.Nodes[index] is Page node))
          ++index;
        else
          break;
      }
      else
        goto label_7;
    }
    this.Current = (object) node;
    return;
label_7:;
  }

  public void Next()
  {
    if (this._documentControl?.Document == null)
      return;
    int num1 = -1;
    if (this.currentPage != null)
      num1 = this.currentPage.Index;
    int index = num1 + 1;
    while (true)
    {
      int num2 = index;
      int? count = this._documentControl?.Document.Nodes.Count;
      int valueOrDefault = count.GetValueOrDefault();
      if (num2 < valueOrDefault & count.HasValue)
      {
        if (!(this._documentControl?.Document.Nodes[index] is Page node))
          ++index;
        else
          break;
      }
      else
        goto label_9;
    }
    this.Current = (object) node;
    return;
label_9:;
  }

  public void Prev()
  {
    if (this._documentControl?.Document == null)
      return;
    int num = -1;
    if (this.currentPage != null)
      num = this.currentPage.Index;
    for (int index = num - 1; index >= 0; --index)
    {
      if (this._documentControl?.Document.Nodes[index] is Page node)
      {
        this.Current = (object) node;
        break;
      }
    }
  }

  public void Last()
  {
    if (this._documentControl?.Document == null)
      return;
    for (int index = this._documentControl.Document.Nodes.Count - 1; index >= 0; --index)
    {
      if (this._documentControl?.Document.Nodes[index] is Page node && !node.IsLockedForLoad)
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
      if (this._documentControl?.Document != null)
      {
        ArrayList arrayList = new ArrayList(this._documentControl.Document.Nodes.Count);
        lock (this._documentControl.Document.Nodes)
        {
          for (int index = 0; index < this._documentControl.Document.Nodes.Count; ++index)
          {
            if (this._documentControl.Document.Nodes[index] is Page node)
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
    [DebuggerStepThrough] get
    {
      return !this._documentControl.PageControl.OnePage ? (object) this._documentControl.PageControl.VisiblePageElementUIs.OfType<PageUI>().Select<PageUI, Page>((Func<PageUI, Page>) (pu => pu.Page)).ToList<Page>() : (object) this._documentControl.ActivePage;
    }
    set
    {
      if (this.currentPage == value)
        return;
      switch (value)
      {
        case null:
        case Page _:
          this.currentPage = value as Page;
          this._documentControl.ActivePage = value as Page;
          EventHandler pageChanged = this.PageChanged;
          if (pageChanged == null)
            break;
          pageChanged((object) this, new EventArgs());
          break;
      }
    }
  }

  public event EventHandler Refit;

  public event EventHandler Refresh;

  /// <summary>Событие перехода на другую страницу</summary>
  public event EventHandler PageChanged;

  /// <summary>получить по точке в документе ID элемента составляющего документ</summary>
  /// <param name="point">по точке в документе </param>
  /// <returns>ID элемента в документе на который указывает точка и стартовый индекс текста, разделенные '!!'</returns>
  public string GetId(PointF point)
  {
    if (this.VisiblePages.Length == 0 || this.VisiblePages[0] == null)
      return (string) null;
    Point point1 = new Point((int) point.X, (int) point.Y);
    PageElementUI elementUiAtPoint = this._documentControl.PageControl.GetPageElementUIAtPoint(point1, false);
    if (elementUiAtPoint?.Element != null)
      return elementUiAtPoint.Element is TextBoxElement element ? $"{element.FindFirstCell().Id}!!{element.StartCharIndex}" : elementUiAtPoint.Element.Id;
    List<PageElementUI> uiList = new List<PageElementUI>();
    foreach (Page visiblePage in this.VisiblePages)
    {
      uiList.Clear();
      visiblePage.PageUI.GetPageElementUIAtPoint(point1, uiList, false);
      if (uiList.Count > 0 && uiList[0].Element != null)
        return uiList[0].Element.Id;
    }
    return this.VisiblePages[0].Id;
  }

  private Page[] VisiblePages
  {
    get
    {
      Page[] visiblePages;
      if (this._documentControl.PageControl.OnePage)
        visiblePages = new Page[1]
        {
          this._documentControl.ActivePage
        };
      else
        visiblePages = this._documentControl.Document.Where<PageData>((Func<PageData, bool>) (x => x is Page page && page.PageUI != null)).OfType<Page>().ToArray<Page>();
      return visiblePages;
    }
  }

  private PointF Dpi
  {
    get
    {
      return this.VisiblePages.Length == 0 ? new PointF(96f, 96f) : this.VisiblePages[0].PageUI.DispayDpi;
    }
  }

  /// <summary>получить ID текущей страницы в документе</summary>
  /// <returns>ID текущей страницы в документе</returns>
  public string GetCurrentPageId() => this.Current is Page current ? current.Id : (string) null;

  /// <summary>получить базовую точку элемента </summary>
  /// <param name="id">ID первого текстового элемента в цепочке в документе и стартовый индекс позиции текста, разделенные '!!'</param>
  /// <returns>базовая точка</returns>
  public PointF GetBasePoint(string id)
  {
    PointF mm = PointF.Empty;
    if (string.IsNullOrWhiteSpace(id))
      return mm;
    int result = -1;
    if (id.Contains("!!"))
    {
      string[] strArray = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries);
      id = strArray[0];
      if (strArray.Length > 1)
        int.TryParse(strArray[1], out result);
    }
    switch (this._documentControl?.Document?.FindNode(id))
    {
      case RectangleElement rectangleElement:
        if (rectangleElement is TextBoxElement textBoxElement)
        {
          while (textBoxElement.NextCell is TextBoxElement nextCell && nextCell.StartCharIndex < result)
            textBoxElement = nextCell;
          rectangleElement = (RectangleElement) textBoxElement;
        }
        ref PointF local = ref mm;
        PointF pointF = rectangleElement.Location;
        double x1 = (double) pointF.X;
        PageData page1 = rectangleElement.Page;
        double x2;
        if (page1 == null)
        {
          pointF = PointF.Empty;
          x2 = (double) pointF.X;
        }
        else
        {
          pointF = page1.Location;
          x2 = (double) pointF.X;
        }
        double x3 = x1 + x2;
        pointF = rectangleElement.Location;
        double y1 = (double) pointF.Y;
        PageData page2 = rectangleElement.Page;
        double y2;
        if (page2 == null)
        {
          pointF = PointF.Empty;
          y2 = (double) pointF.Y;
        }
        else
        {
          pointF = page2.Location;
          y2 = (double) pointF.Y;
        }
        double y3 = y1 + y2;
        local = new PointF((float) x3, (float) y3);
        mm = UnitsConverter.MmToPixelsF(mm, this.Dpi);
        break;
      case Polyline polyline:
        PointF[] pathPoints = polyline.PathPoints;
        if (pathPoints != null && pathPoints.Length != 0)
          return pathPoints[0];
        break;
      case Page page3:
        mm = UnitsConverter.MmToPixelsF(page3.Location, this.Dpi);
        break;
    }
    return mm;
  }

  /// <summary> видим ли графику к указанному элементу </summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент видим</returns>
  public bool GetVisible(string id)
  {
    int result = -1;
    if (id.Contains("!!"))
    {
      string[] strArray = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries);
      id = strArray[0];
      if (strArray.Length > 1)
        int.TryParse(strArray[1], out result);
    }
    DocumentTreeNode documentTreeNode = this._documentControl.Document?.FindNode(id);
    if (documentTreeNode is TextBoxElement textBoxElement)
    {
      while (textBoxElement.NextCell is TextBoxElement nextCell && nextCell.StartCharIndex < result)
        textBoxElement = nextCell;
      documentTreeNode = (DocumentTreeNode) textBoxElement;
    }
    if (documentTreeNode is PageElementNode node && this._documentControl.PageControl.IsNodeVisible(node))
      return true;
    return documentTreeNode is Page page && page.Visible;
  }

  /// <summary> проверить сущетвование элемента в документе</summary>
  /// <param name="id">ID элемента в документе</param>
  /// <returns>true, если элемент существует</returns>
  public bool CheckElementId(string id)
  {
    if (id.Contains("!!"))
      id = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries)[0];
    return this._documentControl.Document?.FindNode(id) != null;
  }

  /// <summary>
  /// Получение страницы в документе для указанного элемента
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public object GetPage(string id)
  {
    int result = -1;
    if (id.Contains("!!"))
    {
      string[] strArray = id.Split(new string[1]{ "!!" }, StringSplitOptions.RemoveEmptyEntries);
      id = strArray[0];
      if (strArray.Length > 1)
        int.TryParse(strArray[1], out result);
    }
    DocumentTreeNode documentTreeNode = this._documentControl?.Document?.FindNode(id);
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
      this._documentControl?.Document?.AbortBackgroundThreads();
    }
    finally
    {
      this.aborted = false;
    }
  }
}
