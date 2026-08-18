// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSystemAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Data;


namespace Intermech.Kernel;

internal class DBSystemAttributeType : DBAttributeType
{
  public DBSystemAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftSystem, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[0];
  }

  public override bool CheckAccess(
    ActionType anAction,
    bool aDefaultAccess,
    CheckAccessFlags flags)
  {
    if (anAction == ActionType.List || anAction == ActionType.Read || anAction == ActionType.GetAccess)
      return base.CheckAccess(anAction, aDefaultAccess, flags);
    if ((flags & CheckAccessFlags.ThrowACException) == CheckAccessFlags.ThrowACException)
      throw new KernelExceptionID(sc_12728.ssp_appserver_12729(1899172244));
    return false;
  }

  public override bool IsVirtualAttribute
  {
    get => ObligatoryObjectAttributesHelper.IsVirtualAttribute(this.AttributeID);
  }

  public override string GetSQL(string mainTableName)
  {
    if (!this.IsVirtualAttribute)
      throw new KernelException($"Вызов ф-ции GetSQL возможен только для виртуальных атрибутов. Атрибут {this.Name} не является виртуальным.");
    switch (this.AttributeID)
    {
      case -87:
        return $"(SELECT MAX({this.UserSession.DataManager.DataProvider.GetUTCSelect("F_START_DATE", this.UserSession.TimeZoneOffset)}) FROM IMS_LCSTART_DATE LC_HISTORY WHERE LC_HISTORY.F_OBJECT_ID = ABS({mainTableName}.F_OBJECT_ID))";
      case -86:
        return $"(SELECT COUNT(DISTINCT RELS_COUNT.F_PROJ_ID) FROM IMS_RELATIONS RELS_COUNT WHERE RELS_COUNT.F_PART_ID = {mainTableName}.F_ID)";
      case -85:
        return $"(SELECT COUNT(*) FROM IMS_OBJECT_LINKS REFS_COUNT WHERE REFS_COUNT.F_TOOBJECT_ID = ABS({mainTableName}.F_OBJECT_ID)) + (SELECT COUNT(*) FROM IMS_ID_LINKS REFS_COUNT2 WHERE REFS_COUNT2.F_TO_ID = O.F_ID)";
      case -84:
        return $"(SELECT COUNT(*) FROM IMS_OBJECTS OBJ_COUNT WHERE OBJ_COUNT.F_ID = {mainTableName}.F_ID AND OBJ_COUNT.F_OBJECT_ID > 0 AND OBJ_COUNT.F_LEVEL_ID <> {this.UserSession.IdentHelper.DeletedID})";
      default:
        throw new KernelException($"Для виртуального атрибута {this.Name} не реализован SQL-запрос.");
    }
  }

  public override DataTable GetPossibleValues() => (DataTable) null;

  public override bool CanUseInFormula
  {
    get
    {
      return ObligatoryObjectAttributesHelper.CanUseInFormula((ObligatoryObjectAttributes) this.AttributeID);
    }
  }

  public override string Mask
  {
    get => this.AttributeID == -24 ? Consts.OnlyDateFunction : base.Mask;
    set => base.Mask = value;
  }

  protected override void DoGetPropertiesStructure(ref AttributeTypeProperties atProperties)
  {
    base.DoGetPropertiesStructure(ref atProperties);
    if (this.AttributeID != -8)
      return;
    atProperties.MetadataExtensions[(object) "OBJ_LINKS_ID"] = (object) new int[2]
    {
      this.UserSession.IdentHelper.UsersTypeID,
      this.UserSession.IdentHelper.GroupsTypeID
    };
  }
}
