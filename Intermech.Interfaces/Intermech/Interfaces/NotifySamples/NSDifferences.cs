
// Type: Intermech.Interfaces.NotifySamples.NSDifferences
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.NotifySamples
{
    /// <summary>
    /// Объект, описывающий разницу с предыдущим состоянием выборки
    /// </summary>
    [Serializable]
    public class NSDifferences
    {
      /// <summary>Массив появившихся в выборке объектов</summary>
      public long[] IncludedObjects;
      /// <summary>Массив исчезнувших из выборки объектов</summary>
      public long[] ExcludedObjects;
      /// <summary>Ид. выборки</summary>
      public long SampleID;
      /// <summary>Наименование выборки</summary>
      public string SampleName;

      public NSDifferences(
        long[] includedObjects,
        long[] excludedObjects,
        long sampleID,
        string sampleName)
      {
        this.IncludedObjects = includedObjects;
        this.ExcludedObjects = excludedObjects;
        this.SampleID = sampleID;
        this.SampleName = sampleName;
      }
    }
}
