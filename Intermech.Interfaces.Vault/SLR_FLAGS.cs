// Decompiled with JetBrains decompiler
// Type: Intermech.Vault.Interfaces.SLR_FLAGS
// Assembly: Intermech.Interfaces.Vault, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 00798F5C-F1D9-4688-8BA7-75723F33BDBF
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Vault.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Vault.xml

using System;

#nullable disable
namespace Intermech.Vault.Interfaces;

[Flags]
internal enum SLR_FLAGS
{
  /// <summary>
  /// Do not display a dialog box if the link cannot be resolved. When SLR_NO_UI is set,
  /// the high-order word of fFlags can be set to a time-out value that specifies the
  /// maximum amount of time to be spent resolving the link. The function returns if the
  /// link cannot be resolved within the time-out duration. If the high-order word is set
  /// to zero, the time-out duration will be set to the default value of 3,000 milliseconds
  /// (3 seconds). To specify a value, set the high word of fFlags to the desired time-out
  /// duration, in milliseconds.
  /// </summary>
  SLR_NO_UI = 1,
  /// <summary>Obsolete and no longer used</summary>
  SLR_ANY_MATCH = 2,
  /// <summary>If the link object has changed, update its path and list of identifiers.
  /// If SLR_UPDATE is set, you do not need to call IPersistFile::IsDirty to determine
  /// whether or not the link object has changed.</summary>
  SLR_UPDATE = 4,
  /// <summary>Do not update the link information</summary>
  SLR_NOUPDATE = 8,
  /// <summary>Do not execute the search heuristics</summary>
  SLR_NOSEARCH = 16, // 0x00000010
  /// <summary>Do not use distributed link tracking</summary>
  SLR_NOTRACK = 32, // 0x00000020
  /// <summary>Disable distributed link tracking. By default, distributed link tracking tracks
  /// removable media across multiple devices based on the volume name. It also uses the
  /// Universal Naming Convention (UNC) path to track remote file systems whose drive letter
  /// has changed. Setting SLR_NOLINKINFO disables both types of tracking.</summary>
  SLR_NOLINKINFO = 64, // 0x00000040
  /// <summary>Call the Microsoft Windows Installer</summary>
  SLR_INVOKE_MSI = 128, // 0x00000080
}
