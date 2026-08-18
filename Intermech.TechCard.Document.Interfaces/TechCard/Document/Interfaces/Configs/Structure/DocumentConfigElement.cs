// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Structure.DocumentConfigElement
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Structure;

public abstract class DocumentConfigElement : IDocumentConfigElement, IAssignable, ICloneable
{
  public string Id { get; set; } = string.Empty;

  public abstract DocumentConfigElementType ElementType { get; }

  protected abstract IDocumentConfigElement CreateEmptyClone();

  public virtual object Clone()
  {
    IDocumentConfigElement emptyClone = this.CreateEmptyClone();
    if (emptyClone == null)
      return (object) emptyClone;
    emptyClone.Assign((object) this);
    return (object) emptyClone;
  }

  public virtual void Clear() => this.Id = string.Empty;

  public virtual void Assign(object source)
  {
    this.Clear();
    if (!(source is DocumentConfigElement documentConfigElement))
      return;
    this.Id = documentConfigElement.Id;
  }
}
