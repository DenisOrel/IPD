// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Interfaces.Entities.IDigitalDocument
// Assembly: Intermech.GTC.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 767EAE12-F30F-454C-81D0-2862AEDD13C4
// Assembly location: D:\IPS\Client\Intermech.GTC.Interfaces.dll

#nullable disable
namespace Intermech.GTC.Interfaces.Entities;

public interface IDigitalDocument : IBaseObject
{
  IDocumentVersion DocumentVersion { get; }

  string Description { get; }

  string Id { get; }

  IDocumentLocationProperty[] CommonLocation { get; }

  IDocumentFormatProperty RepresentationFromat { get; }

  IDigitalFile[] File { get; }

  IDocumentCreationProperty Creation { get; }

  IDocumentContentProperty Content { get; }
}
