
// Type: Intermech.Client.Core.ObjectCreator.NewRelationDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Client.Core.ObjectCreator;

/// <summary>
/// Класс для выбора типа новой связи через диалоговое окно
/// </summary>
internal class NewRelationDialog
{
  /// <summary>
  /// Создание окна диалога для выбора типа создаваемой связи
  /// </summary>
  /// <param name="objectCaption">Описание объекта с которым создается связь </param>
  /// <param name="objectTypeCaption">Описание типа объекта с которым создается связь</param>
  /// <param name="relationTypesIds">Массив идентификаторов допустимых типов связей</param>
  /// <param name="useForAll">Признак, указывающий на применения выбранного типа связи
  /// для всех объектов данного типа</param>
  /// <returns>Идентификатор типа связи. Если Consts.UnknownRelationTypeId, то связь для данного объекта не создавать
  /// (если при этом useForAll=true, то не создавать связи для всех объектов данного типа) </returns>
  public static int GetRelationID(
    string objectCaption,
    string objectTypeCaption,
    int[] relationTypesIds,
    out bool useForAll)
  {
    int relationId = -1;
    useForAll = false;
    if (relationTypesIds != null && relationTypesIds.Length != 0)
    {
      ObjectCreatorNewRelationForm creatorNewRelationForm = new ObjectCreatorNewRelationForm(objectCaption, objectTypeCaption, relationTypesIds);
      if (creatorNewRelationForm.ShowDialog() == DialogResult.OK)
      {
        relationId = creatorNewRelationForm.SelectedRelationType;
        useForAll = creatorNewRelationForm.UseForAll;
      }
    }
    return relationId;
  }
}
