
// Type: Intermech.Interfaces.LifeCycles.NewLCStepInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.LifeCycles
{
    /// <summary>Класс для хранения инфы какой тип куда переводить</summary>
    [Serializable]
    public class NewLCStepInfo
    {
      /// <summary>Тип объектов</summary>
      public int ObjectTypeID { get; private set; }

      /// <summary>Жаг ЖЦ (если &gt; 0)</summary>
      public int LCStepID { get; private set; }

      /// <summary>Уровень продвижения (если &gt; 0)</summary>
      public int LevelID { get; private set; }

      public NewLCStepInfo(int objectTypeID, int lcID, bool isItStepID)
      {
        this.ObjectTypeID = objectTypeID;
        if (isItStepID)
        {
          this.LCStepID = lcID;
          this.LevelID = 0;
        }
        else
        {
          this.LevelID = lcID;
          this.LCStepID = 0;
        }
      }
    }
}
