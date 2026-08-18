// Decompiled with JetBrains decompiler
// Type: Interop.CADInterface.ECADDocType
// Assembly: Interop.CADInterface, Version=7.4.0.0, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 483F07A3-5DB3-4173-82E9-08ADF3509A91
// Assembly location: D:\IPS\Client\Interop.CADInterface.dll

#nullable disable
namespace Interop.CADInterface;

public enum ECADDocType
{
  CDT_Undefined = -1, // 0xFFFFFFFF
  CDT_DefinedByTemplate = 0,
  CDT_Part = 1,
  CDT_Assembly = 2,
  CDT_Drawing = 3,
  CDT_Skeleton = 4,
  CDT_Layout = 5,
  CDT_AssemblyInterchange = 6,
  CDT_Manufacturing = 7,
}
