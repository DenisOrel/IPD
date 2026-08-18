// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Pdm.PreciseProducts.PreciseProductBlank
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Search.Pdm.PreciseProducts;

[Serializable]
public sealed class PreciseProductBlank : INotifyPropertyChanged
{
  private string _productCaption;
  private string _preciseProductDesignation;
  private string _preciseProductName;

  public PreciseProductBlank(long relationID, long productVersionID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(productVersionID))
      throw new ArgumentException();
    this.RelationID = relationID;
    this.ProductVersionID = productVersionID;
    this.Context = new List<Tuple<long, long>>();
  }

  public long RelationID { get; private set; }

  public long ProductVersionID { get; private set; }

  public List<Tuple<long, long>> Context { get; private set; }

  public int ProductObjectTypeID { get; set; }

  public string ProductCaption { get; set; }

  public string ProductDesignation { get; set; }

  public string PreciseProductDesignation
  {
    get => this._preciseProductDesignation;
    set
    {
      if (!(this._preciseProductDesignation != value))
        return;
      this._preciseProductDesignation = value;
      this.OnPropertyChanged(nameof (PreciseProductDesignation));
    }
  }

  public string PreciseProductName
  {
    get => this._preciseProductName;
    set
    {
      if (!(this._preciseProductName != value))
        return;
      this._preciseProductName = value;
      this.OnPropertyChanged(nameof (PreciseProductName));
    }
  }

  public event PropertyChangedEventHandler PropertyChanged;

  private void OnPropertyChanged(string propertyName)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
