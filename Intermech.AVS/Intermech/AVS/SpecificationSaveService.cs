// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecificationSaveService
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Document;
using System.IO;

#nullable disable
namespace Intermech.AVS;

/// <summary>для сохранения точных спецификаций</summary>
public class SpecificationSaveService : ISpecificationSaveService
{
  /// <summary>Создать точную спецификацию и сохранить её
  /// (1002954 сохранение точных спецификаций)</summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  /// <param name="configureCompositionRoot">Корень конфигурации состава</param>
  /// <param name="filtrationOwnerID">Владелец настроек фильтрации</param>
  /// <param name="designation">Cуффикc для обозначения в спецификациях</param>
  /// <param name="filePath">Куда сохраняются файлы объекта, для которого создаём спицификацию</param>
  /// <param name="createFolder">Создавать ли для спецификации отдельную папку</param>
  public void SaveSpecification(
    int objectType,
    long objectId,
    RelationPair configureCompositionRoot,
    string filtrationOwnerID,
    string designation,
    string filePath,
    bool createFolder)
  {
    using (AVSDocument avsDocument = new AVSDocument(objectType, objectId, AVSDocumentForm.Single, configureCompositionRoot, filtrationOwnerID, false))
    {
      if (designation != string.Empty)
        avsDocument.DocumentDesignation += designation;
      string path = avsDocument.DocumentCaption != string.Empty ? avsDocument.DocumentCaption : $"Спецификация объекта с ID {objectId}";
      DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
      if (createFolder)
        filePath = directoryInfo.CreateSubdirectory(path).FullName;
      avsDocument.Document.SaveToXml(Path.Combine(filePath, path + ".spx"), false);
    }
  }
}
