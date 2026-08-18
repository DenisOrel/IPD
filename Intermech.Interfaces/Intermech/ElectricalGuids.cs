
// Type: Intermech.ElectricalGuids
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using System;


namespace Intermech
{
    public static class ElectricalGuids
    {
      /// <summary>Электрические объединенные. Перечни элементов</summary>
      public static Guid elementList0 = new Guid("cad015b4-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические структурные. Перечни элементов</summary>
      public static Guid elementList1 = new Guid("cad015b5-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические функциональные. Перечни элементов</summary>
      public static Guid elementList2 = new Guid("cad015b6-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические принципиальные. Перечни элементов</summary>
      public static Guid elementList3 = new Guid("cad0075c-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические соединений. Перечни элементов</summary>
      public static Guid elementList4 = new Guid("cad015b7-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические подключений. Перечни элементов</summary>
      public static Guid elementList5 = new Guid("cad015b8-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические общие. Перечни элементов</summary>
      public static Guid elementList6 = new Guid("cad015b3-306c-11d8-b4e9-00304f19f545");
      /// <summary>Электрические расположения. Перечни элементов</summary>
      public static Guid elementList7 = new Guid("cad015b9-306c-11d8-b4e9-00304f19f545");
      private static int[] _elementListTypes = (int[]) null;

      /// <summary>Массив идентификаторов типов перечней элементов</summary>
      public static int[] ElementListTypes
      {
        get
        {
          if (ElectricalGuids._elementListTypes == null)
            ElectricalGuids._elementListTypes = new int[8]
            {
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList0),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList1),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList2),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList3),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList4),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList5),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList6),
              MetaDataHelper.GetObjectTypeID(ElectricalGuids.elementList7)
            };
          return ElectricalGuids._elementListTypes;
        }
      }
    }
}
