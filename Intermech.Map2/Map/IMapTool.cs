// Decompiled with JetBrains decompiler
// Type: Intermech.Map.IMapTool
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml


namespace Intermech.Map
{
    public interface IMapTool
    {
      bool CanStart();

      void DoCancelMouse();

      /// <summary>действия когда клавиша клавиатуры нажата</summary>
      void DoKeyDown();

      /// <summary>действия когда клавиша мыши нажата</summary>
      void DoMouseDown();

      void DoMouseHover();

      /// <summary>действия когда мышь двигают</summary>
      void DoMouseMove();

      /// <summary>действия когда клавиша мыши отпущена</summary>
      void DoMouseUp();

      void DoMouseWheel();

      void Start();

      void Stop();

      MapView View { get; set; }
    }
}
