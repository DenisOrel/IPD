// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.LanguageCodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal class LanguageCodeFormer : CodeFormer
{
  public LanguageCodeFormer()
    : base(9)
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
    IDBLanguageType dbLanguageType = dbObject as IDBLanguageType;
    return new List<ScriptNode>()
    {
      (ScriptNode) new ObjectProperty4Script((object) "F_CULTURE_ID", DataSetProcessor.GetCaption("F_CULTURE_ID"), (object) dbLanguageType.CultureID),
      (ScriptNode) new ObjectProperty4Script((object) "F_DEFAULT", DataSetProcessor.GetCaption("F_DEFAULT"), (object) dbLanguageType.IsDefaultLanguage),
      (ScriptNode) new ObjectProperty4Script((object) "F_LANGUAGE_NAME", DataSetProcessor.GetCaption("F_LANGUAGE_NAME"), (object) dbLanguageType.LanguageName)
    };
  }
}
