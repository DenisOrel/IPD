// Decompiled with JetBrains decompiler
// Type: Intermech.Update.Object4Script
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Update;

internal class Object4Script : ScriptNode
{
  public int CategoryID;
  public object ID;
  public object Tag;
  public List<ScriptNode> Properties;

  public Object4Script(int categoryID, object id, string caption)
    : base(caption)
  {
    this.CategoryID = categoryID;
    this.ID = id;
    this.Properties = new List<ScriptNode>();
    this.Tag = (object) null;
  }
}
