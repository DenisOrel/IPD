// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.MetaDataHelperServiceUpdateTask
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;


namespace Intermech.Kernel.Services;

[Flags]
internal enum MetaDataHelperServiceUpdateTask
{
  None = 0,
  Full = 1118481, // 0x00111111
  ObjectTypes = Full, // 0x00111111
  ObjectTypesHierarchy = 1118480, // 0x00111110
  RelationTypes = 1118464, // 0x00111100
  AttrTypes = 1118208, // 0x00111000
  SpecialRelationTypes = 1114112, // 0x00110000
  SpecialObjectTypes = 1048576, // 0x00100000
  LCSteps = 16777216, // 0x01000000
  MetaDataGeneration = 268435456, // 0x10000000
  MetaDataIncGeneration = 536870912, // 0x20000000
  MetaDataCacheGeneration = 1073741824, // 0x40000000
}
