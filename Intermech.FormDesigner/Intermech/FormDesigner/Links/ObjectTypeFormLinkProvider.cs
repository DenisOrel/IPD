// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Links.ObjectTypeFormLinkProvider
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
internal class ObjectTypeFormLinkProvider : 
  IFormDesignerFormLinksProvider,
  ICloneable,
  IFormDesignerFormLinksImages
{
  public static Guid stProviderGuid = new Guid("20C3314E-EC0E-40c6-A9BF-2581AC0DEC82");
  private long _formID;
  private TreeNode _root;
  private Dictionary<Guid, TreeNode> _nodesInRoot;

  /// <summary>Конструктор.</summary>
  public ObjectTypeFormLinkProvider() => this._root = new TreeNode(this.ProviderName);

  /// <summary>
  /// Результат загрузки информации о типах объектов, которым назначена форма.
  /// </summary>
  public bool Loaded { get; private set; }

  /// <summary>Глобальный идентификатор провайдера.</summary>
  public Guid ProviderGuid => ObjectTypeFormLinkProvider.stProviderGuid;

  /// <summary>Наименование провайдера.</summary>
  public string ProviderName => LocalizationHolder.rm.GetString("FormDesigner_10");

  /// <summary>Корневой узел провайдера объектов.</summary>
  public object RootNode => (object) this._root;

  /// <summary>
  /// Список данных о типах объектов, которым назначена форма.
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
        IDBAttribute attributeByGuid = objectActualCopy.GetAttributeByGuid(GuidHolder.GlobalObjGuid, false);
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
                  ObjectTypeFormLink objectTypeFormLink = new ObjectTypeFormLink(guid);
                  this._root.Nodes.Add(objectTypeFormLink.Node);
                  this._nodesInRoot[guid] = objectTypeFormLink.Node;
                }
              }
            }
          }
        }
      }
    }
    this.Loaded = true;
  }

  /// <summary>Добавление формы типу объектов.</summary>
  public void Add()
  {
    if (!this.Loaded)
      return;
    using (SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), this.ProviderName, typeof (ObjectTypeFolder), true))
    {
      if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
        return;
      foreach (int id in selectorForm.IDList)
      {
        ObjectTypeFormLink objectTypeFormLink = new ObjectTypeFormLink(id);
        if (!this._nodesInRoot.ContainsKey(objectTypeFormLink.ObjectTypeGuid))
        {
          this._root.Nodes.Add(objectTypeFormLink.Node);
          this._nodesInRoot[objectTypeFormLink.ObjectTypeGuid] = objectTypeFormLink.Node;
        }
      }
    }
  }

  /// <summary>Удаление формы у типа объектов.</summary>
  /// <param name="node">Выбранный в дереве узел с типом объектов</param>
  public void Delete(object node)
  {
    if (!this.Loaded || !(node is TreeNode treeNode) || !(treeNode.Tag is ObjectTypeFormLink tag) || !this._nodesInRoot.ContainsKey(tag.ObjectTypeGuid))
      return;
    this._root.Nodes.Remove(this._nodesInRoot[tag.ObjectTypeGuid]);
    this._nodesInRoot.Remove(tag.ObjectTypeGuid);
  }

  /// <summary>
  /// Очистка информации о типах объектов, которым назначена форма.
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
      IDBAttribute dbAttribute = objectActualCopy.GetAttributeByGuid(GuidHolder.GlobalObjGuid, false);
      if (dbAttribute == null)
      {
        int attributeTypeId = MetaDataHelper.GetAttributeTypeID(GuidHolder.GlobalObjGuid);
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
    ObjectTypeFormLinkProvider formLinkProvider = new ObjectTypeFormLinkProvider();
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
    if (treeNode.Tag is ObjectTypeFormLink tag)
    {
      nodesInRoot[tag.ObjectTypeGuid] = treeNode;
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
    int num1 = iconSrv.IndexOf(4, 0);
    Size imageSize;
    if (!imageList1.Images.ContainsKey("ObjectType_Root"))
    {
      TreeNode root1 = this._root;
      TreeNode root2 = this._root;
      ImageList.ImageCollection images = imageList1.Images;
      Bitmap bitmap = iconSrv.GetIconEx(4, 0).ToBitmap();
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
      imageList1.Images.SetKeyName(this._root.ImageIndex, "ObjectType_Root");
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string empty = string.Empty;
      foreach (KeyValuePair<Guid, TreeNode> keyValuePair in this._nodesInRoot)
      {
        string str = $"ObjectType_{Convert.ToString((object) keyValuePair.Key)}";
        if (!imageList1.Images.ContainsKey(str))
        {
          int objectTypeId = sessionKeeper.Session.IdentHelper.GetObjectTypeID(Convert.ToString((object) keyValuePair.Key));
          Icon icon = iconSrv.IndexOf(4, objectTypeId) != num1 ? iconSrv.GetIcon(4, objectTypeId) : iconSrv.GetIconEx(4, objectTypeId);
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
