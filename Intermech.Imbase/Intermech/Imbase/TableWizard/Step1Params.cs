// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.Step1Params
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.Imbase.TableWizard;

[Serializable]
internal class Step1Params
{
  internal Step1Params(int button, long tableID)
  {
    this.Button = button;
    this.TableID = tableID;
    if (tableID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(tableID);
      if (dbObject.ObjectType != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
      if (attributeById == null)
        return;
      this.TableID = Convert.ToInt64(attributeById.Value);
    }
  }

  internal int Button { get; set; }

  internal long TableID { get; set; }

  public override bool Equals(object obj)
  {
    if (!(obj is Step1Params step1Params))
      return base.Equals(obj);
    return this.Button == step1Params.Button && this.TableID == step1Params.TableID;
  }

  public override int GetHashCode() => this.TableID.GetHashCode();
}
