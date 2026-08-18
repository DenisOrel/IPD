// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.FormDesigner.ImbaseTypeFormLinkProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Imbase.Commands;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.FormDesigner;

internal class ImbaseTypeFormLinkProvider : 
  IFormDesignerFormLinksProvider,
  ICloneable,
  IFormDesignerFormLinksImages
{
  private string _providerName = string.Empty;
  private long _formID;
  private TreeNode _root;
  private bool _loaded;
  private Dictionary<string, TreeNode> _classifCache;
  private List<int> _objTypeCache = new List<int>();
  private List<ImbaseTypeFormLink> _deleted = new List<ImbaseTypeFormLink>();
  public static Guid sProviderGuid = new Guid("8CA3B59B-CAE1-427e-A6B3-B43A4C1C479B");

  private void InitializeData()
  {
    this._providerName = LocalizationHolder.rm.GetString("Imbase.Client_86");
    this._root = new TreeNode(this.ProviderName);
    this._root.ImageKey = this._root.SelectedImageKey = "Imbase_Root";
  }

  private void GetFormLinks(TreeNode node, List<ImbaseTypeFormLink> formLinks)
  {
    if (node == null || formLinks == null)
      return;
    if (node.Tag is ImbaseTypeFormLink)
      formLinks.Add(node.Tag as ImbaseTypeFormLink);
    foreach (TreeNode node1 in node.Nodes)
      this.GetFormLinks(node1, formLinks);
  }

  private TreeNode[] BuildTreeNodes(List<long> objectIDs, IUserSession session)
  {
    List<TreeNode> treeNodeList = new List<TreeNode>();
    if (objectIDs == null || objectIDs.Count == 0 || session == null || !(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
      return treeNodeList.ToArray();
    DataTable foldersForObjects = customService.GetFoldersForObjects(session.SessionGUID, objectIDs.ToArray(), (long[]) null);
    if (foldersForObjects == null || foldersForObjects.Rows.Count == 0)
      return treeNodeList.ToArray();
    Dictionary<long, long> stub = new Dictionary<long, long>();
    DataRow[] dataRowArray1 = foldersForObjects.Select(string.Format("{0} is NULL OR LEN({0}) = '0'", (object) "F_PATH"));
    if (dataRowArray1 != null && dataRowArray1.Length != 0)
    {
      IDBRelationCollection relationCollection = session.GetRelationCollection(session.IdentHelper.SortedRelationTypeID);
      DBRecordSetParams paramSet = new DBRecordSetParams(1);
      paramSet.Columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_PROJ_ID,
        (object) ObligatoryObjectAttributes.F_PART_ID
      };
      objectIDs.Sort();
      foreach (DataRow dataRow in dataRowArray1)
      {
        long int64_1 = Convert.ToInt64(dataRow["F_OBJECT_ID"]);
        if (objectIDs.BinarySearch(int64_1) >= 0 && !stub.ContainsKey(int64_1))
        {
          DataTable dataTable = relationCollection.EntersInVersion(paramSet, int64_1);
          if (dataTable != null && dataTable.Rows.Count != 0)
          {
            long int64_2 = Convert.ToInt64(dataTable.Rows[0][0]);
            stub[int64_2] = int64_1;
          }
        }
      }
    }
    if (stub.Count > 0)
    {
      foreach (KeyValuePair<long, long> keyValuePair in stub)
        objectIDs.Add(keyValuePair.Key);
      foldersForObjects = customService.GetFoldersForObjects(session.SessionGUID, objectIDs.ToArray(), (long[]) null);
    }
    bool flag = false;
    foreach (DataRow row in (InternalDataCollectionBase) foldersForObjects.Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      if (!this._objTypeCache.Contains(int32))
      {
        this._objTypeCache.Add(int32);
        flag = true;
      }
    }
    if (flag && this._root.TreeView != null && this._root.TreeView.ImageList != null)
      this.GetLinkImages((object) this._root.TreeView.ImageList);
    DataRow[] dataRowArray2 = foldersForObjects.Select($"LEN({"F_PATH"}) = '{2}'");
    if (dataRowArray2 != null && dataRowArray2.Length != 0)
    {
      foreach (DataRow dataRow in dataRowArray2)
      {
        long int64 = Convert.ToInt64(dataRow["F_OBJECT_ID"]);
        if (int64 != 0L)
        {
          TreeNode treeNode = this.BuildTreeNode(int64, objectIDs, foldersForObjects, session, stub);
          if (treeNode != null)
            treeNodeList.Add(treeNode);
        }
      }
    }
    return treeNodeList.ToArray();
  }

  private TreeNode BuildTreeNode(
    long objectID,
    List<long> objectIDs,
    DataTable sourceData,
    IUserSession session,
    Dictionary<long, long> stub)
  {
    if (objectID == 0L || sourceData == null)
      return (TreeNode) null;
    DataRow[] dataRowArray1 = sourceData.Select($"{"F_OBJECT_ID"} = '{objectID}'");
    if (dataRowArray1 == null || dataRowArray1.Length == 0)
      return (TreeNode) null;
    string str = dataRowArray1[0]["F_PATH"].ToString();
    int int32 = Convert.ToInt32(dataRowArray1[0]["F_OBJECT_TYPE"]);
    TreeNode treeNode = (TreeNode) null;
    bool flag = this._classifCache.TryGetValue(str, out treeNode);
    if (flag)
    {
      if (objectIDs.Contains(objectID))
      {
        if (!stub.ContainsKey(objectID))
        {
          ImbaseTypeFormLink imbaseTypeFormLink = new ImbaseTypeFormLink(objectID);
          treeNode.Tag = (object) imbaseTypeFormLink;
        }
        else
        {
          TreeNode node = this.BuildTreeNode(stub[objectID], new List<long>((IEnumerable<long>) new long[1]
          {
            stub[objectID]
          }), sourceData, session, new Dictionary<long, long>());
          if (node != null)
            treeNode.Nodes.Add(node);
        }
      }
    }
    else
    {
      if (objectIDs.Contains(objectID))
      {
        if (!stub.ContainsKey(objectID))
        {
          ImbaseTypeFormLink imbaseTypeFormLink = new ImbaseTypeFormLink(objectID);
          treeNode = new TreeNode(imbaseTypeFormLink.ToString());
          treeNode.Tag = (object) imbaseTypeFormLink;
        }
        else
        {
          treeNode = new TreeNode(session.GetObject(objectID).Caption);
          TreeNode node = this.BuildTreeNode(stub[objectID], new List<long>((IEnumerable<long>) new long[1]
          {
            stub[objectID]
          }), sourceData, session, new Dictionary<long, long>());
          if (node != null)
            treeNode.Nodes.Add(node);
        }
      }
      else
        treeNode = new TreeNode(session.GetObject(objectID).Caption);
      treeNode.SelectedImageKey = treeNode.ImageKey = $"ObjectType_{int32}";
      if (str.Length > 0)
        this._classifCache[str] = treeNode;
    }
    if (str.Length > 0)
    {
      string filterExpression = string.Format("{0} LIKE '{1}' AND LEN({0}) = '{2}'", (object) "F_PATH", (object) $"{SQLStringHelper.QuoteLikeString(str)}%", (object) (str.Length + 2));
      DataRow[] dataRowArray2 = sourceData.Select(filterExpression);
      if (dataRowArray2 != null && dataRowArray2.Length != 0)
      {
        foreach (DataRow dataRow in dataRowArray2)
        {
          TreeNode node = this.BuildTreeNode(Convert.ToInt64(dataRow["F_OBJECT_ID"]), objectIDs, sourceData, session, stub);
          if (node != null)
            treeNode.Nodes.Add(node);
        }
      }
    }
    return !flag ? treeNode : (TreeNode) null;
  }

  public ImbaseTypeFormLinkProvider() => this.InitializeData();

  public bool Loaded => this._loaded;

  public Guid ProviderGuid => ImbaseTypeFormLinkProvider.sProviderGuid;

  public string ProviderName => this._providerName;

  public object RootNode => (object) this._root;

  public List<FormLink> FormLinks
  {
    get
    {
      List<ImbaseTypeFormLink> imbaseTypeFormLinkList = new List<ImbaseTypeFormLink>();
      this.GetFormLinks(this._root, imbaseTypeFormLinkList);
      return new List<FormLink>((IEnumerable<FormLink>) (new ArrayList((ICollection) imbaseTypeFormLinkList).ToArray(typeof (FormLink)) as FormLink[]));
    }
  }

  public void Load(long formID)
  {
    if (this._loaded && this._formID == formID)
      return;
    this._formID = formID;
    this._classifCache = new Dictionary<string, TreeNode>();
    this._root.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DBRecordSetParams rParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.FormListAttributeTypeGuid), RelationalOperators.Equal, (object) formID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
        {
          AttributeSource = AttributeSourceTypes.Object,
          Content = ColumnContents.ID
        }
      }, new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      });
      DataTable dataTable = ImbaseHelper.SelectObjects(sessionKeeper.Session, rParams, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
      if (dataTable != null)
      {
        if (dataTable.Rows.Count > 0)
        {
          List<long> longList = new List<long>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            longList.Add(int64);
          }
          GenericListHelper.MakeUnique<long>(longList);
          if (longList.Count > 0)
            this._root.Nodes.AddRange(this.BuildTreeNodes(longList, sessionKeeper.Session));
        }
      }
    }
    this._loaded = true;
  }

  public void Add()
  {
    if (!this._loaded)
      return;
    ImbaseSelectFromTreeAnalyzer analyzer = new ImbaseSelectFromTreeAnalyzer(new List<int>((IEnumerable<int>) Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS));
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) analyzer, true);
    long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase.Client_87"), string.Empty, (IDescriptor) new ImbaseRootNodeDescriptor(), SelectionOptions.Default);
    if (numArray == null || numArray.Length == 0 || analyzer.TreeSelectedItems == null)
      return;
    List<long> objectIDs = new List<long>(analyzer.TreeSelectedItems.Count);
    for (int index = 0; index < analyzer.TreeSelectedItems.Count; ++index)
    {
      if (analyzer.TreeSelectedItems.GetItemData(index, typeof (IDBObjectID)) is IDBObjectID itemData)
        objectIDs.Add(itemData.Value);
    }
    if (objectIDs.Count == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._root.Nodes.AddRange(this.BuildTreeNodes(objectIDs, sessionKeeper.Session));
  }

  public void Delete(object node)
  {
    if (!this._loaded || !(node is TreeNode))
      return;
    TreeNode key = node as TreeNode;
    if (key.Nodes.Count > 0)
    {
      this._deleted.Add(key.Tag as ImbaseTypeFormLink);
      key.Tag = (object) null;
    }
    else
    {
      Dictionary<TreeNode, string> dictionary = new Dictionary<TreeNode, string>();
      foreach (KeyValuePair<string, TreeNode> keyValuePair in this._classifCache)
        dictionary[keyValuePair.Value] = keyValuePair.Key;
      while (!key.Equals((object) this._root))
      {
        TreeNode parent = key.Parent;
        if (!key.Nodes.Count.Equals(0))
          break;
        if (dictionary.ContainsKey(key))
          this._classifCache.Remove(dictionary[key]);
        if (key.Tag is ImbaseTypeFormLink)
          this._deleted.Add(key.Tag as ImbaseTypeFormLink);
        key.Remove();
        key = parent;
        if (key.Tag != null)
          break;
      }
    }
  }

  public void Clear()
  {
    if (!this._loaded)
      return;
    this.SearchLinkedNode(this._root);
    this._root.Nodes.Clear();
    if (this._classifCache == null)
      return;
    this._classifCache.Clear();
  }

  private void SearchLinkedNode(TreeNode node)
  {
    if (node == null)
      return;
    if (node.Tag is ImbaseTypeFormLink tag)
      this._deleted.Add(tag);
    if (node.Nodes.Count <= 0)
      return;
    foreach (TreeNode node1 in node.Nodes)
      this.SearchLinkedNode(node1);
  }

  public void Commit()
  {
    if (!this._loaded)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(Intermech.Imbase.Consts.FormListAttributeTypeGuid);
    if (attributeTypeId.Equals(0))
      return;
    List<ImbaseTypeFormLink> formLinks = new List<ImbaseTypeFormLink>();
    this.GetFormLinks(this._root, formLinks);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag1 = false;
      long num = Math.Abs(this._formID);
      foreach (ImbaseTypeFormLink imbaseTypeFormLink in this._deleted)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(imbaseTypeFormLink.ObjectID, false);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(attributeTypeId, false);
          if (dbAttribute != null)
          {
            for (int index = dbAttribute.ValuesCount - 1; index >= 0; --index)
            {
              dbAttribute.Index = index;
              if (dbAttribute.AsInteger == num)
              {
                if (index.Equals(0))
                  dbAttribute.Value = (object) DBNull.Value;
                else
                  dbAttribute.DeleteValue();
                flag1 = true;
              }
            }
          }
        }
      }
      foreach (ImbaseTypeFormLink imbaseTypeFormLink in formLinks)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(imbaseTypeFormLink.ObjectID, false);
        if (dbObject != null)
        {
          IDBAttribute dbAttribute = dbObject.Attributes.AddAttribute(attributeTypeId, false);
          if (dbAttribute != null)
          {
            bool flag2 = false;
            for (int index = 0; index < dbAttribute.ValuesCount; ++index)
            {
              dbAttribute.Index = index;
              if (dbAttribute.AsInteger == num)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
            {
              if (dbAttribute.ValuesCount.Equals(1) && dbAttribute.IsNull)
                dbAttribute.AsInteger = this._formID;
              else
                dbAttribute.AddValue((object) this._formID);
              flag1 = true;
            }
          }
        }
      }
      if (!flag1 || !(sessionKeeper.Session.GetCustomService(typeof (IFormDesignerService)) is IFormDesignerService customService))
        return;
      customService.ClearUserVersionCache();
    }
  }

  public void Rollback()
  {
    this._formID = 0L;
    this._loaded = false;
    this._classifCache = (Dictionary<string, TreeNode>) null;
    this._objTypeCache.Clear();
    this._deleted.Clear();
  }

  public object Clone()
  {
    Dictionary<TreeNode, string> mirror = new Dictionary<TreeNode, string>();
    if (this._loaded)
    {
      foreach (KeyValuePair<string, TreeNode> keyValuePair in this._classifCache)
        mirror[keyValuePair.Value] = keyValuePair.Key;
    }
    ImbaseTypeFormLinkProvider newProvider = new ImbaseTypeFormLinkProvider();
    newProvider._formID = this._formID;
    newProvider._loaded = this._loaded;
    if (newProvider._loaded)
      newProvider._classifCache = new Dictionary<string, TreeNode>();
    newProvider._objTypeCache = this._objTypeCache;
    newProvider._deleted = this._deleted;
    newProvider._root = this.Clone(this._root, mirror, newProvider);
    return (object) newProvider;
  }

  private TreeNode Clone(
    TreeNode oldNode,
    Dictionary<TreeNode, string> mirror,
    ImbaseTypeFormLinkProvider newProvider)
  {
    TreeNode treeNode = new TreeNode(oldNode.Text);
    treeNode.ImageKey = oldNode.ImageKey;
    treeNode.SelectedImageKey = oldNode.SelectedImageKey;
    treeNode.Tag = oldNode.Tag is ICloneable ? (oldNode.Tag as ICloneable).Clone() : oldNode.Tag;
    foreach (TreeNode node1 in oldNode.Nodes)
    {
      TreeNode node2 = this.Clone(node1, mirror, newProvider);
      treeNode.Nodes.Add(node2);
    }
    if (newProvider._loaded && mirror.ContainsKey(oldNode))
      newProvider._classifCache[mirror[oldNode]] = treeNode;
    return treeNode;
  }

  public void GetLinkImages(object imageList)
  {
    if (!(imageList is ImageList imageList1))
      return;
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service && !imageList1.Images.Keys.Contains("Imbase_Root"))
    {
      int index = service.ImageIndex("imgImbaseRoot");
      imageList1.Images.Add("Imbase_Root", service.ImageList.Images[index].GetThumbnailImage(imageList1.ImageSize.Width, imageList1.ImageSize.Height, (Image.GetThumbnailImageAbort) null, IntPtr.Zero));
    }
    ICategoryTypeIconService iconSrv = Statics.IconSrv;
    if (iconSrv == null)
      return;
    int num = iconSrv.IndexOf(4, 0);
    foreach (int type in this._objTypeCache)
    {
      string key = $"ObjectType_{type}";
      if (!imageList1.Images.Keys.Contains(key))
      {
        Icon icon = iconSrv.IndexOf(4, type).Equals(num) ? iconSrv.GetIconEx(4, type) : iconSrv.GetIcon(4, type);
        if (icon != null)
          imageList1.Images.Add(key, icon.ToBitmap().GetThumbnailImage(imageList1.ImageSize.Width, imageList1.ImageSize.Height, (Image.GetThumbnailImageAbort) null, IntPtr.Zero));
      }
    }
  }
}
