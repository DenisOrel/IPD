// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Modifiers.Constants
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;


namespace Intermech.Search.Data.Modifiers;

public static class Constants
{
  public static int VersionIDInCompositionAttributeTypeID
  {
    get => MetaDataHelper.GetAttributeTypeID(new Guid("cad001c2-306c-11d8-b4e9-00304f19f545"));
  }

  public static int ProductObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID("cad00268-306c-11d8-b4e9-00304f19f545");
  }

  public static int DocumentObjectTypeID
  {
    get => MetaDataHelper.GetObjectTypeID("cad00070-306c-11d8-b4e9-00304f19f545");
  }
}
