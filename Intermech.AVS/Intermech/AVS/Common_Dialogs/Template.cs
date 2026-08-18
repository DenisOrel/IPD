// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.Template
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

internal class Template
{
  private long id;
  private Guid guid;
  private bool changed;

  public Template(long id, Guid guid)
  {
    this.id = id;
    this.guid = guid;
    this.changed = false;
  }

  public Template(long id, Guid guid, bool changed)
  {
    this.id = id;
    this.guid = guid;
    this.changed = changed;
  }

  public long Id
  {
    get => this.id;
    set
    {
      this.id = value;
      this.changed = true;
    }
  }

  public Guid Guid
  {
    get => this.guid;
    set
    {
      this.guid = value;
      this.changed = true;
    }
  }

  public bool Changed
  {
    get => this.changed;
    set => this.changed = value;
  }
}
