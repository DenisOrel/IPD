// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CADCloneDataCapabilities
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

[Flags]
internal enum CADCloneDataCapabilities
{
  None = 0,
  CanHandleOnlyCADFiles = 1,
  IncludeUnmodifiedReferenceFiles = 2,
}
