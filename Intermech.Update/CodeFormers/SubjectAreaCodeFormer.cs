// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.SubjectAreaCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class SubjectAreaCodeFormer : CodeFormer
{
  public SubjectAreaCodeFormer()
    : base(11)
  {
  }

  public override XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files)
  {
    return base.GenerateNode(session, xmlDocument, obj, path4Files);
  }

  public override List<ScriptNode> GetProperties(IUserSession session, object dbObject)
  {
    IDBSubjectAreaType dbSubjectAreaType = dbObject as IDBSubjectAreaType;
    return new List<ScriptNode>()
    {
      (ScriptNode) new ObjectProperty4Script((object) "F_AREA_NAME", DataSetProcessor.GetCaption("F_AREA_NAME"), (object) dbSubjectAreaType.AreaName),
      (ScriptNode) new ObjectProperty4Script((object) "F_AREA_NOTE", DataSetProcessor.GetCaption("F_AREA_NOTE"), (object) dbSubjectAreaType.Note)
    };
  }
}
