// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.CompositionsConfigurator.CompositionConfiguratorServerService
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Pdm.Instances;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.CompositionsConfigurator;

public sealed class CompositionConfiguratorServerService : 
  LongLifeObject,
  ICompositionConfiguratorServerService
{
  private IInstancesServerService _instancesServerService;

  public CompositionConfiguratorServerService(IInstancesServerService instancesServerService)
  {
    this._instancesServerService = instancesServerService != null ? instancesServerService : throw new ArgumentNullException(nameof (instancesServerService));
  }

  public void CopyApplicationConditionsToAllInstances(
    Guid userSessionGuid,
    Tuple<long, long, long>[] compositionParts)
  {
    using (UserSessionContext.CaptureSession(userSessionGuid))
      this.CopyApplicationConditionsToAllInstances(compositionParts);
  }

  private void CopyApplicationConditionsToAllInstances(Tuple<long, long, long>[] compositionParts)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (KeyValuePair<long, List<Tuple<long, long>>> fromCompositionPart in this.CreateDictionaryFromCompositionParts(compositionParts))
      {
        foreach (Tuple<long, long> tuple in fromCompositionPart.Value)
        {
          ObjectsApplicabilitiesCriterionsCollection collection = new ObjectsApplicabilitiesCriterionsCollection((object) sessionKeeper.Session.GetRelation(tuple.Item1));
          foreach (long instance in this._instancesServerService.FindInstances(sessionKeeper.Session.SessionGUID, fromCompositionPart.Key))
          {
            try
            {
              IDBObject source = sessionKeeper.Session.GetObject(instance, false);
              if (source != null)
              {
                if (source.ObjectModifyMode == ObjectModifyModes.Checkout && source.CheckoutBy != sessionKeeper.Session.UserID)
                  source = source.CheckOut();
                ObjectOptionsHolder objectOptionsHolder = new ObjectOptionsHolder((object) source);
                long[] notPresentOnObject = this.GetNewOptionVersinIdsNotPresentOnObject((IEnumerable<long>) objectOptionsHolder.Options, (IEnumerable<long>) collection.GetOptionVersionIds());
                if (notPresentOnObject.Length != 0)
                {
                  objectOptionsHolder.AddOptions((IList<long>) ((IEnumerable<long>) notPresentOnObject).ToList<long>());
                  objectOptionsHolder.SaveToObject((IDBAttributable) source);
                }
                IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
                DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
                dbRecordSetParams.Columns = new object[1]
                {
                  (object) ObligatoryObjectAttributes.F_PRJLINK_ID
                };
                // ISSUE: explicit reference operation
                (^ref dbRecordSetParams).Conditions = new ConditionStructure[2]
                {
                  new ConditionStructure()
                  {
                    Attribute = (object) ObligatoryObjectAttributes.F_PROJ_ID,
                    RelationalOperator = RelationalOperators.Equal,
                    Value = (object) source.ObjectID,
                    SQL = string.Empty,
                    LogicalOperator = LogicalOperators.AND
                  },
                  new ConditionStructure()
                  {
                    Attribute = (object) ObligatoryObjectAttributes.F_PART_ID,
                    RelationalOperator = RelationalOperators.Equal,
                    Value = (object) tuple.Item2,
                    SQL = string.Empty
                  }
                };
                DBRecordSetParams paramSet = dbRecordSetParams;
                foreach (DataRow row in (InternalDataCollectionBase) relationCollection.Select(paramSet).Rows)
                {
                  long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
                  if (!RelationHelper.IsUnknownRelationID(int64Value))
                  {
                    IDBRelation relation = sessionKeeper.Session.GetRelation(int64Value, false);
                    if (relation != null)
                    {
                      ObjectsApplicabilitiesCriterionsCollection criterionsCollection = new ObjectsApplicabilitiesCriterionsCollection((object) relation);
                      criterionsCollection.AddRange((IList<IPdmCriterion>) collection);
                      criterionsCollection.SaveToObject((IDBAttributable) relation);
                    }
                  }
                }
              }
            }
            catch
            {
            }
          }
        }
      }
    }
  }

  private Dictionary<long, List<Tuple<long, long>>> CreateDictionaryFromCompositionParts(
    Tuple<long, long, long>[] compositionParts)
  {
    Dictionary<long, List<Tuple<long, long>>> compositionParts1 = new Dictionary<long, List<Tuple<long, long>>>();
    foreach (Tuple<long, long, long> compositionPart in compositionParts)
    {
      List<Tuple<long, long>> tupleList = (List<Tuple<long, long>>) null;
      if (!compositionParts1.TryGetValue(compositionPart.Item1, out tupleList))
      {
        tupleList = new List<Tuple<long, long>>();
        compositionParts1.Add(compositionPart.Item1, tupleList);
      }
      tupleList.Add(new Tuple<long, long>(compositionPart.Item2, compositionPart.Item3));
    }
    return compositionParts1;
  }

  private long[] GetNewOptionVersinIdsNotPresentOnObject(
    IEnumerable<long> objectOptionVersionIds,
    IEnumerable<long> newOptionVersionIds)
  {
    return newOptionVersionIds.Where<long>((System.Func<long, bool>) (o => !objectOptionVersionIds.Contains<long>(o))).ToArray<long>();
  }
}
