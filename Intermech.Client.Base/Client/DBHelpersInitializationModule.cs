
// Type: Intermech.Client.DBHelpersInitializationModule
// Assembly: Intermech.Client.Base, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C6CEDFE2-45F7-4A85-9CFB-4D0105C0197F
:\IPS\Client\Intermech.Client.Base.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Base.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;


namespace Intermech.Client
{
    /// <summary>
    /// Модуль для инициализации констант, вспомогательных статических классов клиента и кэширующих сервисов клиента.
    /// Модуль создается и выполняется сразу после инициализации пула сессий (т.е. SessionKeeper, IClientCache и пр. уже доступны)
    /// </summary>
    public sealed class DBHelpersInitializationModule : InitializerModule
    {
      /// <summary>
      /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
      /// </summary>
      protected override void DoInitialize()
      {
        base.DoInitialize();
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          MeasureHelper.Init(sessionKeeper.Session.GetMeasuresList());
      }
    }
}
