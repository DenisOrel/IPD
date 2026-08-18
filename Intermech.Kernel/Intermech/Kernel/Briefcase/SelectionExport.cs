// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.SelectionExport
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Briefcase;

public class SelectionExport : ICategoryExport
{
  private static int _conditionAttrID = -1;

  public string ExporterName => "Kernel.SelectionExport";

  public long[] GetLinkedObjectVersions(IUserSession session, int category, object id)
  {
    if (category == 1)
    {
      List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00156-306c-11d8-b4e9-00304f19f545"));
      IDBObject dbObject = session.GetObject((long) id);
      int objectType = dbObject.ObjectType;
      if (childrenIdRecursive.Contains(objectType))
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad00155-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && attributeByGuid.AsBoolean)
        {
          DataTable dataTable = session.ObjectsSelect(-1, new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(0, RelationalOperators.InSelection, (object) dbObject.ObjectID, LogicalOperators.AND, 0, false)
          }, new object[1]{ (object) -2 }));
          if (dataTable.Rows.Count > 0)
          {
            List<long> longList = new List<long>(dataTable.Rows.Count);
            for (int index = 0; index < dataTable.Rows.Count; ++index)
              longList.Add(Convert.ToInt64(dataTable.Rows[0][0]));
            return longList.ToArray();
          }
        }
      }
    }
    return (long[]) null;
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    return (ExportAttribute[]) null;
  }

  public bool ProcessShortBlobs => false;

  public static void Init()
  {
    SelectionExport._conditionAttrID = MetaDataHelper.GetAttributeTypeID("cad0069b-306c-11d8-b4e9-00304f19f545");
    ICategoryExportManager service;
    if ((service = ServerServices.ServiceContainer.GetService<ICategoryExportManager>()) == null)
      return;
    ICategoryExport iCategoryExport = (ICategoryExport) new SelectionExport();
    service.RegisterCategoryExport(1, iCategoryExport);
  }
}
