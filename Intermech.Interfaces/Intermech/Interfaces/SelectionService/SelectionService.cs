
// Type: Intermech.Interfaces.SelectionService.SelectionService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Expressions.Exceptions;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;


namespace Intermech.Interfaces.SelectionService
{
    /// <summary>Класс реализующий службу для работы с выборками</summary>
    public class SelectionService : ISelectionsService
    {
      private SelectionWrapper wrapper;
      /// <summary>
      /// Кэш для хранения темповой информации по выборке для текущего сеанса работы
      /// (имеет смысл только в клиентской части - серверная иво игнарирует)
      /// </summary>
      private readonly UserConditionStructuresCache _ucscache;
      private readonly SelectionsCache _cache = new SelectionsCache();
      /// <summary>
      /// флаг, показывающий на клиенте или на сервере запущен данный экземпляр службы
      /// </summary>
      private readonly bool _isClientPartService;

      /// <summary>Конструктор класса</summary>
      /// <param name="clientPart">признак, показывающий на клиенте или на сервере
      /// создается данный экземпляр службы. Если экземпляр создается на сервере, то надо
      /// установить false, если на клиенте - true</param>
      public SelectionService(bool clientPart)
      {
        this.wrapper = new SelectionWrapper(clientPart);
        this._isClientPartService = clientPart;
        if (!clientPart)
          return;
        this._ucscache = new UserConditionStructuresCache();
        this._ucscache.Reload();
      }

      private IUserSession GetUserSession(object userSession)
      {
        return userSession is IUserSession ? userSession as IUserSession : throw new InvalidArgumentException();
      }

      public ConditionStructure[] GetConditionStructures(object userSession, long selectionID)
      {
        return this.GetConditionStructures((object) this.GetUserSession(userSession), selectionID, 0L);
      }

      private ConditionStructure[] CopyConditionStructure(object ConStr)
      {
        return (ConditionStructure[]) ConStr;
      }

      public void DisableConditionStructures(long selectionID, List<int> conditionIndexes)
      {
        TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
        if (temporaryInfo != null)
          temporaryInfo.DisableIndexes = conditionIndexes;
        else if (conditionIndexes != null && conditionIndexes.Count > 0)
          this._ucscache.SetValue(selectionID, new TemporaryInfo(conditionIndexes));
        this._ucscache.Save();
      }

      public bool IsEnabledConditionStructure(long selectionID, int conditionIndex)
      {
        TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
        return temporaryInfo == null || temporaryInfo.DisableIndexes == null || !temporaryInfo.DisableIndexes.Contains(conditionIndex);
      }

      public ConditionStructure[] GetConditionStructures(
        object userSession,
        long selectionID,
        long objectID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        ConditionStructure[] conditionStructureArray = this._cache.Get(selectionID, userSession1, this.wrapper);
        if (conditionStructureArray == null)
          return new ConditionStructure[0];
        List<int> intList = new List<int>(0);
        List<object[]> objArrayList = (List<object[]>) null;
        if (this._isClientPartService)
        {
          TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
          if (temporaryInfo != null)
          {
            if (temporaryInfo.DisableIndexes != null)
              intList = temporaryInfo.DisableIndexes;
            if (temporaryInfo.Values != null && temporaryInfo.Values.Count > 0)
              objArrayList = temporaryInfo.Values;
          }
        }
        ConditionStructure[] sourceCS = conditionStructureArray.Clone() as ConditionStructure[];
        for (int index = 0; index < sourceCS.Length; ++index)
        {
          if (this._isClientPartService)
          {
            if (intList.Count > 0 && intList.Contains(index))
            {
              sourceCS[index].RelationalOperator = RelationalOperators.NOP;
              continue;
            }
            if (objArrayList != null)
            {
              object[] objArray = objArrayList[index];
              sourceCS[index].Value = objArray[0];
              sourceCS[index].Value2 = objArray[1];
            }
          }
          this.HandleCondition(ref sourceCS[index], objectID, userSession1);
        }
        return this.HandleObjectTypeConditions(this.CheckConditionStructures(sourceCS));
      }

