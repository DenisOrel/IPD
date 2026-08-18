// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.FolderNameSurrogate
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Наименование папки в каталоге проекта, содержимое которого не импортируется в базу IPS
/// </summary>
public sealed class FolderNameSurrogate : ICloneable
{
  private string _folderName;

  [DisplayName("Наименование папки")]
  [Description("Наименование папки в каталоге проекта, содержимое которого не импортируется в базу IPS.")]
  public string FolderName
  {
    get => this._folderName;
    set => this._folderName = value;
  }

  public FolderNameSurrogate Clone()
  {
    return new FolderNameSurrogate()
    {
      _folderName = this._folderName
    };
  }

  object ICloneable.Clone() => (object) this.Clone();

  public override string ToString() => this.FolderName;

  public override int GetHashCode() => this._folderName.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is FolderNameSurrogate folderNameSurrogate))
      return base.Equals(obj);
    return !(folderNameSurrogate._folderName != this._folderName);
  }
}
