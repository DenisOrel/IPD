
// Type: Intermech.Interfaces.PdmMrpHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Вспомогательный статический класс для работы модулей PDM/MRP
    /// [Потокобезопасные методы и свойства]
    /// </summary>
    public static class PdmMrpHelper
    {
      /// <summary>Массив исходных типов объектов</summary>
      private static List<string> sourceTypes = new List<string>((IEnumerable<string>) new string[6]
      {
        "cad00250-306c-11d8-b4e9-00304f19f545",
        "cad0025e-306c-11d8-b4e9-00304f19f545",
        "cad0025f-306c-11d8-b4e9-00304f19f545",
        "cad0038d-306c-11d8-b4e9-00304f19f545",
        "cad00132-306c-11d8-b4e9-00304f19f545",
        "cad00252-306c-11d8-b4e9-00304f19f545"
      });
      /// <summary>Массив типов объектов для создания экземпляров</summary>
      private static List<string> instanceTypes = new List<string>((IEnumerable<string>) new string[6]
      {
        "cad0058d-306c-11d8-b4e9-00304f19f545",
        "cad01473-306c-11d8-b4e9-00304f19f545",
        "cad01475-306c-11d8-b4e9-00304f19f545",
        "cad01471-306c-11d8-b4e9-00304f19f545",
        "cad0058b-306c-11d8-b4e9-00304f19f545",
        "cad0063c-306c-11d8-b4e9-00304f19f545"
      });
      /// <summary>Массив типов объектов для создания партий</summary>
      private static List<string> partiesTypes = new List<string>((IEnumerable<string>) new string[6]
      {
        "cad0058e-306c-11d8-b4e9-00304f19f545",
        "cad01472-306c-11d8-b4e9-00304f19f545",
        "cad01474-306c-11d8-b4e9-00304f19f545",
        "cad01470-306c-11d8-b4e9-00304f19f545",
        "cad0058c-306c-11d8-b4e9-00304f19f545",
        "cad0063d-306c-11d8-b4e9-00304f19f545"
      });
      /// <summary>
      /// Словарь позволяет получить тип экземпляра для типа объекта
      /// </summary>
      private static Dictionary<int, int> typeToInstance = new Dictionary<int, int>(12);
      /// <summary>
      /// Словарь позволяет получить тип партии для типа объекта
      /// </summary>
      private static Dictionary<int, int> typeToParty = new Dictionary<int, int>(12);
      /// <summary>Массив исходных типов объектов</summary>
      private static List<int> sourceTypeIDs = new List<int>(16 /*0x10*/);
      /// <summary>Массив типов объектов для создания экземпляров</summary>
      private static List<int> instanceTypeIDs = new List<int>(16 /*0x10*/);
      /// <summary>Массив типов объектов для создания экземпляров</summary>
      private static List<int> partiesTypeIDs = new List<int>(16 /*0x10*/);
      /// <summary>Объект для синхронизации</summary>
      private static object syncRoot = new object();
      /// <summary>Делегат для обратной связи с кэшем метаданных</summary>
      private static volatile MetaDataHelperEventHandler onMetaDataHelperEventHandler;

      /// <summary>
      /// Проверить, выполнена ли подписка на событие от MetaDataHelper
      /// </summary>
      private static void CheckMetaDataCallback()
      {
        if (PdmMrpHelper.onMetaDataHelperEventHandler != null)
          return;
        PdmMrpHelper.onMetaDataHelperEventHandler = new MetaDataHelperEventHandler(PdmMrpHelper.Reset);
        MetaDataHelperService.Instance.OnCacheReloaded += PdmMrpHelper.onMetaDataHelperEventHandler;
        PdmMrpHelper.Reset((object) null, EventArgs.Empty);
      }

      /// <summary>
      /// Очистить словарики, т.к. есть изменения в MetaDataHelper
      /// </summary>
      /// <param name="sender">Отправитель</param>
      /// <param name="e">Аргументы события</param>
      private static void Reset(object sender, EventArgs e)
      {
        lock (PdmMrpHelper.syncRoot)
        {
          PdmMrpHelper.typeToInstance.Clear();
          PdmMrpHelper.typeToParty.Clear();
          PdmMrpHelper.sourceTypeIDs.Clear();
          PdmMrpHelper.instanceTypeIDs.Clear();
          PdmMrpHelper.partiesTypeIDs.Clear();
          PdmMrpHelper.sourceTypes.ForEach((Action<string>) (type => PdmMrpHelper.sourceTypeIDs.Add(MetaDataHelper.GetObjectTypeID(type))));
          PdmMrpHelper.instanceTypes.ForEach((Action<string>) (type => PdmMrpHelper.instanceTypeIDs.Add(MetaDataHelper.GetObjectTypeID(type))));
          PdmMrpHelper.partiesTypes.ForEach((Action<string>) (type => PdmMrpHelper.partiesTypeIDs.Add(MetaDataHelper.GetObjectTypeID(type))));
        }
      }

      /// <summary>Получить типы экземпляров и партий для типа объекта</summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      private static void CheckObjectType(int objectTypeID)
      {
        PdmMrpHelper.CheckMetaDataCallback();
        lock (PdmMrpHelper.syncRoot)
        {
          if (PdmMrpHelper.typeToInstance.ContainsKey(objectTypeID))
            return;
          int index = PdmMrpHelper.sourceTypeIDs.IndexOf(objectTypeID);
          if (index < 0)
            index = PdmMrpHelper.sourceTypeIDs.FindIndex((Predicate<int>) (type => MetaDataHelper.IsObjectTypeChildOf(objectTypeID, type)));
          if (index < 0)
            return;
          PdmMrpHelper.typeToInstance[objectTypeID] = PdmMrpHelper.instanceTypeIDs[index];
          PdmMrpHelper.typeToParty[objectTypeID] = PdmMrpHelper.partiesTypeIDs[index];
        }
      }

      /// <summary>Получить тип экземпляра для указанного типа объекта</summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <returns>Тип экземпляра или Intermech.Consts.UnknownObjectTypeId</returns>
      public static int GetInstanceObjectType(int objectTypeID)
      {
        PdmMrpHelper.CheckObjectType(objectTypeID);
        lock (PdmMrpHelper.syncRoot)
        {
          if (PdmMrpHelper.typeToInstance.ContainsKey(objectTypeID))
            return PdmMrpHelper.typeToInstance[objectTypeID];
          if (PdmMrpHelper.typeToInstance.ContainsValue(objectTypeID))
            return objectTypeID;
        }
        return -1;
      }

      /// <summary>Получить тип партии для указанного типа объекта</summary>
      /// <param name="objectTypeID">Идентификатор типа объекта</param>
      /// <returns>Тип партии или Intermech.Consts.UnknownObjectTypeId</returns>
      public static int GetPartyObjectType(int objectTypeID)
      {
        PdmMrpHelper.CheckObjectType(objectTypeID);
        lock (PdmMrpHelper.syncRoot)
        {
          if (PdmMrpHelper.typeToParty.ContainsKey(objectTypeID))
            return PdmMrpHelper.typeToParty[objectTypeID];
          if (PdmMrpHelper.typeToParty.ContainsValue(objectTypeID))
            return objectTypeID;
        }
        return -1;
      }
    }
}
