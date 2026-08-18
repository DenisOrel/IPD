// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Cadmech.StandardPartRelinker
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Remoting.Sponsors;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Tools.StandardParts.Cadmech;

internal sealed class StandardPartRelinker : ImportContextTask
{
  private List<StandardPartRelinker.AssemblyModelType> asmModelTypes;

  protected override void DoInitializeContextData()
  {
    base.DoInitializeContextData();
    this.asmModelTypes = new List<StandardPartRelinker.AssemblyModelType>(this.ImportContext.AssemblyModelTypes.Count);
    foreach (LocalId<int> assemblyModelType in this.ImportContext.AssemblyModelTypes)
    {
      if (this.CanContainRelation(IDCache.Default.DocumentTree.Id, assemblyModelType.Id, this.ImportContext.StandardModelType.Id))
      {
        List<int> productTypes;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          DataTable applicabilitiesList = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(IDCache.Default.ArticleToDocumentTree.Id, assemblyModelType.Id, -1);
          productTypes = new List<int>(applicabilitiesList.Rows.Count);
          foreach (DataRow row in (InternalDataCollectionBase) applicabilitiesList.Rows)
          {
            if (Convert.ToInt32(row["F_MIN_LINKS"]) != -1)
              productTypes.Add(Convert.ToInt32(row["F_INOBJECT_TYPE"]));
          }
        }
        this.asmModelTypes.Add(new StandardPartRelinker.AssemblyModelType(assemblyModelType.Id, (IList<int>) productTypes));
      }
    }
  }

  private bool CanContainRelation(int relationType, int projectType, int partType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(relationType, partType, projectType);
      return applicability != null && applicability.ApplicabilityMode != ApplicabilityModes.Disabled;
    }
  }

  protected override void DoCleanupContextData()
  {
    base.DoCleanupContextData();
    this.asmModelTypes = (List<StandardPartRelinker.AssemblyModelType>) null;
  }

  public void RelinkPart(long partId, long modelId)
  {
    if (partId == 0L)
      throw new ArgumentException();
    if (modelId == 0L)
      throw new ArgumentException();
    this.RequireImportContext();
    this.RelinkCore(this.CollectAssemblyModels(partId), modelId);
  }

  private void RelinkCore(List<long> asmModels, long modelId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = ServiceUtils.GetService<IAdminUtilsService>((object) sessionKeeper.Session, true).GetRelationCollection(sessionKeeper.Session.SessionGUID, IDCache.Default.DocumentTree.Id);
      using (new RemoteLock((object) relationCollection))
      {
        foreach (long asmModel in asmModels)
        {
          if (sessionKeeper.Session.GetRelation(asmModel, modelId, true) == null)
            this.ImportContext.NotifyQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", relationCollection.Create(asmModel, modelId).RelationID));
        }
      }
    }
  }

  private List<long> CollectAssemblyModels(long partId)
  {
    List<long> asmModels = new List<long>(1024 /*0x0400*/);
    foreach (StandardPartRelinker.AssemblyModelType asmModelType in this.asmModelTypes)
    {
      foreach (int productType in (IEnumerable<int>) asmModelType.ProductTypes)
      {
        foreach (long lookupStdPartProduct in this.LookupStdPartProducts(partId, productType, "cad00601-306c-11d8-b4e9-00304f19f545"))
        {
          List<long> longList = this.LookupProductModels(lookupStdPartProduct, asmModelType.ModelType, "cad001e0-306c-11d8-b4e9-00304f19f545");
          asmModels.AddRange((IEnumerable<long>) longList.FindAll((Predicate<long>) (productModel => !asmModels.Contains(productModel))));
        }
      }
    }
    return asmModels;
  }

  private List<long> LookupStdPartProducts(long partId, int productType, string versionsRuleOwner)
  {
    if (partId == 0L)
      throw new ArgumentException();
    if (productType == -1)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(versionsRuleOwner))
      throw new ArgumentException();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleTree.Id);
      relationCollection.ObjectTypeID = productType;
      relationCollection.FiltrationOwnerID = versionsRuleOwner;
      DataTable dataTable = relationCollection.EntersInVersion(paramSet, partId);
      List<long> longList = new List<long>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        longList.Add(Convert.ToInt64(row[0]));
      return longList;
    }
  }

  private List<long> LookupProductModels(long articleId, int modelType, string versionsRuleOwner)
  {
    if (articleId == 0L)
      throw new ArgumentException();
    if (modelType == -1)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(versionsRuleOwner))
      throw new ArgumentException();
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(IDCache.Default.ArticleToDocumentTree.Id);
      relationCollection.FiltrationOwnerID = versionsRuleOwner;
      relationCollection.ObjectTypeID = modelType;
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, articleId);
      List<long> longList = new List<long>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        longList.Add(Convert.ToInt64(row[0]));
      return longList;
    }
  }

  private class AssemblyModelType
  {
    private int modelType;
    private IList<int> productTypes;

    public AssemblyModelType(int modelType, IList<int> productTypes)
    {
      this.modelType = modelType;
      this.productTypes = productTypes;
    }

    public int ModelType => this.modelType;

    public IList<int> ProductTypes => this.productTypes;
  }
}
