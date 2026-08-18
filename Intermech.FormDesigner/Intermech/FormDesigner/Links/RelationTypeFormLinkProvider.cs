// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Links.RelationTypeFormLinkProvider
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.FormDesigner.Links;

/// <summary>
/// 
/// </summary>
internal class RelationTypeFormLinkProvider : 
  IFormDesignerFormLinksProvider,
  ICloneable,
  IFormDesignerFormLinksImages
{
  public static Guid stProviderGuid = new Guid("C72D1A91-195E-454f-8E1A-65971DEBE948");
  private long _formID;
  private TreeNode _root;
  private Dictionary<Guid, TreeNode> _nodesInRoot;

  /// <summary>Конструктор.</summary>
  public RelationTypeFormLinkProvider() => this._root = new TreeNode(this.ProviderName);

  /// <summary>
  /// Результат загрузки информации о типах связей, которым назначена форма.
  /// </summary>
  public bool Loaded { get; private set; }

  /// <summary>Глобальный идентификатор провайдера.</summary>
  public Guid ProviderGuid => RelationTypeFormLinkProvider.stProviderGuid;

  /// <summary>Наименование провайдера.</summary>
  public string ProviderName => LocalizationHolder.rm.GetString("FormDesigner_12");

  /// <summary>Корневой узел провайдера связей.</summary>
  public object RootNode => (object) this._root;

  /// <summary>
  /// Список данных о типах связей, которым назначена форма.
  /// </summary>
  public List<FormLink> FormLinks
  {
    get
    {
      return !this.Loaded ? new List<FormLink>() : this._nodesInRoot.Values.Select<TreeNode, FormLink>((Func<TreeNode, FormLink>) (x => x.Tag as FormLink)).ToList<FormLink>();
    }
  }

  /// <summary>Загрузка информации.</summary>
  /// <param name="formID">Идентификатор формы</param>
  public void Load(long formID)
  {
    if (this.Loaded && this._formID == formID)
      return;
    this._formID = formID;
    this._nodesInRoot = new Dictionary<Guid, TreeNode>();
    this._root.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetObjectActualCopy(formID, false) is IFormDBObject objectActualCopy)
      {
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(GuidHolder.GlobalRelGuid, false);
        if (attributeByGuid != null)
        {
          for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
          {
            attributeByGuid.Index = index;
            if (!attributeByGuid.IsNull)
            {
              string asString = attributeByGuid.AsString;
              if (GuidHelper.IsGuid(asString))
              {
                Guid guid = new Guid(asString);
                if (!(guid == Guid.Empty))
                {
                  RelationTypeFormLink relationTypeFormLink = new RelationTypeFormLink(guid);
                  this._root.Nodes.Add(relationTypeFormLink.Node);
                  this._nodesInRoot[guid] = relationTypeFormLink.Node;
                }
              }
            }
          }
        }
      }
    }
    this.Loaded = true;
  }

  /// <summary>Добавление формы типу связи.</summary>
  public void Add()
  {
    if (!this.Loaded)
      return;
    using (SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), this.ProviderName, typeof (RelationTypeFolder), true))
    {
      if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
        return;
      foreach (int id in selectorForm.IDList)
      {
        RelationTypeFormLink relationTypeFormLink = new RelationTypeFormLink(id);
        if (!this._nodesInRoot.ContainsKey(relationTypeFormLink.RelationTypeGuid))
        {
          this._root.Nodes.Add(relationTypeFormLink.Node);
          this._nodesInRoot[relationTypeFormLink.RelationTypeGuid] = relationTypeFormLink.Node;
        }
      }
    }
  }

  /// <summary>Удаление формы у типа связи.</summary>
  /// <param name="node">Выбранный в дереве узел с типом связи</param>
  public void Delete(object node)
  {
    if (!this.Loaded || !(node is TreeNode treeNode) || !(treeNode.Tag is RelationTypeFormLink tag) || !this._nodesInRoot.ContainsKey(tag.RelationTypeGuid))
      return;
    this._root.Nodes.Remove(this._nodesInRoot[tag.RelationTypeGuid]);
    this._nodesInRoot.Remove(tag.RelationTypeGuid);
  }

  /// <summary>
  /// Очистка информации о типах связей, которым назначена форма.
  /// </summary>
  public void Clear()
  {
    if (!this.Loaded)
      return;
    this._root.Nodes.Clear();
    this._nodesInRoot.Clear();
  }

  /// <summary>Сохранение информации.</summary>
  public void Commit()
  {
    if (this._formID == 0L || !this.Loaded)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetObjectActualCopy(this._formID, false) is IFormDBObject objectActualCopy))
        return;
      IDBAttribute dbAttribute = objectActualCopy.GetAttributeByGuid(GuidHolder.GlobalRelGuid, false);
      if (dbAttribute == null)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(GuidHolder.GlobalRelGuid);
        dbAttribute = objectActualCopy.Attributes.AddAttribute(attributeTypeId, false);
      }
      if (dbAttribute == null)
        return;
      List<Guid> list = this._nodesInRoot.Keys.ToList<Guid>();
      if (list.Count > 0)
        dbAttribute.Values = new ArrayList((ICollection) list.ToArray()).ToArray();
      else
        dbAttribute.ClearValues();
    }
  }

  /// <summary>Обнуление данных.</summary>
  public void Rollback()
  {
    this._formID = 0L;
    this._nodesInRoot = (Dictionary<Guid, TreeNode>) null;
    this.Loaded = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public object Clone()
  {
    RelationTypeFormLinkProvider formLinkProvider = new RelationTypeFormLinkProvider();
    formLinkProvider._formID = this._formID;
    formLinkProvider.Loaded = this.Loaded;
    if (this._nodesInRoot != null)
    {
      formLinkProvider._nodesInRoot = new Dictionary<Guid, TreeNode>();
      foreach (TreeNode node1 in this._root.Nodes)
      {
        TreeNode node2 = this.Clone(node1, formLinkProvider._nodesInRoot);
        formLinkProvider._root.Nodes.Add(node2);
      }
    }
    return (object) formLinkProvider;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="oldNode"></param>
  /// <param name="nodesInRoot"></param>
  /// <returns></returns>
  private TreeNode Clone(TreeNode oldNode, Dictionary<Guid, TreeNode> nodesInRoot)
  {
    TreeNode treeNode = oldNode.Clone() as TreeNode;
    treeNode.Tag = oldNode.Tag is ICloneable ? (oldNode.Tag as ICloneable).Clone() : oldNode.Tag;
    if (treeNode.Tag is RelationTypeFormLink tag)
    {
      nodesInRoot[tag.RelationTypeGuid] = treeNode;
      tag.Node = treeNode;
    }
    foreach (TreeNode node1 in oldNode.Nodes)
    {
      TreeNode node2 = this.Clone(node1, nodesInRoot);
      treeNode.Nodes.Add(node2);
    }
    return treeNode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageList"></param>
  public void GetLinkImages(object imageList)
  {
    if (!(imageList is ImageList imageList1))
      return;
    ICategoryTypeIconService iconSrv = Statics.IconSrv;
    if (iconSrv == null)
      return;
    int num1 = iconSrv.IndexOf(6, 0);
    Size imageSize;
    if (!imageList1.Images.ContainsKey("RelationType_Root"))
    {
      TreeNode root1 = this._root;
      TreeNode root2 = this._root;
      ImageList.ImageCollection images = imageList1.Images;
      Bitmap bitmap = iconSrv.GetIconEx(6, 0).ToBitmap();
      imageSize = imageList1.ImageSize;
      int width = imageSize.Width;
      imageSize = imageList1.ImageSize;
      int height = imageSize.Height;
      IntPtr zero = IntPtr.Zero;
      Image thumbnailImage = bitmap.GetThumbnailImage(width, height, (Image.GetThumbnailImageAbort) null, zero);
      int num2;
      int num3 = num2 = images.AddStrip(thumbnailImage);
      root2.ImageIndex = num2;
      int num4 = num3;
      root1.SelectedImageIndex = num4;
      imageList1.Images.SetKeyName(this._root.ImageIndex, "RelationType_Root");
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string empty = string.Empty;
      foreach (KeyValuePair<Guid, TreeNode> keyValuePair in this._nodesInRoot)
      {
        string str = $"RelationType_{Convert.ToString((object) keyValuePair.Key)}";
        if (!imageList1.Images.ContainsKey(str))
        {
          int relationTypeId = sessionKeeper.Session.IdentHelper.GetRelationTypeID(Convert.ToString((object) keyValuePair.Key));
          Icon icon = iconSrv.IndexOf(6, relationTypeId) != num1 ? iconSrv.GetIcon(6, relationTypeId) : iconSrv.GetIconEx(6, relationTypeId);
          if (icon != null)
          {
            TreeNode treeNode1 = keyValuePair.Value;
            TreeNode treeNode2 = keyValuePair.Value;
            ImageList.ImageCollection images = imageList1.Images;
            Bitmap bitmap = icon.ToBitmap();
            imageSize = imageList1.ImageSize;
            int width = imageSize.Width;
            imageSize = imageList1.ImageSize;
            int height = imageSize.Height;
            IntPtr zero = IntPtr.Zero;
            Image thumbnailImage = bitmap.GetThumbnailImage(width, height, (Image.GetThumbnailImageAbort) null, zero);
            int num5;
            int num6 = num5 = images.AddStrip(thumbnailImage);
            treeNode2.ImageIndex = num5;
            int num7 = num6;
            treeNode1.SelectedImageIndex = num7;
            imageList1.Images.SetKeyName(keyValuePair.Value.ImageIndex, str);
          }
        }
        else
          keyValuePair.Value.SelectedImageIndex = keyValuePair.Value.ImageIndex = imageList1.Images.IndexOfKey(str);
      }
    }
  }
}
