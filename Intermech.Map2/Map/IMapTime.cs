// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapTime
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map
{
    /// <summary>дата создания и последнего изменения примитива  </summary>
    public interface IMapTime
    {
      /// <summary>дата создания примитива</summary>
      DateTime CreateTime { get; set; }

      /// <summary>дата последнего изменения примитива</summary>
      DateTime ModificationTime { get; set; }
    }
}