      private void HandleCondition(
        ref ConditionStructure conditionStructure,
        long objectID,
        IUserSession session)
      {
        if (objectID == 0L)
          return;
        if ((conditionStructure.RelationalOperator == RelationalOperators.ConsistFrom || conditionStructure.RelationalOperator == RelationalOperators.EntersIn || conditionStructure.RelationalOperator == RelationalOperators.EntersInType || conditionStructure.RelationalOperator == RelationalOperators.NotEntersInType || conditionStructure.RelationalOperator == RelationalOperators.ExistsInVersionContext) && Convert.ToInt64(conditionStructure.Value) == -1L)
          conditionStructure.Value = (object) objectID;
        if (conditionStructure.RelationalOperator == RelationalOperators.ConsistFrom && session != null && conditionStructure.Value != null)
        {
          IDBObject dbObject = session.GetObject(Convert.ToInt64(conditionStructure.Value), false);
          conditionStructure.Value = (object) (dbObject != null ? dbObject.ID : 0L);
        }
        object newValue1;
        if (this.HandleInputObjectAttribute(conditionStructure.Value, out newValue1, objectID, session, conditionStructure.RelationalOperator))
          conditionStructure.Value = newValue1;
        object newValue2;
        if (this.HandleInputObjectAttribute(conditionStructure.Value2, out newValue2, objectID, session, conditionStructure.RelationalOperator))
          conditionStructure.Value = newValue2;
        if (conditionStructure.NestedConditions == null)
          return;
        for (int index = 0; index < conditionStructure.NestedConditions.Length; ++index)
          this.HandleCondition(ref conditionStructure.NestedConditions[index], objectID, session);
      }

      private bool HandleInputObjectAttribute(
        object value,
        out object newValue,
        long objectID,
        IUserSession session,
        RelationalOperators oper)
      {
        newValue = (object) null;
        if (value == null)
          return false;
        bool firstValueOnly = oper != RelationalOperators.In && oper != RelationalOperators.NotIn;
        if (value.GetType().Equals(typeof (InputObjectAttribute)))
        {
          newValue = ((InputObjectAttribute) value).GetAttributeValueByObjectID(session, objectID, firstValueOnly);
          return true;
        }
        if (value.GetType().Equals(typeof (object[])))
        {
          object[] objArray = (object[]) value;
          if (objArray.Length != 0 && objArray[0].GetType().Equals(typeof (InputObjectAttribute)))
          {
            List<object> objectList = new List<object>();
            for (int index = 0; index < objArray.Length; ++index)
            {
              object attributeValueByObjectId = ((InputObjectAttribute) objArray[index]).GetAttributeValueByObjectID(session, objectID, firstValueOnly);
              if (attributeValueByObjectId is object[])
                objectList.AddRange((IEnumerable<object>) (object[]) attributeValueByObjectId);
              else
                objectList.Add(attributeValueByObjectId);
            }
            newValue = (object) objectList.ToArray();
            return true;
          }
        }
        return false;
      }

      private ConditionStructure[] HandleObjectTypeConditions(ConditionStructure[] conditions)
      {
        if (conditions == null || conditions.Length == 0)
          return conditions;
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        for (int index1 = 0; index1 < conditions.Length; ++index1)
        {
          ConditionStructure condition = conditions[index1];
          if (condition.RelationalOperator == RelationalOperators.ConsistFromType || condition.RelationalOperator == RelationalOperators.NotConsistFromType || condition.RelationalOperator == RelationalOperators.EntersInType || condition.RelationalOperator == RelationalOperators.NotEntersInType)
          {
            List<int> intList1 = (List<int>) null;
            List<int> intList2 = new List<int>();
            if (condition.Value is int)
              intList1 = new List<int>(1)
              {
                (int) condition.Value
              };
            else if (condition.Value is IList<int>)
              intList1 = new List<int>((IEnumerable<int>) condition.Value);
            if (intList1 != null)
            {
              foreach (int parentTypeID in intList1)
              {
                List<int> childrenIdRecursive = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive(parentTypeID);
                childrenIdRecursive.RemoveAt(0);
                foreach (int num in childrenIdRecursive)
                {
                  if (!intList2.Contains(num))
                    intList2.Add(num);
                }
              }
            }
            if (intList2.Count == 0)
            {
              conditionStructureList.Add(condition);
            }
            else
            {
              LogicalOperators logicalOperators = condition.RelationalOperator == RelationalOperators.ConsistFromType || condition.RelationalOperator == RelationalOperators.EntersInType ? LogicalOperators.OR : LogicalOperators.AND;
              LogicalOperators logicalOperator = condition.LogicalOperator;
              condition.LogicalOperator = logicalOperators;
              ++condition.GroupID;
              conditionStructureList.Add(condition);
              for (int index2 = 0; index2 < intList2.Count; ++index2)
              {
                ConditionStructure conditionStructure = condition.Clone() with
                {
                  Value = (object) intList2[index2],
                  LogicalOperator = logicalOperators
                };
                if (index2 == intList2.Count - 1)
                {
                  conditionStructure.GroupID = -1;
                  conditionStructure.LogicalOperator = logicalOperator;
                }
                else
                  conditionStructure.GroupID = 0;
                conditionStructureList.Add(conditionStructure);
              }
            }
          }
          else
            conditionStructureList.Add(condition);
        }
        return conditionStructureList.ToArray();
      }

