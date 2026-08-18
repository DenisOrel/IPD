// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.IComplectTemplate
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert;

public interface IComplectTemplate : 
  IExpertScriptable,
  IExpertObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  string ObjTypeGuid { get; set; }
}
