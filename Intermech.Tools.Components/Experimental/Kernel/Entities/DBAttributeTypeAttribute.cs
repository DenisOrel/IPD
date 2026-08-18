// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBAttributeTypeAttribute
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Kernel.Entities;

[AttributeUsage(AttributeTargets.Property, Inherited = true)]
public sealed class DBAttributeTypeAttribute(string attributeTypeGuid) : DBGuidAttribute(attributeTypeGuid)
{
}