      private ConditionStructure[] CheckConditionStructures(ConditionStructure[] sourceCS)
      {
        if (sourceCS == null || sourceCS.Length == 0)
          return sourceCS;
        List<ConditionStructure> cs = new List<ConditionStructure>((IEnumerable<ConditionStructure>) sourceCS);
        int num = 0;
        for (int index = 0; index < sourceCS.Length; ++index)
        {
          if (sourceCS[index].RelationalOperator == RelationalOperators.NOP)
          {
            this.RemoveConditionStructure(cs, index - num);
            ++num;
          }
        }
        return cs.ToArray();
      }

      private void RemoveConditionStructure(List<ConditionStructure> cs, int removedIndex)
      {
        int groupId = cs[removedIndex].GroupID;
        if (groupId > 0)
        {
          if (cs.Count >= removedIndex + 2)
          {
            ConditionStructure c = cs[removedIndex + 1];
            c.GroupID += groupId;
            c.LogicalOperator = cs[removedIndex].LogicalOperator;
            cs[removedIndex + 1] = c;
          }
        }
        else if (groupId < 0 && removedIndex - 1 >= 0)
        {
          ConditionStructure c = cs[removedIndex - 1];
          c.GroupID += groupId;
          c.LogicalOperator = cs[removedIndex].LogicalOperator;
          cs[removedIndex - 1] = c;
        }
        cs.RemoveAt(removedIndex);
      }

      public bool SetConditionStructures(
        object userSession,
        long selectionID,
        ConditionStructure[] conditionStructures)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        bool flag = false;
        if (this.wrapper.SaveConditionStructures(userSession1, selectionID, conditionStructures))
        {
          this._cache.Set(selectionID, conditionStructures);
          if (this._isClientPartService)
          {
            if (userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
              flag = customService.SetConditionStructures((object) userSession1.SessionGUID, selectionID, conditionStructures);
          }
          else
            flag = true;
        }
        return flag;
      }

      public void UpdateCashe(object userSession)
      {
        this._cache.Reload(this.GetUserSession(userSession), this.wrapper);
      }

