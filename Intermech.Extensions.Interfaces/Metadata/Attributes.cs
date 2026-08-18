// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.Attributes
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public abstract class Attributes
{
  public const string EntityInstanceNameInGenitiveCase = "атрибута";
  public const int Zero = 0;
  public const int None = -1;
  public const int F_OBJECT_ID = -2;
  public const int F_ID = -3;
  public const int F_LC_STEP = -4;
  public const int F_VERSION_ID = -5;
  public const int F_CHKOUT_BY = -6;
  public const int F_OBJECT_TYPE = -7;
  public const int F_OWNER_ID = -8;
  public const int F_LEVEL_ID = -9;
  public const int F_MODIFY_DATE = -10;
  public const int F_AREA_ID = -11;
  public const int F_GUID = -12;
  public const int F_OBJ_CREATE = -13;
  public const int F_PROJECT_ID = -14;
  public const int F_MODIFICATION_ID = -15;
  public const int F_BASE_VERSION = -16;
  public const int F_SITE_ID = -17;
  public const int F_OBJ_GUID = -18;
  public const int F_OBJECT_VER_TYPE = -19;
  public const int F_PRJLINK_ID = -20;
  public const int F_PROJ_ID = -21;
  public const int F_PART_ID = -22;
  public const int F_RELATION_TYPE = -23;
  public const int F_CREATE_DATE = -24;
  public const int F_PRJ_GUID = -26;
  public const int F_EVENT_ID = -30;
  public const int F_CATEGORY_TYPE = -31;
  public const int F_CATEGORY_ID = -32;
  public const int F_RELATION_ID = -34;
  public const int F_OBJECT_NAME = -35;
  public const int F_USER_ID = -36;
  public const int F_COMPUTER_NAME = -37;
  public const int F_NOTE = -38;
  public const int F_EVENT_TYPE = -39;
  public const int F_BEGIN_DATE = -40;
  public const int F_END_DATE = -41;
  public const int F_AUDIT_TYPE = -42;
  public const int F_ACTUAL_DATE = -43;
  public const int CAPTION = -50;
  public const int F_SET_DATE = -51;
  public const int F_STATUS = -52;
  public const int F_INTEGER_VALUE = -53;
  public const int F_STRING_VALUE = -54;
  public const int F_DOUBLE_VALUE = -55;
  public const int F_DATE_VALUE = -56;
  public const int F_KEY = -57;
  public const int F_ATTRIBUTE_ID = -58;
  public const int F_VERSION_RESULT = -60;
  public const int F_FILE_ID = -70;
  public const int F_FILENAME = -71;
  public const int F_FILESIZE = -72;
  public const int F_FILEDATE = -73;
  public const int F_ZIPSIZE = -74;
  public const int F_OBJECTLINK_ID = -75;
  public const int F_ARC_METHOD = -76;
  public const int F_ELEMENT_STATUSES = -77;
  public const int F_SNAPSHOT_ID = -78;
  public const int F_SNAPSHOT_DATE = -79;
  public const int F_ACCESS = -80;
  public const int F_CREATOR_ID = -81;
  public const int F_REL_CREATOR = -82;
  public const int F_PARENT_OBJECT_ID = -83;
  public const int F_VERSIONS_COUNT = -84;
  public const int F_REFERENCE_COUNT = -85;
  public const int F_RELATIONS_COUNT = -86;
  public const int F_LCSTEP_DATE = -87;
  public const int UnknownID = 0;
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjectVersionID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJECT_ID, nameof (ObjectVersionID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjectID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_ID, nameof (ObjectID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjectVersionGuid = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_GUID, nameof (ObjectVersionGuid));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjectGuid = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJ_GUID, nameof (ObjectGuid));
  [NotNull]
  public static readonly ObligatoryObjectAttribute VersionNum = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_VERSION_ID, nameof (VersionNum));
  [NotNull]
  public static readonly ObligatoryObjectAttribute VersionType = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJECT_VER_TYPE, nameof (VersionType));
  [NotNull]
  public static readonly ObligatoryObjectAttribute IsBaseVersion = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_BASE_VERSION, nameof (IsBaseVersion));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ParentVersionID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PARENT_OBJECT_ID, nameof (ParentVersionID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute VersionsCount = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_VERSIONS_COUNT, nameof (VersionsCount));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjectType = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJECT_TYPE, nameof (ObjectType));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Name = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJECT_NAME, nameof (Name));
  [NotNull]
  public static readonly SystemAttribute Designation = Attributes.Create("cad0001f-306c-11d8-b4e9-00304f19f545", nameof (Designation));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Caption = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.CAPTION, nameof (Caption));
  [NotNull]
  public static readonly SystemAttribute Note = Attributes.Create("cad00021-306c-11d8-b4e9-00304f19f545", nameof (Note));
  [NotNull]
  public static readonly ObligatoryObjectAttribute LcStep = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_LC_STEP, nameof (LcStep));
  [NotNull]
  public static readonly ObligatoryObjectAttribute LcChanged = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_LCSTEP_DATE, nameof (LcChanged));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Level = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_LEVEL_ID, nameof (Level));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Project = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PROJECT_ID, nameof (Project));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Site = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_SITE_ID, nameof (Site));
  [NotNull]
  public static readonly ObligatoryObjectAttribute AccessLevel = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_ACCESS, nameof (AccessLevel));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Creator = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_CREATOR_ID, nameof (Creator));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Owner = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OWNER_ID, nameof (Owner));
  [NotNull]
  public static readonly ObligatoryObjectAttribute CheckedOutBy = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_CHKOUT_BY, nameof (CheckedOutBy));
  [NotNull]
  public static readonly SystemAttribute Author = Attributes.Create("cad00280-306c-11d8-b4e9-00304f19f545", nameof (Author));
  [NotNull]
  public static readonly SystemAttribute CheckedBy = Attributes.Create("cad00282-306c-11d8-b4e9-00304f19f545", nameof (CheckedBy));
  [NotNull]
  public static readonly SystemAttribute ConfirmBy = Attributes.Create("cad00284-306c-11d8-b4e9-00304f19f545", nameof (ConfirmBy));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Modified = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_MODIFY_DATE, nameof (Modified));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Created = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJ_CREATE, nameof (Created));
  [NotNull]
  public static readonly ObligatoryObjectAttribute Modification = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_MODIFICATION_ID, nameof (Modification));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ReferencesCount = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_REFERENCE_COUNT, nameof (ReferencesCount));
  [NotNull]
  public static readonly ObligatoryObjectAttribute UsageCount = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_RELATIONS_COUNT, nameof (UsageCount));
  [NotNull]
  public static readonly ObligatoryObjectAttribute RelationCreator = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_REL_CREATOR, nameof (RelationCreator));
  [NotNull]
  public static readonly ObligatoryObjectAttribute PrjLinkID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PRJLINK_ID, nameof (PrjLinkID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute RelationGuid = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PRJ_GUID, nameof (RelationGuid));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ProjID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PROJ_ID, nameof (ProjID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute PartID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_PART_ID, nameof (PartID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute RelationType = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_RELATION_TYPE, nameof (RelationType));
  [NotNull]
  public static readonly ObligatoryObjectAttribute CreateDate = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_CREATE_DATE, nameof (CreateDate));
  [NotNull]
  public static readonly ObligatoryObjectAttribute RelationActualDate = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_ACTUAL_DATE, nameof (RelationActualDate));
  [NotNull]
  public static readonly ObligatoryObjectAttribute VersionResult = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_VERSION_RESULT, nameof (VersionResult));
  [NotNull]
  public const int AreaID = -11;
  [NotNull]
  public static readonly SystemAttribute ShortName = Attributes.Create("cad00005-306c-11d8-b4e9-00304f19f545", nameof (ShortName));
  [NotNull]
  public static readonly SystemAttribute Process = Attributes.Create("cad002ce-306c-11d8-b4e9-00304f19f545", nameof (Process));
  [NotNull]
  public static readonly SystemAttribute Role = Attributes.Create("cadd94e6-306c-11d8-b4e9-00304f19f545", nameof (Role));
  [NotNull]
  public static readonly SystemAttribute Rank = Attributes.Create("cad00142-306c-11d8-b4e9-00304f19f545", nameof (Rank));
  [NotNull]
  public static readonly SystemAttribute IoUser = Attributes.Create("cadd91f5-306c-11d8-b4e9-00304f19f545", nameof (IoUser));
  [NotNull]
  public static readonly SystemAttribute SecurityLevel = Attributes.Create("cad00816-306c-11d8-b4e9-00304f19f545", nameof (SecurityLevel));
  [NotNull]
  public static readonly ObligatoryObjectAttribute FileID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_FILE_ID, nameof (FileID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute FileName = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_FILENAME, nameof (FileName));
  [NotNull]
  public static readonly ObligatoryObjectAttribute FileSize = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_FILESIZE, nameof (FileSize));
  [NotNull]
  public static readonly ObligatoryObjectAttribute FileDate = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_FILEDATE, nameof (FileDate));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ZipSize = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_ZIPSIZE, nameof (ZipSize));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ObjLinkID = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_OBJECTLINK_ID, nameof (ObjLinkID));
  [NotNull]
  public static readonly ObligatoryObjectAttribute ArchiveMethod = Attributes.ObligatoryAttribute(ObligatoryObjectAttributes.F_ARC_METHOD, nameof (ArchiveMethod));
  [NotNull]
  public static readonly SystemAttribute Data = Attributes.Create("cad001b2-306c-11d8-b4e9-00304f19f545", nameof (Data));
  [NotNull]
  public static readonly SystemAttribute VersionRule = Attributes.Create("cad00696-306c-11d8-b4e9-00304f19f545", nameof (VersionRule));
  [NotNull]
  public static readonly SystemAttribute Description = Attributes.Create("cad0001c-306c-11d8-b4e9-00304f19f545", nameof (Description));
  [NotNull]
  public static readonly SystemAttribute Director = Attributes.Create("cadd9233-306c-11d8-b4e9-00304f19f545", nameof (Director));
  [NotNull]
  public static readonly SystemAttribute ImbaseKey = Attributes.Create("cad00162-306c-11d8-b4e9-00304f19f545", nameof (ImbaseKey));
  [NotNull]
  public static readonly SystemAttribute ImbaseCode = Attributes.Create("cad0020f-306c-11d8-b4e9-00304f19f545", nameof (ImbaseCode));
  [NotNull]
  public static readonly SystemAttribute ImbaseLink = Attributes.Create("cad00209-306c-11d8-b4e9-00304f19f545", nameof (ImbaseLink));
  [NotNull]
  public static readonly SystemAttribute Storage = Attributes.Create("cad0005c-306c-11d8-b4e9-00304f19f545", nameof (Storage));
  [NotNull]
  public static readonly SystemAttribute File = Attributes.Create("cad0004b-306c-11d8-b4e9-00304f19f545", nameof (File));
  [NotNull]
  public static readonly SystemAttribute DocumentFile = Attributes.Create("cadd9620-306c-11d8-b4e9-00304f19f545", nameof (DocumentFile));
  [NotNull]
  public static readonly SystemAttribute ScannedDocument = Attributes.Create("cadd9644-306c-11d8-b4e9-00304f19f545", nameof (ScannedDocument));
  [NotNull]
  public static readonly SystemAttribute ConfigFile = Attributes.Create("cad014d4-306c-11d8-b4e9-00304f19f545", nameof (ConfigFile));
  [NotNull]
  public static readonly SystemAttribute CompositionContext = Attributes.Create("cad00651-306c-11d8-b4e9-00304f19f545", nameof (CompositionContext));
  [NotNull]
  public static readonly SystemAttribute BufferSize = Attributes.Create("cad00027-306c-11d8-b4e9-00304f19f545", nameof (BufferSize));
  [NotNull]
  public static readonly SystemAttribute StorageTableName = Attributes.Create("cad00028-306c-11d8-b4e9-00304f19f545", nameof (StorageTableName));
  [NotNull]
  public static readonly SystemAttribute StorageType = Attributes.Create("cad00000-306c-11d8-b4e9-00304f19f545", nameof (StorageType));
  [NotNull]
  public static readonly SystemAttribute ObjectTypeGuids = Attributes.Create("cad00149-306c-11d8-b4e9-00304f19f545", nameof (ObjectTypeGuids));
  [NotNull]
  public static readonly SystemAttribute RelationTypeGuids = Attributes.Create("cad0014a-306c-11d8-b4e9-00304f19f545", nameof (RelationTypeGuids));
  [NotNull]
  public static readonly SystemAttribute RelationTypeGuid = Attributes.Create("cad001a9-306c-11d8-b4e9-00304f19f545", nameof (RelationTypeGuid));
  [NotNull]
  public static readonly SystemAttribute FilterSelection = Attributes.Create("cad00621-306c-11d8-b4e9-00304f19f545", nameof (FilterSelection));
  [NotNull]
  public static readonly SystemAttribute SortIndex = Attributes.Create("cad00202-306c-11d8-b4e9-00304f19f545", nameof (SortIndex));
  [NotNull]
  public static readonly SystemAttribute ExternalUser = Attributes.Create("cad002df-306c-11d8-b4e9-00304f19f545", nameof (ExternalUser));
  [NotNull]
  public static readonly SystemAttribute OKP = Attributes.Create("cad0038a-306c-11d8-b4e9-00304f19f545", nameof (OKP));
  [NotNull]
  public static readonly SystemAttribute UserName = Attributes.Create("cad0001d-306c-11d8-b4e9-00304f19f545", nameof (UserName));
  [NotNull]
  public static readonly SystemAttribute Format = Attributes.Create("cad00255-306c-11d8-b4e9-00304f19f545", nameof (Format));
  [NotNull]
  public static readonly SystemAttribute Count = Attributes.Create("cad00267-306c-11d8-b4e9-00304f19f545", nameof (Count));
  [NotNull]
  public static readonly SystemAttribute NormalizedName = Attributes.Create("cad00798-306c-11d8-b4e9-00304f19f545", nameof (NormalizedName));
  [NotNull]
  public static readonly SystemAttribute Position = Attributes.Create("cad00270-306c-11d8-b4e9-00304f19f545", nameof (Position));
  [NotNull]
  public static readonly SystemAttribute ActiveStorage = Attributes.Create("cad00032-306c-11d8-b4e9-00304f19f545", nameof (ActiveStorage));
  [NotNull]
  public static readonly SystemAttribute ArticleID = Attributes.Create("cad00622-306c-11d8-b4e9-00304f19f545", nameof (ArticleID));
  [NotNull]
  public static readonly SystemAttribute DocumentID = Attributes.Create("cad00623-306c-11d8-b4e9-00304f19f545", nameof (DocumentID));
  [NotNull]
  public static readonly SystemAttribute Weight = Attributes.Create("cad00275-306c-11d8-b4e9-00304f19f545", nameof (Weight));
  [NotNull]
  public static readonly SystemAttribute UnitWeight = Attributes.Create("cad00276-306c-11d8-b4e9-00304f19f545", nameof (UnitWeight));
  [NotNull]
  public static readonly SystemAttribute Size = Attributes.Create("cad00277-306c-11d8-b4e9-00304f19f545", nameof (Size));
  [NotNull]
  public static readonly SystemAttribute Zone = Attributes.Create("cad0027a-306c-11d8-b4e9-00304f19f545", nameof (Zone));
  [NotNull]
  public static readonly SystemAttribute Subdivision = Attributes.Create("cad00281-306c-11d8-b4e9-00304f19f545", nameof (Subdivision));
  [NotNull]
  public static readonly SystemAttribute Litera = Attributes.Create("cad0038b-306c-11d8-b4e9-00304f19f545", nameof (Litera));
  [NotNull]
  public static readonly SystemAttribute NormControlledBy = Attributes.Create("cad00283-306c-11d8-b4e9-00304f19f545", nameof (NormControlledBy));
  [NotNull]
  public static readonly SystemAttribute ScriptText = Attributes.Create("cad00366-306c-11d8-b4e9-00304f19f545", nameof (ScriptText));
  [NotNull]
  public static readonly SystemAttribute Email = Attributes.Create("cad002de-306c-11d8-b4e9-00304f19f545", nameof (Email));
  [NotNull]
  public static readonly SystemAttribute HomeAddress = Attributes.Create("cad002dc-306c-11d8-b4e9-00304f19f545", nameof (HomeAddress));
  [NotNull]
  public static readonly SystemAttribute Phone = Attributes.Create("cad002da-306c-11d8-b4e9-00304f19f545", nameof (Phone));
  [NotNull]
  public static readonly SystemAttribute PostalAddress = Attributes.Create("cad015dd-306c-11d8-b4e9-00304f19f545", nameof (PostalAddress));
  [NotNull]
  public static readonly SystemAttribute HomePhone = Attributes.Create("cad002dd-306c-11d8-b4e9-00304f19f545", nameof (HomePhone));
  [NotNull]
  public static readonly SystemAttribute MobilePhone = Attributes.Create("cad015df-306c-11d8-b4e9-00304f19f545", nameof (MobilePhone));
  [NotNull]
  public static readonly SystemAttribute Office = Attributes.Create("cad002db-306c-11d8-b4e9-00304f19f545", nameof (Office));
  [NotNull]
  public static readonly SystemAttribute LockedUser = Attributes.Create("cadd99fb-306c-11d8-b4e9-00304f19f545", nameof (LockedUser));
  [NotNull]
  public static readonly SystemAttribute ObjectLink = Attributes.Create("cad001be-306c-11d8-b4e9-00304f19f545", nameof (ObjectLink));
  [NotNull]
  public static readonly SystemAttribute Calendar = Attributes.Create("cad00ea5-306c-11d8-b4e9-00304f19f545", nameof (Calendar));
  [NotNull]
  public static readonly SystemAttribute UserCalendar = Attributes.Create("cadd9b9f-306c-11d8-b4e9-00304f19f545", nameof (UserCalendar));
  [NotNull]
  public static readonly SystemAttribute IterationID = Attributes.Create("cadd95a1-306c-11d8-b4e9-00304f19f545", nameof (IterationID));
  [NotNull]
  public static readonly SystemAttribute UserHireDate = Attributes.Create("cadd9bf1-306c-11d8-b4e9-00304f19f545", nameof (UserHireDate));
  [NotNull]
  public static readonly SystemAttribute UserFireDate = Attributes.Create("cadd9bf2-306c-11d8-b4e9-00304f19f545", nameof (UserFireDate));
  public const string EntityName = "Атрибуты";
  public const string EntityInstanceName = "Атрибут";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemAttribute Create([NotNull, NotWhitespace, ValueProvider("Intermech.SystemGUIDs")] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Attributes.Create<Attributes>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemAttribute Create([NotEmpty] int obligatoryAttributeID, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    System.Guid attributeTypeGuid = MetaDataHelperService.Instance.GetAttributeTypeGuid(obligatoryAttributeID);
    return new SystemAttribute(obligatoryAttributeID, attributeTypeGuid, typeof (Attributes), true, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static ObligatoryObjectAttribute ObligatoryAttribute(
    [NotEmpty] ObligatoryObjectAttributes obligatoryAttributeID,
    [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return new ObligatoryObjectAttribute(obligatoryAttributeID, typeof (Attributes), idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemAttribute CreateObligatory([NotNull, NotWhitespace, ValueProvider("Intermech.SystemGUIDs")] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Attributes.Create<Attributes>(guid, true, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemAttribute Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [CallerMemberName, NotNull, NotWhitespace] string idName = "") where THolder : Attributes
  {
    System.Guid guid1 = new System.Guid(guid);
    return new SystemAttribute(MetaDataHelperService.Instance.GetAttributeTypeID(guid1), guid1, typeof (THolder), obligatory, idName);
  }

  [NotNull]
  public static ObligatoryObjectAttribute ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Attributes.ObjectVersionID;
  }

  [NotNull]
  public static ObligatoryObjectAttribute Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Attributes.ObjectVersionGuid;
  }

  [NotNull]
  public static ObligatoryObjectAttribute RelationID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Attributes.PrjLinkID;
  }

  [NotNull]
  public static SystemAttribute Acting
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Attributes.IoUser;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdByGuid([NotEmpty] System.Guid guid, bool throwIfNotFound = true)
  {
    return Attributes.Implementation.GetIdByGuid(guid, throwIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int GetIdByGuidOrName([NotNull, NotWhitespace] string guidOrName, bool throwIfNotFound = true)
  {
    return Attributes.Implementation.GetIdByGuidOrName(guidOrName, throwIfNotFound);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ColumnContents GetDefaultContent([NotEmpty] int attributeID)
  {
    return Attributes.Implementation.GetDefaultContent(attributeID);
  }

  public abstract class Consts
  {
    public const string ObjectTypeGuid = "cad001a0-306c-11d8-b4e9-00304f19f545";
    public const string DesignationGuid = "cad0001f-306c-11d8-b4e9-00304f19f545";
    public const string NoteGuid = "cad00021-306c-11d8-b4e9-00304f19f545";
    public const string AuthorGuid = "cad00280-306c-11d8-b4e9-00304f19f545";
    public const string CheckedByGuid = "cad00282-306c-11d8-b4e9-00304f19f545";
    public const string ConfirmByGuid = "cad00284-306c-11d8-b4e9-00304f19f545";
    public const string ShortNameGuid = "cad00005-306c-11d8-b4e9-00304f19f545";
    public const string ProcessGuid = "cad002ce-306c-11d8-b4e9-00304f19f545";
    public const string RoleGuid = "cadd94e6-306c-11d8-b4e9-00304f19f545";
    public const string RankGuid = "cad00142-306c-11d8-b4e9-00304f19f545";
    public const string IoUserGuid = "cadd91f5-306c-11d8-b4e9-00304f19f545";
    public const string SecurityLevelGuid = "cad00816-306c-11d8-b4e9-00304f19f545";
    public const string FileIDGuid = "cad001f2-306c-11d8-b4e9-00304f19f545";
    public const string FileNameGuid = "cad001f3-306c-11d8-b4e9-00304f19f545";
    public const string FileSizeGuid = "cad001f4-306c-11d8-b4e9-00304f19f545";
    public const string FileDateGuid = "cad001f5-306c-11d8-b4e9-00304f19f545";
    public const string ZipSizeGuid = "cad001f6-306c-11d8-b4e9-00304f19f545";
    public const string ObjLinkIDGuid = "cad001f7-306c-11d8-b4e9-00304f19f545";
    public const string ArchiveMethodGuid = "cad001f8-306c-11d8-b4e9-00304f19f545";
    public const string DataGuid = "cad001b2-306c-11d8-b4e9-00304f19f545";
    public const string VersionRuleGuid = "cad00696-306c-11d8-b4e9-00304f19f545";
    public const string DescriptionGuid = "cad0001c-306c-11d8-b4e9-00304f19f545";
    public const string DirectorGuid = "cadd9233-306c-11d8-b4e9-00304f19f545";
    public const string ImbaseKeyGuid = "cad00162-306c-11d8-b4e9-00304f19f545";
    public const string ImbaseCodeGuid = "cad0020f-306c-11d8-b4e9-00304f19f545";
    public const string ImbaseLinkGuid = "cad00209-306c-11d8-b4e9-00304f19f545";
    public const string StorageGuid = "cad0005c-306c-11d8-b4e9-00304f19f545";
    public const string FileGuid = "cad0004b-306c-11d8-b4e9-00304f19f545";
    public const string DocumentFileGuid = "cadd9620-306c-11d8-b4e9-00304f19f545";
    public const string ScannedDocumentGuid = "cadd9644-306c-11d8-b4e9-00304f19f545";
    public const string ConfigFileGuid = "cad014d4-306c-11d8-b4e9-00304f19f545";
    public const string CompositionContextGuid = "cad00651-306c-11d8-b4e9-00304f19f545";
    public const string BufferSizeGuid = "cad00027-306c-11d8-b4e9-00304f19f545";
    public const string StorageTableNameGuid = "cad00028-306c-11d8-b4e9-00304f19f545";
    public const string StorageTypeGuid = "cad00000-306c-11d8-b4e9-00304f19f545";
    public const string ObjectTypeGuidsGuid = "cad00149-306c-11d8-b4e9-00304f19f545";
    public const string RelationTypeGuidsGuid = "cad0014a-306c-11d8-b4e9-00304f19f545";
    public const string RelationTypeGuidGuid = "cad001a9-306c-11d8-b4e9-00304f19f545";
    public const string FilterSelectionGuid = "cad00621-306c-11d8-b4e9-00304f19f545";
    public const string SortIndexGuid = "cad00202-306c-11d8-b4e9-00304f19f545";
    public const string ExternalUserGuid = "cad002df-306c-11d8-b4e9-00304f19f545";
    public const string OKPGuid = "cad0038a-306c-11d8-b4e9-00304f19f545";
    public const string UserNameGuid = "cad0001d-306c-11d8-b4e9-00304f19f545";
    public const string FormatGuid = "cad00255-306c-11d8-b4e9-00304f19f545";
    public const string CountGuid = "cad00267-306c-11d8-b4e9-00304f19f545";
    public const string NormalizedNameGuid = "cad00798-306c-11d8-b4e9-00304f19f545";
    public const string PositionGuid = "cad00270-306c-11d8-b4e9-00304f19f545";
    public const string ActiveStorageGuid = "cad00032-306c-11d8-b4e9-00304f19f545";
    public const string ArticleIDGuid = "cad00622-306c-11d8-b4e9-00304f19f545";
    public const string DocumentIDGuid = "cad00623-306c-11d8-b4e9-00304f19f545";
    public const string WeightGuid = "cad00275-306c-11d8-b4e9-00304f19f545";
    public const string UnitWeightGuid = "cad00276-306c-11d8-b4e9-00304f19f545";
    public const string SizeGuid = "cad00277-306c-11d8-b4e9-00304f19f545";
    public const string ZoneGuid = "cad0027a-306c-11d8-b4e9-00304f19f545";
    public const string SubdivisionGuid = "cad00281-306c-11d8-b4e9-00304f19f545";
    public const string LiteraGuid = "cad0038b-306c-11d8-b4e9-00304f19f545";
    public const string NormControlledByGuid = "cad00283-306c-11d8-b4e9-00304f19f545";
    public const string ScriptTextGuid = "cad00366-306c-11d8-b4e9-00304f19f545";
    public const string EmailGuid = "cad002de-306c-11d8-b4e9-00304f19f545";
    public const string HomeAddressGuid = "cad002dc-306c-11d8-b4e9-00304f19f545";
    public const string PhoneGuid = "cad002da-306c-11d8-b4e9-00304f19f545";
    public const string PostalAddressGuid = "cad015dd-306c-11d8-b4e9-00304f19f545";
    public const string HomePhoneGuid = "cad002dd-306c-11d8-b4e9-00304f19f545";
    public const string MobilePhoneGuid = "cad015df-306c-11d8-b4e9-00304f19f545";
    public const string OfficeGuid = "cad002db-306c-11d8-b4e9-00304f19f545";
    public const string LockedUserGuid = "cadd99fb-306c-11d8-b4e9-00304f19f545";
    public const string ObjectLinkGuid = "cad001be-306c-11d8-b4e9-00304f19f545";
    public const string CalendarGuid = "cad00ea5-306c-11d8-b4e9-00304f19f545";
    public const string UserCalendarGuid = "cadd9b9f-306c-11d8-b4e9-00304f19f545";
    public const string IterationIdGuid = "cadd95a1-306c-11d8-b4e9-00304f19f545";
    public const string UserHireDateGuid = "cadd9bf1-306c-11d8-b4e9-00304f19f545";
    public const string UserFireDateGuid = "cadd9bf2-306c-11d8-b4e9-00304f19f545";
  }

  private static class Implementation
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ColumnContents GetDefaultContent([NotEmpty] int attributeID)
    {
      return (MetaDataHelperService.Instance.GetAttributeType(attributeID) ?? throw new AttributeWithIdNotFoundException(attributeID, $"Атрибут с ID={attributeID} не найден!")).GetDefaultContent();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetIdByGuid([NotEmpty] System.Guid guid, bool throwIfNotFound = true)
    {
      int attributeTypeId = MetaDataHelperService.Instance.GetAttributeTypeID(guid);
      return !throwIfNotFound || !Intermech.Check.AttributeIdIsEmpty(attributeTypeId) ? attributeTypeId : throw new AttributeWithGuidNotFoundException(guid, $"Атрибут с Guid={guid} не найден!");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetIdByGuidOrName([NotNull, NotWhitespace] string guidOrName, bool throwIfNotFound = true)
    {
      System.Guid result;
      if (System.Guid.TryParse(guidOrName, out result))
        return Attributes.Implementation.GetIdByGuid(result, throwIfNotFound);
      int attributeByTypeNameId = MetaDataHelperService.Instance.GetAttributeByTypeNameID(guidOrName);
      return !throwIfNotFound || !Intermech.Check.AttributeIdIsEmpty(attributeByTypeNameId) ? attributeByTypeNameId : throw new AttributeNotFoundException($"Атрибут \"{guidOrName}\" не найден!", (string) null, 0L);
    }
  }
}
