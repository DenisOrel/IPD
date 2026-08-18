// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.QuickSearch.ImbaseQuickSearchProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Views;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces.QuickSearch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.QuickSearch;

public class ImbaseQuickSearchProvider : BaseQuickSearchProvider
{
  private readonly ImbaseQuickSearchHelper _searchHelper = new ImbaseQuickSearchHelper();
  private int _folderImgIndex;
  private int _linkImgIndex = 1;
  private ImageList _imgList;
  private NavigatorTreeNode _parentNode;

  public IList<long> CatalogIDs
  {
    get => this._searchHelper.CatalogIDs;
    set
    {
      IList<long> catalogIds = this._searchHelper.CatalogIDs;
      if (catalogIds != null)
      {
        if (value.ToHashSet<long>().SetEquals((IEnumerable<long>) catalogIds.ToHashSet<long>()))
          return;
        this._searchHelper.CatalogIDs = value;
      }
      else
        this._searchHelper.CatalogIDs = value;
    }
  }

  public ImbaseQuickSearchProvider() => this.InitImages();

  public override object ParentNode
  {
    get => (object) this._parentNode;
    set
    {
      this._parentNode = value as NavigatorTreeNode;
      this.CatalogIDs = (IList<long>) (this._parentNode.Handler as ImbaseRootNode).CatalogIDs;
    }
  }

  public override ImageList ImgList => this._imgList;

  public override bool NeedTimerForServerRequest => true;

  protected override List<QuickSearchResultItem> ClientSearch(string text)
  {
    return this.Search(new Func<string, int, List<ImbaseQuickSearchItem>>(this._searchHelper.SearchFolders), text, this.MaxElementCount, this._folderImgIndex);
  }

  protected override bool ClientSelectNode(QuickSearchResultItem resultNode)
  {
    bool flag = false;
    if (resultNode != null && resultNode.Item is ImbaseQuickSearchItem imbaseQuickSearchItem && this._parentNode != null && FindHelper.SearchNodeByNodeID(this._parentNode, imbaseQuickSearchItem.ObjectId) != null)
    {
      flag = true;
      if (imbaseQuickSearchItem.ObjectTypeId == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      {
        SelectedRecords.Clear();
        SelectedRecords.Add(imbaseQuickSearchItem.ObjectId, new long[1]
        {
          imbaseQuickSearchItem.RecordId
        });
      }
    }
    return flag;
  }

  protected override List<QuickSearchResultItem> ServerSearch(string text, int elementCount)
  {
    return this.Search(new Func<string, int, List<ImbaseQuickSearchItem>>(this._searchHelper.SearchRecords), text, elementCount, this._linkImgIndex);
  }

  private void InitImages()
  {
    this._imgList = new ImageList()
    {
      ColorDepth = ColorDepth.Depth24Bit
    };
    if (!(ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service))
      return;
    foreach (int type in new List<int>()
    {
      Intermech.Imbase.Consts.ImbaseFolderTypeID,
      Intermech.Imbase.Consts.ImbaseTableRefTypeID
    })
    {
      int index = service.IndexOf(4, type);
      if (index >= 0)
      {
        using (Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/))
        {
          using (Graphics g = Graphics.FromImage((Image) bitmap))
          {
            service.ImageList.Draw(g, 0, 0, index);
            this._imgList.Images.Add(type.ToString(), (Image) bitmap);
            this._imgList.Draw(g, 0, 0, 0);
          }
        }
      }
    }
  }

  private List<QuickSearchResultItem> Search(
    Func<string, int, List<ImbaseQuickSearchItem>> handler,
    string text,
    int elementCount,
    int imageIndex)
  {
    List<QuickSearchResultItem> searchResultItemList = (List<QuickSearchResultItem>) null;
    if (text.Length > 2)
    {
      List<ImbaseQuickSearchItem> source = handler(text, elementCount);
      if (source != null)
        searchResultItemList = source.Select<ImbaseQuickSearchItem, QuickSearchResultItem>((Func<ImbaseQuickSearchItem, QuickSearchResultItem>) (x => new QuickSearchResultItem(x.Caption, imageIndex, (object) x))).ToList<QuickSearchResultItem>();
    }
    return searchResultItemList;
  }
}
