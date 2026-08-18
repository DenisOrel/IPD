// Decompiled with JetBrains decompiler
// Type: Intermech.Checksums.ChecksumInputStructure
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Checksums;

public struct ChecksumInputStructure(
  long elementId,
  AttributableElements kind,
  int attributeId,
  int index,
  ChecksumAlgorithm algorithm)
{
  public long elementId = elementId;
  public AttributableElements kind = kind;
  public int attributeId = attributeId;
  public int index = index;
  public ChecksumAlgorithm algorithm = algorithm;
}
