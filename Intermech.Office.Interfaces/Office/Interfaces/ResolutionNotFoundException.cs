// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Interfaces.ResolutionNotFoundException
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Office.Interfaces;

[Serializable]
public class ResolutionNotFoundException : 
  ObjectVersionNotFoundException,
  ISerializable,
  IObjectException
{
  public ResolutionNotFoundException(long aObjectVersionID)
    : base(aObjectVersionID)
  {
  }

  protected ResolutionNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public long ResolutionID => this.ObjectVersionID;

  [SpecialName]
  long IObjectException.get_ObjectID() => this.ObjectID;
}
