
// Type: Intermech.Search.Data.Repositories.CompositionRepository
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.Data.Repositories
{
    public sealed class CompositionRepository : ICompositionRepository
    {
      private LazyService<ITypeProvider> _typeProvider = new LazyService<ITypeProvider>();

      public List<CompositionPart> Find(
        long projectVersionID,
        int relationTypeID,
        int partTypeID,
        params ConditionStructure[] conditions)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(relationTypeID);
          IAttributeTypeForRelationRepository relationRepository = ServiceLocator.Get<IAttributeTypeForRelationRepository>();
          IAttributeTypeForObjectRepository objectRepository = ServiceLocator.Get<IAttributeTypeForObjectRepository>();
          List<int> intList1 = new List<int>();
          intList1.AddRange(new List<ObligatoryObjectAttributes>()
          {
            ObligatoryObjectAttributes.F_PRJLINK_ID,
            ObligatoryObjectAttributes.F_PROJ_ID,
            ObligatoryObjectAttributes.F_PART_ID,
            ObligatoryObjectAttributes.F_RELATION_TYPE
          }.Cast<int>());
          intList1.AddRange(relationRepository.Find(relationTypeID).Select<IMSAttribute4RelationType, int>((System.Func<IMSAttribute4RelationType, int>) (o => o.AttributeID)));
          List<int> intList2 = new List<int>();
          intList2.AddRange(new List<ObligatoryObjectAttributes>()
          {
            ObligatoryObjectAttributes.F_BASE_VERSION,
            ObligatoryObjectAttributes.F_CHKOUT_BY,
            ObligatoryObjectAttributes.F_GUID,
            ObligatoryObjectAttributes.F_ID,
            ObligatoryObjectAttributes.F_LC_STEP,
            ObligatoryObjectAttributes.F_LEVEL_ID,
            ObligatoryObjectAttributes.F_MODIFICATION_ID,
            ObligatoryObjectAttributes.F_MODIFY_DATE,
            ObligatoryObjectAttributes.F_OBJ_CREATE,
            ObligatoryObjectAttributes.F_OBJ_GUID,
            ObligatoryObjectAttributes.F_OBJECT_ID,
            ObligatoryObjectAttributes.F_OBJECT_TYPE,
            ObligatoryObjectAttributes.F_OWNER_ID,
            ObligatoryObjectAttributes.F_VERSION_ID,
            ObligatoryObjectAttributes.CAPTION
          }.Cast<int>());
          intList2.AddRange(objectRepository.Find(partTypeID).Select<IMSAttribute4ObjectType, int>((System.Func<IMSAttribute4ObjectType, int>) (o => o.AttributeID)));
          List<Intermech.Kernel.Search.ColumnInfo> columnInfoList = new List<Intermech.Kernel.Search.ColumnInfo>();
          columnInfoList.AddRange(intList1.Select<int, Intermech.Kernel.Search.ColumnInfo>((System.Func<int, Intermech.Kernel.Search.ColumnInfo>) (o => new Intermech.Kernel.Search.ColumnInfo((object) o, AttributeSourceTypes.Relation, (object) null))));
          columnInfoList.AddRange(intList2.Select<int, Intermech.Kernel.Search.ColumnInfo>((System.Func<int, Intermech.Kernel.Search.ColumnInfo>) (o => new Intermech.Kernel.Search.ColumnInfo((object) o, AttributeSourceTypes.Object, (object) null))));
          List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>();
          columnDescriptorList.AddRange(intList1.Select<int, ColumnDescriptor>((System.Func<int, ColumnDescriptor>) (o => new ColumnDescriptor((object) o, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0))));
          columnDescriptorList.AddRange(intList2.Select<int, ColumnDescriptor>((System.Func<int, ColumnDescriptor>) (o => new ColumnDescriptor((object) o, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0))));
          DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columnDescriptorList.ToArray());
          long projectID = projectVersionID;
          DateTime now = DateTime.Now;
          return this.CreateListFromDataTable(relationCollection.Select(paramSet, projectID, -1L, now, (IList<int>) new List<int>()
          {
            partTypeID
          }), intList1, intList2);
        }
      }

      public List<CompositionPart> Find(FindCompositionOptions options)
      {
        throw new NotImplementedException();
      }

      public int FindCount(long projectVersionID, int relationTypeID, int objectTypeID)
      {
        throw new NotImplementedException();
      }

      public int FindCount(FindCompositionPartCountOptions options)
      {
        throw new NotImplementedException();
      }

      private List<CompositionPart> CreateListFromDataTable(
        DataTable dataTable,
        List<int> relationAttributeTypeIds,
        List<int> objectAttributeTypeIds)
      {
        return dataTable.Rows.Cast<DataRow>().Select<DataRow, CompositionPart>((System.Func<DataRow, CompositionPart>) (o => this.CreateFromDataRow(o, relationAttributeTypeIds, objectAttributeTypeIds))).ToList<CompositionPart>();
      }

      private CompositionPart CreateFromDataRow(
        DataRow dataRow,
        List<int> relationAttributeTypeIds,
        List<int> objectAttributeTypeIds)
      {
        Relation relation = new Relation();
        IAttributeValueConverter attributeValueConverter = ServiceLocator.Get<IAttributeValueConverter>();
        for (int index = 0; index < relationAttributeTypeIds.Count; ++index)
        {
          int relationAttributeTypeId = relationAttributeTypeIds[index];
          object obj = attributeValueConverter.Convert(dataRow[index], relationAttributeTypeId);
          relation.Attributes.Add(new _Attribute(relationAttributeTypeId)
          {
            Value = obj
          });
        }
        AttributeCollection source = new AttributeCollection();
        for (int count = relationAttributeTypeIds.Count; count < relationAttributeTypeIds.Count + objectAttributeTypeIds.Count; ++count)
        {
          int objectAttributeTypeId = objectAttributeTypeIds[count - relationAttributeTypeIds.Count];
          object obj = attributeValueConverter.Convert(dataRow[count], objectAttributeTypeId);
          source.Add(new _Attribute(objectAttributeTypeId)
          {
            Value = obj
          });
        }
        _Object part = this.CreateObject((int) (source.Where<_Attribute>((System.Func<_Attribute, bool>) (o => o.TypeID == -7)).FirstOrDefault<_Attribute>() ?? throw new Exception()).Value);
        part.Attributes.AddRange((IEnumerable<_Attribute>) source);
        return new CompositionPart(relation, part);
      }

      private _Object CreateObject(int objectTypeID)
      {
        Type type = this._typeProvider.Value.GetObjectType(objectTypeID);
        if ((object) type == null)
          type = typeof (_Object);
        return Activator.CreateInstance(type) as _Object;
      }
    }
}
