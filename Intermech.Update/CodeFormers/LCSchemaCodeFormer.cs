// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.LCSchemaCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class LCSchemaCodeFormer : CodeFormer
{
  public LCSchemaCodeFormer()
    : base(16 /*0x10*/)
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    object Tag = (object) null;
    if (obj.Tag != null)
      Tag = obj.Tag;
    IDBLCSchema lcSchema = session.GetLCSchema((Guid) obj.ID);
    XmlNode node = this.CreateNode(xmlDocument, obj, Tag);
    if (node == null)
      return (XmlNode) null;
    foreach (ScriptNode property in obj.Properties)
    {
      string id = Convert.ToString((property as ObjectProperty4Script).PropertyID);
      object obj1;
      switch (id)
      {
        case "F_AREA_ID":
          obj1 = (object) this.GetSubjectAreaProperty(session, (string) (property as ObjectProperty4Script).Value);
          break;
        case "F_ACCESS":
          obj1 = (object) this.GetSecurity(session, lcSchema as IDBSecurity);
          break;
        case "F_DRAW_DATA":
          string path2_1 = $"draw{lcSchema.GUID.ToString().ToLower()}.dat";
          using (FileStream fileStream = new FileStream(Path.Combine(path4Files, path2_1), FileMode.Create, FileAccess.Write))
          {
            fileStream.Write(lcSchema.DrawData, 0, lcSchema.DrawData.Length);
            fileStream.Flush();
            fileStream.Close();
          }
          obj1 = (object) path2_1;
          this.temporaries.Enqueue(path2_1);
          break;
        case "F_SCHEMA_DATA":
          DataSet schema = lcSchema.GetStepsCollection().GetSchema();
          DataTable table = schema.Tables["IMS_LC_STEPS"];
          table.Columns.Add(new DataColumn("F_LEVEL_GUID", typeof (string)));
          foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
            row["F_LEVEL_GUID"] = (object) MetaDataHelper.GetLCLevelGuid(Convert.ToInt32(row["F_LEVEL_ID"])).ToString();
          table.AcceptChanges();
          string path2_2 = $"schema{lcSchema.GUID.ToString().ToLower()}.dat";
          BinaryFormatter binaryFormatter = new BinaryFormatter();
          schema.RemotingFormat = SerializationFormat.Binary;
          using (FileStream serializationStream = new FileStream(Path.Combine(path4Files, path2_2), FileMode.Create, FileAccess.Write))
          {
            binaryFormatter.Serialize((Stream) serializationStream, (object) schema);
            serializationStream.Flush();
            serializationStream.Close();
          }
          obj1 = (object) path2_2;
          this.temporaries.Enqueue(path2_2);
          break;
        default:
          obj1 = (property as ObjectProperty4Script).Value;
          break;
      }
      node.AppendChild(this.CreateProperty(xmlDocument, (property as ObjectProperty4Script).Obligatory, id, obj1));
    }
    return node;
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    IDBLCSchema dblcSchema = dbObject as IDBLCSchema;
    List<ScriptNode> properties = new List<ScriptNode>();
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NAME", DataSetProcessor.GetCaption("F_NAME"), (object) dblcSchema.Name));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_NOTE", DataSetProcessor.GetCaption("F_NOTE"), (object) dblcSchema.Note));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) dblcSchema.SchemaProperties.AreaID));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT", DataSetProcessor.GetCaption("F_DEFAULT"), (object) dblcSchema.IsDefaultSchema));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_DRAW_DATA", DataSetProcessor.GetCaption("F_DRAW_DATA"), (object) null));
    foreach (LCSchemaOptions lcSchemaOptions in Enum.GetValues(typeof (LCSchemaOptions)))
    {
      if (lcSchemaOptions != LCSchemaOptions.None)
        properties.Add((ScriptNode) new ObjectProperty4Script((object) $"{"F_OPTIONS"}{(int) lcSchemaOptions}", EnumDescConverter.GetEnumDescription((Enum) lcSchemaOptions), (object) (int) (dblcSchema.Options & lcSchemaOptions)));
    }
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_SCHEMA_DATA", "Схема", (object) null));
    properties.Add((ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null));
    return properties;
  }
}
