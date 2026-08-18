// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSPartViewsProvider
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary> Провайдер вьюшек для элемента спецификации </summary>
internal class AVSPartViewsProvider : IViewsProvider
{
  private static AVSPartViewsProvider instance;

  public AVSPartViewsProvider()
  {
    if (AVSPartViewsProvider.instance != null)
      return;
    AVSPartViewsProvider.instance = this;
  }

  public static AVSPartViewsProvider Instance => AVSPartViewsProvider.instance;

  /// <summary> Получение списка вьюшек для отображения </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    AVSWindow avsWindow = services.GetService(typeof (IAVSViewsService)) is IAVSViewsService service1 ? service1.AVSWindow : (AVSWindow) null;
    IViewState service2 = services.GetService<IViewState>(false);
    if (avsWindow == null || service2 == null || (service2.ViewState & ViewStateFlags.InParametersCard) == ViewStateFlags.None)
      return ViewsInfo.Empty;
    List<AVSRow> list = (List<AVSRow>) null;
    List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
    SpecificationSection specificationSection = (SpecificationSection) null;
    bool flag1 = false;
    IDBTypedObjectID dbTypedObjectId = (IDBTypedObjectID) null;
    if (items != null && items.Count > 0)
      dbTypedObjectId = items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    bool flag2 = !avsWindow.AVSDocument.IsSpecification && avsWindow.GetSelectedProducts().Count > 0;
    if (dbTypedObjectId != null && dbTypedObjectId.ObjectID == avsWindow.DocumentID && !flag2)
    {
      if (!avsWindow.ReadOnly)
        views.Add("AVSRowFormatPanel", new ViewInfo(0, typeof (RowPropsUserControl)));
      return views;
    }
    if (flag2)
      views.Add("ProductPropsUserControl", new ViewInfo(0, typeof (ProductPropsUserControl)));
    if (AVSPlugin.Instance.ActiveAVSWindow == avsWindow)
    {
      list = avsWindow.GetSelectedSpecRows(false);
      documentTreeNodeList = avsWindow.GetSelectedNoteRows();
      specificationSection = avsWindow.GetSelectedSection();
      if (specificationSection == null)
      {
        DocumentTreeNode[] selectedNodes = avsWindow.DocumentControl.GetSelectedNodes();
        flag1 = !((ICollection<DocumentTreeNode>) selectedNodes).IsEmpty<DocumentTreeNode>() && avsWindow.AVSDocument.GetChapter(selectedNodes[0], false) != null;
      }
    }
    if ((avsWindow.ReadOnly || avsWindow.BottomPanelType != AVSWindow.enumBottomPanelType.SelectedRowProperties ? 0 : (!list.IsEmpty<AVSRow>() || documentTreeNodeList != null && documentTreeNodeList.Count > 0 ? 1 : (specificationSection != null ? 1 : 0)) | (flag1 ? 1 : 0)) != 0)
      views.Add("AVSRowFormatPanel", new ViewInfo(0, typeof (RowPropsUserControl)));
    if (items != null && avsWindow.AVSDocument.IsSpecification && avsWindow.BottomPanelType == AVSWindow.enumBottomPanelType.SelectedRowProperties && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData1)
    {
      long objectId = itemData1.ObjectID;
      if (!MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).Contains(itemData1.ObjectType))
      {
        bool flag3 = true;
        for (int index = 0; index < items.Count; ++index)
        {
          if (!(items.GetItemData(index, typeof (IDBRelationID)) is IDBRelationID itemData))
          {
            flag3 = false;
            break;
          }
          if (itemData.PartID != objectId)
          {
            flag3 = false;
            break;
          }
        }
        if (flag3)
        {
          if (avsWindow.ViewMode == AVSViewMode.Page)
          {
            DocumentTreeNode[] selectedNodes = avsWindow.DocumentControl.GetSelectedNodes();
            if (selectedNodes != null && selectedNodes.Length != 0)
            {
              foreach (DocumentTreeNode docNode in selectedNodes)
              {
                if (AVSDocument.IsProductVariableDocNode(docNode))
                {
                  flag3 = false;
                  break;
                }
              }
            }
          }
          else
          {
            foreach (object obj in (IEnumerable) avsWindow.virtualTree.Selection)
            {
              if (obj is ProductVariableDataChapter)
              {
                flag3 = false;
                break;
              }
            }
          }
          if (flag3 && list != null && list.Count > 0)
            views.Add("AVS.ArticleWithDocView", new ViewInfo(0, typeof (ArticleWithDocView)));
        }
      }
    }
    return views;
  }
}
