// Decompiled with JetBrains decompiler
// Type: Intermech.Security.SecurityNode
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Security;

public class SecurityNode : CompositeNode
{
  private static DescriptorCollection _securityItems;
  private const int UsersGroupsOrderID = 10;
  private const int RolesOrderID = 20;
  private const int PluginsOrderID = 30;
  private const int StoragesOrderID = 40;
  private const int EventLogOrderID = 50;

  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new DescriptorsPart(SecurityNode.SecurityItems));
  }

  private static DescriptorCollection SecurityItems
  {
    get
    {
      if (SecurityNode._securityItems == null)
      {
        SecurityNode._securityItems = new DescriptorCollection();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IIDHelper identHelper = sessionKeeper.Session.IdentHelper;
          SecurityNode._securityItems.Add(new Guid("F7CF74AD-58C6-4fbb-8340-F44ABADB12A4"), (IDescriptor) new UsersGroupsDescriptor());
          SecurityNode._securityItems.Add(new Guid("9B58840E-3F2D-4740-AC79-E970135C2987"), (IDescriptor) new MeasuresDescriptor());
          SecurityNode._securityItems.Add(new Guid("65D31552-5F7E-4d90-BB63-34CB72A68BA7"), (IDescriptor) new Intermech.Security.EventLog.Descriptor());
          DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(-1, true).Select("F_OBJ_TYPE_NAME");
          string str = sessionKeeper.Session.GetSubjectAreaType(new Guid("cad002d8-306c-11d8-b4e9-00304f19f545")).AreaID.ToString();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
            if (row["F_AREA_ID"].ToString().Contains(str) && int32 != identHelper.GroupsTypeID && int32 != identHelper.UsersTypeID && int32 != identHelper.MeasureTypeID && int32 != identHelper.PhysicValueTypeID)
            {
              Guid descriptorGuid = new Guid(row["F_GUID"].ToString());
              IDescriptor descriptor = (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(int32);
              SecurityNode._securityItems.Add(descriptorGuid, descriptor);
            }
          }
        }
      }
      return SecurityNode._securityItems;
    }
  }
}
