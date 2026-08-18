
// Type: Intermech.PropertyEditors.ObjTypeApplMember
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

/// <summary>
/// Список типов объектов для использования в RelationTypeMember (для RelationType)
/// </summary>
public class ObjTypeApplMember
{
  public ObjTypeApplList objTypeApplList;
  public int ApplId;
  public int ObjType;
  public int InObjType;
  public int RelType;
  public InheritModes Public;

  public ObjTypeApplMember(
    ObjTypeApplList aObjTypeApplList,
    int aApplId,
    int aInObjType,
    int aObjType,
    int aRelType,
    InheritModes aPublic)
  {
    this.objTypeApplList = aObjTypeApplList;
    this.ApplId = aApplId;
    this.ObjType = aObjType;
    this.InObjType = aInObjType;
    this.RelType = aRelType;
    this.Public = aPublic;
  }
}
