// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetadataUpdates.IPropertyFactory
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.MetadataUpdates;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Kernel.Services.MetadataUpdates;

internal interface IPropertyFactory
{
  void Read(IUserSession session, XmlNode rootNode);

  List<ObligatoryElementKey> ObligatoryElements { get; }

  TValue GetPropertyValue<TValue>(string propertyName);

  TValue GetPropertyValue<TValue>(string propertyName, TValue defaultValue);

  TValue GetObligatoryPropertyValue<TValue>(string propertyName, TValue defaultValue);

  bool IsPropertyObligatory(string propertyName);

  string Directory { get; set; }
}
