// Decompiled with JetBrains decompiler
// Type: Intermech.Update.CodeFormers.ICodeFormer
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using Intermech.Interfaces;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Update.CodeFormers;

internal interface ICodeFormer
{
  XmlNode GenerateNode(
    IUserSession session,
    XmlDocument xmlDocument,
    Object4Script obj,
    string path4Files);

  List<ScriptNode> GetProperties(IUserSession session, object dbObject);

  IEnumerable<string> TempFilePaths { get; }

  bool FailOnError { get; set; }

  List<string> Errors { get; set; }
}
