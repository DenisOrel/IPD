// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.SLGP_FLAGS
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

[Flags]
internal enum SLGP_FLAGS
{
  /// <summary>Retrieves the standard short (8.3 format) file name</summary>
  SLGP_SHORTPATH = 1,
  /// <summary>Retrieves the Universal Naming Convention (UNC) path name of the file</summary>
  SLGP_UNCPRIORITY = 2,
  /// <summary>Retrieves the raw path name. A raw path is something that might not exist and may include environment variables that need to be expanded</summary>
  SLGP_RAWPATH = 4,
}
