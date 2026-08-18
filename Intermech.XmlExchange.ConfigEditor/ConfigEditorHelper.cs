// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ConfigEditorHelper
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.XmlExchange;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class ConfigEditorHelper
{
  private static ConfigEditorHelper _helper;
  private static ICategoryTypeIconService _categoryIcons;
  private INamedImageList _namedImageList;
  private Dictionary<string, string> _rulesDictionary;
  private Dictionary<Guid, int> _objTypeInBase = new Dictionary<Guid, int>();
  private Dictionary<Guid, int> _atrTypeInBase = new Dictionary<Guid, int>();
  private Dictionary<Guid, int> _relTypeInBase = new Dictionary<Guid, int>();
  private ConfigEditorModeView _modeView;

  private ConfigEditorHelper()
  {
    ConfigEditorHelper._categoryIcons = ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._namedImageList = ApplicationServices.Container.GetService(typeof (INamedImageList)) as INamedImageList;
    this._modeView = ConfigEditorModeView.GetModeView();
    this.InitializeTypesList();
  }

  private void InitializeTypesList()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return;
      this._objTypeInBase.Clear();
      this._objTypeInBase.Add(MetaDataHelper.GetObjectTypeGuid(-1), -1);
      DataTable dataTable1 = session.GetObjectTypeCollection(-2).Select("");
      if (dataTable1.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
          this._objTypeInBase.Add(new Guid(row["F_GUID"].ToString()), Convert.ToInt32(row["F_OBJECT_TYPE"]));
      }
      this._atrTypeInBase.Clear();
      DataTable dataTable2 = session.GetAttributeTypeCollection(-1).Select("");
      if (dataTable2.Rows.Count > 0)
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable2.Rows)
          this._atrTypeInBase.Add(new Guid(row["F_GUID"].ToString()), Convert.ToInt32(row["F_ATTRIBUTE_ID"]));
      }
      this._relTypeInBase.Clear();
      DataTable dataTable3 = session.GetRelationTypeCollection().Select("");
      if (dataTable3.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable3.Rows)
        this._relTypeInBase.Add(new Guid(row["F_GUID"].ToString()), Convert.ToInt32(row["F_RELATION_TYPE"]));
    }
  }

  internal static ConfigEditorHelper GetHelper()
  {
    if (ConfigEditorHelper._helper != null)
      return ConfigEditorHelper._helper;
    ConfigEditorHelper._helper = new ConfigEditorHelper();
    return ConfigEditorHelper._helper;
  }

  internal bool ObjTypeInBase(Guid objTypeGuid) => this._objTypeInBase.ContainsKey(objTypeGuid);

  internal bool ObjTypeInBase(Guid objTypeGuid, int objTypeId)
  {
    int num;
    return this._objTypeInBase.TryGetValue(objTypeGuid, out num) && objTypeId == num;
  }

  internal bool AtrTypeInBase(Guid atrTypeGuid) => this._atrTypeInBase.ContainsKey(atrTypeGuid);

  internal bool AtrTypeInBase(Guid atrTypeGuid, int atrTypeId)
  {
    int num;
    return this._atrTypeInBase.TryGetValue(atrTypeGuid, out num) && atrTypeId == num;
  }

  internal bool RelTypeInBase(Guid relTypeGuid) => this._relTypeInBase.ContainsKey(relTypeGuid);

  internal bool RelTypeInBase(Guid relTypeGuid, int relTypeId)
  {
    int num;
    return this._relTypeInBase.TryGetValue(relTypeGuid, out num) && relTypeId == num;
  }

  internal Dictionary<string, string> VersionRulesDictionary
  {
    get
    {
      if (this._rulesDictionary == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          if (session != null)
          {
            DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad001b3-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[2]
            {
              new ColumnDescriptor((object) ObligatoryObjectAttributes.F_GUID, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0),
              new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.String, ColumnNameMapping.Guid, SortOrders.NONE, 0)
            }));
            if (dataTable.Rows.Count > 0)
            {
              this._rulesDictionary = new Dictionary<string, string>();
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                this._rulesDictionary.Add(row[0].ToString(), row[1].ToString());
            }
          }
        }
      }
      return this._rulesDictionary;
    }
  }

  internal IMSObjectType DiagSelectObjectType(List<int> objtypes)
  {
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Любой тип объекта", typeof (ObjectTypeFolder), false);
    selectorForm.AllowRootSelect = true;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return (IMSObjectType) null;
    int id = (int) selectorForm.IDList[0];
    if (id == 0)
      return (IMSObjectType) null;
    foreach (int objtype in objtypes)
    {
      if (objtype == id)
      {
        int num = (int) MessageBox.Show($"Попытка повторного добавления типа объекта \"{(id != -1 ? (object) MetaDataHelper.GetObjectName(id) : (object) "Любой тип объекта")}\".", "Ошибка");
        return (IMSObjectType) null;
      }
    }
    if (id != -1)
      return MetaDataHelper.GetObjectType(id) ?? (IMSObjectType) null;
    return new IMSObjectType()
    {
      Guid = Guid.Empty,
      ObjectTypeID = -1,
      ObjectTypeName = "Любой тип объекта"
    };
  }

  internal IMSRelationType DiagSelectRelationType(List<int> typeList)
  {
    return this.DiagSelectRelationType(typeList, 0);
  }

  internal IMSRelationType DiagSelectRelationType(List<int> typeList, int projType)
  {
    List<int> c = (List<int>) null;
    if (projType > 0)
      c = MetaDataHelper.GetApplicabilityRelationTypesID(projType);
    SelectorForm selectorForm = new SelectorForm(typeof (RelationTypesFolder), "Допустимые типы связей", typeof (RelationTypeFolder), false);
    if (c != null)
    {
      selectorForm.InitSelectionAsType(new ArrayList((ICollection) c), new ArrayList((ICollection) new System.Type[1]
      {
        typeof (RelationTypesFolder)
      }));
      selectorForm.SelectorFilter = (ISelectorFilter) new TypeSelectorFilter(c.ToArray(), true, true);
      selectorForm.AdditionalRoot = true;
    }
    else
      selectorForm.ExpandLevelsOnLoad = 1;
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count <= 0)
      return (IMSRelationType) null;
    int id = (int) selectorForm.IDList[0];
    if (id == 0)
      return (IMSRelationType) null;
    foreach (int type in typeList)
    {
      if (type == id)
      {
        int num = (int) MessageBox.Show($"Попытка повторного добавления типа связи \"{MetaDataHelper.GetRelationType(id).Description}\".", "Ошибка");
        return (IMSRelationType) null;
      }
    }
    return MetaDataHelper.GetRelationType(id) ?? (IMSRelationType) null;
  }

  internal IMSAttributeType DiagSelectAttributeType(List<int> typeList)
  {
    return this.DiagSelectAttributeType(typeList, (string) null, (string) null);
  }

  internal IMSAttributeType DiagSelectAttributeType(
    List<int> typeList,
    string guidTypeObject,
    string guidTypeRelation)
  {
    AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false);
    if (guidTypeObject != null)
      attributesSelectDlg.LoadAttrDialogForObjectsTypes(new Guid(guidTypeObject));
    else if (guidTypeRelation != null)
      attributesSelectDlg.LoadAttrDialogForRelationsTypes(new Guid(guidTypeRelation));
    if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesGuid.Count <= 0)
      return (IMSAttributeType) null;
    int attrTypeID = attributesSelectDlg.SelectedAttributesID[0];
    if (attrTypeID == 0)
      return (IMSAttributeType) null;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrTypeID);
    if (attributeType == null)
      return (IMSAttributeType) null;
    foreach (int type in typeList)
    {
      if (type == attributeType.AttributeID)
      {
        int num = (int) MessageBox.Show($"Попытка повторного добавления типа атрибута \"{attributeType.Name}\".", "Ошибка");
        return (IMSAttributeType) null;
      }
    }
    return attributeType;
  }

  internal ICategoryTypeIconService CategoryIcons => ConfigEditorHelper._categoryIcons;

  internal int IconsIndexOf(int category, int type, object data)
  {
    return ConfigEditorHelper._categoryIcons != null ? ConfigEditorHelper._categoryIcons.IndexOf(category, type, data) : 0;
  }

  internal int IconsIndexOf(int category, int type)
  {
    return ConfigEditorHelper._categoryIcons != null ? ConfigEditorHelper._categoryIcons.IndexOf(category, type > 0 ? type : 0) : 0;
  }

  internal int IndexImageIcon(string nameIcon)
  {
    return this._namedImageList != null ? this._namedImageList.ImageIndex(nameIcon) : 0;
  }

  internal Image ImageIcon(string nameIcon)
  {
    return this._namedImageList != null ? this._namedImageList.ImageList.Images[this.IndexImageIcon(nameIcon)] : (Image) null;
  }

  internal int IconsIndexObjType(string guidType)
  {
    return this.IconsIndexOf(4, MetaDataHelper.GetObjectTypeID(guidType));
  }

  public static Bitmap MergeTwoImages(Image firstImage, Image secondImage)
  {
    Bitmap bitmap = new Bitmap(32 /*0x20*/, firstImage.Height);
    using (Graphics graphics = Graphics.FromImage((Image) bitmap))
    {
      graphics.DrawImage(firstImage, 0, 0, new Rectangle(0, 0, 16 /*0x10*/, 16 /*0x10*/), GraphicsUnit.Pixel);
      if (secondImage != null)
        graphics.DrawImage(secondImage, 16 /*0x10*/, 0, new Rectangle(0, 0, 16 /*0x10*/, 16 /*0x10*/), GraphicsUnit.Pixel);
    }
    return bitmap;
  }

  public string ExportTypedName(XmlExchangeExportTypedItem type)
  {
    string str1 = string.Empty;
    string str2;
    if (this._modeView.UserDataOnly)
    {
      if (type.UserName != null && type.UserName.Length > 0)
        str1 = type.UserName;
      else if (type.UserAlias != null && type.UserAlias.Length > 0)
        str1 = type.UserAlias;
      else if (type.UserID != null && type.UserID.Length > 0)
        str1 = type.UserID;
      if (str1.Length > 0)
        str1 = $" [{str1}]";
      str2 = type.TypeName + str1;
    }
    else
      str2 = type.TypeName;
    return str2;
  }
}
