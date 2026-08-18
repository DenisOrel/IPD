// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBEncryptedAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBEncryptedAttributeType : DBAttributeType
{
  public DBEncryptedAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftPassword, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[1]
    {
      FieldTypes.ftPassword
    };
  }

  internal override string[] IndexFieldNames => (string[]) null;

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxPasswordSize);
  }
}
