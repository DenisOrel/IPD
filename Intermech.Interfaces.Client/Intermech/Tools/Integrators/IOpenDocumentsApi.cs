// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IOpenDocumentsApi
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators;

public interface IOpenDocumentsApi
{
  IOpenDocument FindOpenDocument(string fullPath);

  IOpenDocument OpenDocument(string fullPath);

  IAttributeCodec GetCodec(IOpenDocument openDocument);

  IValueBagContainer GetAttributeContainer(IOpenDocument openDocument);

  void Save(IOpenDocument openDocument);

  void Close(IOpenDocument openDocument);
}
