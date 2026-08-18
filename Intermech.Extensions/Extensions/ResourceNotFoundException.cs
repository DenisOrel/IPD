// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ResourceNotFoundException
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace Intermech.Extensions;

[Serializable]
public class ResourceNotFoundException : Exception, ISerializable
{
  [NotNull]
  [NotWhitespace]
  public string AssemblyName { get; }

  [NotNull]
  [NotWhitespace]
  public string ResourceName { get; }

  public ResourceNotFoundException([NotNull, NotWhitespace] string assemblyName, [NotNull, NotWhitespace] string resourceName, [CanBeNull] string message = null)
    : base(message ?? ResourceNotFoundException.GetDefaultMessage(assemblyName, resourceName))
  {
    this.AssemblyName = assemblyName;
    this.ResourceName = resourceName;
  }

  [NotNull]
  [NotWhitespace]
  private static string GetDefaultMessage([NotNull, NotWhitespace] string assemblyName, [NotNull, NotWhitespace] string resourceName)
  {
    return $"Resource \"{resourceName}\" not found in assembly \"{assemblyName}\"!";
  }

  [SecuritySafeCritical]
  protected ResourceNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.AssemblyName = info.GetString(nameof (AssemblyName));
    this.ResourceName = info.GetString(nameof (ResourceName));
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("AssemblyName", (object) this.AssemblyName);
    info.AddValue("ResourceName", (object) this.ResourceName);
  }

  public override int GetHashCode() => (this.AssemblyName, this.ResourceName).GetHashCode();
}
