// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.PropertyEditors.MemberOfAssemblyEditor
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Compositions.CompositionService;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Navigator.Filters;
using Intermech.TechCard.Client.TcObjectsTypes;
using Intermech.TechCard.Client.Tools.Controls.Navigator;
using Intermech.TechCard.Client.Tools.Controls.Navigator.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.UI.PropertyEditors;

/// <summary>Редактор для атрибута "Входимость - сборка"</summary>
internal class MemberOfAssemblyEditor : UITypeEditor
{
  /// <summary>Допустимые типы объектов для тек. атрибута</summary>
  private readonly int[] _linkedObjectTypes;

  /// <summary>Выбор объектов по входимости</summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <param name="objectItem"></param>
  /// <returns></returns>
  private object SelectAssemblyForObject(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value,
    ObjInfoItem objectItem)
  {
    if (context == null)
      return value;
    if ((TypedInfoItem) objectItem == (TypedInfoItem) null || !MetaDataHelper.IsObjectTypeChildOf(objectItem.ObjTypeID, TechCardConsts.ObjectTypes.ProcRoutingID) && !MetaDataHelper.IsObjectTypeChildOf(objectItem.ObjTypeID, TechCardConsts.ObjectTypes.ProcRoutingEntryID))
      return this.SelectAssembly(context, provider, value);
    List<long> articlesForProcRoute;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      articlesForProcRoute = ProcRouteHelper.GetArticlesForProcRoute(objectItem.ObjectID, sessionKeeper.Session);
    if (articlesForProcRoute.Count == 0)
      return this.SelectAssembly(context, provider, value);
    IList<long> projObjectIds = (IList<long>) null;
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) sessionKeeper.Session, true);
      ConditionStructure[] conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-7, RelationalOperators.In, (object) MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.ArticleObjectTypes).ToArray(), LogicalOperators.NONE, 0, false)
      };
      ColumnDescriptor[] columns = new ColumnDescriptor[3]
      {
        new ColumnDescriptor((object) -21, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -20, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
        new ColumnDescriptor((object) -23, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
      };
      CompositionLoadingParams loadingParams = new CompositionLoadingParams((IEnumerable<ObjInfoItem>) SomeTypedInfoHelper<ObjInfoItem>.GetItemInfoList((IEnumerable<long>) articlesForProcRoute), (IEnumerable<int>) null, (IEnumerable<int>) null, (IEnumerable<int>) new int[2]
      {
        TechCardConsts.RelTypes.ProektRelationID,
        TechCardConsts.RelTypes.ProductReportRelationID
      }, (IEnumerable<ColumnDescriptor>) columns, (IEnumerable<ConditionStructure>) conditions, false, false, 1, (VersionsRule) null, DataHelper.Consts.cnt_def_filtrationRule);
      dataTable = service.LoadComplexCompositions((object) sessionKeeper.Session.SessionGUID, loadingParams);
      if (dataTable != null)
        projObjectIds = (IList<long>) dataTable.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (item => DataSetProcessor.GetInt64Value(item, "F_PROJ_ID", 0L))).ToList<long>();
    }
    string caption = LocalizationHolder.rm.GetString("TechCard.Client_210");
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (long objId in articlesForProcRoute)
    {
      TechCompositionDataTableFilter compositionDataTableFilter = new TechCompositionDataTableFilter(RelatedObjectsRole.Applicability, dataTable);
      IDescriptor descriptor = (IDescriptor) new TechCompositionDescriptor(1, 0, objId, TechCardConsts.ObjectTypes.ArticleBaseID, (IEnumerable<int>) new int[2]
      {
        TechCardConsts.RelTypes.ProektRelationID,
        TechCardConsts.RelTypes.ProductReportRelationID
      }, caption, RelatedObjectsRole.Applicability, (ITechCompositionFilter) compositionDataTableFilter, (IEnumerable<NodeColumnID>) null);
      descriptors.Add(descriptor);
    }
    IDescriptor descriptor1 = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, TechCardConsts.ObjectTypes.ArticleBaseID, caption, descriptors);
    using (TechcardObjectForm objForm = new TechcardObjectForm())
    {
      objForm.Name = "MemberOfAssemblyEditor_SelectAssembly";
      objForm.tolcTechObjList.SelectedItemsChanged += new EventHandler(SelectedItemChangedHandler);
      objForm.Load += new EventHandler(LoadHandler);
      objForm.LoadData(LocalizationHolder.rm.GetString("TechCard.Client_211"), descriptor1);
      object obj = (object) DBNull.Value;
      try
      {
        if (objForm.ShowDialog() != DialogResult.OK || objForm.tolcTechObjList.SelectedItems.Count == 0)
          return value;
        if (objForm.tolcTechObjList.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(itemData.ObjectID);
            obj = !objectInfo.Empty ? (object) new ObjectPropertyClass(objectInfo.ObjectID, objectInfo.Caption) : (object) DBNull.Value;
          }
        }
      }
      finally
      {
        objForm.tolcTechObjList.SelectedItemsChanged -= new EventHandler(SelectedItemChangedHandler);
        objForm.Load -= new EventHandler(LoadHandler);
      }
      return obj;

      void SelectedItemChangedHandler(object sender, EventArgs e)
      {
        IDBTypedObjectID itemData = objForm.tolcTechObjList.SelectedItems.Count > 0 ? objForm.tolcTechObjList.SelectedItems.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
        objForm.btnApply.Enabled = itemData != null && projObjectIds != null && projObjectIds.Contains(itemData.ObjectID);
      }

      void LoadHandler(object sender, EventArgs e)
      {
        if (objForm.tolcTechObjList.RootNode?.Children == null)
          return;
        foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) objForm.tolcTechObjList.RootNode.Children)
        {
          if (child is TechcardNavTreeNode techcardNavTreeNode)
            techcardNavTreeNode.ExpandNode(false);
        }
      }
    }
  }

  /// <summary>Выбор объектов из списка</summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  private object SelectAssembly(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    object obj = value;
    List<long> longList = TechCardClientConst.SelectObjectsDlg((IEnumerable<Guid>) ((IEnumerable<int>) this._linkedObjectTypes).Select<int, Guid>(new System.Func<int, Guid>(MetaDataHelper.GetObjectTypeGuid)).ToArray<Guid>(), LocalizationHolder.rm.GetString("TechCard.Client_211"));
    if (longList.Count == 0)
      return obj;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(longList[0] == 0L ? -1L : longList[0]);
      return !objectInfo.Empty ? (object) new ObjectPropertyClass(objectInfo.ObjectID, objectInfo.Caption) : (object) DBNull.Value;
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="attributeId"></param>
  public MemberOfAssemblyEditor(int attributeId)
  {
    this._linkedObjectTypes = MetaDataHelper.GetLinkedObjectTypes(attributeId)?.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <returns></returns>
  public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
  {
    return UITypeEditorEditStyle.Modal;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="provider"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object EditValue(
    ITypeDescriptorContext context,
    System.IServiceProvider provider,
    object value)
  {
    if (context == null)
      return value;
    TypedInfoItem typedInfoItem = (TypedInfoItem) null;
    Intermech.Client.Core.FormDesigner.Controls.ElementInfo elementInfo;
    if (context.Instance is ObjectPropDescriptorHolder instance)
    {
      elementInfo = new Intermech.Client.Core.FormDesigner.Controls.ElementInfo(instance.Id, instance.AttributableElement);
      typedInfoItem = new TypedInfoItem(instance.Id, instance.ElementType);
    }
    else
    {
      elementInfo = context.PropertyDescriptor is PropDescriptor propertyDescriptor ? propertyDescriptor.Component as Intermech.Client.Core.FormDesigner.Controls.ElementInfo : (Intermech.Client.Core.FormDesigner.Controls.ElementInfo) null;
      if (elementInfo != null)
        typedInfoItem = new TypedInfoItem(elementInfo.ElementIdentifier);
    }
    ObjInfoItem objectItem = (ObjInfoItem) null;
    if (elementInfo != null && elementInfo.ElementKind == AttributableElements.Object)
    {
      objectItem = new ObjInfoItem(typedInfoItem);
      if (objectItem.ObjTypeID == -1)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          objectItem.ObjTypeID = sessionKeeper.Session.GetObjectInfo(objectItem.ObjectID).ObjectTypeID;
      }
    }
    return !((TypedInfoItem) objectItem != (TypedInfoItem) null) ? this.SelectAssembly(context, provider, value) : this.SelectAssemblyForObject(context, provider, value, objectItem);
  }
}
