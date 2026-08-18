// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ISynchronizedObjectAttributes
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Интерфейс проекции настроек интегратора, которая содержит список синхронизируемых документов чего-либо - документа, изделия и др.
/// </summary>
public interface ISynchronizedObjectAttributes
{
  ICollection<StringKey> GetAttributes();

  ICollection<StringKey> GetAttributes(bool dbOnly);

  ICollection<StringKey> GetAttributes(int objectType, bool dbOnly);
}
