// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapPort
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System.Collections;
using System.Drawing;


namespace Intermech.Map
{
    public interface IMapPort : IMapGraphPart
    {
      void AddDestinationLink(IMapLink l);

      void AddSourceLink(IMapLink l);

      bool CanLinkFrom();

      bool CanLinkTo();

      void ClearLinks();

      bool ContainsLink(IMapLink l);

      IMapLink[] CopyLinksArray();

      bool IsValidLink(IMapPort toPort);

      void OnLinkChanged(
        IMapLink link,
        int subhint,
        int oldI,
        object oldVal,
        RectangleF oldRect,
        int newI,
        object newVal,
        RectangleF newRect);

      void RemoveLink(IMapLink l);

      IEnumerable DestinationLinks { get; }

      int DestinationLinksCount { get; }

      IEnumerable Links { get; }

      int LinksCount { get; }

      IMapNode Node { get; }

      IEnumerable SourceLinks { get; }

      int SourceLinksCount { get; }
    }
}
