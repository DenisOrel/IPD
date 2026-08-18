
// Type: Intermech.Interfaces.ObjectCheckedOutVersionsHolder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Контейнер с информацией о том, какие версии объектов и связи были получены
    /// у сервиса IObjectsCheckOutServerService
    /// </summary>
    [Serializable]
    public sealed class ObjectCheckedOutVersionsHolder
    {
      /// <summary>Список описаний версий объектов для редактирования</summary>
      public List<ObjectCheckOutVersionDescription> Objects = new List<ObjectCheckOutVersionDescription>();
      /// <summary>
      /// Список описаний исходных версий для выпущенных парных версий объектов
      /// </summary>
      public List<ObjectCheckOutVersionDescription> PairVersionSources = new List<ObjectCheckOutVersionDescription>();
      /// <summary>Список описаний выпущенных парных версий объектов</summary>
      public List<ObjectCheckOutVersionDescription> PairVersionTargets = new List<ObjectCheckOutVersionDescription>();

      /// <summary>Создать пустой экземпляр класса</summary>
      public ObjectCheckedOutVersionsHolder()
      {
      }

      /// <summary>Создать заполненный экземпляр класса</summary>
      /// <param name="objects">Список описаний версий объектов для редактирования</param>
      /// <param name="pairVersionSources">Список описаний исходных версий для выпущенных парных версий объектов</param>
      /// <param name="pairVersionTargets">Список описаний выпущенных парных версий объектов</param>
      public ObjectCheckedOutVersionsHolder(
        List<ObjectCheckOutVersionDescription> objects,
        List<ObjectCheckOutVersionDescription> pairVersionSources,
        List<ObjectCheckOutVersionDescription> pairVersionTargets)
      {
        this.Objects = objects ?? new List<ObjectCheckOutVersionDescription>();
        this.PairVersionSources = pairVersionSources ?? new List<ObjectCheckOutVersionDescription>();
        this.PairVersionTargets = pairVersionTargets ?? new List<ObjectCheckOutVersionDescription>();
      }
    }
}
