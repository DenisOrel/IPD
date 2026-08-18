// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.LCSchemaNodeReader
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal sealed class LCSchemaNodeReader(
  XmlNode node,
  IUserSession userSession,
  IEventLogHelper eHelper,
  string curDirectory,
  IObligatoryObjectsRegistryService obligatoryObjects,
  Guid schemaGuid) : NodeReader(node, userSession, eHelper, curDirectory, obligatoryObjects, schemaGuid, (IPropertyFactory) new LCSchemaPropertyFactory())
{
  protected override void OnRead(out int categoryID, out object id)
  {
    DBLCSchemaProperties properties1 = ((LCSchemaPropertyFactory) this.propertyFactory).Properties with
    {
      GUID = this.GUID
    };
    byte[] propertyValue1 = this.propertyFactory.GetPropertyValue<byte[]>("F_DRAW_DATA", (byte[]) null);
    DataSet propertyValue2 = this.propertyFactory.GetPropertyValue<DataSet>("F_SCHEMA_DATA", (DataSet) null);
    List<UpdateScriptAccessRight> propertyValue3 = this.propertyFactory.GetPropertyValue<List<UpdateScriptAccessRight>>("F_ACCESS", (List<UpdateScriptAccessRight>) null);
    IDBLCSchema lcSchema = this.session.GetLCSchema(this.GUID, false);
    if (lcSchema == null)
    {
      IDBLCSchemaCollection schemaCollection = this.session.GetLCSchemaCollection();
      properties1.CreateEmptySchema = true;
      DBLCSchemaProperties properties2 = properties1;
      lcSchema = this.session.GetLCSchema(schemaCollection.Create(properties2));
      DataTable table1 = propertyValue2.Tables["IMS_LC_STEPS"];
      foreach (DataRow row in (InternalDataCollectionBase) table1.Rows)
      {
        row["F_SCHEMA_ID"] = (object) lcSchema.SchemaID;
        row["F_LC_STEP"] = (object) (-1 * Convert.ToInt32(row["F_LC_STEP"]));
        IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(new Guid(Convert.ToString(row["F_LEVEL_GUID"])), true);
        row["F_LEVEL_ID"] = (object) lifecycleLevel.LevelID;
      }
      table1.Columns.Remove("F_LEVEL_GUID");
      table1.AcceptChanges();
      DataTable table2 = propertyValue2.Tables["IMS_LC_LINKS"];
      foreach (DataRow row in (InternalDataCollectionBase) table2.Rows)
      {
        row["F_FROM_STEP"] = (object) (-1 * Convert.ToInt32(row["F_FROM_STEP"]));
        row["F_TO_STEP"] = (object) (-1 * Convert.ToInt32(row["F_TO_STEP"]));
      }
      table2.AcceptChanges();
      lcSchema.GetStepsCollection().SetSchema(propertyValue2);
      lcSchema.DrawData = propertyValue1;
      this.SetAccess(lcSchema as IDBSecurity, propertyValue3, 16 /*0x10*/, Convert.ToInt64(lcSchema.SchemaID));
    }
    else
    {
      lcSchema.Name = this.propertyFactory.GetPropertyValue<string>("F_NAME", lcSchema.Name);
      lcSchema.Note = this.propertyFactory.GetPropertyValue<string>("F_NOTE", lcSchema.Note);
      lcSchema.IsDefaultSchema = this.propertyFactory.GetPropertyValue<bool>("F_DEFAULT", lcSchema.IsDefaultSchema);
      lcSchema.DrawData = this.propertyFactory.GetPropertyValue<byte[]>("F_DRAW_DATA", lcSchema.DrawData);
      (lcSchema as IDBSubjectArea).SubjectAreas = this.propertyFactory.GetPropertyValue<string>("F_AREA_ID", (lcSchema as IDBSubjectArea).SubjectAreas);
      if (this.propertyFactory.IsPropertyObligatory("F_SCHEMA_DATA"))
      {
        DataTable table3 = propertyValue2.Tables["IMS_LC_STEPS"];
        HashSet<int> intSet = new HashSet<int>();
        Dictionary<int, int> dictionary = new Dictionary<int, int>();
        foreach (DataRow row in (InternalDataCollectionBase) table3.Rows)
        {
          row["F_SCHEMA_ID"] = (object) lcSchema.SchemaID;
          bool flag = true;
          IDBLifecycleStep lifecycleStep = this.session.GetLifecycleStep(new Guid(Convert.ToString(row["F_GUID"])), false);
          if (lifecycleStep != null && lifecycleStep.SchemaID.Equals(lcSchema.SchemaID))
          {
            dictionary.Add(Convert.ToInt32(row["F_LC_STEP"]), lifecycleStep.LCStep);
            row["F_LC_STEP"] = (object) lifecycleStep.LCStep;
            flag = false;
          }
          if (flag)
          {
            int int32 = Convert.ToInt32(row["F_LC_STEP"]);
            intSet.Add(int32);
            row["F_LC_STEP"] = (object) (-1 * int32);
          }
          IDBLifecycleLevelType lifecycleLevel = this.session.GetLifecycleLevel(new Guid(Convert.ToString(row["F_LEVEL_GUID"])), true);
          row["F_LEVEL_ID"] = (object) lifecycleLevel.LevelID;
        }
        table3.Columns.Remove("F_LEVEL_GUID");
        table3.AcceptChanges();
        DataTable table4 = propertyValue2.Tables["IMS_LC_LINKS"];
        foreach (DataRow row in (InternalDataCollectionBase) table4.Rows)
        {
          int int32_1 = Convert.ToInt32(row["F_FROM_STEP"]);
          int int32_2 = Convert.ToInt32(row["F_TO_STEP"]);
          row["F_FROM_STEP"] = intSet.Contains(int32_1) ? (object) (-1 * int32_1) : (object) dictionary[Convert.ToInt32(row["F_FROM_STEP"])];
          row["F_TO_STEP"] = intSet.Contains(int32_2) ? (object) (-1 * int32_2) : (object) dictionary[Convert.ToInt32(row["F_TO_STEP"])];
        }
        table4.AcceptChanges();
        IDBLifecycleStepCollection stepsCollection = lcSchema.GetStepsCollection();
        bool flag1 = false;
        DataSet schema = stepsCollection.GetSchema();
        DataTable table5 = schema.Tables["IMS_LC_STEPS"];
        DataTable table6 = schema.Tables["IMS_LC_LINKS"];
        if (table5.Rows.Count != table3.Rows.Count || table6.Rows.Count != table4.Rows.Count)
        {
          flag1 = true;
        }
        else
        {
          for (int index = 0; index < table5.Rows.Count; ++index)
          {
            DataRow row1 = table5.Rows[index];
            DataRow row2 = table3.Rows[index];
            if (Convert.ToInt32(row1["F_LC_STEP"]) != Convert.ToInt32(row2["F_LC_STEP"]))
            {
              flag1 = true;
              break;
            }
            if (!Convert.ToString(row1["F_LC_NAME"]).Equals(Convert.ToString(row2["F_LC_NAME"])) || !Convert.ToString(row1["F_NOTE"]).Equals(Convert.ToString(row2["F_NOTE"])) || !Convert.ToInt32(row1["F_ACCESS_TYPE"]).Equals(Convert.ToInt32(row2["F_ACCESS_TYPE"])) || !Convert.ToInt32(row1["F_DELETED"]).Equals(Convert.ToInt32(row2["F_DELETED"])) || !Convert.ToInt32(row1["F_MODIFY_MODE"]).Equals(Convert.ToInt32(row2["F_MODIFY_MODE"])) || !Convert.ToInt32(row1["F_FIRST"]).Equals(Convert.ToInt32(row2["F_FIRST"])) || !Convert.ToInt32(row1["F_OPTIONS"]).Equals(Convert.ToInt32(row2["F_OPTIONS"])))
              this.session.GetLifecycleStep(Convert.ToInt32(row1["F_LC_STEP"]), true).Properties = new DBLifecycleStepProperties(row2);
          }
          if (!flag1)
          {
            for (int index = 0; index < table6.Rows.Count; ++index)
            {
              if (Convert.ToInt32(table6.Rows[index]["F_FROM_STEP"]) != Convert.ToInt32(table4.Rows[index]["F_FROM_STEP"]) || Convert.ToInt32(table6.Rows[index]["F_TO_STEP"]) != Convert.ToInt32(table4.Rows[index]["F_TO_STEP"]))
              {
                flag1 = true;
                break;
              }
            }
          }
        }
        if (flag1)
          stepsCollection.SetSchema(propertyValue2);
      }
    }
    categoryID = 16 /*0x10*/;
    id = (object) lcSchema.SchemaID;
  }
}
