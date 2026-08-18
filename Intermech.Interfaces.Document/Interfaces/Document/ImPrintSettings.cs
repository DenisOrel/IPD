// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ImPrintSettings
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

public class ImPrintSettings
{
  private int currentPrintPageIndex;
  private int printPageIndex;
  private List<PageData> pagesForPrint = new List<PageData>();
  private bool? fitToPagePrint;
  private List<int> selectedPrintPages = new List<int>();

  public void Reset()
  {
    this.pagesForPrint.Clear();
    this.currentPrintPageIndex = 0;
    this.printPageIndex = 0;
  }

  public PageData CurrentPrintPage
  {
    get
    {
      return this.CurrentPrintPageIndex < this.PagesForPrint.Count ? this.PagesForPrint[this.CurrentPrintPageIndex] : (PageData) null;
    }
  }

  public bool HasCurrentPage => this.CurrentPrintPageIndex < this.PagesForPrint.Count;

  public int PrintPageIndex
  {
    get => this.printPageIndex;
    set => this.printPageIndex = value;
  }

  public int CurrentPrintPageIndex
  {
    get => this.currentPrintPageIndex;
    set => this.currentPrintPageIndex = value;
  }

  public List<PageData> PagesForPrint
  {
    get => this.pagesForPrint;
    set => this.pagesForPrint = value;
  }

  public bool? FitToPagePrint
  {
    get => this.fitToPagePrint;
    set => this.fitToPagePrint = value;
  }

  public List<int> SelectedPrintPages
  {
    get => this.selectedPrintPages;
    set => this.selectedPrintPages = value;
  }
}
