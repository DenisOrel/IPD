
// Type: Intermech.Interfaces.CompositionSortingProjInfoExtension
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public static class CompositionSortingProjInfoExtension
    {
      /// <summary>Проверка записи на наличие "недостающей" информации</summary>
      /// <param name="compositionInfo">Информация о элементе состава</param>
      /// <returns>true - если не все данные заданы в записи</returns>
      public static bool HasEmptyInfo(this CompositionSortingProjInfo compositionInfo)
      {
        return compositionInfo == null || compositionInfo.RelTypeID == -1 || compositionInfo.ProjObjID == 0L || compositionInfo.ProjTypeID == -1 || compositionInfo.PartObjType == -1;
      }
    }
}
