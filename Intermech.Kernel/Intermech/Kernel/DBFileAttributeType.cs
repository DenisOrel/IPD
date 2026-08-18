// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBFileAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBFileAttributeType : DBAttributeType
{
  public DBFileAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftFile, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._CanStorePossibleValues = false;
    this.CompatibleTypes = new FieldTypes[1]
    {
      FieldTypes.ftFile
    };
  }

  internal override string[] IndexFieldNames
  {
    get
    {
      return new string[3]
      {
        "F" + this.AttributeID.ToString(),
        $"F{this.AttributeID.ToString()}ID2",
        $"F{this.AttributeID.ToString()}ID3"
      };
    }
  }

  internal override string ColumnSQL
  {
    get
    {
      return string.Format("{0} {1}, {0}ID {2}, {0}ID2 {3}, {0}ID3 {4}", (object) base.ColumnSQL, (object) this.UserSession.DataManager.DataProvider.NVARCHARType(Consts.MaxStringSize), (object) this.UserSession.DataManager.DataProvider.INTEGERType, (object) this.UserSession.DataManager.DataProvider.FLOATType, (object) this.UserSession.DataManager.DataProvider.DATEType);
    }
  }

  public override void ValidateSizeType(long newValue)
  {
    base.ValidateSizeType(newValue);
    this.CheckMaxSize(newValue, (long) Consts.MaxNumericSize);
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
  }

  public override string SizeTypeDescription => string.Empty;
}
