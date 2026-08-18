// Decompiled with JetBrains decompiler
// Type: Intermech.Map.MapGeneralNodePortLabel
// Assembly: Intermech.Map2, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C50C6EBA-2322-47FA-9E95-25B5EFF3114E
// Assembly location: D:\IPS\Client\Intermech.Map2.dll
// XML documentation location: D:\IPS\Client\Intermech.Map2.xml

using System;


namespace Intermech.Map
{
    [Serializable]
    public class MapGeneralNodePortLabel : MapText
    {
      private MapGeneralNodePort myPort;

      public MapGeneralNodePortLabel()
      {
        this.myPort = (MapGeneralNodePort) null;
        this.Selectable = false;
        this.Editable = true;
        this.FontSize = MapText.DefaultFontSize - 2f;
      }

      public MapGeneralNodePort Port
      {
        get => this.myPort;
        set => this.myPort = value;
      }

      public override string Text
      {
        set
        {
          if (!(this.Text != value))
            return;
          base.Text = value;
          if (this.Port == null)
            return;
          this.Port.Name = this.Text;
        }
      }
    }
}
