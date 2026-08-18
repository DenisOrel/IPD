// Decompiled with JetBrains decompiler
// Type: Intermech.Map2.MapTextImage
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map2
{
    public class MapTextImage
    {
      /// <summary>тип класса Intermech.Controls.CharacterMap</summary>
      private static readonly Lazy<Type> TypeCharacterMap = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Controls.CharacterMap,Intermech.Controls", false)));
      /// <summary>тип класса Intermech.Controls.CharacterMap.CharSelectedEventHandler</summary>
      private static readonly Lazy<Type> TypeCharSelectedEventHandler = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Controls.CharacterMap.CharSelectedEventHandler,Intermech.Controls", false)));
      /// <summary>тип класса Intermech.Controls.CharacterMap.CharacterMapEventArgs</summary>
      private static readonly Lazy<Type> TypeCharacterMapEventArgs = new Lazy<Type>((Func<Type>) (() => Type.GetType("Intermech.Controls.CharacterMap.CharacterMapEventArgs,Intermech.Controls", false)));
    }
}
