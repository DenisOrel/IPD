// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.Elements
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using System;

#nullable disable
namespace Intermech.Imbase.Views;

internal struct Elements(string caption, long objID, int id) : IComparable
{
  private string _caption = caption;
  private long _ObjID = objID;
  private int _TypeID = id;

  public string Caption => this._caption;

  public long ObjID => this._ObjID;

  public int TypeID => this._TypeID;

  public int CompareTo(object obj)
  {
    if (!(obj is Elements elements))
      throw new ArgumentException(sc_7952.ssp_imbase_7953());
    int num = this._caption.CompareTo(elements.Caption);
    if (this._TypeID == elements.TypeID)
      return num;
    return this._TypeID == Intermech.Imbase.Consts.ImbaseFolderTypeID ? -1 : 1;
  }
}
