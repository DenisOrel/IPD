// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionUtils
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.AutoSelection.AutoSelectionCache;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client;

public static class AutoSelectionUtils
{
  public static class ServiceKeeper
  {
    public static IAutoSelectionRuleCacheService GetAutosServerService()
    {
      return ServiceUtils.GetService<IAutoSelectionRuleCacheService>((object) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService), true);
    }

    public static IAutoSelectionRuleCacheService GetAutosServerService(IUserSession session)
    {
      return ServiceUtils.GetService<IAutoSelectionRuleCacheService>((object) session, true);
    }

    public static IExpertUser GetExpertUserService()
    {
      return ServiceUtils.GetService<IExpertUser>((object) ApplicationServices.Container, false) ?? throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString(sc_631.ssp_automatch_632()), (object) typeof (IExpertUser)));
    }

    public static IExpertServer GetExpertServerService(IUserSession session)
    {
      return ServiceUtils.GetService<IExpertServer>((object) session, true);
    }

    public static IExpertServer GetExpertServerService()
    {
      return ServiceUtils.GetService<IExpertServer>((object) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService), true);
    }

    public static IImbaseServer GetImbaseServerService(IUserSession session)
    {
      return ServiceUtils.GetService<IImbaseServer>((object) session, true);
    }

    public static IImbaseServer GetImbaseServerService()
    {
      return ServiceUtils.GetService<IImbaseServer>((object) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService), true);
    }

    public static IAutoSelectionService GetAutoSelectionService()
    {
      return ServiceUtils.GetService<IAutoSelectionService>((object) ApplicationServices.Container, false) ?? throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString(sc_631.ssp_automatch_633()), (object) typeof (IAutoSelectionService)));
    }

    public static ICategoryTypeIconService GetCategoryTypeIconService()
    {
      return ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false) ?? throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString(sc_631.ssp_automatch_634()), (object) typeof (ICategoryTypeIconService)));
    }
  }

  public static class Common
  {
    public static System.Type GetNodeObjectType(AutoSelectionNodeType selNodeType)
    {
      switch (selNodeType)
      {
        case AutoSelectionNodeType.None:
          return (System.Type) null;
        case AutoSelectionNodeType.ItemImbase:
          return typeof (AutoSelectionNodeItemImbase);
        case AutoSelectionNodeType.ItemObject:
          return typeof (AutoSelectionNodeItemObject);
        case AutoSelectionNodeType.Folder:
          return typeof (AutoSelectionNodeFolder);
        case AutoSelectionNodeType.Question:
          return typeof (AutoSelectionNodeQuest);
        case AutoSelectionNodeType.ProcCall:
          return typeof (AutoSelectionNodeProc);
        case AutoSelectionNodeType.ScriptCall:
          return typeof (AutoSelectionNodeScript);
        case AutoSelectionNodeType.FillAttributes:
          return typeof (AutoSelectionNodeFillAttributes);
        default:
          return (System.Type) null;
      }
    }

    public static void UpdateNodesLinkCaptions(List<AutoSelectionNodeBase> nodes)
    {
      if (nodes == null || nodes.Count == 0)
        return;
      Dictionary<long, int> id2Types = new Dictionary<long, int>();
      Dictionary<Guid, int> objGuid2Types = new Dictionary<Guid, int>();
      foreach (AutoSelectionNodeBase node in nodes)
        node.CollectLinks(id2Types, objGuid2Types);
      if (id2Types.Count == 0 && objGuid2Types.Count == 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        Dictionary<int, List<long>> dictionary1 = new Dictionary<int, List<long>>();
        Dictionary<long, string> id2Caption = new Dictionary<long, string>();
        Dictionary<Guid, string> guid2Caption = new Dictionary<Guid, string>();
        if (new List<long>((IEnumerable<long>) id2Types.Keys).Count != 0)
        {
          foreach (KeyValuePair<long, int> keyValuePair in id2Types)
          {
            List<long> longList;
            if (!dictionary1.TryGetValue(keyValuePair.Value, out longList))
            {
              longList = new List<long>();
              dictionary1[keyValuePair.Value] = longList;
            }
            longList.Add(keyValuePair.Key);
          }
          List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
          columnDescriptorList.Add(new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
          columnDescriptorList.Add(new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0));
          DataTable toTable = (DataTable) null;
          foreach (KeyValuePair<int, List<long>> keyValuePair in dictionary1)
          {
            int key = keyValuePair.Key;
            List<long> longList = keyValuePair.Value;
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(key);
            if (objectCollection != null)
            {
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
              }, columnDescriptorList.ToArray());
              bool flag = false;
              if (key != -1)
              {
                if (MetaDataHelper.GetObjectType(key) != null)
                {
                  List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(key);
                  if (childrenIdRecursive != null)
                  {
                    childrenIdRecursive.Remove(key);
                    flag = childrenIdRecursive.Count > 0;
                  }
                }
                else
                  continue;
              }
              DataTable fromTable = flag ? objectCollection.SelectWithLocalObjects(paramSet) : objectCollection.Select(paramSet);
              if (toTable == null)
                toTable = fromTable;
              else
                DataSetProcessor.AddTable(toTable, fromTable, false);
            }
          }
          if (toTable != null)
          {
            toTable.AcceptChanges();
            int columnIndex1 = toTable.Columns.IndexOf("F_OBJECT_ID");
            int columnIndex2 = toTable.Columns.IndexOf("CAPTION");
            foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
            {
              long int64 = Convert.ToInt64(row[columnIndex1]);
              string str = row[columnIndex2].ToString();
              if (int64 != 0L)
                id2Caption[int64] = str;
            }
          }
        }
        if (new List<Guid>((IEnumerable<Guid>) objGuid2Types.Keys).Count != 0)
        {
          Dictionary<int, List<Guid>> dictionary2 = new Dictionary<int, List<Guid>>();
          foreach (KeyValuePair<Guid, int> keyValuePair in objGuid2Types)
          {
            List<Guid> guidList;
            if (!dictionary2.TryGetValue(keyValuePair.Value, out guidList))
            {
              guidList = new List<Guid>();
              dictionary2[keyValuePair.Value] = guidList;
            }
            guidList.Add(keyValuePair.Key);
          }
          List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
          {
            new ColumnDescriptor((object) -12, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
            new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
          };
          DataTable toTable = (DataTable) null;
          foreach (KeyValuePair<int, List<Guid>> keyValuePair in dictionary2)
          {
            int key = keyValuePair.Key;
            List<Guid> guidList = keyValuePair.Value;
            IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(key);
            if (objectCollection != null)
            {
              DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
              {
                new ConditionStructure(-12, RelationalOperators.In, (object) guidList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
              }, columnDescriptorList.ToArray());
              DataTable fromTable = objectCollection.Select(paramSet);
              if (fromTable != null)
              {
                if (toTable == null)
                  toTable = fromTable;
                else
                  DataSetProcessor.AddTable(toTable, fromTable, false);
              }
            }
          }
          if (toTable != null && toTable.Rows.Count > 0)
          {
            int columnIndex3 = toTable.Columns.IndexOf("F_GUID");
            int columnIndex4 = toTable.Columns.IndexOf("CAPTION");
            foreach (DataRow row in (InternalDataCollectionBase) toTable.Rows)
            {
              string str1 = row[columnIndex3].ToString();
              string str2 = row[columnIndex4].ToString();
              if (GuidHelper.IsGuid(str1))
                guid2Caption[new Guid(str1)] = str2;
            }
          }
        }
        foreach (AutoSelectionNodeBase node in nodes)
          node.UpdateLinks(id2Caption, guid2Caption);
      }
    }

    public static List<long> GetAvailabledRules(
      Guid objectType,
      List<long> excludeRuleList,
      IUserSession session)
    {
      List<long> availabledRules = new List<long>();
      if (objectType.Equals(Guid.Empty))
        return availabledRules;
      if (excludeRuleList != null && excludeRuleList.Count == 0)
        excludeRuleList.Add(0L);
      IMSObjectType objectType1 = MetaDataHelper.GetObjectType(AutoSelectionConsts.objTypeRuleGuid);
      if (objectType1 == null)
        return availabledRules;
      bool flag = false;
      List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(objectType1.ObjectTypeID);
      if (childrenIdRecursive != null)
      {
        childrenIdRecursive.Remove(objectType1.ObjectTypeID);
        flag = childrenIdRecursive.Count != 0;
      }
      IDBObjectCollection objectCollection = session.GetObjectCollection(AutoSelectionConsts.objTypeRuleGuid);
      int attributeId = MetaDataHelper.GetAttributeID((object) "cad001a0-306c-11d8-b4e9-00304f19f545");
      List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
      if (excludeRuleList != null)
        conditionStructureList.Add(new ConditionStructure(-2, RelationalOperators.NotIn, (object) excludeRuleList.ToArray(), LogicalOperators.AND, 0, false));
      conditionStructureList.Add(new ConditionStructure(attributeId, RelationalOperators.Equal, (object) objectType, LogicalOperators.NONE, 0, true));
      object[] columns = new object[2]
      {
        (object) -2,
        (object) -7
      };
      DBRecordSetParams paramSet = new DBRecordSetParams(conditionStructureList.ToArray(), columns, (object[]) null, (SortOrders[]) null)
      {
        ColumnNames = new ColumnNameMapping[2]
        {
          ColumnNameMapping.ID,
          ColumnNameMapping.ID
        },
        TableName = "f",
        FailIfNotFound = false
      };
      foreach (DataRow row in (InternalDataCollectionBase) (flag ? objectCollection.SelectWithLocalObjects(paramSet) : objectCollection.Select(paramSet)).Rows)
      {
        long result;
        if (long.TryParse(Convert.ToString(row[-2.ToString()]), out result))
          availabledRules.Add(result);
      }
      return availabledRules;
    }
  }

  public static class Forms
  {
    public static void LoadSettings(Form techForm)
    {
      Form form1 = new Form();
      form1.Name = techForm.Name;
      Form form2 = form1;
      FormStorage.LoadLayout((Control) form2);
      techForm.Location = form2.Location;
      techForm.Size = form2.Size;
    }

    public static void SaveSettings(Form techForm)
    {
      Form form = new Form();
      form.Name = techForm.Name;
      form.Location = techForm.Location;
      form.Size = techForm.Size;
      FormStorage.SaveLayout((Control) form);
    }
  }

  public static class Output
  {
    private static readonly string CategoryName = LocalizationHolder.rm.GetString("AutoSelection.Client_85");

    public static void WriteString(string text)
    {
      IOutputView service = ServiceUtils.GetService<IOutputView>((object) ApplicationServices.Container, false);
      if (service == null)
        return;
      service.WriteString(AutoSelectionUtils.Output.CategoryName, text);
      service.Activate(AutoSelectionUtils.Output.CategoryName);
      service.ShowView();
    }
  }

  public static class Cache
  {
    private static DateTime _objTypesLoadTime = DateTime.MinValue;
    private static DateTime _objTypesWithRulesLoadTime = DateTime.MinValue;
    private static List<int> _objTypes;
    private static List<int> _objTypesWithRules;

    static Cache()
    {
      AutoSelectionUtils.Cache.GetObjectTypes(true);
      AutoSelectionUtils.Cache.GetObjectTypesWithRules(true);
    }

    public static List<int> GetObjectTypes(bool forceReload = false)
    {
      if (AutoSelectionUtils.Cache._objTypes != null && !forceReload && (DateTime.Now - AutoSelectionUtils.Cache._objTypesLoadTime).TotalSeconds < (double) ClientConsts.CacheLifeTime)
        return AutoSelectionUtils.Cache._objTypes;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AutoSelectionUtils.Cache._objTypes = new List<int>((IEnumerable<int>) AutoSelectionUtils.ServiceKeeper.GetAutosServerService().GetObjectTypes(sessionKeeper.Session.SessionGUID));
        AutoSelectionUtils.Cache._objTypesLoadTime = DateTime.Now;
      }
      GenericListHelper.MakeUnique<int>(AutoSelectionUtils.Cache._objTypes);
      return AutoSelectionUtils.Cache._objTypes;
    }

    public static List<int> GetObjectTypesWithRules(bool forceReload = false)
    {
      if (AutoSelectionUtils.Cache._objTypesWithRules != null && !forceReload && (DateTime.Now - AutoSelectionUtils.Cache._objTypesWithRulesLoadTime).TotalSeconds < (double) ClientConsts.CacheLifeTime)
        return AutoSelectionUtils.Cache._objTypesWithRules;
      AutoSelectionUtils.Cache._objTypesWithRules = new List<int>((IEnumerable<int>) AutoSelectionUtils.ServiceKeeper.GetAutosServerService().GetAllRulesObjTypes());
      AutoSelectionUtils.Cache._objTypesWithRulesLoadTime = DateTime.Now;
      GenericListHelper.MakeUnique<int>(AutoSelectionUtils.Cache._objTypesWithRules);
      return AutoSelectionUtils.Cache._objTypesWithRules;
    }

    public static void Invalidate()
    {
      AutoSelectionUtils.Cache._objTypesLoadTime = AutoSelectionUtils.Cache._objTypesWithRulesLoadTime = DateTime.MinValue;
    }
  }

  public static class ObjectType
  {
    public static bool IsObjectSerializable(object obj)
    {
      return obj != null && obj.GetType().GetCustomAttributes(typeof (SerializableAttribute), false).Length != 0;
    }

    public static List<int> GetParentObjTypes(
      int objTypeId,
      AutoSelectionExecObjMode execObjMode,
      IUserSession session)
    {
      List<int> list = new List<int>();
      if (objTypeId == -1 || session == null)
        return list;
      switch (execObjMode)
      {
        case AutoSelectionExecObjMode.CurrentObject:
          list.Add(objTypeId);
          break;
        case AutoSelectionExecObjMode.ParentObject:
          DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(-1, objTypeId, -1);
          if (applicabilitiesList != null)
          {
            int columnIndex = applicabilitiesList.Columns.IndexOf("F_INOBJECT_TYPE");
            if (columnIndex != -1)
            {
              foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
                list.Add(Convert.ToInt32(row[columnIndex]));
              GenericListHelper.MakeUnique<int>(list);
              break;
            }
            break;
          }
          break;
      }
      return list;
    }
  }
}
