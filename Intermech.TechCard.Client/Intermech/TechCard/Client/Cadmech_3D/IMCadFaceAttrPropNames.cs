// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Cadmech_3D.IMCadFaceAttrPropNames
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.CADInterface.Proxies.Cadmech;
using System.ComponentModel;

#nullable disable
namespace Intermech.TechCard.Client.Cadmech_3D;

/// <summary>Допустимые наименование имен свойств</summary>
internal static class IMCadFaceAttrPropNames
{
  /// <summary>Уникальный идентификатор параметра</summary>
  [Description("Уникальный идентификатор параметра")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St, IMTextFaceAttributeType.Tlr, IMTextFaceAttributeType.Base, IMTextFaceAttributeType.Sc, IMTextFaceAttributeType.Cl, IMTextFaceAttributeType.Ms, IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich, IMTextFaceAttributeType.Wj, IMTextFaceAttributeType.Parameter})]
  public const string UUID = "UUID";
  /// <summary>Ccылка на пункт технических требований</summary>
  [Description("Ccылка на пункт технических требований")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St, IMTextFaceAttributeType.Tlr, IMTextFaceAttributeType.Sc, IMTextFaceAttributeType.Cl, IMTextFaceAttributeType.Ms, IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich, IMTextFaceAttributeType.Wj})]
  public const string TTUUID = "TTUUID";
  /// <summary>Тип стрелки</summary>
  /// <remarks>
  /// _None        = 0,
  /// _ClosedFilled= 1,//залитая стрелка
  /// _DotSmall    = 2,//точка
  /// _Datum       = 3,//треугольник
  /// _Half1Filled = 11,//стрелка сварки прямая
  /// _Half2Filled = 12,//стрелка сварки обратная
  /// </remarks>
  [Description("Тип стрелки")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St, IMTextFaceAttributeType.Tlr, IMTextFaceAttributeType.Base, IMTextFaceAttributeType.Sc, IMTextFaceAttributeType.Cl, IMTextFaceAttributeType.Ms, IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich, IMTextFaceAttributeType.Wj, IMTextFaceAttributeType.Parameter})]
  public const string LeaderStyle = "LeaderStyle";
  /// <summary>Вид знака</summary>
  /// <remarks>
  /// Вид знака:
  /// Способ обработки не установлен( знак - птичка )
  ///   stNoProc=0,
  /// Обработка с удалением слоя материала( знак - птичка с треугольником )
  ///   stMatRem=1,
  /// Обработка без удаления слоя материала( знак - птичка с кружком )
  ///   stNoMatRem=2
  ///  </remarks>
  [Description("Вид знака")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string Type = "Type";
  /// <summary>Обозначение</summary>
  [Description("Обозначение")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {})]
  public const string Designation = "Designation";
  /// <summary>Примечание</summary>
  [Description("Примечание")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St, IMTextFaceAttributeType.Tlr})]
  public const string Note = "Note";
  /// <summary>Параметр шероховатости 1</summary>
  [Description("Параметр шероховатости 1")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string ST1 = "ST1";
  /// <summary>Параметр шероховатости 2</summary>
  [Description("Параметр шероховатости 2")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string ST2 = "ST2";
  /// <summary>Параметр шероховатости 3</summary>
  [Description("Параметр шероховатости 3")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string ST3 = "ST3";
  /// <summary>Параметр шероховатости 4</summary>
  [Description("Параметр шероховатости 4")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string ST4 = "ST4";
  /// <summary>Способ обработки поверхности</summary>
  [Description("Способ обработки поверхности")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string PType = "PType";
  /// <summary>Направление неровностей</summary>
  /// <remarks>
  /// dNone=0,
  /// dPrl=1, //=
  /// dPrp=2,	//T
  /// dCrs=3, //X
  /// dM=4,		//M
  /// dC=5,		//C
  /// dR=6,		//R
  /// dP=7		//P
  /// </remarks>
  [Description("Направление неровностей")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string Direction = "Direction";
  /// <summary>Шероховатость пов-ей образующих контур</summary>
  [Description("Шероховатость пов-ей образующих контур")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string Circle = "Circle";
  /// <summary>Вид знака в углу</summary>
  /// <remarks>
  /// Нет знака
  ///   mNoMark=0,
  /// Знак с текстом(обозначением шероховатости)
  ///  mText=1,
  /// Знак без текста(обозначения шероховатости, в скобках маленький знак)
  /// 	mNoText=2
  /// </remarks>
  [Description("Вид знака в углу")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string Mark = "Mark";
  /// <summary>Скобки</summary>
  /// <remarks>
  /// Нет скобок
  ///   brNone=0,
  /// Круглые скобки
  ///   brParant=1,
  /// Квадратные скобки
  ///   brSquare=2
  /// </remarks>
  [Description("Скобки")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string Brackets = "Brackets";
  /// <summary>Базовая длина 1</summary>
  [Description("Базовая длина 1")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string BaseLeng = "BaseLeng";
  /// <summary>Базовая длина 2</summary>
  [Description("Базовая длина 2")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string BaseLeng2 = "BaseLeng2";
  /// <summary>Базовая длина 3</summary>
  [Description("Базовая длина 3")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string BaseLeng3 = "BaseLeng3";
  /// <summary>Базовая длина 4</summary>
  [Description("Базовая длина 4")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.St})]
  public const string BaseLeng4 = "BaseLeng4";
  /// <summary>Обозначение допуска 11</summary>
  /// <remarks>
  /// Тип допуска
  ///   ttNone=0,//"НИЧЕГО",-1
  /// 	ttLine=2,//"Допуск прямолинейности",0
  /// 	ttPlane=3,//"Допуск плоскостности",1
  /// 	ttRound=4,//"Допуск круглости",2
  /// 	ttCylinder=5,//"Допуск цилиндричности",3
  /// 	ttProfile=6,//"Допуск профиля продольного сечения",4
  /// 
  /// 	ttParallel=8,//"Допуск параллельности",5
  /// 	ttPerp=9,//"Допуск перпендикулярности",6
  /// 	ttPitch=10,//"Допуск наклона",7
  /// 	ttCoAxis=11,//"Допуск соосности",8
  /// 	ttSymmetric=12,//"Допуск симметричности",9
  /// 	ttPosition=13,//"Позиционный допуск ",10
  /// 	ttCrossAxis=14,//"Допуск пересечения осей",11
  /// 
  /// 	ttPulseRad=16,//"Допуск радиального биения",12
  /// 	ttPulseEnd=17,//"Допуск торцевого биения",12
  /// 	ttPulseDirect=18,//"Допуск биения в заданном направлении",12
  /// 	ttFullPulseRad=19,//"Допуск полного радиального биения",13
  /// 	ttFullPulseEnd=20,//"Допуск полного торцевого биения",13
  /// 	ttShapeProf=21,//"Допуск формы заданного профиля",14
  /// 	ttShapeSurf=22//"Допуск формы заданной поверхности",15
  ///  </remarks>
  [Description("Обозначение допуска 11")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string TlrType11 = "TlrType11";
  /// <summary>Обозначение допуска 12</summary>
  /// <remarks> См. Обозначение допуска 11</remarks>
  [Description("Обозначение допуска 12")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string TlrType12 = "TlrType12";
  /// <summary>Обозначение допуска 21</summary>
  /// <remarks> См. Обозначение допуска 11</remarks>
  [Description("Обозначение допуска 21")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string TlrType21 = "TlrType21";
  /// <summary>Обозначение допуска 22</summary>
  /// <remarks> См. Обозначение допуска 11</remarks>
  [Description("Обозначение допуска 22")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string TlrType22 = "TlrType22";
  /// <summary>Значение допуска 1</summary>
  [Description("Значение допуска 1")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string Value1 = "Value1";
  /// <summary>Значение допуска 2</summary>
  [Description("Значение допуска 2")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string Value2 = "Value2";
  /// <summary>Префикс 1</summary>
  /// <remarks>
  /// tpNone=0,
  /// tpDiam=1,
  /// tpRad=2,
  /// tpT=3, //допуск указан в диаметральном выражении
  /// tpT2=4 //допуск указан в радиусном выражении
  /// </remarks>
  [Description("Префикс 1")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string Prefix1 = "Prefix1";
  /// <summary>Префикс 2</summary>
  /// <remarks> См. Префикс 1</remarks>
  [Description("Префикс 2")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string Prefix2 = "Prefix2";
  /// <summary>Знак зависимого допуска 1</summary>
  /// <remarks>
  /// tdNone=0,
  /// tdM=1,
  /// tdL=2,
  /// tdS=3
  /// </remarks>
  [Description("Знак зависимого допуска 1")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr1 = "DepTlr1";
  /// <summary>Знак зависимого допуска 2</summary>
  /// <remarks> См. Знак зависимого допуска 1</remarks>
  [Description("Знак зависимого допуска 2")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr2 = "DepTlr2";
  /// <summary>Знак выступающего поля допуска 1</summary>
  /// <remarks>
  /// 0,
  /// 1
  /// </remarks>
  [Description("Знак выступающего поля допуска 1")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string signP1 = "signP1";
  /// <summary>Знак выступающего поля допуска 2</summary>
  [Description("Знак выступающего поля допуска 2")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string signP2 = "signP2";
  /// <summary>Знак зависимого допуска 11</summary>
  [Description("Знак зависимого допуска 11")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr11 = "DepTlr11";
  /// <summary>Знак зависимого допуска 12</summary>
  [Description("Знак зависимого допуска 12")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr12 = "DepTlr12";
  /// <summary>Знак зависимого допуска 13</summary>
  [Description("Знак зависимого допуска 13")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr13 = "DepTlr13";
  /// <summary>Знак зависимого допуска 21</summary>
  [Description("Знак зависимого допуска 21")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr21 = "DepTlr21";
  /// <summary>Знак зависимого допуска 22</summary>
  [Description("Знак зависимого допуска 22")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr22 = "DepTlr22";
  /// <summary>Знак зависимого допуска 23</summary>
  [Description("Знак зависимого допуска 23")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string DepTlr23 = "DepTlr23";
  /// <summary>Привязка к базе</summary>
  [Description("Привязка к базе ")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID = "baseUUID";
  /// <summary>Привязка к базе 0</summary>
  [Description("Привязка к базе 0")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID0 = "baseUUID0";
  /// <summary>Привязка к базе 1</summary>
  [Description("Привязка к базе 1")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID1 = "baseUUID1";
  /// <summary>Привязка к базе 2</summary>
  [Description("Привязка к базе 2")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID2 = "baseUUID2";
  /// <summary>Привязка к базе 3</summary>
  [Description("Привязка к базе 3")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID3 = "baseUUID3";
  /// <summary>Привязка к базе 4</summary>
  [Description("Привязка к базе 4")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID4 = "baseUUID4";
  /// <summary>Привязка к базе 5</summary>
  [Description("Привязка к базе 5")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID5 = "baseUUID5";
  /// <summary>Привязка к базе 6</summary>
  [Description("Привязка к базе 6")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID6 = "baseUUID6";
  /// <summary>Привязка к базе 7</summary>
  [Description("Привязка к базе 7")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID7 = "baseUUID7";
  /// <summary>Привязка к базе 8</summary>
  [Description("Привязка к базе 8")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID8 = "baseUUID8";
  /// <summary>Привязка к базе 9</summary>
  [Description("Привязка к базе 9")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID9 = "baseUUID9";
  /// <summary>Привязка к базе 10</summary>
  [Description("Привязка к базе 10")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID10 = "baseUUID10";
  /// <summary>Привязка к базе 11</summary>
  [Description("Привязка к базе 11")]
  [ImCadPropType(IMCadFaceAttrPropType.GUID)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Tlr})]
  public const string baseUUID11 = "baseUUID11";
  /// <summary>Обозначение базы</summary>
  [Description("Обозначение базы")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Base, IMTextFaceAttributeType.Cl})]
  public const string Des = "DES";
  /// <summary>Тип базы</summary>
  [Description("Тип базы")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Base, IMTextFaceAttributeType.Cl})]
  public const string DesType = "DESTYPE";
  /// <summary>Примечание</summary>
  [Description("Примечание")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Sc, IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich})]
  public const string Comment = "Comment";
  /// <summary>Тип выноски</summary>
  [Description("Тип выноски")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Cl})]
  public const string DType = "DType";
  /// <summary>Текст выноски</summary>
  /// <remarks>
  /// Текст выноски
  /// специальный параметр, в CADMECH его нет
  /// там применяется счетчик
  /// например для каждой выноски:
  /// String0
  /// String1
  /// String2 и так далее
  /// в IPS это будет записываться в один атрибут,
  /// каждая строка будет отделена #13+#10
  ///  </remarks>
  [Description("Текст выноски")]
  [ImCadPropType(IMCadFaceAttrPropType.Strings)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Cl})]
  public const string Strings = "Strings";
  /// <summary>Способ</summary>
  /// <remarks>1-Клеймение, 0-маркировка</remarks>
  [Description("Способ")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Ms})]
  public const string isStamp = "isStamp";
  /// <summary>Содержание</summary>
  [Description("Содержание")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Ms})]
  public const string Content = "Content";
  /// <summary>Соединение по замкнутой линии</summary>
  [Description("Соединение по замкнутой линии")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich, IMTextFaceAttributeType.Wj})]
  public const string Close = "Close";
  /// <summary>Соединение изображено с оборотной стороны</summary>
  [Description("Соединение изображено с оборотной стороны")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich, IMTextFaceAttributeType.Wj})]
  public const string BackSide = "BackSide";
  /// <summary>Номер соединения</summary>
  [Description("Номер соединения")]
  [ImCadPropType(IMCadFaceAttrPropType.Strings)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich})]
  public const string Number = "Number";
  /// <summary>Количество соединенй</summary>
  [Description("Количество соединенй")]
  [ImCadPropType(IMCadFaceAttrPropType.Strings)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich})]
  public const string Count = "Count";
  /// <summary>Для сшивания скобами 1-внахлестку, 0-нет</summary>
  [Description("Для сшивания скобами 1-внахлестку, 0-нет")]
  [ImCadPropType(IMCadFaceAttrPropType.Integer)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich})]
  public const string Tag = "Tag";
  /// <summary>Для сшивания - к-во швов и расстояние между ними</summary>
  [Description("Для сшивания - к-во швов и расстояние между ними")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.DjSeam, IMTextFaceAttributeType.DjSolder, IMTextFaceAttributeType.DjSplice, IMTextFaceAttributeType.DjWirestich})]
  public const string STag = "STag";
  /// <summary>Стандарт на типы и конструктивные элементы швов</summary>
  [Description("Стандарт на типы и конструктивные элементы швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string GOST = "GOST";
  /// <summary>Стандарт на типы и конструктивные элементы швов</summary>
  [Description("Стандарт на типы и конструктивные элементы швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string GOST_u = "GOST_u";
  /// <summary>Обозначение шва по стандарту</summary>
  [Description("Обозначение шва по стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wDesignation = "Designation";
  /// <summary>Обозначение шва по стандарту</summary>
  [Description("Обозначение шва по стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wDesignation_u = "Designation_u";
  /// <summary>Обозначение способа сварки по стандарту</summary>
  [Description("Обозначение способа сварки по стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wType = "Type";
  /// <summary>Обозначение способа сварки по стандарту</summary>
  [Description("Обозначение способа сварки по стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wType_u = "Type_u";
  /// <summary>Размер катета согласно стандарту</summary>
  [Description("Размер катета согласно стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string Leg = "Leg";
  /// <summary>Размер катета согласно стандарту</summary>
  [Description("Размер катета согласно стандарту")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string Leg_u = "Leg_u";
  /// <summary>Отклонение верхнее / нижнее если нет / значит ±</summary>
  [Description("Отклонение верхнее / нижнее если нет / значит ±")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string LegTolerance = "LegTolerance";
  /// <summary>Отклонение верхнее / нижнее если нет / значит ±</summary>
  [Description("Отклонение верхнее / нижнее если нет / значит ±")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string LegTolerance_u = "LegTolerance_u";
  /// <summary>Дополнительные размеры</summary>
  [Description("Дополнительные размеры")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string ExtraValue = "ExtraValue";
  /// <summary>Дополнительные размеры</summary>
  [Description("Дополнительные размеры")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string ExtraValue_u = "ExtraValue_u";
  /// <summary>Номер шва</summary>
  [Description("Номер шва")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wNumber = "Number";
  /// <summary>Номер шва</summary>
  [Description("Номер шва")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wNumber_u = "Number_u";
  /// <summary>Количество швов</summary>
  [Description("Количество швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wCount = "Count";
  /// <summary>Количество швов</summary>
  [Description("Количество швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string wCount_u = "Count_u";
  /// <summary>Усиление шва снять</summary>
  [Description("Усиление шва снять")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WS = "WS";
  /// <summary>Усиление шва снять с обратной стороны</summary>
  [Description("Усиление шва снять с обратной стороны")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WS_u = "WS_u";
  /// <summary>Шов по незамкнутой линии</summary>
  [Description("Шов по незамкнутой линии")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WC = "WC";
  /// <summary>Шов по незамкнутой линии с обратной стороны</summary>
  [Description("Шов по незамкнутой линии с обратной стороны")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WC_u = "WC_u";
  /// <summary>
  /// Наплывы и неровности шва обработать с плавным переходом к основному
  /// </summary>
  [Description("Наплывы и неровности шва обработать с плавным переходом к основному")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WN = "WN";
  /// <summary>
  /// Наплывы и неровности шва обработать с плавным переходом к основному металлу с обратной стороны
  /// </summary>
  [Description("Наплывы и неровности шва обработать с плавным переходом к основному металлу с обратной стороны")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WN_u = "WN_u";
  /// <summary>Комментарий в таблице швов</summary>
  [Description("Комментарий в таблице швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string TableNote = "TableNote";
  /// <summary>Комментарий в таблице швов</summary>
  [Description("Комментарий в таблице швов")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string TableNote_u = "TableNote_u";
  /// <summary>Длина шва</summary>
  [Description("Длина шва")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WeldLeng = "WeldLeng";
  /// <summary>Длина шва</summary>
  [Description("Длина шва")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string WeldLeng_u = "WeldLeng_u";
  /// <summary>
  /// Обозначение контрольного комплекса или категории контроля шва
  /// </summary>
  [Description("Обозначение контрольного комплекса или категории контроля шва")]
  [ImCadPropType(IMCadFaceAttrPropType.String)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string Control = "Control";
  /// <summary>Шов выполнить при монтаже изделия</summary>
  [Description("Шов выполнить при монтаже изделия")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string Mount = "Mount";
  /// <summary>Соединение изображено с двух сторон</summary>
  [Description("Соединение изображено с двух сторон")]
  [ImCadPropType(IMCadFaceAttrPropType.Boolean)]
  [ImCadAttrType(new IMTextFaceAttributeType[] {IMTextFaceAttributeType.Wj})]
  public const string BothSide = "BothSide";
  /// <summary>
  /// Наименование параметра, в значении которого храниться ссылка на типовой элемент Imbase
  /// </summary>
  public const string Template = "FCN_TEMPLATE";
}
