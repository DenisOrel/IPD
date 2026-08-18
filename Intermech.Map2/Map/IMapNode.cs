// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapNode
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.Collections;


namespace Intermech.Map
{
    public interface IMapNode : IMapGraphPart
    {
      IEnumerable DestinationLinks { get; }

      IEnumerable Destinations { get; }

      IEnumerable Links { get; }

      IEnumerable Nodes { get; }

      IEnumerable Ports { get; }

      IEnumerable SourceLinks { get; }

      IEnumerable Sources { get; }
    }
}
