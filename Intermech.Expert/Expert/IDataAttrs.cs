// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IDataAttrs
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Expert;

public interface IDataAttrs
{
  List<string> DataAttrGuids { get; }

  List<string> DataAttrTexts { get; }

  bool this[int index] { get; }
}
