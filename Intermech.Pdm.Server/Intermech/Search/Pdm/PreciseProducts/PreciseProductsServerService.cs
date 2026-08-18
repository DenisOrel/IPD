// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.PreciseProductsServerService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

public sealed class PreciseProductsServerService : LongLifeObject, IPreciseProductsServerService
{
  public PreciseProductBlank[] CreatePreciseProductsBlanks(
    Guid userSessionGuid,
    CreatePreciseProductsBlanksParams createPreciseProductsBlanksParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return createPreciseProductsBlanksParams != null ? this.CreatePreciseProductsBlanksInternal(createPreciseProductsBlanksParams) : throw new ArgumentNullException(nameof (createPreciseProductsBlanksParams));
  }

  public CreatePreciseProductResult CreatePreciseProduct(
    Guid userSessionGuid,
    CreatePreciseProductParams createPreciseProductParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (createPreciseProductParams == null)
        throw new ArgumentNullException(nameof (createPreciseProductParams));
      return CreatePreciseProductParams.Check(createPreciseProductParams) ? this.CreatePreciseProductInternal(createPreciseProductParams) : throw new ArgumentException();
    }
  }

  private PreciseProductBlank[] CreatePreciseProductsBlanksInternal(
    CreatePreciseProductsBlanksParams createPreciseProductsBlanksParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(createPreciseProductsBlanksParams.RelationID);
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(relation.ProjID);
      List<PreciseProductBlank> preciseProductsBlanks = new List<PreciseProductBlank>();
      PreciseProductBlank preciseProductBlank = new PreciseProductBlank(createPreciseProductsBlanksParams.RelationID, createPreciseProductsBlanksParams.ProductVersionID);
      IDBObject dbObject2 = sessionKeeper.Session.GetObject(createPreciseProductsBlanksParams.ProductVersionID);
      preciseProductBlank.ProductCaption = dbObject2.Caption;
      preciseProductBlank.ProductObjectTypeID = dbObject2.ObjectType;
      IDBAttribute byId1 = dbObject2.Attributes.FindByID(PreciseProductsConstants.NameAttributeTypeID);
      preciseProductBlank.PreciseProductName = byId1?.AsString;
      IDBAttribute byId2 = dbObject2.Attributes.FindByID(PreciseProductsConstants.DesignationAttributeTypeID);
      preciseProductBlank.ProductDesignation = byId2?.AsString;
      preciseProductBlank.PreciseProductDesignation = preciseProductBlank.ProductDesignation;
      preciseProductsBlanks.Add(preciseProductBlank);
      List<Tuple<long, long>> context = new List<Tuple<long, long>>()
      {
        new Tuple<long, long>(createPreciseProductsBlanksParams.RelationID, createPreciseProductsBlanksParams.ProductVersionID)
      };
      this.CreatePreciseProductsBlanksInternal(dbObject1.ObjectID, dbObject1.TypeID, createPreciseProductsBlanksParams.RelationID, createPreciseProductsBlanksParams.ProductVersionID, preciseProductsBlanks, context);
      return preciseProductsBlanks.ToArray();
    }
  }

  private CreatePreciseProductResult CreatePreciseProductInternal(
    CreatePreciseProductParams createPreciseProductParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = sessionKeeper.Session.GetCustomService(typeof (IDBTransactions)) as IDBTransactions;
      customService.StartTransaction();
      try
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(createPreciseProductParams.RelationID);
        IDBObject dbObject = sessionKeeper.Session.GetObject(relation.ProjID);
        PreciseProductsServerService.CreatePreciseProductContext createPreciseAssemlyUnitContext = new PreciseProductsServerService.CreatePreciseProductContext(dbObject.ObjectID, dbObject.ObjectType);
        this.CreatePreciseProductInternal(createPreciseProductParams, createPreciseAssemlyUnitContext, true);
        customService.Commit();
        return new CreatePreciseProductResult()
        {
          CreatedPreciseProductVersionIDDictionaryByCompositionPartID = createPreciseAssemlyUnitContext.CreatedPresiceProductVersionIDDictionaryByCompositionPartID
        };
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private bool CreatePreciseProductsBlanksInternal(
    long rootObjectVersionID,
    int rootObjectTypeID,
    long relationID,
    long objectVersionID,
    List<PreciseProductBlank> preciseProductsBlanks,
    List<Tuple<long, long>> context)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ColumnInfo[] columns = new ColumnInfo[9]
      {
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.CAPTION,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) PreciseProductsConstants.PdmConfiguratorContextAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnInfo()
        {
          AttributeID = (object) PreciseProductsConstants.PdmConfiguratorOptionsLinkAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) PreciseProductsConstants.DesignationAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) PreciseProductsConstants.NameAttributeTypeID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_ELEMENT_STATUSES
        }
      };
      DataTable configurableComposition = this.FindConfigurableComposition(rootObjectVersionID, rootObjectTypeID, relationID, objectVersionID, PreciseProductsConstants.ProductCompositionRelationTypeID, columns);
      DataTable composition = this.FindComposition(objectVersionID, PreciseProductsConstants.ProductCompositionRelationTypeID);
      IElementStatusesService customService = sessionKeeper.Session.GetCustomService(typeof (IElementStatusesService)) as IElementStatusesService;
      bool productsBlanksInternal = configurableComposition.Rows.Count != composition.Rows.Count;
      foreach (DataRow row in (InternalDataCollectionBase) configurableComposition.Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        string stringValue = DataSetProcessor.GetStringValue(row, 1, (string) null);
        long partVersionID = DataSetProcessor.GetInt64Value(row, 2, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 3, -1);
        DataSetProcessor.GetStringValue(row, 4, (string) null);
        DataSetProcessor.GetStringValue(row, 5, (string) null);
        byte[] elementStatuses = row[8] as byte[];
        PdmConfiguratorStates elementStatuses16 = (PdmConfiguratorStates) customService.GetElementStatuses16("cad005f6-306c-11d8-b4e9-00304f19f545", elementStatuses);
        switch (elementStatuses16)
        {
          case PdmConfiguratorStates.None:
          case PdmConfiguratorStates.Configured:
            if (elementStatuses16 == PdmConfiguratorStates.Configured)
              productsBlanksInternal = true;
            if (!context.Any<Tuple<long, long>>((System.Func<Tuple<long, long>, bool>) (o => Math.Abs(o.Item2) == Math.Abs(partVersionID))))
            {
              List<Tuple<long, long>> list = context.ToList<Tuple<long, long>>();
              list.Add(new Tuple<long, long>(int64Value, partVersionID));
              if (PreciseProductsHelper.IsObjectTypeSuitableForCreatePreciseProduct(int32Value) && this.CreatePreciseProductsBlanksInternal(rootObjectVersionID, rootObjectTypeID, int64Value, partVersionID, preciseProductsBlanks, list))
              {
                productsBlanksInternal = true;
                PreciseProductBlank preciseProductBlank = new PreciseProductBlank(int64Value, partVersionID)
                {
                  ProductCaption = stringValue,
                  ProductObjectTypeID = int32Value,
                  PreciseProductDesignation = DataSetProcessor.GetStringValue(row, 6, (string) null),
                  PreciseProductName = DataSetProcessor.GetStringValue(row, 7, (string) null),
                  ProductDesignation = DataSetProcessor.GetStringValue(row, 6, (string) null)
                };
                preciseProductBlank.Context.AddRange((IEnumerable<Tuple<long, long>>) list);
                preciseProductsBlanks.Add(preciseProductBlank);
                continue;
              }
              continue;
            }
            continue;
          default:
            throw new Exception($"Ошибка создания точного изделия. Объект #{objectVersionID} \"{this.GetObjectNameInMessages(objectVersionID)}\" не сконфигурирован.");
        }
      }
      return productsBlanksInternal;
    }
  }

  private DataTable FindComposition(long objectVersionID, int relationTypeID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelationCollection(relationTypeID).ConsistFrom(new DBRecordSetParams()
      {
        Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID
        }
      }, objectVersionID);
  }

  private string GetObjectNameInMessages(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID, false);
      return dbObject != null ? dbObject.NameInMessages : string.Empty;
    }
  }

  private DataTable FindConfigurableComposition(
    long rootObjectVersionID,
    int rootObjectTypeID,
    long relationID,
    long objectVersionID,
    int relationTypeID,
    ColumnInfo[] columns)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersionID);
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = ((IEnumerable<ColumnInfo>) columns).Select<ColumnInfo, object>((System.Func<ColumnInfo, object>) (o => o.AttributeID)).ToArray<object>(),
        ColumnsInfo = columns,
        Tags = new HybridDictionary()
        {
          {
            (object) "{78D53C74-3CF7-4F48-94FC-80C4FCB0BA77}",
            (object) new RelationPair(0L, rootObjectVersionID, rootObjectTypeID, relationID, sessionKeeper.Session.UserID, objectVersionID, relationTypeID, dbObject.ObjectType, false)
          }
        }
      };
      bool enabledPdmConfigurator = sessionKeeper.Session.EnabledPdmConfigurator;
      try
      {
        if (!sessionKeeper.Session.EnabledPdmConfigurator)
          sessionKeeper.Session.EnabledPdmConfigurator = true;
        return relationCollection.ConsistFrom(paramSet, objectVersionID);
      }
      finally
      {
        if (enabledPdmConfigurator != sessionKeeper.Session.EnabledPdmConfigurator)
          sessionKeeper.Session.EnabledPdmConfigurator = enabledPdmConfigurator;
      }
    }
  }

  private void CopyProductComposition(
    CreatePreciseProductParams @params,
    long objectVersionID,
    PreciseProductsServerService.CreatePreciseProductContext createPreciseProductContext)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(PreciseProductsConstants.ProductCompositionRelationTypeID);
      ColumnInfo[] columns = new ColumnInfo[4]
      {
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          AttributeSource = AttributeSourceTypes.Object
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.F_PART_ID,
          AttributeSource = AttributeSourceTypes.Relation
        },
        new ColumnInfo()
        {
          AttributeID = (object) ObligatoryObjectAttributes.CAPTION,
          AttributeSource = AttributeSourceTypes.Object
        }
      };
      foreach (DataRow row in (InternalDataCollectionBase) this.FindConfigurableComposition(createPreciseProductContext.RootObjectVersionID, createPreciseProductContext.RootObjectTypeID, @params.RelationID, @params.ProductVersionID, PreciseProductsConstants.ProductCompositionRelationTypeID, columns).Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        long partID = DataSetProcessor.GetInt64Value(row, 2, 0L);
        if (this.GetPreciseProductBlank(@params.Blanks, int64Value1, int64Value2) != null)
          partID = this.CreatePreciseProductInternal(new CreatePreciseProductParams(int64Value1, int64Value2)
          {
            Blanks = @params.Blanks,
            CopyDocumentation = @params.CopyDocumentation,
            KeepCheckedOutCreatedObjects = @params.KeepCheckedOutCreatedObjects,
            SpecificationArchiveVersionID = @params.SpecificationArchiveVersionID,
            UseExistsProducts = @params.UseExistsProducts
          }, createPreciseProductContext).ID;
        this.ClearPdmConfiguratorAttributes((IDBAttributable) relationCollection.Create(new NewRelationProperties(int64Value1, objectVersionID, partID)
        {
          PartObjectID = int64Value2
        }));
      }
    }
  }

  private IDBObject FindObjectWithDesignation(int objectTypeId, string designation)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(objectTypeId);
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) Constants.DesignationAttributeTypeID,
          RelationalOperator = RelationalOperators.Equal,
          Value = (object) designation,
          SQL = string.Empty
        }
      };
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataTable dataTable = objectCollection.Select(paramSet);
      if (dataTable.Rows.Count == 0)
        return (IDBObject) null;
      long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[0], 0, 0L);
      return ObjectHelper.IsUnknownObjectVersionID(int64Value) ? (IDBObject) null : sessionKeeper.Session.GetObject(int64Value, false);
    }
  }

  private IDBObject CreatePreciseProductInternal(
    CreatePreciseProductParams @params,
    PreciseProductsServerService.CreatePreciseProductContext createPreciseAssemlyUnitContext,
    bool skipDesignationCheck = false)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      PreciseProductBlank preciseProductBlank = this.GetPreciseProductBlank(@params.Blanks, @params.RelationID, @params.ProductVersionID);
      if (!skipDesignationCheck && @params.UseExistsProducts)
      {
        IDBObject objectWithDesignation = this.FindObjectWithDesignation(preciseProductBlank.ProductObjectTypeID, preciseProductBlank.PreciseProductDesignation);
        if (objectWithDesignation != null)
          return objectWithDesignation;
      }
      IDBObject product = sessionKeeper.Session.GetObject(@params.ProductVersionID);
      IDBObject preciseProduct = sessionKeeper.Session.GetObjectCollection(product.ObjectType).Create();
      this.CopyAttributes(product, preciseProduct);
      preciseProduct.SetAttributesValues(new AttributeValues[3]
      {
        new AttributeValues(PreciseProductsConstants.DesignationAttributeTypeID)
        {
          Values = new object[1]
          {
            (object) preciseProductBlank.PreciseProductDesignation
          }
        },
        new AttributeValues(PreciseProductsConstants.NameAttributeTypeID)
        {
          Values = new object[1]
          {
            (object) preciseProductBlank.PreciseProductName
          }
        },
        new AttributeValues(PreciseProductsConstants.AppliedPdmConfiguratorOptionsAttributeTypeID)
        {
          Values = new object[1]
          {
            (object) this.GetAppliedPdmConfiguratorOptionsInfo(preciseProductBlank)
          },
          ThrowSetException = false
        }
      });
      this.CopyProductComposition(@params, preciseProduct.ObjectID, createPreciseAssemlyUnitContext);
      if (@params.CopyDocumentation)
      {
        this.CopyDesignDocumentation(preciseProductBlank.ProductVersionID, preciseProduct.ObjectID, @params, createPreciseAssemlyUnitContext);
        this.CopyTechnologicalComposition(preciseProductBlank.ProductVersionID, preciseProduct.ObjectID, @params, createPreciseAssemlyUnitContext);
      }
      preciseProduct.CommitCreation(true, true);
      IDBObject preciseProductInternal = sessionKeeper.Session.GetObject(preciseProduct.ObjectID, false);
      if (!@params.KeepCheckedOutCreatedObjects && preciseProductInternal.ObjectModifyMode == ObjectModifyModes.Checkout)
        preciseProductInternal.CheckIn();
      createPreciseAssemlyUnitContext.CreatedPresiceProductVersionIDDictionaryByCompositionPartID.Add(new Tuple<long, long>(preciseProductBlank.RelationID, preciseProductBlank.ProductVersionID), preciseProductInternal.ObjectID);
      return preciseProductInternal;
    }
  }

  private string GetAppliedPdmConfiguratorOptionsInfo(PreciseProductBlank preciseProductBlank)
  {
    string configuratorOptionsInfo = "";
    List<Tuple<long, long>> list = preciseProductBlank.Context.ToList<Tuple<long, long>>();
    list.Add(new Tuple<long, long>(preciseProductBlank.RelationID, preciseProductBlank.ProductVersionID));
    List<Guid> configuratorOptionGuids = this.GetVisiblePdmConfiguratorOptionGuids(preciseProductBlank.ProductVersionID);
    Guid guid1 = configuratorOptionGuids.LastOrDefault<Guid>();
    Dictionary<Guid, string> configuratorOptionValueIds = this.GetPdmConfiguratorOptionValueIds(list);
    foreach (Guid guid2 in configuratorOptionGuids)
    {
      OptionHolder option = PdmConfiguratorCache.CacheFindOption(guid2);
      string str = (string) null;
      if (configuratorOptionValueIds.TryGetValue(guid2, out str))
      {
        OptionValue optionValue = option.OptionValues.FindValue(str);
        if (optionValue != null)
        {
          configuratorOptionsInfo += !string.IsNullOrEmpty(optionValue.Code) ? optionValue.Code : "{Код значения опции не указан}";
          configuratorOptionsInfo += " ";
          configuratorOptionsInfo += !string.IsNullOrEmpty(option.OptionCaption) ? option.OptionCaption : "{Название опции не указано}";
          configuratorOptionsInfo += " - ";
          configuratorOptionsInfo += !string.IsNullOrEmpty(optionValue.Value) ? optionValue.Value : "{Значение опции не указано}";
          if (guid2 != guid1)
            configuratorOptionsInfo = $"{configuratorOptionsInfo};{Environment.NewLine}";
        }
      }
    }
    return configuratorOptionsInfo;
  }

  private List<Guid> GetVisiblePdmConfiguratorOptionGuids(long assemblyVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return new ObjectOptionsHolder((object) sessionKeeper.Session.GetObject(assemblyVersionID)).VisibleOptionValues.Items.Keys.Distinct<Guid>().ToList<Guid>();
  }

  private Dictionary<Guid, string> GetPdmConfiguratorOptionValueIds(
    List<Tuple<long, long>> contextAndSelf)
  {
    Dictionary<Guid, string> configuratorOptionValueIds = new Dictionary<Guid, string>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (Tuple<long, long> tuple in contextAndSelf)
      {
        foreach (KeyValuePair<Guid, string> optionsValue in new PdmConfiguratorContext((object) sessionKeeper.Session.GetRelation(tuple.Item1)).OptionsValues)
        {
          if (!configuratorOptionValueIds.ContainsKey(optionsValue.Key))
            configuratorOptionValueIds.Add(optionsValue.Key, optionsValue.Value);
          else
            configuratorOptionValueIds[optionsValue.Key] = optionsValue.Value;
        }
      }
    }
    return configuratorOptionValueIds;
  }

  private PreciseProductBlank GetPreciseProductBlank(
    List<PreciseProductBlank> blanks,
    long relationID,
    long assemblyVersionID)
  {
    return blanks.Where<PreciseProductBlank>((System.Func<PreciseProductBlank, bool>) (o => Math.Abs(o.RelationID) == Math.Abs(relationID) && Math.Abs(o.ProductVersionID) == Math.Abs(assemblyVersionID))).FirstOrDefault<PreciseProductBlank>();
  }

  private void CopyAttributes(IDBObject product, IDBObject preciseProduct)
  {
    preciseProduct.Attributes.Assign(product.Attributes);
    this.ClearPdmConfiguratorAttributes((IDBAttributable) preciseProduct);
    this.ClearProductGroupIDAttribute(preciseProduct);
  }

  private void ClearPdmConfiguratorAttributes(IDBAttributable attributable)
  {
    foreach (int configuratorAttributeId in this.GetPdmConfiguratorAttributeIds())
      attributable.Attributes.FindByID(configuratorAttributeId)?.Delete(0L);
  }

  private void ClearProductGroupIDAttribute(IDBObject product)
  {
    IDBAttribute byId = product.Attributes.FindByID(PreciseProductsConstants.ProductGroupIDAttributeTypeID);
    if (byId == null)
      return;
    byId.Value = (object) null;
  }

  private List<int> GetPdmConfiguratorAttributeIds()
  {
    return MetaDataHelper.GetAttributesInGroup(PreciseProductsConstants.PdmConfiguratorAttributeGroupID);
  }

  private void CopyTechnologicalComposition(
    long prototypeVersionID,
    long objectVersionID,
    CreatePreciseProductParams createPreciseProductParams,
    PreciseProductsServerService.CreatePreciseProductContext createPreciseAssemlyUnitContext)
  {
    this.CopyRelations(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), prototypeVersionID, objectVersionID, createPreciseProductParams, createPreciseAssemlyUnitContext, new List<int>()
    {
      PreciseProductsConstants.ProcessingRouteObjectTypeID
    });
  }

  private void CopyDesignDocumentation(
    long prototypeVersionID,
    long objectVersionID,
    CreatePreciseProductParams createPreciseProductParams,
    PreciseProductsServerService.CreatePreciseProductContext createPreciseAssemlyUnitContext)
  {
    this.CopyRelations(MetaDataHelper.GetRelationTypeID("cad00154-306c-11d8-b4e9-00304f19f545"), prototypeVersionID, objectVersionID, createPreciseProductParams, createPreciseAssemlyUnitContext, new List<int>()
    {
      PreciseProductsConstants.SpecificationObjectTypeID,
      PreciseProductsConstants.AssemblyUnitModelObjectTypeID
    });
  }

  private void CopyRelations(
    int relationTypeID,
    long prototypeVersionID,
    long objectVersionID,
    CreatePreciseProductParams createPreciseProductParams,
    PreciseProductsServerService.CreatePreciseProductContext createPreciseAssemlyUnitContext,
    List<int> ignoredObjectTypeIds = null)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
      List<int> intList = new List<int>();
      if (ignoredObjectTypeIds != null)
      {
        intList.AddRange((IEnumerable<int>) ignoredObjectTypeIds);
        foreach (int ignoredObjectTypeId in ignoredObjectTypeIds)
          intList.AddRange((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(ignoredObjectTypeId));
      }
      relationCollection.LocalTypesMode = true;
      ColumnInfo[] columnInfoArray = new ColumnInfo[4];
      ColumnInfo columnInfo = new ColumnInfo();
      columnInfo.AttributeID = (object) ObligatoryObjectAttributes.F_PRJLINK_ID;
      columnInfo.AttributeSource = AttributeSourceTypes.Relation;
      columnInfoArray[0] = columnInfo;
      columnInfo = new ColumnInfo();
      columnInfo.AttributeID = (object) ObligatoryObjectAttributes.F_PART_ID;
      columnInfo.AttributeSource = AttributeSourceTypes.Relation;
      columnInfoArray[1] = columnInfo;
      columnInfo = new ColumnInfo();
      columnInfo.AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE;
      columnInfo.AttributeSource = AttributeSourceTypes.Object;
      columnInfoArray[2] = columnInfo;
      columnInfo = new ColumnInfo();
      columnInfo.AttributeID = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
      columnInfo.AttributeSource = AttributeSourceTypes.Object;
      columnInfoArray[3] = columnInfo;
      ColumnInfo[] columns = columnInfoArray;
      foreach (DataRow row in (InternalDataCollectionBase) this.FindConfigurableComposition(createPreciseAssemlyUnitContext.RootObjectVersionID, createPreciseAssemlyUnitContext.RootObjectTypeID, createPreciseProductParams.RelationID, createPreciseProductParams.ProductVersionID, relationTypeID, columns).Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(row, 2, -1);
        long int64Value3 = DataSetProcessor.GetInt64Value(row, 3, 0L);
        if (!intList.Contains(int32Value))
        {
          IDBRelation attributable = relationCollection.Create(new NewRelationProperties(int64Value1, objectVersionID, int64Value2)
          {
            PartObjectID = int64Value3
          });
          if (attributable != null)
            this.ClearPdmConfiguratorAttributes((IDBAttributable) attributable);
        }
      }
    }
  }

  private sealed class CreatePreciseProductContext
  {
    public CreatePreciseProductContext(long rootObjectVersionID, int rootObjectTypeID)
    {
      if (ObjectHelper.IsUnknownObjectVersionID(rootObjectVersionID))
        throw new ArgumentException();
      if (rootObjectTypeID == -1)
        throw new ArgumentException();
      this.RootObjectVersionID = rootObjectVersionID;
      this.RootObjectTypeID = rootObjectTypeID;
      this.CreatedPresiceProductVersionIDDictionaryByCompositionPartID = new Dictionary<Tuple<long, long>, long>();
    }

    public long RootObjectVersionID { get; private set; }

    public int RootObjectTypeID { get; private set; }

    public Dictionary<Tuple<long, long>, long> CreatedPresiceProductVersionIDDictionaryByCompositionPartID { get; private set; }
  }
}
