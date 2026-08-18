// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.OptimizationValue
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll


namespace Intermech.Kernel.Helpers;

internal struct OptimizationValue(
  int attributeID,
  int objectTypeID,
  int relationTypeID,
  RequestOperations operation)
{
  public int AttributeID = attributeID;
  public int RelationTypeID = relationTypeID;
  public int ObjectTypeID = objectTypeID;
  public RequestOperations Operation = operation;
}