      public void UpdateCashe(object userSession, long selectionID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        this._cache.Reload(userSession1, this.wrapper, selectionID);
        if (!this._isClientPartService || !(userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
          return;
        customService.UpdateCashe((object) userSession1.SessionGUID, selectionID);
      }

      public void ClearCashe() => this._cache.Clear();

      public IObjectClassificator GetObjectClassificator(object userSession, long classifierID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GetObjectClassificator((object) userSession1.SessionGUID, classifierID) : (IObjectClassificator) null;
      }

      public void IncludeObjects(object userSession, long selectionID, long[] objectIDs)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (!this._isClientPartService || !(userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
          return;
        customService.IncludeObjects((object) userSession1.SessionGUID, selectionID, objectIDs);
      }

      public void IncludeObjects(object userSession, Guid selectionGuid, long[] objectIDs)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (!this._isClientPartService || !(userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
          return;
        customService.IncludeObjects((object) userSession1.SessionGUID, selectionGuid, objectIDs);
      }

      public void ExcludeObjects(object userSession, long selectionID, long[] objectIDs)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (!this._isClientPartService || !(userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
          return;
        customService.ExcludeObjects((object) userSession1.SessionGUID, selectionID, objectIDs);
      }

      public void ExcludeObjectsByID(object userSession, long selectionID, long[] IDs)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (!this._isClientPartService || !(userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
          return;
        customService.ExcludeObjectsByID((object) userSession1.SessionGUID, selectionID, IDs);
      }

      public bool ExistsObject(object userSession, long selectionID, long objectID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService && customService.ExistsObject((object) userSession1.SessionGUID, selectionID, objectID);
      }

      public long[] ExistsObjectsID(object userSession, long selectionID, long[] objectIDs)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
          return customService.ExistsObjectsID((object) userSession1.SessionGUID, selectionID, objectIDs);
        throw new KernelException("Неверное использование службы ISelectionsService");
      }

      public void SetShowInternalFolders(bool newValue) => this.wrapper.ShowInternalFolders = newValue;

      public bool GetShowInternalFolders() => this.wrapper.ShowInternalFolders;

      public void LoadClassifierToObjTypeCache()
      {
      }

      public long[] GetClassifierForObjType(object userSession, int objType)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GetClassifierForObjType((object) userSession1.SessionGUID, objType) : (long[]) null;
      }

      public void DeleteClassifierFromCache(long classifierID)
      {
      }

      public void AddClassifierToCache(IUserSession session, long classifierID)
      {
      }

      public long GetClassifierForObject(object userSession, long ID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GetClassifierForObject((object) userSession1.SessionGUID, ID) : -1L;
      }

      public string GenerateNextTopLevelKey(object userSession)
      {
        string nextTopLevelKey = "";
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
          nextTopLevelKey = customService.GenerateNextTopLevelKey((object) userSession1.SessionGUID);
        return nextTopLevelKey;
      }

      public string GenerateNextTopLevelKey(object userSession, int objType)
      {
        string nextTopLevelKey = "";
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
          nextTopLevelKey = customService.GenerateNextTopLevelKey((object) userSession1.SessionGUID, objType);
        return nextTopLevelKey;
      }

      public int[] GetObjectTypesForClassifier(object userSession, long classifierID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GetObjectTypesForClassifier((object) userSession1.SessionGUID, classifierID) : (int[]) null;
      }

      public string GenerateNextClassifierKey(
        object userSession,
        int parentTypeID,
        string parentKey,
        int objType)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GenerateNextClassifierKey((object) userSession1.SessionGUID, parentTypeID, parentKey, objType) : string.Empty;
      }

      public bool CanUpperMemo(object userSession)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService && customService.CanUpperMemo((object) userSession1.SessionGUID);
      }

      public void SetTemporaryValues(long selectionID, List<object[]> values)
      {
        TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
        if (temporaryInfo != null)
          temporaryInfo.Values = values;
        else
          this._ucscache.SetValue(selectionID, new TemporaryInfo(values));
        this._ucscache.Save();
      }

      public List<object[]> GetTemporaryValues(long selectionID)
      {
        return this._ucscache.GetValue(selectionID)?.Values;
      }

      public bool IsTemporaryValuesPresent(long selectionID)
      {
        TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
        return temporaryInfo != null && temporaryInfo.Values != null && temporaryInfo.Values.Count > 0;
      }

      public void RemoveTemporaryValues(long selectionID)
      {
        TemporaryInfo temporaryInfo = this._ucscache.GetValue(selectionID);
        if (temporaryInfo == null)
          return;
        temporaryInfo.Values = (List<object[]>) null;
        this._ucscache.Save();
      }

      public long GetRootClassifier(object userSession, long childClassifier)
      {
        long rootClassifier = 0;
        IUserSession userSession1 = this.GetUserSession(userSession);
        if (this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
          rootClassifier = customService.GetRootClassifier((object) userSession1.SessionGUID, childClassifier);
        return rootClassifier;
      }

      public long GetRootClassifier(object userSession, IDBObject childClassifier)
      {
        return this.GetRootClassifier(userSession, childClassifier.ObjectID);
      }

      public Dictionary<int, List<long>> IncludedObjects(object userSession, long selectionID)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.IncludedObjects((object) userSession1.SessionGUID, selectionID) : (Dictionary<int, List<long>>) null;
      }

      public Guid StartCopyStructure(object userSession, string name, long prototypeID, long parentID)
      {
        throw new NotImplementedException();
      }

      public void StopCopyStructure(Guid copierGuid) => throw new NotImplementedException();

      public StructureCopierStateInfo GetCopyStructureInfo(Guid copierGuid)
      {
        throw new NotImplementedException();
      }

      public string GenerateNextClassifierKey(object userSession, int objType, long id)
      {
        IUserSession userSession1 = this.GetUserSession(userSession);
        return this._isClientPartService && userSession1.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService ? customService.GenerateNextClassifierKey((object) userSession1.SessionGUID, objType, id) : string.Empty;
      }
    }
}
