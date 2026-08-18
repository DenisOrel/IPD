// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Interfaces.Configs.Common.GroupDocument
// Assembly: Intermech.TechCard.Document.Interfaces, Version=7.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: D9DB0A36-F52B-4632-90E0-E8B14A322D86
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Interfaces.dll

using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;
using System;

#nullable disable
namespace Intermech.TechCard.Document.Interfaces.Configs.Common;

public static class GroupDocument
{
  public static class GDObject
  {
    public static int GDObjectId;
    public static readonly string GdObjectGuid = "cadd9a1f-306c-11d8-b4e9-00304f19f545";

    static GDObject()
    {
      GroupDocument.GDObject.GDObjectId = MetaDataHelper.GetObjectTypeID(GroupDocument.GDObject.GdObjectGuid);
    }
  }

  public static class FirstPage
  {
    public static int FirstPageId;
    public static Guid FirstPageGuid;
    public static string FirstPageNumber = "cad014b1-306c-11d8-b4e9-00304f19f545";

    static FirstPage()
    {
      GroupDocument.FirstPage.FirstPageGuid = new Guid(GroupDocument.FirstPage.FirstPageNumber);
      GroupDocument.FirstPage.FirstPageId = MetaDataHelper.GetObjectTypeID(GroupDocument.FirstPage.FirstPageGuid);
    }

    public static string FirstPageGuidStr() => GroupDocument.FirstPage.FirstPageGuid.ToString();
  }

  public static class StepNumber
  {
    public static int StepNumberId;
    public static Guid StepNumberGuid = TechCardConsts.AttributeTypes.NumerationStepAttrGuid;

    static StepNumber()
    {
      GroupDocument.StepNumber.StepNumberId = MetaDataHelper.GetObjectTypeID(GroupDocument.StepNumber.StepNumberGuid);
    }

    public static string StepNumberGuidStr() => GroupDocument.StepNumber.StepNumberGuid.ToString();
  }

  public static class NumberOfChar
  {
    public static int NumberOfCharId;
    public static Guid NumberOfCharGuid = TechCardConsts.AttributeTypes.NumerationNumberLengthAttrGuid;

    static NumberOfChar()
    {
      GroupDocument.NumberOfChar.NumberOfCharId = MetaDataHelper.GetObjectTypeID(GroupDocument.NumberOfChar.NumberOfCharGuid);
    }

    public static string NumberOfCharGuidStr()
    {
      return GroupDocument.NumberOfChar.NumberOfCharGuid.ToString();
    }
  }

  public static class Name
  {
    public static int NameId;
    public static Guid NameGuid = new Guid("cad00020-306c-11d8-b4e9-00304f19f545");

    static Name()
    {
      GroupDocument.Name.NameId = MetaDataHelper.GetObjectTypeID(GroupDocument.Name.NameGuid);
    }

    public static string NameGuidStr() => GroupDocument.Name.NameGuid.ToString();
  }
}
