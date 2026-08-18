// Decompiled with JetBrains decompiler
// Type: Intermech.Update.ObjectProperty4Script
// Assembly: Intermech.Update, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 825FBF29-0112-4B23-8140-950E091D8F10
// Assembly location: D:\IPS\Client\Intermech.Update.dll

#nullable disable
namespace Intermech.Update;

internal class ObjectProperty4Script : ScriptNode
{
  public object PropertyID;
  public object Value;
  public bool Obligatory;
  public bool Visible;

  public ObjectProperty4Script(object propertyID, string caption, object value)
    : base(caption)
  {
    this.PropertyID = propertyID;
    this.Value = value;
    this.Obligatory = false;
    this.Visible = true;
  }

  public ObjectProperty4Script(object propertyID, string caption, object value, bool obligatory)
    : base(caption)
  {
    this.PropertyID = propertyID;
    this.Value = value;
    this.Obligatory = obligatory;
    this.Visible = true;
  }

  public ObjectProperty4Script(
    object propertyID,
    string caption,
    object value,
    bool obligatory,
    bool visible)
    : this(propertyID, caption, value, obligatory)
  {
    this.Visible = visible;
  }
}
