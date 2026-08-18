// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.LCLevelCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class LCLevelCodeFormer : CodeFormer
{
  public LCLevelCodeFormer()
    : base(8)
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
    IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel((Guid) obj.ID);
    XmlNode node1 = this.CreateNode(xmlDocument, obj, Tag);
    if (node1 == null)
      return (XmlNode) null;
    foreach (ScriptNode property in obj.Properties)
    {
      if (property is ObjectProperty4Script)
      {
        string id = Convert.ToString((property as ObjectProperty4Script).PropertyID);
        object obj1 = (object) null;
        switch (id)
        {
          case "F_ICON":
            if (CompareValuesHelper.NormalizedValue((property as ObjectProperty4Script).Value) != null)
            {
              byte[] buffer = (byte[]) (property as ObjectProperty4Script).Value;
              string path2 = $"icon{8}{lifecycleLevel.GUID.ToString().ToLower()}.dat";
              FileStream fileStream = new FileStream(Path.Combine(path4Files, path2), FileMode.Create, FileAccess.Write);
              try
              {
                fileStream.Write(buffer, 0, buffer.Length);
              }
              finally
              {
                fileStream.Flush();
                fileStream.Close();
              }
              obj1 = (object) path2;
              this.temporaries.Enqueue(path2);
              break;
            }
            break;
          case "F_AREA_ID":
            obj1 = (object) Convert.ToString((property as ObjectProperty4Script).Value).Trim();
            break;
          case "F_ACCESS":
            obj1 = (object) this.GetSecurity(session, lifecycleLevel as IDBSecurity);
            break;
          default:
            obj1 = (property as ObjectProperty4Script).Value;
            break;
        }
        node1.AppendChild(this.CreateProperty(xmlDocument, (property as ObjectProperty4Script).Obligatory, id, obj1));
      }
      else if (property is Object4Script)
      {
        XmlNode node2 = this.GenerateNode(session, xmlDocument, property as Object4Script, path4Files);
        if (node2 != null)
          node1.AppendChild(node2);
      }
    }
    return node1;
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    IDBLifecycleLevelType lifecycleLevelType = dbObject as IDBLifecycleLevelType;
    return new List<ScriptNode>()
    {
      (ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT", DataSetProcessor.GetCaption("F_DEFAULT"), (object) lifecycleLevelType.IsDefaultLevel),
      (ScriptNode) new ObjectProperty4Script((object) "F_ICON", DataSetProcessor.GetCaption("F_ICON"), (object) lifecycleLevelType.LevelIcon),
      (ScriptNode) new ObjectProperty4Script((object) "F_LEVEL_NAME", DataSetProcessor.GetCaption("F_LEVEL_NAME"), (object) lifecycleLevelType.LevelName),
      (ScriptNode) new ObjectProperty4Script((object) "F_LITERA", DataSetProcessor.GetCaption("F_LITERA"), (object) lifecycleLevelType.Litera),
      (ScriptNode) new ObjectProperty4Script((object) "F_AREA_ID", DataSetProcessor.GetCaption("F_AREA_ID"), (object) (lifecycleLevelType as IDBSubjectArea).SubjectAreas),
      (ScriptNode) new ObjectProperty4Script((object) "F_ACCESS", UpdateScriptHelper.AccessNodeText, (object) null)
    };
  }
}
