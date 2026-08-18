// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.A_NastrVed
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.AVS.Properties;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AVS.Victor;

public class A_NastrVed : Form
{
  public Vedomost_VB.TypeDoc TypeDoc;
  private long templateID_curr_Vyvod = -1;
  private long templateID_Vyvod = -1;
  private long templateID_curr_Xml = -1;
  private long templateID_Xml = -1;
  private long templateID_B = -1;
  private long templateID_curr_Avs = -1;
  private long templateID_Avs = -1;
  private long templateID_B_Avs = -1;
  public Guid _guidTypeVed_Curr;
  public Guid _guidTemplateVed_Curr;
  public Vedomost_VB_Static.One_Conformity_Template_Nastr _one_Conformity_Template_Nastr_Curr;
  public string _documentName_Curr;
  public IMSObjectType _imsObjectType_Curr;
  private string docName = "";
  private bool IsModifiedAll;
  private bool isCreate = true;
  private bool IsModified_Page_Bases;
  private bool IsModified_Page_Sbor;
  private bool IsModified_Page_Sortings;
  private bool IsModified_Page_Razdels;
  private bool IsModified_Page_PodRazdels;
  private bool IsModified_Page_Zagolovki;
  private bool IsModified_Page_Vyvod;
  private bool IsModified_Page_Xml;
  private bool IsModified_Page_Avs;
  public bool IsBylo_IsModified_Page_Vyvod;
  public bool IsBylo_IsModified;
  private bool IsModified_Page_Service;
  private bool isByloButtonTypeVedTo_Click;
  private string nameRazdel = "";
  public string formaGroupDoc_start = "Ed";
  private bool IsModifiedFromFile;
  private bool isButtonDefault;
  private bool isButtonB;
  private bool is_one_Ved_Nastr_New;
  private bool isKudaVhoditInfo;
  private bool isItogoInfo;
  private bool noClosing;
  private List<string> listWarnings = new List<string>();
  public One_Ved_Nastr _one_Ved_Nastr_Curr;
  public One_ImsObjectType_With_One_Ved_Nastr _one_ImsObjectType_With_One_Ved_Nastr_Curr;
  public One_Ved_Nastr _one_Ved_Nastr_Window_Curr;
  public One_Ved_Nastr _one_Ved_Nastr_Tmp;
  private List<QuickObjectInfo> list_CalalogsImbaseFull;
  private List<QuickObjectInfo> list_CalalogsImbaseTmp;
  private int _indexImageList_Section;
  private int _indexImageList_Empty;
  private int _indexImageList_InvalidRule;
  private int _indexImageList_GreenBall;
  private int _indexImageList_RuleCriterion;
  private Vedomost_VB.Usl_Read_From_SP _usl_Read_From_SP_CurrentRazdel;
  private Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One_Current;
  private A_NastrVed.UsloviaNode usloviaNode_Current;
  private Vedomost_VB.Usl_Read_From_SP _usl_Read_From_SP_Reference_CurrentRazdel;
  private Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_Reference_One_Current;
  private A_NastrVed.UsloviaNode usloviaNode_Current_Reference;
  private Vedomost_VB.Sorting_Usl_OneRazdel sorting_Usl_OneRazdel_curr;
  private int i_sorting_Usl_OneRazdel_curr = -1;
  private Vedomost_VB.Sorting_Usl_One sorting_Usl_One_curr;
  private int i_sorting_Usl_One_curr = -1;
  private Vedomost_VB.Sorting_Usl_Doc_OneRazdel sorting_Usl_Doc_OneRazdel_curr;
  private int i_sorting_Usl_Doc_OneRazdel_curr = -1;
  private Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa_curr;
  private int i_sorting_Usl_Doc_One_curr = -1;
  private bool isSortDoc;
  private A_NastrVed.TypeSortRec typeSortRec;
  private int rowNumCurrent_Sorting = -1;
  private DataGridView dataGridView_Sorting_Curr;
  private int rowNumCurrent_Razdels = -1;
  private int rowNumPrevision_Razdels = -1;
  private int rowNumCurrent_PodRazdels = -1;
  private int rowNumPrevision_PodRazdels = -1;
  private Vedomost_VB.OneRazdelVed oneRazdelVed_Curr;
  private Vedomost_VB.OneRazdelVed oneRazdelVed_Prevision;
  private Vedomost_VB.OnePodRazdelVed onePodRazdelVed_Curr;
  private Vedomost_VB.OnePodRazdelVed onePodRazdelVed_Prevision;
  private bool isVydelenieRazdelaAuto;
  private bool isVydeleniePodRazdelaAuto;
  private bool is_Podrazdel_Auto;
  private bool is_Extended_List_Names_Pages_ByTemplate;
  private string name_Page_Common = "";
  private int rowNumCurrent_Zagolovok = -1;
  private ImDocument imDocument_template_Vyvod;
  private ImDocument imDocument_template_Vyvod_FromDump;
  private long templID_Vyvod;
  private DocumentControl docControl_Vyvod;
  private DockControl docKcontrol_Vyvod;
  private DocumentTreeViewDlg documentTreeViewDlg_Vyvod;
  private A_NastrVed.OneVyvodNode oneTreeNode_Current;
  private Vedomost_VB.OneRecordToPrint oneRecordToPrint_Current;
  private Vedomost_VB.OneGrafaToPrint oneGrafaToPrint_Current;
  private int i_curr_oneGrafaToPrint_Current = -1;
  private Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint_current;
  private Vedomost_VB.AlgorithmToPrint algorithmToPrint_curr;
  private Vedomost_VB.AlgorithmToPrint algorithmToPrint;
  private Vedomost_VB.AlgorithmToPrint algorithmToPrint_B;
  private bool isradioButtonEdOrA = true;
  private bool isradioButtonGroupB;
  private bool isradioButtonEdOrA_Avs = true;
  private bool isradioButtonGroupB_Avs;
  private bool estGroupB;
  private ImDocument imDocument_template_Xml;
  private long templID_Xml;
  private DocumentControl docControl_Xml;
  private DockControl docKcontrol_Xml;
  private DocumentTreeViewDlg documentTreeViewDlg_Xml;
  private string text_Old = "";
  private Vedomost_VB_Static.OneXmlNode oneXmlNode_Curr;
  private ImDocument imDocument_template_Avs;
  private long templID_Avs;
  private DocumentControl docControl_Avs;
  private DockControl docKcontrol_Avs;
  private DocumentTreeViewDlg documentTreeViewDlg_Avs;
  private A_NastrVed.OneAvsNode oneTreeNode_Avs_Current;
  private Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs_Current;
  private Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafa_Avs_Current;
  private int i_curr_oneGrafa_Avs_Current = -1;
  private Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs_current;
  private Vedomost_VB.Algorithm_Avs6_To_Ips algorithm_Avs_curr;
  private Vedomost_VB.Algorithm_Avs6_To_Ips algorithm_Avs;
  private Vedomost_VB.Algorithm_Avs6_To_Ips algorithm_Avs_B;
  private bool est_Avs_GroupB;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  private Button buttonSave1;
  private Button buttonDefault;
  internal Button bCancel;
  internal Button bOK;
  private System.Windows.Forms.TabControl tabControl_Nastr;
  private System.Windows.Forms.TabPage tabPage_Bases;
  private System.Windows.Forms.TabPage tabPage_Sbor;
  private System.Windows.Forms.TabPage tabPage_Sorting;
  private System.Windows.Forms.TabPage tabPage_Razdels;
  private System.Windows.Forms.TabPage tabPage_Vyvod;
  private System.Windows.Forms.TabControl tabControl_Usl_Bases;
  private System.Windows.Forms.TabPage tabPage_Bases_Main;
  private Label label_Usl_Bases_MainCaption;
  private GroupBox groupBox_Usl_Bases_MainStep;
  private CheckBox checkBox_Usl_Bases_isMainSumm;
  private CheckBox checkBox_Usl_Bases_isMainCreateVtorRecords;
  private CheckBox checkBox_Usl_Bases_isMainSort2;
  private CheckBox checkBox_Usl_Bases_isMainSummOdinakovyh;
  private CheckBox checkBox_Usl_Bases_isMainSort1;
  private System.Windows.Forms.TabPage tabPage_Usl_Bases_Sbor;
  private Label label_Usl_Bases_Sbor1;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedAddToSp;
  private GroupBox groupBox_Usl_Bases_Sbor_VedStep;
  private GroupBox groupBox_Usl_Bases_Sbor_VedGroup;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedMergerIsp;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedSortGroup;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedExtrectionVtor;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedUnion;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedSort1;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedAddFuncGroup;
  private ToolTip toolTip1;
  private ImageList imageList1;
  private ImageList imagesToolbars;
  private GroupBox groupBox_Sorting_AttribVedRec1;
  private ListBox listBox_Sorting_AttribVedRec;
  private DataGridView dataGridView_Sorting;
  private GroupBox groupBox_Sorting_PoriadokSortirovki;
  private RadioButton radioButton_Sorting_PoriadokSortirovkiUbyvanie;
  private RadioButton radioButton_Sorting_PoriadokSortirovkiVozrastanie;
  private GroupBox groupBox_Sorting_PustyeStroki;
  private RadioButton radioButton_Sorting_PustyeStrokiVkonce;
  private RadioButton radioButton_Sorting_PustyeStrokiVnathale;
  private GroupBox groupBox_Sorting_Sravnenie;
  private RadioButton radioButton_Sorting_SravnenieNumber;
  private RadioButton radioButton_Sorting_SravnenieSymbol;
  private GroupBox groupBox_Sorting_End;
  private ComboBox comboBox_Sorting_SymbolEnd;
  private Label labelEnd_Sorting_2;
  private NumericUpDown numericUpDown_Sorting_NumberEnd;
  private Label labelEnd_Sorting_1;
  private GroupBox groupBox_Sorting_Do;
  private RadioButton radioButton_Sorting_DoSymbolNumbEnd;
  private RadioButton radioButton_Sorting_DoSymbolNumb;
  private RadioButton radioButton_Sorting_DoBukvyNumb;
  private RadioButton radioButton_Sorting_DoEnd;
  private GroupBox groupBox_Sorting_Begin;
  private ComboBox comboBox_Sorting_SymbolBegin;
  private Label labelBegin_Sorting_2;
  private NumericUpDown numericUpDown_Sorting_NumberBegin;
  private Label labelBegin_Sorting_1;
  private GroupBox groupBox_Sorting_Ot;
  private RadioButton radioButton_Sorting_OtSymbolNumbEnd;
  private RadioButton radioButton_Sorting_OtSymbolNumb;
  private RadioButton radioButton_Sorting_OtBukvyNumb;
  private RadioButton radioButton_Sorting_OtBegin;
  private Button buttonDelete_Sorting_1;
  private Button buttonAdd_Sorting_1;
  private Button buttonEdit_Sorting_1;
  private DataGridViewImageColumn ImgColumn;
  private DataGridViewTextBoxColumn ColumnAttribut;
  private DataGridViewTextBoxColumn ColumnOt;
  private DataGridViewTextBoxColumn ColumnDo;
  private DataGridViewTextBoxColumn ColumnSravnenie;
  private DataGridViewTextBoxColumn ColumnPustye;
  private ImageList imageListSort;
  private Button _btnMoveUp_Sorting;
  private Button _btnMoveDown_Sorting;
  private GroupBox Razdels_groupBoxListRazdelov;
  private DataGridView Razdels_dataGridViewListRazdels;
  private Button buttonDelete_Razdel;
  private Button buttonAdd_Razdel;
  private System.Windows.Forms.TabControl tabControl_Vyvod;
  private System.Windows.Forms.TabPage tabPage_Vyvod_2;
  private GroupBox groupBox_Vyvod2_SkipRows;
  private Label label_Vyvod2_AfterRemark;
  private NumericUpDown numericUpDown_Vyvod2_AfterRemark;
  private Label label_Vyvod2_AfterInfo;
  private NumericUpDown numericUpDown_Vyvod2_AfterInfo;
  private GroupBox group_Vyvod2_BoxLizm;
  private CheckBox checkBox_Vyvod2_IncludedLizmInDoc;
  private Label label_Vyvod2_Lizm;
  private NumericUpDown numericUpDown_Vyvod2_Lizm;
  private CheckBox checkBox_Vyvod2_Lizm;
  private DockManager dockMan_Vyvod;
  private DockContainer docKcontainer_Vyvod;
  private System.Windows.Forms.TabPage tabPage_Vyvod_1;
  private DockContainer rightDock_Vyvod;
  private GroupBox groupBox_Vyvod_AttribVedRec1;
  private ListBox listBox_Vyvod_AttribVedRec;
  private GroupBox groupBox_Vyvod_Ved_Pasport;
  private ListBox listBoxAttrib_Vyvod_VedPasport;
  private DockContainer bottomDock;
  private DockContainer topDock_Vyvod;
  private DockContainer leftDock_Vyvod;
  private Panel panel_Vyvod_1;
  private GroupBox groupBox_Vyvod_Forma;
  private RadioButton radioButton_Vyvod_GroupB;
  private RadioButton radioButton_Vyvod_EdOrA;
  private NumericUpDown numeric_Vyvod_UpDownKolGraf;
  private Label label_Vyvod_Graf;
  private Button button_Vyvod_AddAttribut;
  private Button button_Vyvod_Delete;
  private Button button_Vyvod_Edit;
  private Button button_Vyvod_AddCell;
  private GroupBox groupBox_Vyvod_TextRazdelitel;
  private ComboBox comboBox_Vyvod_TextRazdelitel;
  private TreeView treeView_Vyvod;
  private DocumentContainer docContainer_Vyvod;
  private Button buttonSelectVed;
  private Button buttonCopyFrom;
  private System.Windows.Forms.TabPage tabPage_Service;
  private Button buttonServicesCopyAll;
  private Button buttonServicesDefaultAll;
  private Button buttonServicesTypeVedTo;
  private Label labelService2;
  private Label labelService1;
  private Button buttonServiceCreateDump;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
  private Button buttonServicesFileOpen;
  private Button buttonSevicesForGroupB;
  private System.Windows.Forms.TabPage tabPage_Usl_Bases_SborDialog;
  private GroupBox groupBox_Usl_Bases_Sbor_Input;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputIzd;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputDoc;
  private Button buttonAdd_PodRazdel;
  private Button buttonDelete_PodRazdel;
  private CheckBox checkBox_Razdel_PodRazdel;
  private GroupBox Razdels_groupBoxListPodRazdelov;
  private DataGridView Razdels_dataGridViewListPodRazdels;
  private CheckBox checkBox_Usl_Bases_isOnlyUroven1;
  private Button button_Vyvod_PoRazdelam;
  private Button button_Vyvod_Obshaia;
  private Label label_ServicesFileOpen;
  private Label label_ServiceCreateDump;
  private Label label_SevicesForGroupB;
  private Label label_ServicesTypeVedTo;
  private Label label_ServicesCopyAll;
  private Label label_ServicesDefaultAll;
  private Button buttonWarnings;
  private GroupBox groupBox_Usl_Bases_ImbaseCatalog;
  private ListBox listBox_QuickObjectInfo;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
  private Button button_Delete_From_To_listBox_QuickObjectInfo;
  private Button button_Add_To_listBox_QuickObjectInfo;
  private ListBox listBox_CatalogsImbase;
  private Label label_QuickObjectInfo;
  private Label label_CatalogsImbase;
  private System.Windows.Forms.TabPage tabPage_Xml;
  private DocumentContainer docContainer_Xml;
  private DockContainer docKcontainer_Xml;
  private DockManager dockMan_Xml;
  private TreeView treeView_Xml;
  private GroupBox groupBox_Xml_Text;
  private TextBox textBox_Xml_Text;
  private Button button_Xml_Delete;
  private Button button_Xml_Edit;
  private Button button_Xml_Add;
  private GroupBox groupBox_Xml_In;
  private GroupBox groupBox_Xml_Out;
  private RadioButton radioButton_Xml_PassportOutNo;
  private RadioButton radioButton_Xml_PassportOutDialog;
  private RadioButton radioButton_Xml_PassporOutAlways;
  private GroupBox groupBox_Xml_EmptyString;
  private Label label_Xml_AfterRemark;
  private NumericUpDown numeric_UpDown_Xml_AfterRemark;
  private Label label_Xml_AfterInfo;
  private NumericUpDown numeric_UpDown_Xml_AfterInfo;
  private RadioButton radioButton_Xml_PassportInNo;
  private RadioButton radioButton_Xml_PassportInDialog;
  private RadioButton radioButton_Xml_PassporInAlways;
  private GroupBox groupBox_Xml_Folder_In;
  private TextBox textBox_Xml_Folder_In;
  private Button button_Xml_Folder_In;
  private CheckBox checkBox_Services_CreateDump;
  private GroupBox groupBox_Vyvod_Additional;
  private CheckBox checkBox_Vyvod_Additional4;
  private CheckBox checkBox_Vyvod_Additional3;
  private CheckBox checkBox_Vyvod_Additional2;
  private CheckBox checkBox_Vyvod_Additional1;
  private System.Windows.Forms.TabPage tabPage_Avs6;
  private GroupBox groupBox_Avs6_Fields;
  private ListBox listBox_Avs6_Fields;
  private Panel panel_Avs_1;
  private GroupBox groupBox_Avs_Forma;
  private RadioButton radioButton_Avs_GroupB;
  private RadioButton radioButton_Avs_EdOrA;
  private NumericUpDown numeric_Avs_UpDownKolGraf;
  private Label label_Avs_Graf;
  private Button button_Avs_AddAttribut;
  private Button button_Avs_Delete;
  private Button button_Avs_Edit;
  private Button button_Avs_AddCell;
  private GroupBox groupBox_Avs_TextRazdelitel;
  private ComboBox comboBox_Avs_TextRazdelitel;
  private TreeView treeView_Avs;
  private DockContainer dockContainer_Avs;
  private DockManager dockMan_Avs;
  private DocumentContainer docContainer_Avs;
  private Button button_Avs_Obshaia;
  private Button button_Avs_PoRazdelam;
  private GroupBox groupBox_AccessLevel;
  private RadioButton radioButton_AccessLevel2;
  private RadioButton radioButton_AccessLevel1;
  private RadioButton radioButton_AccessLevel0;
  private GroupBox groupBox_Dump;
  private System.Windows.Forms.TabPage tabPage_Zagolovki;
  protected internal GroupBox groupBox_Zagolovki_AttribVedRec1;
  private ListBox listBox_Zagolovki_AttribVedRec;
  private GroupBox groupBox_Zagolovki_TypeCompare;
  private RadioButton radioButton_Zagolovki_Compare_Symbol;
  private RadioButton radioButton_Zagolovki_Compare_Int;
  private Button button_Zagolovki_FromList;
  private Label label_NoZgolovki;
  private Button button_Zagolovki_EditKeyAttribut;
  private Label label_Zagolovki_Attribut;
  private Label label_Zagolovki_SlevaVverhu;
  private Label label_Zagolovki_SpravaVnizu;
  private Button buttonDelete_Zagolovki;
  private Button buttonAdd_Zagolovki;
  private CheckBox checkBox_Zagolovki_VyvoditPodrazdely;
  private GroupBox groupBox_ListZagolovkov;
  private DataGridView dataGridView_ListZagolovkov;
  private DataGridViewTextBoxColumn Zagolovok_Column1;
  private DataGridViewTextBoxColumn Zagolovok_Column2;
  private System.Windows.Forms.TabControl tabControl_Page_Sbor;
  private System.Windows.Forms.TabPage tabPage_Sbor_Usl;
  private Panel Sbor_Usl_Panel;
  private GroupBox groupBox_Sbor_Usl_I_ILI;
  private RadioButton radioButton_Sbor_Usl_Ili;
  private RadioButton radioButton_Sbor_Usl_I;
  private GroupBox groupBox_Sbor_Usl_Text;
  private TextBox textBox_Sbor_Usl_TextDliaSravnenia;
  private GroupBox groupBox_Sbor_Usl_Sravnenie;
  private RadioButton radioButton_Sbor_Usl_Nathinaetsia;
  private RadioButton radioButton_Sbor_Usl_NeSoderzit;
  private RadioButton radioButton_Sbor_Usl_Soderzit;
  private RadioButton radioButton_Sbor_Usl_NeRavno;
  private RadioButton radioButton_Sbor_Usl_Ravno;
  private GroupBox groupBox_Sbor_Usl_AttributeControl1;
  private SelectAvsAttributeControl select_Sbor_Usl_AttributeControl1;
  private Button button_Sbor_Usl_NeVvodit;
  private Button button_Sbor_Usl_BezUsl;
  private Button button_Sbor_Usl_Delete1;
  private Button button_Sbor_Usl_Edit1;
  private Button button_Sbor_Usl_Add1;
  private GroupBox groupBox_Sbor_Usl_CollapsedTreeView;
  private RadioButton radioButtonCollapsedEmpty;
  private RadioButton radioButtonExpanded;
  private RadioButton radioButtonCollapseAll;
  private GroupBox groupBox_UsloviaVvoda;
  private TreeView treeView_UsloviaSbora;
  private System.Windows.Forms.TabPage tabPage_Sbor_Peredatha;
  private Button button_Sbor_Peredatha_Delete2;
  private Button button_Sbor_Peredatha_Add2;
  private GroupBox groupBox_Sbor_Peredatha_AttributeControl1;
  private SelectAvsAttributeControl select_Sbor_Peredatha_AttributeControl2;
  private GroupBox groupBox_Sbor_Peredatha_ListId;
  private ListBox listBox_Sbor_Peredatha_ListId;
  private System.Windows.Forms.TabPage tabPage_Sbor_Others;
  private CheckBox checkBox_Others_Reference_Show;
  private GroupBox groupBox_Sbor_Others_DopZam;
  private CheckBox checkBox_Sbor_Others_IsDopZam;
  private GroupBox groupBox_Sbor_Others_Complecty;
  private CheckBox checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty;
  private CheckBox checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty;
  private GroupBox groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved;
  private CheckBox checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved;
  private CheckBox checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit;
  private System.Windows.Forms.TabPage tabPage_Sbor_Usl_Reference;
  private Panel Sbor_Usl_Reference_Panel;
  private GroupBox groupBox_Sbor_Usl_I_ILI_Reference;
  private RadioButton radioButton_Sbor_Usl_Ili_Reference;
  private RadioButton radioButton_Sbor_Usl_I_Reference;
  private GroupBox groupBox_Sbor_Usl_Text_Reference;
  private TextBox textBox_Sbor_Usl_TextDliaSravnenia_Reference;
  private GroupBox groupBox_Sbor_Usl_Sravnenie_Reference;
  private RadioButton radioButton_Sbor_Usl_Nathinaetsia_Reference;
  private RadioButton radioButton_Sbor_Usl_NeSoderzit_Reference;
  private RadioButton radioButton_Sbor_Usl_Soderzit_Reference;
  private RadioButton radioButton_Sbor_Usl_NeRavno_Reference;
  private RadioButton radioButton_Sbor_Usl_Ravno_Reference;
  private GroupBox groupBox_Sbor_Usl_CollapsedTreeView_Reference;
  private RadioButton radioButtonCollapsedEmpty_Reference;
  private RadioButton radioButtonExpanded_Reference;
  private RadioButton radioButtonCollapseAll_Reference;
  private GroupBox groupBox_UsloviaVvoda_Reference;
  private TreeView treeView_UsloviaSbora_Reference;
  private GroupBox groupBox_Sbor_Usl_AttributeControl_Reference;
  private SelectAvsAttributeControl select_Sbor_Usl_AttributeControl_Reference;
  private Button button_Sbor_Usl_Reference_NeVvodit;
  private Button button_Sbor_Usl_Reference_BezUsl;
  private Button button_Sbor_Usl_Reference_Delete1;
  private Button button_Sbor_Usl_Reference_Edit1;
  private Button button_Sbor_Usl_Reference_Add1;
  private GroupBox groupBox_Usl_Bases_Sbor_isVedAddToRazdel;
  private RadioButton radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl;
  private RadioButton radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc;
  private GroupBox groupBox_Zagolovki_List_Ved_Id;
  private ListBox listBox_Zagolovki_List_Ved_Id;
  private GroupBox groupBox_Sorting_List_Ved_Id;
  private ListBox listBox_Sorting_List_Ved_Id;
  private GroupBox groupBox_Vyvod_List_Ved_Id;
  private ListBox listBox_Vyvod_List_Ved_Id;
  private CheckBox checkBox_Usl_Bases_Sbor_isInputMat;
  private GroupBox groupBox_Vyvod_isDeleteIdenticalTexts;
  private CheckBox checkBox_Vyvod_isDeleteIdenticalTexts;
  private System.Windows.Forms.TabPage tabPage_Merge;
  private GroupBox groupBox_Merge_AttribVedRec1;
  private ListBox listBox_Merge_AttribVedRec;
  private GroupBox groupBox_Merge_List_Ved_Id;
  private ListBox listBox_Merge_List_Ved_Id;
  private GroupBox groupBox_Merge_List_Merge_Usl2;
  private ListBox listBox_Merge_List_Merge_Usl2;
  private Button button_Merge_Del;
  private Button button_Merge_Add;
  private Button buttonEditTemplate;
  private GroupBox groupBox_Include_Name;
  private TextBox textBox_Include_Name;
  private GroupBox groupBox_Usl_Bases_Sbor_isVedExtrectionVtor;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedSummVtor;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedSortVtor;
  private CheckBox checkBox_Usl_Bases_Sbor_isVedMergerVtor;
  private GroupBox groupBox_Usl_Bases_Sbor_For_ZIP;
  private GroupBox groupBox_Usl_Bases_Sbor_For_ZIP_COMPL;
  private CheckBox checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add;
  private CheckBox checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr;
  private GroupBox groupBox_Usl_Bases_Sbor_For_ZIP_SB;
  private CheckBox checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add;
  private CheckBox checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr;
  private GroupBox groupBox_Conformity_Name_Page_for_Razdel;
  private Button button_Add_NamePage;
  private GroupBox groupBox_NamePage;
  private DataGridView dataGridView_NamePage;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
  private GroupBox groupBox_RazdelVedAndNamePage;
  private DataGridView dataGridView_RazdelVedAndNamePage;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
  private GroupBox groupBox_SpecificationSections;
  private CheckBox checkBox_Specification_Instrument;
  private DataGridView drawGrid_SpecificationSections;
  private DataGridViewCheckBoxColumn Column5;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
  private CheckBox checkBox_Sbor_Others_IsAllocateDopZam;
  private Button button_OpenDumpFolder;
  private Label label_DumpFolder;
  private CheckBox checkBox_Services_isCreateDumpAuto;
  private CheckBox checkBox_Services_autoSbor;
  private Label label_UsloviaSbora_Current;
  private FontDialog fontDialog1;
  private DataGridViewTextBoxColumn PodRazdels_Column1;
  private DataGridViewTextBoxColumn PodRazdels_Column2;
  private DataGridViewTextBoxColumn Razdels_Column1;
  private DataGridViewTextBoxColumn Razdels_Column2;
  private GroupBox groupBox_Protection_From_Editing;
  private CheckBox checkBox_isFullProhibition;
  private CheckBox checkBox_isProhibition_DocRowWithObj;
  private GroupBox groupBox_ProtectionCommand;
  private CheckBox checkBox_isProtectionCommand;
  private CheckBox checkBox_LocationZagolovki;
  private CheckBox checkBox_UserZagolovki;
  private System.Windows.Forms.TabPage tabPage_ESPD;
  private GroupBox groupBox_Check;
  private CheckBox checkBox_isCheck;
  private Button button_Check;
  private GroupBox groupBox_isUnbrokenDefis;
  private CheckBox checkBox_isUnbrokenDefis;
  private GroupBox groupBox_FirstOpen;
  private CheckBox checkBox_isCreateLU;
  private CheckBox checkBox_isAddLU;
  private FontDialog fontDialog2;
  private CheckBox checkBox_isOpenLU;
  private GroupBox groupBox_AddToSP;
  private CheckBox checkBox_isAddToSpLU;
  private GroupBox groupBox_Remark;
  private CheckBox checkBox_isAddRemark;
  private TextBox textBox_textRemark;
  private GroupBox groupBox_Sorting_List_Ved_Graf;
  private ListBox listBox_Sorting_List_Ved_Graf;
  private DataGridView dataGridView_Sorting_Doc;
  private DataGridViewImageColumn dataGridViewImageColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

  public A_NastrVed() => this.InitializeComponent();

  private void A_NastrVed_Load(object sender, EventArgs e)
  {
    if (this._one_Conformity_Template_Nastr_Curr == null)
    {
      if (this._guidTemplateVed_Curr == Guid.Empty)
      {
        int num = (int) MessageBox.Show("Шаблон документа пустой", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        this.Close();
        return;
      }
      if (this._one_Ved_Nastr_Curr == null)
      {
        this.Processing_Template(this._guidTemplateVed_Curr);
        if (this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr != null)
        {
          this._one_Ved_Nastr_Curr = this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr;
        }
        else
        {
          this._one_Ved_Nastr_Curr = new One_Ved_Nastr(true, this.isKudaVhoditInfo, this.isItogoInfo);
          this.is_one_Ved_Nastr_New = true;
          int num = (int) MessageBox.Show("Для данного типа документа настройки отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        this._one_Ved_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateVed_Curr;
      }
      else
        this.Processing_Template(this._guidTemplateVed_Curr);
    }
    else
    {
      this._guidTemplateVed_Curr = this._one_Conformity_Template_Nastr_Curr._guid_Template;
      this._guidTypeVed_Curr = this._one_Conformity_Template_Nastr_Curr._guid_TypeVed;
      this._documentName_Curr = this._one_Conformity_Template_Nastr_Curr._name_Ved;
      this._one_Ved_Nastr_Curr = this._one_Conformity_Template_Nastr_Curr._one_Ved_Nastr;
      this._one_Ved_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateVed_Curr;
      this.Processing_Template(this._guidTemplateVed_Curr);
    }
    Vedomost_VB_Static.ListOneAttribVedRec_Init();
    Vedomost_VB_Static.ListOneAttribVedPasport_Init();
    this._one_Ved_Nastr_Curr._imsObjectType = this._imsObjectType_Curr;
    this._one_Ved_Nastr_Curr._nameVed = this._imsObjectType_Curr.ObjectName;
    this._one_Ved_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Ved_Nastr_Curr);
    if (this._one_Ved_Nastr_Tmp._algorithmToPrint == null || this._one_Ved_Nastr_Tmp._typeCreateNastr == TypeCreateNastr.Empty || this._one_Ved_Nastr_Tmp._algorithmToPrint._list_OneRazdelToPrint == null && this._one_Ved_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info == null)
    {
      if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
      {
        this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_EMPTY_Init();
        this._one_Ved_Nastr_Tmp._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Empty_Init();
      }
      else
      {
        this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Default_Init();
        this._one_Ved_Nastr_Tmp._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Default_Init();
      }
      int num = (int) MessageBox.Show("Для данного типа документа настройки вывода и XML созданы программой\r\n\r\nПроверьте" + "\r\n\r\nИли на странице настройки \"Сервис\" выgолните команду \"Тип ведомости\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this.Text = $"{this.Text} [{this._one_Ved_Nastr_Curr._imsObjectType.ObjectName}]";
    if (!string.IsNullOrEmpty(this._one_Ved_Nastr_Curr._dateIni))
      this.Text = $"{this.Text} {this._one_Ved_Nastr_Curr._dateIni}";
    else
      this.Text += " новая";
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    this.list_CalalogsImbaseFull = Vedomost_VB_Static.FindCatalogs();
    this.list_CalalogsImbaseTmp = Vedomost_VB_Static.FindCatalogs();
    Vedomost_VB_Static.Begin_For_Avs6();
    string machineName = Environment.MachineName;
    AVS6_From_Avs6Main.Inits();
    this.Cursor = Cursors.WaitCursor;
    this.groupBox_Sbor_Usl_AttributeControl1.Location = new Point(6, 6);
    this.Draw_All();
    this.Cursor = Cursors.Default;
    if (!AvsConfig.General.AskAVS6 && !Vedomost_VB_Static.IsAvs6ToIps)
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    if (!List_Element_Accord_Avs6_Ips.isPopytkaInits)
    {
      List_Element_Accord_Avs6_Ips.Read_From_Base();
      List_Element_Accord_Avs6_Ips.Begin();
    }
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    this.isCreate = false;
    if (!(this.formaGroupDoc_start == "B") || this.algorithmToPrint_B == null)
      return;
    this.radioButton_Vyvod_GroupB.Checked = true;
    this.groupB();
  }

  private void A_NastrVed_Shown(object sender, EventArgs e)
  {
    if (Vedomost_VB_Static.AssemblyAttributes.IPSVersion.StartsWith("6"))
      this.checkBox_Services_autoSbor.Visible = false;
    if (this._one_Ved_Nastr_Tmp._algorithmToPrint != null && (this._one_Ved_Nastr_Tmp._algorithmToPrint._list_OneRazdelToPrint != null || this._one_Ved_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info != null))
      return;
    this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Default_Init();
    this._one_Ved_Nastr_Tmp._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Default_Init();
    int num = (int) MessageBox.Show("Для данного типа документа отсутствуют настройки вывода", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  /// <summary> Рисование ВСЕХ страниц </summary>
  private void Draw_All()
  {
    this.dataGridView_Sorting.Rows.Clear();
    this.dataGridView_Sorting_Doc.Rows.Clear();
    this.Razdels_dataGridViewListRazdels.Rows.Clear();
    this.dataGridView_ListZagolovkov.Rows.Clear();
    this.Draw_Page_Service();
    this.Draw_Page_Bases();
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VSI)
    {
      this.tabPage_Zagolovki.Parent = (Control) null;
      this.tabPage_Razdels.Parent = (Control) null;
      this.tabPage_Sbor.Parent = (Control) null;
    }
    else
    {
      if (this.tabPage_Sbor.Parent == null)
        this.tabControl_Nastr.TabPages.Insert(1, this.tabPage_Sbor);
      if (this.tabPage_Zagolovki.Parent == null)
        this.tabControl_Nastr.TabPages.Insert(4, this.tabPage_Zagolovki);
      if (this.tabPage_Razdels.Parent == null)
        this.tabControl_Nastr.TabPages.Insert(5, this.tabPage_Razdels);
    }
    this.Draw_Page_Sbor();
    this.Draw_Page_Razdels();
    this.Draw_Page_Zagolovki();
    this.Draw_Page_Usl_Sorting();
    this.Draw_Page_Merge();
    this.Draw_Page_Vyvod();
    this.docKcontainer_Vyvod.Location = new Point(1296, 1);
    this.docKcontainer_Xml.Location = new Point(1308, 1);
    this.docKcontainer_Vyvod.Width = 265;
    this.docKcontainer_Xml.Width = 265;
    this.dockContainer_Avs.Location = new Point(1308, 1);
    this.dockContainer_Avs.Width = 265;
    this.Draw_Page_Xml();
    bool flag = List_Element_Accord_Avs6_Ips.Find(this._one_Ved_Nastr_Curr._imsObjectType.ObjectName);
    if (!flag)
      flag = List_Element_Accord_Avs6_Ips.Find(this._one_Ved_Nastr_Curr._imsObjectType.ObjectTypeName);
    if (flag && AvsConfig.General.AskAVS6)
    {
      if (this.tabControl_Nastr.TabPages.Count == 9)
        this.tabControl_Nastr.TabPages.Insert(8, this.tabPage_Avs6);
    }
    else
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Avs6);
    this.Draw_Page_Avs();
    this.IsButtonB();
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    this.isByloButtonTypeVedTo_Click = false;
  }

  /// <summary> Основные параметры сбора </summary>
  private void Draw_Page_Bases()
  {
    this.Draw_PodPage_Usl_Bases_Dialog();
    if (this.tabControl_Usl_Bases.TabPages.Count > 1)
    {
      this.Draw_PodPage_Usl_Bases_Main();
      this.Draw_PodPage_Usl_Bases_Sbor();
    }
    else
    {
      this.tabControl_Usl_Bases.SelectedTab = this.tabPage_Usl_Bases_SborDialog;
      this.tabControl_Usl_Bases.SelectedIndex = 2;
      this.Draw_PodPage_Usl_Bases_Dialog();
    }
  }

  /// <summary> Рисование подстраницы "Правила сбора/Предварительный сбор" </summary>
  private void Draw_PodPage_Usl_Bases_Main()
  {
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VSI || this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
    {
      this.tabPage_Bases_Main.Parent = (Control) null;
      this.tabPage_Usl_Bases_Sbor.Parent = (Control) null;
    }
    else if (this.tabControl_Usl_Bases.TabPages.Count < 3)
    {
      this.tabControl_Usl_Bases.TabPages.Insert(0, this.tabPage_Bases_Main);
      this.tabControl_Usl_Bases.TabPages.Insert(1, this.tabPage_Usl_Bases_Sbor);
    }
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved == null)
      this._one_Ved_Nastr_Tmp._bases_Options_Ved = Vedomost_VB_Static.Bases_Options_Ved_Init(this._one_Ved_Nastr_Tmp._typeVed);
    this.checkBox_Usl_Bases_isMainSort1.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSort1;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSummOdinakovyh;
    this.checkBox_Usl_Bases_isMainSort2.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSort2;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainCreateVtorRecords;
    this.checkBox_Usl_Bases_isMainSumm.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSumm;
    this.checkBox_Usl_Bases_isOnlyUroven1.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isOnlyUroven1;
    if (this.checkBox_Usl_Bases_isMainCreateVtorRecords.Checked)
    {
      this.checkBox_Usl_Bases_isMainSumm.Enabled = true;
    }
    else
    {
      this.checkBox_Usl_Bases_isMainSumm.Checked = false;
      this.checkBox_Usl_Bases_isMainSumm.Enabled = false;
    }
    this.Draw_SpecificationSections();
  }

  /// <summary> Рисуем список ракрываемых разделов </summary>
  private void Draw_SpecificationSections()
  {
    this.drawGrid_SpecificationSections.Columns[0].ReadOnly = false;
    this.drawGrid_SpecificationSections.Rows.Clear();
    foreach (object specificationSectionInfo in Vedomost_VB_Static._specificationSectionInfos)
      this.drawGrid_SpecificationSections.Rows.Add((object) false, (object) specificationSectionInfo.ToString());
    this.checkBox_Specification_Instrument.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._is_Specification_Instrument;
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._is_Specification_Instrument)
    {
      string str1 = "";
      for (int index1 = 0; index1 < 3; ++index1)
      {
        bool flag = false;
        switch (index1)
        {
          case 0:
            str1 = "Инструмент";
            break;
          case 1:
            str1 = "Принадлежности";
            break;
          case 2:
            str1 = "Приспособления";
            break;
        }
        for (int index2 = 0; index2 < this.drawGrid_SpecificationSections.Rows.Count; ++index2)
        {
          string str2 = this.drawGrid_SpecificationSections.Rows[index2].Cells[1].Value.ToString();
          if (str1 == str2)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          this.drawGrid_SpecificationSections.Rows.Add((object) false, (object) str1);
      }
    }
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections == null)
      return;
    for (int index3 = 0; index3 < this.drawGrid_SpecificationSections.Rows.Count; ++index3)
    {
      string str = this.drawGrid_SpecificationSections.Rows[index3].Cells[1].Value.ToString();
      for (int index4 = 0; index4 < this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections.Count; ++index4)
      {
        string opеningSection = this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections[index4];
        if (str == opеningSection)
          this.drawGrid_SpecificationSections.Rows[index3].Cells[0].Value = (object) true;
      }
    }
  }

  /// <summary> Птичка на списке раскрываемых разделов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void drawGrid_SpecificationSections_CellMouseClick(
    object sender,
    DataGridViewCellMouseEventArgs e)
  {
    if (e.ColumnIndex != 0)
      return;
    bool flag = (bool) this.drawGrid_SpecificationSections.Rows[e.RowIndex].Cells[0].Value;
    this.drawGrid_SpecificationSections.Rows[e.RowIndex].Cells[0].Value = (object) !flag;
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Использовать разделы комплекта инстументов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void checkBox_Specification_Insrument_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    if (this.checkBox_Specification_Instrument.Checked)
    {
      this.drawGrid_SpecificationSections.Rows.Add((object) false, (object) "Инструмент");
      this.drawGrid_SpecificationSections.Rows.Add((object) false, (object) "Принадлежности");
      this.drawGrid_SpecificationSections.Rows.Add((object) false, (object) "Приспособления");
    }
    else
    {
      this.drawGrid_SpecificationSections.Rows.RemoveAt(this.drawGrid_SpecificationSections.Rows.Count - 1);
      this.drawGrid_SpecificationSections.Rows.RemoveAt(this.drawGrid_SpecificationSections.Rows.Count - 1);
      this.drawGrid_SpecificationSections.Rows.RemoveAt(this.drawGrid_SpecificationSections.Rows.Count - 1);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Рисование подстраницы "Сбор ведомости" </summary>
  private void Draw_PodPage_Usl_Bases_Sbor()
  {
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSortGroup;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedMergerIsp;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddFuncGroup;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSort1;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedUnion;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedExtrectionVtor;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedMergerVtor;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSortVtor;
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSummVtor;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolIspoln;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolSvoiaVed;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolPoPriznaku;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToSp;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_SB_Raskr;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_SB_Add;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_COMPL_Raskr;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_COMPL_Add;
    if (this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Checked)
    {
      this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Enabled = true;
    }
    else
    {
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Enabled = false;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Enabled = false;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Enabled = false;
      this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Enabled = false;
    }
    if (this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Checked)
    {
      this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Visible = true;
      if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToRazdel == 0)
      {
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = true;
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = false;
      }
      else
      {
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = false;
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = true;
      }
    }
    else
    {
      this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = true;
      this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = false;
      this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Visible = false;
    }
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ZI)
      this.groupBox_Usl_Bases_Sbor_For_ZIP.Visible = true;
    else
      this.groupBox_Usl_Bases_Sbor_For_ZIP.Visible = false;
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VSI)
    {
      this.groupBox_Usl_Bases_Sbor_VedStep.Enabled = false;
      this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Enabled = false;
    }
    else
    {
      this.groupBox_Usl_Bases_Sbor_VedStep.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Enabled = true;
    }
  }

  /// <summary> Удаление из list_CalalogsImbaseTmp и listBox_CatalogsImbase </summary>
  /// <param textFromColumn="quickObjectInfo_Del"></param>
  private void Delete_From_list_CalalogsImbaseTmp(QuickObjectInfo quickObjectInfo_Del)
  {
    if (quickObjectInfo_Del.Empty)
      return;
    for (int index = 0; index < this.list_CalalogsImbaseTmp.Count; ++index)
    {
      if (this.list_CalalogsImbaseTmp[index].Caption == quickObjectInfo_Del.Caption)
      {
        this.list_CalalogsImbaseTmp.RemoveAt(index);
        break;
      }
    }
    this.Draw_listBox_CatalogsImbase();
  }

  /// <summary> Рисование listBox_CatalogsImbase в соответствии с list_CalalogsImbaseTmp </summary>
  private void Draw_listBox_CatalogsImbase()
  {
    this.listBox_CatalogsImbase.Items.Clear();
    for (int index = 0; index < this.list_CalalogsImbaseTmp.Count; ++index)
      this.listBox_CatalogsImbase.Items.Add((object) this.list_CalalogsImbaseTmp[index].Caption);
    if (this.listBox_CatalogsImbase.Items.Count > 0)
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = true;
    else
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved == null || this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo == null)
      this._one_Ved_Nastr_Tmp._bases_Options_Ved = Vedomost_VB_Static.Bases_Options_Ved_Init(this._one_Ved_Nastr_Tmp._typeVed);
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Count > 0)
      this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = true;
    else
      this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = false;
  }

  /// <summary> Рисование подстраницы "Ввод данных в диалоге" </summary>
  private void Draw_PodPage_Usl_Bases_Dialog()
  {
    this.listBox_QuickObjectInfo.Items.Clear();
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved == null)
      return;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputDoc;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputIzd;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Checked = this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputMat;
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo != null)
    {
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Count; ++index)
      {
        QuickObjectInfo quickObjectInfo_Del = this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo[index];
        if (!string.IsNullOrEmpty(quickObjectInfo_Del.Caption))
          this.listBox_QuickObjectInfo.Items.Add((object) quickObjectInfo_Del.Caption);
        this.Delete_From_list_CalalogsImbaseTmp(quickObjectInfo_Del);
        if (this.listBox_CatalogsImbase.Items.Count > 0)
          this.button_Add_To_listBox_QuickObjectInfo.Enabled = true;
        else
          this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
        this.button_Delete_From_To_listBox_QuickObjectInfo.Enabled = true;
      }
    }
    else
      this.button_Add_To_listBox_QuickObjectInfo.Enabled = false;
    this.Draw_listBox_CatalogsImbase();
  }

  /// <summary> Сохранение редактирования страницы "Основные" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Bases()
  {
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSort1 = this.checkBox_Usl_Bases_isMainSort1.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSummOdinakovyh = this.checkBox_Usl_Bases_isMainSummOdinakovyh.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSort2 = this.checkBox_Usl_Bases_isMainSort2.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainCreateVtorRecords = this.checkBox_Usl_Bases_isMainCreateVtorRecords.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isMainSumm = this.checkBox_Usl_Bases_isMainSumm.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isOnlyUroven1 = this.checkBox_Usl_Bases_isOnlyUroven1.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._is_Specification_Instrument = this.checkBox_Specification_Instrument.Checked;
    if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections == null)
      this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections = new List<string>();
    else
      this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections.Clear();
    for (int index = 0; index < this.drawGrid_SpecificationSections.Rows.Count; ++index)
    {
      if ((bool) this.drawGrid_SpecificationSections.Rows[index].Cells[0].Value)
        this._one_Ved_Nastr_Tmp._bases_Options_Ved._opеning_Sections.Add(this.drawGrid_SpecificationSections.Rows[index].Cells[1].Value.ToString());
    }
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSortGroup = this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedMergerIsp = this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddFuncGroup = this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSort1 = this.checkBox_Usl_Bases_Sbor_isVedSort1.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedUnion = this.checkBox_Usl_Bases_Sbor_isVedUnion.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedExtrectionVtor = this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedMergerVtor = this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSortVtor = this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedSummVtor = this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolIspoln = this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolSvoiaVed = this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedCreateZagolPoPriznaku = this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToSp = this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_SB_Raskr = this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_SB_Add = this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_COMPL_Raskr = this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Checked;
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isFor_ZIP_COMPL_Add = this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Checked;
    if (this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked)
      this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToRazdel = 0;
    else
      this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToRazdel = 1;
  }

  private void checkBox_Usl_Bases_isMainSort1_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_isMainSummOdinakovyh_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_isMainSort2_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_isMainCreateVtorRecords_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Usl_Bases_isMainCreateVtorRecords.Checked)
    {
      this.checkBox_Usl_Bases_isMainSumm.Enabled = true;
    }
    else
    {
      this.checkBox_Usl_Bases_isMainSumm.Checked = false;
      this.checkBox_Usl_Bases_isMainSumm.Enabled = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_isMainSumm_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_isOnlyUroven1_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedSortGroup_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedMergerIsp_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedAddFuncGroup_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedSort1_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedUnion_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedExtrectionVtor_CheckedChanged(
    object sender,
    EventArgs e)
  {
    if (this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Checked)
    {
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Enabled = true;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Enabled = true;
    }
    else
    {
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Enabled = false;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Enabled = false;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Checked = false;
      this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Enabled = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedMergerVtor_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedSortVtor_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedSummVtor_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isVedAddToSp_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Checked)
    {
      this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Visible = true;
      if (this._one_Ved_Nastr_Tmp._bases_Options_Ved._isVedAddToRazdel == 0)
      {
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = true;
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = false;
      }
      else
      {
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = false;
        this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = true;
      }
    }
    else
    {
      this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = true;
      this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Checked = false;
      this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Visible = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isInputDoc_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputDoc = this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked;
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isInputIzd_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputIzd = this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked;
    this.IsModified_Page_Bases = true;
  }

  private void checkBox_Usl_Bases_Sbor_isInputMat_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._isInputMat = this.checkBox_Usl_Bases_Sbor_isInputMat.Checked;
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Правила сбора </summary>
  private void Draw_Page_Sbor()
  {
    this.Draw_PodPage_Sbor_Peredatha();
    if (this.tabControl_Page_Sbor.TabPages.Count <= 1)
      return;
    this.Draw_PodPage_Sbor_Usl();
    this.Draw_PodPage_Sbor_Others();
    this.Draw_PodPage_Sbor_Usl_Reference();
    this.Draw_PodPage_Espd();
  }

  /// <summary> Рисование подстраницы "Условия ввода данных" </summary>
  private void Draw_PodPage_Sbor_Usl()
  {
    INamedImageList namedImageList = (INamedImageList) null;
    if (AVSPlugin.ServiceProvider != null)
      namedImageList = (INamedImageList) AVSPlugin.ServiceProvider.GetService(typeof (INamedImageList));
    this.imageList1 = namedImageList.ImageList;
    this.treeView_UsloviaSbora.ImageList = this.imageList1;
    this.treeView_UsloviaSbora_Reference.ImageList = this.imageList1;
    this._indexImageList_Section = namedImageList.ImageIndex("imgFolder");
    this._indexImageList_Empty = namedImageList.ImageIndex("imgEmpty");
    this._indexImageList_InvalidRule = namedImageList.ImageIndex("imgInvalidRule");
    this._indexImageList_GreenBall = namedImageList.ImageIndex("imgGreenBall");
    this._indexImageList_RuleCriterion = namedImageList.ImageIndex("imgRuleCriterion");
    this.treeView_UsloviaSbora_Draw();
    this.treeView_UsloviaSbora.SelectedNode = this.treeView_UsloviaSbora.Nodes[0];
    this.treeView_UsloviaSbora.Select();
    this.usloviaNode_Current = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
    if (this.usloviaNode_Current == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      this._usl_Read_From_SP_CurrentRazdel = this.Usl_Read_From_SP(this.usloviaNode_Current._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP);
      this.usl_Read_From_SP_One_Current = this.usloviaNode_Current._i_usl <= -1 ? (Vedomost_VB.Usl_Read_From_SP_One) null : this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current._i_usl];
      if (this.Usl_Read_From_SP(0, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP) == null)
        this.treeView_UsloviaSbora.SelectedNode.Collapse();
      this.select_Sbor_Usl_AttributeControl1.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    }
  }

  /// <summary> Рисование подстраницы "Условия ввода данных связей" </summary>
  private void Draw_PodPage_Sbor_Usl_Reference()
  {
    INamedImageList namedImageList = (INamedImageList) null;
    if (AVSPlugin.ServiceProvider != null)
      namedImageList = (INamedImageList) AVSPlugin.ServiceProvider.GetService(typeof (INamedImageList));
    this.imageList1 = namedImageList.ImageList;
    this.treeView_UsloviaSbora_Reference.ImageList = this.imageList1;
    this.treeView_UsloviaSbora_Reference.ImageList = this.imageList1;
    this._indexImageList_Section = namedImageList.ImageIndex("imgFolder");
    this._indexImageList_Empty = namedImageList.ImageIndex("imgEmpty");
    this._indexImageList_InvalidRule = namedImageList.ImageIndex("imgInvalidRule");
    this._indexImageList_GreenBall = namedImageList.ImageIndex("imgGreenBall");
    this._indexImageList_RuleCriterion = namedImageList.ImageIndex("imgRuleCriterion");
    this.treeView_UsloviaSbora_Reference_Draw();
    this.treeView_UsloviaSbora_Reference.SelectedNode = this.treeView_UsloviaSbora_Reference.Nodes[0];
    this.treeView_UsloviaSbora_Reference.Select();
    this.usloviaNode_Current_Reference = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
    if (this.usloviaNode_Current_Reference == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      this._usl_Read_From_SP_Reference_CurrentRazdel = this.Usl_Read_From_SP(this.usloviaNode_Current_Reference._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference);
      this.usl_Read_From_SP_Reference_One_Current = this.usloviaNode_Current_Reference._i_usl <= -1 ? (Vedomost_VB.Usl_Read_From_SP_One) null : this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current_Reference._i_usl];
      if (this.Usl_Read_From_SP(0, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference) == null)
        this.treeView_UsloviaSbora_Reference.SelectedNode.Collapse();
      this.select_Sbor_Usl_AttributeControl_Reference.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    }
  }

  /// <summary> Рисование дерева условий </summary>
  private void treeView_UsloviaSbora_Draw()
  {
    this.treeView_UsloviaSbora.Nodes.Clear();
    for (int index1 = 0; index1 < Vedomost_VB_Static._list_SpecificationSectionInfo.Count; ++index1)
    {
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[index1];
      A_NastrVed.UsloviaNode usloviaNode1 = new A_NastrVed.UsloviaNode();
      usloviaNode1.Text = "Раздел: " + specificationSectionInfo.Caption;
      usloviaNode1.ImageIndex = this._indexImageList_Section;
      usloviaNode1.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.UsloviaNode node1 = usloviaNode1;
      node1._uroven = 0;
      node1._i_Razd = index1;
      node1._specificationSectionInfo = specificationSectionInfo;
      this.treeView_UsloviaSbora.Nodes.Add((TreeNode) node1);
      Vedomost_VB.Usl_Read_From_SP uslReadFromSp = this.Usl_Read_From_SP(index1, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP);
      if (uslReadFromSp == null)
      {
        A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
        usloviaNode2.Text = "Ввод не производится";
        usloviaNode2.ImageIndex = this._indexImageList_InvalidRule;
        usloviaNode2.SelectedImageIndex = this._indexImageList_InvalidRule;
        A_NastrVed.UsloviaNode node2 = usloviaNode2;
        node2._usloviaNodeRazdel = node1;
        node2._i_Razd = index1;
        node2._uroven = 1;
        node2._usloviaNodeParent = node1;
        this.treeView_UsloviaSbora.Nodes[index1].Nodes.Add((TreeNode) node2);
        this.treeView_UsloviaSbora.Nodes[index1].Collapse();
      }
      else if (uslReadFromSp._list_Usl_Read_From_SP_One.Count == 0)
      {
        A_NastrVed.UsloviaNode usloviaNode3 = new A_NastrVed.UsloviaNode();
        usloviaNode3.Text = "Ввод производится без условий";
        usloviaNode3.ImageIndex = this._indexImageList_GreenBall;
        usloviaNode3.SelectedImageIndex = this._indexImageList_GreenBall;
        A_NastrVed.UsloviaNode node3 = usloviaNode3;
        node3._usloviaNodeRazdel = node1;
        node3._i_Razd = index1;
        node3._uroven = 1;
        node3._usloviaNodeParent = node1;
        this.treeView_UsloviaSbora.Nodes[index1].Nodes.Add((TreeNode) node3);
        this.treeView_UsloviaSbora.Nodes[index1].Expand();
      }
      else
      {
        string prevision_Or_And = "";
        A_NastrVed.UsloviaNode usloviaNode4 = node1;
        for (int index2 = 0; index2 < uslReadFromSp._list_Usl_Read_From_SP_One.Count; ++index2)
        {
          Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One = uslReadFromSp._list_Usl_Read_From_SP_One[index2];
          string str = this.strokaTreeViewsUsl(usl_Read_From_SP_One, prevision_Or_And);
          A_NastrVed.UsloviaNode usloviaNode5 = new A_NastrVed.UsloviaNode();
          usloviaNode5.Text = str;
          usloviaNode5.ImageIndex = this._indexImageList_RuleCriterion;
          usloviaNode5.SelectedImageIndex = this._indexImageList_RuleCriterion;
          A_NastrVed.UsloviaNode node4 = usloviaNode5;
          node4._usloviaNodeRazdel = node1;
          node4._i_Razd = index1;
          node4._i_usl = index2;
          if (prevision_Or_And == "" || prevision_Or_And == "или")
          {
            node4._uroven = 1;
            node4._usloviaNodeParent = node1;
            node1.Nodes.Add((TreeNode) node4);
            usloviaNode4 = node4;
          }
          else
          {
            A_NastrVed.UsloviaNode usloviaNode6 = usloviaNode4;
            node4._uroven = usloviaNode4._uroven + 1;
            node4._usloviaNodeParent = usloviaNode6;
            usloviaNode4.Nodes.Add((TreeNode) node4);
            usloviaNode4 = node4;
          }
          prevision_Or_And = !usl_Read_From_SP_One._or_and ? "или" : "и";
        }
        this.treeView_UsloviaSbora.Nodes[index1].ExpandAll();
        this._usl_Read_From_SP_CurrentRazdel = this.Usl_Read_From_SP(0, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP);
      }
    }
  }

  /// <summary> Рисование дерева условий </summary>
  private void treeView_UsloviaSbora_Reference_Draw()
  {
    this.treeView_UsloviaSbora_Reference.Nodes.Clear();
    for (int index1 = 0; index1 < Vedomost_VB_Static._list_SpecificationSectionInfo.Count; ++index1)
    {
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[index1];
      A_NastrVed.UsloviaNode usloviaNode1 = new A_NastrVed.UsloviaNode();
      usloviaNode1.Text = "Раздел: " + specificationSectionInfo.Caption;
      usloviaNode1.ImageIndex = this._indexImageList_Section;
      usloviaNode1.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.UsloviaNode node1 = usloviaNode1;
      node1._uroven = 0;
      node1._i_Razd = index1;
      node1._specificationSectionInfo = specificationSectionInfo;
      this.treeView_UsloviaSbora_Reference.Nodes.Add((TreeNode) node1);
      Vedomost_VB.Usl_Read_From_SP uslReadFromSp = this.Usl_Read_From_SP(index1, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference);
      if (uslReadFromSp == null)
      {
        A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
        usloviaNode2.Text = "Ввод не производится";
        usloviaNode2.ImageIndex = this._indexImageList_InvalidRule;
        usloviaNode2.SelectedImageIndex = this._indexImageList_InvalidRule;
        A_NastrVed.UsloviaNode node2 = usloviaNode2;
        node2._usloviaNodeRazdel = node1;
        node2._i_Razd = index1;
        node2._uroven = 1;
        node2._usloviaNodeParent = node1;
        this.treeView_UsloviaSbora_Reference.Nodes[index1].Nodes.Add((TreeNode) node2);
        this.treeView_UsloviaSbora_Reference.Nodes[index1].Collapse();
      }
      else if (uslReadFromSp._list_Usl_Read_From_SP_One.Count == 0)
      {
        A_NastrVed.UsloviaNode usloviaNode3 = new A_NastrVed.UsloviaNode();
        usloviaNode3.Text = "Ввод производится без условий";
        usloviaNode3.ImageIndex = this._indexImageList_GreenBall;
        usloviaNode3.SelectedImageIndex = this._indexImageList_GreenBall;
        A_NastrVed.UsloviaNode node3 = usloviaNode3;
        node3._usloviaNodeRazdel = node1;
        node3._i_Razd = index1;
        node3._uroven = 1;
        node3._usloviaNodeParent = node1;
        this.treeView_UsloviaSbora_Reference.Nodes[index1].Nodes.Add((TreeNode) node3);
        this.treeView_UsloviaSbora_Reference.Nodes[index1].Expand();
      }
      else
      {
        string prevision_Or_And = "";
        A_NastrVed.UsloviaNode usloviaNode4 = node1;
        for (int index2 = 0; index2 < uslReadFromSp._list_Usl_Read_From_SP_One.Count; ++index2)
        {
          Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One = uslReadFromSp._list_Usl_Read_From_SP_One[index2];
          string str = this.strokaTreeViewsUsl(usl_Read_From_SP_One, prevision_Or_And);
          A_NastrVed.UsloviaNode usloviaNode5 = new A_NastrVed.UsloviaNode();
          usloviaNode5.Text = str;
          usloviaNode5.ImageIndex = this._indexImageList_RuleCriterion;
          usloviaNode5.SelectedImageIndex = this._indexImageList_RuleCriterion;
          A_NastrVed.UsloviaNode node4 = usloviaNode5;
          node4._usloviaNodeRazdel = node1;
          node4._i_Razd = index1;
          node4._i_usl = index2;
          if (prevision_Or_And == "" || prevision_Or_And == "или")
          {
            node4._uroven = 1;
            node4._usloviaNodeParent = node1;
            node1.Nodes.Add((TreeNode) node4);
            usloviaNode4 = node4;
          }
          else
          {
            A_NastrVed.UsloviaNode usloviaNode6 = usloviaNode4;
            node4._uroven = usloviaNode4._uroven + 1;
            node4._usloviaNodeParent = usloviaNode6;
            usloviaNode4.Nodes.Add((TreeNode) node4);
            usloviaNode4 = node4;
          }
          prevision_Or_And = !usl_Read_From_SP_One._or_and ? "или" : "и";
        }
        this.treeView_UsloviaSbora_Reference.Nodes[index1].ExpandAll();
        this._usl_Read_From_SP_Reference_CurrentRazdel = this.Usl_Read_From_SP(0, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference);
      }
    }
  }

  /// <summary> Получение условия по номеру раздела </summary>
  /// <param textFromColumn="iRazd"></param>
  /// <returns></returns>
  private Vedomost_VB.Usl_Read_From_SP Usl_Read_From_SP(
    int iRazd,
    List<Vedomost_VB.Usl_Read_From_SP> list_Usl_Read_From_SP)
  {
    if (iRazd < 0)
      return (Vedomost_VB.Usl_Read_From_SP) null;
    SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
    if (list_Usl_Read_From_SP != null)
    {
      for (int index = 0; index < list_Usl_Read_From_SP.Count; ++index)
      {
        Vedomost_VB.Usl_Read_From_SP uslReadFromSp = list_Usl_Read_From_SP[index];
        if (uslReadFromSp._section_SP == specificationSectionInfo.Caption)
          return uslReadFromSp;
      }
    }
    return (Vedomost_VB.Usl_Read_From_SP) null;
  }

  /// <summary> Создание текста для одной ветви условий </summary>
  /// <param textFromColumn="usl_Read_From_SP_One"></param>
  /// <param textFromColumn="prevision_Or_And"></param>
  /// <returns></returns>
  private string strokaTreeViewsUsl(
    Vedomost_VB.Usl_Read_From_SP_One usl_Read_From_SP_One,
    string prevision_Or_And)
  {
    string str1 = prevision_Or_And;
    if (str1 != "")
      str1 += " ";
    string str2 = $"{str1}Если информация по атрибуту \"{usl_Read_From_SP_One._oneFieldSpForRead._name}\" ";
    if (usl_Read_From_SP_One._uslovie == "=")
      str2 += "равно";
    if (usl_Read_From_SP_One._uslovie == "!=")
      str2 += "не равно";
    if (usl_Read_From_SP_One._uslovie == "?")
      str2 += "содержит";
    if (usl_Read_From_SP_One._uslovie == "!?")
      str2 += "не содержит";
    if (usl_Read_From_SP_One._uslovie == "&")
      str2 += "начинается с";
    return $"{str2 + " "}\"{usl_Read_From_SP_One._text}\"";
  }

  /// <summary> Свернуть ВСЕ ветки дерева условий </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButtonCollapseAll_CheckedChanged(object sender, EventArgs e)
  {
    this.button_Sbor_Usl_Add1.Enabled = false;
    this.treeView_UsloviaSbora.CollapseAll();
    this.button_Sbor_Usl_Edit1.Enabled = false;
    this.button_Sbor_Usl_Delete1.Enabled = false;
    this.button_Sbor_Usl_BezUsl.Enabled = false;
    this.button_Sbor_Usl_NeVvodit.Enabled = false;
  }

  /// <summary> Развернуть ВСЕ ветки дерева условий </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButtonExpanded_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView_UsloviaSbora.ExpandAll();
  }

  /// <summary> Свернуть пустые ветки дерева условий </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButtonCollapsedEmpty_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView_UsloviaSbora_Draw();
  }

  private void radioButtonCollapseAll_Reference_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView_UsloviaSbora_Reference.CollapseAll();
  }

  private void radioButtonExpanded_Reference_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView_UsloviaSbora_Reference.ExpandAll();
  }

  private void radioButtonCollapsedEmpty_Reference_CheckedChanged(object sender, EventArgs e)
  {
    this.treeView_UsloviaSbora_Reference_Draw();
  }

  /// Условия сбора. Кнопка "Добавить"
  private void button_Sbor_Usl_Add1_Click(object sender, EventArgs e)
  {
    if (this.select_Sbor_Usl_AttributeControl1.SelectedAttributeId == -1)
    {
      int num1 = (int) MessageBox.Show("Не выбран атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Usl_AttributeControl1.SelectedAttribute);
      if (specRowAttributeInfo == null)
        return;
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne1 = new Vedomost_VB.Usl_Read_From_SP_One();
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
      if (oneFieldSpForRead == null)
      {
        int num2 = (int) MessageBox.Show("Не смогли обработать выбранный атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne2 = new Vedomost_VB.Usl_Read_From_SP_One();
        uslReadFromSpOne2._oneFieldSpForRead = oneFieldSpForRead;
        uslReadFromSpOne2._text = this.textBox_Sbor_Usl_TextDliaSravnenia.Text;
        if (this.radioButton_Sbor_Usl_Ravno.Checked)
          uslReadFromSpOne2._uslovie = "=";
        if (this.radioButton_Sbor_Usl_NeRavno.Checked)
          uslReadFromSpOne2._uslovie = "!=";
        if (this.radioButton_Sbor_Usl_Soderzit.Checked)
          uslReadFromSpOne2._uslovie = "?";
        if (this.radioButton_Sbor_Usl_NeSoderzit.Checked)
          uslReadFromSpOne2._uslovie = "!?";
        if (this.radioButton_Sbor_Usl_Nathinaetsia.Checked)
          uslReadFromSpOne2._uslovie = "&";
        if (this._usl_Read_From_SP_CurrentRazdel == null)
        {
          int index = this.Nomer_Usl_Read_From_SP(this.usloviaNode_Current._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP);
          if (index < 0)
            return;
          this._usl_Read_From_SP_CurrentRazdel = new Vedomost_VB.Usl_Read_From_SP();
          this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>();
          this._usl_Read_From_SP_CurrentRazdel._section_SP = Vedomost_VB_Static.FindRazdelCaptionByRazdelNum(this.usloviaNode_Current._i_Razd);
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Insert(index, this._usl_Read_From_SP_CurrentRazdel);
        }
        if (this.usl_Read_From_SP_One_Current != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count > this.usloviaNode_Current._i_usl)
        {
          Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne3 = this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current._i_usl];
          uslReadFromSpOne2._or_and = this.usl_Read_From_SP_One_Current._or_and;
          if (this.radioButton_Sbor_Usl_I.Checked)
            this.usl_Read_From_SP_One_Current._or_and = true;
          if (this.radioButton_Sbor_Usl_Ili.Checked)
            this.usl_Read_From_SP_One_Current._or_and = false;
          this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Insert(this.usloviaNode_Current._i_usl + 1, uslReadFromSpOne2);
        }
        else
        {
          uslReadFromSpOne2._or_and = true;
          this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Insert(0, uslReadFromSpOne2);
        }
        this.treeView_UsloviaSbora_Draw();
        this.draw_Label_UsloviaSbora_Current();
        this.ModifiedAll(true);
        this.IsModified_Page_Sbor = true;
        if (this._one_Ved_Nastr_Tmp._typeVed != Vedomost_VB.TypeVed.VP || this.usloviaNode_Current._specificationSectionInfo == null || !(this.usloviaNode_Current._specificationSectionInfo.Caption == "Комплекты"))
          return;
        int num3 = (int) MessageBox.Show("Заносить изделия в раздел Комплекты не рекомендуется. (См. Руководство п.4.4.3.3)", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }
  }

  /// <summary> Создание OneFieldSpForRead на основе выбранного атрибута </summary>
  /// <param textFromColumn="specRowAttributeInfo"></param>
  /// <returns></returns>
  private Vedomost_VB.OneFieldSpForRead create_OneFieldSpForRead(
    AvsRowAttributeInfo specRowAttributeInfo,
    bool quiet)
  {
    int attributeId = specRowAttributeInfo.AttributeId;
    AttributeSourceTypes attr = AttributeSourceTypes.Auto;
    Vedomost_VB.TypeDataSel attrType = Vedomost_VB.TypeDataSel.Undefined;
    if (specRowAttributeInfo.AttrSrc == FieldSource.Object)
      attr = AttributeSourceTypes.Object;
    if (specRowAttributeInfo.AttrSrc == FieldSource.Relation)
      attr = AttributeSourceTypes.Relation;
    if (attr == AttributeSourceTypes.Auto)
    {
      if (!quiet)
      {
        int num = (int) MessageBox.Show("Не определено\r\nЭто атрибут объекта или связи", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return (Vedomost_VB.OneFieldSpForRead) null;
    }
    switch (specRowAttributeInfo.FieldType)
    {
      case FieldTypes.ftString:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftInteger:
        attrType = Vedomost_VB.TypeDataSel.Int;
        break;
      case FieldTypes.ftDouble:
        attrType = Vedomost_VB.TypeDataSel.Float;
        break;
      case FieldTypes.ftObjectLink:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftMeasured:
        attrType = Vedomost_VB.TypeDataSel.String;
        break;
      case FieldTypes.ftGuid:
        attrType = Vedomost_VB.TypeDataSel.Guid;
        break;
    }
    if (attrType != Vedomost_VB.TypeDataSel.Undefined)
      return new Vedomost_VB.OneFieldSpForRead(attributeId, attr, attrType);
    if (!quiet)
    {
      int num1 = (int) MessageBox.Show("Условие с таким типом атрибута не может быть создано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    return (Vedomost_VB.OneFieldSpForRead) null;
  }

  /// <summary> Сбор. Номер условия для раздела </summary>
  /// <param textFromColumn="iRazd"></param>
  /// <returns></returns>
  private int Nomer_Usl_Read_From_SP(
    int iRazd,
    List<Vedomost_VB.Usl_Read_From_SP> list_Usl_Read_From_SP)
  {
    if (iRazd < 0)
      return -1;
    if (iRazd == 0)
      return 0;
    int num = 0;
    SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
    for (int index = 0; index < list_Usl_Read_From_SP.Count; ++index)
    {
      if (list_Usl_Read_From_SP[index] != null)
        ++num;
      if (index == iRazd)
        break;
    }
    return num;
  }

  /// <summary> Условия сбора. Кнопка "Изменить" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Sbor_Usl_Edit1_Click(object sender, EventArgs e)
  {
    string prevision_Or_And = "";
    this.usl_Read_From_SP_One_Current._text = this.textBox_Sbor_Usl_TextDliaSravnenia.Text;
    if (this.radioButton_Sbor_Usl_Ravno.Checked)
      this.usl_Read_From_SP_One_Current._uslovie = "=";
    if (this.radioButton_Sbor_Usl_NeRavno.Checked)
      this.usl_Read_From_SP_One_Current._uslovie = "!=";
    if (this.radioButton_Sbor_Usl_Soderzit.Checked)
      this.usl_Read_From_SP_One_Current._uslovie = "?";
    if (this.radioButton_Sbor_Usl_NeSoderzit.Checked)
      this.usl_Read_From_SP_One_Current._uslovie = "!?";
    if (this.radioButton_Sbor_Usl_Nathinaetsia.Checked)
      this.usl_Read_From_SP_One_Current._uslovie = "&";
    if (this.usloviaNode_Current._i_usl != 0)
    {
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current._i_usl - 1];
      if (this.radioButton_Sbor_Usl_I.Checked)
      {
        uslReadFromSpOne._or_and = true;
        prevision_Or_And = "и";
      }
      if (this.radioButton_Sbor_Usl_Ili.Checked)
      {
        uslReadFromSpOne._or_and = false;
        prevision_Or_And = "или";
      }
    }
    if (this.select_Sbor_Usl_AttributeControl1.SelectedAttributeId != -1)
    {
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Usl_AttributeControl1.SelectedAttribute);
      if (specRowAttributeInfo != null)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this.usl_Read_From_SP_One_Current._oneFieldSpForRead;
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead2 = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
        if (oneFieldSpForRead2 != null)
        {
          oneFieldSpForRead1._attributeSourceTypes = oneFieldSpForRead2._attributeSourceTypes;
          oneFieldSpForRead1._guid = oneFieldSpForRead2._guid;
          oneFieldSpForRead1._id = oneFieldSpForRead2._id;
          oneFieldSpForRead1._name = oneFieldSpForRead2._name;
          oneFieldSpForRead1._perv_Vtor = oneFieldSpForRead2._perv_Vtor;
          oneFieldSpForRead1._type = oneFieldSpForRead2._type;
        }
      }
    }
    this.usloviaNode_Current.Text = this.strokaTreeViewsUsl(this.usl_Read_From_SP_One_Current, prevision_Or_And);
    this.treeView_UsloviaSbora.SelectedNode = (TreeNode) this.usloviaNode_Current;
    this.treeView_UsloviaSbora.Select();
    this.draw_Label_UsloviaSbora_Current();
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  /// <summary> Условия сбора. Кнопка "Удалить" </summary>
  /// 
  ///             Удалить условия текущего узла. Получается "ВВод без условий"
  ///             <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Sbor_Usl_Delete1_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora.SelectedNode == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode1 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
      if (selectedNode1 == null)
      {
        int num2 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        A_NastrVed.UsloviaNode node1 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.Nodes[selectedNode1._i_Razd];
        A_NastrVed.UsloviaNode usloviaNodeParent = selectedNode1._usloviaNodeParent;
        A_NastrVed.UsloviaNode usloviaNode1 = selectedNode1._i_usl <= 0 ? usloviaNodeParent : (A_NastrVed.UsloviaNode) selectedNode1.PrevNode;
        if (selectedNode1.Nodes.Count > 0)
        {
          A_NastrVed.UsloviaNode node2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode.Nodes[0];
          selectedNode1.Nodes[0].Remove();
          int index = usloviaNodeParent.Nodes.IndexOf((TreeNode) selectedNode1);
          int iUsl = selectedNode1._i_usl;
          this.treeView_UsloviaSbora.SelectedNode.Remove();
          usloviaNodeParent.Nodes.Insert(index, (TreeNode) node2);
          if (this._usl_Read_From_SP_CurrentRazdel != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One != null && iUsl < this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count)
            this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.RemoveAt(iUsl);
        }
        else
        {
          int iUsl = selectedNode1._i_usl;
          if (iUsl > 0)
            this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[iUsl - 1]._or_and = this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[iUsl]._or_and;
          this.treeView_UsloviaSbora.SelectedNode = (TreeNode) usloviaNode1;
          this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.RemoveAt(iUsl);
          selectedNode1.Remove();
        }
        this.treeView_UsloviaSbora.SelectedNode = (TreeNode) usloviaNode1;
        this.treeView_UsloviaSbora.Select();
        A_NastrVed.UsloviaNode usloviaNode2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
        int iRazd = usloviaNode2._i_Razd;
        while (usloviaNode2.NextVisibleNode != null)
        {
          usloviaNode2 = (A_NastrVed.UsloviaNode) usloviaNode2.NextVisibleNode;
          if (usloviaNode2._i_Razd == iRazd)
          {
            if (usloviaNode2._i_usl > 0)
              --usloviaNode2._i_usl;
          }
          else
            break;
        }
        A_NastrVed.UsloviaNode usloviaNode3 = node1;
        while (usloviaNode3.NextVisibleNode != null)
        {
          usloviaNode3 = (A_NastrVed.UsloviaNode) usloviaNode3.NextVisibleNode;
          if (usloviaNode3._i_Razd != iRazd)
            break;
        }
        if (this.treeView_UsloviaSbora.SelectedNode.Nodes.Count == 0)
        {
          A_NastrVed.UsloviaNode selectedNode2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
          if (selectedNode2._uroven == 0)
          {
            A_NastrVed.UsloviaNode usloviaNode4 = new A_NastrVed.UsloviaNode();
            usloviaNode4.Text = "Ввод производится без условий";
            usloviaNode4.ImageIndex = this._indexImageList_GreenBall;
            usloviaNode4.SelectedImageIndex = this._indexImageList_GreenBall;
            A_NastrVed.UsloviaNode node3 = usloviaNode4;
            node3._i_Razd = selectedNode2._i_Razd;
            node3._uroven = 1;
            node3._usloviaNodeParent = selectedNode2;
            selectedNode2.Nodes.Add((TreeNode) node3);
            selectedNode2.Nodes[0].Expand();
          }
        }
        this.draw_Label_UsloviaSbora_Current();
        this.ModifiedAll(true);
        this.IsModified_Page_Sbor = true;
      }
    }
  }

  /// <summary> Условия сбора. Кнопка "Без условий" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Sbor_Usl_BezUsl_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora.SelectedNode == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
      A_NastrVed.UsloviaNode usloviaNode1 = selectedNode._uroven != 0 ? selectedNode._usloviaNodeRazdel : selectedNode;
      int iRazd = usloviaNode1._i_Razd;
      A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
      usloviaNode2.Text = "Ввод производится без условий";
      usloviaNode2.ImageIndex = this._indexImageList_GreenBall;
      usloviaNode2.SelectedImageIndex = this._indexImageList_GreenBall;
      A_NastrVed.UsloviaNode node = usloviaNode2;
      node._usloviaNodeRazdel = usloviaNode1;
      node._i_Razd = iRazd;
      node._uroven = 1;
      node._usloviaNodeParent = usloviaNode1;
      this.treeView_UsloviaSbora.Nodes[iRazd].Nodes.Clear();
      this.treeView_UsloviaSbora.Nodes[iRazd].Nodes.Add((TreeNode) node);
      this.treeView_UsloviaSbora.Nodes[iRazd].Expand();
      this.treeView_UsloviaSbora.SelectedNode = (TreeNode) usloviaNode1;
      this.treeView_UsloviaSbora.Select();
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
      bool flag = false;
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
      {
        Vedomost_VB.Usl_Read_From_SP uslReadFromSp = this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index];
        if (uslReadFromSp._section_SP == specificationSectionInfo.Caption)
        {
          uslReadFromSp._list_Usl_Read_From_SP_One.Clear();
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
        {
          int num2 = this.Specification_by_Caption(this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index]._section_SP);
          if (iRazd < num2)
          {
            this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Insert(index, new Vedomost_VB.Usl_Read_From_SP()
            {
              _section_SP = specificationSectionInfo.Caption,
              _list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>()
            });
            break;
          }
          if (index == this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count - 1)
          {
            this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Add(new Vedomost_VB.Usl_Read_From_SP()
            {
              _section_SP = specificationSectionInfo.Caption,
              _list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>()
            });
            break;
          }
        }
      }
      this.draw_Label_UsloviaSbora_Current();
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
      this.button_Sbor_Usl_Add1.Enabled = true;
      this.button_Sbor_Usl_Edit1.Enabled = false;
      this.button_Sbor_Usl_Delete1.Enabled = false;
      this.button_Sbor_Usl_BezUsl.Enabled = false;
      this.button_Sbor_Usl_NeVvodit.Enabled = true;
    }
  }

  /// <summary> Номер раздела по его заголовку </summary>
  /// <param textFromColumn="caption"></param>
  /// <returns></returns>
  public int Specification_by_Caption(string caption)
  {
    if (caption == "")
      return -1;
    int num = -1;
    for (int index = 0; index < Vedomost_VB_Static._list_SpecificationSectionInfo.Count; ++index)
    {
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[index];
      if (caption == specificationSectionInfo.Caption)
        return index;
    }
    return num;
  }

  private void button_Sbor_Usl_NeVvodit_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora.SelectedNode == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
      A_NastrVed.UsloviaNode usloviaNode1 = selectedNode._uroven != 0 ? selectedNode._usloviaNodeRazdel : selectedNode;
      int iRazd = usloviaNode1._i_Razd;
      A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
      usloviaNode2.Text = "Ввод не производится";
      usloviaNode2.ImageIndex = this._indexImageList_InvalidRule;
      usloviaNode2.SelectedImageIndex = this._indexImageList_InvalidRule;
      A_NastrVed.UsloviaNode node = usloviaNode2;
      node._usloviaNodeRazdel = usloviaNode1;
      node._i_Razd = iRazd;
      node._uroven = 1;
      node._usloviaNodeParent = usloviaNode1;
      this.treeView_UsloviaSbora.Nodes[iRazd].Nodes.Clear();
      this.treeView_UsloviaSbora.Nodes[iRazd].Nodes.Add((TreeNode) node);
      this.treeView_UsloviaSbora.Nodes[iRazd].Expand();
      this.treeView_UsloviaSbora.SelectedNode = (TreeNode) usloviaNode1;
      this.treeView_UsloviaSbora.Select();
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
      {
        if (this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index]._section_SP == specificationSectionInfo.Caption)
        {
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.RemoveAt(index);
          break;
        }
      }
      this.draw_Label_UsloviaSbora_Current();
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
  }

  /// <summary> Условия сбора. Нажатие мышкой на узел </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void treeViewUsloviaVVoda_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._usl_Read_From_SP_CurrentRazdel != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count > 0)
      this.treeView_UsloviaSbora.SelectedNode.Expand();
    this.usloviaNode_Current = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora.SelectedNode;
    this._usl_Read_From_SP_CurrentRazdel = this.Usl_Read_From_SP(this.usloviaNode_Current._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP);
    this.usl_Read_From_SP_One_Current = this.usloviaNode_Current._i_usl <= -1 || this._usl_Read_From_SP_CurrentRazdel == null || this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One == null || this.usloviaNode_Current._i_usl >= this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count ? (Vedomost_VB.Usl_Read_From_SP_One) null : this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current._i_usl];
    if (this.treeView_UsloviaSbora.SelectedNode.Parent == null)
    {
      this.button_Sbor_Usl_Add1.Enabled = true;
      this.button_Sbor_Usl_Edit1.Enabled = false;
      this.button_Sbor_Usl_Delete1.Enabled = false;
      if (this._usl_Read_From_SP_CurrentRazdel == null || this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One == null)
      {
        this.button_Sbor_Usl_BezUsl.Enabled = true;
        this.button_Sbor_Usl_NeVvodit.Enabled = false;
      }
      if (this._usl_Read_From_SP_CurrentRazdel != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count == 0)
      {
        this.button_Sbor_Usl_BezUsl.Enabled = false;
        this.button_Sbor_Usl_NeVvodit.Enabled = true;
      }
      if (this._usl_Read_From_SP_CurrentRazdel != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One.Count > 0)
      {
        this.button_Sbor_Usl_BezUsl.Enabled = true;
        this.button_Sbor_Usl_NeVvodit.Enabled = true;
      }
    }
    else
    {
      if (this.treeView_UsloviaSbora.SelectedNode.ImageIndex == this._indexImageList_GreenBall)
      {
        this.button_Sbor_Usl_Add1.Enabled = true;
        this.button_Sbor_Usl_Edit1.Enabled = false;
        this.button_Sbor_Usl_Delete1.Enabled = false;
        this.button_Sbor_Usl_BezUsl.Enabled = false;
        this.button_Sbor_Usl_NeVvodit.Enabled = true;
      }
      if (this.treeView_UsloviaSbora.SelectedNode.ImageIndex == this._indexImageList_InvalidRule)
      {
        this.button_Sbor_Usl_Add1.Enabled = true;
        this.button_Sbor_Usl_Edit1.Enabled = false;
        this.button_Sbor_Usl_Delete1.Enabled = false;
        this.button_Sbor_Usl_BezUsl.Enabled = true;
        this.button_Sbor_Usl_NeVvodit.Enabled = false;
      }
      if (this.treeView_UsloviaSbora.SelectedNode.ImageIndex == this._indexImageList_RuleCriterion)
      {
        this.button_Sbor_Usl_Add1.Enabled = true;
        this.button_Sbor_Usl_Edit1.Enabled = true;
        this.button_Sbor_Usl_Delete1.Enabled = true;
        this.button_Sbor_Usl_BezUsl.Enabled = true;
        this.button_Sbor_Usl_NeVvodit.Enabled = true;
      }
    }
    this.draw_GroupBox_From_Uslovie();
    this.draw_Label_UsloviaSbora_Current();
  }

  private void draw_Label_UsloviaSbora_Current()
  {
    TreeNode selectedNode = this.treeView_UsloviaSbora.SelectedNode;
    string str = "";
    if (selectedNode != null)
    {
      str = selectedNode.Text;
      TreeNode parent1 = selectedNode.Parent;
      if (parent1 != null)
      {
        str = $"{parent1.Text}/{str}";
        TreeNode parent2 = parent1.Parent;
        if (parent2 != null)
        {
          str = $"{parent2.Text}/{str}";
          TreeNode parent3 = parent2.Parent;
          if (parent3 != null)
            str = $"{parent3.Text}/{str}";
        }
      }
    }
    this.label_UsloviaSbora_Current.Text = str;
  }

  /// <summary> На основании текущего условия выставить в эти же положения элементы диалога </summary>
  private void draw_GroupBox_From_Uslovie()
  {
    if (this.usl_Read_From_SP_One_Current == null)
    {
      this.radioButton_Sbor_Usl_Ravno.Checked = true;
      this.textBox_Sbor_Usl_TextDliaSravnenia.Text = "";
      this.radioButton_Sbor_Usl_Ili.Checked = true;
      this.groupBox_Sbor_Usl_I_ILI.Enabled = false;
      this.select_Sbor_Usl_AttributeControl1.SelectedAttributeId = -1;
    }
    else
    {
      if (this.usl_Read_From_SP_One_Current._uslovie == "=")
        this.radioButton_Sbor_Usl_Ravno.Checked = true;
      if (this.usl_Read_From_SP_One_Current._uslovie == "!=")
        this.radioButton_Sbor_Usl_NeRavno.Checked = true;
      if (this.usl_Read_From_SP_One_Current._uslovie == "?")
        this.radioButton_Sbor_Usl_Soderzit.Checked = true;
      if (this.usl_Read_From_SP_One_Current._uslovie == "!?")
        this.radioButton_Sbor_Usl_NeSoderzit.Checked = true;
      if (this.usl_Read_From_SP_One_Current._uslovie == "&")
        this.radioButton_Sbor_Usl_Nathinaetsia.Checked = true;
      this.textBox_Sbor_Usl_TextDliaSravnenia.Text = this.usl_Read_From_SP_One_Current._text;
      this.groupBox_Sbor_Usl_I_ILI.Enabled = true;
      if (this.usloviaNode_Current._i_usl == 0)
        this.radioButton_Sbor_Usl_Ili.Checked = true;
      else if (this._usl_Read_From_SP_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current._i_usl - 1]._or_and)
        this.radioButton_Sbor_Usl_I.Checked = true;
      else
        this.radioButton_Sbor_Usl_Ili.Checked = true;
      this.select_Sbor_Usl_AttributeControl1.SelectedAttributeId = this.usl_Read_From_SP_One_Current._oneFieldSpForRead._id;
    }
  }

  private void checkBox_Others_Reference_Show_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    if (this.checkBox_Others_Reference_Show.Checked)
      this.tabPage_Sbor_Usl_Reference.Parent = (Control) this.tabControl_Page_Sbor;
    else
      this.tabPage_Sbor_Usl_Reference.Parent = (Control) null;
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
      this.tabPage_ESPD.Parent = (Control) this.tabControl_Page_Sbor;
    else
      this.tabPage_ESPD.Parent = (Control) null;
  }

  /// <summary> Рисование подстраницы "Передача данных" </summary>
  private void Draw_PodPage_Sbor_Peredatha()
  {
    this.select_Sbor_Peredatha_AttributeControl2.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    this.listBox_Sbor_Peredatha_ListId_Draw();
  }

  /// <summary> Рисование списка передаваемых атрибутов </summary>
  private void listBox_Sbor_Peredatha_ListId_Draw()
  {
    this.listBox_Sbor_Peredatha_ListId.Items.Clear();
    if (Vedomost_VB_Static._listObligatoryId.Count == 0)
      Vedomost_VB_Static.ListObligatoryId_Filled();
    if (this._one_Ved_Nastr_Tmp._list_Ved_ID == null)
      return;
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._list_Ved_ID.Count; ++index1)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this._one_Ved_Nastr_Tmp._list_Ved_ID[index1];
      string str = oneFieldSpForRead1._name;
      bool flag = false;
      for (int index2 = 0; index2 < Vedomost_VB_Static._listObligatoryId.Count; ++index2)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead2 = Vedomost_VB_Static._listObligatoryId[index2];
        if (oneFieldSpForRead1._name == oneFieldSpForRead2._name)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        str = "*" + str;
      if (oneFieldSpForRead1._attributeSourceTypes == AttributeSourceTypes.Relation)
        str += " (связь)";
      this.listBox_Sbor_Peredatha_ListId.Items.Add((object) str);
    }
  }

  /// <summary> Добавить имя в список передаваемых атрибутов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Sbor_Peredatha_Add2_Click(object sender, EventArgs e)
  {
    if (this.select_Sbor_Peredatha_AttributeControl2.SelectedAttributeId == -1)
    {
      int num1 = (int) MessageBox.Show("Не выбран атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      int isUzeEst = -1;
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Peredatha_AttributeControl2.SelectedAttribute);
      if (specRowAttributeInfo == null || this.Add_Id_To_list_Ved_ID(specRowAttributeInfo, out isUzeEst) || isUzeEst <= -1)
        return;
      this.listBox_Sbor_Peredatha_ListId.SelectedIndex = isUzeEst;
      int num2 = (int) MessageBox.Show("Данный атрибут в списке уже есть", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
  }

  /// <summary> Добавление в список ВВОДИМЫХ </summary>
  /// <param textFromColumn="specRowAttributeInfo"></param>
  /// <param textFromColumn="isUzeEst"></param>
  /// <returns></returns>
  private bool Add_Id_To_list_Ved_ID(AvsRowAttributeInfo specRowAttributeInfo, out int isUzeEst)
  {
    if (specRowAttributeInfo == null)
    {
      isUzeEst = -1;
      return false;
    }
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Ved_ID.Count; ++index)
    {
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this._one_Ved_Nastr_Tmp._list_Ved_ID[index];
      if (specRowAttributeInfo.AttributeId == oneFieldSpForRead._id)
      {
        isUzeEst = index;
        return false;
      }
    }
    Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
    if (oneFieldSpForRead1 == null)
    {
      isUzeEst = -1;
      return false;
    }
    this._one_Ved_Nastr_Tmp._list_Ved_ID.Add(oneFieldSpForRead1);
    string name = oneFieldSpForRead1._name;
    if (oneFieldSpForRead1._attributeSourceTypes == AttributeSourceTypes.Relation)
      name += " (связь)";
    this.listBox_Sbor_Peredatha_ListId.Items.Add((object) name);
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
    isUzeEst = this.listBox_Sbor_Peredatha_ListId.Items.Count - 1;
    return true;
  }

  /// <summary> Удалить имя из списка передаваемых атрибутов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Sbor_Peredatha_Delete2_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.listBox_Sbor_Peredatha_ListId.SelectedIndex;
    if (this.listBox_Sbor_Peredatha_ListId.SelectedIndex < 0)
      return;
    if (this.listBox_Sbor_Peredatha_ListId.Items[selectedIndex].ToString()[0] == '*')
    {
      int num = (int) MessageBox.Show("Системные атрибуты (помеченные знаком *) не удаляются", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      this._one_Ved_Nastr_Tmp._list_Ved_ID.RemoveAt(selectedIndex);
      this.listBox_Sbor_Peredatha_ListId.Items.RemoveAt(selectedIndex);
      if (this.listBox_Sbor_Peredatha_ListId.Items.Count == 0)
      {
        this.listBox_Sbor_Peredatha_ListId.SelectedIndex = -1;
      }
      else
      {
        if (selectedIndex >= this.listBox_Sbor_Peredatha_ListId.Items.Count)
          --selectedIndex;
        this.listBox_Sbor_Peredatha_ListId.SelectedIndex = selectedIndex;
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
  }

  /// <summary> Нажатие кнопки DEL </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Sbor_Peredatha_ListId_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.button_Sbor_Peredatha_Delete2_Click(sender, (EventArgs) e);
  }

  private void Draw_PodPage_Sbor_Others()
  {
    if (this._one_Ved_Nastr_Tmp._sbor_Options == null)
      return;
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Checked = this._one_Ved_Nastr_Tmp._sbor_Options._isSamuSP_ne_iz_spiska_zanosit;
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Checked = this._one_Ved_Nastr_Tmp._sbor_Options._isRaskrSP_s_takoi_Ved != 0;
    this.checkBox_Others_Reference_Show.Checked = this._one_Ved_Nastr_Tmp._sbor_Options._isReference_Show;
    if (this._one_Ved_Nastr_Tmp._sbor_Options._isReference_Show)
      this.tabPage_Sbor_Usl_Reference.Parent = (Control) this.tabControl_Page_Sbor;
    else
      this.tabPage_Sbor_Usl_Reference.Parent = (Control) null;
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Checked = this._one_Ved_Nastr_Tmp._sbor_Options._is_Vydeliat_Therez_Komplekty;
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VP)
    {
      this._one_Ved_Nastr_Tmp._sbor_Options._is_Vydeliat_Sami_Komplekty = false;
      this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Visible = false;
    }
    else
      this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Visible = true;
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Checked = this._one_Ved_Nastr_Tmp._sbor_Options._is_Vydeliat_Sami_Komplekty;
    if (this._one_Ved_Nastr_Tmp._sbor_Options._isDopZam == 0)
    {
      this.checkBox_Sbor_Others_IsDopZam.Checked = false;
      this.checkBox_Sbor_Others_IsAllocateDopZam.Checked = false;
      this.checkBox_Sbor_Others_IsAllocateDopZam.Visible = false;
    }
    else
    {
      this.checkBox_Sbor_Others_IsDopZam.Checked = true;
      this.checkBox_Sbor_Others_IsAllocateDopZam.Visible = true;
      if (this._one_Ved_Nastr_Tmp._sbor_Options._isAllocateDopZam == 0)
        this.checkBox_Sbor_Others_IsAllocateDopZam.Checked = false;
      else
        this.checkBox_Sbor_Others_IsAllocateDopZam.Checked = true;
    }
  }

  private void Draw_PodPage_Espd()
  {
    if (this._one_Ved_Nastr_Tmp._espd == null)
      return;
    this.checkBox_isAddLU.Checked = this._one_Ved_Nastr_Tmp._espd._isAddLU;
    if (this.checkBox_isAddLU.Checked)
    {
      this.checkBox_isCreateLU.Visible = true;
      this.checkBox_isCreateLU.Checked = this._one_Ved_Nastr_Tmp._espd._isCreateLU;
      if (this.checkBox_isCreateLU.Checked)
      {
        this.checkBox_isOpenLU.Visible = true;
        this.checkBox_isOpenLU.Checked = this._one_Ved_Nastr_Tmp._espd._isOpenLU;
      }
      else
      {
        this.checkBox_isOpenLU.Checked = false;
        this.checkBox_isOpenLU.Visible = false;
      }
    }
    else
    {
      this.checkBox_isCreateLU.Checked = false;
      this.checkBox_isCreateLU.Visible = false;
      this.checkBox_isOpenLU.Checked = false;
      this.checkBox_isOpenLU.Visible = false;
    }
    this.checkBox_isAddToSpLU.Checked = this._one_Ved_Nastr_Tmp._espd._isAddToSpLU;
    this.checkBox_isAddRemark.Checked = this._one_Ved_Nastr_Tmp._espd._isAddRemark;
    if (this.checkBox_isAddRemark.Checked)
    {
      this.textBox_textRemark.Visible = true;
      this.textBox_textRemark.Text = this._one_Ved_Nastr_Tmp._espd._textRemark;
    }
    else
    {
      this.textBox_textRemark.Visible = false;
      this.textBox_textRemark.Text = this._one_Ved_Nastr_Tmp._espd._textRemark;
    }
    if (this.checkBox_isAddLU.Checked || this.checkBox_isAddToSpLU.Checked)
      this.groupBox_Remark.Visible = true;
    else
      this.groupBox_Remark.Visible = false;
  }

  /// <summary> Заносить в спецификацию Лист урверждения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isAddLU_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    this._one_Ved_Nastr_Tmp._espd._isAddLU = this.checkBox_isAddLU.Checked;
    if (this.checkBox_isAddLU.Checked)
    {
      this.checkBox_isCreateLU.Visible = true;
      this.checkBox_isCreateLU.Checked = this._one_Ved_Nastr_Tmp._espd._isCreateLU;
      if (this.checkBox_isCreateLU.Checked)
      {
        this.checkBox_isOpenLU.Visible = true;
        this.checkBox_isOpenLU.Checked = this._one_Ved_Nastr_Tmp._espd._isOpenLU;
      }
      else
      {
        this.checkBox_isOpenLU.Checked = false;
        this.checkBox_isOpenLU.Visible = false;
      }
    }
    else
    {
      this.checkBox_isCreateLU.Checked = false;
      this.checkBox_isCreateLU.Visible = false;
      this.checkBox_isOpenLU.Checked = false;
      this.checkBox_isOpenLU.Visible = false;
    }
    if (this.checkBox_isAddLU.Checked || this.checkBox_isAddToSpLU.Checked)
      this.groupBox_Remark.Visible = true;
    else
      this.groupBox_Remark.Visible = false;
  }

  /// <summary> Создавать Лист утверждения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isCreateLU_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    this._one_Ved_Nastr_Tmp._espd._isCreateLU = this.checkBox_isCreateLU.Checked;
    if (this.checkBox_isCreateLU.Checked)
    {
      this.checkBox_isOpenLU.Visible = true;
      this.checkBox_isOpenLU.Checked = this._one_Ved_Nastr_Tmp._espd._isOpenLU;
    }
    else
    {
      this.checkBox_isOpenLU.Checked = false;
      this.checkBox_isOpenLU.Visible = false;
    }
  }

  /// <summary> После создания Листа утверждения открывать его в редакторе </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isOpenLU_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    this._one_Ved_Nastr_Tmp._espd._isOpenLU = this.checkBox_isOpenLU.Checked;
  }

  /// <summary> Автоматически заносить в спецификацию и Лист утвержения </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isAddToSpLU_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    this._one_Ved_Nastr_Tmp._espd._isAddToSpLU = this.checkBox_isAddToSpLU.Checked;
    if (this.checkBox_isAddLU.Checked || this.checkBox_isAddToSpLU.Checked)
      this.groupBox_Remark.Visible = true;
    else
      this.groupBox_Remark.Visible = false;
  }

  /// <summary> Заносить текст в Примечание </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isAddRemark_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.isCreate)
    {
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
    this._one_Ved_Nastr_Tmp._espd._isAddRemark = this.checkBox_isAddRemark.Checked;
    if (this.checkBox_isAddRemark.Checked)
    {
      this.textBox_textRemark.Visible = true;
      this.textBox_textRemark.Text = this._one_Ved_Nastr_Tmp._espd._textRemark;
    }
    else
    {
      this.textBox_textRemark.Visible = false;
      this.textBox_textRemark.Text = this._one_Ved_Nastr_Tmp._espd._textRemark;
    }
  }

  private void textBox_textRemark_TextChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void textBox_textRemark_Leave(object sender, EventArgs e)
  {
    this._one_Ved_Nastr_Tmp._espd._textRemark = this.textBox_textRemark.Text;
  }

  private void checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit_CheckedChanged(
    object sender,
    EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty_CheckedChanged(
    object sender,
    EventArgs e)
  {
    if (this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Checked)
      this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Checked = false;
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty_CheckedChanged(
    object sender,
    EventArgs e)
  {
    if (this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Checked)
      this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Checked = false;
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void checkBox_Sbor_Others_IsDopZam_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Sbor_Others_IsDopZam.Checked)
    {
      this.checkBox_Sbor_Others_IsAllocateDopZam.Visible = true;
      this.checkBox_Sbor_Others_IsAllocateDopZam.Checked = true;
    }
    else
    {
      this.checkBox_Sbor_Others_IsAllocateDopZam.Checked = false;
      this.checkBox_Sbor_Others_IsAllocateDopZam.Visible = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void checkBox_Sbor_Others_IsAllocateDopZam_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  /// <summary> Кнопка -&gt;&gt; </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Add_To_listBox_QuickObjectInfo_Click(object sender, EventArgs e)
  {
    if (this.listBox_CatalogsImbase.SelectedIndex < 0)
      return;
    int selectedIndex = this.listBox_CatalogsImbase.SelectedIndex;
    QuickObjectInfo quickObjectInfo = this.list_CalalogsImbaseTmp[selectedIndex];
    this.list_CalalogsImbaseTmp.RemoveAt(selectedIndex);
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.Add(quickObjectInfo);
    this.listBox_QuickObjectInfo.Items.Add((object) quickObjectInfo.Caption);
    this.Draw_listBox_CatalogsImbase();
    if (selectedIndex > 0)
      this.listBox_CatalogsImbase.SelectedIndex = selectedIndex - 1;
    else if (this.listBox_CatalogsImbase.Items.Count == 0)
      this.listBox_CatalogsImbase.SelectedIndex = -1;
    else
      this.listBox_CatalogsImbase.SelectedIndex = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  /// <summary> Удаление из списка выбранных Каталогов (Кнопка  &lt;&lt;-) </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Delete_From_To_listBox_QuickObjectInfo_Click(object sender, EventArgs e)
  {
    if (this.listBox_QuickObjectInfo.SelectedIndex < 0)
      return;
    int selectedIndex = this.listBox_QuickObjectInfo.SelectedIndex;
    QuickObjectInfo quickObjectInfo = this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo[selectedIndex];
    this._one_Ved_Nastr_Tmp._bases_Options_Ved._list_quickObjectInfo.RemoveAt(selectedIndex);
    this.listBox_QuickObjectInfo.Items.RemoveAt(selectedIndex);
    this.list_CalalogsImbaseTmp.Add(quickObjectInfo);
    this.Draw_listBox_CatalogsImbase();
    if (selectedIndex > 0)
      this.listBox_QuickObjectInfo.SelectedIndex = selectedIndex - 1;
    else if (this.listBox_QuickObjectInfo.Items.Count == 0)
      this.listBox_QuickObjectInfo.SelectedIndex = -1;
    else
      this.listBox_QuickObjectInfo.SelectedIndex = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Bases = true;
  }

  private void treeView_UsloviaSbora_Reference_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this._usl_Read_From_SP_Reference_CurrentRazdel != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count > 0)
      this.treeView_UsloviaSbora_Reference.SelectedNode.Expand();
    this.usloviaNode_Current_Reference = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
    if (this.usloviaNode_Current_Reference == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      this._usl_Read_From_SP_Reference_CurrentRazdel = this.Usl_Read_From_SP(this.usloviaNode_Current_Reference._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference);
      this.usl_Read_From_SP_Reference_One_Current = this.usloviaNode_Current_Reference._i_usl <= -1 || this._usl_Read_From_SP_Reference_CurrentRazdel == null || this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One == null || this.usloviaNode_Current_Reference._i_usl >= this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count ? (Vedomost_VB.Usl_Read_From_SP_One) null : this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current_Reference._i_usl];
      if (this.treeView_UsloviaSbora_Reference.SelectedNode.Parent == null)
      {
        this.button_Sbor_Usl_Reference_Add1.Enabled = true;
        this.button_Sbor_Usl_Reference_Edit1.Enabled = false;
        this.button_Sbor_Usl_Reference_Delete1.Enabled = false;
        if (this._usl_Read_From_SP_Reference_CurrentRazdel == null || this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One == null)
        {
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = true;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = false;
        }
        if (this._usl_Read_From_SP_Reference_CurrentRazdel != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count == 0)
        {
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = false;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = true;
        }
        if (this._usl_Read_From_SP_Reference_CurrentRazdel != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count > 0)
        {
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = true;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = true;
        }
      }
      else
      {
        if (this.treeView_UsloviaSbora_Reference.SelectedNode.ImageIndex == this._indexImageList_GreenBall)
        {
          this.button_Sbor_Usl_Reference_Add1.Enabled = true;
          this.button_Sbor_Usl_Reference_Edit1.Enabled = false;
          this.button_Sbor_Usl_Reference_Delete1.Enabled = false;
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = false;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = true;
        }
        if (this.treeView_UsloviaSbora_Reference.SelectedNode.ImageIndex == this._indexImageList_InvalidRule)
        {
          this.button_Sbor_Usl_Reference_Add1.Enabled = true;
          this.button_Sbor_Usl_Reference_Edit1.Enabled = false;
          this.button_Sbor_Usl_Reference_Delete1.Enabled = false;
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = true;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = false;
        }
        if (this.treeView_UsloviaSbora_Reference.SelectedNode.ImageIndex == this._indexImageList_RuleCriterion)
        {
          this.button_Sbor_Usl_Reference_Add1.Enabled = true;
          this.button_Sbor_Usl_Reference_Edit1.Enabled = true;
          this.button_Sbor_Usl_Reference_Delete1.Enabled = true;
          this.button_Sbor_Usl_Reference_BezUsl.Enabled = true;
          this.button_Sbor_Usl_Reference_NeVvodit.Enabled = true;
        }
      }
      this.draw_GroupBox_From_Uslovie_Reference();
    }
  }

  private void button_Sbor_Usl_Reference_Add1_Click(object sender, EventArgs e)
  {
    if (this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttributeId == -1)
    {
      int num1 = (int) MessageBox.Show("Не выбран атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttribute);
      if (specRowAttributeInfo == null)
        return;
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne1 = new Vedomost_VB.Usl_Read_From_SP_One();
      Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
      if (oneFieldSpForRead == null)
      {
        int num2 = (int) MessageBox.Show("Не смогли обработать выбранный атрибут", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne2 = new Vedomost_VB.Usl_Read_From_SP_One();
        uslReadFromSpOne2._oneFieldSpForRead = oneFieldSpForRead;
        uslReadFromSpOne2._text = this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Text;
        if (this.radioButton_Sbor_Usl_Ravno_Reference.Checked)
          uslReadFromSpOne2._uslovie = "=";
        if (this.radioButton_Sbor_Usl_NeRavno_Reference.Checked)
          uslReadFromSpOne2._uslovie = "!=";
        if (this.radioButton_Sbor_Usl_Soderzit_Reference.Checked)
          uslReadFromSpOne2._uslovie = "?";
        if (this.radioButton_Sbor_Usl_NeSoderzit_Reference.Checked)
          uslReadFromSpOne2._uslovie = "!?";
        if (this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Checked)
          uslReadFromSpOne2._uslovie = "&";
        if (this._usl_Read_From_SP_Reference_CurrentRazdel == null)
        {
          int index = this.Nomer_Usl_Read_From_SP(this.usloviaNode_Current_Reference._i_Razd, this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference);
          if (index < 0)
            return;
          this._usl_Read_From_SP_Reference_CurrentRazdel = new Vedomost_VB.Usl_Read_From_SP();
          this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>();
          this._usl_Read_From_SP_Reference_CurrentRazdel._section_SP = Vedomost_VB_Static.FindRazdelCaptionByRazdelNum(this.usloviaNode_Current_Reference._i_Razd);
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Insert(index, this._usl_Read_From_SP_Reference_CurrentRazdel);
        }
        if (this.usl_Read_From_SP_Reference_One_Current != null)
        {
          Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne3 = this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current_Reference._i_usl];
          uslReadFromSpOne2._or_and = this.usl_Read_From_SP_Reference_One_Current._or_and;
          if (this.radioButton_Sbor_Usl_I_Reference.Checked)
            this.usl_Read_From_SP_Reference_One_Current._or_and = true;
          if (this.radioButton_Sbor_Usl_Ili_Reference.Checked)
            this.usl_Read_From_SP_Reference_One_Current._or_and = false;
          this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Insert(this.usloviaNode_Current_Reference._i_usl + 1, uslReadFromSpOne2);
        }
        else
        {
          uslReadFromSpOne2._or_and = true;
          this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Insert(0, uslReadFromSpOne2);
        }
        this.treeView_UsloviaSbora_Reference_Draw();
        this.ModifiedAll(true);
        this.IsModified_Page_Sbor = true;
      }
    }
  }

  private void button_Sbor_Usl_Reference_Edit1_Click(object sender, EventArgs e)
  {
    string prevision_Or_And = "";
    this.usl_Read_From_SP_Reference_One_Current._text = this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Text;
    if (this.radioButton_Sbor_Usl_Ravno_Reference.Checked)
      this.usl_Read_From_SP_Reference_One_Current._uslovie = "=";
    if (this.radioButton_Sbor_Usl_NeRavno_Reference.Checked)
      this.usl_Read_From_SP_Reference_One_Current._uslovie = "!=";
    if (this.radioButton_Sbor_Usl_Soderzit_Reference.Checked)
      this.usl_Read_From_SP_Reference_One_Current._uslovie = "?";
    if (this.radioButton_Sbor_Usl_NeSoderzit_Reference.Checked)
      this.usl_Read_From_SP_Reference_One_Current._uslovie = "!?";
    if (this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Checked)
      this.usl_Read_From_SP_Reference_One_Current._uslovie = "&";
    if (this.usloviaNode_Current_Reference._i_usl != 0)
    {
      Vedomost_VB.Usl_Read_From_SP_One uslReadFromSpOne = this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current_Reference._i_usl - 1];
      if (this.radioButton_Sbor_Usl_I_Reference.Checked)
        uslReadFromSpOne._or_and = true;
      if (this.radioButton_Sbor_Usl_Ili_Reference.Checked)
        uslReadFromSpOne._or_and = false;
      prevision_Or_And = !this.usl_Read_From_SP_Reference_One_Current._or_and ? "или" : "и";
    }
    if (this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttributeId != -1)
    {
      AvsRowAttributeInfo specRowAttributeInfo = new AvsRowAttributeInfo(this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttribute);
      if (specRowAttributeInfo != null)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead1 = this.usl_Read_From_SP_Reference_One_Current._oneFieldSpForRead;
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead2 = this.create_OneFieldSpForRead(specRowAttributeInfo, false);
        if (oneFieldSpForRead2 != null)
        {
          oneFieldSpForRead1._attributeSourceTypes = oneFieldSpForRead2._attributeSourceTypes;
          oneFieldSpForRead1._guid = oneFieldSpForRead2._guid;
          oneFieldSpForRead1._id = oneFieldSpForRead2._id;
          oneFieldSpForRead1._name = oneFieldSpForRead2._name;
          oneFieldSpForRead1._perv_Vtor = oneFieldSpForRead2._perv_Vtor;
          oneFieldSpForRead1._type = oneFieldSpForRead2._type;
        }
      }
    }
    this.usloviaNode_Current_Reference.Text = this.strokaTreeViewsUsl(this.usl_Read_From_SP_Reference_One_Current, prevision_Or_And);
    this.treeView_UsloviaSbora_Reference.SelectedNode = (TreeNode) this.usloviaNode_Current_Reference;
    this.treeView_UsloviaSbora_Reference.Select();
    this.ModifiedAll(true);
    this.IsModified_Page_Sbor = true;
  }

  private void button_Sbor_Usl_Reference_Delete1_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora_Reference.SelectedNode == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (this._usl_Read_From_SP_Reference_CurrentRazdel != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count == 1)
    {
      this.button_Sbor_Usl_Reference_BezUsl_Click(sender, e);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode1 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
      A_NastrVed.UsloviaNode node1 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.Nodes[selectedNode1._i_Razd];
      A_NastrVed.UsloviaNode usloviaNodeParent = selectedNode1._usloviaNodeParent;
      A_NastrVed.UsloviaNode usloviaNode1 = selectedNode1._i_usl <= 0 ? usloviaNodeParent : (A_NastrVed.UsloviaNode) selectedNode1.PrevNode;
      if (selectedNode1.Nodes.Count > 0)
      {
        A_NastrVed.UsloviaNode node2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode.Nodes[0];
        selectedNode1.Nodes[0].Remove();
        int index = usloviaNodeParent.Nodes.IndexOf((TreeNode) selectedNode1);
        int iUsl = selectedNode1._i_usl;
        this.treeView_UsloviaSbora_Reference.SelectedNode.Remove();
        usloviaNodeParent.Nodes.Insert(index, (TreeNode) node2);
        if (this._usl_Read_From_SP_Reference_CurrentRazdel != null && this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One != null && iUsl < this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.Count)
          this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.RemoveAt(iUsl);
      }
      else
      {
        int iUsl = selectedNode1._i_usl;
        if (iUsl > 0)
          this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[iUsl - 1]._or_and = this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[iUsl]._or_and;
        this.treeView_UsloviaSbora_Reference.SelectedNode = (TreeNode) usloviaNode1;
        this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One.RemoveAt(iUsl);
        selectedNode1.Remove();
      }
      this.treeView_UsloviaSbora_Reference.SelectedNode = (TreeNode) usloviaNode1;
      this.treeView_UsloviaSbora_Reference.Select();
      A_NastrVed.UsloviaNode usloviaNode2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
      int iRazd = usloviaNode2._i_Razd;
      while (usloviaNode2.NextVisibleNode != null)
      {
        usloviaNode2 = (A_NastrVed.UsloviaNode) usloviaNode2.NextVisibleNode;
        if (usloviaNode2._i_Razd == iRazd)
        {
          if (usloviaNode2._i_usl > 0)
            --usloviaNode2._i_usl;
        }
        else
          break;
      }
      A_NastrVed.UsloviaNode usloviaNode3 = node1;
      while (usloviaNode3.NextVisibleNode != null)
      {
        usloviaNode3 = (A_NastrVed.UsloviaNode) usloviaNode3.NextVisibleNode;
        if (usloviaNode3._i_Razd != iRazd)
          break;
      }
      if (this.treeView_UsloviaSbora_Reference.SelectedNode.Nodes.Count == 0)
      {
        A_NastrVed.UsloviaNode selectedNode2 = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
        if (selectedNode2._uroven == 0)
        {
          A_NastrVed.UsloviaNode usloviaNode4 = new A_NastrVed.UsloviaNode();
          usloviaNode4.Text = "Ввод производится без условий";
          usloviaNode4.ImageIndex = this._indexImageList_GreenBall;
          usloviaNode4.SelectedImageIndex = this._indexImageList_GreenBall;
          A_NastrVed.UsloviaNode node3 = usloviaNode4;
          node3._i_Razd = selectedNode2._i_Razd;
          node3._uroven = 1;
          node3._usloviaNodeParent = selectedNode2;
          selectedNode2.Nodes.Add((TreeNode) node3);
          selectedNode2.Nodes[0].Expand();
        }
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
  }

  private void button_Sbor_Usl_Reference_BezUsl_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora_Reference.SelectedNode == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
      A_NastrVed.UsloviaNode usloviaNode1 = selectedNode._uroven != 0 ? selectedNode._usloviaNodeRazdel : selectedNode;
      int iRazd = usloviaNode1._i_Razd;
      A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
      usloviaNode2.Text = "Ввод производится без условий";
      usloviaNode2.ImageIndex = this._indexImageList_GreenBall;
      usloviaNode2.SelectedImageIndex = this._indexImageList_GreenBall;
      A_NastrVed.UsloviaNode node = usloviaNode2;
      node._usloviaNodeRazdel = usloviaNode1;
      node._i_Razd = iRazd;
      node._uroven = 1;
      node._usloviaNodeParent = usloviaNode1;
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Nodes.Clear();
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Nodes.Add((TreeNode) node);
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Expand();
      this.treeView_UsloviaSbora_Reference.SelectedNode = (TreeNode) usloviaNode1;
      this.treeView_UsloviaSbora_Reference.Select();
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
      bool flag = false;
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
      {
        Vedomost_VB.Usl_Read_From_SP uslReadFromSp = this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index];
        if (uslReadFromSp._section_SP == specificationSectionInfo.Caption)
        {
          uslReadFromSp._list_Usl_Read_From_SP_One.Clear();
          flag = true;
          break;
        }
      }
      if (!flag)
      {
        for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
        {
          int num2 = this.Specification_by_Caption(this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index]._section_SP);
          if (iRazd < num2)
          {
            this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Insert(index, new Vedomost_VB.Usl_Read_From_SP()
            {
              _section_SP = specificationSectionInfo.Caption,
              _list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>()
            });
            break;
          }
          if (index == this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count - 1)
          {
            this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Add(new Vedomost_VB.Usl_Read_From_SP()
            {
              _section_SP = specificationSectionInfo.Caption,
              _list_Usl_Read_From_SP_One = new List<Vedomost_VB.Usl_Read_From_SP_One>()
            });
            break;
          }
        }
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
      this.button_Sbor_Usl_Reference_Add1.Enabled = true;
      this.button_Sbor_Usl_Reference_Edit1.Enabled = false;
      this.button_Sbor_Usl_Reference_Delete1.Enabled = false;
      this.button_Sbor_Usl_Reference_BezUsl.Enabled = false;
      this.button_Sbor_Usl_Reference_NeVvodit.Enabled = true;
    }
  }

  private void button_Sbor_Usl_Reference_NeVvodit_Click(object sender, EventArgs e)
  {
    if (this.treeView_UsloviaSbora_Reference.SelectedNode == null)
    {
      int num = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      A_NastrVed.UsloviaNode selectedNode = (A_NastrVed.UsloviaNode) this.treeView_UsloviaSbora_Reference.SelectedNode;
      A_NastrVed.UsloviaNode usloviaNode1 = selectedNode._uroven != 0 ? selectedNode._usloviaNodeRazdel : selectedNode;
      int iRazd = usloviaNode1._i_Razd;
      A_NastrVed.UsloviaNode usloviaNode2 = new A_NastrVed.UsloviaNode();
      usloviaNode2.Text = "Ввод не производится";
      usloviaNode2.ImageIndex = this._indexImageList_InvalidRule;
      usloviaNode2.SelectedImageIndex = this._indexImageList_InvalidRule;
      A_NastrVed.UsloviaNode node = usloviaNode2;
      node._usloviaNodeRazdel = usloviaNode1;
      node._i_Razd = iRazd;
      node._uroven = 1;
      node._usloviaNodeParent = usloviaNode1;
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Nodes.Clear();
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Nodes.Add((TreeNode) node);
      this.treeView_UsloviaSbora_Reference.Nodes[iRazd].Expand();
      this.treeView_UsloviaSbora_Reference.SelectedNode = (TreeNode) usloviaNode1;
      this.treeView_UsloviaSbora_Reference.Select();
      SpecificationSectionInfo specificationSectionInfo = Vedomost_VB_Static._list_SpecificationSectionInfo[iRazd];
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.Count; ++index)
      {
        if (this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP[index]._section_SP == specificationSectionInfo.Caption)
        {
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP.RemoveAt(index);
          break;
        }
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Sbor = true;
    }
  }

  /// <summary> На основании текущего условия выставить в эти же положения элементы диалога </summary>
  private void draw_GroupBox_From_Uslovie_Reference()
  {
    if (this.usl_Read_From_SP_Reference_One_Current == null)
    {
      this.radioButton_Sbor_Usl_Ravno_Reference.Checked = true;
      this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Text = "";
      this.radioButton_Sbor_Usl_Ili_Reference.Checked = true;
      this.groupBox_Sbor_Usl_I_ILI_Reference.Enabled = false;
      this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttributeId = -1;
    }
    else
    {
      if (this.usl_Read_From_SP_Reference_One_Current._uslovie == "=")
        this.radioButton_Sbor_Usl_Ravno_Reference.Checked = true;
      if (this.usl_Read_From_SP_Reference_One_Current._uslovie == "!=")
        this.radioButton_Sbor_Usl_NeRavno_Reference.Checked = true;
      if (this.usl_Read_From_SP_Reference_One_Current._uslovie == "?")
        this.radioButton_Sbor_Usl_Soderzit_Reference.Checked = true;
      if (this.usl_Read_From_SP_Reference_One_Current._uslovie == "!?")
        this.radioButton_Sbor_Usl_NeSoderzit_Reference.Checked = true;
      if (this.usl_Read_From_SP_Reference_One_Current._uslovie == "&")
        this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Checked = true;
      this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Text = this.usl_Read_From_SP_Reference_One_Current._text;
      this.groupBox_Sbor_Usl_I_ILI_Reference.Enabled = true;
      if (this.usloviaNode_Current_Reference._i_usl == 0)
        this.radioButton_Sbor_Usl_Ili_Reference.Checked = true;
      else if (this._usl_Read_From_SP_Reference_CurrentRazdel._list_Usl_Read_From_SP_One[this.usloviaNode_Current_Reference._i_usl - 1]._or_and)
        this.radioButton_Sbor_Usl_I_Reference.Checked = true;
      else
        this.radioButton_Sbor_Usl_Ili_Reference.Checked = true;
      this.select_Sbor_Usl_AttributeControl_Reference.SelectedAttributeId = this.usl_Read_From_SP_Reference_One_Current._oneFieldSpForRead._id;
    }
  }

  /// <summary> Сохранение редактирования страницы "Сбор" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Sbor()
  {
    this._one_Ved_Nastr_Tmp._sbor_Options._isSamuSP_ne_iz_spiska_zanosit = this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Checked;
    this._one_Ved_Nastr_Tmp._sbor_Options._isRaskrSP_s_takoi_Ved = !this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Checked ? 0 : 1;
    this._one_Ved_Nastr_Tmp._sbor_Options._isReference_Show = this.checkBox_Others_Reference_Show.Checked;
    this._one_Ved_Nastr_Tmp._sbor_Options._is_Vydeliat_Therez_Komplekty = this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Checked;
    this._one_Ved_Nastr_Tmp._sbor_Options._is_Vydeliat_Sami_Komplekty = this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Checked;
    this._one_Ved_Nastr_Tmp._sbor_Options._isDopZam = !this.checkBox_Sbor_Others_IsDopZam.Checked ? 0 : 1;
    if (this.checkBox_Sbor_Others_IsAllocateDopZam.Checked)
      this._one_Ved_Nastr_Tmp._sbor_Options._isAllocateDopZam = 1;
    else
      this._one_Ved_Nastr_Tmp._sbor_Options._isAllocateDopZam = 0;
  }

  /// <summary>  Прорисовка страницы сортировки </summary>
  private void Draw_Page_Usl_Sorting()
  {
    this.isSortDoc = this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD;
    if (this._one_Ved_Nastr_Tmp._sorting_Usl == null)
      this._one_Ved_Nastr_Tmp._sorting_Usl = new Vedomost_VB.Sorting_Usl();
    if (!this.isSortDoc)
    {
      this.groupBox_Sorting_List_Ved_Graf.Visible = false;
      this.groupBox_Sorting_List_Ved_Id.Visible = true;
      this.groupBox_Sorting_AttribVedRec1.Visible = true;
      this.List_Ved_Id_Draw(this.listBox_Sorting_List_Ved_Id);
      this.listBox_Sorting_AttribVedRec_Filled();
      this.dataGridView_Sorting_Doc.Visible = false;
      this.dataGridView_Sorting.Visible = true;
      this.dataGridView_Sorting_Curr = this.dataGridView_Sorting;
      if (this.dataGridView_Sorting_Curr == this.dataGridView_Sorting)
        this.dataGridView_Sorting_Draw();
      else
        this.dataGridView_Sorting_Doc_Draw();
      this.dataGridView_Sorting_Curr.Focus();
    }
    else
    {
      if (this._one_Ved_Nastr_Tmp._sorting_Usl_Doc == null)
        this._one_Ved_Nastr_Tmp._sorting_Usl_Doc = new Vedomost_VB.Sorting_Usl_Doc();
      this.groupBox_Sorting_List_Ved_Id.Visible = false;
      this.groupBox_Sorting_AttribVedRec1.Visible = false;
      this.groupBox_Sorting_List_Ved_Graf.Visible = true;
      List<string> namesGrafaTemlate = Vedomost_VB_Static.Get_List_Names_Grafa_Temlate(this.imDocument_template_Vyvod);
      this.listBox_Sorting_List_Ved_Graf.Items.Clear();
      for (int index = 0; index < namesGrafaTemlate.Count; ++index)
        this.listBox_Sorting_List_Ved_Graf.Items.Add((object) namesGrafaTemlate[index]);
      this.dataGridView_Sorting_Doc.Visible = true;
      this.dataGridView_Sorting.Visible = false;
      this.dataGridView_Sorting_Curr = this.dataGridView_Sorting_Doc;
      this.dataGridView_Sorting_Doc_Draw();
      this.dataGridView_Sorting_Doc.Focus();
    }
  }

  /// <summary> Заполнение списка специализированных аттрибутов </summary>
  private void listBox_Sorting_AttribVedRec_Filled()
  {
    this.listBox_Sorting_AttribVedRec.Items.Clear();
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedRec.Count; ++index)
      this.listBox_Sorting_AttribVedRec.Items.Add((object) Vedomost_VB_Static._listOneAttribVedRec[index]._name);
  }

  /// <summary> Рисование Таблицы сортировки </summary>
  private void dataGridView_Sorting_Draw()
  {
    if (this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn == null || this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel == null)
      return;
    this.dataGridView_Sorting.Rows.Clear();
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count; ++index1)
    {
      Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel = this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel[index1];
      this.dataGridView_Sorting.Rows.Add((object[]) this.drawZagolString(sortingUslOneRazdel._razdelNum));
      this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].Cells[0].Value = (object) this.imageListSort.Images[0];
      this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGray;
      this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].DefaultCellStyle.Font = new Font(this.dataGridView_Sorting.DefaultCellStyle.Font, FontStyle.Bold);
      for (int index2 = 0; index2 < sortingUslOneRazdel._list_sorting_Usl_One.Count; ++index2)
      {
        Vedomost_VB.Sorting_Usl_One sorting_Usl_One = sortingUslOneRazdel._list_sorting_Usl_One[index2];
        this.dataGridView_Sorting.Rows.Add((object[]) this.drawInfoString(sorting_Usl_One));
        this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].Cells[0].Value = sorting_Usl_One._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
      }
    }
    if (this.dataGridView_Sorting.RowCount > 0)
    {
      this.SelectDataGridView_Sorting_Row(0);
      this.Usl_For_Rownumber(0);
    }
    else
    {
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
      {
        this.dataGridView_Sorting.Rows.Add((object[]) this.drawZagolString((long) this._one_Ved_Nastr_Tmp._list_RazdelsVed[index]._razdelVed));
        this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].Cells[0].Value = (object) this.imageListSort.Images[0];
        this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGray;
        this.dataGridView_Sorting.Rows[this.dataGridView_Sorting.RowCount - 1].DefaultCellStyle.Font = new Font(this.dataGridView_Sorting.DefaultCellStyle.Font, FontStyle.Bold);
      }
      this.sorting_Usl_One_curr = (Vedomost_VB.Sorting_Usl_One) null;
      this.Usl_For_Rownumber(0);
    }
  }

  private void dataGridView_Sorting_Doc_Draw()
  {
    if (this._one_Ved_Nastr_Tmp._sorting_Usl_Doc == null || this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc == null)
      return;
    this.dataGridView_Sorting_Doc.Rows.Clear();
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc.Count; ++index1)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel = this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc[index1];
      this.dataGridView_Sorting_Doc.Rows.Add((object[]) this.drawZagolString(sortingUslDocOneRazdel._razdelNum));
      this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].Cells[0].Value = (object) this.imageListSort.Images[0];
      this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGray;
      this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].DefaultCellStyle.Font = new Font(this.dataGridView_Sorting_Doc.DefaultCellStyle.Font, FontStyle.Bold);
      for (int index2 = 0; index2 < sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel.Count; ++index2)
      {
        Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_One = sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel[index2];
        this.dataGridView_Sorting_Doc.Rows.Add((object[]) this.drawInfoString_Doc(sorting_Usl_One));
        this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].Cells[0].Value = sorting_Usl_One._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
      }
    }
    if (this.dataGridView_Sorting_Doc.RowCount > 0)
    {
      this.SelectDataGridView_Sorting_Row(0);
      this.Usl_For_Rownumber(0);
    }
    else
    {
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
      {
        this.dataGridView_Sorting_Doc.Rows.Add((object[]) this.drawZagolString((long) this._one_Ved_Nastr_Tmp._list_RazdelsVed[index]._razdelVed));
        this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].Cells[0].Value = (object) this.imageListSort.Images[0];
        this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGray;
        this.dataGridView_Sorting_Doc.Rows[this.dataGridView_Sorting_Doc.RowCount - 1].DefaultCellStyle.Font = new Font(this.dataGridView_Sorting_Doc.DefaultCellStyle.Font, FontStyle.Bold);
      }
      this.sorting_Usl_Doc_OneGrafa_curr = (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
      this.Usl_For_Rownumber(0);
    }
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param textFromColumn="rowNum"></param>
  public void SelectDataGridView_Sorting_Row(int rowNum)
  {
    this.SelectDataGridView_Sorting_Cell(rowNum, 0);
  }

  /// <summary> Выделить ячейку </summary>
  /// <param textFromColumn="rowIndex"></param>
  /// <param textFromColumn="cellIndex"></param>
  public void SelectDataGridView_Sorting_Cell(int rowIndex, int cellIndex)
  {
    if (this.dataGridView_Sorting_Curr.Rows.Count <= 0)
      return;
    this.dataGridView_Sorting_Curr.CurrentCell = this.dataGridView_Sorting_Curr.Rows[rowIndex].Cells[0];
    this.dataGridView_Sorting_Curr.CurrentCell.Selected = true;
  }

  /// <summary> Рисование заголовка раздела </summary>
  /// <param textFromColumn="razdelNum"></param>
  /// <returns></returns>
  private string[] drawZagolString(long razdelNum)
  {
    string[] strArray = new string[5];
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed == null)
      this._one_Ved_Nastr_Tmp._list_RazdelsVed = new List<Vedomost_VB.OneRazdelVed>();
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count == 0)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed1 = new Vedomost_VB.OneRazdelVed()
      {
        _name = "Общий",
        _razdelVed = 1
      };
    }
    if (razdelNum != 1000L)
    {
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
      {
        Vedomost_VB.OneRazdelVed oneRazdelVed2 = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
        if (oneRazdelVed2._razdelVed == (int) razdelNum)
        {
          strArray[2] = oneRazdelVed2._name.ToString();
          break;
        }
      }
    }
    else
      strArray[2] = "Ведомости составных частей";
    return strArray;
  }

  /// <summary> Рисование информационной строки </summary>
  /// <param textFromColumn="sorting_Usl_Doc_OneGrafa"></param>
  /// <returns></returns>
  private string[] drawInfoString(Vedomost_VB.Sorting_Usl_One sorting_Usl_One)
  {
    string[] strArray = new string[6];
    string str1 = "";
    if (sorting_Usl_One._typeField == Vedomost_VB.TypeField.ObjectType)
      str1 = MetaDataHelper.GetAttributeTypeName(sorting_Usl_One._objectType);
    if (sorting_Usl_One._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
    {
      int index = -1;
      str1 = Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(sorting_Usl_One._typeFieldVedRec, out index)._name;
    }
    strArray[1] = str1;
    string str2 = "";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.S_begin)
      str2 = "начала параметра";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.S_pozicii)
      str2 = "буквы номер " + sorting_Usl_One._num_symb_ot.ToString();
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola)
      str2 = $"символа \"{sorting_Usl_One._symb_ot}\" номер {sorting_Usl_One._num_symb_ot.ToString()}";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola_s_konca)
      str2 = $"символа \"{sorting_Usl_One._symb_ot}\" номер {sorting_Usl_One._num_symb_ot.ToString()} (с конца строки)";
    strArray[2] = str2;
    string str3 = "";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_end)
      str3 = "конца параметра";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Skolko)
      str3 = "количество символов " + sorting_Usl_One._num_symb_do.ToString();
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_symbola)
      str3 = $"символа \"{sorting_Usl_One._symb_do}\" номер {sorting_Usl_One._num_symb_do.ToString()}";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_symbola_s_konca)
      str3 = $"символа \"{sorting_Usl_One._symb_do}\" номер {sorting_Usl_One._num_symb_do.ToString()} (с конца строки)";
    strArray[3] = str3;
    string str4 = "";
    if (sorting_Usl_One._sravnenie == Vedomost_VB.Sravnenie.Number)
      str4 = "Числовое";
    if (sorting_Usl_One._sravnenie == Vedomost_VB.Sravnenie.Symbol)
      str4 = "Символьное";
    strArray[4] = str4;
    string str5 = "";
    if (sorting_Usl_One._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce)
      str5 = "В конец";
    if (sorting_Usl_One._pustyeStroki == Vedomost_VB.PustyeStroki.Vnathale)
      str5 = "В начало";
    strArray[5] = str5;
    return strArray;
  }

  private string[] drawInfoString_Doc(
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_One)
  {
    string[] strArray = new string[6];
    string grafa = sorting_Usl_One._grafa;
    strArray[1] = grafa;
    string str1 = "";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.S_begin)
      str1 = "начала параметра";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.S_pozicii)
      str1 = "буквы номер " + sorting_Usl_One._num_symb_ot.ToString();
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola)
      str1 = $"символа \"{sorting_Usl_One._symb_ot}\" номер {sorting_Usl_One._num_symb_ot.ToString()}";
    if (sorting_Usl_One._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola_s_konca)
      str1 = $"символа \"{sorting_Usl_One._symb_ot}\" номер {sorting_Usl_One._num_symb_ot.ToString()} (с конца строки)";
    strArray[2] = str1;
    string str2 = "";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_end)
      str2 = "конца параметра";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Skolko)
      str2 = "количество символов " + sorting_Usl_One._num_symb_do.ToString();
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_symbola)
      str2 = $"символа \"{sorting_Usl_One._symb_do}\" номер {sorting_Usl_One._num_symb_do.ToString()}";
    if (sorting_Usl_One._endSravn == Vedomost_VB.EndSravn.Do_symbola_s_konca)
      str2 = $"символа \"{sorting_Usl_One._symb_do}\" номер {sorting_Usl_One._num_symb_do.ToString()} (с конца строки)";
    strArray[3] = str2;
    string str3 = "";
    if (sorting_Usl_One._sravnenie == Vedomost_VB.Sravnenie.Number)
      str3 = "Числовое";
    if (sorting_Usl_One._sravnenie == Vedomost_VB.Sravnenie.Symbol)
      str3 = "Символьное";
    strArray[4] = str3;
    string str4 = "";
    if (sorting_Usl_One._pustyeStroki == Vedomost_VB.PustyeStroki.Vkonce)
      str4 = "В конец";
    if (sorting_Usl_One._pustyeStroki == Vedomost_VB.PustyeStroki.Vnathale)
      str4 = "В начало";
    strArray[5] = str4;
    return strArray;
  }

  /// <summary> Текущее условие сортировки по номеру строки </summary>
  /// <param textFromColumn="RowNumber"></param>
  private void Usl_For_Rownumber(int RowNumber)
  {
    this.sorting_Usl_OneRazdel_curr = (Vedomost_VB.Sorting_Usl_OneRazdel) null;
    this.i_sorting_Usl_OneRazdel_curr = -1;
    this.sorting_Usl_One_curr = (Vedomost_VB.Sorting_Usl_One) null;
    this.i_sorting_Usl_One_curr = -1;
    this.typeSortRec = A_NastrVed.TypeSortRec.Undefined;
    if (this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn == null || this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel == null || RowNumber < 0 || this.dataGridView_Sorting_Curr.RowCount < 1)
      return;
    int num = -1;
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Count; ++index1)
    {
      Vedomost_VB.Sorting_Usl_OneRazdel sortingUslOneRazdel = this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel[index1];
      ++num;
      if (RowNumber == num)
      {
        this.i_sorting_Usl_OneRazdel_curr = index1;
        this.sorting_Usl_OneRazdel_curr = sortingUslOneRazdel;
        this.typeSortRec = A_NastrVed.TypeSortRec.Zagolovok;
        break;
      }
      for (int index2 = 0; index2 < sortingUslOneRazdel._list_sorting_Usl_One.Count; ++index2)
      {
        Vedomost_VB.Sorting_Usl_One sortingUslOne = sortingUslOneRazdel._list_sorting_Usl_One[index2];
        ++num;
        if (RowNumber == num)
        {
          this.i_sorting_Usl_OneRazdel_curr = index1;
          this.sorting_Usl_OneRazdel_curr = sortingUslOneRazdel;
          this.i_sorting_Usl_One_curr = index2;
          this.sorting_Usl_One_curr = sortingUslOne;
          this.typeSortRec = A_NastrVed.TypeSortRec.Info;
          return;
        }
      }
    }
  }

  private void Usl_For_Rownumber_Doc(int RowNumber)
  {
    this.sorting_Usl_Doc_OneRazdel_curr = (Vedomost_VB.Sorting_Usl_Doc_OneRazdel) null;
    this.i_sorting_Usl_Doc_OneRazdel_curr = -1;
    this.sorting_Usl_Doc_OneGrafa_curr = (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
    this.i_sorting_Usl_Doc_One_curr = -1;
    this.typeSortRec = A_NastrVed.TypeSortRec.Undefined;
    if (this._one_Ved_Nastr_Tmp._sorting_Usl_Doc == null || this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc == null || RowNumber < 0 || this.dataGridView_Sorting_Curr.RowCount < 1)
      return;
    int num = -1;
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc.Count; ++index1)
    {
      Vedomost_VB.Sorting_Usl_Doc_OneRazdel sortingUslDocOneRazdel = this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc[index1];
      ++num;
      if (RowNumber == num)
      {
        this.i_sorting_Usl_Doc_OneRazdel_curr = index1;
        this.sorting_Usl_Doc_OneRazdel_curr = sortingUslDocOneRazdel;
        this.typeSortRec = A_NastrVed.TypeSortRec.Zagolovok;
        break;
      }
      for (int index2 = 0; index2 < sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel.Count; ++index2)
      {
        Vedomost_VB.Sorting_Usl_Doc_OneGrafa sortingUslDocOneGrafa = sortingUslDocOneRazdel._list_sorting_Usl_Doc_OneRazdel[index2];
        ++num;
        if (RowNumber == num)
        {
          this.i_sorting_Usl_Doc_OneRazdel_curr = index1;
          this.sorting_Usl_Doc_OneRazdel_curr = sortingUslDocOneRazdel;
          this.i_sorting_Usl_Doc_One_curr = index2;
          this.sorting_Usl_Doc_OneGrafa_curr = sortingUslDocOneGrafa;
          this.typeSortRec = A_NastrVed.TypeSortRec.Info;
          return;
        }
      }
    }
  }

  /// <summary> Заполнение (изменение) sorting_Usl_OneGrafa </summary>
  /// <param textFromColumn="sorting_Usl_Doc_OneGrafa"></param>
  /// <returns></returns>
  public bool edit_sorting_Usl_One(
    Vedomost_VB.Sorting_Usl_One sorting_Usl_One,
    out AvsRowAttributeInfo specRowAttributeInfo)
  {
    specRowAttributeInfo = (AvsRowAttributeInfo) null;
    if (sorting_Usl_One == null || this.listBox_Sorting_List_Ved_Id.SelectedIndex < 0 && this.listBox_Sorting_AttribVedRec.SelectedIndex < 0)
      return false;
    if (this.listBox_Sorting_List_Ved_Id.SelectedIndex > -1)
    {
      this.listBox_Sorting_AttribVedRec.SelectedIndex = -1;
      sorting_Usl_One._objectType = this.Get_ObjType_By_index(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.listBox_Sorting_List_Ved_Id.SelectedIndex);
      sorting_Usl_One._typeField = Vedomost_VB.TypeField.ObjectType;
      sorting_Usl_One._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
    }
    else
    {
      sorting_Usl_One._objectType = -1;
      sorting_Usl_One._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
      Vedomost_VB.OneAttribVedRec oneAttribVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Sorting_AttribVedRec.SelectedIndex];
      sorting_Usl_One._typeFieldVedRec = oneAttribVedRec._typeFieldVedRec;
    }
    if (this.radioButton_Sorting_OtBegin.Checked)
    {
      sorting_Usl_One._beginSravn = Vedomost_VB.BeginSravn.S_begin;
      sorting_Usl_One._num_symb_ot = 0;
      sorting_Usl_One._symb_ot = "";
    }
    if (this.radioButton_Sorting_OtBukvyNumb.Checked)
    {
      sorting_Usl_One._beginSravn = Vedomost_VB.BeginSravn.S_pozicii;
      sorting_Usl_One._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_One._symb_ot = "";
    }
    if (this.radioButton_Sorting_OtSymbolNumb.Checked)
    {
      sorting_Usl_One._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola;
      sorting_Usl_One._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_One._symb_ot = this.translate_text(this.comboBox_Sorting_SymbolBegin.Text, false);
    }
    if (this.radioButton_Sorting_OtSymbolNumbEnd.Checked)
    {
      sorting_Usl_One._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola_s_konca;
      sorting_Usl_One._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_One._symb_ot = this.translate_text(this.comboBox_Sorting_SymbolBegin.Text, false);
    }
    if (this.radioButton_Sorting_DoEnd.Checked)
    {
      sorting_Usl_One._endSravn = Vedomost_VB.EndSravn.Do_end;
      sorting_Usl_One._num_symb_do = 0;
      sorting_Usl_One._symb_do = "";
    }
    if (this.radioButton_Sorting_DoBukvyNumb.Checked)
    {
      sorting_Usl_One._endSravn = Vedomost_VB.EndSravn.Skolko;
      sorting_Usl_One._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_One._symb_do = "";
    }
    if (this.radioButton_Sorting_DoSymbolNumb.Checked)
    {
      sorting_Usl_One._endSravn = Vedomost_VB.EndSravn.Do_symbola;
      sorting_Usl_One._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_One._symb_do = this.translate_text(this.comboBox_Sorting_SymbolEnd.Text, false);
    }
    if (this.radioButton_Sorting_DoSymbolNumbEnd.Checked)
    {
      sorting_Usl_One._endSravn = Vedomost_VB.EndSravn.Do_symbola_s_konca;
      sorting_Usl_One._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_One._symb_do = this.translate_text(this.comboBox_Sorting_SymbolEnd.Text, false);
    }
    sorting_Usl_One._sravnenie = !this.radioButton_Sorting_SravnenieSymbol.Checked ? Vedomost_VB.Sravnenie.Number : Vedomost_VB.Sravnenie.Symbol;
    sorting_Usl_One._pustyeStroki = !this.radioButton_Sorting_PustyeStrokiVnathale.Checked ? Vedomost_VB.PustyeStroki.Vkonce : Vedomost_VB.PustyeStroki.Vnathale;
    sorting_Usl_One._poriadokSortirovki = !this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked ? Vedomost_VB.PoriadokSortirovki.Ubyvanie : Vedomost_VB.PoriadokSortirovki.Vozrastanie;
    return true;
  }

  /// <summary> Заполнение (изменение) sorting_Usl_Doc_OneGrafa </summary>
  /// <param name="sorting_Usl_Doc_OneGrafa"></param>
  /// <param name="specRowAttributeInfo"></param>
  /// <returns></returns>
  public bool edit_sorting_Usl_Doc_One(
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sorting_Usl_Doc_OneGrafa,
    out string grafa)
  {
    grafa = "";
    if (sorting_Usl_Doc_OneGrafa == null || this.listBox_Sorting_List_Ved_Graf.SelectedIndex < 0)
      return false;
    grafa = (string) this.listBox_Sorting_List_Ved_Graf.Items[this.listBox_Sorting_List_Ved_Graf.SelectedIndex];
    sorting_Usl_Doc_OneGrafa._grafa = grafa;
    if (this.radioButton_Sorting_OtBegin.Checked)
    {
      sorting_Usl_Doc_OneGrafa._beginSravn = Vedomost_VB.BeginSravn.S_begin;
      sorting_Usl_Doc_OneGrafa._num_symb_ot = 0;
      sorting_Usl_Doc_OneGrafa._symb_ot = "";
    }
    if (this.radioButton_Sorting_OtBukvyNumb.Checked)
    {
      sorting_Usl_Doc_OneGrafa._beginSravn = Vedomost_VB.BeginSravn.S_pozicii;
      sorting_Usl_Doc_OneGrafa._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_Doc_OneGrafa._symb_ot = "";
    }
    if (this.radioButton_Sorting_OtSymbolNumb.Checked)
    {
      sorting_Usl_Doc_OneGrafa._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola;
      sorting_Usl_Doc_OneGrafa._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_Doc_OneGrafa._symb_ot = this.translate_text(this.comboBox_Sorting_SymbolBegin.Text, false);
    }
    if (this.radioButton_Sorting_OtSymbolNumbEnd.Checked)
    {
      sorting_Usl_Doc_OneGrafa._beginSravn = Vedomost_VB.BeginSravn.Ot_symbola_s_konca;
      sorting_Usl_Doc_OneGrafa._num_symb_ot = (int) this.numericUpDown_Sorting_NumberBegin.Value;
      sorting_Usl_Doc_OneGrafa._symb_ot = this.translate_text(this.comboBox_Sorting_SymbolBegin.Text, false);
    }
    if (this.radioButton_Sorting_DoEnd.Checked)
    {
      sorting_Usl_Doc_OneGrafa._endSravn = Vedomost_VB.EndSravn.Do_end;
      sorting_Usl_Doc_OneGrafa._num_symb_do = 0;
      sorting_Usl_Doc_OneGrafa._symb_do = "";
    }
    if (this.radioButton_Sorting_DoBukvyNumb.Checked)
    {
      sorting_Usl_Doc_OneGrafa._endSravn = Vedomost_VB.EndSravn.Skolko;
      sorting_Usl_Doc_OneGrafa._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_Doc_OneGrafa._symb_do = "";
    }
    if (this.radioButton_Sorting_DoSymbolNumb.Checked)
    {
      sorting_Usl_Doc_OneGrafa._endSravn = Vedomost_VB.EndSravn.Do_symbola;
      sorting_Usl_Doc_OneGrafa._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_Doc_OneGrafa._symb_do = this.translate_text(this.comboBox_Sorting_SymbolEnd.Text, false);
    }
    if (this.radioButton_Sorting_DoSymbolNumbEnd.Checked)
    {
      sorting_Usl_Doc_OneGrafa._endSravn = Vedomost_VB.EndSravn.Do_symbola_s_konca;
      sorting_Usl_Doc_OneGrafa._num_symb_do = (int) this.numericUpDown_Sorting_NumberEnd.Value;
      sorting_Usl_Doc_OneGrafa._symb_do = this.translate_text(this.comboBox_Sorting_SymbolEnd.Text, false);
    }
    sorting_Usl_Doc_OneGrafa._sravnenie = !this.radioButton_Sorting_SravnenieSymbol.Checked ? Vedomost_VB.Sravnenie.Number : Vedomost_VB.Sravnenie.Symbol;
    sorting_Usl_Doc_OneGrafa._pustyeStroki = !this.radioButton_Sorting_PustyeStrokiVnathale.Checked ? Vedomost_VB.PustyeStroki.Vkonce : Vedomost_VB.PustyeStroki.Vnathale;
    sorting_Usl_Doc_OneGrafa._poriadokSortirovki = !this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked ? Vedomost_VB.PoriadokSortirovki.Ubyvanie : Vedomost_VB.PoriadokSortirovki.Vozrastanie;
    return true;
  }

  /// <summary> Если тыкаем на верхний список, то в нижнем деактивируем строку </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Sorting_List_Ved_Id_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Sorting_AttribVedRec.SelectedIndex = -1;
  }

  /// <summary> Если тыкаем на нижний список, то в верхнем деактивируем строку </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Sorting_AttribVedRec_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Sorting_List_Ved_Id.SelectedIndex = -1;
  }

  /// <summary> Выбор текущей строки таблицы </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_Sorting_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    this.rowNumCurrent_Sorting = this.dataGridView_Sorting.CurrentCell.RowIndex;
    this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
    this.Displays_The_Current_Record();
  }

  private void dataGridView_Sorting_Doc_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    this.rowNumCurrent_Sorting = this.dataGridView_Sorting_Doc.CurrentCell.RowIndex;
    this.Usl_For_Rownumber_Doc(this.rowNumCurrent_Sorting);
    this.Displays_The_Current_Record();
  }

  /// <summary> Установка элементов диалога по условиям текущей строки таблицы </summary>
  private void Displays_The_Current_Record()
  {
    this.numericUpDown_Sorting_NumberBegin.Value = 1M;
    this.numericUpDown_Sorting_NumberBegin.Enabled = false;
    this.comboBox_Sorting_SymbolBegin.Text = "";
    this.comboBox_Sorting_SymbolBegin.Enabled = false;
    this.numericUpDown_Sorting_NumberEnd.Value = 1M;
    this.numericUpDown_Sorting_NumberEnd.Enabled = false;
    this.comboBox_Sorting_SymbolEnd.Text = "";
    this.comboBox_Sorting_SymbolEnd.Enabled = false;
    this._btnMoveUp_Sorting.Enabled = true;
    this._btnMoveDown_Sorting.Enabled = true;
    if (this.typeSortRec == A_NastrVed.TypeSortRec.Zagolovok || this.typeSortRec == A_NastrVed.TypeSortRec.Undefined || this.dataGridView_Sorting_Curr.CurrentCell.RowIndex < 0)
    {
      this.radioButton_Sorting_OtBegin.Checked = true;
      this.radioButton_Sorting_DoEnd.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Value = 1M;
      this.numericUpDown_Sorting_NumberBegin.Enabled = false;
      this.comboBox_Sorting_SymbolBegin.Text = "";
      this.comboBox_Sorting_SymbolBegin.Enabled = false;
      this.numericUpDown_Sorting_NumberEnd.Value = 1M;
      this.numericUpDown_Sorting_NumberEnd.Enabled = false;
      this.comboBox_Sorting_SymbolEnd.Text = "";
      this.comboBox_Sorting_SymbolEnd.Enabled = false;
      this.radioButton_Sorting_SravnenieSymbol.Checked = true;
      this.radioButton_Sorting_PustyeStrokiVkonce.Checked = true;
      this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked = true;
      this.buttonEdit_Sorting_1.Enabled = false;
      this.buttonDelete_Sorting_1.Enabled = false;
      this._btnMoveUp_Sorting.Enabled = false;
      this._btnMoveDown_Sorting.Enabled = false;
    }
    else if (!this.isSortDoc)
      this.Displays_The_Current_Record_Usl();
    else
      this.Displays_The_Current_Record_Doc();
  }

  private void Displays_The_Current_Record_Usl()
  {
    if (this.i_sorting_Usl_One_curr == 0)
      this._btnMoveUp_Sorting.Enabled = false;
    if (this.i_sorting_Usl_One_curr >= this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Count - 1)
      this._btnMoveDown_Sorting.Enabled = false;
    if (this.sorting_Usl_One_curr._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      this.List_Ved_Id_SelectedByObjType(this.listBox_Sorting_List_Ved_Id, this._one_Ved_Nastr_Tmp._list_Ved_ID, this.sorting_Usl_One_curr._objectType);
      this.listBox_Sorting_AttribVedRec.SelectedIndex = -1;
    }
    if (this.sorting_Usl_One_curr._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
    {
      int index = -1;
      Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this.sorting_Usl_One_curr._typeFieldVedRec, out index);
      this.listBox_Sorting_AttribVedRec.SelectedIndex = index;
      this.listBox_Sorting_List_Ved_Id.SelectedIndex = -1;
    }
    if (this.sorting_Usl_One_curr._beginSravn == Vedomost_VB.BeginSravn.S_begin)
      this.radioButton_Sorting_OtBegin.Checked = true;
    if (this.sorting_Usl_One_curr._beginSravn == Vedomost_VB.BeginSravn.S_pozicii)
    {
      this.radioButton_Sorting_OtBukvyNumb.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_ot;
    }
    if (this.sorting_Usl_One_curr._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola)
    {
      this.radioButton_Sorting_OtSymbolNumb.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_ot;
      this.comboBox_Sorting_SymbolBegin.Enabled = true;
      this.comboBox_Sorting_SymbolBegin.Text = this.translate_text(this.sorting_Usl_One_curr._symb_ot, true);
    }
    if (this.sorting_Usl_One_curr._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola_s_konca)
    {
      this.radioButton_Sorting_OtSymbolNumbEnd.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_ot;
      this.comboBox_Sorting_SymbolBegin.Enabled = true;
      this.comboBox_Sorting_SymbolBegin.Text = this.translate_text(this.sorting_Usl_One_curr._symb_ot, true);
    }
    if (this.sorting_Usl_One_curr._endSravn == Vedomost_VB.EndSravn.Do_end)
      this.radioButton_Sorting_DoEnd.Checked = true;
    if (this.sorting_Usl_One_curr._endSravn == Vedomost_VB.EndSravn.Skolko)
    {
      this.radioButton_Sorting_DoBukvyNumb.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_do;
    }
    if (this.sorting_Usl_One_curr._endSravn == Vedomost_VB.EndSravn.Do_symbola)
    {
      this.radioButton_Sorting_DoSymbolNumb.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_do;
      this.comboBox_Sorting_SymbolEnd.Enabled = true;
      this.comboBox_Sorting_SymbolEnd.Text = this.translate_text(this.sorting_Usl_One_curr._symb_do, true);
    }
    if (this.sorting_Usl_One_curr._endSravn == Vedomost_VB.EndSravn.Do_symbola_s_konca)
    {
      this.radioButton_Sorting_DoSymbolNumbEnd.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_One_curr._num_symb_do;
      this.comboBox_Sorting_SymbolEnd.Enabled = true;
      this.comboBox_Sorting_SymbolEnd.Text = this.translate_text(this.sorting_Usl_One_curr._symb_do, true);
    }
    if (this.sorting_Usl_One_curr._sravnenie == Vedomost_VB.Sravnenie.Symbol)
      this.radioButton_Sorting_SravnenieSymbol.Checked = true;
    else
      this.radioButton_Sorting_SravnenieNumber.Checked = true;
    if (this.sorting_Usl_One_curr._pustyeStroki == Vedomost_VB.PustyeStroki.Vnathale)
      this.radioButton_Sorting_PustyeStrokiVnathale.Checked = true;
    else
      this.radioButton_Sorting_PustyeStrokiVkonce.Checked = true;
    if (this.sorting_Usl_One_curr._poriadokSortirovki == Vedomost_VB.PoriadokSortirovki.Vozrastanie)
      this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked = true;
    else
      this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Checked = true;
    this.buttonEdit_Sorting_1.Enabled = true;
    this.buttonDelete_Sorting_1.Enabled = true;
  }

  private void Displays_The_Current_Record_Doc()
  {
    if (this.i_sorting_Usl_Doc_One_curr == 0)
      this._btnMoveUp_Sorting.Enabled = false;
    if (this.i_sorting_Usl_Doc_One_curr >= this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel.Count - 1)
      this._btnMoveDown_Sorting.Enabled = false;
    this.listBox_Sorting_List_Ved_Graf.SelectedIndex = -1;
    for (int index = 0; index < this.listBox_Sorting_List_Ved_Graf.Items.Count; ++index)
    {
      if (this.sorting_Usl_Doc_OneGrafa_curr._grafa == (string) this.listBox_Sorting_List_Ved_Graf.Items[index])
      {
        this.listBox_Sorting_List_Ved_Graf.SelectedIndex = index;
        break;
      }
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._beginSravn == Vedomost_VB.BeginSravn.S_begin)
      this.radioButton_Sorting_OtBegin.Checked = true;
    if (this.sorting_Usl_Doc_OneGrafa_curr._beginSravn == Vedomost_VB.BeginSravn.S_pozicii)
    {
      this.radioButton_Sorting_OtBukvyNumb.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_ot;
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola)
    {
      this.radioButton_Sorting_OtSymbolNumb.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_ot;
      this.comboBox_Sorting_SymbolBegin.Enabled = true;
      this.comboBox_Sorting_SymbolBegin.Text = this.translate_text(this.sorting_Usl_Doc_OneGrafa_curr._symb_ot, true);
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._beginSravn == Vedomost_VB.BeginSravn.Ot_symbola_s_konca)
    {
      this.radioButton_Sorting_OtSymbolNumbEnd.Checked = true;
      this.numericUpDown_Sorting_NumberBegin.Enabled = true;
      this.numericUpDown_Sorting_NumberBegin.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_ot;
      this.comboBox_Sorting_SymbolBegin.Enabled = true;
      this.comboBox_Sorting_SymbolBegin.Text = this.translate_text(this.sorting_Usl_Doc_OneGrafa_curr._symb_ot, true);
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._endSravn == Vedomost_VB.EndSravn.Do_end)
      this.radioButton_Sorting_DoEnd.Checked = true;
    if (this.sorting_Usl_Doc_OneGrafa_curr._endSravn == Vedomost_VB.EndSravn.Skolko)
    {
      this.radioButton_Sorting_DoBukvyNumb.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_do;
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._endSravn == Vedomost_VB.EndSravn.Do_symbola)
    {
      this.radioButton_Sorting_DoSymbolNumb.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_do;
      this.comboBox_Sorting_SymbolEnd.Enabled = true;
      this.comboBox_Sorting_SymbolEnd.Text = this.translate_text(this.sorting_Usl_Doc_OneGrafa_curr._symb_do, true);
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._endSravn == Vedomost_VB.EndSravn.Do_symbola_s_konca)
    {
      this.radioButton_Sorting_DoSymbolNumbEnd.Checked = true;
      this.numericUpDown_Sorting_NumberEnd.Enabled = true;
      this.numericUpDown_Sorting_NumberEnd.Value = (Decimal) this.sorting_Usl_Doc_OneGrafa_curr._num_symb_do;
      this.comboBox_Sorting_SymbolEnd.Enabled = true;
      this.comboBox_Sorting_SymbolEnd.Text = this.translate_text(this.sorting_Usl_Doc_OneGrafa_curr._symb_do, true);
    }
    if (this.sorting_Usl_Doc_OneGrafa_curr._sravnenie == Vedomost_VB.Sravnenie.Symbol)
      this.radioButton_Sorting_SravnenieSymbol.Checked = true;
    else
      this.radioButton_Sorting_SravnenieNumber.Checked = true;
    if (this.sorting_Usl_Doc_OneGrafa_curr._pustyeStroki == Vedomost_VB.PustyeStroki.Vnathale)
      this.radioButton_Sorting_PustyeStrokiVnathale.Checked = true;
    else
      this.radioButton_Sorting_PustyeStrokiVkonce.Checked = true;
    if (this.sorting_Usl_Doc_OneGrafa_curr._poriadokSortirovki == Vedomost_VB.PoriadokSortirovki.Vozrastanie)
      this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked = true;
    else
      this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Checked = true;
    this.buttonEdit_Sorting_1.Enabled = true;
    this.buttonDelete_Sorting_1.Enabled = true;
  }

  /// <summary> Преобразование строк для Combobox и обратно </summary>
  /// <param textFromColumn="s1"></param>
  /// <returns></returns>
  private string translate_text(string s1, bool tuda)
  {
    if (s1 == "")
      return "";
    string str;
    if (tuda)
    {
      switch (s1)
      {
        case "":
          str = "(без пробела)";
          break;
        case "\r\n":
          str = "(перенос)";
          break;
        case " ":
          str = "  (пробел)";
          break;
        case "*":
          str = "* (звездочка)";
          break;
        case ",":
          str = ", (запятая)";
          break;
        case "-":
          str = "- (минус)";
          break;
        case ".":
          str = ". (точка)";
          break;
        case " ":
          str = "(неразрывный пробел)";
          break;
        case "–":
          str = "(неразрывный дефис)";
          break;
        default:
          str = s1;
          break;
      }
    }
    else
    {
      switch (s1)
      {
        case "  (пробел)":
          str = " ";
          break;
        case "(без пробела)":
          str = "";
          break;
        case "(неразрывный дефис)":
          str = "–";
          break;
        case "(неразрывный пробел)":
          str = " ";
          break;
        case "(перенос)":
          str = "\r\n";
          break;
        case "* (звездочка)":
          str = "*";
          break;
        case ", (запятая)":
          str = ",";
          break;
        case "- (минус)":
          str = "-";
          break;
        case ". (точка)":
          str = ".";
          break;
        default:
          str = s1;
          break;
      }
    }
    return str;
  }

  /// <summary> Нажатие клавиши DEL </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_Sorting_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.typeSortRec != A_NastrVed.TypeSortRec.Info || e.KeyCode != Keys.Delete)
      return;
    this.buttonDelete_Sorting_1_Click((object) null, (EventArgs) null);
  }

  private void dataGridView_Sorting_Doc_KeyDown(object sender, KeyEventArgs e)
  {
    if (this.typeSortRec != A_NastrVed.TypeSortRec.Info || e.KeyCode != Keys.Delete)
      return;
    this.buttonDelete_Sorting_1_Click((object) null, (EventArgs) null);
  }

  /// <summary> Нажатие кнопки Вверх </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void _btnMoveUp_Sorting_Click(object sender, EventArgs e)
  {
    Vedomost_VB.Sorting_Usl_One sorting_Usl_One = this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One[this.i_sorting_Usl_One_curr];
    int sortingUslOneCurr = this.i_sorting_Usl_One_curr;
    int numCurrentSorting = this.rowNumCurrent_Sorting;
    this.dataGridView_Sorting_Curr.Rows.RemoveAt(this.rowNumCurrent_Sorting);
    this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Remove(this.sorting_Usl_One_curr);
    this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Insert(sortingUslOneCurr - 1, sorting_Usl_One);
    string[] strArray = this.drawInfoString(sorting_Usl_One);
    this.dataGridView_Sorting_Curr.Rows.Insert(numCurrentSorting - 1, (object[]) strArray);
    this.SelectDataGridView_Sorting_Row(numCurrentSorting - 1);
    this.dataGridView_Sorting_Curr.Rows[numCurrentSorting - 1].Cells[0].Value = sorting_Usl_One._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
    this.typeSortRec = A_NastrVed.TypeSortRec.Info;
    for (int index = 0; index < this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_One sortingUslOne = this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One[index];
    }
    this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
    this.Displays_The_Current_Record();
    this.ModifiedAll(true);
    this.IsModified_Page_Sortings = true;
  }

  /// <summary> Нажатие кнопки Вниз </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void _btnMoveDown_Sorting_Click(object sender, EventArgs e)
  {
    Vedomost_VB.Sorting_Usl_One sorting_Usl_One = this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One[this.i_sorting_Usl_One_curr];
    int sortingUslOneCurr = this.i_sorting_Usl_One_curr;
    int numCurrentSorting = this.rowNumCurrent_Sorting;
    this.dataGridView_Sorting_Curr.Rows.RemoveAt(this.rowNumCurrent_Sorting);
    this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Remove(this.sorting_Usl_One_curr);
    this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Insert(sortingUslOneCurr + 1, sorting_Usl_One);
    string[] strArray = this.drawInfoString(sorting_Usl_One);
    this.dataGridView_Sorting_Curr.Rows.Insert(numCurrentSorting + 1, (object[]) strArray);
    this.SelectDataGridView_Sorting_Row(numCurrentSorting + 1);
    this.dataGridView_Sorting_Curr.Rows[numCurrentSorting + 1].Cells[0].Value = sorting_Usl_One._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
    this.typeSortRec = A_NastrVed.TypeSortRec.Info;
    for (int index = 0; index < this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Count; ++index)
    {
      Vedomost_VB.Sorting_Usl_One sortingUslOne = this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One[index];
    }
    this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
    this.Displays_The_Current_Record();
    this.ModifiedAll(true);
    this.IsModified_Page_Sortings = true;
  }

  private void radioButton_Sorting_OtBegin_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberBegin.Value = 1M;
    this.numericUpDown_Sorting_NumberBegin.Enabled = false;
    this.comboBox_Sorting_SymbolBegin.Text = "";
    this.comboBox_Sorting_SymbolBegin.Enabled = false;
  }

  private void radioButton_Sorting_OtBukvyNumb_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberBegin.Enabled = true;
    this.comboBox_Sorting_SymbolBegin.Text = "";
    this.comboBox_Sorting_SymbolBegin.Enabled = false;
  }

  private void radioButton_Sorting_OtSymbolNumb_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberBegin.Enabled = true;
    this.comboBox_Sorting_SymbolBegin.Enabled = true;
  }

  private void radioButton_Sorting_OtSymbolNumbEnd_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberBegin.Enabled = true;
    this.comboBox_Sorting_SymbolBegin.Enabled = true;
  }

  private void radioButton_Sorting_DoEnd_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberEnd.Value = 1M;
    this.numericUpDown_Sorting_NumberEnd.Enabled = false;
    this.comboBox_Sorting_SymbolEnd.Text = "";
    this.comboBox_Sorting_SymbolEnd.Enabled = false;
  }

  private void radioButton_Sorting_DoBukvyNumb_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberEnd.Enabled = true;
    this.comboBox_Sorting_SymbolEnd.Text = "";
    this.comboBox_Sorting_SymbolEnd.Enabled = false;
  }

  private void radioButton_Sorting_DoSymbolNumb_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberEnd.Enabled = true;
    this.comboBox_Sorting_SymbolEnd.Enabled = true;
  }

  private void radioButton_Sorting_DoSymbolNumbEnd_MouseClick(object sender, MouseEventArgs e)
  {
    this.numericUpDown_Sorting_NumberEnd.Enabled = true;
    this.comboBox_Sorting_SymbolEnd.Enabled = true;
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonEdit_Sorting_1_Click(object sender, EventArgs e)
  {
    AvsRowAttributeInfo specRowAttributeInfo = (AvsRowAttributeInfo) null;
    string grafa = "";
    string[] strArray;
    if (!this.isSortDoc)
    {
      if (!this.edit_sorting_Usl_One(this.sorting_Usl_One_curr, out specRowAttributeInfo))
        return;
      strArray = this.drawInfoString(this.sorting_Usl_One_curr);
    }
    else
    {
      if (!this.edit_sorting_Usl_Doc_One(this.sorting_Usl_Doc_OneGrafa_curr, out grafa))
        return;
      strArray = this.drawInfoString_Doc(this.sorting_Usl_Doc_OneGrafa_curr);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Sortings = true;
    this.dataGridView_Sorting_Curr.Rows.Insert(this.rowNumCurrent_Sorting, (object[]) strArray);
    if (this.dataGridView_Sorting_Curr.RowCount - this.rowNumCurrent_Sorting > 1)
    {
      this.dataGridView_Sorting_Curr.Rows.RemoveAt(this.rowNumCurrent_Sorting);
      --this.rowNumCurrent_Sorting;
    }
    else
      this.dataGridView_Sorting_Curr.Rows.RemoveAt(this.rowNumCurrent_Sorting);
    this.SelectDataGridView_Sorting_Row(this.rowNumCurrent_Sorting);
    this.dataGridView_Sorting_Curr.Rows[this.rowNumCurrent_Sorting].Cells[0].Value = this.isSortDoc ? (this.sorting_Usl_Doc_OneGrafa_curr._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1]) : (this.sorting_Usl_One_curr._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1]);
    this.Displays_The_Current_Record();
    if (!this.isSortDoc)
    {
      if (Vedomost_VB_Static.Find_Id_In_List_Ved_Id(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.sorting_Usl_One_curr._objectType))
        return;
      int isUzeEst = -1;
      this.Add_Id_To_list_Ved_ID(specRowAttributeInfo, out isUzeEst);
    }
    else
    {
      if (Vedomost_VB_Static.Find_In_ListBox(this.listBox_Sorting_List_Ved_Graf, this.sorting_Usl_Doc_OneGrafa_curr._grafa))
        return;
      int isUzeEst = -1;
      this.Add_Id_To_list_Ved_ID(specRowAttributeInfo, out isUzeEst);
    }
  }

  /// <summary> Кнопка ДОБАВИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonAdd_Sorting_1_Click(object sender, EventArgs e)
  {
    AvsRowAttributeInfo specRowAttributeInfo = (AvsRowAttributeInfo) null;
    string grafa = "";
    Vedomost_VB.Sorting_Usl_One sorting_Usl_One = new Vedomost_VB.Sorting_Usl_One();
    Vedomost_VB.Sorting_Usl_Doc_OneGrafa sortingUslDocOneGrafa = new Vedomost_VB.Sorting_Usl_Doc_OneGrafa();
    string[] strArray;
    if (!this.isSortDoc)
    {
      if (!this.edit_sorting_Usl_One(sorting_Usl_One, out specRowAttributeInfo))
        return;
      strArray = this.drawInfoString(sorting_Usl_One);
    }
    else
    {
      if (!this.edit_sorting_Usl_Doc_One(sortingUslDocOneGrafa, out grafa))
        return;
      strArray = this.drawInfoString_Doc(sortingUslDocOneGrafa);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Sortings = true;
    ++this.rowNumCurrent_Sorting;
    this.dataGridView_Sorting_Curr.Rows.Insert(this.rowNumCurrent_Sorting, (object[]) strArray);
    if (!this.isSortDoc)
    {
      ++this.i_sorting_Usl_One_curr;
      if (this.sorting_Usl_OneRazdel_curr == null)
      {
        this.sorting_Usl_OneRazdel_curr = new Vedomost_VB.Sorting_Usl_OneRazdel();
        this.sorting_Usl_OneRazdel_curr._razdelNum = 1L;
        this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One = new List<Vedomost_VB.Sorting_Usl_One>();
        this._one_Ved_Nastr_Tmp._sorting_Usl.Sorting_Usl_VedOsn._list_sorting_Usl_OneRazdel.Add(this.sorting_Usl_OneRazdel_curr);
      }
      this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Insert(this.i_sorting_Usl_One_curr, sorting_Usl_One);
      this.SelectDataGridView_Sorting_Row(this.rowNumCurrent_Sorting);
      this.dataGridView_Sorting_Curr.Rows[this.rowNumCurrent_Sorting].Cells[0].Value = sorting_Usl_One._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
      this.typeSortRec = A_NastrVed.TypeSortRec.Info;
      this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
      this.Displays_The_Current_Record();
      if (Vedomost_VB_Static.Find_Id_In_List_Ved_Id(this._one_Ved_Nastr_Tmp._list_Ved_ID, sorting_Usl_One._objectType))
        return;
      int isUzeEst = -1;
      this.Add_Id_To_list_Ved_ID(specRowAttributeInfo, out isUzeEst);
    }
    else
    {
      ++this.i_sorting_Usl_Doc_One_curr;
      if (this.sorting_Usl_Doc_OneRazdel_curr == null)
      {
        this.sorting_Usl_Doc_OneRazdel_curr = new Vedomost_VB.Sorting_Usl_Doc_OneRazdel();
        this.sorting_Usl_Doc_OneRazdel_curr._razdelNum = 1L;
        this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel = new List<Vedomost_VB.Sorting_Usl_Doc_OneGrafa>();
        this._one_Ved_Nastr_Tmp._sorting_Usl_Doc._list_sorting_Usl_Doc.Add(this.sorting_Usl_Doc_OneRazdel_curr);
      }
      this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel.Insert(this.i_sorting_Usl_Doc_One_curr, sortingUslDocOneGrafa);
      this.SelectDataGridView_Sorting_Row(this.rowNumCurrent_Sorting);
      this.dataGridView_Sorting_Curr.Rows[this.rowNumCurrent_Sorting].Cells[0].Value = sortingUslDocOneGrafa._poriadokSortirovki != Vedomost_VB.PoriadokSortirovki.Vozrastanie ? (object) this.imageListSort.Images[2] : (object) this.imageListSort.Images[1];
      this.typeSortRec = A_NastrVed.TypeSortRec.Info;
      this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
      this.Displays_The_Current_Record();
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonDelete_Sorting_1_Click(object sender, EventArgs e)
  {
    this.dataGridView_Sorting_Curr.Rows.RemoveAt(this.rowNumCurrent_Sorting);
    if (!this.isSortDoc)
    {
      if (this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Count == 1)
      {
        this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Clear();
        this.sorting_Usl_One_curr = (Vedomost_VB.Sorting_Usl_One) null;
      }
      else
        this.sorting_Usl_OneRazdel_curr._list_sorting_Usl_One.Remove(this.sorting_Usl_One_curr);
      this.Usl_For_Rownumber(this.rowNumCurrent_Sorting);
      this.Displays_The_Current_Record();
    }
    else
    {
      if (this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel.Count == 1)
      {
        this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel.Clear();
        this.sorting_Usl_Doc_OneGrafa_curr = (Vedomost_VB.Sorting_Usl_Doc_OneGrafa) null;
      }
      else
        this.sorting_Usl_Doc_OneRazdel_curr._list_sorting_Usl_Doc_OneRazdel.Remove(this.sorting_Usl_Doc_OneGrafa_curr);
      this.Usl_For_Rownumber_Doc(this.rowNumCurrent_Sorting);
      this.Displays_The_Current_Record();
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Sortings = true;
  }

  /// <summary> Сохранение редактирования страницы "Сортировка" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Sorting()
  {
  }

  /// <summary> Рисование страницы РАЗДЕЛЫ </summary>
  private void Draw_Page_Razdels()
  {
    this.rowNumCurrent_Razdels = -1;
    this.rowNumCurrent_PodRazdels = -1;
    this.rowNumPrevision_Razdels = -1;
    this.isVydelenieRazdelaAuto = true;
    this.isVydeleniePodRazdelaAuto = true;
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed != null)
    {
      for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
      {
        Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
        this.Razdels_dataGridViewListRazdels.Rows.Add((object[]) new string[2]
        {
          oneRazdelVed._razdelVed.ToString(),
          oneRazdelVed._name
        });
        if (index == 0)
        {
          this.oneRazdelVed_Curr = oneRazdelVed;
          this.oneRazdelVed_Prevision = (Vedomost_VB.OneRazdelVed) null;
          this.rowNumCurrent_Razdels = 0;
          this.rowNumPrevision_Razdels = 0;
        }
      }
      this.SelectDataGridView_Razdels_Row(0);
    }
    this.Draw_Table_PodRazdel(this.oneRazdelVed_Curr);
    if (this.Razdels_dataGridViewListRazdels.Rows.Count < 2)
    {
      this.rowNumCurrent_Razdels = -1;
      this.rowNumCurrent_PodRazdels = -1;
      this.rowNumPrevision_Razdels = -1;
      this.oneRazdelVed_Curr = (Vedomost_VB.OneRazdelVed) null;
      this.oneRazdelVed_Prevision = (Vedomost_VB.OneRazdelVed) null;
      this.buttonDelete_Razdel.Enabled = false;
    }
    this.isVydelenieRazdelaAuto = false;
    this.isVydeleniePodRazdelaAuto = false;
    this.IsModified_Page_PodRazdels = false;
    this.is_Extended_List_Names_Pages_ByTemplate = Vedomost_VB_Static.Is_Extended_List_Names_Pages_ByTemplate((DocumentTreeNode) this.imDocument_template_Vyvod);
    if (this.is_Extended_List_Names_Pages_ByTemplate)
    {
      this.groupBox_Conformity_Name_Page_for_Razdel.Visible = true;
      this.Draw_dataGridView_RazdelVedAndNamePage();
      this.Draw_dataGridView_NamePage();
    }
    else
    {
      this.dataGridView_NamePage.Rows.Clear();
      this.dataGridView_RazdelVedAndNamePage.Rows.Clear();
      this.groupBox_Conformity_Name_Page_for_Razdel.Visible = false;
    }
  }

  /// <summary> Прорисовка списка соответсвия Разделов и страниц шаблона (Внизу справа) </summary>
  private void Draw_dataGridView_RazdelVedAndNamePage()
  {
    this.dataGridView_RazdelVedAndNamePage.Rows.Clear();
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      this.dataGridView_RazdelVedAndNamePage.Rows.Add((object[]) new string[2]
      {
        oneRazdelVed._name,
        !string.IsNullOrEmpty(oneRazdelVed._namePage) ? oneRazdelVed._namePage : this.name_Page_Common
      });
    }
  }

  /// <summary> Прорисовка списка имен страниц шаблона (Внизу справа) </summary>
  private void Draw_dataGridView_NamePage()
  {
    this.dataGridView_NamePage.Rows.Clear();
    List<string> namesPagesTemlate = Vedomost_VB_Static.Get_List_Names_Pages_Temlate((DocumentTreeNode) this.imDocument_template_Vyvod);
    string str1 = "";
    bool flag = false;
    for (int index = 0; index < namesPagesTemlate.Count; ++index)
    {
      string str2 = namesPagesTemlate[index];
      switch (str2)
      {
        case "Титульный лист":
        case "Лист регистрации изменений":
          continue;
        case "Заглавный лист":
          if (index < namesPagesTemlate.Count - 1)
          {
            string str3 = namesPagesTemlate[index + 1];
            if (str3 == "Следующая страница" && str2 == "Заглавный лист")
            {
              str2 = $"{str2} или {str3}";
              flag = true;
              break;
            }
            str1 = "";
            break;
          }
          break;
      }
      if (!(str2 == "Следующая страница" & flag))
        this.dataGridView_NamePage.Rows.Add((object[]) new string[1]
        {
          str2
        });
    }
  }

  /// <summary> Имя страницы шаблона заносим в список соответствия </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Add_NamePage_Click(object sender, EventArgs e)
  {
    int rowIndex = this.dataGridView_RazdelVedAndNamePage.CurrentCell.RowIndex;
    string str = this.dataGridView_NamePage.Rows[this.dataGridView_NamePage.CurrentCell.RowIndex].Cells[0].Value.ToString();
    this.dataGridView_RazdelVedAndNamePage.Rows[rowIndex].Cells[1].Value = (object) str;
    this._one_Ved_Nastr_Tmp._list_RazdelsVed[rowIndex]._namePage = str == "Заглавный лист" || str == "Следующая страница" || str == "Заглавный лист или Следующая страница" ? "" : str;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
  }

  /// <summary> DragAndDrop Имени страницы шаблона </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_NamePage_MouseDown(object sender, MouseEventArgs e)
  {
    int num = (int) this.dataGridView_NamePage.DoDragDrop((object) this.dataGridView_NamePage.CurrentCell.RowIndex, DragDropEffects.Copy);
  }

  private void dataGridView_RazdelVedAndNamePage_DragOver(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.Copy;
  }

  /// <summary> Завершение DragAndDrop Имени страницы шаблона </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_RazdelVedAndNamePage_DragDrop(object sender, DragEventArgs e)
  {
    string str = this.dataGridView_NamePage.Rows[Convert.ToInt32(e.Data.GetData(System.Type.GetType("System.Int32")))].Cells[0].Value.ToString();
    int rowIndex1 = this.dataGridView_RazdelVedAndNamePage.CurrentCell.RowIndex;
    Point client = this.dataGridView_RazdelVedAndNamePage.PointToClient(new Point(e.X, e.Y));
    DataGridView.HitTestInfo hitTestInfo = this.dataGridView_RazdelVedAndNamePage.HitTest(client.X, client.Y);
    if (hitTestInfo.Type != DataGridViewHitTestType.Cell)
      return;
    int rowIndex2 = hitTestInfo.RowIndex;
    this.dataGridView_RazdelVedAndNamePage.Rows[rowIndex2].Cells[1].Value = (object) str;
    this._one_Ved_Nastr_Tmp._list_RazdelsVed[rowIndex2]._namePage = str == "Заглавный лист" || str == "Следующая страница" || str == "Заглавный лист или Следующая страница" ? "" : str;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
  }

  /// <summary> Рисование таблицы подразделов </summary>
  /// <param textFromColumn="oneRazdelVed"></param>
  private void Draw_Table_PodRazdel(Vedomost_VB.OneRazdelVed oneRazdelVed)
  {
    if (oneRazdelVed == null || oneRazdelVed._list_onePodRazdels == null || oneRazdelVed._list_onePodRazdels.Count == 0)
    {
      this.is_Podrazdel_Auto = true;
      this.Razdels_dataGridViewListPodRazdels.Rows.Clear();
      this.Razdels_groupBoxListPodRazdelov.Visible = false;
      this.checkBox_Razdel_PodRazdel.Checked = false;
      this.buttonAdd_PodRazdel.Visible = false;
      this.buttonDelete_PodRazdel.Visible = false;
      this.is_Podrazdel_Auto = false;
    }
    else
    {
      this.checkBox_Razdel_PodRazdel.Checked = true;
      this.Razdels_groupBoxListPodRazdelov.Visible = true;
      this.is_Podrazdel_Auto = true;
      this.buttonAdd_PodRazdel.Visible = true;
      this.buttonDelete_PodRazdel.Visible = true;
      this.Razdels_dataGridViewListPodRazdels.Rows.Clear();
      for (int index = 0; index < this.oneRazdelVed_Curr._list_onePodRazdels.Count; ++index)
      {
        Vedomost_VB.OnePodRazdelVed listOnePodRazdel = oneRazdelVed._list_onePodRazdels[index];
        this.Razdels_dataGridViewListPodRazdels.Rows.Add((object[]) new string[2]
        {
          listOnePodRazdel._podRazdelVed.ToString(),
          listOnePodRazdel._name
        });
      }
      this.is_Podrazdel_Auto = false;
      this.SelectDataGridView_PodRazdels_Row(0);
    }
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param textFromColumn="rowNum"></param>
  public void SelectDataGridView_Razdels_Row(int rowNum)
  {
    this.SelectDataGridView_Razdels_Cell(rowNum, 0);
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param textFromColumn="rowNum"></param>
  public void SelectDataGridView_PodRazdels_Row(int rowNum)
  {
    this.SelectDataGridView_PodRazdels_Cell(rowNum, 0);
  }

  /// <summary> Выделить ячейку </summary>
  /// <param textFromColumn="rowIndex"></param>
  /// <param textFromColumn="cellIndex"></param>
  public void SelectDataGridView_Razdels_Cell(int rowIndex, int cellIndex)
  {
    this.Razdels_dataGridViewListRazdels.CurrentCell = this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[0];
    this.Razdels_dataGridViewListRazdels.CurrentCell.Selected = true;
  }

  /// <summary> Выделить ячейку </summary>
  /// <param textFromColumn="rowIndex"></param>
  /// <param textFromColumn="cellIndex"></param>
  public void SelectDataGridView_PodRazdels_Cell(int rowIndex, int cellIndex)
  {
    this.Razdels_dataGridViewListPodRazdels.CurrentCell = this.Razdels_dataGridViewListPodRazdels.Rows[rowIndex].Cells[0];
    this.Razdels_dataGridViewListPodRazdels.CurrentCell.Selected = true;
  }

  /// <summary> Сохранение редактирования страницы "Разделы" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Razdels()
  {
  }

  /// <summary> Сохранение результатов редактирования таблицы Подразделы </summary>
  /// <returns></returns>
  private List<Vedomost_VB.OnePodRazdelVed> Saving_podrazdels()
  {
    if (this.oneRazdelVed_Curr == null)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    if (this.Razdels_dataGridViewListPodRazdels.Rows.Count == 0)
      return (List<Vedomost_VB.OnePodRazdelVed>) null;
    if (this.oneRazdelVed_Curr._list_onePodRazdels == null)
      this.oneRazdelVed_Curr._list_onePodRazdels = new List<Vedomost_VB.OnePodRazdelVed>();
    else
      this.oneRazdelVed_Curr._list_onePodRazdels.Clear();
    for (int index = 0; index < this.Razdels_dataGridViewListPodRazdels.RowCount - 1; ++index)
    {
      string str1 = "";
      string str2 = "";
      DataGridViewCell cell1 = this.Razdels_dataGridViewListPodRazdels.Rows[index].Cells[0];
      DataGridViewCell cell2 = this.Razdels_dataGridViewListPodRazdels.Rows[index].Cells[1];
      if (cell1.Value != null)
        str1 = cell1.Value.ToString();
      if (cell2.Value != null)
        str2 = cell2.Value.ToString();
      Vedomost_VB.OnePodRazdelVed onePodRazdelVed = new Vedomost_VB.OnePodRazdelVed();
      string source = str1.Trim();
      string str3 = str2.Trim();
      onePodRazdelVed._podRazdelVed = 0;
      if (source != "")
        onePodRazdelVed._podRazdelVed = !source.All<char>(new Func<char, bool>(char.IsDigit)) ? 0 : (int) Convert.ToInt16(source);
      onePodRazdelVed._name = str3;
      if (string.IsNullOrEmpty(onePodRazdelVed._name))
        this.oneRazdelVed_Curr._list_onePodRazdels.Add(onePodRazdelVed);
    }
    return this.oneRazdelVed_Curr._list_onePodRazdels;
  }

  /// <summary> Контроль значения ячеек разделов при выходе из них </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListRazdels_CellValidating(
    object sender,
    DataGridViewCellValidatingEventArgs e)
  {
    if (this.oneRazdelVed_Curr == null || this.rowNumCurrent_Razdels < 0 || this.rowNumCurrent_Razdels == this.Razdels_dataGridViewListRazdels.Rows.Count - 1)
      return;
    string textFromCell = e.FormattedValue.ToString().Trim();
    string text = "";
    if (e.ColumnIndex == 0)
      text = this.Check_Cell_Integer(textFromCell, e.RowIndex, 1000, "раздела");
    if (e.ColumnIndex == 1)
      text = this.Check_Cell_Name(textFromCell, e.RowIndex);
    if (!string.IsNullOrEmpty(text))
    {
      this.Razdels_dataGridViewListRazdels.Rows[e.RowIndex].ErrorText = text;
      int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
      this.Razdels_dataGridViewListRazdels.Rows[e.RowIndex].ErrorText = "";
  }

  private string Check_Cell_Integer(string textFromCell, int rowIndex, int nMax, string sRazdel)
  {
    if (textFromCell == "")
      return "Номер не заполнен";
    if (!textFromCell.All<char>(new Func<char, bool>(char.IsDigit)))
      return "Номер не целое число";
    int int32 = Convert.ToInt32(textFromCell);
    return int32 > nMax || int32 < 1 ? $"Номер {sRazdel} должен быть целым числом более 0 и не более {nMax.ToString()}" : "";
  }

  /// <summary> Проверка ячейки на НАИМЕНОВАНИЕ </summary>
  /// <param name="textFromCell"></param>
  /// <param name="rowIndex"></param>
  /// <returns></returns>
  private string Check_Cell_Name(string textFromCell, int rowIndex)
  {
    return textFromCell == "" ? "Наименование не заполнено" : "";
  }

  /// <summary> Контроль значения ячеек ПОДразделов при выходе из них. И СОХРАНЕНИЕ РЕЗУЛЬТАТОВ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListPodRazdels_CellValidating(
    object sender,
    DataGridViewCellValidatingEventArgs e)
  {
    if (this.onePodRazdelVed_Curr == null || this.rowNumCurrent_PodRazdels < 0 || this.rowNumCurrent_PodRazdels == this.Razdels_dataGridViewListPodRazdels.Rows.Count - 1)
      return;
    string textFromCell = e.FormattedValue.ToString().Trim();
    string text = "";
    if (e.ColumnIndex == 0)
      text = this.Check_Cell_Integer(textFromCell, e.RowIndex, 1000, "подраздела");
    if (e.ColumnIndex == 1)
      text = this.Check_Cell_Name(textFromCell, e.RowIndex);
    if (!string.IsNullOrEmpty(text))
    {
      this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = text;
      int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
      this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = "";
  }

  /// <summary> Выход из строки Раздела </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListRazdels_RowValidating(
    object sender,
    DataGridViewCellCancelEventArgs e)
  {
    this.Check_List_OnePodRadels(this.oneRazdelVed_Curr);
  }

  /// <summary> Контроль страницы Разделов (Подразделов) </summary>
  /// <returns></returns>
  private bool Razdels_Control_Main()
  {
    bool flag = this.Razdels_Control(this.Razdels_dataGridViewListRazdels);
    if (flag || !this.checkBox_Razdel_PodRazdel.Checked)
      return flag;
    flag = this.Razdels_Control(this.Razdels_dataGridViewListPodRazdels);
    return flag;
  }

  /// <summary> Очистка таблицы ПОДразделов от "плохих" строк</summary>
  /// <param name="oneRazdelVed"></param>
  private void Check_List_OnePodRadels(Vedomost_VB.OneRazdelVed oneRazdelVed)
  {
    if (oneRazdelVed == null || oneRazdelVed._list_onePodRazdels == null)
      return;
    for (int index = oneRazdelVed._list_onePodRazdels.Count - 1; index >= 0; --index)
    {
      Vedomost_VB.OnePodRazdelVed listOnePodRazdel = oneRazdelVed._list_onePodRazdels[index];
      if (string.IsNullOrEmpty(listOnePodRazdel._name) || listOnePodRazdel._podRazdelVed < 1 || listOnePodRazdel._podRazdelVed > 100)
        oneRazdelVed._list_onePodRazdels.RemoveAt(index);
    }
    if (oneRazdelVed._list_onePodRazdels.Count != 0)
      return;
    oneRazdelVed._list_onePodRazdels = (List<Vedomost_VB.OnePodRazdelVed>) null;
  }

  /// <summary> Выход из строки подраздела </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListPodRazdels_RowValidating(
    object sender,
    DataGridViewCellCancelEventArgs e)
  {
    this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = string.Empty;
    DataGridViewRow row = this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex];
    DataGridViewCell cell1 = row.Cells[0];
    DataGridViewCell cell2 = row.Cells[1];
    if (this.rowNumCurrent_PodRazdels < 0 || this.rowNumCurrent_PodRazdels == this.Razdels_dataGridViewListPodRazdels.Rows.Count - 1)
      return;
    string str1 = "";
    int num = 0;
    if (cell1.Value != null)
      str1 = cell1.Value.ToString();
    string source = str1.Trim();
    if (source == "")
    {
      this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = "Значение в ячейке не должно быть пустым";
    }
    else
    {
      bool flag = source.All<char>(new Func<char, bool>(char.IsDigit));
      if (source == "" || !flag)
      {
        this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = "Номер подраздела должен быть целым числом";
      }
      else
      {
        num = Convert.ToInt32(source);
        if (num > 100 || num < 1)
          this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = "Номер подраздела должен быть целым числом более 0 и не более 100";
      }
    }
    if (!string.IsNullOrEmpty(this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText))
      return;
    string str2 = "";
    if (cell2.Value != null)
      str2 = cell2.Value.ToString();
    string str3 = str2.Trim();
    if (str3 == "")
      this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText = "Наименование подраздела не должно быть пустым";
    if (!string.IsNullOrEmpty(this.Razdels_dataGridViewListPodRazdels.Rows[e.RowIndex].ErrorText) || this.onePodRazdelVed_Curr == null)
      return;
    this.onePodRazdelVed_Curr._podRazdelVed = num;
    this.onePodRazdelVed_Curr._name = str3;
  }

  /// <summary> Контроль таблицы Разделов (Подразделов) </summary>
  /// <param name="dataGridView"></param>
  /// <returns></returns>
  private bool Razdels_Control(DataGridView dataGridView)
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    string str1;
    int num1;
    if (dataGridView == this.Razdels_dataGridViewListRazdels)
    {
      str1 = "разделов";
      num1 = 1000;
    }
    else
    {
      str1 = "подразделов";
      num1 = 100;
    }
    for (int index = 0; index < dataGridView.RowCount - 1; ++index)
    {
      DataGridViewCell cell1 = dataGridView.Rows[index].Cells[0];
      string source = cell1.Value == null ? "" : cell1.Value.ToString();
      bool flag4 = source.All<char>(new Func<char, bool>(char.IsDigit));
      if (source == "" || !flag4)
      {
        flag2 = true;
      }
      else
      {
        int int32 = Convert.ToInt32(source);
        if (int32 > num1 || int32 < 1)
          flag2 = true;
      }
      DataGridViewCell cell2 = dataGridView.Rows[index].Cells[1];
      string str2 = cell2.Value == null ? "" : cell2.Value.ToString();
      if (source == "")
        flag2 = true;
      if (str2 == "")
        flag3 = true;
    }
    if (!(flag2 | flag3))
      return flag1;
    string text = $"Есть ошибки в списке {str1}:";
    if (flag2)
      text = $"{text}\r\n\r\nНомер должен быть целым числом более 0 и не более {num1.ToString()}";
    if (flag3)
      text += "\r\n\r\nТекст заголовка не должен быть пустым";
    this.tabControl_Nastr.SelectTab(this.tabPage_Razdels);
    int num2 = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    return true;
  }

  /// <summary> Произошло изменение ячейки раздела Мы только делаем ModifiedAll(true) </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListRazdels_CellValueChanged(
    object sender,
    DataGridViewCellEventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
  }

  /// <summary> Произошло изменение ячейки подраздела </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListPodRazdels_CellValueChanged(
    object sender,
    DataGridViewCellEventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
    this.IsModified_Page_PodRazdels = true;
  }

  /// <summary> Контроль страницы "Разделы" </summary>
  /// <returns></returns>
  private bool Razdels_dataGridViewListRazdels_Control()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < this.Razdels_dataGridViewListRazdels.RowCount - 1; ++index)
    {
      DataGridViewCell cell1 = this.Razdels_dataGridViewListRazdels.Rows[index].Cells[0];
      DataGridViewCell cell2 = this.Razdels_dataGridViewListRazdels.Rows[index].Cells[1];
      bool flag4 = cell1.Value != null && cell1.Value.ToString().All<char>(new Func<char, bool>(char.IsDigit));
      string str = cell2.Value == null ? "" : cell2.Value.ToString();
      if (!flag4)
        flag2 = true;
      if (str == "")
        flag3 = true;
    }
    if (flag2 | flag3)
    {
      string text = "Есть ошибки в списке разделов:";
      if (flag2)
        text += "\r\n\r\nНомер раздела должен быть целым числом";
      if (flag3)
        text += "\r\n\r\nНаименование раздела не должно быть пустым";
      this.tabControl_Nastr.SelectTab(this.tabPage_Razdels);
      int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return true;
    }
    if (this.Razdels_dataGridViewListRazdels_ControlData())
      flag1 = true;
    if (this.DuplicateName_Control())
      flag1 = true;
    return flag1;
  }

  /// <summary> Удаление текущей строки </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Delete_Razdels()
  {
    if (this.rowNumCurrent_Razdels < 0 || this.Razdels_dataGridViewListRazdels.Rows.Count < 2 || this.rowNumCurrent_Razdels == this.Razdels_dataGridViewListRazdels.Rows.Count - 1)
      return;
    this._one_Ved_Nastr_Tmp._list_RazdelsVed.Remove(this.oneRazdelVed_Curr);
    this.Razdels_dataGridViewListRazdels.Rows.RemoveAt(this.rowNumCurrent_Razdels);
    this.oneRazdelVed_Curr = (Vedomost_VB.OneRazdelVed) null;
    if (this.Razdels_dataGridViewListRazdels.Rows.Count < 2)
    {
      this.rowNumCurrent_Razdels = -1;
      this.rowNumPrevision_Razdels = -1;
      this.oneRazdelVed_Prevision = (Vedomost_VB.OneRazdelVed) null;
      this.buttonDelete_Razdel.Enabled = false;
    }
    else
      this.Razdels_dataGridViewListRazdels_Izmenenie_RowIndex();
    if (this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count)
      this.oneRazdelVed_Curr = this._one_Ved_Nastr_Tmp._list_RazdelsVed[this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex];
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonDelete_Razdel_Click(object sender, EventArgs e) => this.Delete_Razdels();

  private void buttonDelete_PodRazdel_Click(object sender, EventArgs e)
  {
    if (this.rowNumCurrent_PodRazdels < 0 || this.Razdels_dataGridViewListPodRazdels.Rows.Count < 2 || this.rowNumCurrent_PodRazdels == this.Razdels_dataGridViewListPodRazdels.Rows.Count - 1)
      return;
    this.Razdels_dataGridViewListPodRazdels.Rows.RemoveAt(this.rowNumCurrent_PodRazdels);
    this.oneRazdelVed_Curr._list_onePodRazdels.Remove(this.onePodRazdelVed_Curr);
    this.onePodRazdelVed_Curr = (Vedomost_VB.OnePodRazdelVed) null;
    if (this.Razdels_dataGridViewListPodRazdels.Rows.Count < 2)
    {
      this.rowNumCurrent_PodRazdels = -1;
      this.rowNumPrevision_PodRazdels = -1;
      this.onePodRazdelVed_Prevision = (Vedomost_VB.OnePodRazdelVed) null;
      this.buttonDelete_PodRazdel.Enabled = false;
    }
    else
      this.Razdels_dataGridViewListPodRazdels_Izmenenie_RowIndex();
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
    this.IsModified_Page_PodRazdels = true;
  }

  /// <summary> Нажатие клавиши DEL </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListRazdels_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.Delete_Razdels();
  }

  /// <summary> Выбор ЯЧЕЙКИ в списке разделов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListRazdels_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    if (this.isVydelenieRazdelaAuto)
      return;
    this.rowNumPrevision_Razdels = this.rowNumCurrent_Razdels;
    this.rowNumCurrent_Razdels = this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex;
    this.Razdels_dataGridViewListRazdels_Izmenenie_RowIndex();
    this.Activate_Deactivate_Buttons();
  }

  /// <summary> Включение-выключеник кнопок "Добавить" и "Удалить" </summary>
  private void Activate_Deactivate_Buttons()
  {
    string str1 = "";
    string str2 = "";
    if (this.Razdels_dataGridViewListRazdels.CurrentCell != null)
    {
      int rowIndex = this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex;
      if (this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[0].Value != null)
        str1 = this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[0].Value.ToString();
      if (this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[1].Value != null)
        str2 = this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[1].Value.ToString();
    }
    if (str1 == "" || str2 == "")
      this.buttonAdd_Razdel.Enabled = false;
    else
      this.buttonAdd_Razdel.Enabled = true;
  }

  /// <summary> Включение-выключеник кнопок "Добавить" и "Удалить" </summary>
  private void Activate_Deactivate_Buttons_Podrazdel()
  {
    string str1 = "";
    string str2 = "";
    if (this.Razdels_dataGridViewListPodRazdels.CurrentCell != null)
    {
      int rowIndex = this.Razdels_dataGridViewListPodRazdels.CurrentCell.RowIndex;
      if (this.Razdels_dataGridViewListPodRazdels.Rows[rowIndex].Cells[0].Value != null)
        str1 = this.Razdels_dataGridViewListPodRazdels.Rows[rowIndex].Cells[0].Value.ToString();
      if (this.Razdels_dataGridViewListPodRazdels.Rows[rowIndex].Cells[1].Value != null)
        str2 = this.Razdels_dataGridViewListPodRazdels.Rows[rowIndex].Cells[1].Value.ToString();
    }
    if (str1 == "" || str2 == "")
      this.buttonAdd_PodRazdel.Enabled = false;
    else
      this.buttonAdd_PodRazdel.Enabled = true;
    if (str1 == "" && str2 == "")
      this.buttonDelete_PodRazdel.Enabled = false;
    else
      this.buttonDelete_PodRazdel.Enabled = true;
  }

  /// <summary> Выбор ЯЧЕЙКИ в списке подразделов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListPodRazdels_CellEnter(
    object sender,
    DataGridViewCellEventArgs e)
  {
    if (this.isVydeleniePodRazdelaAuto)
      return;
    this.rowNumPrevision_PodRazdels = this.rowNumCurrent_PodRazdels;
    this.rowNumCurrent_PodRazdels = this.Razdels_dataGridViewListPodRazdels.CurrentCell.RowIndex;
    this.Razdels_dataGridViewListPodRazdels_Izmenenie_RowIndex();
    this.Activate_Deactivate_Buttons_Podrazdel();
  }

  /// <summary> Выбор строки в списке разделов</summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListRazdels_CellClick(object sender, DataGridViewCellEventArgs e)
  {
    if (this.isVydelenieRazdelaAuto)
      return;
    this.rowNumCurrent_Razdels = this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex;
    this.Razdels_dataGridViewListRazdels_Izmenenie_RowIndex();
  }

  /// <summary> Выбор строки в списке подразделов </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void Razdels_dataGridViewListPodRazdels_CellClick(
    object sender,
    DataGridViewCellEventArgs e)
  {
    if (this.isVydeleniePodRazdelaAuto)
      return;
    this.rowNumCurrent_PodRazdels = this.Razdels_dataGridViewListPodRazdels.CurrentCell.RowIndex;
    this.Razdels_dataGridViewListPodRazdels_Izmenenie_RowIndex();
  }

  /// <summary> Изменился номер текущей строки списка разделов </summary>
  private void Razdels_dataGridViewListRazdels_Izmenenie_RowIndex()
  {
    if (this.rowNumCurrent_Razdels == this.rowNumPrevision_Razdels)
      return;
    if (this.rowNumCurrent_Razdels == this.Razdels_dataGridViewListRazdels.Rows.Count - 1)
    {
      this.Draw_Table_PodRazdel((Vedomost_VB.OneRazdelVed) null);
      this.Activate_Deactivate_Buttons();
    }
    else
    {
      this.Activate_Deactivate_Buttons();
      this.oneRazdelVed_Prevision = this.oneRazdelVed_Curr;
      if (this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count > this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex)
        this.oneRazdelVed_Curr = this._one_Ved_Nastr_Tmp._list_RazdelsVed[this.Razdels_dataGridViewListRazdels.CurrentCell.RowIndex];
      this.Draw_Table_PodRazdel(this.oneRazdelVed_Curr);
    }
  }

  /// <summary> Изменился номер текущей строки списка подразделов </summary>
  private void Razdels_dataGridViewListPodRazdels_Izmenenie_RowIndex()
  {
    if (this.rowNumCurrent_PodRazdels == this.rowNumPrevision_PodRazdels)
      return;
    if (this.rowNumCurrent_PodRazdels == this.Razdels_dataGridViewListPodRazdels.Rows.Count - 1 || this.Razdels_dataGridViewListPodRazdels.Rows.Count == 1)
    {
      this.Activate_Deactivate_Buttons_Podrazdel();
    }
    else
    {
      this.Activate_Deactivate_Buttons_Podrazdel();
      this.onePodRazdelVed_Prevision = this.onePodRazdelVed_Curr;
      if (this.oneRazdelVed_Curr._list_onePodRazdels != null && this.oneRazdelVed_Curr._list_onePodRazdels.Count > this.Razdels_dataGridViewListPodRazdels.CurrentCell.RowIndex)
        this.onePodRazdelVed_Curr = this.oneRazdelVed_Curr._list_onePodRazdels[this.Razdels_dataGridViewListPodRazdels.CurrentCell.RowIndex];
    }
    this.rowNumPrevision_PodRazdels = this.rowNumCurrent_PodRazdels;
  }

  /// <summary> checkBox "Подразделы" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void checkBox_Razdel_PodRazdel_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    if (this.checkBox_Razdel_PodRazdel.Checked)
    {
      this.Razdels_groupBoxListPodRazdelov.Visible = true;
      this.buttonAdd_PodRazdel.Visible = true;
      this.buttonDelete_PodRazdel.Visible = true;
      this.Activate_Deactivate_Buttons_Podrazdel();
    }
    else
    {
      this.Razdels_groupBoxListPodRazdelov.Visible = false;
      this.buttonAdd_PodRazdel.Visible = false;
      this.buttonDelete_PodRazdel.Visible = false;
      if (this.oneRazdelVed_Curr._list_onePodRazdels == null)
        return;
      this.Cleaning_Of_Empty_OnePodRazdelVed(this.oneRazdelVed_Curr);
    }
  }

  /// <summary> Добавить строку выше текущей </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonAdd_Razdel_Click(object sender, EventArgs e)
  {
    int y = this.Razdels_dataGridViewListRazdels.CurrentCellAddress.Y;
    string[] strArray = new string[2];
    this.isVydelenieRazdelaAuto = true;
    this.Razdels_dataGridViewListRazdels.Rows.Insert(y, (object[]) strArray);
    this.Razdels_dataGridViewListRazdels.CurrentCell = this.Razdels_dataGridViewListRazdels.Rows[y].Cells[0];
    this.isVydelenieRazdelaAuto = false;
    this.buttonAdd_Razdel.Enabled = false;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
  }

  /// <summary> Нажатие кнопки Добавить Добавить строку выше текущей </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonAdd_PodRazdel_Click(object sender, EventArgs e)
  {
    int y = this.Razdels_dataGridViewListPodRazdels.CurrentCellAddress.Y;
    string[] strArray = new string[2];
    this.isVydeleniePodRazdelaAuto = true;
    this.Razdels_dataGridViewListPodRazdels.Rows.Insert(y, (object[]) strArray);
    this.Razdels_dataGridViewListPodRazdels.CurrentCell = this.Razdels_dataGridViewListPodRazdels.Rows[y].Cells[0];
    this.isVydeleniePodRazdelaAuto = false;
    this.ModifiedAll(true);
    this.IsModified_Page_Razdels = true;
    this.IsModified_Page_PodRazdels = true;
  }

  /// <summary> Контролировать данные таблицы РАЗДЕЛОВ в т.ч. порядок возрастания</summary>
  /// <returns></returns>
  private bool Razdels_dataGridViewListRazdels_ControlData()
  {
    string strB = "";
    int num1 = 0;
    int num2 = 0;
    bool flag = false;
    for (int index = 0; index < this.Razdels_dataGridViewListRazdels.RowCount - 1; ++index)
    {
      if (this.Razdels_dataGridViewListRazdels.Rows[index].Cells[0].Value == null)
      {
        this.SelectRazdels_dataGridViewListRazdelsCell(index, 0);
        int num3 = (int) MessageBox.Show("Номер раздела не задан", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
      if (this.Razdels_dataGridViewListRazdels.Rows[index].Cells[1].Value == null)
      {
        this.SelectRazdels_dataGridViewListRazdelsCell(index, 1);
        int num4 = (int) MessageBox.Show("Наименование раздела не задано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
      string strA = this.Razdels_dataGridViewListRazdels.Rows[index].Cells[0].Value.ToString();
      if (strB != "")
      {
        try
        {
          num1 = Convert.ToInt32(strA);
          num2 = Convert.ToInt32(strB);
        }
        catch
        {
        }
        if (num1 > 0 && num2 > 0)
        {
          if (num2 > num1 || num1 == num2)
          {
            this.SelectRazdels_dataGridViewListRazdelsCell(index, 0);
            int num5 = (int) MessageBox.Show("Номер раздела не в порядке возрастания", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            flag = true;
            break;
          }
        }
        else if (string.Compare(strA, strB, StringComparison.Ordinal) < 0)
        {
          this.SelectRazdels_dataGridViewListRazdelsCell(index, 0);
          int num6 = (int) MessageBox.Show("Номер раздела не в порядке возрастания", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          flag = true;
          break;
        }
      }
      strB = strA;
    }
    return flag;
  }

  /// <summary> Проверка повторения наименования раздела </summary>
  /// <returns></returns>
  private bool DuplicateName_Control()
  {
    for (int index1 = 0; index1 < this.Razdels_dataGridViewListRazdels.RowCount - 2; ++index1)
    {
      if (this.Razdels_dataGridViewListRazdels.Rows[index1].Cells[1].Value == null)
      {
        this.SelectRazdels_dataGridViewListRazdelsCell(index1, 1);
        int num = (int) MessageBox.Show("Наименование раздела не задано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return true;
      }
      string str1 = this.Razdels_dataGridViewListRazdels.Rows[index1].Cells[1].Value.ToString().Trim();
      for (int index2 = index1 + 1; index2 < this.Razdels_dataGridViewListRazdels.RowCount - 1; ++index2)
      {
        if (this.Razdels_dataGridViewListRazdels.Rows[index2].Cells[1].Value == null)
        {
          this.SelectRazdels_dataGridViewListRazdelsCell(index1, 1);
          int num = (int) MessageBox.Show("Наименование раздела не задано", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return true;
        }
        string str2 = this.Razdels_dataGridViewListRazdels.Rows[index2].Cells[1].Value.ToString().Trim();
        if (str1 == str2)
        {
          this.SelectRazdels_dataGridViewListRazdelsCell(index2, 1);
          int num = (int) MessageBox.Show("Наименование раздела повторяется\r\n" + str1, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return true;
        }
      }
    }
    return false;
  }

  /// <summary> Очистка от пустых  </summary>
  public void Cleaning_Of_Empty_OneRazdelVed()
  {
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed == null)
      return;
    for (int index = this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count - 1; index > -1; --index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      if (this.Cleaning_Of_Empty_OneRazdelVed_Curr(oneRazdelVed))
        this._one_Ved_Nastr_Tmp._list_RazdelsVed.RemoveAt(index);
      else
        this.Cleaning_Of_Empty_OnePodRazdelVed(oneRazdelVed);
    }
  }

  /// <summary> Очистка от пустых строк </summary>
  public bool Cleaning_Of_Empty_OneRazdelVed_Curr(Vedomost_VB.OneRazdelVed oneRazdelVed)
  {
    if (oneRazdelVed == null || oneRazdelVed._name == null || oneRazdelVed._name == "" || oneRazdelVed._razdelVed < 1)
      return true;
    this.Cleaning_Of_Empty_OnePodRazdelVed(oneRazdelVed);
    return false;
  }

  /// <summary> Очистка от пустых элементов </summary>
  /// <param textFromColumn="oneRazdelVed"></param>
  public void Cleaning_Of_Empty_OnePodRazdelVed(Vedomost_VB.OneRazdelVed oneRazdelVed)
  {
    if (oneRazdelVed._list_onePodRazdels == null)
      return;
    for (int index = oneRazdelVed._list_onePodRazdels.Count - 1; index > -1; --index)
    {
      Vedomost_VB.OnePodRazdelVed listOnePodRazdel = oneRazdelVed._list_onePodRazdels[index];
      if (listOnePodRazdel._name == null || listOnePodRazdel._name == "")
        oneRazdelVed._list_onePodRazdels.RemoveAt(index);
      else if (listOnePodRazdel._podRazdelVed < 1)
        oneRazdelVed._list_onePodRazdels.RemoveAt(index);
    }
    if (oneRazdelVed._list_onePodRazdels.Count != 0)
      return;
    oneRazdelVed._list_onePodRazdels = (List<Vedomost_VB.OnePodRazdelVed>) null;
  }

  /// <summary> Выделить ячейку </summary>
  /// <param textFromColumn="rowIndex"></param>
  /// <param textFromColumn="cellIndex"></param>
  public void SelectRazdels_dataGridViewListRazdelsCell(int rowIndex, int cellIndex)
  {
    this.Razdels_dataGridViewListRazdels.CurrentCell = this.Razdels_dataGridViewListRazdels.Rows[rowIndex].Cells[cellIndex];
    this.Razdels_dataGridViewListRazdels.CurrentCell.Selected = true;
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param textFromColumn="rowNum"></param>
  public void SelectRazdels_dataGridViewListRazdelsRow(int rowNum)
  {
    this.SelectRazdels_dataGridViewListRazdelsCell(rowNum, 0);
  }

  /// <summary> Добавляется строка Разделов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListRazdels_RowsAdded(
    object sender,
    DataGridViewRowsAddedEventArgs e)
  {
    if (this.isCreate)
      return;
    this.oneRazdelVed_Prevision = this.oneRazdelVed_Curr;
    this.oneRazdelVed_Curr = new Vedomost_VB.OneRazdelVed();
    int rowIndex = e.RowIndex;
    if (rowIndex == this.Razdels_dataGridViewListRazdels.Rows.Count - 1)
      this._one_Ved_Nastr_Tmp._list_RazdelsVed.Add(this.oneRazdelVed_Curr);
    else
      this._one_Ved_Nastr_Tmp._list_RazdelsVed.Insert(rowIndex, this.oneRazdelVed_Curr);
    this.Draw_Table_PodRazdel(this.oneRazdelVed_Curr);
  }

  /// <summary> Добавляется строка Подразделов </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Razdels_dataGridViewListPodRazdels_RowsAdded(
    object sender,
    DataGridViewRowsAddedEventArgs e)
  {
    if (this.isCreate || this.is_Podrazdel_Auto)
      return;
    this.onePodRazdelVed_Prevision = this.onePodRazdelVed_Curr;
    this.onePodRazdelVed_Curr = new Vedomost_VB.OnePodRazdelVed();
    int rowIndex = e.RowIndex;
    if (this.oneRazdelVed_Curr._list_onePodRazdels == null)
      this.oneRazdelVed_Curr._list_onePodRazdels = new List<Vedomost_VB.OnePodRazdelVed>();
    if (rowIndex == this.Razdels_dataGridViewListPodRazdels.Rows.Count - 1)
      this.oneRazdelVed_Curr._list_onePodRazdels.Add(this.onePodRazdelVed_Curr);
    else
      this.oneRazdelVed_Curr._list_onePodRazdels.Insert(rowIndex, this.onePodRazdelVed_Curr);
  }

  /// <summary> Рисование страницы ЗАГОЛОВКИ </summary>
  private void Draw_Page_Zagolovki()
  {
    this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
    this.listBox_Zagolovki_AttribVedRec_Filled();
    this.List_Ved_Id_Draw(this.listBox_Zagolovki_List_Ved_Id);
    this.ListZagolovkov_draw();
    if (this.dataGridView_ListZagolovkov.Rows.Count < 2)
    {
      this.rowNumCurrent_Zagolovok = -1;
      this.buttonDelete_Zagolovki.Enabled = false;
    }
    if (this._one_Ved_Nastr_Tmp._zagolovki_Ved == null || this._one_Ved_Nastr_Tmp._list_RazdelsVed == null)
      return;
    this.checkBox_Zagolovki_VyvoditPodrazdely.Visible = false;
    if (this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeCompare == Vedomost_VB.TypeCompare.Int)
    {
      this.radioButton_Zagolovki_Compare_Int.Checked = true;
      this.radioButton_Zagolovki_Compare_Symbol.Checked = false;
    }
    else
    {
      this.radioButton_Zagolovki_Compare_Int.Checked = false;
      this.radioButton_Zagolovki_Compare_Symbol.Checked = true;
    }
    this.textBox_Include_Name.Text = this._one_Ved_Nastr_Tmp._zagolovki_Ved._include_Name;
    this.label_Zagolovki_SpravaVnizu_Draw();
    this.checkBox_Zagolovki_VyvoditPodrazdely.Checked = false;
    this.checkBox_Zagolovki_VyvoditPodrazdely.Visible = false;
    for (int index = 1; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      if (oneRazdelVed._list_onePodRazdels != null && oneRazdelVed._list_onePodRazdels.Count > 0)
      {
        this.checkBox_Zagolovki_VyvoditPodrazdely.Visible = true;
        this.checkBox_Zagolovki_VyvoditPodrazdely.Checked = this._one_Ved_Nastr_Tmp._zagolovki_Ved._vyvodit_PodZagolovki;
        break;
      }
    }
    this.checkBox_UserZagolovki.Checked = this._one_Ved_Nastr_Tmp._zagolovki_Ved._userZagolovki;
    this.checkBox_LocationZagolovki.Checked = this._one_Ved_Nastr_Tmp._zagolovki_Ved._locationZagolovki;
    if (Vedomost_VB_Static.List_Ved_OpisanieVed != null)
      return;
    Vedomost_VB_Static.Begin_For_Ved();
    Vedomost_VB_Static.List_Ved_OpisanieVed_Create();
  }

  /// <summary> Нажатие "Выводить заголовки подразделов" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void checkBox_Zagolovki_VyvoditPodrazdely_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  private void checkBox_UserZagolovki_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  private void checkBox_LocationZagolovki_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  private void radioButton_Zagolovki_Compare_Int_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.label_Zagolovki_SpravaVnizu_Draw();
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  private void radioButton_Zagolovki_Compare_Symbol_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.label_Zagolovki_SpravaVnizu_Draw();
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Заполнение списка атрибутов ведомостей </summary>
  private void listBox_Zagolovki_AttribVedRec_Filled()
  {
    this.listBox_Zagolovki_AttribVedRec.Items.Clear();
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedRec.Count; ++index)
      this.listBox_Zagolovki_AttribVedRec.Items.Add((object) Vedomost_VB_Static._listOneAttribVedRec[index]._name);
  }

  /// <summary> Отрисовка списка заголовков </summary>
  private void ListZagolovkov_draw()
  {
    string text = "";
    if (this._one_Ved_Nastr_Tmp._zagolovki_Ved == null)
      return;
    if (this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      text = MetaDataHelper.GetAttributeTypeName(this._one_Ved_Nastr_Tmp._zagolovki_Ved._objectType);
      this.List_Ved_Id_SelectedValue(this.listBox_Zagolovki_List_Ved_Id, text);
      this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
      this.button_Zagolovki_FromList.Visible = false;
    }
    else
    {
      int index = -1;
      Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec, out index);
      this.listBox_Zagolovki_AttribVedRec.SelectedIndex = index;
      if (index > -1 && this.listBox_Zagolovki_AttribVedRec.Items.Count > index)
        text = this.listBox_Zagolovki_AttribVedRec.Items[index].ToString();
      if (this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
        this.button_Zagolovki_FromList.Visible = true;
      else
        this.button_Zagolovki_FromList.Visible = false;
      this.listBox_Zagolovki_List_Ved_Id.SelectedIndex = -1;
    }
    this.label_Zagolovki_Attribut.Text = text;
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok.Count; ++index)
    {
      Vedomost_VB.One_Zagolovok oneZagolovok = this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok[index];
      this.dataGridView_ListZagolovkov.Rows.Add((object[]) new string[2]
      {
        oneZagolovok._granicaPriznaka,
        oneZagolovok._name
      });
    }
    this.Select_dataGridViewListZagolovkov_Row(0);
    this.dataGridView_ListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
    if (this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok.Count == 0)
    {
      this.label_NoZgolovki.Visible = true;
      this.label_Zagolovki_SpravaVnizu.Visible = false;
    }
    else
    {
      this.label_NoZgolovki.Visible = false;
      this.label_Zagolovki_SpravaVnizu.Visible = true;
    }
  }

  /// <summary> Выделить ячейку </summary>
  /// <param textFromColumn="rowIndex"></param>
  /// <param textFromColumn="cellIndex"></param>
  public void Select_dataGridViewListZagolovkov_Cell(int rowIndex, int cellIndex)
  {
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0];
    this.dataGridView_ListZagolovkov.CurrentCell.Selected = true;
  }

  /// <summary> Выделить строку rowNum </summary>
  /// <param textFromColumn="rowNum"></param>
  public void Select_dataGridViewListZagolovkov_Row(int rowNum)
  {
    this.Select_dataGridViewListZagolovkov_Cell(rowNum, 0);
  }

  /// <summary> Отрисовка комментария текущей строки </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_ListZagolovkov_CellEnter(object sender, DataGridViewCellEventArgs e)
  {
    this.buttonAdd_Zagolovki.Enabled = true;
    this.buttonDelete_Zagolovki.Enabled = true;
    string[] currentRowZagolovki = this.getCurrentRow_Zagolovki();
    if (currentRowZagolovki[0] == "" && currentRowZagolovki[1] == "")
    {
      this.buttonAdd_Zagolovki.Enabled = false;
      if (this.dataGridView_ListZagolovkov.CurrentCell.RowIndex == this.dataGridView_ListZagolovkov.RowCount - 1)
        this.buttonDelete_Zagolovki.Enabled = false;
    }
    if (currentRowZagolovki[0] == "" && currentRowZagolovki[1] != "")
      this.buttonAdd_Zagolovki.Enabled = false;
    if (currentRowZagolovki[0] != "" && currentRowZagolovki[1] == "")
      this.buttonAdd_Zagolovki.Enabled = false;
    this.label_Zagolovki_SpravaVnizu_Draw();
    string str1 = "";
    string str2 = "";
    int rowIndex = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value != null)
      str1 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value != null)
      str2 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value.ToString();
    if (str1 != "" || str2 != "")
      this.label_NoZgolovki.Visible = false;
    this.rowNumCurrent_Zagolovok = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    if (this.rowNumCurrent_Zagolovok == this.dataGridView_ListZagolovkov.Rows.Count - 1)
    {
      this.buttonDelete_Zagolovki.Enabled = false;
      this.buttonAdd_Zagolovki.Enabled = false;
    }
    else
    {
      this.buttonDelete_Zagolovki.Enabled = true;
      this.buttonAdd_Zagolovki.Enabled = true;
    }
  }

  private void label_Zagolovki_SpravaVnizu_Draw()
  {
    string str1 = "";
    string[] currentRowZagolovki = this.getCurrentRow_Zagolovki();
    if (currentRowZagolovki[0] != "" && currentRowZagolovki[1] != "")
    {
      string str2 = $"{$"{"Записи ведомости, у которых значение атрибута" + "\n"}\"{this.label_Zagolovki_Attribut.Text}\"" + "\n" + "Равно или более"} {currentRowZagolovki[0]}";
      string[] nextRowZagolovki = this.getNextRow_Zagolovki();
      if (nextRowZagolovki[0] != "")
        str2 = $"{str2 + " и менее"} {nextRowZagolovki[0]}";
      string str3 = $"{str2 + "\n" + "Будут иметь заголовок" + "\n"}\"{currentRowZagolovki[1]}\"" + "\n";
      str1 = !this.radioButton_Zagolovki_Compare_Int.Checked ? str3 + "Сравнение значений производится посимвольное" + "\n" + "'Строки могут иметь разную длину и содержать буквы" : str3 + "Сравнение значений производится числовое";
    }
    this.label_Zagolovki_SpravaVnizu.Text = str1;
    if (str1 == "")
      this.label_Zagolovki_SpravaVnizu.Visible = false;
    else
      this.label_Zagolovki_SpravaVnizu.Visible = true;
  }

  /// <summary> Текущая СТРОКА </summary>
  /// <returns></returns>
  private string[] getCurrentRow_Zagolovki()
  {
    string[] currentRowZagolovki = new string[2]{ "", "" };
    int rowIndex = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    string str1 = "";
    string str2 = "";
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value != null)
      str1 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value != null)
      str2 = this.dataGridView_ListZagolovkov.Rows[rowIndex].Cells[1].Value.ToString();
    currentRowZagolovki[0] = str1;
    currentRowZagolovki[1] = str2;
    return currentRowZagolovki;
  }

  /// <summary> Следующая СТРОКА </summary>
  /// <returns></returns>
  private string[] getNextRow_Zagolovki()
  {
    string[] nextRowZagolovki = new string[2]{ "", "" };
    int index = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex + 1;
    string str1 = "";
    string str2 = "";
    if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value != null)
      str1 = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
    if (this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value != null)
      str2 = this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value.ToString();
    nextRowZagolovki[0] = str1;
    nextRowZagolovki[1] = str2;
    return nextRowZagolovki;
  }

  /// <summary> Выбор в списке специфичных атрибутов ведомости ВНИЗУ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Zagolovki_AttribVedRec_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Zagolovki_List_Ved_Id.SelectedIndex = -1;
    this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    if (Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex]._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
      this.button_Zagolovki_FromList.Visible = true;
    else
      this.button_Zagolovki_FromList.Visible = false;
  }

  /// <summary> Изменение ключевого атрибута </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Zagolovki_EditKeyAttribut_Click(object sender, EventArgs e)
  {
    if (this.listBox_Zagolovki_List_Ved_Id.SelectedIndex < 0 && this.listBox_Zagolovki_AttribVedRec.SelectedIndex < 0)
    {
      int num1 = (int) MessageBox.Show("Нет выбранного атрибута", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      string str;
      if (this.listBox_Zagolovki_List_Ved_Id.SelectedIndex > -1)
      {
        this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._objectType = this.Get_ObjType_By_index(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.listBox_Zagolovki_List_Ved_Id.SelectedIndex);
        str = this.listBox_Zagolovki_List_Ved_Id.Items[this.listBox_Zagolovki_List_Ved_Id.SelectedIndex].ToString();
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeField = Vedomost_VB.TypeField.ObjectType;
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
        if (!Vedomost_VB_Static.Find_Id_In_SortingUsl(this._one_Ved_Nastr_Tmp._sorting_Usl, this._one_Ved_Nastr_Tmp._zagolovki_Ved._objectType))
        {
          string attributeTypeName = MetaDataHelper.GetAttributeTypeName(this._one_Ved_Nastr_Tmp._zagolovki_Ved._objectType);
          string text = $"Атрибут\r\n\"{attributeTypeName}\"\r\n\r\n" + "Для создания заголовков необходима и сортировка по данному атрибуту.\r\nВнесите его на странице настройки\r\n\"Правила сортировки\"";
          this.buttonWarnings.Visible = true;
          int num2 = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          this.listWarnings.Add($"Атрибут  \"{attributeTypeName}\"  добавить в \"Правила сортировки\"");
        }
        this.button_Zagolovki_FromList.Visible = false;
      }
      else
      {
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._objectType = -1;
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
        Vedomost_VB.OneAttribVedRec oneAttribVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex];
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec = oneAttribVedRec._typeFieldVedRec;
        str = this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
        if (oneAttribVedRec._typeFieldVedRec == Vedomost_VB.TypeFieldVedRec.Razdel_Ved)
          this.button_Zagolovki_FromList.Visible = true;
        else
          this.button_Zagolovki_FromList.Visible = false;
      }
      this.label_Zagolovki_Attribut.Text = str;
      this.dataGridView_ListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
      this.ModifiedAll(true);
      this.IsModified_Page_Zagolovki = true;
    }
  }

  /// <summary> Если тыкаем на нижний список, то в верхнем деактивируем строку </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Zagolovki_AttribVedRec_MouseClick_1(object sender, MouseEventArgs e)
  {
    this.listBox_Zagolovki_List_Ved_Id.SelectedIndex = -1;
  }

  /// <summary> Ткнули в списке собираемых атрибутов ВВЕРХУ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Zagolovki_List_Ved_Id_MouseClick(object sender, MouseEventArgs e)
  {
    if (this.listBox_Zagolovki_List_Ved_Id.SelectedIndex < 0)
      return;
    this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
    this.listBox_Zagolovki_List_Ved_Id.Items[this.listBox_Zagolovki_List_Ved_Id.SelectedIndex].ToString();
    this.button_Zagolovki_FromList.Visible = false;
  }

  /// <summary> Выборали Double в списке атрибутов ВВЕРХУ</summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Zagolovki_List_Ved_Id_DoubleClick(object sender, EventArgs e)
  {
    this.listBox_Zagolovki_AttribVedRec.SelectedIndex = -1;
    this.button_Zagolovki_EditKeyAttribut_Click(sender, e);
  }

  /// <summary> Начало редактирования строки </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_ListZagolovkov_CellBeginEdit(
    object sender,
    DataGridViewCellCancelEventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Кнопка "По списку разделов" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Zagolovki_FromList_Click(object sender, EventArgs e)
  {
    this.create_Zagolovki_Ved_From_list_RazdelsVed();
  }

  /// <summary> Создание списка заголовков по списку разделов </summary>
  private void create_Zagolovki_Ved_From_list_RazdelsVed()
  {
    this.dataGridView_ListZagolovkov.Rows.Clear();
    this._one_Ved_Nastr_Tmp._zagolovki_Ved.Clear();
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Razdel_Ved;
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
      {
        _granicaPriznaka = oneRazdelVed._razdelVed.ToString(),
        _name = oneRazdelVed._name
      });
    }
    this.ListZagolovkov_draw();
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeFieldVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Zagolovki_AttribVedRec.SelectedIndex]._typeFieldVedRec;
    this.label_Zagolovki_Attribut.Text = this.listBox_Zagolovki_AttribVedRec.Items[this.listBox_Zagolovki_AttribVedRec.SelectedIndex].ToString();
    this.dataGridView_ListZagolovkov_CellEnter((object) null, (DataGridViewCellEventArgs) null);
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Кнопка ДОБАВИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Zagolovki_Add_Click(object sender, EventArgs e)
  {
    int y = this.dataGridView_ListZagolovkov.CurrentCellAddress.Y;
    string[] strArray = new string[2];
    if (this.dataGridView_ListZagolovkov.Rows[y].Cells[0].Value == null || this.dataGridView_ListZagolovkov.Rows[y].Cells[1].Value == null || string.IsNullOrEmpty(this.dataGridView_ListZagolovkov.Rows[y].Cells[0].Value.ToString()) || string.IsNullOrEmpty(this.dataGridView_ListZagolovkov.Rows[y].Cells[1].Value.ToString()))
      return;
    this.dataGridView_ListZagolovkov.Rows.Insert(y, (object[]) strArray);
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[y].Cells[0];
    this.label_NoZgolovki.Visible = false;
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Удаление заголовка </summary>
  private void Delete_Zagolovok()
  {
    if (this.rowNumCurrent_Zagolovok < 0 || this.dataGridView_ListZagolovkov.Rows.Count < 2 || this.rowNumCurrent_Zagolovok == this.dataGridView_ListZagolovkov.Rows.Count - 1)
      return;
    int y = this.dataGridView_ListZagolovkov.CurrentCellAddress.Y;
    this.dataGridView_ListZagolovkov.Rows.RemoveAt(this.rowNumCurrent_Zagolovok);
    this.dataGridView_ListZagolovkov.CurrentCell = this.dataGridView_ListZagolovkov.Rows[y].Cells[0];
    if (this.dataGridView_ListZagolovkov.Rows.Count == 1)
      this.label_NoZgolovki.Visible = true;
    if (this.dataGridView_ListZagolovkov.Rows.Count < 2)
    {
      this.rowNumCurrent_Zagolovok = -1;
      this.buttonDelete_Zagolovki.Enabled = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Zagolovki_Delete_Click(object sender, EventArgs e) => this.Delete_Zagolovok();

  private void dataGridView_ListZagolovkov_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.Delete_Zagolovok();
  }

  /// <summary> Редактировние заголовка Ведомости составных частей </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void textBox_Include_Name_KeyDown(object sender, KeyEventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  /// <summary> Контроль значения ячеек при выходе из них</summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void dataGridView_ListZagolovkov_CellValidating(
    object sender,
    DataGridViewCellValidatingEventArgs e)
  {
    if (this.rowNumCurrent_Zagolovok < 0 || this.rowNumCurrent_Zagolovok == this.dataGridView_ListZagolovkov.Rows.Count - 1)
      return;
    if (this.dataGridView_ListZagolovkov.Columns[e.ColumnIndex].Name == "Zagolovok_Column1" && e.FormattedValue != null && e.FormattedValue.ToString().Trim() == "")
    {
      this.dataGridView_ListZagolovkov.Rows[e.RowIndex].ErrorText = "Значение атрибута не должно быть пустым";
      e.Cancel = true;
    }
    if (this.dataGridView_ListZagolovkov.Columns[e.ColumnIndex].Name == "Zagolovok_Column2" && e.FormattedValue != null && e.FormattedValue.ToString().Trim() == "")
    {
      this.dataGridView_ListZagolovkov.Rows[e.RowIndex].ErrorText = "Текст заголовка не должен быть пустым";
      e.Cancel = true;
    }
    if (!e.Cancel)
      return;
    int num = (int) MessageBox.Show(this.dataGridView_ListZagolovkov.Rows[e.RowIndex].ErrorText, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private void dataGridView_ListZagolovkov_CellValueChanged(
    object sender,
    DataGridViewCellEventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Zagolovki = true;
  }

  private void dataGridView_ListZagolovkov_CellEndEdit(object sender, DataGridViewCellEventArgs e)
  {
    this.dataGridView_ListZagolovkov.Rows[e.RowIndex].ErrorText = string.Empty;
  }

  private void dataGridView_ListZagolovkov_CellClick(object sender, DataGridViewCellEventArgs e)
  {
    this.rowNumCurrent_Zagolovok = this.dataGridView_ListZagolovkov.CurrentCell.RowIndex;
    if (this.rowNumCurrent_Zagolovok == this.dataGridView_ListZagolovkov.Rows.Count - 1)
    {
      this.buttonDelete_Zagolovki.Enabled = false;
      this.buttonAdd_Zagolovki.Enabled = false;
    }
    else
    {
      this.buttonDelete_Zagolovki.Enabled = true;
      this.buttonAdd_Zagolovki.Enabled = true;
    }
  }

  /// <summary> Контроль страницы "Заголовки" </summary>
  /// <returns></returns>
  private bool Zagolovki_dataGridView_ListZagolovkov_Control()
  {
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      DataGridViewCell cell1 = this.dataGridView_ListZagolovkov.Rows[index].Cells[0];
      string str1 = cell1.Value == null ? "" : cell1.Value.ToString();
      DataGridViewCell cell2 = this.dataGridView_ListZagolovkov.Rows[index].Cells[1];
      string str2 = cell2.Value == null ? "" : cell2.Value.ToString();
      if (str1 == "")
        flag2 = true;
      if (str2 == "")
        flag3 = true;
    }
    if (flag2 | flag3)
    {
      string text = "Есть ошибки в списке Заголовков:";
      if (flag2)
        text += "\r\n\r\nЗначение атрибута не должно быть пустым";
      if (flag3)
        text += "\r\n\r\nТекст заголовка не должен быть пустым";
      this.tabControl_Nastr.SelectTab(this.tabPage_Zagolovki);
      int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      return true;
    }
    if (this.dataGridView_ListZagolovkov_ControlData())
      flag1 = true;
    return flag1;
  }

  /// <summary> Контролировать данные таблицы </summary>
  /// <returns></returns>
  private bool dataGridView_ListZagolovkov_ControlData()
  {
    string strY = "";
    bool flag = false;
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value == null)
      {
        this.Select_dataGridViewListZagolovkov_Cell(index, 0);
        int num = (int) MessageBox.Show("Признак заголовка не задан", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value == null)
      {
        this.Select_dataGridViewListZagolovkov_Cell(index, 0);
        int num = (int) MessageBox.Show("Текст заголовка не задан", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
      string strX = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
      if (strY != "" && Vedomost_VB_Static.StringCompareWithNumber(strX, strY) < 0)
      {
        this.Select_dataGridViewListZagolovkov_Cell(index, 0);
        int num = (int) MessageBox.Show("Признак заголовка не в порядке возрастания", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = true;
        break;
      }
      strY = strX;
    }
    return flag;
  }

  /// <summary> Сохранение редактирования страницы "Заголовки" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Zagolovki()
  {
    this.dataGridViewListZagolovkov_CleanFromEmpty();
    this.dataGridView_ListZagolovkov_ControlData();
    this.dataGridViewListZagolovkov_To_zagolovki_Ved();
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._vyvodit_PodZagolovki = this.checkBox_Zagolovki_VyvoditPodrazdely.Checked;
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._userZagolovki = this.checkBox_UserZagolovki.Checked;
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._locationZagolovki = this.checkBox_LocationZagolovki.Checked;
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._typeCompare = !this.radioButton_Zagolovki_Compare_Int.Checked ? Vedomost_VB.TypeCompare.Symbol : Vedomost_VB.TypeCompare.Int;
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._include_Name = this.textBox_Include_Name.Text;
  }

  /// <summary> Удаление пустых строк </summary>
  private void dataGridViewListZagolovkov_CleanFromEmpty()
  {
    for (int index = this.dataGridView_ListZagolovkov.RowCount - 2; index > -1; --index)
    {
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value == null)
        this.dataGridView_ListZagolovkov.Rows.RemoveAt(index);
    }
  }

  /// <summary> Данные из таблицы перенести в zagolovki_Ved_Curr </summary>
  /// <returns></returns>
  private void dataGridViewListZagolovkov_To_zagolovki_Ved()
  {
    this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok.Clear();
    for (int index = 0; index < this.dataGridView_ListZagolovkov.RowCount - 1; ++index)
    {
      string str1 = "";
      string str2 = "";
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value != null)
        str1 = this.dataGridView_ListZagolovkov.Rows[index].Cells[0].Value.ToString();
      if (this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value != null)
        str2 = this.dataGridView_ListZagolovkov.Rows[index].Cells[1].Value.ToString();
      if (!(str1 == ""))
        this._one_Ved_Nastr_Tmp._zagolovki_Ved._list_One_Zagolovok.Add(new Vedomost_VB.One_Zagolovok()
        {
          _granicaPriznaka = str1,
          _name = str2
        });
    }
  }

  /// <summary> Чтение и обработка ШАБЛОНА </summary>
  /// <param textFromColumn="template_Guid"></param>
  private void Processing_Template(Guid template_Guid)
  {
    this.isKudaVhoditInfo = false;
    this.isItogoInfo = false;
    this.imDocument_template_Vyvod = (ImDocument) null;
    if (template_Guid == Guid.Empty)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(template_Guid, false);
      this.templateID_Vyvod = dbObject == null ? -1L : dbObject.ObjectID;
      this.templateID_curr_Vyvod = this.templateID_Vyvod;
    }
    if (this.templateID_Vyvod != 0L && this.templateID_Vyvod != -1L)
      this.imDocument_template_Vyvod = this.LoadTemplateFromObject(this.templateID_curr_Vyvod);
    this.isKudaVhoditInfo = false;
    this.isItogoInfo = false;
    if (this.imDocument_template_Vyvod.FindNode("Подтаблица Куда входит") == null)
      return;
    this.isKudaVhoditInfo = true;
    if (this.imDocument_template_Vyvod.FindNode("Строка Кол итого") == null)
      return;
    this.isItogoInfo = true;
  }

  /// <summary> Загрузить шаблон </summary>
  /// <param textFromColumn="objectId"></param>
  /// <returns></returns>
  public ImDocument LoadTemplateFromObject(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectId, false);
      if (dbObject != null)
        this.docName = dbObject.Caption;
    }
    ImDocument imDocument = (ImDocument) null;
    MemoryStream aDestStream = new MemoryStream();
    try
    {
      new BlobProcReader(objectId, AttributableElements.Object, Vedomost_VB_Static.fileAttrId, 0, 0, (Stream) aDestStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
      aDestStream.Position = 0L;
      if (aDestStream.Length != 0L)
      {
        imDocument = ImDocument.LoadFromStream((Stream) aDestStream, true, false, true);
      }
      else
      {
        imDocument = new ImDocument(true);
        imDocument.Modified = false;
      }
    }
    finally
    {
      aDestStream.Close();
    }
    return imDocument;
  }

  private long TemplateId_Vyvod
  {
    get => this.templID_Vyvod;
    set
    {
      this.templID_Vyvod = value;
      this.CloseAllDocs_Vyvod();
      if (this.templID_Vyvod == 0L || this.templID_Vyvod == -1L)
        return;
      this.OpenTemplate_Vyvod(this.imDocument_template_Vyvod);
    }
  }

  private void OpenTemplate_Vyvod(ImDocument doc)
  {
    this.CloseAllDocs_Vyvod();
    this.docControl_Vyvod = new DocumentControl();
    this.docControl_Vyvod.Document = doc;
    double num = (double) this.docControl_Vyvod.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Vyvod = new DockControl((Control) this.docControl_Vyvod, this.docName);
    this.docKcontrol_Vyvod.Show(this.dockMan_Vyvod, DockState.Document);
    this.docKcontrol_Vyvod.Closable = false;
    this.ShowDocumentTreeView_Vyvod();
    this.docKcontrol_Vyvod.Select();
    this.docControl_Vyvod.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Vyvod
  {
    get
    {
      return this.documentTreeViewDlg_Vyvod != null && !this.documentTreeViewDlg_Vyvod.IsDisposed ? this.documentTreeViewDlg_Vyvod : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Vyvod()
  {
    if (this.DocumentTreeViewDlg_Vyvod == null)
      this.documentTreeViewDlg_Vyvod = new DocumentTreeViewDlg();
    if (this.docControl_Vyvod != null)
    {
      DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
      this.documentTreeViewDlg_Vyvod.TreeRoot = (DocumentTreeNode) this.docControl_Vyvod.Document;
      this.documentTreeViewDlg_Vyvod.DocumentControl = this.docControl_Vyvod;
      this.documentTreeViewDlg_Vyvod.UpdateSelection();
    }
    this.documentTreeViewDlg_Vyvod.Show(this.dockMan_Vyvod, DockState.DockRight);
    this.documentTreeViewDlg_Vyvod.Closable = false;
  }

  /// <summary> Очистка контейнера в окне вывода </summary>
  private void CloseAllDocs_Vyvod()
  {
    for (int index = 0; index < this.docContainer_Vyvod.Documents.Length; ++index)
      this.docContainer_Vyvod.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementStr_Vyvod(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Vyvod != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Vyvod.FindNode(currId) ?? this.imDocument_template_Vyvod.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Vyvod.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Vyvod.ResetTernBufer();
        this.documentTreeViewDlg_Vyvod.UpdateSelection();
      }
      else if (selection == null)
      {
        int num = (int) MessageBox.Show($"В шаблоне не найден элемент\r\n\r\n\"{currId}\"", "Ошибка!");
      }
      else
        this.docControl_Vyvod.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Vyvod == null)
        return;
      this.docControl_Vyvod.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementInt_Vyvod(int currId) => this.SetElementStr_Vyvod(currId.ToString());

  /// <summary> Рисование страницы ВЫВОД </summary>
  private void Draw_Page_Vyvod()
  {
    this.algorithmToPrint = this._one_Ved_Nastr_Tmp._algorithmToPrint;
    if (this._one_Ved_Nastr_Tmp._algorithmToPrint_B != null)
    {
      Guid templateObjectGuidB = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      this.algorithmToPrint_B = this._one_Ved_Nastr_Tmp._algorithmToPrint_B;
      this.groupBox_Vyvod_Forma.Visible = true;
      this.estGroupB = true;
    }
    else
    {
      this.algorithmToPrint_B = (Vedomost_VB.AlgorithmToPrint) null;
      this.groupBox_Vyvod_Forma.Visible = false;
      this.estGroupB = false;
    }
    this.algorithmToPrint_curr = this.algorithmToPrint;
    this.List_Ved_Id_Draw(this.listBox_Vyvod_List_Ved_Id);
    this.listBox_Vyvod_AttribVedRec_Filled();
    this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
    this.listBoxAttrib_Vyvod_VedPasport_Filled();
    this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Vyvod = dbObject.ObjectID;
      this.templateID_curr_Vyvod = this.templateID_Vyvod;
    }
    this.radioButton_Vyvod_EdOrA.Checked = true;
    this.radioButton_Vyvod_GroupB.Checked = false;
    if (this.estGroupB)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = (IDBObject) null;
        if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B != Guid.Empty)
          dbObject = sessionKeeper.Session.GetObject(this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B, false);
        if (dbObject != null)
          this.templateID_B = dbObject.ObjectID;
      }
      if (this.algorithmToPrint_B._kolGraf > 0 && this.algorithmToPrint_B._kolGraf < 11)
        this.numeric_Vyvod_UpDownKolGraf.Value = (Decimal) this.algorithmToPrint_B._kolGraf;
    }
    this.TemplateId_Vyvod = this.templateID_curr_Vyvod;
    this.treeView_Vyvod_Draw();
    if (this.treeView_Vyvod != null && this.treeView_Vyvod.Nodes.Count > 0)
      this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
    this.Page_Vyvod_2_Draw();
  }

  /// <summary> Заполнение списка атрибутов ведомостей </summary>
  private void listBox_Vyvod_AttribVedRec_Filled()
  {
    this.listBox_Vyvod_AttribVedRec.Items.Clear();
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedRec.Count; ++index)
      this.listBox_Vyvod_AttribVedRec.Items.Add((object) Vedomost_VB_Static._listOneAttribVedRec[index]._name);
  }

  /// <summary> Заполнение списка атрибутов основной надписи ведомостей </summary>
  private void listBoxAttrib_Vyvod_VedPasport_Filled()
  {
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedPasport.Count; ++index)
      this.listBoxAttrib_Vyvod_VedPasport.Items.Add((object) Vedomost_VB_Static._listOneAttribVedPasport[index]._name);
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Vyvod_Draw()
  {
    this.treeView_Vyvod.Nodes.Clear();
    if (this.imDocument_template_Vyvod == null || this.algorithmToPrint_curr == null)
      return;
    A_NastrVed.OneVyvodNode oneVyvodNode1 = new A_NastrVed.OneVyvodNode();
    oneVyvodNode1.Text = "Правила вывода на шаблон: " + this.algorithmToPrint_curr._tableName;
    oneVyvodNode1.ImageIndex = this._indexImageList_Section;
    oneVyvodNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneVyvodNode oneVyvodNode2 = oneVyvodNode1;
    oneVyvodNode2._oneVyvodNode_Parent = (A_NastrVed.OneVyvodNode) null;
    oneVyvodNode2._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode2._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode2._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    this.treeView_Vyvod.Nodes.Add((TreeNode) oneVyvodNode2);
    A_NastrVed.OneVyvodNode oneVyvodNode3 = new A_NastrVed.OneVyvodNode();
    oneVyvodNode3.Text = "Информационные записи: ";
    oneVyvodNode3.ImageIndex = this._indexImageList_Section;
    oneVyvodNode3.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneVyvodNode oneVyvodNode4 = oneVyvodNode3;
    oneVyvodNode4._oneVyvodNode_Parent = (A_NastrVed.OneVyvodNode) null;
    oneVyvodNode4._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode4._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode4._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode4._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    oneVyvodNode2.Nodes.Add((TreeNode) oneVyvodNode4);
    if (this.algorithmToPrint_curr._list_OneRazdelToPrint != null && this.algorithmToPrint_curr._list_OneRazdelToPrint.Count > 0)
    {
      for (int index = 0; index < this.algorithmToPrint_curr._list_OneRazdelToPrint.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrint oneRazdelToPrint = this.algorithmToPrint_curr._list_OneRazdelToPrint[index];
        Vedomost_VB.OneRecordToPrint recordToPrintInfo = oneRazdelToPrint._oneRecordToPrint_Info;
        if (recordToPrintInfo != null)
          this.Tree_Add(recordToPrintInfo, oneVyvodNode4, oneRazdelToPrint._razdelVed);
      }
      this.button_Vyvod_PoRazdelam.Visible = false;
      this.button_Vyvod_Obshaia.Visible = true;
    }
    else
    {
      if (this.algorithmToPrint_curr._oneRecordToPrint_Info != null)
      {
        A_NastrVed.OneVyvodNode oneVyvodNode5 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info, oneVyvodNode4);
        if (oneVyvodNode5 != null)
        {
          oneVyvodNode4.Nodes.Add((TreeNode) oneVyvodNode5);
          if (this.isKudaVhoditInfo && this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Vtor != null)
          {
            A_NastrVed.OneVyvodNode node = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Vtor, oneVyvodNode5);
            if (node != null)
              oneVyvodNode5.Nodes.Add((TreeNode) node);
          }
          if (this.isItogoInfo && this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Itogo != null)
          {
            A_NastrVed.OneVyvodNode node = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrint_Info._oneRecordToPrint_Itogo, oneVyvodNode5);
            if (node != null)
              oneVyvodNode5.Nodes.Add((TreeNode) node);
          }
          oneVyvodNode5.Expand();
        }
      }
      this.button_Vyvod_PoRazdelam.Visible = true;
      this.button_Vyvod_Obshaia.Visible = false;
    }
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed == null || this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count < 3)
    {
      this.button_Vyvod_PoRazdelam.Visible = false;
      this.button_Vyvod_Obshaia.Visible = false;
    }
    oneVyvodNode4.Expand();
    if (this.algorithmToPrint_curr._oneRecordToPrintTitleIncluded != null)
    {
      A_NastrVed.OneVyvodNode oneVyvodNode6 = new A_NastrVed.OneVyvodNode();
      oneVyvodNode6.Text = "Ведомости составных частей: ";
      oneVyvodNode6.ImageIndex = this._indexImageList_Section;
      oneVyvodNode6.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneVyvodNode node1 = oneVyvodNode6;
      node1._oneVyvodNode_Parent = (A_NastrVed.OneVyvodNode) null;
      node1._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
      node1._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      node1._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node1._typeNode = Vedomost_VB_Static.TypeNode_Tree.VedSost;
      A_NastrVed.OneVyvodNode node2 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintTitleIncluded, oneVyvodNode2);
      if (node2 != null)
        node1.Nodes.Add((TreeNode) node2);
      oneVyvodNode2.Nodes.Add((TreeNode) node1);
      A_NastrVed.OneVyvodNode oneVyvodNode7 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintIncluded, oneVyvodNode2);
      if (oneVyvodNode7 != null)
      {
        node1.Nodes.Add((TreeNode) oneVyvodNode7);
        if (this.isKudaVhoditInfo && this.algorithmToPrint_curr._oneRecordToPrintIncluded._oneRecordToPrint_Vtor != null)
        {
          A_NastrVed.OneVyvodNode node3 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintIncluded._oneRecordToPrint_Vtor, oneVyvodNode7);
          if (node3 != null)
            oneVyvodNode7.Nodes.Add((TreeNode) node3);
        }
        if (this.isItogoInfo && this.algorithmToPrint_curr._oneRecordToPrintIncluded._oneRecordToPrint_Itogo != null)
        {
          A_NastrVed.OneVyvodNode node4 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintIncluded._oneRecordToPrint_Itogo, oneVyvodNode7);
          if (node4 != null)
            oneVyvodNode7.Nodes.Add((TreeNode) node4);
        }
        oneVyvodNode7.Expand();
      }
      node1.Expand();
    }
    A_NastrVed.OneVyvodNode node5 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintTitle, oneVyvodNode2);
    if (node5 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node5);
    A_NastrVed.OneVyvodNode node6 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintTitlePodSection, oneVyvodNode2);
    if (node6 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node6);
    A_NastrVed.OneVyvodNode node7 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintTitleVar, oneVyvodNode2);
    if (node7 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node7);
    A_NastrVed.OneVyvodNode node8 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintTitleIsp, oneVyvodNode2);
    if (node8 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node8);
    A_NastrVed.OneVyvodNode node9 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintRemark, oneVyvodNode2);
    if (node9 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node9);
    A_NastrVed.OneVyvodNode node10 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintRemarkShort, oneVyvodNode2);
    if (node10 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node10);
    if (this.algorithmToPrint_curr._list_OneRazdelToPrintAdditional != null && this.algorithmToPrint_curr._list_OneRazdelToPrintAdditional.Count > 0)
    {
      A_NastrVed.OneVyvodNode oneVyvodNode8 = new A_NastrVed.OneVyvodNode();
      oneVyvodNode8.Text = "Дополнительные записи: ";
      oneVyvodNode8.ImageIndex = this._indexImageList_Section;
      oneVyvodNode8.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneVyvodNode oneVyvodNode9 = oneVyvodNode8;
      oneVyvodNode9._oneVyvodNode_Parent = (A_NastrVed.OneVyvodNode) null;
      oneVyvodNode9._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
      oneVyvodNode9._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      oneVyvodNode9._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      oneVyvodNode9._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
      oneVyvodNode2.Nodes.Add((TreeNode) oneVyvodNode9);
      for (int index = 0; index < this.algorithmToPrint_curr._list_OneRazdelToPrintAdditional.Count; ++index)
      {
        Vedomost_VB.OneRazdelToPrintAdditional toPrintAdditional = this.algorithmToPrint_curr._list_OneRazdelToPrintAdditional[index];
        if (this.algorithmToPrint_curr._additional1 == 1)
        {
          Vedomost_VB.OneRecordToPrint printAdditional1 = toPrintAdditional._oneRecordToPrint_Additional1;
          if (printAdditional1 != null)
            this.Tree_Add(printAdditional1, oneVyvodNode9, toPrintAdditional._razdelVed);
        }
        if (this.algorithmToPrint_curr._additional2 == 1)
        {
          Vedomost_VB.OneRecordToPrint printAdditional2 = toPrintAdditional._oneRecordToPrint_Additional2;
          if (printAdditional2 != null)
            this.Tree_Add(printAdditional2, oneVyvodNode9, toPrintAdditional._razdelVed);
        }
        if (this.algorithmToPrint_curr._additional3 == 1)
        {
          Vedomost_VB.OneRecordToPrint printAdditional3 = toPrintAdditional._oneRecordToPrint_Additional3;
          if (printAdditional3 != null)
            this.Tree_Add(printAdditional3, oneVyvodNode9, toPrintAdditional._razdelVed);
        }
        if (this.algorithmToPrint_curr._additional4 == 1)
        {
          Vedomost_VB.OneRecordToPrint printAdditional4 = toPrintAdditional._oneRecordToPrint_Additional4;
          if (printAdditional4 != null)
            this.Tree_Add(printAdditional4, oneVyvodNode9, toPrintAdditional._razdelVed);
        }
      }
      oneVyvodNode9.Expand();
    }
    else
    {
      if (this.algorithmToPrint_curr._additional1 == 1)
        this.Tree_Add(this.algorithmToPrint_curr._oneRecordToPrintAdditional1, oneVyvodNode2);
      if (this.algorithmToPrint_curr._additional2 == 1)
        this.Tree_Add(this.algorithmToPrint_curr._oneRecordToPrintAdditional2, oneVyvodNode2);
      if (this.algorithmToPrint_curr._additional3 == 1)
        this.Tree_Add(this.algorithmToPrint_curr._oneRecordToPrintAdditional3, oneVyvodNode2);
      if (this.algorithmToPrint_curr._additional4 == 1)
        this.Tree_Add(this.algorithmToPrint_curr._oneRecordToPrintAdditional4, oneVyvodNode2);
    }
    A_NastrVed.OneVyvodNode node11 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintPasport, oneVyvodNode2);
    if (node11 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node11);
    A_NastrVed.OneVyvodNode node12 = this.oneRecordNode_Vyvod_Create(this.algorithmToPrint_curr._oneRecordToPrintEmpty, oneVyvodNode2);
    if (node12 != null)
      oneVyvodNode2.Nodes.Add((TreeNode) node12);
    oneVyvodNode2.Expand();
  }

  /// <summary> Формируется ветка и добавляется к ветке node_Target </summary>
  /// <param textFromColumn="oneRecord_Nastr"></param>
  /// <param textFromColumn="node_Target"></param>
  /// <param textFromColumn="razdelVed"></param>
  private void Tree_Add(
    Vedomost_VB.OneRecordToPrint oneRecord_Nastr,
    A_NastrVed.OneVyvodNode node_Target,
    int razdelVed = 0)
  {
    A_NastrVed.OneVyvodNode oneVyvodNode = this.oneRecordNode_Vyvod_Create(oneRecord_Nastr, node_Target, razdelVed);
    if (oneVyvodNode == null)
      return;
    node_Target.Nodes.Add((TreeNode) oneVyvodNode);
    if (this.isKudaVhoditInfo && oneRecord_Nastr._oneRecordToPrint_Vtor != null)
    {
      A_NastrVed.OneVyvodNode node = this.oneRecordNode_Vyvod_Create(oneRecord_Nastr._oneRecordToPrint_Vtor, oneVyvodNode);
      if (node != null)
        oneVyvodNode.Nodes.Add((TreeNode) node);
    }
    if (!this.isItogoInfo || oneRecord_Nastr._oneRecordToPrint_Itogo == null)
      return;
    A_NastrVed.OneVyvodNode node1 = this.oneRecordNode_Vyvod_Create(oneRecord_Nastr._oneRecordToPrint_Itogo, oneVyvodNode);
    if (node1 == null)
      return;
    oneVyvodNode.Nodes.Add((TreeNode) node1);
  }

  /// <summary> Ветка, описывающая одну ЗАПИСЬ </summary>
  /// <param textFromColumn="oneRecordToPrint"></param>
  /// <param textFromColumn="oneVyvodNode_Parent"></param>
  /// <returns></returns>
  private A_NastrVed.OneVyvodNode oneRecordNode_Vyvod_Create(
    Vedomost_VB.OneRecordToPrint oneRecordToPrint,
    A_NastrVed.OneVyvodNode oneVyvodNode_Parent,
    int razdelVed = 0)
  {
    if (oneRecordToPrint == null)
      return (A_NastrVed.OneVyvodNode) null;
    string str1 = Vedomost_VB_Static.TypeRecName_by_TypeRec(oneRecordToPrint._nameTypeRec);
    string str2 = !(str1 != "Информационная") || str1.IndexOf("Дополнительная") == 0 ? "" : str1;
    if (oneRecordToPrint._tableRowId != "")
    {
      if (str2 != "")
      {
        str2 = $"{str2}: {oneRecordToPrint._tableRowId}";
      }
      else
      {
        if (razdelVed > 0)
        {
          string nameRazdelVed = Vedomost_VB_Static.Get_NameRazdelVed(this._one_Ved_Nastr_Tmp._list_RazdelsVed, razdelVed);
          if (nameRazdelVed != "")
            str2 = nameRazdelVed + ": ";
        }
        str2 += oneRecordToPrint._tableRowId;
      }
    }
    A_NastrVed.OneVyvodNode oneVyvodNode = new A_NastrVed.OneVyvodNode();
    oneVyvodNode.Text = str2;
    oneVyvodNode.ImageIndex = this._indexImageList_Section;
    oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneVyvodNode oneVyvodNode_Parent1 = oneVyvodNode;
    oneVyvodNode_Parent1._oneVyvodNode_Parent = oneVyvodNode_Parent;
    oneVyvodNode_Parent1._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode_Parent1._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode_Parent1._oneRecordToPrint = oneRecordToPrint;
    oneVyvodNode_Parent1._typeNode = !(oneRecordToPrint._nameTypeRec == "oneRecordToPrintPasport") ? Vedomost_VB_Static.TypeNode_Tree.Record : Vedomost_VB_Static.TypeNode_Tree.RecordPasport;
    if (oneRecordToPrint._listOneGrafaToPrint != null)
    {
      for (int index = 0; index < oneRecordToPrint._listOneGrafaToPrint.Count; ++index)
      {
        A_NastrVed.OneVyvodNode node = this.oneGrafaNode_Vyvod_Create(oneRecordToPrint._listOneGrafaToPrint[index], oneVyvodNode_Parent1);
        if (node != null)
          oneVyvodNode_Parent1.Nodes.Add((TreeNode) node);
      }
    }
    return oneVyvodNode_Parent1;
  }

  /// <summary> Ветка, описывающая одну ГРАФУ </summary>
  /// <param textFromColumn="oneGrafaToPrint"></param>
  /// <param textFromColumn="oneVyvodNode_Parent"></param>
  /// <returns></returns>
  private A_NastrVed.OneVyvodNode oneGrafaNode_Vyvod_Create(
    Vedomost_VB.OneGrafaToPrint oneGrafaToPrint,
    A_NastrVed.OneVyvodNode oneVyvodNode_Parent)
  {
    if (oneGrafaToPrint == null)
      return (A_NastrVed.OneVyvodNode) null;
    string cellId = oneGrafaToPrint._cell_ID;
    A_NastrVed.OneVyvodNode oneVyvodNode = new A_NastrVed.OneVyvodNode();
    oneVyvodNode.Text = "Ячейка шаблона: " + cellId;
    oneVyvodNode.ImageIndex = this._indexImageList_Section;
    oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneVyvodNode oneVyvodNode_Parent1 = oneVyvodNode;
    oneVyvodNode_Parent1._oneVyvodNode_Parent = oneVyvodNode_Parent;
    oneVyvodNode_Parent1._oneGrafaToPrint = oneGrafaToPrint;
    oneVyvodNode_Parent1._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
    oneVyvodNode_Parent1._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
    if (oneGrafaToPrint._listOneDataFieldToPrint != null)
    {
      for (int index = 0; index < oneGrafaToPrint._listOneDataFieldToPrint.Count; ++index)
      {
        A_NastrVed.OneVyvodNode node = this.oneDataNode_Vyvod_Create(oneGrafaToPrint._listOneDataFieldToPrint[index], oneVyvodNode_Parent1, index);
        if (node != null)
        {
          node._oneVyvodNode_Parent = oneVyvodNode_Parent1;
          oneVyvodNode_Parent1.Nodes.Add((TreeNode) node);
        }
      }
    }
    return oneVyvodNode_Parent1;
  }

  /// <summary> Формирование одной конечной ветки ДАННЫх для дерева </summary>
  /// <param textFromColumn="oneDataFieldToPrint"></param>
  /// <param textFromColumn="oneVyvodNode_Parent"></param>
  /// <param textFromColumn="iData"></param>
  /// <returns></returns>
  private A_NastrVed.OneVyvodNode oneDataNode_Vyvod_Create(
    Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint,
    A_NastrVed.OneVyvodNode oneVyvodNode_Parent,
    int iData)
  {
    if (oneDataFieldToPrint == null)
      return (A_NastrVed.OneVyvodNode) null;
    string str = this.OneDataField_Draw(oneDataFieldToPrint, iData);
    A_NastrVed.OneVyvodNode oneVyvodNode1 = new A_NastrVed.OneVyvodNode();
    oneVyvodNode1.Text = str;
    oneVyvodNode1.ImageIndex = this._indexImageList_Section;
    oneVyvodNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneVyvodNode oneVyvodNode2 = oneVyvodNode1;
    oneVyvodNode2._oneVyvodNode_Parent = iData <= 0 ? oneVyvodNode_Parent : oneVyvodNode_Parent._oneVyvodNode_Parent;
    oneVyvodNode2._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    oneVyvodNode2._oneDataFieldToPrint = oneDataFieldToPrint;
    oneVyvodNode2._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    oneVyvodNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    oneVyvodNode2._iData = iData;
    return oneVyvodNode2;
  }

  /// <summary> Формирование одной конечной строчки ДАННЫх для дерева </summary>
  /// <param textFromColumn="oneDataFieldToPrint"></param>
  /// <returns></returns>
  private string OneDataField_Draw(
    Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint,
    int iData)
  {
    if (oneDataFieldToPrint == null)
      return "";
    string str = "";
    this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text(oneDataFieldToPrint._symbolRazd, true);
    if (iData > 0)
      str = $"\"{this.translate_text(oneDataFieldToPrint._symbolRazd, false)}\" ";
    if (oneDataFieldToPrint._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      string attributeTypeName = MetaDataHelper.GetAttributeTypeName(oneDataFieldToPrint._objectType);
      if (attributeTypeName != null)
        str += attributeTypeName;
    }
    if (oneDataFieldToPrint._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
    {
      int index = -1;
      Vedomost_VB.OneAttribVedRec oneAttribVedRec = Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(oneDataFieldToPrint._typeFieldVedRec, out index);
      if (oneAttribVedRec != null)
      {
        string name = oneAttribVedRec._name;
        str += name;
      }
    }
    if (oneDataFieldToPrint._typeField == Vedomost_VB.TypeField.TypeFieldVedPasport)
    {
      int index = -1;
      Vedomost_VB.OneAttribVedPasport attribVedPasport = Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedPasport(oneDataFieldToPrint._typeFieldVedPasport, out index);
      if (attribVedPasport != null)
      {
        string name = attribVedPasport._name;
        str += name;
      }
    }
    return str;
  }

  /// <summary>Рисование подстраницы Вывод/Прочее</summary>
  public void Page_Vyvod_2_Draw()
  {
    if (this.algorithmToPrint_curr == null)
      return;
    if (this.algorithmToPrint_curr._includedLizmInDoc == 0)
    {
      this.numericUpDown_Vyvod2_Lizm.Value = (Decimal) this.algorithmToPrint_curr._iLIZM;
      this.numericUpDown_Vyvod2_Lizm.Enabled = true;
      this.checkBox_Vyvod2_Lizm.Enabled = true;
      this.checkBox_Vyvod2_IncludedLizmInDoc.Checked = false;
      this.checkBox_Vyvod2_Lizm.Checked = this.algorithmToPrint_curr._iLIZM > 0;
    }
    else
    {
      this.numericUpDown_Vyvod2_Lizm.Value = 0M;
      this.numericUpDown_Vyvod2_Lizm.Enabled = false;
      this.checkBox_Vyvod2_Lizm.Checked = false;
      this.checkBox_Vyvod2_Lizm.Enabled = false;
      this.checkBox_Vyvod2_IncludedLizmInDoc.Checked = true;
    }
    this.numericUpDown_Vyvod2_AfterInfo.Value = (Decimal) this.algorithmToPrint_curr._afterInfo;
    this.numericUpDown_Vyvod2_AfterRemark.Value = (Decimal) this.algorithmToPrint_curr._afterRemark;
    this.checkBox_Vyvod_Additional1.Checked = this.algorithmToPrint_curr._additional1 != 0;
    this.checkBox_Vyvod_Additional2.Checked = this.algorithmToPrint_curr._additional2 != 0;
    this.checkBox_Vyvod_Additional3.Checked = this.algorithmToPrint_curr._additional3 != 0;
    this.checkBox_Vyvod_Additional4.Checked = this.algorithmToPrint_curr._additional4 != 0;
    this.checkBox_Vyvod_isDeleteIdenticalTexts.Checked = this.algorithmToPrint_curr._isDeleteIdenticalTexts;
    this.checkBox_isCheck.Checked = this.algorithmToPrint_curr._isCheck;
    this.checkBox_isUnbrokenDefis.Checked = this.algorithmToPrint_curr._isUnbrokenDefis;
    if (this.checkBox_Services_autoSbor.Checked)
      this.checkBox_isFullProhibition.Visible = true;
    else
      this.checkBox_isFullProhibition.Visible = false;
    if (this._one_Ved_Nastr_Tmp._protection_From_Editing == null)
    {
      this.checkBox_isFullProhibition.Checked = false;
      this.checkBox_isProhibition_DocRowWithObj.Checked = false;
      this.groupBox_Protection_From_Editing.Visible = false;
      this.checkBox_isProtectionCommand.Checked = false;
      this.groupBox_ProtectionCommand.Visible = false;
    }
    else
    {
      string ipsVersion = Vedomost_VB_Static.AssemblyAttributes.IPSVersion;
      if (Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain || !ipsVersion.StartsWith("6"))
      {
        this.groupBox_Protection_From_Editing.Visible = true;
        this.checkBox_isFullProhibition.Checked = this._one_Ved_Nastr_Tmp._protection_From_Editing._isFullProhibition;
        this.checkBox_isProhibition_DocRowWithObj.Checked = this._one_Ved_Nastr_Tmp._protection_From_Editing._isProhibition_DocRowWithObj;
        if (Vedomost_VB_Static.IsAdmin)
          this.groupBox_ProtectionCommand.Visible = true;
        else
          this.groupBox_ProtectionCommand.Visible = false;
        this.checkBox_isProtectionCommand.Checked = this._one_Ved_Nastr_Tmp._protection_From_Editing._isProtectionCommand;
      }
      else
      {
        this.checkBox_isFullProhibition.Checked = false;
        this.checkBox_isProhibition_DocRowWithObj.Checked = false;
        this.groupBox_Protection_From_Editing.Visible = false;
        this.checkBox_isProtectionCommand.Checked = false;
        this.groupBox_ProtectionCommand.Visible = false;
      }
    }
  }

  private void checkBox_Vyvod_Additional1_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._additional1 = !this.checkBox_Vyvod_Additional1.Checked ? 0 : 1;
    this.treeView_Vyvod_Draw();
  }

  private void checkBox_Vyvod_Additional2_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._additional2 = !this.checkBox_Vyvod_Additional2.Checked ? 0 : 1;
    this.treeView_Vyvod_Draw();
  }

  private void checkBox_Vyvod_Additional3_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._additional3 = !this.checkBox_Vyvod_Additional3.Checked ? 0 : 1;
    this.treeView_Vyvod_Draw();
  }

  private void checkBox_Vyvod_Additional4_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._additional4 = !this.checkBox_Vyvod_Additional4.Checked ? 0 : 1;
    this.treeView_Vyvod_Draw();
  }

  private void checkBox_Vyvod_isDeleteIdenticalTexts_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._isDeleteIdenticalTexts = this.checkBox_Vyvod_isDeleteIdenticalTexts.Checked;
  }

  private void checkBox_isCheck_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._isCheck = this.checkBox_isCheck.Checked;
  }

  private void checkBox_isUnbrokenDefis_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.algorithmToPrint_curr._isUnbrokenDefis = this.checkBox_isUnbrokenDefis.Checked;
  }

  /// <summary> Запретить редактирование содержания ведомости, созданной программой автоматически </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isFullProhibition_CheckedChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this._one_Ved_Nastr_Tmp._protection_From_Editing._isFullProhibition = this.checkBox_isFullProhibition.Checked;
  }

  /// <summary> Запретить редактирование данных, соответствующих объектам, введенных из базы </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isProhibition_DocRowWithObj_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this._one_Ved_Nastr_Tmp._protection_From_Editing._isProhibition_DocRowWithObj = this.checkBox_isProhibition_DocRowWithObj.Checked;
  }

  /// <summary> Отображать или нет команды "Только для чтения" </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void checkBox_isProtectionCommand_CheckedChanged(object sender, EventArgs e)
  {
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this._one_Ved_Nastr_Tmp._protection_From_Editing._isProtectionCommand = this.checkBox_isProtectionCommand.Checked;
  }

  /// <summary> При выделении ветки в дереве </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void treeView_Vyvod_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string currId1 = "";
    this.i_curr_oneGrafaToPrint_Current = -1;
    this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
    this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
    this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
    this.listBox_Vyvod_List_Ved_Id.Enabled = true;
    this.listBox_Vyvod_AttribVedRec.Enabled = true;
    this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
    this.groupBox_Vyvod_AttribVedRec1.Visible = true;
    this.groupBox_Vyvod_Ved_Pasport.Visible = false;
    this.comboBox_Vyvod_TextRazdelitel.Enabled = true;
    this.groupBox_Vyvod_TextRazdelitel.Enabled = true;
    this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
    this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
    this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex = -1;
    this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text("", true);
    this.treeView_Vyvod.Enabled = true;
    this.nameRazdel = "";
    this.oneTreeNode_Current = (A_NastrVed.OneVyvodNode) this.treeView_Vyvod.SelectedNode;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      this.listBox_Vyvod_AttribVedRec.Enabled = false;
      this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
      this.groupBox_Vyvod_AttribVedRec1.Visible = true;
      this.groupBox_Vyvod_Ved_Pasport.Visible = false;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      string tableName = this.algorithmToPrint_curr == null ? "" : this.algorithmToPrint_curr._tableName;
      this.SetElementStr_Vyvod(tableName);
      if (tableName == "")
      {
        this.button_Vyvod_AddCell.Enabled = true;
        this.button_Vyvod_AddAttribut.Enabled = true;
        this.button_Vyvod_Edit.Enabled = false;
      }
      else
      {
        this.button_Vyvod_AddCell.Enabled = false;
        this.button_Vyvod_AddAttribut.Enabled = false;
        this.button_Vyvod_Edit.Enabled = true;
      }
      this.button_Vyvod_Delete.Enabled = false;
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Info || this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.VedSost)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = (Vedomost_VB.OneRecordToPrint) null;
      this.button_Vyvod_AddCell.Enabled = false;
      this.button_Vyvod_AddAttribut.Enabled = false;
      this.button_Vyvod_Edit.Enabled = false;
      this.button_Vyvod_Delete.Enabled = false;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      this.listBox_Vyvod_AttribVedRec.Enabled = false;
      this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
      this.groupBox_Vyvod_AttribVedRec1.Visible = true;
      this.groupBox_Vyvod_Ved_Pasport.Visible = false;
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      this.SetElementStr_Vyvod("");
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = (Vedomost_VB.OneGrafaToPrint) null;
      this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneRecordToPrint;
      this.button_Vyvod_AddCell.Enabled = true;
      this.button_Vyvod_AddAttribut.Enabled = false;
      this.button_Vyvod_Edit.Enabled = true;
      this.button_Vyvod_Delete.Enabled = false;
      this.i_curr_oneGrafaToPrint_Current = -1;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = false;
      this.listBox_Vyvod_AttribVedRec.Enabled = false;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport" || this.oneTreeNode_Current.Text == "Основная надпись")
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = false;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = true;
        this.groupBox_Vyvod_AttribVedRec1.Visible = false;
        this.groupBox_Vyvod_Ved_Pasport.Visible = true;
        this.button_Vyvod_Edit.Enabled = false;
      }
      else
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = false;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
        this.groupBox_Vyvod_AttribVedRec1.Visible = true;
        this.groupBox_Vyvod_Ved_Pasport.Visible = false;
        if (this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintInfo")
        {
          this.nameRazdel = "";
          string text = this.oneTreeNode_Current.Text;
          int length = text.IndexOf(':');
          if (length > -1)
            this.nameRazdel = text.Substring(0, length);
        }
      }
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      string currId2 = this.oneRecordToPrint_Current == null ? "" : this.oneRecordToPrint_Current._tableRowId;
      if (currId2 == "" && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport")
        currId2 = "Основная надпись";
      this.SetElementStr_Vyvod(currId2);
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.oneDataFieldToPrint_current = (Vedomost_VB.OneDataFieldToPrint) null;
      this.oneGrafaToPrint_Current = this.oneTreeNode_Current._oneGrafaToPrint;
      this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneRecordToPrint;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._listOneGrafaToPrint != null)
        this.i_curr_oneGrafaToPrint_Current = this.oneRecordToPrint_Current._listOneGrafaToPrint.IndexOf(this.oneGrafaToPrint_Current);
      this.button_Vyvod_AddCell.Enabled = true;
      this.button_Vyvod_AddAttribut.Enabled = true;
      this.button_Vyvod_Edit.Enabled = true;
      this.button_Vyvod_Delete.Enabled = true;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = true;
      this.listBox_Vyvod_AttribVedRec.Enabled = true;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport" || this.oneTreeNode_Current.Text == "Основная надпись")
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = false;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = true;
        this.groupBox_Vyvod_AttribVedRec1.Visible = false;
        this.groupBox_Vyvod_Ved_Pasport.Visible = true;
      }
      else
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = true;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
        this.groupBox_Vyvod_AttribVedRec1.Visible = true;
        this.groupBox_Vyvod_Ved_Pasport.Visible = false;
      }
      this.comboBox_Vyvod_TextRazdelitel.Enabled = false;
      this.groupBox_Vyvod_TextRazdelitel.Enabled = false;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex = -1;
      this.SetElementStr_Vyvod(this.oneGrafaToPrint_Current == null ? "" : this.oneGrafaToPrint_Current._cell_ID);
    }
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
    {
      this.oneDataFieldToPrint_current = this.oneTreeNode_Current._oneDataFieldToPrint;
      this.oneGrafaToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneGrafaToPrint;
      this.oneRecordToPrint_Current = this.oneTreeNode_Current._oneVyvodNode_Parent._oneVyvodNode_Parent._oneRecordToPrint;
      this.button_Vyvod_AddCell.Enabled = false;
      this.button_Vyvod_AddAttribut.Enabled = true;
      this.button_Vyvod_Edit.Enabled = true;
      this.button_Vyvod_Delete.Enabled = true;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBox_Vyvod_List_Ved_Id.Enabled = true;
      this.listBox_Vyvod_AttribVedRec.Enabled = true;
      if (this.oneRecordToPrint_Current != null && this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport" || this.oneTreeNode_Current.Text == "Основная надпись")
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = false;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = true;
        this.groupBox_Vyvod_AttribVedRec1.Visible = false;
        this.groupBox_Vyvod_Ved_Pasport.Visible = true;
      }
      else
      {
        this.listBox_Vyvod_AttribVedRec.Enabled = true;
        this.listBoxAttrib_Vyvod_VedPasport.Enabled = false;
        this.groupBox_Vyvod_AttribVedRec1.Visible = true;
        this.groupBox_Vyvod_Ved_Pasport.Visible = false;
      }
      this.comboBox_Vyvod_TextRazdelitel.Enabled = true;
      this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
      this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
      this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex = -1;
      this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text("", true);
      if (this.oneDataFieldToPrint_current._typeField == Vedomost_VB.TypeField.ObjectType && this._one_Ved_Nastr_Tmp._list_Ved_ID != null)
        this.List_Ved_Id_SelectedByObjType(this.listBox_Vyvod_List_Ved_Id, this._one_Ved_Nastr_Tmp._list_Ved_ID, this.oneDataFieldToPrint_current._objectType);
      if (this.oneDataFieldToPrint_current._typeField == Vedomost_VB.TypeField.TypeFieldVedRec)
      {
        int index = -1;
        Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(this.oneDataFieldToPrint_current._typeFieldVedRec, out index);
        this.listBox_Vyvod_AttribVedRec.SelectedIndex = index;
      }
      if (this.oneDataFieldToPrint_current._typeField == Vedomost_VB.TypeField.TypeFieldVedPasport)
      {
        int index = -1;
        Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedPasport(this.oneDataFieldToPrint_current._typeFieldVedPasport, out index);
        this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex = index;
      }
      this.SetElementStr_Vyvod(this.oneGrafaToPrint_Current == null ? "" : this.oneGrafaToPrint_Current._cell_ID);
      this.comboBox_Vyvod_TextRazdelitel.Text = this.translate_text(this.oneDataFieldToPrint_current._symbolRazd, true);
    }
    else
      this.SetElementStr_Vyvod(currId1);
  }

  /// <summary> Если тыкаем на верхний список, то в нижнем деактивируем строку </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Vyvod_List_Ved_Id_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
  }

  /// <summary> Если тыкаем на верхний список, то в нижнем деактивируем строку </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void listBox_Vyvod_AttribVedRec_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
  }

  /// <summary> Переключение к Единичному и форме А </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Vyvod_EdOrA_Click(object sender, EventArgs e)
  {
    if (this.isradioButtonEdOrA)
      return;
    this.algorithmToPrint_curr = this.algorithmToPrint;
    this.templateID_curr_Vyvod = this.templateID_Vyvod;
    if (this.TemplateId_Vyvod != 0L && this.TemplateId_Vyvod != -1L)
      this.imDocument_template_Vyvod = this.LoadTemplateFromObject(this.templateID_curr_Vyvod);
    this.TemplateId_Vyvod = this.templateID_curr_Vyvod;
    this.label_Vyvod_Graf.Visible = false;
    this.numeric_Vyvod_UpDownKolGraf.Visible = false;
    this.treeView_Vyvod_Draw();
    this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
    this.isradioButtonEdOrA = true;
    this.isradioButtonGroupB = false;
    this.Page_Vyvod_2_Draw();
    this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
  }

  /// <summary> Изменение КолГраф </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numeric_Vyvod_UpDownKolGraf_ValueChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.algorithmToPrint_curr._kolGraf = (int) this.numeric_Vyvod_UpDownKolGraf.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary> Переключение к форме Б </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Vyvod_GroupB_Click(object sender, EventArgs e)
  {
    if (this.isradioButtonGroupB)
      return;
    this.groupB();
  }

  /// <summary> Групповая фориа Б </summary>
  private void groupB()
  {
    if (this.templateID_B == -1L || this.templateID_B == 0L)
    {
      int num = (int) MessageBox.Show("Бланк (шаблон) для формы Б не назначен\r\n\r\nСмотри на закладке \"Сервис\" кнопка \"Шаблон Б\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      this.radioButton_Vyvod_EdOrA.Checked = true;
    }
    else
    {
      this.algorithmToPrint_curr = this.algorithmToPrint_B;
      this.templateID_curr_Vyvod = this.templateID_B;
      if (this.TemplateId_Vyvod != 0L && this.TemplateId_Vyvod != -1L)
        this.imDocument_template_Vyvod = this.LoadTemplateFromObject(this.templateID_curr_Vyvod);
      this.TemplateId_Vyvod = this.templateID_curr_Vyvod;
      this.label_Vyvod_Graf.Visible = true;
      this.numeric_Vyvod_UpDownKolGraf.Visible = true;
      this.treeView_Vyvod_Draw();
      this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
      this.isradioButtonEdOrA = false;
      this.isradioButtonGroupB = true;
      this.Page_Vyvod_2_Draw();
      this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
    }
  }

  /// <summary> Кнопка ДОБАВИТЬ ЯЧЕЙКУ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Vyvod_AddCell_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
    if (activeElement == null || activeElement.Id == "")
      return;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      A_NastrVed.OneVyvodNode oneVyvodNode = new A_NastrVed.OneVyvodNode();
      oneVyvodNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneVyvodNode.ImageIndex = this._indexImageList_Section;
      oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneVyvodNode node = oneVyvodNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneVyvodNode_Parent = this.oneTreeNode_Current;
      node._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node._iData = -1;
      Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint();
      node._oneGrafaToPrint = oneGrafaToPrint;
      oneGrafaToPrint._cell_ID = activeElement.Id;
      oneGrafaToPrint._listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>();
      this.oneTreeNode_Current.Nodes.Insert(0, (TreeNode) node);
      this.oneRecordToPrint_Current._listOneGrafaToPrint.Insert(0, oneGrafaToPrint);
      this.ModifiedAll(true);
      this.IsModified_Page_Vyvod = true;
    }
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrVed.OneVyvodNode oneVyvodNode = new A_NastrVed.OneVyvodNode();
      oneVyvodNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneVyvodNode.ImageIndex = this._indexImageList_Section;
      oneVyvodNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneVyvodNode node = oneVyvodNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneVyvodNode_Parent = this.oneTreeNode_Current._oneVyvodNode_Parent;
      node._oneDataFieldToPrint = (Vedomost_VB.OneDataFieldToPrint) null;
      node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
      node._iData = -1;
      Vedomost_VB.OneGrafaToPrint oneGrafaToPrint = new Vedomost_VB.OneGrafaToPrint();
      node._oneGrafaToPrint = oneGrafaToPrint;
      oneGrafaToPrint._cell_ID = activeElement.Id;
      oneGrafaToPrint._listOneDataFieldToPrint = new List<Vedomost_VB.OneDataFieldToPrint>();
      this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.Insert(this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Current) + 1, (TreeNode) node);
      this.oneRecordToPrint_Current._listOneGrafaToPrint.Insert(this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Current) + 1, oneGrafaToPrint);
      this.ModifiedAll(true);
      this.IsModified_Page_Vyvod = true;
    }
    this.treeView_Vyvod.Select();
  }

  /// <summary> Кнопка ДОБАВИТЬ АТРИБУТ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Vyvod_AddAttribut_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    Vedomost_VB.OneDataFieldToPrint oneDataFieldToPrint = new Vedomost_VB.OneDataFieldToPrint();
    oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.ObjectType;
    if (this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport")
    {
      if (this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex <= -1)
        return;
      oneDataFieldToPrint._objectType = -1;
      oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.TypeFieldVedPasport;
      Vedomost_VB.OneAttribVedPasport attribVedPasport = Vedomost_VB_Static._listOneAttribVedPasport[this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex];
      oneDataFieldToPrint._typeFieldVedPasport = attribVedPasport._typeFieldVedPasport;
    }
    else
    {
      if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex == -1 && this.listBox_Vyvod_AttribVedRec.SelectedIndex == -1)
      {
        int num = (int) MessageBox.Show("Атрибут не выбран", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex > -1)
      {
        this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
        oneDataFieldToPrint._objectType = this.Get_ObjType_By_index(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.listBox_Vyvod_List_Ved_Id.SelectedIndex);
        oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.ObjectType;
        oneDataFieldToPrint._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
      }
      else if (this.listBox_Vyvod_AttribVedRec.SelectedIndex > -1)
      {
        this.listBox_Vyvod_List_Ved_Id.SelectedIndex = -1;
        oneDataFieldToPrint._objectType = -1;
        oneDataFieldToPrint._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
        Vedomost_VB.OneAttribVedRec oneAttribVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Vyvod_AttribVedRec.SelectedIndex];
        oneDataFieldToPrint._typeFieldVedRec = oneAttribVedRec._typeFieldVedRec;
      }
    }
    A_NastrVed.OneVyvodNode node;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      oneDataFieldToPrint._symbolRazd = "";
      node = this.oneDataNode_Vyvod_Create(oneDataFieldToPrint, this.oneTreeNode_Current, 0);
    }
    else
    {
      oneDataFieldToPrint._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
      node = this.oneDataNode_Vyvod_Create(oneDataFieldToPrint, this.oneTreeNode_Current, this.oneTreeNode_Current._iData + 1);
    }
    node._oneGrafaToPrint = (Vedomost_VB.OneGrafaToPrint) null;
    node._oneDataFieldToPrint = oneDataFieldToPrint;
    node._oneRecordToPrint = (Vedomost_VB.OneRecordToPrint) null;
    node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    node._iData = this.oneTreeNode_Current._iData + 1;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Insert(0, oneDataFieldToPrint);
      this.oneTreeNode_Current.Nodes.Insert(0, (TreeNode) node);
    }
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
    {
      int num = this.oneGrafaToPrint_Current._listOneDataFieldToPrint.IndexOf(this.oneDataFieldToPrint_current);
      this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Insert(num + 1, oneDataFieldToPrint);
      this.oneTreeNode_Current._oneVyvodNode_Parent.Nodes.Insert(num + 1, (TreeNode) node);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.oneTreeNode_Current.Expand();
    this.treeView_Vyvod.Select();
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Vyvod_Edit_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Vyvod.ActiveElement;
    if (activeElement == null)
      return;
    string str = "";
    string id = activeElement.Id;
    string name1 = activeElement.Name;
    this.oneTreeNode_Current = (A_NastrVed.OneVyvodNode) this.treeView_Vyvod.SelectedNode;
    if (this.oneTreeNode_Current == null)
    {
      int num1 = (int) MessageBox.Show("В \"Дереве\" не выбран элемент", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num2 = (int) MessageBox.Show("На шаблоне не выбрана таблица", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        string name2 = activeElement.Name;
        if (name2 != "")
        {
          this.algorithmToPrint_curr._tableName = name2;
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
      {
        if (activeElement.NodeClass != "TableElement")
        {
          int num3 = (int) MessageBox.Show("На шаблоне не выбрана строка", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (activeElement.Id != "" && this.oneRecordToPrint_Current != null && activeElement.Id != "")
        {
          this.oneRecordToPrint_Current._tableRowId = activeElement.Id;
          this.oneTreeNode_Current.Text = $"{(!(this.nameRazdel == "") ? this.nameRazdel : Vedomost_VB_Static.TypeRecName_by_TypeRec(this.oneRecordToPrint_Current._nameTypeRec))}: Строка: {this.oneRecordToPrint_Current._tableRowId}";
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num4 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        str = activeElement.Id;
        if (this.oneGrafaToPrint_Current != null)
        {
          this.oneGrafaToPrint_Current._cell_ID = activeElement.Id;
          this.oneTreeNode_Current.Text = "Ячейка шаблона: " + this.oneGrafaToPrint_Current._cell_ID;
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
        {
          int num5 = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (this.oneRecordToPrint_Current._nameTypeRec == "oneRecordToPrintPasport")
        {
          if (this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex > -1)
          {
            this.oneDataFieldToPrint_current._objectType = -1;
            this.oneDataFieldToPrint_current._typeField = Vedomost_VB.TypeField.TypeFieldVedPasport;
            this.oneDataFieldToPrint_current._typeFieldVedPasport = Vedomost_VB_Static._listOneAttribVedPasport[this.listBoxAttrib_Vyvod_VedPasport.SelectedIndex]._typeFieldVedPasport;
            this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
            this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
            this.ModifiedAll(true);
            this.IsModified_Page_Vyvod = true;
          }
        }
        else
        {
          if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex == -1 && this.listBox_Vyvod_AttribVedRec.SelectedIndex == -1)
          {
            int num6 = (int) MessageBox.Show("Атрибут не выбран", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            return;
          }
          if (this.listBox_Vyvod_List_Ved_Id.SelectedIndex > -1)
          {
            this.listBox_Vyvod_AttribVedRec.SelectedIndex = -1;
            this.oneDataFieldToPrint_current._objectType = this.Get_ObjType_By_index(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.listBox_Vyvod_List_Ved_Id.SelectedIndex);
            this.oneDataFieldToPrint_current._typeField = Vedomost_VB.TypeField.ObjectType;
            this.oneDataFieldToPrint_current._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
            this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
            this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
            this.ModifiedAll(true);
            this.IsModified_Page_Vyvod = true;
          }
          else if (this.listBox_Vyvod_AttribVedRec.SelectedIndex > -1)
          {
            this.oneDataFieldToPrint_current._objectType = -1;
            this.oneDataFieldToPrint_current._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
            this.oneDataFieldToPrint_current._typeFieldVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Vyvod_AttribVedRec.SelectedIndex]._typeFieldVedRec;
            this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
            this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
            this.ModifiedAll(true);
            this.IsModified_Page_Vyvod = true;
          }
        }
        if (this.oneTreeNode_Current._iData > 0)
        {
          this.oneDataFieldToPrint_current._symbolRazd = this.translate_text(this.comboBox_Vyvod_TextRazdelitel.Text, false);
          this.oneTreeNode_Current.Text = this.OneDataField_Draw(this.oneDataFieldToPrint_current, this.oneTreeNode_Current._iData);
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
        }
      }
      this.treeView_Vyvod.Select();
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Vyvod_Delete_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    A_NastrVed.OneVyvodNode oneVyvodNode = (A_NastrVed.OneVyvodNode) this.oneTreeNode_Current.PrevNode ?? this.oneTreeNode_Current._oneVyvodNode_Parent;
    if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data && this.oneTreeNode_Current._iData > -1 && this.oneGrafaToPrint_Current._listOneDataFieldToPrint != null && this.oneTreeNode_Current._iData < this.oneGrafaToPrint_Current._listOneDataFieldToPrint.Count)
      this.oneGrafaToPrint_Current._listOneDataFieldToPrint.RemoveAt(this.oneTreeNode_Current._iData);
    else if (this.oneTreeNode_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && this.i_curr_oneGrafaToPrint_Current > -1 && this.oneRecordToPrint_Current._listOneGrafaToPrint != null && this.oneRecordToPrint_Current._listOneGrafaToPrint.Count > this.i_curr_oneGrafaToPrint_Current)
      this.oneRecordToPrint_Current._listOneGrafaToPrint.RemoveAt(this.i_curr_oneGrafaToPrint_Current);
    this.oneTreeNode_Current.Remove();
    if (oneVyvodNode != null)
      this.treeView_Vyvod.SelectedNode = (TreeNode) oneVyvodNode;
    this.treeView_Vyvod.Select();
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary> Сохранение редактирования страницы "Вывод" в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Page_Vyvod()
  {
    if (!this.IsModified_Page_Vyvod)
      return;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  /// <summary> Default для ВЫВОДА </summary>
  private void Default_Vyvod()
  {
    if (this._one_Ved_Nastr_Tmp._typeCreate != Vedomost_VB.TypeCreate.System)
      Vedomost_VB_Static.GuidTypeVedByTypeVed(this._one_Ved_Nastr_Tmp._typeVed);
    this.algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Init_By_GuidVed(this._one_Ved_Nastr_Tmp._typeVed);
    this._one_Ved_Nastr_Tmp._algorithmToPrint = this.algorithmToPrint;
    if (this._one_Ved_Nastr_Tmp._algorithmToPrint_B != null)
    {
      Guid templateObjectGuidB = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      this.algorithmToPrint_B = Vedomost_VB_Static.AlgorithmToPrint_Init_B(this._one_Ved_Nastr_Tmp._typeVed);
      this._one_Ved_Nastr_Tmp._algorithmToPrint_B = this.algorithmToPrint_B;
      this.numeric_Vyvod_UpDownKolGraf.Value = this.algorithmToPrint_B._kolGraf <= 0 || this.algorithmToPrint_B._kolGraf >= 11 ? 1M : (Decimal) this.algorithmToPrint_B._kolGraf;
      this.radioButton_Vyvod_EdOrA.Checked = true;
      this.groupBox_Vyvod_Forma.Visible = true;
      this.algorithmToPrint_curr = this.algorithmToPrint;
      this.templateID_curr_Vyvod = this.templateID_Vyvod;
      this.TemplateId_Vyvod = this.templateID_curr_Vyvod;
    }
    else
    {
      this.radioButton_Vyvod_EdOrA.Checked = true;
      this.groupBox_Vyvod_Forma.Visible = false;
    }
    this.algorithmToPrint_curr = this.algorithmToPrint;
    this._one_Ved_Nastr_Tmp._protection_From_Editing = Vedomost_VB_Static.Protection_From_Editing_Init(this._guidTypeVed_Curr);
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.treeView_Vyvod_Draw();
    this.Page_Vyvod_2_Draw();
  }

  /// <summary> Вывод "По разделам" </summary>
  private void button_Vyvod_PoRazdelam_Click(object sender, EventArgs e)
  {
    this.Vyvod_PoRazdelam();
    this.treeView_Vyvod_Draw();
    this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
  }

  /// <summary> Вывод "По разделам" </summary>
  private void Vyvod_PoRazdelam()
  {
    this.algorithmToPrint_curr._list_OneRazdelToPrint = new List<Vedomost_VB.OneRazdelToPrint>();
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      if (!(oneRazdelVed._name == "Ведомости составных частей") && oneRazdelVed._razdelVed != 1000)
        this.algorithmToPrint_curr._list_OneRazdelToPrint.Add(new Vedomost_VB.OneRazdelToPrint()
        {
          _razdelVed = oneRazdelVed._razdelVed,
          _oneRecordToPrint_Info = Vedomost_VB_Static.oneRecordToPrint_Copy(this.algorithmToPrint_curr._oneRecordToPrint_Info)
        });
    }
    this.algorithmToPrint_curr._oneRecordToPrint_Info = (Vedomost_VB.OneRecordToPrint) null;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary> Вывод "Общая" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Vyvod_Obshaia_Click(object sender, EventArgs e)
  {
    this.algorithmToPrint_curr._oneRecordToPrint_Info = Vedomost_VB_Static.oneRecordToPrint_Copy(this.algorithmToPrint_curr._list_OneRazdelToPrint[0]._oneRecordToPrint_Info);
    this.algorithmToPrint_curr._list_OneRazdelToPrint.Clear();
    this.algorithmToPrint_curr._list_OneRazdelToPrint = (List<Vedomost_VB.OneRazdelToPrint>) null;
    this.treeView_Vyvod_Draw();
    this.treeView_Vyvod.SelectedNode = this.treeView_Vyvod.Nodes[0];
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary> LIZM </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numericUpDown_Vyvod2_Lizm_ValueChanged(object sender, EventArgs e)
  {
    this.algorithmToPrint_curr._iLIZM = (int) Convert.ToInt16(this.numericUpDown_Vyvod2_Lizm.Value);
    this.checkBox_Vyvod2_Lizm.Checked = this.algorithmToPrint_curr._iLIZM > 0;
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  /// <summary> LIZM </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void checkBox_Vyvod2_Lizm_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Vyvod2_Lizm.Checked)
    {
      this.numericUpDown_Vyvod2_Lizm.Value = 5M;
      this.algorithmToPrint_curr._iLIZM = 5;
    }
    else
    {
      this.numericUpDown_Vyvod2_Lizm.Value = 0M;
      this.algorithmToPrint_curr._iLIZM = 0;
    }
    if (this.isCreate)
      return;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  /// <summary> Пропускать строк после информационной </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numericUpDown_Vyvod2_AfterInfo_ValueChanged(object sender, EventArgs e)
  {
    this._one_Ved_Nastr_Tmp._algorithmToPrint._afterInfo = (int) this.numericUpDown_Vyvod2_AfterInfo.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  /// <summary> Пропускать строк после примечаний </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numericUpDown_Vyvod2_AfterRemark_ValueChanged(object sender, EventArgs e)
  {
    this._one_Ved_Nastr_Tmp._algorithmToPrint._afterRemark = (int) this.numericUpDown_Vyvod2_AfterRemark.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Vyvod = true;
  }

  private long TemplateId_Xml
  {
    get => this.templID_Xml;
    set
    {
      this.templID_Xml = this.templID_Vyvod;
      this.CloseAllDocs_Xml();
      if (this.templID_Xml == 0L || this.templID_Xml == -1L)
        return;
      this.imDocument_template_Xml = this.LoadTemplateFromObject(this.templID_Xml);
      this.OpenTemplate_Xml(this.imDocument_template_Xml);
    }
  }

  private void OpenTemplate_Xml(ImDocument doc)
  {
    this.CloseAllDocs_Xml();
    this.docControl_Xml = new DocumentControl();
    this.docControl_Xml.Document = doc;
    double num = (double) this.docControl_Xml.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Xml = new DockControl((Control) this.docControl_Xml, this.docName);
    this.docKcontrol_Xml.Show(this.dockMan_Xml, DockState.Document);
    this.docKcontrol_Xml.Closable = false;
    this.ShowDocumentTreeView_Xml();
    this.docKcontrol_Xml.Select();
    this.docControl_Xml.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Xml
  {
    get
    {
      return this.documentTreeViewDlg_Xml != null && !this.documentTreeViewDlg_Xml.IsDisposed ? this.documentTreeViewDlg_Xml : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Xml()
  {
    if (this.DocumentTreeViewDlg_Xml == null)
      this.documentTreeViewDlg_Xml = new DocumentTreeViewDlg();
    if (this.docControl_Xml != null)
    {
      DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
      this.documentTreeViewDlg_Xml.TreeRoot = (DocumentTreeNode) this.docControl_Xml.Document;
      this.documentTreeViewDlg_Xml.DocumentControl = this.docControl_Xml;
      this.documentTreeViewDlg_Xml.UpdateSelection();
    }
    this.documentTreeViewDlg_Xml.Show(this.dockMan_Xml, DockState.DockRight);
    this.documentTreeViewDlg_Xml.Closable = false;
  }

  /// <summary> Очистка контейнера в окне Xml </summary>
  private void CloseAllDocs_Xml()
  {
    for (int index = 0; index < this.docContainer_Xml.Documents.Length; ++index)
      this.docContainer_Xml.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementStr_Xml(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Xml != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Xml.FindNode(currId) ?? this.imDocument_template_Xml.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Xml.ResetTernBufer();
        this.documentTreeViewDlg_Xml.UpdateSelection();
      }
      else if (selection == null)
      {
        int num = (int) MessageBox.Show($"В шаблоне не найден элемент\r\n\r\n\"{currId}\"", "Ошибка!");
      }
      else
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Xml == null)
        return;
      this.docControl_Xml.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> Установить на ячейку в определенной строке </summary>
  /// <param textFromColumn="currId_Record"></param>
  /// <param textFromColumn="currId_Field"></param>
  private void SetElementStr_Xml2(string currId_Record, string currId_Field)
  {
    if (string.IsNullOrEmpty(currId_Record) && string.IsNullOrEmpty(currId_Field))
      this.SetElementStr_Xml("");
    else if (string.IsNullOrEmpty(currId_Record))
      this.SetElementStr_Xml(currId_Field);
    else if (string.IsNullOrEmpty(currId_Field))
    {
      this.SetElementStr_Xml(currId_Record);
    }
    else
    {
      DocumentTreeNode documentTreeNode = (this.imDocument_template_Xml.FindNode(currId_Record) ?? this.imDocument_template_Xml.FindFirstNodeByName(currId_Record)) ?? (DocumentTreeNode) this.imDocument_template_Xml;
      DocumentTreeNode selection = documentTreeNode.FindNode(currId_Field) ?? documentTreeNode.FindFirstNodeByName(currId_Field);
      if (selection != null)
      {
        this.docControl_Xml.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Xml.ResetTernBufer();
        this.documentTreeViewDlg_Xml.UpdateSelection();
      }
      else
        this.docControl_Xml.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementInt_Xml(int currId) => this.SetElementStr_Xml(currId.ToString());

  private void Draw_Page_Xml()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Xml = dbObject.ObjectID;
      this.templateID_curr_Xml = this.templateID_Xml;
    }
    this.TemplateId_Xml = this.templateID_curr_Xml;
    this.treeView_Xml_Draw();
    if (this._one_Ved_Nastr_Tmp._algorithmXml == null)
      return;
    this.numeric_UpDown_Xml_AfterInfo.Value = (Decimal) this._one_Ved_Nastr_Tmp._algorithmXml._afterInfo;
    this.numeric_UpDown_Xml_AfterRemark.Value = (Decimal) this._one_Ved_Nastr_Tmp._algorithmXml._afterRemark;
    switch (this._one_Ved_Nastr_Tmp._algorithmXml._passportIn)
    {
      case 0:
        this.radioButton_Xml_PassportInNo.Checked = true;
        break;
      case 1:
        this.radioButton_Xml_PassportInDialog.Checked = true;
        break;
      case 2:
        this.radioButton_Xml_PassporInAlways.Checked = true;
        break;
    }
    switch (this._one_Ved_Nastr_Tmp._algorithmXml._passportOut)
    {
      case 0:
        this.radioButton_Xml_PassportOutNo.Checked = true;
        break;
      case 1:
        this.radioButton_Xml_PassportOutDialog.Checked = true;
        break;
      case 2:
        this.radioButton_Xml_PassporOutAlways.Checked = true;
        break;
    }
    this.textBox_Xml_Folder_In.Text = this._one_Ved_Nastr_Tmp._algorithmXml._folderXmlIn;
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Xml_Draw()
  {
    this.treeView_Xml.Nodes.Clear();
    Vedomost_VB_Static.OneXmlNode oneXmlNode1 = new Vedomost_VB_Static.OneXmlNode();
    oneXmlNode1.Text = "Правила связи с XML";
    oneXmlNode1.ImageIndex = this._indexImageList_Section;
    oneXmlNode1.SelectedImageIndex = this._indexImageList_Section;
    Vedomost_VB_Static.OneXmlNode oneXmlNode2 = oneXmlNode1;
    oneXmlNode2._oneXmlNode_Parent = (Vedomost_VB_Static.OneXmlNode) null;
    oneXmlNode2._oneFieldXml = (Vedomost_VB.OneFieldXml) null;
    oneXmlNode2._oneRecordXml = (Vedomost_VB.OneRecordXml) null;
    oneXmlNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    this.treeView_Xml.Nodes.Add((TreeNode) oneXmlNode2);
    if (this._one_Ved_Nastr_Tmp._algorithmXml == null)
      return;
    oneXmlNode2._oneXmlNode_Parent = (Vedomost_VB_Static.OneXmlNode) null;
    oneXmlNode2._oneFieldXml = (Vedomost_VB.OneFieldXml) null;
    oneXmlNode2._oneRecordXml = (Vedomost_VB.OneRecordXml) null;
    oneXmlNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlPasport, "Основная надпись", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXml_Info, "Информационная", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitleIncluded, "Заголовок \"Ведомости составных частей\"", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlIncluded, "Ведомость составных частей", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitleVar, "Переменные данные для исполнений", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitleIsp, "Заголовок исполнения", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitle, "Заголовок", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitlePodSection, "Заголовок подраздела", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlRemark, "Примечание", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlRemarkShort, "Примечание короткое", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlTitlePart, "Заголовок части", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional1, "Дополнительная1", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional2, "Дополнительная2", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional3, "Дополнительная3", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlAdditional4, "Дополнительная4", oneXmlNode2);
    Vedomost_VB_Static.oneRecordNode_Xml_CreateAll(this._one_Ved_Nastr_Tmp._algorithmXml._oneRecordXmlEmpty, "Пустая", oneXmlNode2);
    oneXmlNode2.Expand();
    if (this.treeView_Xml.Nodes.Count <= 0 || this.treeView_Xml.Nodes[0].Nodes.Count <= 1)
      return;
    this.treeView_Xml.SelectedNode = this.treeView_Xml.Nodes[0].Nodes[1];
  }

  /// <summary> Что то указали на дереве </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void treeView_Xml_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string str1 = "";
    string currId_Field = "";
    string str2 = "";
    if (this.treeView_Xml.SelectedNode == this.treeView_Xml.Nodes[0])
    {
      if (this.treeView_Xml.Nodes[0].Nodes.Count <= 0)
        return;
      this.treeView_Xml.SelectedNode = this.treeView_Xml.Nodes[0].Nodes[0];
    }
    this.oneXmlNode_Curr = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Undefined)
    {
      this.treeView_Xml.SelectedNode = this.treeView_Xml.SelectedNode.Parent;
      this.oneXmlNode_Curr = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
    }
    if ((this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport) && this.oneXmlNode_Curr._oneRecordXml != null)
    {
      str1 = this.oneXmlNode_Curr._oneRecordXml._tableRowId;
      this.SetElementStr_Xml(str1);
      this.textBox_Xml_Text.Enabled = true;
      this.button_Xml_Edit.Enabled = true;
      this.button_Xml_Add.Enabled = true;
      this.button_Xml_Delete.Enabled = false;
    }
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (this.oneXmlNode_Curr._oneRecordXml != null)
        str1 = this.oneXmlNode_Curr._oneRecordXml._tableRowId;
      if (this.oneXmlNode_Curr._oneFieldXml != null)
        currId_Field = this.oneXmlNode_Curr._oneFieldXml._nameToFile;
      this.SetElementStr_Xml2(str1, currId_Field);
      str2 = this.oneXmlNode_Curr._oneFieldXml._nameToXml;
      this.textBox_Xml_Text.Enabled = true;
      this.button_Xml_Edit.Enabled = true;
      this.button_Xml_Add.Enabled = true;
      this.button_Xml_Delete.Enabled = true;
    }
    this.textBox_Xml_Text.Text = str2;
    this.text_Old = str2;
  }

  private void treeView_Xml_KeyDown(object sender, KeyEventArgs e)
  {
    e.KeyCode.Equals((object) Keys.Up);
    e.KeyCode.Equals((object) Keys.Down);
    if (!e.KeyCode.Equals((object) Keys.Delete) || this.oneXmlNode_Curr._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell)
      return;
    this.button_Xml_Delete_Click(sender, (EventArgs) e);
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Xml_Edit_Click(object sender, EventArgs e)
  {
    if (this.oneXmlNode_Curr == null)
      return;
    bool flag = false;
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && !Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
    if (activeElement == null || string.IsNullOrEmpty(activeElement.Id))
    {
      int num1 = (int) MessageBox.Show("Не выбран элемент на шаблоне", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      Vedomost_VB_Static.TypeNodeElement_Shablona nodeElementShablona = Vedomost_VB_Static.Check_tableElement(activeElement);
      int num2 = (int) Vedomost_VB_Static.Check_tableElement_Parent(activeElement);
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
      {
        int num3 = (int) MessageBox.Show("На шаблоне необходимо выбрать ячейку", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else if ((this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record || this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.RecordPasport) && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.RecordMain && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.RecordVtor && nodeElementShablona != Vedomost_VB_Static.TypeNodeElement_Shablona.Pasport)
      {
        int num4 = (int) MessageBox.Show("На шаблоне необходимо выбрать запись", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        if (nodeElementShablona == Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
        {
          int num5 = (int) Vedomost_VB_Static.Check_tableElement_Parent(activeElement);
        }
        Vedomost_VB.OneFieldXml oneFieldXml = (Vedomost_VB.OneFieldXml) null;
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
          oneFieldXml = this.oneXmlNode_Curr._oneFieldXml;
        Vedomost_VB.OneRecordXml oneRecordXml = this.oneXmlNode_Curr._oneRecordXml;
        if (!Vedomost_VB_Static.control_Douplication(oneRecordXml, oneFieldXml, this.textBox_Xml_Text.Text, activeElement.Name, true))
          return;
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
        {
          if (!Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
            return;
          string nameToFile = oneFieldXml._nameToFile;
          if (this.text_Old != this.textBox_Xml_Text.Text)
          {
            oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
            this.oneXmlNode_Curr.Text = this.oneXmlNode_Curr.Text.Replace(this.text_Old, this.textBox_Xml_Text.Text);
            this.text_Old = this.textBox_Xml_Text.Text;
            flag = true;
          }
          if (nameToFile != activeElement.Id)
          {
            this.oneXmlNode_Curr._oneFieldXml._nameToFile = activeElement.Id;
            this.oneXmlNode_Curr.Text = Vedomost_VB_Static.Text_oneFieldXml(this.oneXmlNode_Curr._oneFieldXml._nameToFile, this.oneXmlNode_Curr._oneFieldXml._nameToXml, this.oneXmlNode_Curr._lBases);
            flag = true;
          }
        }
        if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
        {
          string tableRowId = oneRecordXml._tableRowId;
          if (tableRowId != activeElement.Id)
          {
            this.oneXmlNode_Curr._oneRecordXml._tableRowId = activeElement.Id;
            this.oneXmlNode_Curr.Text = this.oneXmlNode_Curr.Text.Replace(tableRowId, activeElement.Id);
            flag = true;
          }
        }
        if (!flag)
          return;
        this.ModifiedAll(true);
        this.IsModified_Page_Xml = true;
      }
    }
  }

  /// <summary> Нажатие ENTER в textBox_Xml_Text </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void textBox_Xml_Text_KeyDown(object sender, KeyEventArgs e)
  {
    if (!e.KeyCode.Equals((object) Keys.Return) || !Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      this.button_Xml_Add_Click(sender, (EventArgs) e);
    }
    else
    {
      Vedomost_VB_Static.OneXmlNode selectedNode = (Vedomost_VB_Static.OneXmlNode) this.treeView_Xml.SelectedNode;
      Vedomost_VB.OneFieldXml oneFieldXml = selectedNode._oneFieldXml;
      if (!Vedomost_VB_Static.control_Douplication(selectedNode._oneRecordXml, oneFieldXml, this.textBox_Xml_Text.Text, (string) null, false))
        return;
      selectedNode._oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
      selectedNode.Text = selectedNode.Text.Replace(this.text_Old, this.textBox_Xml_Text.Text);
      this.text_Old = this.textBox_Xml_Text.Text;
      this.ModifiedAll(true);
      this.IsModified_Page_Xml = true;
    }
  }

  /// <summary> Проверка текста имени Xml "На лету" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void textBox_Xml_Text_TextChanged(object sender, EventArgs e)
  {
    Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text);
  }

  /// <summary>  Кнопка ДОБАВИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Xml_Add_Click(object sender, EventArgs e)
  {
    if (!Vedomost_VB_Static.name_Field_Xml_Check(this.textBox_Xml_Text.Text, this.textBox_Xml_Text))
      return;
    DocumentTreeNode activeElement = this.docControl_Xml.ActiveElement;
    if (activeElement == null || string.IsNullOrEmpty(activeElement.Id))
    {
      int num1 = (int) MessageBox.Show("Не выбран элемент на шаблоне", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (Vedomost_VB_Static.Check_tableElement(activeElement) != Vedomost_VB_Static.TypeNodeElement_Shablona.Cell)
    {
      int num2 = (int) MessageBox.Show("На шаблоне необходимо выбрать ячейку", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      Vedomost_VB.OneFieldXml oneFieldXml = new Vedomost_VB.OneFieldXml();
      oneFieldXml._nameToXml = this.textBox_Xml_Text.Text;
      oneFieldXml._nameToFile = activeElement.Name;
      oneFieldXml._typeDataToXml = Vedomost_VB.TypeDataToXml.Field;
      Vedomost_VB_Static.OneXmlNode node = Vedomost_VB_Static.oneFieldNode_Xml_Create(this.oneXmlNode_Curr._oneRecordXml, oneFieldXml, this.oneXmlNode_Curr, this.oneXmlNode_Curr._lBases, this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Count - 1);
      if (node == null)
        return;
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
      {
        this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Insert(this.oneXmlNode_Curr._iData + 1, oneFieldXml);
        this.oneXmlNode_Curr.Parent.Nodes.Insert(this.oneXmlNode_Curr.Index + 1, (TreeNode) node);
        this.treeView_Xml.SelectedNode = (TreeNode) node;
      }
      if (this.oneXmlNode_Curr._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
      {
        this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Add(oneFieldXml);
        this.oneXmlNode_Curr.Nodes.Insert(this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Count - 1, (TreeNode) node);
        this.treeView_Xml.SelectedNode = (TreeNode) node;
      }
      this.ModifiedAll(true);
      this.IsModified_Page_Xml = true;
    }
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Xml_Delete_Click(object sender, EventArgs e)
  {
    if (this.oneXmlNode_Curr == null)
      return;
    this.oneXmlNode_Curr._oneRecordXml._listOneFieldXml.Remove(this.oneXmlNode_Curr._oneFieldXml);
    this.oneXmlNode_Curr.Parent.Nodes.Remove((TreeNode) this.oneXmlNode_Curr);
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Пропускать строк после информационной </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numeric_UpDown_Xml_AfterInfo_ValueChanged(object sender, EventArgs e)
  {
    this._one_Ved_Nastr_Tmp._algorithmXml._afterInfo = (int) this.numeric_UpDown_Xml_AfterInfo.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Пропускать строк после примечания </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void numeric_UpDown_Xml_AfterRemark_ValueChanged(object sender, EventArgs e)
  {
    this._one_Ved_Nastr_Tmp._algorithmXml._afterRemark = (int) this.numeric_UpDown_Xml_AfterRemark.Value;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Изменение ввода основной надписи </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Xml_PassporIn(object sender, MouseEventArgs e)
  {
    if (this.radioButton_Xml_PassporInAlways.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportIn = 2;
    else if (this.radioButton_Xml_PassportInDialog.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportIn = 1;
    else if (this.radioButton_Xml_PassportInNo.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportIn = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Изменение вывода основной надписи </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Xml_PassporOut(object sender, MouseEventArgs e)
  {
    if (this.radioButton_Xml_PassporOutAlways.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportOut = 2;
    else if (this.radioButton_Xml_PassportOutDialog.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportOut = 1;
    else if (this.radioButton_Xml_PassportOutNo.Checked)
      this._one_Ved_Nastr_Tmp._algorithmXml._passportOut = 0;
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Окончание редактирования строки </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void textBox_Xml_Folder_In_Leave(object sender, EventArgs e)
  {
    if (!Vedomost_VB_Static.check_Text_For_Filename(this.textBox_Xml_Folder_In.Text, this.textBox_Xml_Folder_In) || !Vedomost_VB_Static.check_Text_For_FilenameExists(this.textBox_Xml_Folder_In.Text, this.textBox_Xml_Folder_In))
      return;
    this._one_Ved_Nastr_Tmp._algorithmXml._folderXmlIn = this.textBox_Xml_Folder_In.Text.Trim();
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Нажатие кнопки выбора папки </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Xml_Folder_In_Click(object sender, EventArgs e)
  {
    string str = "";
    try
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        folderBrowserDialog.SelectedPath = this._one_Ved_Nastr_Tmp._algorithmXml._folderXmlIn;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
        {
          str = folderBrowserDialog.SelectedPath;
          if (!string.IsNullOrEmpty(str))
            this.textBox_Xml_Folder_In.Text = str;
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show("Ошибка\r\n\r\n" + str, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    this._one_Ved_Nastr_Tmp._algorithmXml._folderXmlIn = this.textBox_Xml_Folder_In.Text.Trim();
    this.ModifiedAll(true);
    this.IsModified_Page_Xml = true;
  }

  /// <summary> Рисование страницы СЕРВИС </summary>
  private void Draw_Page_Service()
  {
    if (this._one_Ved_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.buttonServicesTypeVedTo.Visible = false;
      this.label_ServicesTypeVedTo.Visible = false;
      this.labelService1.Text = "Ведомость системная";
      this.labelService2.Text = "";
    }
    else
    {
      this.buttonServicesTypeVedTo.Visible = true;
      this.label_ServicesTypeVedTo.Visible = true;
      this.labelService1.Text = "Ведомость пользовательская";
      this.labelService2.Text = "";
      this.buttonServicesTypeVedTo.Visible = true;
      this.label_ServicesTypeVedTo.Visible = true;
      string str = Vedomost_VB_Static.TypeVed_string(this._one_Ved_Nastr_Tmp._typeVed);
      if (!string.IsNullOrEmpty(str))
      {
        this.labelService2.Visible = true;
        this.labelService2.Text = "Аналог: " + str;
      }
      else
      {
        this.labelService2.Text = "";
        this.labelService2.Visible = false;
      }
    }
    if (Vedomost_VB_Static.isCreateDump_Tmp)
    {
      this.checkBox_Services_CreateDump.Checked = true;
      this.checkBox_Services_CreateDump.Enabled = true;
      this.label_DumpFolder.Visible = true;
      this.label_DumpFolder.Text = Vedomost_VB_Static.DirectoryDump;
    }
    if (Vedomost_VB_Static.isCreateDump_System)
    {
      Vedomost_VB_Static.isCreateDump_Tmp = true;
      this.checkBox_Services_CreateDump.Checked = true;
      this.checkBox_Services_CreateDump.Enabled = false;
      this.checkBox_Services_CreateDump.Text = "Создавать протоколы и Dump в текущем сеансе работы (включено постоянно в системной переменной)";
      this.label_DumpFolder.Visible = true;
      this.label_DumpFolder.Text = Vedomost_VB_Static.DirectoryDump;
    }
    if (this._one_Ved_Nastr_Tmp._accessLevel == 0)
      this.radioButton_AccessLevel0.Checked = true;
    else if (this._one_Ved_Nastr_Tmp._accessLevel == 1)
      this.radioButton_AccessLevel1.Checked = true;
    else
      this.radioButton_AccessLevel2.Checked = true;
    this.checkBox_Services_isCreateDumpAuto.Checked = this._one_Ved_Nastr_Tmp._isCreateDumpAuto != 0;
    if (this._one_Ved_Nastr_Tmp._autoSbor == 0)
    {
      this.checkBox_Services_autoSbor.Checked = false;
      this.HidePages(true);
    }
    else
    {
      this.checkBox_Services_autoSbor.Checked = true;
      this.HidePages(false);
    }
  }

  /// <summary> Пользовательской ведомости присвоить какой либо системный тип </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonTypeVedTo_Click(object sender, EventArgs e)
  {
    using (VyborVedomosti_withIni vedomostiWithIni = new VyborVedomosti_withIni())
    {
      vedomostiWithIni.List_Type_Systems = Vedomost_VB_Static.List_TypeVed_Systems;
      if (vedomostiWithIni.ShowDialog() != DialogResult.OK)
        return;
      this._one_Ved_Nastr_Tmp._typeVed = vedomostiWithIni.typeVed_result;
      this._one_Ved_Nastr_Tmp._nameVed = this._imsObjectType_Curr.ObjectName;
      this.Draw_Page_Service();
      this.IsButtonB();
      this.isByloButtonTypeVedTo_Click = true;
      this.IsButtonDefault();
      this.buttonDefault.Visible = false;
      this.isCreate = false;
      this.ModifiedAll(true);
    }
  }

  /// <summary> Кнопка "Создать Dump" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonServiceDump_Click(object sender, EventArgs e)
  {
    try
    {
      Vedomost_VB_Static.Checking_FOLDER_ForDump();
      if (string.IsNullOrEmpty(Vedomost_VB_Static.DirectoryDump) || !Directory.Exists(Vedomost_VB_Static.DirectoryDump))
        return;
      Vedomost_VB_Static.CleaningDirectoryDumpVed();
      this.Saving_Pages();
      Vedomost_VB_Static.OneVedNastrToDump(this._one_Ved_Nastr_Tmp);
      Vedomost_VB_Static.ShablonToDump(this._one_Ved_Nastr_Tmp);
      if (Vedomost_VB_Static.xmlProtocol_Last != null)
        Vedomost_VB_Static.xmlProtocol_Last.Save(Vedomost_VB_Static.DirectoryDump + "\\Protocol.xml");
      if (Vedomost_VB_Static.xml_SborMainVed_Dump_Last != null)
        Vedomost_VB_Static.xml_SborMainVed_Dump_Last.Save(Vedomost_VB_Static.DirectoryDump + "\\SborMainVed_Dump.xml");
      if (Vedomost_VB_Static.xml_SborVed_Dump_Last != null)
        Vedomost_VB_Static.xml_SborVed_Dump_Last.Save(Vedomost_VB_Static.DirectoryDump + "\\SborVed_Dump.xml");
      if (Vedomost_VB_Static.imDocument != null)
      {
        string textIn1 = Vedomost_VB_Static.DirectoryDump + "\\Vedomost.pdf";
        string textIn2 = Vedomost_VB_Static.DirectoryDump + "\\Vedomost.imdx";
        string fileName1 = Vedomost_VB_Static.Replace_Invalid_Char(textIn1, true);
        string fileName2 = Vedomost_VB_Static.Replace_Invalid_Char(textIn2, true);
        Vedomost_VB_Static.imDocument.SaveToPdf(fileName1);
        Vedomost_VB_Static.imDocument.SaveToXml(fileName2, false);
      }
      Vedomost_VB_Static.AboutToFile();
      if (MessageBox.Show($"Параметры настройки и шаблон сохранены в файлы.\r\n\r\nПапка\r\n{Vedomost_VB_Static.DirectoryDump}\r\n\r\nОткрыть эту папку?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      Process.Start(Vedomost_VB_Static.DirectoryDump);
    }
    catch
    {
      int num = (int) MessageBox.Show("Создать файлы для Dump не удалось.\r\n\r\nПапка\r\n" + Vedomost_VB_Static.DirectoryDump, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  /// <summary> Открыть папку Dump </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_OpenDumpFolder_Click(object sender, EventArgs e)
  {
    Process.Start(Vedomost_VB_Static.DirectoryDump);
  }

  /// <summary> Кнопка "Читать из файла" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonServicesFileOpen_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.RestoreDirectory = true;
    openFileDialog.Filter = "Ini файлы (*.xml)|*.xml";
    openFileDialog.DefaultExt = "xml";
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    string fileName = openFileDialog.FileName;
    string str = Vedomost_VB_Static.FileName_Template_For_FileName_Nastr(fileName);
    this.imDocument_template_Vyvod_FromDump = (ImDocument) null;
    if (!string.IsNullOrEmpty(str) && File.Exists(str))
      this.imDocument_template_Vyvod_FromDump = ImDocument.LoadFromFile(str, out DocumentFileType _, false) as ImDocument;
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(fileName);
    if (xmlDocument.DocumentElement.Name.ToUpper() != "ONE_VED_NASTR")
    {
      int num1 = (int) MessageBox.Show($"Файл\r\n\r\n{fileName}\r\n\r\nНе файл настройки", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      string xmlAttribute1 = Vedomost_VB_Static.GetXmlAttribute(xmlDocument, "_typeDoc");
      if (xmlAttribute1 != "Ved" && xmlAttribute1 != "Espd")
      {
        int num2 = (int) MessageBox.Show($"Файл\r\n\r\n{fileName}\r\n\r\nНе файл настройки ведомости", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else if (Vedomost_VB_Static.GetXmlAttribute(xmlDocument, "typeVed") != this._one_Ved_Nastr_Tmp._typeVed.ToString())
      {
        int num3 = (int) MessageBox.Show($"Файл\r\n\r\n{fileName}\r\n\r\nНесовместимые типы ведомости", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
      {
        string xmlAttribute2 = Vedomost_VB_Static.GetXmlAttribute(xmlDocument, "typeCreate");
        if (this._one_Ved_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System && xmlAttribute2 != "System")
        {
          int num4 = (int) MessageBox.Show("Текущая настройка имеет статус СИСТЕМНАЯ\r\n\r\nНастрока выбранного файла не системная\r\n\r\nНесовместимые типы", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        else
        {
          Vedomost_VB.TypeCreate typeCreate = this._one_Ved_Nastr_Tmp._typeCreate;
          string nameVed = this._one_Ved_Nastr_Tmp._nameVed;
          this._one_Ved_Nastr_Tmp = new One_Ved_Nastr();
          this._one_Ved_Nastr_Tmp.Filled_One_Ved_Nastr_FromXml(xmlDocument);
          this._one_Ved_Nastr_Tmp._typeCreate = typeCreate;
          this._one_Ved_Nastr_Tmp._nameVed = nameVed;
          this._imsObjectType_Curr = MetaDataHelper.GetObjectType(this._one_Ved_Nastr_Tmp._guidTypeVed);
          this._one_Ved_Nastr_Tmp._imsObjectType = this._imsObjectType_Curr;
          this._one_Ved_Nastr_Tmp._imsObjectType = this._imsObjectType_Curr;
          if (this._imsObjectType_Curr != null)
          {
            this._guidTypeVed_Curr = this._one_Ved_Nastr_Tmp._guidTypeVed;
            this._guidTemplateVed_Curr = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid;
          }
          else
          {
            this._one_Ved_Nastr_Tmp._typeCreate = Vedomost_VB.TypeCreate.User;
            this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid = this._one_Ved_Nastr_Curr._vedomostTemplateObjectGuid;
          }
          this.Text = "Настройка ведомости:";
          if (this._imsObjectType_Curr != null)
            this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
          else
            this.Text = $"{this.Text} [{this._one_Ved_Nastr_Tmp._nameVed}]";
          if (this._one_Ved_Nastr_Curr._dateIni != "")
            this.Text = $"{this.Text} {this._one_Ved_Nastr_Curr._dateIni}";
          this.Text = $"{this.Text}: Настройки изменены данными из файла (Dump): {fileName}";
          this.ModifiedAll(true);
          this.isCreate = true;
          this.dataGridView_Sorting_Curr.Rows.Clear();
          this.Razdels_dataGridViewListRazdels.Rows.Clear();
          this.dataGridView_ListZagolovkov.Rows.Clear();
          this.Draw_All();
          this.isCreate = false;
          this.ModifiedAll(true);
          this.tabControl_Usl_Bases.SelectedTab = this.tabPage_Bases_Main;
          this.tabControl_Page_Sbor.SelectedTab = this.tabPage_Sbor_Usl;
          this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
          this.IsModifiedFromFile = true;
        }
      }
    }
  }

  private void buttonSevicesForGroupB_Click(object sender, EventArgs e)
  {
    using (VyborShablona vyborShablona = new VyborShablona())
    {
      vyborShablona._imsObjectTypeCurr = this._imsObjectType_Curr;
      if (vyborShablona.ShowDialog() != DialogResult.OK)
        return;
      this.isCreate = true;
      this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B = vyborShablona._quickObjectInfo_Result.VersionGuid;
      this.Draw_All();
      this.isCreate = false;
      this.ModifiedAll(true);
    }
  }

  private void checkBox_Services_CreateDump_CheckedChanged(object sender, EventArgs e)
  {
    if (this.checkBox_Services_CreateDump.Checked)
    {
      Vedomost_VB_Static.isCreateDump_Tmp = true;
      this.button_OpenDumpFolder.Visible = true;
      this.label_DumpFolder.Visible = true;
      this.label_DumpFolder.Text = Vedomost_VB_Static.DirectoryDump;
    }
    else
    {
      Vedomost_VB_Static.isCreateDump_Tmp = false;
      this.button_OpenDumpFolder.Visible = false;
      this.label_DumpFolder.Visible = false;
      this.label_DumpFolder.Text = "";
    }
  }

  private void Saving_Page_Service()
  {
    if (this.radioButton_AccessLevel0.Checked)
      this._one_Ved_Nastr_Tmp._accessLevel = 0;
    else if (this.radioButton_AccessLevel1.Checked)
      this._one_Ved_Nastr_Tmp._accessLevel = 1;
    else if (this.radioButton_AccessLevel2.Checked)
      this._one_Ved_Nastr_Tmp._accessLevel = 2;
    this._one_Ved_Nastr_Tmp._autoSbor = !this.checkBox_Services_autoSbor.Checked ? 0 : 1;
    if (!this.IsModified_Page_Vyvod)
      return;
    this.IsBylo_IsModified_Page_Vyvod = true;
  }

  private void checkBox_Services_isCreateDumpAuto_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Tmp._isCreateDumpAuto = !this.checkBox_Services_isCreateDumpAuto.Checked ? 0 : 1;
    this.IsModified_Page_Service = true;
  }

  private void radioButton_AccessLevel0_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Curr._accessLevel = 0;
    this.IsModified_Page_Service = true;
  }

  private void radioButton_AccessLevel1_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Curr._accessLevel = 1;
    this.IsModified_Page_Service = true;
  }

  private void radioButton_AccessLevel2_MouseClick(object sender, MouseEventArgs e)
  {
    this.ModifiedAll(true);
    this._one_Ved_Nastr_Curr._accessLevel = 2;
    this.IsModified_Page_Service = true;
  }

  /// <summary> Переключатель Создавать ведомость автоматически </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void checkBox_Services_autoSbor_MouseClick(object sender, MouseEventArgs e)
  {
    if (this.checkBox_Services_autoSbor.Checked)
    {
      this._one_Ved_Nastr_Tmp._autoSbor = 1;
      this.HidePages(false);
      this.checkBox_isFullProhibition.Visible = true;
    }
    else
    {
      this._one_Ved_Nastr_Tmp._autoSbor = 0;
      this.HidePages(true);
      this.checkBox_isFullProhibition.Visible = false;
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Service = true;
  }

  private long TemplateId_Avs
  {
    get => this.templID_Avs;
    set
    {
      this.templID_Avs = this.templID_Vyvod;
      this.CloseAllDocs_Avs();
      if (this.templID_Avs == 0L || this.templID_Avs == -1L)
        return;
      this.imDocument_template_Avs = this.LoadTemplateFromObject(this.templID_Avs);
      this.OpenTemplate_Avs(this.imDocument_template_Avs);
    }
  }

  private void OpenTemplate_Avs(ImDocument doc)
  {
    this.CloseAllDocs_Avs();
    this.docControl_Avs = new DocumentControl();
    this.docControl_Avs.Document = doc;
    double num = (double) this.docControl_Avs.SetZoom(DocZoomMode.FitPage, 0.0f);
    this.docKcontrol_Avs = new DockControl((Control) this.docControl_Avs, this.docName);
    this.docKcontrol_Avs.Show(this.dockMan_Avs, DockState.Document);
    this.docKcontrol_Avs.Closable = false;
    this.ShowDocumentTreeView_Avs();
    this.docKcontrol_Avs.Select();
    this.docControl_Avs.ReadOnly = true;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg_Avs
  {
    get
    {
      return this.documentTreeViewDlg_Avs != null && !this.documentTreeViewDlg_Avs.IsDisposed ? this.documentTreeViewDlg_Avs : (DocumentTreeViewDlg) null;
    }
  }

  public void ShowDocumentTreeView_Avs()
  {
    if (this.DocumentTreeViewDlg_Avs == null)
      this.documentTreeViewDlg_Avs = new DocumentTreeViewDlg();
    if (this.docControl_Avs != null)
    {
      DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
      this.documentTreeViewDlg_Avs.TreeRoot = (DocumentTreeNode) this.docControl_Avs.Document;
      this.documentTreeViewDlg_Avs.DocumentControl = this.docControl_Avs;
      this.documentTreeViewDlg_Avs.UpdateSelection();
    }
    this.documentTreeViewDlg_Avs.Show(this.dockMan_Avs, DockState.DockRight);
    this.documentTreeViewDlg_Avs.Closable = false;
  }

  /// <summary> Очистка контейнера в окне Avs </summary>
  private void CloseAllDocs_Avs()
  {
    for (int index = 0; index < this.docContainer_Avs.Documents.Length; ++index)
      this.docContainer_Avs.Documents[index].Close();
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementStr_Avs(string currId)
  {
    if (currId != null && currId != "" && this.imDocument_template_Avs != null)
    {
      DocumentTreeNode selection = this.imDocument_template_Avs.FindNode(currId) ?? this.imDocument_template_Avs.FindFirstNodeByName(currId);
      if (selection != null)
      {
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Avs.ResetTernBufer();
        this.documentTreeViewDlg_Avs.UpdateSelection();
      }
      else
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
    }
    else
    {
      if (this.imDocument_template_Avs == null)
        return;
      this.docControl_Avs.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> Установить на ячейку в определенной строке </summary>
  /// <param textFromColumn="currId_Record"></param>
  /// <param textFromColumn="currId_Field"></param>
  private void SetElementStr_Avs2(string currId_Record, string currId_Field)
  {
    if (string.IsNullOrEmpty(currId_Record) && string.IsNullOrEmpty(currId_Field))
      this.SetElementStr_Avs("");
    else if (string.IsNullOrEmpty(currId_Record))
      this.SetElementStr_Avs(currId_Field);
    else if (string.IsNullOrEmpty(currId_Field))
    {
      this.SetElementStr_Avs(currId_Record);
    }
    else
    {
      DocumentTreeNode documentTreeNode = (this.imDocument_template_Avs.FindNode(currId_Record) ?? this.imDocument_template_Avs.FindFirstNodeByName(currId_Record)) ?? (DocumentTreeNode) this.imDocument_template_Avs;
      DocumentTreeNode selection = documentTreeNode.FindFirstChildNodeByName(currId_Field) ?? documentTreeNode.FindFirstNodeByName(currId_Field);
      if (selection != null)
      {
        this.docControl_Avs.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl_Avs.ResetTernBufer();
        this.documentTreeViewDlg_Avs.UpdateSelection();
      }
      else
        this.docControl_Avs.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  /// <param textFromColumn="currId"></param>
  private void SetElementInt_Avs(int currId) => this.SetElementStr_Avs(currId.ToString());

  private void Draw_Page_Avs()
  {
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      return;
    this.algorithm_Avs = this._one_Ved_Nastr_Tmp._algorithm_Avs6_To_Ips;
    if (this._one_Ved_Nastr_Tmp._algorithm_Avs6_To_Ips_B != null)
    {
      Guid templateObjectGuidB = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      this.algorithm_Avs_B = this._one_Ved_Nastr_Tmp._algorithm_Avs6_To_Ips_B;
      this.groupBox_Avs_Forma.Visible = true;
      this.est_Avs_GroupB = true;
    }
    else
    {
      this.algorithm_Avs_B = (Vedomost_VB.Algorithm_Avs6_To_Ips) null;
      this.groupBox_Avs_Forma.Visible = false;
      this.est_Avs_GroupB = false;
    }
    this.algorithm_Avs_curr = this.algorithm_Avs;
    this.Draw_listBox_Avs6_Fields();
    if (this.listBox_Avs6_Fields.Items.Count > 0)
      this.listBox_Avs6_Fields.SelectedIndex = 0;
    else
      this.listBox_Avs6_Fields.SelectedIndex = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = (IDBObject) null;
      if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
        dbObject = sessionKeeper.Session.GetObject(this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid, false);
      if (dbObject != null)
        this.templateID_Avs = dbObject.ObjectID;
      this.templateID_curr_Avs = this.templateID_Avs;
    }
    this.radioButton_Avs_EdOrA.Checked = true;
    this.radioButton_Avs_GroupB.Checked = false;
    if (this.est_Avs_GroupB)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = (IDBObject) null;
        if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B != Guid.Empty)
          dbObject = sessionKeeper.Session.GetObject(this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B, false);
        if (dbObject != null)
          this.templateID_B_Avs = dbObject.ObjectID;
      }
    }
    this.TemplateId_Avs = this.templateID_curr_Avs;
    this.treeView_Avs_Draw();
  }

  private void Draw_listBox_Avs6_Fields()
  {
    this.listBox_Avs6_Fields.Items.Clear();
    if (AVS6_From_Avs6Main._list_recordFields == null || AVS6_From_Avs6Main._list_recordFields.Count == 0)
      return;
    for (int index = 0; index < AVS6_From_Avs6Main._list_recordFields.Count; ++index)
      this.listBox_Avs6_Fields.Items.Add((object) AVS6_From_Avs6Main._list_recordFields[index]._fieldName_Avs6);
  }

  /// <summary> Рисование ДЕРЕВА выводимых полей </summary>
  private void treeView_Avs_Draw()
  {
    this.treeView_Avs.Nodes.Clear();
    this.treeView_Avs.Nodes.Clear();
    if (this.algorithm_Avs_curr == null)
      return;
    bool flag1 = this.imDocument_template_Avs.FindNode("Подтаблица Куда входит") != null;
    bool flag2 = this.imDocument_template_Avs.FindNode("Строка Кол итого") != null;
    A_NastrVed.OneAvsNode oneAvsNode1 = new A_NastrVed.OneAvsNode();
    oneAvsNode1.Text = "Правила вывода на шаблон: Главная таблица";
    oneAvsNode1.ImageIndex = this._indexImageList_Section;
    oneAvsNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode oneAvsNode2 = oneAvsNode1;
    oneAvsNode2._oneAvsNode_Parent = (A_NastrVed.OneAvsNode) null;
    oneAvsNode2._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode2._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode2._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Main;
    this.treeView_Avs.Nodes.Add((TreeNode) oneAvsNode2);
    A_NastrVed.OneAvsNode oneAvsNode3 = new A_NastrVed.OneAvsNode();
    oneAvsNode3.Text = "Информационные записи: ";
    oneAvsNode3.ImageIndex = this._indexImageList_Section;
    oneAvsNode3.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode oneAvsNode4 = oneAvsNode3;
    oneAvsNode4._oneAvsNode_Parent = (A_NastrVed.OneAvsNode) null;
    oneAvsNode4._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode4._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode4._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode4._typeNode = Vedomost_VB_Static.TypeNode_Tree.Info;
    oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode4);
    if (this.algorithm_Avs_curr != null)
    {
      if (this.algorithm_Avs_curr != null && this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips != null && this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Count > 0)
      {
        for (int index = 0; index < this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Count; ++index)
        {
          Vedomost_VB.OneRazdel_Avs6_To_Ips oneRazdelAvs6ToIp = this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips[index];
          if (oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info != null)
          {
            A_NastrVed.OneAvsNode oneAvsNode5 = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info, oneAvsNode4, oneRazdelAvs6ToIp._razdelVed);
            if (oneAvsNode5 != null)
            {
              oneAvsNode4.Nodes.Add((TreeNode) oneAvsNode5);
              if (flag1 && oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor != null)
              {
                A_NastrVed.OneAvsNode node = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode5);
                if (node != null)
                  oneAvsNode5.Nodes.Add((TreeNode) node);
              }
              if (flag2 && oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo != null)
              {
                A_NastrVed.OneAvsNode node = this.oneRecordNode_Avs_Create(oneRazdelAvs6ToIp._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode5);
                if (node != null)
                  oneAvsNode5.Nodes.Add((TreeNode) node);
              }
            }
          }
        }
        this.button_Avs_PoRazdelam.Visible = false;
        this.button_Avs_Obshaia.Visible = true;
      }
      else
      {
        if (this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info != null)
        {
          A_NastrVed.OneAvsNode oneAvsNode6 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info, oneAvsNode4);
          if (oneAvsNode6 != null)
          {
            oneAvsNode4.Nodes.Add((TreeNode) oneAvsNode6);
            if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor != null)
            {
              A_NastrVed.OneAvsNode node = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode6);
              if (node != null)
                oneAvsNode6.Nodes.Add((TreeNode) node);
            }
            if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo != null)
            {
              A_NastrVed.OneAvsNode node = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode6);
              if (node != null)
                oneAvsNode6.Nodes.Add((TreeNode) node);
            }
            oneAvsNode6.Expand();
          }
        }
        this.button_Avs_PoRazdelam.Visible = true;
        this.button_Avs_Obshaia.Visible = false;
      }
    }
    if (this._one_Ved_Nastr_Tmp._list_RazdelsVed == null || this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count < 3)
    {
      this.button_Avs_PoRazdelam.Visible = false;
      this.button_Avs_Obshaia.Visible = false;
    }
    oneAvsNode4.Expand();
    A_NastrVed.OneAvsNode oneAvsNode7 = new A_NastrVed.OneAvsNode();
    oneAvsNode7.Text = "Ведомости составных частей: ";
    oneAvsNode7.ImageIndex = this._indexImageList_Section;
    oneAvsNode7.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode node1 = oneAvsNode7;
    node1._oneAvsNode_Parent = (A_NastrVed.OneAvsNode) null;
    node1._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    node1._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    node1._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    node1._typeNode = Vedomost_VB_Static.TypeNode_Tree.VedSost;
    if (this.algorithm_Avs_curr != null)
    {
      A_NastrVed.OneAvsNode node2 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitleIncluded, oneAvsNode2);
      if (node2 != null)
        node1.Nodes.Add((TreeNode) node2);
    }
    A_NastrVed.OneAvsNode oneAvsNode8 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Included, oneAvsNode2);
    if (oneAvsNode8 != null)
    {
      node1.Nodes.Add((TreeNode) oneAvsNode8);
      if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Included._oneRecord_Avs6_To_Ips_Vtor != null)
      {
        A_NastrVed.OneAvsNode node3 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Included._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode8);
        if (node3 != null)
          oneAvsNode8.Nodes.Add((TreeNode) node3);
      }
      if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Included._oneRecord_Avs6_To_Ips_Itogo != null)
      {
        A_NastrVed.OneAvsNode node4 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Included._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode8);
        if (node4 != null)
          oneAvsNode8.Nodes.Add((TreeNode) node4);
      }
      oneAvsNode8.Expand();
    }
    oneAvsNode2.Nodes.Add((TreeNode) node1);
    node1.Expand();
    A_NastrVed.OneAvsNode node5 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Title, oneAvsNode2);
    if (node5 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node5);
    A_NastrVed.OneAvsNode node6 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitlePodSection, oneAvsNode2);
    if (node6 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node6);
    A_NastrVed.OneAvsNode node7 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitleVar, oneAvsNode2);
    if (node7 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node7);
    A_NastrVed.OneAvsNode node8 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_TitleIsp, oneAvsNode2);
    if (node8 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node8);
    A_NastrVed.OneAvsNode node9 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Remark, oneAvsNode2);
    if (node9 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node9);
    A_NastrVed.OneAvsNode node10 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_RemarkShort, oneAvsNode2);
    if (node10 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node10);
    if (this.algorithmToPrint_curr._additional1 == 1)
    {
      A_NastrVed.OneAvsNode oneAvsNode9 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1, oneAvsNode2);
      if (oneAvsNode9 != null)
      {
        oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode9);
        if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          A_NastrVed.OneAvsNode node11 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode9);
          if (node11 != null)
            oneAvsNode9.Nodes.Add((TreeNode) node11);
        }
        if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          A_NastrVed.OneAvsNode node12 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional1._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode9);
          if (node12 != null)
            oneAvsNode9.Nodes.Add((TreeNode) node12);
        }
      }
    }
    if (this.algorithmToPrint_curr._additional2 == 1)
    {
      A_NastrVed.OneAvsNode oneAvsNode10 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2, oneAvsNode2);
      if (oneAvsNode10 != null)
      {
        oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode10);
        if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          A_NastrVed.OneAvsNode node13 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode10);
          if (node13 != null)
            oneAvsNode10.Nodes.Add((TreeNode) node13);
        }
        if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          A_NastrVed.OneAvsNode node14 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional2._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode10);
          if (node14 != null)
            oneAvsNode10.Nodes.Add((TreeNode) node14);
        }
      }
    }
    if (this.algorithmToPrint_curr._additional3 == 1)
    {
      A_NastrVed.OneAvsNode oneAvsNode11 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3, oneAvsNode2);
      if (oneAvsNode11 != null)
      {
        oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode11);
        if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          A_NastrVed.OneAvsNode node15 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode11);
          if (node15 != null)
            oneAvsNode11.Nodes.Add((TreeNode) node15);
        }
        if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          A_NastrVed.OneAvsNode node16 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional3._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode11);
          if (node16 != null)
            oneAvsNode11.Nodes.Add((TreeNode) node16);
        }
      }
    }
    if (this.algorithmToPrint_curr._additional4 == 1)
    {
      A_NastrVed.OneAvsNode oneAvsNode12 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4, oneAvsNode2);
      if (oneAvsNode12 != null)
      {
        oneAvsNode2.Nodes.Add((TreeNode) oneAvsNode12);
        if (flag1 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4._oneRecord_Avs6_To_Ips_Vtor != null)
        {
          A_NastrVed.OneAvsNode node17 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4._oneRecord_Avs6_To_Ips_Vtor, oneAvsNode12);
          if (node17 != null)
            oneAvsNode12.Nodes.Add((TreeNode) node17);
        }
        if (flag2 && this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4._oneRecord_Avs6_To_Ips_Itogo != null)
        {
          A_NastrVed.OneAvsNode node18 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Additional4._oneRecord_Avs6_To_Ips_Itogo, oneAvsNode12);
          if (node18 != null)
            oneAvsNode12.Nodes.Add((TreeNode) node18);
        }
      }
    }
    A_NastrVed.OneAvsNode node19 = this.oneRecordNode_Avs_Create(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Empty, oneAvsNode2);
    if (node19 != null)
      oneAvsNode2.Nodes.Add((TreeNode) node19);
    oneAvsNode2.Expand();
  }

  /// <summary> Ветка, описывающая одну ЗАПИСЬ </summary>
  /// <param textFromColumn="oneRecord_Avs"></param>
  /// <param textFromColumn="oneAvsNode_Parent"></param>
  /// <returns></returns>
  private A_NastrVed.OneAvsNode oneRecordNode_Avs_Create(
    Vedomost_VB.OneRecord_Avs6_To_Ips oneRecord_Avs,
    A_NastrVed.OneAvsNode oneAvsNode_Parent,
    int razdelVed = 0)
  {
    if (oneRecord_Avs == null)
      return (A_NastrVed.OneAvsNode) null;
    string str1 = Vedomost_VB_Static.TypeRecName_by_TypeRec_Avs(oneRecord_Avs._nameTypeRec);
    string str2 = !(str1 != "Информационная") ? "" : str1;
    if (oneRecord_Avs._tableRowId != "")
    {
      if (str2 != "")
      {
        str2 = $"{str2}: {oneRecord_Avs._tableRowId}";
      }
      else
      {
        if (razdelVed > 0)
        {
          string nameRazdelVed = Vedomost_VB_Static.Get_NameRazdelVed(this._one_Ved_Nastr_Tmp._list_RazdelsVed, razdelVed);
          if (nameRazdelVed != "")
            str2 = nameRazdelVed + ": ";
        }
        str2 += oneRecord_Avs._tableRowId;
      }
    }
    A_NastrVed.OneAvsNode oneAvsNode = new A_NastrVed.OneAvsNode();
    oneAvsNode.Text = str2;
    oneAvsNode.ImageIndex = this._indexImageList_Section;
    oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode oneAvsNode_Parent1 = oneAvsNode;
    oneAvsNode_Parent1._oneAvsNode_Parent = oneAvsNode_Parent;
    oneAvsNode_Parent1._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneRecord_Avs = oneRecord_Avs;
    oneAvsNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Record;
    if (oneRecord_Avs._listOneGrafa_Avs6_To_Ips != null)
    {
      for (int index = 0; index < oneRecord_Avs._listOneGrafa_Avs6_To_Ips.Count; ++index)
      {
        A_NastrVed.OneAvsNode node = this.oneGrafaNode_Avs_Create(oneRecord_Avs._listOneGrafa_Avs6_To_Ips[index], oneAvsNode_Parent1);
        if (node != null)
          oneAvsNode_Parent1.Nodes.Add((TreeNode) node);
      }
    }
    return oneAvsNode_Parent1;
  }

  /// <summary> Ветка, описывающая одну ГРАФУ </summary>
  /// <param textFromColumn="oneGrafa_Avs"></param>
  /// <param textFromColumn="oneAvsNode_Parent"></param>
  /// <returns></returns>
  private A_NastrVed.OneAvsNode oneGrafaNode_Avs_Create(
    Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafa_Avs,
    A_NastrVed.OneAvsNode oneAvsNode_Parent)
  {
    if (oneGrafa_Avs == null)
      return (A_NastrVed.OneAvsNode) null;
    string cellId = oneGrafa_Avs._cell_ID;
    A_NastrVed.OneAvsNode oneAvsNode = new A_NastrVed.OneAvsNode();
    oneAvsNode.Text = "Ячейка шаблона: " + cellId;
    oneAvsNode.ImageIndex = this._indexImageList_Section;
    oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode oneAvsNode_Parent1 = oneAvsNode;
    oneAvsNode_Parent1._oneAvsNode_Parent = oneAvsNode_Parent;
    oneAvsNode_Parent1._oneGrafa_Avs = oneGrafa_Avs;
    oneAvsNode_Parent1._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    oneAvsNode_Parent1._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode_Parent1._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
    if (oneGrafa_Avs._listOneDataField_Avs6_To_Ips != null)
    {
      for (int index = 0; index < oneGrafa_Avs._listOneDataField_Avs6_To_Ips.Count; ++index)
      {
        A_NastrVed.OneAvsNode node = this.oneDataNode_Avs_Create(oneGrafa_Avs._listOneDataField_Avs6_To_Ips[index], oneAvsNode_Parent1, index);
        if (node != null)
        {
          node._oneAvsNode_Parent = oneAvsNode_Parent1;
          oneAvsNode_Parent1.Nodes.Add((TreeNode) node);
        }
      }
    }
    return oneAvsNode_Parent1;
  }

  /// <summary> Формирование одной конечной ветки ДАННЫх для дерева </summary>
  /// <param textFromColumn="oneDataField_Avs"></param>
  /// <param textFromColumn="oneAvsNode_Parent"></param>
  /// <param textFromColumn="iData"></param>
  /// <returns></returns>
  private A_NastrVed.OneAvsNode oneDataNode_Avs_Create(
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs,
    A_NastrVed.OneAvsNode oneAvsNode_Parent,
    int iData)
  {
    if (oneDataField_Avs == null)
      return (A_NastrVed.OneAvsNode) null;
    string str = this.OneDataField_Avs_Draw(oneDataField_Avs, iData);
    A_NastrVed.OneAvsNode oneAvsNode1 = new A_NastrVed.OneAvsNode();
    oneAvsNode1.Text = str;
    oneAvsNode1.ImageIndex = this._indexImageList_Section;
    oneAvsNode1.SelectedImageIndex = this._indexImageList_Section;
    A_NastrVed.OneAvsNode oneAvsNode2 = oneAvsNode1;
    oneAvsNode2._oneAvsNode_Parent = iData <= 0 ? oneAvsNode_Parent : oneAvsNode_Parent._oneAvsNode_Parent;
    oneAvsNode2._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    oneAvsNode2._oneDataField_Avs = oneDataField_Avs;
    oneAvsNode2._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    oneAvsNode2._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    oneAvsNode2._iData = iData;
    return oneAvsNode2;
  }

  /// <summary> Формирование одной конечной строчки ДАННЫх для дерева </summary>
  /// <param textFromColumn="oneDataField_Avs"></param>
  /// <returns></returns>
  private string OneDataField_Avs_Draw(
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs,
    int iData)
  {
    if (oneDataField_Avs == null)
      return "";
    string str1 = "";
    this.comboBox_Avs_TextRazdelitel.Text = this.translate_text(oneDataField_Avs._symbolRazd, true);
    if (iData > 0)
      str1 = $"\"{this.translate_text(oneDataField_Avs._symbolRazd, false)}\" ";
    string str2 = AVS6_From_Avs6Main.FieldNameByType(AVS6_From_Avs6Main.TypeListFields.Record, (byte) oneDataField_Avs._objectType);
    if (str2 != null)
      str1 += str2;
    return str1;
  }

  /// <summary> Выбор строки на ДЕРЕВЕ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void treeView_Avs_AfterSelect(object sender, TreeViewEventArgs e)
  {
    string currId = "";
    this.i_curr_oneGrafa_Avs_Current = -1;
    this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
    this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    this.comboBox_Avs_TextRazdelitel.Enabled = true;
    this.groupBox_Avs_TextRazdelitel.Enabled = true;
    this.comboBox_Avs_TextRazdelitel.Text = this.translate_text("", false);
    this.treeView_Avs.Enabled = true;
    this.oneTreeNode_Avs_Current = (A_NastrVed.OneAvsNode) this.treeView_Avs.SelectedNode;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
    {
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      string tableName = this.algorithm_Avs_curr == null ? "" : this.algorithm_Avs_curr._tableName;
      this.SetElementStr_Avs(tableName);
      if (tableName == "")
      {
        this.button_Avs_AddCell.Enabled = true;
        this.button_Avs_AddAttribut.Enabled = true;
      }
      else
      {
        this.button_Avs_AddCell.Enabled = false;
        this.button_Avs_AddAttribut.Enabled = false;
      }
      this.button_Avs_Edit.Enabled = false;
      this.button_Avs_Delete.Enabled = false;
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Info)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      this.button_Avs_AddCell.Enabled = false;
      this.button_Avs_AddAttribut.Enabled = false;
      this.button_Avs_Edit.Enabled = false;
      this.button_Avs_Delete.Enabled = false;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs("");
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
      this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneRecord_Avs;
      this.button_Avs_AddCell.Enabled = true;
      this.button_Avs_AddAttribut.Enabled = false;
      this.button_Avs_Edit.Enabled = true;
      this.button_Avs_Delete.Enabled = false;
      this.i_curr_oneGrafa_Avs_Current = -1;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs(this.oneRecord_Avs_Current == null ? "" : this.oneRecord_Avs_Current._tableRowId);
    }
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.listBox_Avs6_Fields.SelectedIndex = -1;
      this.oneDataField_Avs_current = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      this.oneGrafa_Avs_Current = this.oneTreeNode_Avs_Current._oneGrafa_Avs;
      this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneRecord_Avs;
      if (this.oneRecord_Avs_Current != null && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips != null)
        this.i_curr_oneGrafa_Avs_Current = this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.IndexOf(this.oneGrafa_Avs_Current);
      this.button_Avs_AddCell.Enabled = true;
      this.button_Avs_AddAttribut.Enabled = true;
      this.button_Avs_Edit.Enabled = true;
      this.button_Avs_Delete.Enabled = true;
      this.comboBox_Avs_TextRazdelitel.Enabled = false;
      this.groupBox_Avs_TextRazdelitel.Enabled = false;
      this.SetElementStr_Avs(this.oneGrafa_Avs_Current == null ? "" : this.oneGrafa_Avs_Current._cell_ID);
    }
    else
    {
      if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
      {
        this.listBox_Avs6_Fields.SelectedIndex = -1;
        this.oneDataField_Avs_current = this.oneTreeNode_Avs_Current._oneDataField_Avs;
        this.oneGrafa_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneGrafa_Avs;
        this.oneRecord_Avs_Current = this.oneTreeNode_Avs_Current._oneAvsNode_Parent._oneAvsNode_Parent._oneRecord_Avs;
        this.button_Avs_AddCell.Enabled = false;
        this.button_Avs_AddAttribut.Enabled = true;
        this.button_Avs_Edit.Enabled = true;
        this.button_Avs_Delete.Enabled = true;
        this.comboBox_Avs_TextRazdelitel.Enabled = true;
        this.comboBox_Avs_TextRazdelitel.Text = this.translate_text("", true);
        this.listBox_Avs6_Fields.SelectedIndex = AVS6_From_Avs6Main.IndexByType(AVS6_From_Avs6Main.TypeListFields.Record, (byte) this.oneDataField_Avs_current._objectType);
        currId = this.oneGrafa_Avs_Current == null ? "" : this.oneGrafa_Avs_Current._cell_ID;
        this.SetElementStr_Avs(currId);
        this.comboBox_Avs_TextRazdelitel.Text = this.translate_text(this.oneDataField_Avs_current._symbolRazd, true);
      }
      this.SetElementStr_Avs(currId);
    }
  }

  /// <summary> Кнопка ДОБАВИТЬ ЯЧЕЙКУ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Avs_AddCell_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
    if (activeElement == null || activeElement.Id == "")
      return;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrVed.OneAvsNode oneAvsNode = new A_NastrVed.OneAvsNode();
      oneAvsNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneAvsNode.ImageIndex = this._indexImageList_Section;
      oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneAvsNode node = oneAvsNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneAvsNode_Parent = this.oneTreeNode_Avs_Current;
      node._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      node._iData = -1;
      Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips();
      node._oneGrafa_Avs = oneGrafaAvs6ToIps;
      oneGrafaAvs6ToIps._cell_ID = activeElement.Id;
      oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>();
      this.oneTreeNode_Avs_Current.Nodes.Insert(0, (TreeNode) node);
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Insert(0, oneGrafaAvs6ToIps);
      this.ModifiedAll(true);
      this.IsModified_Page_Avs = true;
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (activeElement.NodeClass != "TextBoxElement")
      {
        int num = (int) MessageBox.Show("На шаблоне необходимо выбрать текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      A_NastrVed.OneAvsNode oneAvsNode = new A_NastrVed.OneAvsNode();
      oneAvsNode.Text = "Ячейка шаблона: " + activeElement.Id;
      oneAvsNode.ImageIndex = this._indexImageList_Section;
      oneAvsNode.SelectedImageIndex = this._indexImageList_Section;
      A_NastrVed.OneAvsNode node = oneAvsNode;
      node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Cell;
      node._oneAvsNode_Parent = this.oneTreeNode_Avs_Current._oneAvsNode_Parent;
      node._oneDataField_Avs = (Vedomost_VB.OneDataField_Avs6_To_Ips) null;
      node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
      node._iData = -1;
      Vedomost_VB.OneGrafa_Avs6_To_Ips oneGrafaAvs6ToIps = new Vedomost_VB.OneGrafa_Avs6_To_Ips();
      node._oneGrafa_Avs = oneGrafaAvs6ToIps;
      oneGrafaAvs6ToIps._cell_ID = activeElement.Id;
      oneGrafaAvs6ToIps._listOneDataField_Avs6_To_Ips = new List<Vedomost_VB.OneDataField_Avs6_To_Ips>();
      this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.Insert(this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Avs_Current) + 1, (TreeNode) node);
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Insert(this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.IndexOf((TreeNode) this.oneTreeNode_Avs_Current) + 1, oneGrafaAvs6ToIps);
      this.ModifiedAll(true);
      this.IsModified_Page_Avs = true;
    }
    this.treeView_Avs.Select();
  }

  /// <summary> Кнопка Добавить поле Avs </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Avs_AddAttribut_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    Vedomost_VB.OneDataField_Avs6_To_Ips oneDataField_Avs = new Vedomost_VB.OneDataField_Avs6_To_Ips();
    if (this.listBox_Avs6_Fields.SelectedIndex <= -1)
      return;
    byte num1 = AVS6_From_Avs6Main.FieldTypeByIndex(AVS6_From_Avs6Main.TypeListFields.Record, this.listBox_Avs6_Fields.SelectedIndex);
    oneDataField_Avs._objectType = (int) num1;
    A_NastrVed.OneAvsNode node;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      oneDataField_Avs._symbolRazd = "";
      node = this.oneDataNode_Avs_Create(oneDataField_Avs, this.oneTreeNode_Avs_Current, 0);
    }
    else
    {
      oneDataField_Avs._symbolRazd = this.translate_text(this.comboBox_Avs_TextRazdelitel.Text, false);
      node = this.oneDataNode_Avs_Create(oneDataField_Avs, this.oneTreeNode_Avs_Current, this.oneTreeNode_Avs_Current._iData + 1);
    }
    node._oneGrafa_Avs = (Vedomost_VB.OneGrafa_Avs6_To_Ips) null;
    node._oneDataField_Avs = oneDataField_Avs;
    node._oneRecord_Avs = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    node._typeNode = Vedomost_VB_Static.TypeNode_Tree.Data;
    node._iData = this.oneTreeNode_Avs_Current._iData + 1;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.Insert(0, oneDataField_Avs);
      this.oneTreeNode_Avs_Current.Nodes.Insert(0, (TreeNode) node);
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
    {
      int num2 = this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.IndexOf(this.oneDataField_Avs_current);
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.Insert(num2 + 1, oneDataField_Avs);
      this.oneTreeNode_Avs_Current._oneAvsNode_Parent.Nodes.Insert(num2 + 1, (TreeNode) node);
    }
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
    this.oneTreeNode_Avs_Current.Expand();
    this.treeView_Avs.Select();
  }

  /// <summary> Кнопка ИЗМЕНИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Avs_Edit_Click(object sender, EventArgs e)
  {
    DocumentTreeNode activeElement = this.docControl_Avs.ActiveElement;
    if (activeElement == null)
      return;
    string str = "";
    string id = activeElement.Id;
    string name1 = activeElement.Name;
    this.oneTreeNode_Avs_Current = (A_NastrVed.OneAvsNode) this.treeView_Avs.SelectedNode;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Main)
    {
      if (activeElement.NodeClass != "TableElement")
      {
        int num = (int) MessageBox.Show("На шаблоне не выбрана таблица", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      string name2 = activeElement.Name;
      if (name2 != "")
      {
        this.algorithm_Avs_curr._tableName = name2;
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
      }
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Record)
    {
      if (activeElement.NodeClass != "TableElement")
      {
        int num = (int) MessageBox.Show("На шаблоне не выбрана строка", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (activeElement.Id != "" && this.oneRecord_Avs_Current != null && activeElement.Id != "")
      {
        this.oneRecord_Avs_Current._tableRowId = activeElement.Id;
        this.oneTreeNode_Avs_Current.Text = $"{Vedomost_VB_Static.TypeRecName_by_TypeRec_Avs(this.oneRecord_Avs_Current._nameTypeRec)}: Строка: {this.oneRecord_Avs_Current._tableRowId}";
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
      }
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell)
    {
      if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
      {
        int num = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      str = activeElement.Id;
      if (this.oneRecord_Avs_Current != null)
      {
        this.oneRecord_Avs_Current._tableRowId = activeElement.Id;
        this.oneTreeNode_Avs_Current.Text = "Ячейка шаблона: " + this.oneRecord_Avs_Current._tableRowId;
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
      }
    }
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data)
    {
      if (activeElement.NodeClass != "TextBoxElement" || !(activeElement.Id != ""))
      {
        int num = (int) MessageBox.Show("На шаблоне не выбрано текстовое поле", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      if (this.listBox_Avs6_Fields.SelectedIndex > -1)
      {
        this.oneDataField_Avs_current._objectType = (int) AVS6_From_Avs6Main.FieldTypeByIndex(AVS6_From_Avs6Main.TypeListFields.Record, this.listBox_Avs6_Fields.SelectedIndex);
        this.oneDataField_Avs_current._symbolRazd = this.translate_text(this.comboBox_Avs_TextRazdelitel.Text, false);
        this.oneTreeNode_Avs_Current.Text = this.OneDataField_Avs_Draw(this.oneDataField_Avs_current, this.oneTreeNode_Avs_Current._iData);
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
      }
    }
    this.treeView_Avs.Select();
  }

  /// <summary> Кнопка УДАЛИТЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void button_Avs_Delete_Click(object sender, EventArgs e)
  {
    if (this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Cell && this.oneTreeNode_Avs_Current._typeNode != Vedomost_VB_Static.TypeNode_Tree.Data)
      return;
    A_NastrVed.OneAvsNode oneAvsNode = (A_NastrVed.OneAvsNode) this.oneTreeNode_Avs_Current.PrevNode ?? this.oneTreeNode_Avs_Current._oneAvsNode_Parent;
    if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Data && this.oneTreeNode_Avs_Current._iData > -1 && this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips != null && this.oneTreeNode_Avs_Current._iData < this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.Count)
      this.oneGrafa_Avs_Current._listOneDataField_Avs6_To_Ips.RemoveAt(this.oneTreeNode_Avs_Current._iData);
    else if (this.oneTreeNode_Avs_Current._typeNode == Vedomost_VB_Static.TypeNode_Tree.Cell && this.i_curr_oneGrafa_Avs_Current > -1 && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips != null && this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.Count > this.i_curr_oneGrafa_Avs_Current)
      this.oneRecord_Avs_Current._listOneGrafa_Avs6_To_Ips.RemoveAt(this.i_curr_oneGrafa_Avs_Current);
    this.oneTreeNode_Avs_Current.Remove();
    if (oneAvsNode != null)
      this.treeView_Avs.SelectedNode = (TreeNode) oneAvsNode;
    this.treeView_Avs.Select();
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
  }

  /// <summary> Единичный или А </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Avs_EdOrA_Click(object sender, EventArgs e)
  {
    if (this.isradioButtonEdOrA_Avs)
      return;
    this.algorithm_Avs_curr = this.algorithm_Avs;
    this.templateID_curr_Avs = this.templateID_Avs;
    this.TemplateId_Avs = this.templateID_curr_Avs;
    this.treeView_Avs_Draw();
    this.treeView_Avs.SelectedNode = this.treeView_Avs.Nodes[0];
    this.isradioButtonEdOrA_Avs = true;
    this.isradioButtonGroupB_Avs = false;
  }

  /// <summary> Групповой Б </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void radioButton_Avs_GroupB_Click(object sender, EventArgs e)
  {
    if (this.isradioButtonGroupB_Avs)
      return;
    this.algorithm_Avs_curr = this.algorithm_Avs_B;
    this.templateID_curr_Avs = this.templateID_B_Avs;
    this.TemplateId_Avs = this.templateID_curr_Avs;
    this.treeView_Avs_Draw();
    this.treeView_Avs.SelectedNode = this.treeView_Avs.Nodes[0];
    this.isradioButtonEdOrA_Avs = false;
    this.isradioButtonGroupB_Avs = true;
  }

  private void button_Avs_PoRazdelam_Click(object sender, EventArgs e)
  {
    this.Avs_PoRazdelam();
    this.treeView_Avs_Draw();
    this.treeView_Avs.SelectedNode = this.treeView_Avs.Nodes[0];
  }

  /// <summary> Вывод "По разделам" </summary>
  private void Avs_PoRazdelam()
  {
    this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips = new List<Vedomost_VB.OneRazdel_Avs6_To_Ips>();
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index)
    {
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index];
      if (!(oneRazdelVed._name == "Ведомости составных частей") && oneRazdelVed._razdelVed != 1000)
        this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Add(new Vedomost_VB.OneRazdel_Avs6_To_Ips()
        {
          _razdelVed = oneRazdelVed._razdelVed,
          _oneRecord_Avs6_To_Ips_Info = Vedomost_VB_Static.oneRecord_Avs6_To_Ips_Copy(this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info)
        });
    }
    this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info = (Vedomost_VB.OneRecord_Avs6_To_Ips) null;
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
  }

  private void button_Avs_Obshaia_Click(object sender, EventArgs e)
  {
    this.algorithm_Avs_curr._oneRecord_Avs6_To_Ips_Info = Vedomost_VB_Static.oneRecord_Avs6_To_Ips_Copy(this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips[0]._oneRecord_Avs6_To_Ips_Info);
    this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips.Clear();
    this.algorithm_Avs_curr._list_OneRazdel_Avs6_To_Ips = (List<Vedomost_VB.OneRazdel_Avs6_To_Ips>) null;
    this.treeView_Avs_Draw();
    this.treeView_Avs.SelectedNode = this.treeView_Avs.Nodes[0];
    this.ModifiedAll(true);
    this.IsModified_Page_Avs = true;
  }

  /// <summary> Открыть или спрятать кнопки Save </summary>
  /// <param textFromColumn="isModified"></param>
  private void ModifiedAll(bool isModifiedAll)
  {
    if (this.isCreate)
    {
      this.IsModifiedAll = false;
      this.buttonSave1.Enabled = false;
      this.IsModified_Page_Bases = false;
      this.IsModified_Page_Sbor = false;
      this.IsModified_Page_Sortings = false;
      this.IsModified_Page_Razdels = false;
      this.IsModified_Page_Zagolovki = false;
      this.IsModified_Page_Vyvod = false;
      this.IsModified_Page_Xml = false;
      this.IsModified_Page_Service = false;
    }
    else
    {
      if (isModifiedAll)
      {
        this.IsModifiedAll = true;
        this.buttonSave1.Enabled = true;
        this.IsBylo_IsModified = true;
      }
      else
      {
        this.IsModifiedAll = false;
        this.buttonSave1.Enabled = false;
        this.IsModified_Page_Bases = false;
        this.IsModified_Page_Sbor = false;
        this.IsModified_Page_Sortings = false;
        this.IsModified_Page_Razdels = false;
        this.IsModified_Page_Zagolovki = false;
        this.IsModified_Page_Vyvod = false;
        this.IsModified_Page_Service = false;
      }
      if (!this.buttonSave1.Enabled)
        return;
      if (this._one_Ved_Nastr_Tmp._accessLevel == 0)
      {
        if (!(Vedomost_VB_Static.UserName != "Системный администратор"))
          return;
        this.buttonSave1.Enabled = false;
      }
      else
      {
        if (this._one_Ved_Nastr_Tmp._accessLevel != 1 || !(Vedomost_VB_Static.UserName != "Системный администратор") || Vedomost_VB_Static.IsAdmin)
          return;
        this.buttonSave1.Enabled = false;
      }
    }
  }

  /// <summary> Сохранение редактирования страниц в _one_Tabl_Nastr_Tmp </summary>
  private void Saving_Pages()
  {
    this.Saving_Page_Bases();
    this.Saving_Page_Sbor();
    this.Saving_Page_Sorting();
    this.Saving_Page_Razdels();
    this.Saving_Page_Zagolovki();
    this.Saving_Page_Vyvod();
    this.Saving_Page_Service();
  }

  /// <summary> Нажатие кнопки Сохранить </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonSave1_Click(object sender, EventArgs e)
  {
    this.Cleaning_Of_Empty_OneRazdelVed();
    if (this.ControlPages())
      return;
    string text = this.Save();
    if (!(text != ""))
      return;
    int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary> Контроль ВСЕХ страниц </summary>
  /// <returns></returns>
  private bool ControlPages()
  {
    bool flag = this.Razdels_dataGridViewListRazdels_Control();
    if (!flag)
      flag = this.Razdels_Control_Main();
    if (!flag)
      flag = this.Zagolovki_dataGridView_ListZagolovkov_Control();
    return flag;
  }

  /// <summary> Сохранение _one_ved_Nastr_Curr </summary>
  private string Save()
  {
    if (this.IsModifiedFromFile && MessageBox.Show("Параметры настройки были изменены данными из файла (Dump)\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return "";
    this.Saving_Pages();
    if (this._one_Ved_Nastr_Tmp._nameVed == null || this._one_Ved_Nastr_Tmp._nameVed == "")
      this._one_Ved_Nastr_Tmp._nameVed = this._imsObjectType_Curr.ObjectName;
    if (!this.is_one_Ved_Nastr_New)
    {
      Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Ved_Nastr_Tmp, this._one_Ved_Nastr_Curr);
      Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Ved_Nastr_Tmp, this._one_ImsObjectType_With_One_Ved_Nastr_Curr.one_Ved_Nastr);
      if (this._one_Ved_Nastr_Window_Curr != null && this._one_Ved_Nastr_Window_Curr._nameVed == this._one_Ved_Nastr_Tmp._nameVed)
        Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Ved_Nastr_Tmp, this._one_Ved_Nastr_Window_Curr);
      XmlDocument xmlDocument = this._one_Ved_Nastr_Curr.XmlDocument_create();
      string str;
      try
      {
        str = Vedomost_VB_Static.WriteXmlNastrToBase(xmlDocument, this._one_Ved_Nastr_Curr._vedomostTemplateObjectGuid);
      }
      catch
      {
        return "Неопределенная ошибка сохранения настроек";
      }
      if (str != "")
        return str;
      if (Vedomost_VB_Static.isCreateDump_Tmp || Vedomost_VB_Static.isComputerName_Victor || Vedomost_VB_Static.isHozain)
        xmlDocument.Save(Vedomost_VB_Static.DirectoryDump + "\\onenastr.xml");
      if (Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
        Vedomost_VB_Static.Write_One_Ved_Nastr_Directly_ToBase(this._one_Ved_Nastr_Curr, true);
      this.ModifiedAll(false);
      this.IsModifiedFromFile = false;
      this.Text = "Настройка ведомости:";
      if (this._imsObjectType_Curr != null)
        this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
      else
        this.Text = $"{this.Text} [{this._one_Ved_Nastr_Curr._nameVed}]";
      if (this._one_Ved_Nastr_Curr._dateIni != "")
        this.Text = $"{this.Text} {this._one_Ved_Nastr_Curr._dateIni}";
      int num = (int) MessageBox.Show("Параметры настройки сохранены", "Выполнено!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      return "";
    }
    string vedByTemplateGuid = Vedomost_VB_Static.Get_NameTypeVed_By_TemplateGuid(Vedomost_VB_Static.List_Conformity_Template_Nastr_Ved, this._guidTemplateVed_Curr);
    string str1 = "Шаблон, данной настройки, уже применяется в другом типе документа";
    if (vedByTemplateGuid != "")
      str1 = $"{str1}\r\n\r\n({vedByTemplateGuid})";
    return str1 + "\r\n\r\nПараметры настройки не могут быть сохранены";
  }

  /// <summary> При ПОПЫТКЕ закрыть окно диалога </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void A_NastrVed_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.noClosing)
      return;
    e.Cancel = true;
    this.noClosing = false;
  }

  /// <summary> Нажатие кнопки OK </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void bOK_Click(object sender, EventArgs e)
  {
    this.noClosing = false;
    if (this.ControlPages())
    {
      int num = (int) MessageBox.Show("Закрыть окно диалога возможно без сохранения результатов\r\nпо нажатию кнопки Отмена", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      this.noClosing = true;
    }
    else
    {
      if (this.IsModifiedAll && this.buttonSave1.Enabled)
      {
        this.Cleaning_Of_Empty_OneRazdelVed();
        if (this.Save() != "")
        {
          this.noClosing = true;
          return;
        }
      }
      this.Close();
    }
  }

  /// <summary> Нажатие кнопки "По умолчанию" </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonDefault_Click(object sender, EventArgs e)
  {
    string name = this.tabControl_Nastr.SelectedTab.Name;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
    {
      case 311298622:
        if (!(name == "tabPage_Avs6"))
          break;
        Vedomost_VB.Algorithm_Avs6_To_Ips ipsInitByTypeVed = Vedomost_VB_Static.Algorithm_Avs6_To_Ips_Init_By_TypeVed(this._one_Ved_Nastr_Curr._typeVed);
        if (ipsInitByTypeVed == null)
          break;
        this._one_Ved_Nastr_Tmp._algorithm_Avs6_To_Ips = ipsInitByTypeVed;
        this.Draw_Page_Avs();
        this.ModifiedAll(true);
        this.IsModified_Page_Avs = true;
        break;
      case 326637659:
        if (!(name == "tabPage_Service"))
          break;
        this.ModifiedAll(true);
        this.checkBox_Services_isCreateDumpAuto.Checked = true;
        this.IsModified_Page_Service = true;
        break;
      case 1754209060:
        if (!(name == "tabPage_Merge"))
          break;
        Vedomost_VB.Merge_Usl2 mergeUsl2 = Vedomost_VB_Static.Merge_Usl2_Ved_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (mergeUsl2 == null || mergeUsl2._list_Merge_Usl2 == null)
          break;
        this._one_Ved_Nastr_Tmp._merge_Usl2 = mergeUsl2;
        this.Draw_Page_Merge();
        this.ModifiedAll(true);
        break;
      case 3109688624:
        if (!(name == "tabPage_Zagolovki"))
          break;
        Vedomost_VB.Zagolovki_Ved zagolovkiVed = Vedomost_VB_Static.Zagolovki_Ved_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (zagolovkiVed == null)
          break;
        this._one_Ved_Nastr_Tmp._zagolovki_Ved = zagolovkiVed;
        this.dataGridView_ListZagolovkov.Rows.Clear();
        this.Draw_Page_Zagolovki();
        this.ModifiedAll(true);
        this.IsModified_Page_Zagolovki = true;
        break;
      case 3244506512:
        if (!(name == "tabPage_Sbor"))
          break;
        List<Vedomost_VB.Usl_Read_From_SP> uslReadFromSpList1 = Vedomost_VB_Static.List_Usl_Read_From_SP_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (uslReadFromSpList1 != null)
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP = uslReadFromSpList1;
        List<Vedomost_VB.Usl_Read_From_SP> uslReadFromSpList2 = Vedomost_VB_Static.List_Usl_Read_From_SP_Reference_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (uslReadFromSpList2 != null)
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP_Reference = uslReadFromSpList2;
        List<Vedomost_VB.OneFieldSpForRead> oneFieldSpForReadList = Vedomost_VB_Static.List_Ved_ID_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (oneFieldSpForReadList != null)
          this._one_Ved_Nastr_Tmp._list_Ved_ID = oneFieldSpForReadList;
        Vedomost_VB.Sbor_Options sborOptions = Vedomost_VB_Static.Sbor_Options_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (sborOptions != null)
          this._one_Ved_Nastr_Tmp._sbor_Options = sborOptions;
        if (this.isSortDoc)
        {
          Vedomost_VB.ESPD espd = Espd_Static.Espd_Init();
          if (espd != null)
            this._one_Ved_Nastr_Tmp._espd = espd;
        }
        this.Draw_Page_Sbor();
        this.ModifiedAll(true);
        this.IsModified_Page_Sbor = true;
        break;
      case 3316967738:
        if (!(name == "tabPage_Bases"))
          break;
        Vedomost_VB.Bases_Options_Ved basesOptionsVed = Vedomost_VB_Static.Bases_Options_Ved_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (basesOptionsVed == null)
          break;
        this._one_Ved_Nastr_Tmp._bases_Options_Ved = basesOptionsVed;
        this.Draw_Page_Bases();
        this.ModifiedAll(true);
        this.IsModified_Page_Bases = true;
        break;
      case 3885257238:
        if (!(name == "tabPage_Vyvod"))
          break;
        this.Default_Vyvod();
        this.ModifiedAll(true);
        this.IsModified_Page_Vyvod = true;
        break;
      case 3927720027:
        if (!(name == "tabPage_Razdels"))
          break;
        List<Vedomost_VB.OneRazdelVed> oneRazdelVedList = Vedomost_VB_Static.List_Razdels_Ved_Init(this._one_Ved_Nastr_Curr._typeVed);
        if (oneRazdelVedList == null)
          break;
        this.isCreate = true;
        this._one_Ved_Nastr_Tmp._list_RazdelsVed = oneRazdelVedList;
        this.Razdels_dataGridViewListRazdels.Rows.Clear();
        this.Razdels_dataGridViewListPodRazdels.Rows.Clear();
        this.Draw_Page_Razdels();
        this.isCreate = false;
        this.ModifiedAll(true);
        this.isCreate = false;
        this.IsModified_Page_Razdels = true;
        this.IsModified_Page_PodRazdels = false;
        break;
      case 4107045604:
        if (!(name == "tabPage_Sorting"))
          break;
        if (!this.isSortDoc)
        {
          Vedomost_VB.Sorting_Usl sortingUsl = Vedomost_VB_Static.Sorting_Usl_Ved_Init(this._one_Ved_Nastr_Curr._typeVed);
          if (sortingUsl == null)
            break;
          this._one_Ved_Nastr_Tmp._sorting_Usl = sortingUsl;
          this.ModifiedAll(true);
          this.IsModified_Page_Sortings = true;
          this.dataGridView_Sorting_Curr.Rows.Clear();
          this.dataGridView_Sorting_Draw();
          this.SelectDataGridView_Sorting_Row(0);
          this.Displays_The_Current_Record();
          this.listBox_Sorting_List_Ved_Id.SelectedIndex = -1;
          this.listBox_Sorting_AttribVedRec.SelectedIndex = -1;
        }
        else
        {
          Vedomost_VB.Sorting_Usl_Doc sortingUslDoc = Vedomost_VB_Static.Sorting_Usl_Doc_Init(this._one_Ved_Nastr_Curr._typeVed);
          if (sortingUslDoc == null)
            break;
          this._one_Ved_Nastr_Tmp._sorting_Usl_Doc = sortingUslDoc;
          this.ModifiedAll(true);
          this.IsModified_Page_Sortings = true;
          this.dataGridView_Sorting_Curr.Rows.Clear();
          this.dataGridView_Sorting_Doc_Draw();
          this.SelectDataGridView_Sorting_Row(0);
          this.Displays_The_Current_Record();
          this.listBox_Sorting_List_Ved_Graf.SelectedIndex = -1;
        }
        this.dataGridView_Sorting_Curr.Focus();
        break;
      case 4223869073:
        if (!(name == "tabPage_Xml"))
          break;
        Vedomost_VB.AlgorithmXml algorithmXml = Vedomost_VB_Static.AlgorithmXml_Init_By_TypeVed(this._one_Ved_Nastr_Curr._typeVed);
        if (algorithmXml == null)
          break;
        this._one_Ved_Nastr_Tmp._algorithmXml = algorithmXml;
        this.Draw_Page_Xml();
        this.ModifiedAll(true);
        this.IsModified_Page_Xml = true;
        break;
    }
  }

  /// <summary> Изменение текущей страницы </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void tabControl_Nastr_SelectedIndexChanged(object sender, EventArgs e)
  {
    System.Windows.Forms.TabPage selectedTab = this.tabControl_Nastr.SelectedTab;
    this.IsButtonDefault();
    this.IsButtonCopyFrom();
    string name = selectedTab.Name;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
    {
      case 326637659:
        if (!(name == "tabPage_Service"))
          break;
        this.buttonCopyFrom.Visible = false;
        this.buttonDefault.Visible = false;
        if (this._one_Ved_Nastr_Tmp._typeCreate != Vedomost_VB.TypeCreate.System)
        {
          this.buttonServicesCopyAll.Visible = true;
          this.buttonServicesTypeVedTo.Visible = true;
          this.label_ServicesTypeVedTo.Visible = true;
          this.label_ServicesCopyAll.Visible = true;
          break;
        }
        this.buttonServicesCopyAll.Visible = false;
        this.buttonServicesTypeVedTo.Visible = false;
        this.label_ServicesTypeVedTo.Visible = false;
        this.label_ServicesCopyAll.Visible = false;
        break;
      case 1754209060:
        int num1 = name == "tabPage_Merge" ? 1 : 0;
        break;
      case 3109688624:
        if (!(name == "tabPage_Zagolovki"))
          break;
        this.List_Ved_Id_Draw(this.listBox_Zagolovki_List_Ved_Id);
        break;
      case 3244506512:
        int num2 = name == "tabPage_Sbor" ? 1 : 0;
        break;
      case 3316967738:
        int num3 = name == "tabPage_Bases" ? 1 : 0;
        break;
      case 3885257238:
        if (!(name == "tabPage_Vyvod"))
          break;
        this.List_Ved_Id_Draw(this.listBox_Vyvod_List_Ved_Id);
        if (!this.IsModified_Page_Razdels)
          break;
        this.Synchronization_list_RazdelsVed_and_list_OneRazdelToPrint();
        this.treeView_Vyvod_Draw();
        break;
      case 3927720027:
        int num4 = name == "tabPage_Razdels" ? 1 : 0;
        break;
      case 4107045604:
        if (!(name == "tabPage_Sorting"))
          break;
        this.List_Ved_Id_Draw(this.listBox_Sorting_List_Ved_Id);
        break;
      case 4223869073:
        int num5 = name == "tabPage_Xml" ? 1 : 0;
        break;
    }
  }

  /// <summary> Проверка прм выходе из любой страницы </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void tabControl_Nastr_Deselecting(object sender, TabControlCancelEventArgs e)
  {
    if (e.TabPage == null)
      return;
    if (e.TabPage.Name == "tabPage_Razdels")
    {
      this.Cleaning_Of_Empty_OneRazdelVed();
      if (this.Razdels_dataGridViewListRazdels_Control())
        e.Cancel = true;
    }
    if (!(e.TabPage.Name == "tabPage_Zagolovki") || !this.Zagolovki_dataGridView_ListZagolovkov_Control())
      return;
    e.Cancel = true;
  }

  /// <summary> Кнопка По умолчанию все </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonDefaultAll_Click(object sender, EventArgs e)
  {
    One_Ved_Nastr one_Ved_Nastr = Vedomost_VB_Static.Ved_Nastr_Init(this._one_Ved_Nastr_Tmp._typeVed, Guid.Empty, false, this._one_Ved_Nastr_Tmp);
    if (one_Ved_Nastr == null)
      return;
    if (this.isByloButtonTypeVedTo_Click || this._one_Ved_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.User)
    {
      one_Ved_Nastr._guidParent = this._one_Ved_Nastr_Tmp._guidParent;
      one_Ved_Nastr._guidTypeVed = this._one_Ved_Nastr_Tmp._guidTypeVed;
      one_Ved_Nastr._idTypeVed = this._one_Ved_Nastr_Tmp._idTypeVed;
      one_Ved_Nastr._imsObjectType = this._imsObjectType_Curr;
      one_Ved_Nastr._nameVed = this._imsObjectType_Curr.ObjectName;
      one_Ved_Nastr._typeCreate = this._one_Ved_Nastr_Tmp._typeCreate;
      one_Ved_Nastr._vedomostTemplateObjectGuid = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid;
      one_Ved_Nastr._vedomostTemplateObjectGuid_B = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      this.isByloButtonTypeVedTo_Click = false;
    }
    else
    {
      one_Ved_Nastr._typeCreate = this._one_Ved_Nastr_Tmp._typeCreate;
      if (one_Ved_Nastr._vedomostTemplateObjectGuid == Guid.Empty)
        one_Ved_Nastr._vedomostTemplateObjectGuid = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid;
      one_Ved_Nastr._vedomostTemplateObjectGuid_B = this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid_B;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = (IDBObject) null;
        if (this._one_Ved_Nastr_Tmp._vedomostTemplateObjectGuid != Guid.Empty)
          dbObject = sessionKeeper.Session.GetObject(one_Ved_Nastr._vedomostTemplateObjectGuid, false);
        if (dbObject != null)
          this.templateID_Vyvod = dbObject.ObjectID;
        this.templateID_curr_Vyvod = this.templateID_Vyvod;
      }
      this.Processing_Template(one_Ved_Nastr._vedomostTemplateObjectGuid);
    }
    one_Ved_Nastr._isCreateDumpAuto = 1;
    this.isCreate = true;
    this._one_Ved_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(one_Ved_Nastr);
    this._one_Ved_Nastr_Tmp._accessLevel = this._one_Ved_Nastr_Curr._accessLevel;
    this.dataGridView_Sorting_Curr.Rows.Clear();
    this.Razdels_dataGridViewListRazdels.Rows.Clear();
    this.dataGridView_ListZagolovkov.Rows.Clear();
    this.Draw_All();
    this.isCreate = false;
    this.ModifiedAll(true);
    if (this._one_Ved_Nastr_Tmp._typeVed != Vedomost_VB.TypeVed.VSI)
    {
      this.tabControl_Usl_Bases.SelectedIndex = 0;
      this.tabControl_Page_Sbor.SelectedIndex = 0;
      this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
    }
    this.isCreate = false;
  }

  /// <summary>Кнопка Копировать из ...</summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonCopyFrom_Click(object sender, EventArgs e)
  {
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Ved;
    using (vyborVedomosti)
    {
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      bool flag = true;
      if (this._one_Ved_Nastr_Tmp._typeVed != vyborVedomosti._one_Ved_Nastr_Result._typeVed && MessageBox.Show("Типы ведомостей не совпадают\r\n\r\nКопировать настройки?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        flag = false;
      if (!flag)
        return;
      string name = this.tabControl_Nastr.SelectedTab.Name;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(name))
      {
        case 326637659:
          if (!(name == "tabPage_Service"))
            break;
          this.Draw_Page_Service();
          this.ModifiedAll(true);
          this.IsModified_Page_Service = true;
          break;
        case 1754209060:
          if (name == "tabPage_Merge")
            break;
          break;
        case 3109688624:
          if (!(name == "tabPage_Zagolovki"))
            break;
          this._one_Ved_Nastr_Tmp._zagolovki_Ved = Vedomost_VB_Static.Zagolovki_Ved_Copy(vyborVedomosti._one_Ved_Nastr_Result._zagolovki_Ved);
          this.dataGridView_ListZagolovkov.Rows.Clear();
          this.Draw_Page_Zagolovki();
          this.ModifiedAll(true);
          this.IsModified_Page_Zagolovki = true;
          break;
        case 3244506512:
          if (!(name == "tabPage_Sbor"))
            break;
          this._one_Ved_Nastr_Tmp._list_Usl_Read_From_SP = Vedomost_VB_Static.List_Usl_Read_From_SP_Copy(vyborVedomosti._one_Ved_Nastr_Result._list_Usl_Read_From_SP);
          this._one_Ved_Nastr_Tmp._list_Ved_ID = Vedomost_VB_Static.List_Ved_ID_Copy(vyborVedomosti._one_Ved_Nastr_Result._list_Ved_ID);
          this._one_Ved_Nastr_Tmp._sbor_Options = Vedomost_VB_Static.Sbor_Options_Init(this._guidTypeVed_Curr);
          this.Draw_Page_Sbor();
          this.ModifiedAll(true);
          this.IsModified_Page_Sbor = true;
          break;
        case 3316967738:
          if (!(name == "tabPage_Bases"))
            break;
          this._one_Ved_Nastr_Tmp._sbor_Options = Vedomost_VB_Static.Sbor_Options_Copy(vyborVedomosti._one_Ved_Nastr_Result._sbor_Options);
          this._one_Ved_Nastr_Tmp._bases_Options_Ved = Vedomost_VB_Static.Bases_Options_Ved_Copy(vyborVedomosti._one_Ved_Nastr_Result._bases_Options_Ved);
          this.Draw_Page_Bases();
          this.ModifiedAll(true);
          this.IsModified_Page_Bases = true;
          break;
        case 3885257238:
          if (!(name == "tabPage_Vyvod"))
            break;
          this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Copy(vyborVedomosti._one_Ved_Nastr_Result._algorithmToPrint);
          this.algorithmToPrint = this._one_Ved_Nastr_Tmp._algorithmToPrint;
          this._one_Ved_Nastr_Tmp._algorithmToPrint_B = (Vedomost_VB.AlgorithmToPrint) null;
          if (vyborVedomosti._one_Ved_Nastr_Result._algorithmToPrint_B != null)
          {
            Guid templateObjectGuidB = vyborVedomosti._one_Ved_Nastr_Result._vedomostTemplateObjectGuid_B;
            this.algorithmToPrint_B = Vedomost_VB_Static.AlgorithmToPrint_Copy(vyborVedomosti._one_Ved_Nastr_Result._algorithmToPrint_B);
            this.groupBox_Vyvod_Forma.Visible = true;
          }
          this.algorithmToPrint_curr = this.algorithmToPrint;
          this.algorithmToPrint_B = this._one_Ved_Nastr_Tmp._algorithmToPrint_B;
          this.Draw_Page_Vyvod();
          this.ModifiedAll(true);
          this.IsModified_Page_Vyvod = true;
          break;
        case 3927720027:
          if (!(name == "tabPage_Razdels"))
            break;
          this._one_Ved_Nastr_Tmp._list_RazdelsVed = Vedomost_VB_Static.List_RazdelsVed_Copy(vyborVedomosti._one_Ved_Nastr_Result._list_RazdelsVed);
          this.Razdels_dataGridViewListRazdels.Rows.Clear();
          this.Razdels_dataGridViewListPodRazdels.Rows.Clear();
          this.Draw_Page_Razdels();
          this.ModifiedAll(true);
          this.IsModified_Page_Razdels = true;
          this.IsModified_Page_PodRazdels = false;
          break;
        case 4107045604:
          if (!(name == "tabPage_Sorting"))
            break;
          this._one_Ved_Nastr_Tmp._sorting_Usl = Vedomost_VB_Static.Sorting_Usl_Copy(vyborVedomosti._one_Ved_Nastr_Result._sorting_Usl);
          this.ModifiedAll(true);
          this.IsModified_Page_Sortings = true;
          this.dataGridView_Sorting_Curr.Rows.Clear();
          if (this.dataGridView_Sorting_Curr == this.dataGridView_Sorting)
            this.dataGridView_Sorting_Draw();
          else
            this.dataGridView_Sorting_Doc_Draw();
          this.SelectDataGridView_Sorting_Row(0);
          this.Displays_The_Current_Record();
          this.dataGridView_Sorting_Curr.Focus();
          this.listBox_Sorting_List_Ved_Id.SelectedIndex = -1;
          this.listBox_Sorting_AttribVedRec.SelectedIndex = -1;
          break;
        case 4223869073:
          if (!(name == "tabPage_Xml"))
            break;
          this.ModifiedAll(true);
          break;
      }
    }
  }

  /// <summary> Кнопка Копировать все из ... </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonCopyAll_Click(object sender, EventArgs e)
  {
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Ved;
    using (vyborVedomosti)
    {
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      bool flag = true;
      if (this._one_Ved_Nastr_Tmp._typeVed != vyborVedomosti._one_Ved_Nastr_Result._typeVed && MessageBox.Show("Типы ведомостей не совпадают\r\n\r\nКопировать настройки?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        flag = false;
      if (this._one_Ved_Nastr_Tmp._nameVed == vyborVedomosti._one_Ved_Nastr_Result._nameVed)
      {
        int num = (int) MessageBox.Show("Нельзя копировать саму себя", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        flag = false;
      }
      if (!flag)
        return;
      this._one_Ved_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy_NotFull(vyborVedomosti._one_Ved_Nastr_Result, this._one_Ved_Nastr_Tmp);
      this.isCreate = true;
      this.dataGridView_Sorting_Curr.Rows.Clear();
      this.Razdels_dataGridViewListRazdels.Rows.Clear();
      this.dataGridView_ListZagolovkov.Rows.Clear();
      this.Draw_All();
      this.ModifiedAll(true);
      this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      this.isCreate = false;
    }
  }

  /// <summary> Выбор другого типа ведомости </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonSelectVed_Click(object sender, EventArgs e)
  {
    if (this.IsModifiedAll)
    {
      DialogResult dialogResult = MessageBox.Show("Параметры настройки изменены\r\n\r\nСохранить?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
      if (dialogResult == DialogResult.Yes)
      {
        if (this.ControlPages())
          return;
        if (this.Save() != "")
          dialogResult = DialogResult.Cancel;
      }
      if (dialogResult == DialogResult.No)
        this.ModifiedAll(false);
      if (dialogResult == DialogResult.Cancel)
        return;
    }
    VyborVedomosti vyborVedomosti = new VyborVedomosti();
    vyborVedomosti._typeDoc = Vedomost_VB.TypeDoc.Ved;
    using (vyborVedomosti)
    {
      vyborVedomosti._imsObjectTypeDel = this._imsObjectType_Curr;
      vyborVedomosti._caption = "Перейти к настройке ведомости ...";
      vyborVedomosti._list_ImsObjectType_With_One_Ved_Nastrs = Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr;
      if (vyborVedomosti.ShowDialog() != DialogResult.OK)
        return;
      if (!Vedomost_VB_Static.IsUse_New_System_ByOneNastr)
      {
        One_ImsObjectType_With_One_Ved_Nastr typeWithOneVedNastr = Vedomost_VB_Static.Checking_Use_Template(Vedomost_VB_Static._list_Ved_Arbeit_ImsObjectType_With_One_Ved_Nastr, vyborVedomosti._guidTemplateVed_Result, vyborVedomosti._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectTypeName);
        if (typeWithOneVedNastr != null)
        {
          int num = (int) MessageBox.Show($"{$"В документе \"{vyborVedomosti._one_ImsObjectType_With_One_Ved_Nastr.imsObjectType.ObjectName}\"" + "\r\nнастроено использование шаблона, который уже используется в другом документе"}\r\n\r\n\"{typeWithOneVedNastr.imsObjectType.ObjectName}\"" + "\r\n\r\nЭто не допускается" + "\r\n\r\nКаждому типу документа должен соответствовать свой шаблон", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
      }
      this.is_one_Ved_Nastr_New = false;
      this._guidTemplateVed_Curr = vyborVedomosti._guidTemplateVed_Result;
      this._guidTypeVed_Curr = vyborVedomosti._guidTypeVed_Result;
      this._documentName_Curr = vyborVedomosti._documentName_Result;
      this._imsObjectType_Curr = vyborVedomosti._imsObjectType_Result;
      if (vyborVedomosti._one_Ved_Nastr_Result != null)
      {
        this.isCreate = true;
        this._one_Ved_Nastr_Curr = Vedomost_VB_Static.One_Ved_Nastr_Copy(vyborVedomosti._one_Ved_Nastr_Result);
        this._one_Ved_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(vyborVedomosti._one_Ved_Nastr_Result);
        if (this._one_Ved_Nastr_Tmp._algorithmToPrint == null || this._one_Ved_Nastr_Tmp._typeCreateNastr == TypeCreateNastr.Empty || this._one_Ved_Nastr_Tmp._algorithmToPrint._list_OneRazdelToPrint == null && this._one_Ved_Nastr_Tmp._algorithmToPrint._oneRecordToPrint_Info == null)
        {
          if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
          {
            this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_EMPTY_Init();
            this._one_Ved_Nastr_Tmp._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Empty_Init();
          }
          else
          {
            this._one_Ved_Nastr_Tmp._algorithmToPrint = Vedomost_VB_Static.AlgorithmToPrint_Default_Init();
            this._one_Ved_Nastr_Tmp._algorithmXml = Vedomost_VB_Static.AlgorithmXml_Default_Init();
          }
          int num = (int) MessageBox.Show("Для данного типа документа настройки вывода и XML созданы программой\r\n\r\nПроверьте" + "\r\n\r\nИли на странице настройки \"Сервис\" выgолните команду \"Тип ведомости\"", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      else
      {
        this._one_Ved_Nastr_Curr = new One_Ved_Nastr(true, this.isKudaVhoditInfo, this.isItogoInfo);
        this._one_Ved_Nastr_Curr._vedomostTemplateObjectGuid = this._guidTemplateVed_Curr;
        this._one_Ved_Nastr_Curr._imsObjectType = this._imsObjectType_Curr;
        this._one_Ved_Nastr_Tmp = Vedomost_VB_Static.One_Ved_Nastr_Copy(this._one_Ved_Nastr_Curr);
        this.is_one_Ved_Nastr_New = true;
        int num = (int) MessageBox.Show("Для данного типа документа настройки отсутствуют", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        this.isCreate = true;
      }
      this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      this.tabControl_Usl_Bases.SelectedTab = this.tabPage_Usl_Bases_SborDialog;
      this.Processing_Template(this._guidTemplateVed_Curr);
      this.Text = "Настройка ведомости:";
      this.Text = $"{this.Text} [{this._imsObjectType_Curr.ObjectName}]";
      if (this._one_Ved_Nastr_Curr._dateIni != "")
        this.Text = $"{this.Text} {this._one_Ved_Nastr_Curr._dateIni}";
      this.dataGridView_Sorting_Curr.Rows.Clear();
      this.Razdels_dataGridViewListRazdels.Rows.Clear();
      this.dataGridView_ListZagolovkov.Rows.Clear();
      this.Draw_All();
      if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VSI)
      {
        this.tabControl_Usl_Bases.SelectedTab = this.tabPage_Usl_Bases_SborDialog;
        this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      }
      else
      {
        if (this.tabControl_Usl_Bases.TabPages.Count > 1)
          this.tabControl_Usl_Bases.SelectedTab = this.tabPage_Bases_Main;
        this.tabControl_Page_Sbor.SelectedTab = this.tabPage_Sbor_Usl;
        this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      }
      this.IsModifiedFromFile = false;
      this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
      this.isCreate = false;
      Vedomost_VB_Static.xmlProtocol_Last = (XmlDocument) null;
      Vedomost_VB_Static.xml_SborMainVed_Dump_Last = (XmlDocument) null;
      Vedomost_VB_Static.xml_SborVed_Dump_Last = (XmlDocument) null;
      Vedomost_VB_Static.imDocument = (ImDocument) null;
    }
  }

  /// <summary> Выключение и включение постраничной кнопки "По умолчанию" </summary>
  private void IsButtonDefault()
  {
    if (this._one_Ved_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.isButtonDefault = true;
      this.buttonDefault.Visible = true;
      this.buttonServicesDefaultAll.Visible = true;
      this.label_ServicesDefaultAll.Visible = true;
    }
    else
    {
      Vedomost_VB.TypeVed typeVed = this._one_Ved_Nastr_Tmp._typeVed;
      for (int index = 0; index < Vedomost_VB_Static.List_TypeVed_Systems.Count; ++index)
      {
        if (Vedomost_VB_Static.List_TypeVed_Systems[index].typeVed == typeVed)
        {
          this.isButtonDefault = true;
          this.buttonDefault.Visible = true;
          this.buttonServicesDefaultAll.Visible = true;
          this.label_ServicesDefaultAll.Visible = true;
          return;
        }
      }
      this.isButtonDefault = false;
      this.buttonDefault.Visible = false;
      this.buttonServicesDefaultAll.Visible = false;
      this.label_ServicesDefaultAll.Visible = false;
    }
  }

  /// <summary> Выключение и включение нижней кнопки "Копировать из ..." </summary>
  private void IsButtonCopyFrom()
  {
    if (this._one_Ved_Nastr_Tmp._typeCreate == Vedomost_VB.TypeCreate.System)
    {
      this.buttonCopyFrom.Visible = false;
      this.buttonServicesCopyAll.Visible = false;
      this.label_ServicesCopyAll.Visible = false;
      this.buttonServicesTypeVedTo.Visible = false;
      this.label_ServicesTypeVedTo.Visible = false;
    }
    else
    {
      this.buttonCopyFrom.Visible = true;
      this.buttonServicesCopyAll.Visible = true;
      this.label_ServicesCopyAll.Visible = true;
      this.buttonServicesTypeVedTo.Visible = true;
      this.label_ServicesTypeVedTo.Visible = true;
    }
  }

  private void IsButtonB()
  {
    if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VS || this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.VP)
    {
      this.isButtonB = true;
      this.buttonSevicesForGroupB.Visible = true;
      this.label_SevicesForGroupB.Visible = true;
    }
    else
    {
      this.isButtonB = false;
      this.buttonSevicesForGroupB.Visible = false;
      this.label_SevicesForGroupB.Visible = false;
    }
  }

  private void buttonWarnings_Click(object sender, EventArgs e)
  {
    string text = "";
    for (int index = 0; index < this.listWarnings.Count; ++index)
    {
      if (index > 0)
        text += "\r\n\r\n";
      string listWarning = this.listWarnings[index];
      text += listWarning;
    }
    int num = (int) MessageBox.Show(text, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  private void Synchronization_list_RazdelsVed_and_list_OneRazdelToPrint()
  {
    if (this.algorithmToPrint_curr._list_OneRazdelToPrint == null || this.algorithmToPrint_curr._list_OneRazdelToPrint.Count == 0)
      return;
    this.algorithmToPrint._oneRecordToPrint_Info = (Vedomost_VB.OneRecordToPrint) null;
    Vedomost_VB.OneRazdelToPrint oneRazdelToPrint1 = this.algorithmToPrint_curr._list_OneRazdelToPrint[0];
    for (int index1 = 0; index1 < this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count; ++index1)
    {
      int index2 = index1;
      Vedomost_VB.OneRazdelVed oneRazdelVed = this._one_Ved_Nastr_Tmp._list_RazdelsVed[index1];
      if (!(oneRazdelVed._name == "Ведомости составных частей") && oneRazdelVed._razdelVed != 1000)
      {
        if (index2 < this.algorithmToPrint_curr._list_OneRazdelToPrint.Count)
        {
          Vedomost_VB.OneRazdelToPrint oneRazdelToPrint2 = this.algorithmToPrint_curr._list_OneRazdelToPrint[index2];
          if (oneRazdelToPrint2._razdelVed != oneRazdelVed._razdelVed && oneRazdelVed._razdelVed > oneRazdelToPrint2._razdelVed)
          {
            while (oneRazdelVed._razdelVed > oneRazdelToPrint2._razdelVed && index2 < this.algorithmToPrint_curr._list_OneRazdelToPrint.Count)
            {
              this.algorithmToPrint_curr._list_OneRazdelToPrint.RemoveAt(index2);
              oneRazdelToPrint2 = this.algorithmToPrint_curr._list_OneRazdelToPrint[index2];
              int razdelVed1 = oneRazdelToPrint2._razdelVed;
              int razdelVed2 = oneRazdelVed._razdelVed;
            }
          }
        }
        else
          this.algorithmToPrint_curr._list_OneRazdelToPrint.Add(new Vedomost_VB.OneRazdelToPrint()
          {
            _oneRecordToPrint_Info = Vedomost_VB_Static.oneRecordToPrint_Copy(oneRazdelToPrint1._oneRecordToPrint_Info),
            _razdelVed = oneRazdelVed._razdelVed
          });
      }
    }
    for (int index3 = this.algorithmToPrint_curr._list_OneRazdelToPrint.Count - 1; index3 > -1; --index3)
    {
      Vedomost_VB.OneRazdelToPrint oneRazdelToPrint3 = this.algorithmToPrint_curr._list_OneRazdelToPrint[index3];
      bool flag = false;
      for (int index4 = this._one_Ved_Nastr_Tmp._list_RazdelsVed.Count - 1; index4 > -1; --index4)
      {
        if (this._one_Ved_Nastr_Tmp._list_RazdelsVed[index4]._razdelVed == oneRazdelToPrint3._razdelVed)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.algorithmToPrint_curr._list_OneRazdelToPrint.RemoveAt(index3);
    }
  }

  /// <summary> Рисование списка собранных атрибутов системы  </summary>
  /// <param textFromColumn="listBox"></param>
  private void List_Ved_Id_Draw(ListBox listBox)
  {
    listBox.Items.Clear();
    if (this._one_Ved_Nastr_Tmp._list_Ved_ID == null)
      return;
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._list_Ved_ID.Count; ++index)
    {
      string name = this._one_Ved_Nastr_Tmp._list_Ved_ID[index]._name;
      listBox.Items.Add((object) name);
    }
    listBox.SelectedIndex = -1;
  }

  /// <summary> Установить курсор по тексту строки </summary>
  /// <param textFromColumn="listBox"></param>
  /// <param textFromColumn="text"></param>
  private void List_Ved_Id_SelectedValue(ListBox listBox, string text)
  {
    if (string.IsNullOrEmpty(text))
    {
      listBox.SelectedIndex = -1;
    }
    else
    {
      for (int index = 0; index < listBox.Items.Count; ++index)
      {
        string str = listBox.Items[index].ToString();
        if (text == str)
        {
          listBox.SelectedIndex = index;
          break;
        }
      }
    }
  }

  /// <summary> Установить курсор по objType </summary>
  /// <param textFromColumn="listBox"></param>
  /// <param textFromColumn="list_Ved_ID"></param>
  /// <param textFromColumn="objType"></param>
  private void List_Ved_Id_SelectedByObjType(
    ListBox listBox,
    List<Vedomost_VB.OneFieldSpForRead> list_Ved_ID,
    int objType)
  {
    if (objType == 0 || objType == -1)
    {
      listBox.SelectedIndex = -1;
    }
    else
    {
      for (int index = 0; index < list_Ved_ID.Count; ++index)
      {
        Vedomost_VB.OneFieldSpForRead oneFieldSpForRead = list_Ved_ID[index];
        if (objType == oneFieldSpForRead._id)
        {
          listBox.SelectedIndex = index;
          return;
        }
      }
      listBox.SelectedIndex = -1;
    }
  }

  /// <summary> AttrId по номеру </summary>
  /// <param textFromColumn="list_Ved_ID"></param>
  /// <param textFromColumn="index"></param>
  /// <returns></returns>
  private int Get_ObjType_By_index(List<Vedomost_VB.OneFieldSpForRead> list_Ved_ID, int index)
  {
    return index < 0 ? -1 : list_Ved_ID[index]._id;
  }

  /// <summary> СЛУШАЮ И ПОВИНУЮСЬ </summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void panelForButtons_MouseClick(object sender, MouseEventArgs e)
  {
    if ((Control.ModifierKeys & Keys.Control) != Keys.Control || (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
      return;
    Vedomost_VB_Static.isHozain = true;
    int num = (int) MessageBox.Show("Слушаю и повинуюсь", "ПРИВЕТ!");
  }

  /// <summary>  Прорисовка страницы сортировки </summary>
  private void Draw_Page_Merge()
  {
    if (this._one_Ved_Nastr_Tmp._merge_Usl2 == null)
      this._one_Ved_Nastr_Tmp._merge_Usl2 = new Vedomost_VB.Merge_Usl2();
    this.List_Ved_Id_Draw(this.listBox_Merge_List_Ved_Id);
    this.listBox_Sorting_AttribVedRec_Filled();
    this.listBox_Merge_AttribVedRec_Filled();
    this.listBox_Merge_List_Merge_Usl2_Draw();
  }

  /// <summary> Заполнение списка специализированных аттрибутов </summary>
  private void listBox_Merge_AttribVedRec_Filled()
  {
    this.listBox_Merge_AttribVedRec.Items.Clear();
    for (int index = 0; index < Vedomost_VB_Static._listOneAttribVedRec.Count; ++index)
      this.listBox_Merge_AttribVedRec.Items.Add((object) Vedomost_VB_Static._listOneAttribVedRec[index]._name);
  }

  private void listBox_Merge_List_Merge_Usl2_Draw()
  {
    this.listBox_Merge_List_Merge_Usl2.Items.Clear();
    if (this._one_Ved_Nastr_Tmp._merge_Usl2 == null || this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2 == null || this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2.Count == 0)
      return;
    for (int index = 0; index < this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2.Count; ++index)
      this.listBox_Merge_List_Merge_Usl2.Items.Add((object) this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2[index].Name_Attribut());
    if (!this.isCreate || this.listBox_Merge_List_Merge_Usl2.Items.Count <= 0)
      return;
    this.listBox_Merge_List_Merge_Usl2.SelectedIndex = 0;
  }

  private void listBox_Merge_List_Merge_Usl2_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.listBox_Merge_List_Ved_Id.SelectedIndex = -1;
    this.listBox_Merge_AttribVedRec.SelectedIndex = -1;
    if (this.listBox_Merge_List_Merge_Usl2.SelectedIndex < 0)
      return;
    Vedomost_VB.Merge_Usl_One mergeUslOne = this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2[this.listBox_Merge_List_Merge_Usl2.SelectedIndex];
    if (mergeUslOne._typeField == Vedomost_VB.TypeField.ObjectType)
    {
      this.List_Ved_Id_SelectedByObjType(this.listBox_Merge_List_Ved_Id, this._one_Ved_Nastr_Tmp._list_Ved_ID, mergeUslOne._objectType);
      this.listBox_Merge_AttribVedRec.SelectedIndex = -1;
    }
    if (mergeUslOne._typeField != Vedomost_VB.TypeField.TypeFieldVedRec)
      return;
    int index = -1;
    Vedomost_VB_Static.oneAttribVed_by_TypeFieldVedRec(mergeUslOne._typeFieldVedRec, out index);
    this.listBox_Merge_AttribVedRec.SelectedIndex = index;
    this.listBox_Merge_List_Ved_Id.SelectedIndex = -1;
  }

  private void listBox_Merge_List_Ved_Id_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Merge_AttribVedRec.SelectedIndex = -1;
  }

  private void listBox_Merge_AttribVedRec_MouseClick(object sender, MouseEventArgs e)
  {
    this.listBox_Merge_List_Ved_Id.SelectedIndex = -1;
  }

  private Vedomost_VB.Merge_Usl_One Create_merge_Usl_One()
  {
    if (this.listBox_Merge_AttribVedRec.SelectedIndex < 0 && this.listBox_Merge_List_Ved_Id.SelectedIndex < 0)
      return (Vedomost_VB.Merge_Usl_One) null;
    Vedomost_VB.Merge_Usl_One mergeUslOne = new Vedomost_VB.Merge_Usl_One();
    if (this.listBox_Merge_List_Ved_Id.SelectedIndex > 0)
    {
      mergeUslOne._typeField = Vedomost_VB.TypeField.ObjectType;
      mergeUslOne._typeFieldVedRec = Vedomost_VB.TypeFieldVedRec.Undefined;
      mergeUslOne._objectType = this.Get_ObjType_By_index(this._one_Ved_Nastr_Tmp._list_Ved_ID, this.listBox_Merge_List_Ved_Id.SelectedIndex);
    }
    else
    {
      mergeUslOne._typeField = Vedomost_VB.TypeField.TypeFieldVedRec;
      mergeUslOne._objectType = -1;
      Vedomost_VB.OneAttribVedRec oneAttribVedRec = Vedomost_VB_Static._listOneAttribVedRec[this.listBox_Merge_AttribVedRec.SelectedIndex];
      mergeUslOne._typeFieldVedRec = oneAttribVedRec._typeFieldVedRec;
    }
    return mergeUslOne;
  }

  private int Check_In_listBox_Merge_List_Merge_Usl2(Vedomost_VB.Merge_Usl_One merge_Usl_One)
  {
    return merge_Usl_One == null ? -1 : this._one_Ved_Nastr_Tmp._merge_Usl2.Find_Merge_Usl_One(merge_Usl_One);
  }

  private void button_Merge_Add_Click(object sender, EventArgs e)
  {
    Vedomost_VB.Merge_Usl_One mergeUslOne = this.Create_merge_Usl_One();
    if (mergeUslOne == null)
      return;
    int num = this.Check_In_listBox_Merge_List_Merge_Usl2(mergeUslOne);
    if (num < 0)
    {
      this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2.Add(mergeUslOne);
      this.listBox_Merge_List_Merge_Usl2.Items.Add((object) mergeUslOne.Name_Attribut());
      this.ModifiedAll(true);
      num = this.listBox_Merge_List_Merge_Usl2.Items.Count - 1;
    }
    this.listBox_Merge_List_Merge_Usl2.SelectedIndex = num;
  }

  private void Delete_From_listBox_Merge_List_Merge_Usl2()
  {
    int selectedIndex = this.listBox_Merge_List_Merge_Usl2.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.listBox_Merge_List_Merge_Usl2.Items.RemoveAt(selectedIndex);
    this._one_Ved_Nastr_Tmp._merge_Usl2._list_Merge_Usl2.RemoveAt(selectedIndex);
    if (this.listBox_Merge_List_Merge_Usl2.Items.Count == 0)
    {
      this.listBox_Merge_List_Merge_Usl2.SelectedIndex = -1;
    }
    else
    {
      if (selectedIndex >= this.listBox_Merge_List_Merge_Usl2.Items.Count)
        --selectedIndex;
      this.listBox_Merge_List_Merge_Usl2.SelectedIndex = selectedIndex;
    }
    this.ModifiedAll(true);
  }

  private void button_Merge_Del_Click(object sender, EventArgs e)
  {
    this.Delete_From_listBox_Merge_List_Merge_Usl2();
  }

  private void listBox_Merge_List_Merge_Usl2_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    this.Delete_From_listBox_Merge_List_Merge_Usl2();
  }

  /// <summary>Редактор шаблона (бланка)</summary>
  /// <param textFromColumn="sender"></param>
  /// <param textFromColumn="e"></param>
  private void buttonEditTemplate_Click(object sender, EventArgs e)
  {
    DocumentEditorPlugin.Instance.OpenDocumentImDocumentObject(this.templateID_curr_Vyvod, false, true);
    int num = (int) MessageBox.Show("Редактор шаблона (бланка) открыт на общей панели\r\nДля доступа к редактору закройте окно настройки", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }

  /// <summary> Спрятать страницы (или наоборот - открыть), то что нужно для АВТОСБОРА ведомости </summary>
  /// <param textFromColumn="hide"></param>
  private void HidePages(bool hide)
  {
    if (this.isCreate)
    {
      if (hide)
      {
        this.tabControl_Usl_Bases.TabPages.Remove(this.tabPage_Bases_Main);
        this.tabControl_Usl_Bases.TabPages.Remove(this.tabPage_Usl_Bases_Sbor);
        this.tabControl_Page_Sbor.TabPages.Remove(this.tabPage_Sbor_Usl);
        this.tabControl_Page_Sbor.TabPages.Remove(this.tabPage_Sbor_Others);
        this.tabPage_Sbor.Text = "Данные";
        if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
        {
          this.tabPage_ESPD.Parent = (Control) this.tabControl_Page_Sbor;
        }
        else
        {
          this.tabControl_Nastr.TabPages.Remove(this.tabPage_Sorting);
          this.tabPage_ESPD.Parent = (Control) null;
        }
        this.tabControl_Nastr.TabPages.Remove(this.tabPage_Merge);
        this.tabControl_Usl_Bases.SelectedIndex = 2;
        this.tabControl_Page_Sbor.SelectedIndex = 0;
        this.tabControl_Nastr.SelectedTab = this.tabPage_Bases;
        this.tabPage_Usl_Bases_SborDialog.Select();
      }
      else
      {
        if (this.tabControl_Usl_Bases.TabPages.Count < 3)
        {
          this.tabControl_Usl_Bases.TabPages.Insert(0, this.tabPage_Bases_Main);
          this.tabControl_Usl_Bases.TabPages.Insert(1, this.tabPage_Usl_Bases_Sbor);
          this.Draw_Page_Bases();
        }
        if (this.tabControl_Page_Sbor.TabPages.Count == 1)
        {
          this.tabControl_Page_Sbor.TabPages.Insert(0, this.tabPage_Sbor_Usl);
          this.tabControl_Page_Sbor.TabPages.Insert(1, this.tabPage_Sbor_Others);
          this.tabPage_Sbor.Text = "Правила сбора";
          this.Draw_Page_Sbor();
        }
        if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
          this.tabPage_ESPD.Parent = (Control) this.tabControl_Page_Sbor;
        else
          this.tabPage_ESPD.Parent = (Control) null;
        System.Windows.Forms.TabPage tabPage1 = this.tabControl_Nastr.TabPages[1];
        System.Windows.Forms.TabPage tabPage2 = this.tabControl_Nastr.TabPages[2];
        if (tabPage1 != null && tabPage1.Name != "tabPage_Sorting" && tabPage2 != null && tabPage2.Name != "tabPage_Sorting")
        {
          this.tabControl_Nastr.TabPages.Insert(2, this.tabPage_Sorting);
          this.Draw_Page_Usl_Sorting();
        }
        System.Windows.Forms.TabPage tabPage3 = this.tabControl_Nastr.TabPages[2];
        System.Windows.Forms.TabPage tabPage4 = this.tabControl_Nastr.TabPages[3];
        if (tabPage3 == null || !(tabPage3.Name != "tabPage_Merge") || !(tabPage4.Name != "tabPage_Merge"))
          return;
        this.tabControl_Nastr.TabPages.Insert(3, this.tabPage_Merge);
        this.Draw_Page_Merge();
      }
    }
    else if (hide)
    {
      this.tabControl_Usl_Bases.TabPages.Remove(this.tabPage_Bases_Main);
      this.tabControl_Usl_Bases.TabPages.Remove(this.tabPage_Usl_Bases_Sbor);
      this.Draw_Page_Bases();
      this.tabControl_Page_Sbor.TabPages.Remove(this.tabPage_Sbor_Usl);
      this.tabControl_Page_Sbor.TabPages.Remove(this.tabPage_Sbor_Others);
      this.tabPage_Sbor.Text = "Данные";
      if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
      {
        this.tabPage_ESPD.Parent = (Control) this.tabControl_Page_Sbor;
      }
      else
      {
        this.tabControl_Nastr.TabPages.Remove(this.tabPage_Sorting);
        this.tabPage_ESPD.Parent = (Control) null;
      }
      this.tabControl_Nastr.TabPages.Remove(this.tabPage_Merge);
    }
    else
    {
      if (this.tabControl_Usl_Bases.TabPages.Count < 3)
      {
        this.tabControl_Usl_Bases.TabPages.Insert(0, this.tabPage_Bases_Main);
        this.tabControl_Usl_Bases.TabPages.Insert(1, this.tabPage_Usl_Bases_Sbor);
        this.Draw_Page_Bases();
      }
      if (this.tabControl_Page_Sbor.TabPages.Count == 1)
      {
        this.tabControl_Page_Sbor.TabPages.Insert(0, this.tabPage_Sbor_Usl);
        this.tabControl_Page_Sbor.TabPages.Insert(1, this.tabPage_Sbor_Others);
        this.tabPage_Sbor.Text = "Правила сбора";
        this.Draw_Page_Sbor();
      }
      if (this._one_Ved_Nastr_Tmp._typeVed == Vedomost_VB.TypeVed.ESPD)
        this.tabPage_ESPD.Parent = (Control) this.tabControl_Page_Sbor;
      else
        this.tabPage_ESPD.Parent = (Control) null;
      System.Windows.Forms.TabPage tabPage5 = this.tabControl_Nastr.TabPages[2];
      if (tabPage5 != null && tabPage5.Name != "tabPage_Sorting")
      {
        this.tabControl_Nastr.TabPages.Insert(2, this.tabPage_Sorting);
        this.Draw_Page_Usl_Sorting();
      }
      System.Windows.Forms.TabPage tabPage6 = this.tabControl_Nastr.TabPages[2];
      System.Windows.Forms.TabPage tabPage7 = this.tabControl_Nastr.TabPages[3];
      if (tabPage6 == null || !(tabPage6.Name != "tabPage_Merge") || !(tabPage7.Name != "tabPage_Merge"))
        return;
      this.tabControl_Nastr.TabPages.Insert(3, this.tabPage_Merge);
      this.Draw_Page_Merge();
    }
  }

  private void buttonCheck_Click(object sender, EventArgs e)
  {
    ListError_OneError listError_OneError = new ListError_OneError();
    Vedomost_VB_Static.Checking_Template_And_Nastr(listError_OneError, this.algorithmToPrint_curr, this.imDocument_template_Vyvod);
    if (listError_OneError._list.Count == 0)
    {
      int num1 = (int) MessageBox.Show("Замечаний нет", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      listError_OneError.Sort();
      listError_OneError.Union();
      listError_OneError.CreateErrorMessage();
      ListMessage listMessage = new ListMessage();
      listMessage._listStr = listError_OneError._listStr;
      listMessage._listStr.Insert(0, this.imDocument_template_Vyvod.Name);
      int num2 = (int) listMessage.ShowDialog();
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (A_NastrVed));
    DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle13 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle14 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle15 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle16 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle17 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle18 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle19 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle20 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle21 = new DataGridViewCellStyle();
    DataGridViewCellStyle gridViewCellStyle22 = new DataGridViewCellStyle();
    this.panelForButtons = new Panel();
    this.buttonWarnings = new Button();
    this.buttonCopyFrom = new Button();
    this.buttonSelectVed = new Button();
    this.buttonSave1 = new Button();
    this.buttonDefault = new Button();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.tabControl_Nastr = new System.Windows.Forms.TabControl();
    this.tabPage_Bases = new System.Windows.Forms.TabPage();
    this.tabControl_Usl_Bases = new System.Windows.Forms.TabControl();
    this.tabPage_Bases_Main = new System.Windows.Forms.TabPage();
    this.groupBox_SpecificationSections = new GroupBox();
    this.checkBox_Specification_Instrument = new CheckBox();
    this.drawGrid_SpecificationSections = new DataGridView();
    this.Column5 = new DataGridViewCheckBoxColumn();
    this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
    this.checkBox_Usl_Bases_isOnlyUroven1 = new CheckBox();
    this.label_Usl_Bases_MainCaption = new Label();
    this.groupBox_Usl_Bases_MainStep = new GroupBox();
    this.checkBox_Usl_Bases_isMainSumm = new CheckBox();
    this.checkBox_Usl_Bases_isMainCreateVtorRecords = new CheckBox();
    this.checkBox_Usl_Bases_isMainSort2 = new CheckBox();
    this.checkBox_Usl_Bases_isMainSummOdinakovyh = new CheckBox();
    this.checkBox_Usl_Bases_isMainSort1 = new CheckBox();
    this.tabPage_Usl_Bases_Sbor = new System.Windows.Forms.TabPage();
    this.groupBox_Usl_Bases_Sbor_For_ZIP = new GroupBox();
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr = new CheckBox();
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr = new CheckBox();
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel = new GroupBox();
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl = new RadioButton();
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc = new RadioButton();
    this.label_Usl_Bases_Sbor1 = new Label();
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp = new CheckBox();
    this.groupBox_Usl_Bases_Sbor_VedStep = new GroupBox();
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor = new CheckBox();
    this.groupBox_Usl_Bases_Sbor_VedGroup = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedUnion = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedSort1 = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup = new CheckBox();
    this.tabPage_Usl_Bases_SborDialog = new System.Windows.Forms.TabPage();
    this.groupBox_Usl_Bases_ImbaseCatalog = new GroupBox();
    this.label_QuickObjectInfo = new Label();
    this.label_CatalogsImbase = new Label();
    this.button_Delete_From_To_listBox_QuickObjectInfo = new Button();
    this.button_Add_To_listBox_QuickObjectInfo = new Button();
    this.listBox_CatalogsImbase = new ListBox();
    this.listBox_QuickObjectInfo = new ListBox();
    this.groupBox_Usl_Bases_Sbor_Input = new GroupBox();
    this.checkBox_Usl_Bases_Sbor_isInputMat = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isInputIzd = new CheckBox();
    this.checkBox_Usl_Bases_Sbor_isInputDoc = new CheckBox();
    this.tabPage_Sbor = new System.Windows.Forms.TabPage();
    this.tabControl_Page_Sbor = new System.Windows.Forms.TabControl();
    this.tabPage_Sbor_Usl = new System.Windows.Forms.TabPage();
    this.Sbor_Usl_Panel = new Panel();
    this.label_UsloviaSbora_Current = new Label();
    this.groupBox_Sbor_Usl_I_ILI = new GroupBox();
    this.radioButton_Sbor_Usl_Ili = new RadioButton();
    this.radioButton_Sbor_Usl_I = new RadioButton();
    this.groupBox_Sbor_Usl_Text = new GroupBox();
    this.textBox_Sbor_Usl_TextDliaSravnenia = new TextBox();
    this.groupBox_Sbor_Usl_Sravnenie = new GroupBox();
    this.radioButton_Sbor_Usl_Nathinaetsia = new RadioButton();
    this.radioButton_Sbor_Usl_NeSoderzit = new RadioButton();
    this.radioButton_Sbor_Usl_Soderzit = new RadioButton();
    this.radioButton_Sbor_Usl_NeRavno = new RadioButton();
    this.radioButton_Sbor_Usl_Ravno = new RadioButton();
    this.groupBox_Sbor_Usl_AttributeControl1 = new GroupBox();
    this.select_Sbor_Usl_AttributeControl1 = new SelectAvsAttributeControl();
    this.button_Sbor_Usl_NeVvodit = new Button();
    this.button_Sbor_Usl_BezUsl = new Button();
    this.button_Sbor_Usl_Delete1 = new Button();
    this.button_Sbor_Usl_Edit1 = new Button();
    this.button_Sbor_Usl_Add1 = new Button();
    this.groupBox_Sbor_Usl_CollapsedTreeView = new GroupBox();
    this.radioButtonCollapsedEmpty = new RadioButton();
    this.radioButtonExpanded = new RadioButton();
    this.radioButtonCollapseAll = new RadioButton();
    this.groupBox_UsloviaVvoda = new GroupBox();
    this.treeView_UsloviaSbora = new TreeView();
    this.imageList1 = new ImageList(this.components);
    this.tabPage_Sbor_Peredatha = new System.Windows.Forms.TabPage();
    this.button_Sbor_Peredatha_Delete2 = new Button();
    this.button_Sbor_Peredatha_Add2 = new Button();
    this.groupBox_Sbor_Peredatha_AttributeControl1 = new GroupBox();
    this.select_Sbor_Peredatha_AttributeControl2 = new SelectAvsAttributeControl();
    this.groupBox_Sbor_Peredatha_ListId = new GroupBox();
    this.listBox_Sbor_Peredatha_ListId = new ListBox();
    this.tabPage_Sbor_Others = new System.Windows.Forms.TabPage();
    this.checkBox_Others_Reference_Show = new CheckBox();
    this.groupBox_Sbor_Others_DopZam = new GroupBox();
    this.checkBox_Sbor_Others_IsAllocateDopZam = new CheckBox();
    this.checkBox_Sbor_Others_IsDopZam = new CheckBox();
    this.groupBox_Sbor_Others_Complecty = new GroupBox();
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty = new CheckBox();
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty = new CheckBox();
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved = new GroupBox();
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved = new CheckBox();
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit = new CheckBox();
    this.tabPage_Sbor_Usl_Reference = new System.Windows.Forms.TabPage();
    this.Sbor_Usl_Reference_Panel = new Panel();
    this.groupBox_Sbor_Usl_I_ILI_Reference = new GroupBox();
    this.radioButton_Sbor_Usl_Ili_Reference = new RadioButton();
    this.radioButton_Sbor_Usl_I_Reference = new RadioButton();
    this.groupBox_Sbor_Usl_Text_Reference = new GroupBox();
    this.textBox_Sbor_Usl_TextDliaSravnenia_Reference = new TextBox();
    this.groupBox_Sbor_Usl_Sravnenie_Reference = new GroupBox();
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference = new RadioButton();
    this.radioButton_Sbor_Usl_NeSoderzit_Reference = new RadioButton();
    this.radioButton_Sbor_Usl_Soderzit_Reference = new RadioButton();
    this.radioButton_Sbor_Usl_NeRavno_Reference = new RadioButton();
    this.radioButton_Sbor_Usl_Ravno_Reference = new RadioButton();
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference = new GroupBox();
    this.radioButtonCollapsedEmpty_Reference = new RadioButton();
    this.radioButtonExpanded_Reference = new RadioButton();
    this.radioButtonCollapseAll_Reference = new RadioButton();
    this.groupBox_UsloviaVvoda_Reference = new GroupBox();
    this.treeView_UsloviaSbora_Reference = new TreeView();
    this.groupBox_Sbor_Usl_AttributeControl_Reference = new GroupBox();
    this.select_Sbor_Usl_AttributeControl_Reference = new SelectAvsAttributeControl();
    this.button_Sbor_Usl_Reference_NeVvodit = new Button();
    this.button_Sbor_Usl_Reference_BezUsl = new Button();
    this.button_Sbor_Usl_Reference_Delete1 = new Button();
    this.button_Sbor_Usl_Reference_Edit1 = new Button();
    this.button_Sbor_Usl_Reference_Add1 = new Button();
    this.tabPage_ESPD = new System.Windows.Forms.TabPage();
    this.groupBox_Remark = new GroupBox();
    this.textBox_textRemark = new TextBox();
    this.checkBox_isAddRemark = new CheckBox();
    this.groupBox_AddToSP = new GroupBox();
    this.checkBox_isAddToSpLU = new CheckBox();
    this.groupBox_FirstOpen = new GroupBox();
    this.checkBox_isOpenLU = new CheckBox();
    this.checkBox_isCreateLU = new CheckBox();
    this.checkBox_isAddLU = new CheckBox();
    this.tabPage_Sorting = new System.Windows.Forms.TabPage();
    this.dataGridView_Sorting_Doc = new DataGridView();
    this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
    this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn17 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn18 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn19 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn20 = new DataGridViewTextBoxColumn();
    this.groupBox_Sorting_List_Ved_Graf = new GroupBox();
    this.listBox_Sorting_List_Ved_Graf = new ListBox();
    this.groupBox_Sorting_List_Ved_Id = new GroupBox();
    this.listBox_Sorting_List_Ved_Id = new ListBox();
    this._btnMoveDown_Sorting = new Button();
    this._btnMoveUp_Sorting = new Button();
    this.buttonDelete_Sorting_1 = new Button();
    this.buttonAdd_Sorting_1 = new Button();
    this.buttonEdit_Sorting_1 = new Button();
    this.groupBox_Sorting_PoriadokSortirovki = new GroupBox();
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie = new RadioButton();
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie = new RadioButton();
    this.groupBox_Sorting_PustyeStroki = new GroupBox();
    this.radioButton_Sorting_PustyeStrokiVkonce = new RadioButton();
    this.radioButton_Sorting_PustyeStrokiVnathale = new RadioButton();
    this.groupBox_Sorting_Sravnenie = new GroupBox();
    this.radioButton_Sorting_SravnenieNumber = new RadioButton();
    this.radioButton_Sorting_SravnenieSymbol = new RadioButton();
    this.groupBox_Sorting_End = new GroupBox();
    this.comboBox_Sorting_SymbolEnd = new ComboBox();
    this.labelEnd_Sorting_2 = new Label();
    this.numericUpDown_Sorting_NumberEnd = new NumericUpDown();
    this.labelEnd_Sorting_1 = new Label();
    this.groupBox_Sorting_Do = new GroupBox();
    this.radioButton_Sorting_DoSymbolNumbEnd = new RadioButton();
    this.radioButton_Sorting_DoSymbolNumb = new RadioButton();
    this.radioButton_Sorting_DoBukvyNumb = new RadioButton();
    this.radioButton_Sorting_DoEnd = new RadioButton();
    this.groupBox_Sorting_Begin = new GroupBox();
    this.comboBox_Sorting_SymbolBegin = new ComboBox();
    this.labelBegin_Sorting_2 = new Label();
    this.numericUpDown_Sorting_NumberBegin = new NumericUpDown();
    this.labelBegin_Sorting_1 = new Label();
    this.groupBox_Sorting_Ot = new GroupBox();
    this.radioButton_Sorting_OtSymbolNumbEnd = new RadioButton();
    this.radioButton_Sorting_OtSymbolNumb = new RadioButton();
    this.radioButton_Sorting_OtBukvyNumb = new RadioButton();
    this.radioButton_Sorting_OtBegin = new RadioButton();
    this.dataGridView_Sorting = new DataGridView();
    this.ImgColumn = new DataGridViewImageColumn();
    this.ColumnAttribut = new DataGridViewTextBoxColumn();
    this.ColumnOt = new DataGridViewTextBoxColumn();
    this.ColumnDo = new DataGridViewTextBoxColumn();
    this.ColumnSravnenie = new DataGridViewTextBoxColumn();
    this.ColumnPustye = new DataGridViewTextBoxColumn();
    this.groupBox_Sorting_AttribVedRec1 = new GroupBox();
    this.listBox_Sorting_AttribVedRec = new ListBox();
    this.tabPage_Merge = new System.Windows.Forms.TabPage();
    this.button_Merge_Del = new Button();
    this.button_Merge_Add = new Button();
    this.groupBox_Merge_List_Merge_Usl2 = new GroupBox();
    this.listBox_Merge_List_Merge_Usl2 = new ListBox();
    this.groupBox_Merge_AttribVedRec1 = new GroupBox();
    this.listBox_Merge_AttribVedRec = new ListBox();
    this.groupBox_Merge_List_Ved_Id = new GroupBox();
    this.listBox_Merge_List_Ved_Id = new ListBox();
    this.tabPage_Razdels = new System.Windows.Forms.TabPage();
    this.groupBox_Conformity_Name_Page_for_Razdel = new GroupBox();
    this.button_Add_NamePage = new Button();
    this.groupBox_NamePage = new GroupBox();
    this.dataGridView_NamePage = new DataGridView();
    this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
    this.groupBox_RazdelVedAndNamePage = new GroupBox();
    this.dataGridView_RazdelVedAndNamePage = new DataGridView();
    this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
    this.buttonAdd_PodRazdel = new Button();
    this.buttonDelete_PodRazdel = new Button();
    this.checkBox_Razdel_PodRazdel = new CheckBox();
    this.Razdels_groupBoxListPodRazdelov = new GroupBox();
    this.Razdels_dataGridViewListPodRazdels = new DataGridView();
    this.PodRazdels_Column1 = new DataGridViewTextBoxColumn();
    this.PodRazdels_Column2 = new DataGridViewTextBoxColumn();
    this.Razdels_groupBoxListRazdelov = new GroupBox();
    this.Razdels_dataGridViewListRazdels = new DataGridView();
    this.Razdels_Column1 = new DataGridViewTextBoxColumn();
    this.Razdels_Column2 = new DataGridViewTextBoxColumn();
    this.buttonAdd_Razdel = new Button();
    this.buttonDelete_Razdel = new Button();
    this.tabPage_Zagolovki = new System.Windows.Forms.TabPage();
    this.checkBox_LocationZagolovki = new CheckBox();
    this.checkBox_UserZagolovki = new CheckBox();
    this.groupBox_Include_Name = new GroupBox();
    this.textBox_Include_Name = new TextBox();
    this.groupBox_Zagolovki_List_Ved_Id = new GroupBox();
    this.listBox_Zagolovki_List_Ved_Id = new ListBox();
    this.groupBox_Zagolovki_AttribVedRec1 = new GroupBox();
    this.listBox_Zagolovki_AttribVedRec = new ListBox();
    this.groupBox_Zagolovki_TypeCompare = new GroupBox();
    this.radioButton_Zagolovki_Compare_Symbol = new RadioButton();
    this.radioButton_Zagolovki_Compare_Int = new RadioButton();
    this.button_Zagolovki_FromList = new Button();
    this.label_NoZgolovki = new Label();
    this.button_Zagolovki_EditKeyAttribut = new Button();
    this.label_Zagolovki_Attribut = new Label();
    this.label_Zagolovki_SlevaVverhu = new Label();
    this.label_Zagolovki_SpravaVnizu = new Label();
    this.buttonDelete_Zagolovki = new Button();
    this.buttonAdd_Zagolovki = new Button();
    this.checkBox_Zagolovki_VyvoditPodrazdely = new CheckBox();
    this.groupBox_ListZagolovkov = new GroupBox();
    this.dataGridView_ListZagolovkov = new DataGridView();
    this.Zagolovok_Column1 = new DataGridViewTextBoxColumn();
    this.Zagolovok_Column2 = new DataGridViewTextBoxColumn();
    this.tabPage_Vyvod = new System.Windows.Forms.TabPage();
    this.tabControl_Vyvod = new System.Windows.Forms.TabControl();
    this.tabPage_Vyvod_1 = new System.Windows.Forms.TabPage();
    this.buttonEditTemplate = new Button();
    this.groupBox_Vyvod_List_Ved_Id = new GroupBox();
    this.listBox_Vyvod_List_Ved_Id = new ListBox();
    this.button_Vyvod_Obshaia = new Button();
    this.button_Vyvod_PoRazdelam = new Button();
    this.rightDock_Vyvod = new DockContainer();
    this.groupBox_Vyvod_AttribVedRec1 = new GroupBox();
    this.listBox_Vyvod_AttribVedRec = new ListBox();
    this.groupBox_Vyvod_Ved_Pasport = new GroupBox();
    this.listBoxAttrib_Vyvod_VedPasport = new ListBox();
    this.bottomDock = new DockContainer();
    this.topDock_Vyvod = new DockContainer();
    this.leftDock_Vyvod = new DockContainer();
    this.panel_Vyvod_1 = new Panel();
    this.groupBox_Vyvod_Forma = new GroupBox();
    this.radioButton_Vyvod_GroupB = new RadioButton();
    this.radioButton_Vyvod_EdOrA = new RadioButton();
    this.numeric_Vyvod_UpDownKolGraf = new NumericUpDown();
    this.label_Vyvod_Graf = new Label();
    this.button_Vyvod_AddAttribut = new Button();
    this.button_Vyvod_Delete = new Button();
    this.button_Vyvod_Edit = new Button();
    this.button_Vyvod_AddCell = new Button();
    this.groupBox_Vyvod_TextRazdelitel = new GroupBox();
    this.comboBox_Vyvod_TextRazdelitel = new ComboBox();
    this.treeView_Vyvod = new TreeView();
    this.docContainer_Vyvod = new DocumentContainer();
    this.docKcontainer_Vyvod = new DockContainer();
    this.dockMan_Vyvod = new DockManager();
    this.tabPage_Vyvod_2 = new System.Windows.Forms.TabPage();
    this.groupBox_isUnbrokenDefis = new GroupBox();
    this.checkBox_isUnbrokenDefis = new CheckBox();
    this.groupBox_Check = new GroupBox();
    this.button_Check = new Button();
    this.checkBox_isCheck = new CheckBox();
    this.groupBox_ProtectionCommand = new GroupBox();
    this.checkBox_isProtectionCommand = new CheckBox();
    this.groupBox_Protection_From_Editing = new GroupBox();
    this.checkBox_isProhibition_DocRowWithObj = new CheckBox();
    this.checkBox_isFullProhibition = new CheckBox();
    this.groupBox_Vyvod_isDeleteIdenticalTexts = new GroupBox();
    this.checkBox_Vyvod_isDeleteIdenticalTexts = new CheckBox();
    this.groupBox_Vyvod_Additional = new GroupBox();
    this.checkBox_Vyvod_Additional4 = new CheckBox();
    this.checkBox_Vyvod_Additional3 = new CheckBox();
    this.checkBox_Vyvod_Additional2 = new CheckBox();
    this.checkBox_Vyvod_Additional1 = new CheckBox();
    this.groupBox_Vyvod2_SkipRows = new GroupBox();
    this.label_Vyvod2_AfterRemark = new Label();
    this.numericUpDown_Vyvod2_AfterRemark = new NumericUpDown();
    this.label_Vyvod2_AfterInfo = new Label();
    this.numericUpDown_Vyvod2_AfterInfo = new NumericUpDown();
    this.group_Vyvod2_BoxLizm = new GroupBox();
    this.checkBox_Vyvod2_IncludedLizmInDoc = new CheckBox();
    this.label_Vyvod2_Lizm = new Label();
    this.numericUpDown_Vyvod2_Lizm = new NumericUpDown();
    this.checkBox_Vyvod2_Lizm = new CheckBox();
    this.tabPage_Xml = new System.Windows.Forms.TabPage();
    this.groupBox_Xml_Folder_In = new GroupBox();
    this.button_Xml_Folder_In = new Button();
    this.textBox_Xml_Folder_In = new TextBox();
    this.groupBox_Xml_EmptyString = new GroupBox();
    this.label_Xml_AfterRemark = new Label();
    this.numeric_UpDown_Xml_AfterRemark = new NumericUpDown();
    this.label_Xml_AfterInfo = new Label();
    this.numeric_UpDown_Xml_AfterInfo = new NumericUpDown();
    this.groupBox_Xml_Out = new GroupBox();
    this.radioButton_Xml_PassportOutNo = new RadioButton();
    this.radioButton_Xml_PassportOutDialog = new RadioButton();
    this.radioButton_Xml_PassporOutAlways = new RadioButton();
    this.groupBox_Xml_In = new GroupBox();
    this.radioButton_Xml_PassportInNo = new RadioButton();
    this.radioButton_Xml_PassportInDialog = new RadioButton();
    this.radioButton_Xml_PassporInAlways = new RadioButton();
    this.groupBox_Xml_Text = new GroupBox();
    this.textBox_Xml_Text = new TextBox();
    this.treeView_Xml = new TreeView();
    this.docKcontainer_Xml = new DockContainer();
    this.dockMan_Xml = new DockManager();
    this.docContainer_Xml = new DocumentContainer();
    this.button_Xml_Delete = new Button();
    this.button_Xml_Edit = new Button();
    this.button_Xml_Add = new Button();
    this.tabPage_Avs6 = new System.Windows.Forms.TabPage();
    this.button_Avs_Obshaia = new Button();
    this.button_Avs_PoRazdelam = new Button();
    this.panel_Avs_1 = new Panel();
    this.groupBox_Avs_Forma = new GroupBox();
    this.radioButton_Avs_GroupB = new RadioButton();
    this.radioButton_Avs_EdOrA = new RadioButton();
    this.numeric_Avs_UpDownKolGraf = new NumericUpDown();
    this.label_Avs_Graf = new Label();
    this.button_Avs_AddAttribut = new Button();
    this.button_Avs_Delete = new Button();
    this.button_Avs_Edit = new Button();
    this.button_Avs_AddCell = new Button();
    this.groupBox_Avs_TextRazdelitel = new GroupBox();
    this.comboBox_Avs_TextRazdelitel = new ComboBox();
    this.treeView_Avs = new TreeView();
    this.groupBox_Avs6_Fields = new GroupBox();
    this.listBox_Avs6_Fields = new ListBox();
    this.docContainer_Avs = new DocumentContainer();
    this.dockContainer_Avs = new DockContainer();
    this.dockMan_Avs = new DockManager();
    this.tabPage_Service = new System.Windows.Forms.TabPage();
    this.checkBox_Services_autoSbor = new CheckBox();
    this.label_DumpFolder = new Label();
    this.groupBox_AccessLevel = new GroupBox();
    this.radioButton_AccessLevel2 = new RadioButton();
    this.radioButton_AccessLevel1 = new RadioButton();
    this.radioButton_AccessLevel0 = new RadioButton();
    this.checkBox_Services_CreateDump = new CheckBox();
    this.label_ServicesFileOpen = new Label();
    this.label_ServiceCreateDump = new Label();
    this.label_SevicesForGroupB = new Label();
    this.label_ServicesTypeVedTo = new Label();
    this.label_ServicesCopyAll = new Label();
    this.label_ServicesDefaultAll = new Label();
    this.buttonSevicesForGroupB = new Button();
    this.buttonServicesFileOpen = new Button();
    this.buttonServiceCreateDump = new Button();
    this.labelService2 = new Label();
    this.labelService1 = new Label();
    this.buttonServicesTypeVedTo = new Button();
    this.buttonServicesCopyAll = new Button();
    this.buttonServicesDefaultAll = new Button();
    this.groupBox_Dump = new GroupBox();
    this.checkBox_Services_isCreateDumpAuto = new CheckBox();
    this.button_OpenDumpFolder = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.imagesToolbars = new ImageList(this.components);
    this.imageListSort = new ImageList(this.components);
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
    this.fontDialog1 = new FontDialog();
    this.fontDialog2 = new FontDialog();
    this.panelForButtons.SuspendLayout();
    this.tabControl_Nastr.SuspendLayout();
    this.tabPage_Bases.SuspendLayout();
    this.tabControl_Usl_Bases.SuspendLayout();
    this.tabPage_Bases_Main.SuspendLayout();
    this.groupBox_SpecificationSections.SuspendLayout();
    ((ISupportInitialize) this.drawGrid_SpecificationSections).BeginInit();
    this.groupBox_Usl_Bases_MainStep.SuspendLayout();
    this.tabPage_Usl_Bases_Sbor.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_For_ZIP.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_VedStep.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_VedGroup.SuspendLayout();
    this.tabPage_Usl_Bases_SborDialog.SuspendLayout();
    this.groupBox_Usl_Bases_ImbaseCatalog.SuspendLayout();
    this.groupBox_Usl_Bases_Sbor_Input.SuspendLayout();
    this.tabPage_Sbor.SuspendLayout();
    this.tabControl_Page_Sbor.SuspendLayout();
    this.tabPage_Sbor_Usl.SuspendLayout();
    this.Sbor_Usl_Panel.SuspendLayout();
    this.groupBox_Sbor_Usl_I_ILI.SuspendLayout();
    this.groupBox_Sbor_Usl_Text.SuspendLayout();
    this.groupBox_Sbor_Usl_Sravnenie.SuspendLayout();
    this.groupBox_Sbor_Usl_AttributeControl1.SuspendLayout();
    this.groupBox_Sbor_Usl_CollapsedTreeView.SuspendLayout();
    this.groupBox_UsloviaVvoda.SuspendLayout();
    this.tabPage_Sbor_Peredatha.SuspendLayout();
    this.groupBox_Sbor_Peredatha_AttributeControl1.SuspendLayout();
    this.groupBox_Sbor_Peredatha_ListId.SuspendLayout();
    this.tabPage_Sbor_Others.SuspendLayout();
    this.groupBox_Sbor_Others_DopZam.SuspendLayout();
    this.groupBox_Sbor_Others_Complecty.SuspendLayout();
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.SuspendLayout();
    this.tabPage_Sbor_Usl_Reference.SuspendLayout();
    this.Sbor_Usl_Reference_Panel.SuspendLayout();
    this.groupBox_Sbor_Usl_I_ILI_Reference.SuspendLayout();
    this.groupBox_Sbor_Usl_Text_Reference.SuspendLayout();
    this.groupBox_Sbor_Usl_Sravnenie_Reference.SuspendLayout();
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.SuspendLayout();
    this.groupBox_UsloviaVvoda_Reference.SuspendLayout();
    this.groupBox_Sbor_Usl_AttributeControl_Reference.SuspendLayout();
    this.tabPage_ESPD.SuspendLayout();
    this.groupBox_Remark.SuspendLayout();
    this.groupBox_AddToSP.SuspendLayout();
    this.groupBox_FirstOpen.SuspendLayout();
    this.tabPage_Sorting.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Sorting_Doc).BeginInit();
    this.groupBox_Sorting_List_Ved_Graf.SuspendLayout();
    this.groupBox_Sorting_List_Ved_Id.SuspendLayout();
    this.groupBox_Sorting_PoriadokSortirovki.SuspendLayout();
    this.groupBox_Sorting_PustyeStroki.SuspendLayout();
    this.groupBox_Sorting_Sravnenie.SuspendLayout();
    this.groupBox_Sorting_End.SuspendLayout();
    this.numericUpDown_Sorting_NumberEnd.BeginInit();
    this.groupBox_Sorting_Do.SuspendLayout();
    this.groupBox_Sorting_Begin.SuspendLayout();
    this.numericUpDown_Sorting_NumberBegin.BeginInit();
    this.groupBox_Sorting_Ot.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_Sorting).BeginInit();
    this.groupBox_Sorting_AttribVedRec1.SuspendLayout();
    this.tabPage_Merge.SuspendLayout();
    this.groupBox_Merge_List_Merge_Usl2.SuspendLayout();
    this.groupBox_Merge_AttribVedRec1.SuspendLayout();
    this.groupBox_Merge_List_Ved_Id.SuspendLayout();
    this.tabPage_Razdels.SuspendLayout();
    this.groupBox_Conformity_Name_Page_for_Razdel.SuspendLayout();
    this.groupBox_NamePage.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_NamePage).BeginInit();
    this.groupBox_RazdelVedAndNamePage.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_RazdelVedAndNamePage).BeginInit();
    this.Razdels_groupBoxListPodRazdelov.SuspendLayout();
    ((ISupportInitialize) this.Razdels_dataGridViewListPodRazdels).BeginInit();
    this.Razdels_groupBoxListRazdelov.SuspendLayout();
    ((ISupportInitialize) this.Razdels_dataGridViewListRazdels).BeginInit();
    this.tabPage_Zagolovki.SuspendLayout();
    this.groupBox_Include_Name.SuspendLayout();
    this.groupBox_Zagolovki_List_Ved_Id.SuspendLayout();
    this.groupBox_Zagolovki_AttribVedRec1.SuspendLayout();
    this.groupBox_Zagolovki_TypeCompare.SuspendLayout();
    this.groupBox_ListZagolovkov.SuspendLayout();
    ((ISupportInitialize) this.dataGridView_ListZagolovkov).BeginInit();
    this.tabPage_Vyvod.SuspendLayout();
    this.tabControl_Vyvod.SuspendLayout();
    this.tabPage_Vyvod_1.SuspendLayout();
    this.groupBox_Vyvod_List_Ved_Id.SuspendLayout();
    this.groupBox_Vyvod_AttribVedRec1.SuspendLayout();
    this.groupBox_Vyvod_Ved_Pasport.SuspendLayout();
    this.panel_Vyvod_1.SuspendLayout();
    this.groupBox_Vyvod_Forma.SuspendLayout();
    this.numeric_Vyvod_UpDownKolGraf.BeginInit();
    this.groupBox_Vyvod_TextRazdelitel.SuspendLayout();
    this.tabPage_Vyvod_2.SuspendLayout();
    this.groupBox_isUnbrokenDefis.SuspendLayout();
    this.groupBox_Check.SuspendLayout();
    this.groupBox_ProtectionCommand.SuspendLayout();
    this.groupBox_Protection_From_Editing.SuspendLayout();
    this.groupBox_Vyvod_isDeleteIdenticalTexts.SuspendLayout();
    this.groupBox_Vyvod_Additional.SuspendLayout();
    this.groupBox_Vyvod2_SkipRows.SuspendLayout();
    this.numericUpDown_Vyvod2_AfterRemark.BeginInit();
    this.numericUpDown_Vyvod2_AfterInfo.BeginInit();
    this.group_Vyvod2_BoxLizm.SuspendLayout();
    this.numericUpDown_Vyvod2_Lizm.BeginInit();
    this.tabPage_Xml.SuspendLayout();
    this.groupBox_Xml_Folder_In.SuspendLayout();
    this.groupBox_Xml_EmptyString.SuspendLayout();
    this.numeric_UpDown_Xml_AfterRemark.BeginInit();
    this.numeric_UpDown_Xml_AfterInfo.BeginInit();
    this.groupBox_Xml_Out.SuspendLayout();
    this.groupBox_Xml_In.SuspendLayout();
    this.groupBox_Xml_Text.SuspendLayout();
    this.tabPage_Avs6.SuspendLayout();
    this.panel_Avs_1.SuspendLayout();
    this.groupBox_Avs_Forma.SuspendLayout();
    this.numeric_Avs_UpDownKolGraf.BeginInit();
    this.groupBox_Avs_TextRazdelitel.SuspendLayout();
    this.groupBox_Avs6_Fields.SuspendLayout();
    this.tabPage_Service.SuspendLayout();
    this.groupBox_AccessLevel.SuspendLayout();
    this.groupBox_Dump.SuspendLayout();
    this.SuspendLayout();
    this.panelForButtons.AutoSize = true;
    this.panelForButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.buttonWarnings);
    this.panelForButtons.Controls.Add((Control) this.buttonCopyFrom);
    this.panelForButtons.Controls.Add((Control) this.buttonSelectVed);
    this.panelForButtons.Controls.Add((Control) this.buttonSave1);
    this.panelForButtons.Controls.Add((Control) this.buttonDefault);
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 757);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(1584, 39);
    this.panelForButtons.TabIndex = 11;
    this.panelForButtons.MouseClick += new MouseEventHandler(this.panelForButtons_MouseClick);
    this.buttonWarnings.ForeColor = Color.Red;
    this.buttonWarnings.Location = new Point(700, 5);
    this.buttonWarnings.Name = "buttonWarnings";
    this.buttonWarnings.Size = new Size(121, 27);
    this.buttonWarnings.TabIndex = 9;
    this.buttonWarnings.Text = "Предупреждения";
    this.toolTip1.SetToolTip((Control) this.buttonWarnings, "Смотреть список предупреждений");
    this.buttonWarnings.UseVisualStyleBackColor = true;
    this.buttonWarnings.Visible = false;
    this.buttonWarnings.Click += new EventHandler(this.buttonWarnings_Click);
    this.buttonCopyFrom.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCopyFrom.Location = new Point(850, 5);
    this.buttonCopyFrom.Name = "buttonCopyFrom";
    this.buttonCopyFrom.Size = new Size(121, 27);
    this.buttonCopyFrom.TabIndex = 7;
    this.buttonCopyFrom.Text = "Копировать из ...";
    this.toolTip1.SetToolTip((Control) this.buttonCopyFrom, "Всем значениям ТЕКУЩЕЙ страницы копировать значения из другой ведомости");
    this.buttonCopyFrom.UseVisualStyleBackColor = true;
    this.buttonCopyFrom.Click += new EventHandler(this.buttonCopyFrom_Click);
    this.buttonSelectVed.Location = new Point(12, 5);
    this.buttonSelectVed.Name = "buttonSelectVed";
    this.buttonSelectVed.Size = new Size(168, 27);
    this.buttonSelectVed.TabIndex = 6;
    this.buttonSelectVed.Text = "Выбрать ведомость";
    this.toolTip1.SetToolTip((Control) this.buttonSelectVed, "Выбрать для настройки другую ведомость");
    this.buttonSelectVed.UseVisualStyleBackColor = true;
    this.buttonSelectVed.Click += new EventHandler(this.buttonSelectVed_Click);
    this.buttonSave1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSave1.Enabled = false;
    this.buttonSave1.Location = new Point(1150, 5);
    this.buttonSave1.Name = "buttonSave1";
    this.buttonSave1.Size = new Size(121, 27);
    this.buttonSave1.TabIndex = 4;
    this.buttonSave1.Text = "Сохранить";
    this.toolTip1.SetToolTip((Control) this.buttonSave1, "Сохранить все произведенные изменения. Окно диалога не закрывать");
    this.buttonSave1.UseVisualStyleBackColor = true;
    this.buttonSave1.Click += new EventHandler(this.buttonSave1_Click);
    this.buttonDefault.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(1000, 5);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(121, 27);
    this.buttonDefault.TabIndex = 3;
    this.buttonDefault.Text = "По умолчанию";
    this.toolTip1.SetToolTip((Control) this.buttonDefault, "Всем значениям ТЕКУЩЕЙ страницы присвоить значения по умолчанию");
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(1450, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Закрыть";
    this.toolTip1.SetToolTip((Control) this.bCancel, "Закрыть окно диалога");
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(1300, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.toolTip1.SetToolTip((Control) this.bOK, "Сохранить все произведенные изменения и закрыть окно диалога");
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Bases);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Sbor);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Sorting);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Merge);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Razdels);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Zagolovki);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Vyvod);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Xml);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Avs6);
    this.tabControl_Nastr.Controls.Add((Control) this.tabPage_Service);
    this.tabControl_Nastr.Dock = DockStyle.Fill;
    this.tabControl_Nastr.Location = new Point(0, 0);
    this.tabControl_Nastr.Name = "tabControl_Nastr";
    this.tabControl_Nastr.SelectedIndex = 0;
    this.tabControl_Nastr.Size = new Size(1584, 757);
    this.tabControl_Nastr.TabIndex = 12;
    this.tabControl_Nastr.SelectedIndexChanged += new EventHandler(this.tabControl_Nastr_SelectedIndexChanged);
    this.tabControl_Nastr.Deselecting += new TabControlCancelEventHandler(this.tabControl_Nastr_Deselecting);
    this.tabPage_Bases.Controls.Add((Control) this.tabControl_Usl_Bases);
    this.tabPage_Bases.Location = new Point(4, 22);
    this.tabPage_Bases.Name = "tabPage_Bases";
    this.tabPage_Bases.Padding = new Padding(3);
    this.tabPage_Bases.Size = new Size(1576, 731);
    this.tabPage_Bases.TabIndex = 0;
    this.tabPage_Bases.Text = "Основные параметры";
    this.tabPage_Bases.UseVisualStyleBackColor = true;
    this.tabControl_Usl_Bases.Controls.Add((Control) this.tabPage_Bases_Main);
    this.tabControl_Usl_Bases.Controls.Add((Control) this.tabPage_Usl_Bases_Sbor);
    this.tabControl_Usl_Bases.Controls.Add((Control) this.tabPage_Usl_Bases_SborDialog);
    this.tabControl_Usl_Bases.Dock = DockStyle.Fill;
    this.tabControl_Usl_Bases.Location = new Point(3, 3);
    this.tabControl_Usl_Bases.Name = "tabControl_Usl_Bases";
    this.tabControl_Usl_Bases.SelectedIndex = 0;
    this.tabControl_Usl_Bases.Size = new Size(1570, 725);
    this.tabControl_Usl_Bases.TabIndex = 12;
    this.tabPage_Bases_Main.BackColor = Color.LightYellow;
    this.tabPage_Bases_Main.Controls.Add((Control) this.groupBox_SpecificationSections);
    this.tabPage_Bases_Main.Controls.Add((Control) this.checkBox_Usl_Bases_isOnlyUroven1);
    this.tabPage_Bases_Main.Controls.Add((Control) this.label_Usl_Bases_MainCaption);
    this.tabPage_Bases_Main.Controls.Add((Control) this.groupBox_Usl_Bases_MainStep);
    this.tabPage_Bases_Main.Location = new Point(4, 22);
    this.tabPage_Bases_Main.Name = "tabPage_Bases_Main";
    this.tabPage_Bases_Main.Padding = new Padding(3);
    this.tabPage_Bases_Main.Size = new Size(1562, 699);
    this.tabPage_Bases_Main.TabIndex = 0;
    this.tabPage_Bases_Main.Text = "Предварительный сбор";
    this.groupBox_SpecificationSections.Controls.Add((Control) this.checkBox_Specification_Instrument);
    this.groupBox_SpecificationSections.Controls.Add((Control) this.drawGrid_SpecificationSections);
    this.groupBox_SpecificationSections.Location = new Point(10, 247);
    this.groupBox_SpecificationSections.Name = "groupBox_SpecificationSections";
    this.groupBox_SpecificationSections.Size = new Size(321, 303);
    this.groupBox_SpecificationSections.TabIndex = 7;
    this.groupBox_SpecificationSections.TabStop = false;
    this.groupBox_SpecificationSections.Text = "Разделы спецификаций";
    this.toolTip1.SetToolTip((Control) this.groupBox_SpecificationSections, "Из каких разделов спецификации раскрывать");
    this.checkBox_Specification_Instrument.AutoSize = true;
    this.checkBox_Specification_Instrument.Location = new Point(6, 275);
    this.checkBox_Specification_Instrument.Name = "checkBox_Specification_Instrument";
    this.checkBox_Specification_Instrument.Size = new Size(274, 17);
    this.checkBox_Specification_Instrument.TabIndex = 17;
    this.checkBox_Specification_Instrument.Text = "Разделы спецификаций комплекта инструмента";
    this.toolTip1.SetToolTip((Control) this.checkBox_Specification_Instrument, "Учитывать разделы спецификаций комплекта инструмента и принадлежностей");
    this.checkBox_Specification_Instrument.UseVisualStyleBackColor = true;
    this.checkBox_Specification_Instrument.CheckedChanged += new EventHandler(this.checkBox_Specification_Insrument_CheckedChanged);
    this.drawGrid_SpecificationSections.AllowDrop = true;
    this.drawGrid_SpecificationSections.AllowUserToAddRows = false;
    this.drawGrid_SpecificationSections.AllowUserToDeleteRows = false;
    this.drawGrid_SpecificationSections.AllowUserToResizeColumns = false;
    this.drawGrid_SpecificationSections.AllowUserToResizeRows = false;
    this.drawGrid_SpecificationSections.BackgroundColor = Color.White;
    gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle1.BackColor = SystemColors.Control;
    gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle1.ForeColor = SystemColors.WindowText;
    gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
    this.drawGrid_SpecificationSections.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.drawGrid_SpecificationSections.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.drawGrid_SpecificationSections.ColumnHeadersVisible = false;
    this.drawGrid_SpecificationSections.Columns.AddRange((DataGridViewColumn) this.Column5, (DataGridViewColumn) this.dataGridViewTextBoxColumn15);
    gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle2.BackColor = SystemColors.Window;
    gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle2.ForeColor = SystemColors.ControlText;
    gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
    this.drawGrid_SpecificationSections.DefaultCellStyle = gridViewCellStyle2;
    this.drawGrid_SpecificationSections.Location = new Point(6, 19);
    this.drawGrid_SpecificationSections.MultiSelect = false;
    this.drawGrid_SpecificationSections.Name = "drawGrid_SpecificationSections";
    this.drawGrid_SpecificationSections.ReadOnly = true;
    gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle3.BackColor = SystemColors.Control;
    gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle3.ForeColor = SystemColors.WindowText;
    gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
    this.drawGrid_SpecificationSections.RowHeadersDefaultCellStyle = gridViewCellStyle3;
    this.drawGrid_SpecificationSections.RowHeadersVisible = false;
    this.drawGrid_SpecificationSections.RowTemplate.Height = 20;
    this.drawGrid_SpecificationSections.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.drawGrid_SpecificationSections.Size = new Size(300, 248);
    this.drawGrid_SpecificationSections.TabIndex = 16 /*0x10*/;
    this.toolTip1.SetToolTip((Control) this.drawGrid_SpecificationSections, "Из каких разделов спецификации раскрывать");
    this.drawGrid_SpecificationSections.CellMouseClick += new DataGridViewCellMouseEventHandler(this.drawGrid_SpecificationSections_CellMouseClick);
    this.Column5.FillWeight = 20f;
    this.Column5.HeaderText = "Column_Check";
    this.Column5.Name = "Column5";
    this.Column5.ReadOnly = true;
    this.Column5.Resizable = DataGridViewTriState.False;
    this.Column5.Width = 20;
    this.dataGridViewTextBoxColumn15.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    this.dataGridViewTextBoxColumn15.HeaderText = "Column_Name";
    this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
    this.dataGridViewTextBoxColumn15.ReadOnly = true;
    this.checkBox_Usl_Bases_isOnlyUroven1.AutoSize = true;
    this.checkBox_Usl_Bases_isOnlyUroven1.Location = new Point(28, 213);
    this.checkBox_Usl_Bases_isOnlyUroven1.Name = "checkBox_Usl_Bases_isOnlyUroven1";
    this.checkBox_Usl_Bases_isOnlyUroven1.Size = new Size(257, 17);
    this.checkBox_Usl_Bases_isOnlyUroven1.TabIndex = 5;
    this.checkBox_Usl_Bases_isOnlyUroven1.Text = "Раскрывать только основную спецификацию";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isOnlyUroven1, "Раскрывать только основную спецификацию или всесь состав изделия");
    this.checkBox_Usl_Bases_isOnlyUroven1.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isOnlyUroven1.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isOnlyUroven1_CheckedChanged);
    this.label_Usl_Bases_MainCaption.AutoSize = true;
    this.label_Usl_Bases_MainCaption.Location = new Point(25, 14);
    this.label_Usl_Bases_MainCaption.Name = "label_Usl_Bases_MainCaption";
    this.label_Usl_Bases_MainCaption.Size = new Size(263, 13);
    this.label_Usl_Bases_MainCaption.TabIndex = 1;
    this.label_Usl_Bases_MainCaption.Text = "Вначале собирается \"дерево\" всех спецификаций";
    this.groupBox_Usl_Bases_MainStep.Controls.Add((Control) this.checkBox_Usl_Bases_isMainSumm);
    this.groupBox_Usl_Bases_MainStep.Controls.Add((Control) this.checkBox_Usl_Bases_isMainCreateVtorRecords);
    this.groupBox_Usl_Bases_MainStep.Controls.Add((Control) this.checkBox_Usl_Bases_isMainSort2);
    this.groupBox_Usl_Bases_MainStep.Controls.Add((Control) this.checkBox_Usl_Bases_isMainSummOdinakovyh);
    this.groupBox_Usl_Bases_MainStep.Controls.Add((Control) this.checkBox_Usl_Bases_isMainSort1);
    this.groupBox_Usl_Bases_MainStep.Location = new Point(10, 40);
    this.groupBox_Usl_Bases_MainStep.Name = "groupBox_Usl_Bases_MainStep";
    this.groupBox_Usl_Bases_MainStep.Size = new Size(500, 150);
    this.groupBox_Usl_Bases_MainStep.TabIndex = 0;
    this.groupBox_Usl_Bases_MainStep.TabStop = false;
    this.groupBox_Usl_Bases_MainStep.Text = "Этапы обработки \"дерева\" спецификаций по всему изделию";
    this.checkBox_Usl_Bases_isMainSumm.AutoSize = true;
    this.checkBox_Usl_Bases_isMainSumm.Checked = true;
    this.checkBox_Usl_Bases_isMainSumm.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_isMainSumm.Location = new Point(18, 114);
    this.checkBox_Usl_Bases_isMainSumm.Name = "checkBox_Usl_Bases_isMainSumm";
    this.checkBox_Usl_Bases_isMainSumm.Size = new Size(214, 17);
    this.checkBox_Usl_Bases_isMainSumm.TabIndex = 4;
    this.checkBox_Usl_Bases_isMainSumm.Text = "Суммирование \"Вторичных записей\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isMainSumm, componentResourceManager.GetString("checkBox_Usl_Bases_isMainSumm.ToolTip"));
    this.checkBox_Usl_Bases_isMainSumm.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isMainSumm.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isMainSumm_CheckedChanged);
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.AutoSize = true;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Checked = true;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Location = new Point(18, 91);
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Name = "checkBox_Usl_Bases_isMainCreateVtorRecords";
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Size = new Size(187, 17);
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.TabIndex = 3;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.Text = "Создание \"Вторичных записей\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isMainCreateVtorRecords, componentResourceManager.GetString("checkBox_Usl_Bases_isMainCreateVtorRecords.ToolTip"));
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isMainCreateVtorRecords.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isMainCreateVtorRecords_CheckedChanged);
    this.checkBox_Usl_Bases_isMainSort2.AutoSize = true;
    this.checkBox_Usl_Bases_isMainSort2.Checked = true;
    this.checkBox_Usl_Bases_isMainSort2.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_isMainSort2.Location = new Point(18, 68);
    this.checkBox_Usl_Bases_isMainSort2.Name = "checkBox_Usl_Bases_isMainSort2";
    this.checkBox_Usl_Bases_isMainSort2.Size = new Size(196, 17);
    this.checkBox_Usl_Bases_isMainSort2.TabIndex = 2;
    this.checkBox_Usl_Bases_isMainSort2.Text = "Сортировка с учетом исполнений";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isMainSort2, componentResourceManager.GetString("checkBox_Usl_Bases_isMainSort2.ToolTip"));
    this.checkBox_Usl_Bases_isMainSort2.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isMainSort2.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isMainSort2_CheckedChanged);
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.AutoSize = true;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Checked = true;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Location = new Point(18, 45);
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Name = "checkBox_Usl_Bases_isMainSummOdinakovyh";
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Size = new Size(287, 17);
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.TabIndex = 1;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.Text = "Суммирование (объединение) одинаковых записей";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isMainSummOdinakovyh, componentResourceManager.GetString("checkBox_Usl_Bases_isMainSummOdinakovyh.ToolTip"));
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isMainSummOdinakovyh.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isMainSummOdinakovyh_CheckedChanged);
    this.checkBox_Usl_Bases_isMainSort1.AutoSize = true;
    this.checkBox_Usl_Bases_isMainSort1.Checked = true;
    this.checkBox_Usl_Bases_isMainSort1.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_isMainSort1.Location = new Point(18, 22);
    this.checkBox_Usl_Bases_isMainSort1.Name = "checkBox_Usl_Bases_isMainSort1";
    this.checkBox_Usl_Bases_isMainSort1.Size = new Size(179, 17);
    this.checkBox_Usl_Bases_isMainSort1.TabIndex = 0;
    this.checkBox_Usl_Bases_isMainSort1.Text = "Предварительная сортировка";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_isMainSort1, componentResourceManager.GetString("checkBox_Usl_Bases_isMainSort1.ToolTip"));
    this.checkBox_Usl_Bases_isMainSort1.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_isMainSort1.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_isMainSort1_CheckedChanged);
    this.tabPage_Usl_Bases_Sbor.BackColor = Color.LightYellow;
    this.tabPage_Usl_Bases_Sbor.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP);
    this.tabPage_Usl_Bases_Sbor.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel);
    this.tabPage_Usl_Bases_Sbor.Controls.Add((Control) this.label_Usl_Bases_Sbor1);
    this.tabPage_Usl_Bases_Sbor.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedAddToSp);
    this.tabPage_Usl_Bases_Sbor.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_VedStep);
    this.tabPage_Usl_Bases_Sbor.Location = new Point(4, 22);
    this.tabPage_Usl_Bases_Sbor.Name = "tabPage_Usl_Bases_Sbor";
    this.tabPage_Usl_Bases_Sbor.Padding = new Padding(3);
    this.tabPage_Usl_Bases_Sbor.Size = new Size(1562, 699);
    this.tabPage_Usl_Bases_Sbor.TabIndex = 1;
    this.tabPage_Usl_Bases_Sbor.Text = "Сбор ведомости";
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL);
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP_SB);
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Location = new Point(596, 41);
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Name = "groupBox_Usl_Bases_Sbor_For_ZIP";
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Size = new Size(401, 193);
    this.groupBox_Usl_Bases_Sbor_For_ZIP.TabIndex = 16 /*0x10*/;
    this.groupBox_Usl_Bases_Sbor_For_ZIP.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Text = "При сборе ведомости ЗИП (при анализе спецификации Комплекта)";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP, "Дейсвия если в спецификации Комплекта найдена запись о спецификации Сборочной единицы или Комплекта");
    this.groupBox_Usl_Bases_Sbor_For_ZIP.Visible = false;
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Location = new Point(7, 104);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Name = "groupBox_Usl_Bases_Sbor_For_ZIP_COMPL";
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Size = new Size(388, 74);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.TabIndex = 18;
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.Text = "Комплект (в составе другого Комплекта)";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL, "Действия со спецификацией Комплекта");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Checked = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Location = new Point(6, 47);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Name = "checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add";
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Size = new Size(142, 17);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.TabIndex = 13;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.Text = "Включать в ведомость";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add, "В ведомость заносить запись о данном Комплекте");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Add_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Location = new Point(6, 21);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Name = "checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr";
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Size = new Size(88, 17);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.TabIndex = 12;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.Text = "Раскрывать";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr, "Анализировать и раскрывать спецификацию Комплекта");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_For_ZIP_COMPL_Raskr_CheckedChanged);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Location = new Point(7, 19);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Name = "groupBox_Usl_Bases_Sbor_For_ZIP_SB";
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Size = new Size(388, 74);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.TabIndex = 17;
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.Text = "Сборочная единица (в составе Комплекта)";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_For_ZIP_SB, "Действия со спецификацией Сборочной единицы");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Location = new Point(6, 47);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Name = "checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add";
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Size = new Size(142, 17);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.TabIndex = 13;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.Text = "Включать в ведомость";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add, "В ведомость заносить запись о данной Сборочной единице");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Add_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Checked = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Location = new Point(6, 21);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Name = "checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr";
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Size = new Size(88, 17);
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.TabIndex = 12;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.Text = "Раскрывать";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr, "Анализировать и раскрывать спецификацию Сборочной единица");
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_For_ZIP_SB_Raskr_CheckedChanged);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Controls.Add((Control) this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Controls.Add((Control) this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Location = new Point(23, 448);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Name = "groupBox_Usl_Bases_Sbor_isVedAddToRazdel";
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Size = new Size(300, 60);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.TabIndex = 15;
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.Text = "Занести в раздел:";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel, "В какой раздел спецификации занести данную ведомость");
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.AutoSize = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Location = new Point(6, 34);
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Name = "radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl";
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Size = new Size(83, 17);
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.TabIndex = 1;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.Text = "Комплекты";
    this.toolTip1.SetToolTip((Control) this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl, "Занести в раздел Комплекты");
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Compl.UseVisualStyleBackColor = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.AutoSize = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Checked = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Location = new Point(6, 15);
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Name = "radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc";
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Size = new Size(100, 17);
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.TabIndex = 0;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.TabStop = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.Text = "Документация";
    this.toolTip1.SetToolTip((Control) this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc, "Занести в раздел Документация");
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.UseVisualStyleBackColor = true;
    this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc.CheckedChanged += new EventHandler(this.radioButton_Usl_Bases_Sbor_isVedAddToRazdel_Doc_CheckedChanged);
    this.label_Usl_Bases_Sbor1.AutoSize = true;
    this.label_Usl_Bases_Sbor1.Location = new Point(25, 14);
    this.label_Usl_Bases_Sbor1.Name = "label_Usl_Bases_Sbor1";
    this.label_Usl_Bases_Sbor1.Size = new Size(619, 13);
    this.label_Usl_Bases_Sbor1.TabIndex = 3;
    this.label_Usl_Bases_Sbor1.Text = "На основании \"дерева\" спецификаций производится сбор всех данных, их обработка, создание документа (ведомости)";
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Location = new Point(28, 425);
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Name = "checkBox_Usl_Bases_Sbor_isVedAddToSp";
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Size = new Size(209, 17);
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.TabIndex = 2;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.Text = "Документ занести в спецификацию";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedAddToSp, "В головной спецификации автоматически создавать запись о данной ведомости");
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddToSp.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedAddToSp_CheckedChanged);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_VedGroup);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedUnion);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedSort1);
    this.groupBox_Usl_Bases_Sbor_VedStep.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup);
    this.groupBox_Usl_Bases_Sbor_VedStep.Location = new Point(10, 41);
    this.groupBox_Usl_Bases_Sbor_VedStep.Name = "groupBox_Usl_Bases_Sbor_VedStep";
    this.groupBox_Usl_Bases_Sbor_VedStep.Size = new Size(500, 377);
    this.groupBox_Usl_Bases_Sbor_VedStep.TabIndex = 1;
    this.groupBox_Usl_Bases_Sbor_VedStep.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_VedStep.Text = "Этапы обработки ведомости";
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedSummVtor);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedSortVtor);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedMergerVtor);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Location = new Point(6, 189);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Name = "groupBox_Usl_Bases_Sbor_isVedExtrectionVtor";
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Size = new Size(473, 103);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.TabIndex = 17;
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.Text = "Обработка \"Вторичных записей\"";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor, "Действия, производимые со вторичными записями");
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Location = new Point(11, 71);
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Name = "checkBox_Usl_Bases_Sbor_isVedSummVtor";
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Size = new Size(208 /*0xD0*/, 17);
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.TabIndex = 11;
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.Text = "Суммировать \"Вторичные записей\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedSummVtor, componentResourceManager.GetString("checkBox_Usl_Bases_Sbor_isVedSummVtor.ToolTip"));
    this.checkBox_Usl_Bases_Sbor_isVedSummVtor.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Location = new Point(11, 48 /*0x30*/);
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Name = "checkBox_Usl_Bases_Sbor_isVedSortVtor";
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Size = new Size(263, 17);
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.TabIndex = 10;
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.Text = "Сортировать \"Вторичные записи\" (Куда водит)";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedSortVtor, "В каждой \"основной\" записи \"вторичные\" записи сортируются в алфавитном порядке");
    this.checkBox_Usl_Bases_Sbor_isVedSortVtor.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Location = new Point(11, 25);
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Name = "checkBox_Usl_Bases_Sbor_isVedMergerVtor";
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Size = new Size(328, 17);
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.TabIndex = 9;
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.Text = "Объединять записи с одинаковой первичной информацией";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedMergerVtor, componentResourceManager.GetString("checkBox_Usl_Bases_Sbor_isVedMergerVtor.ToolTip"));
    this.checkBox_Usl_Bases_Sbor_isVedMergerVtor.UseVisualStyleBackColor = true;
    this.groupBox_Usl_Bases_Sbor_VedGroup.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedMergerIsp);
    this.groupBox_Usl_Bases_Sbor_VedGroup.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isVedSortGroup);
    this.groupBox_Usl_Bases_Sbor_VedGroup.Location = new Point(13, 21);
    this.groupBox_Usl_Bases_Sbor_VedGroup.Name = "groupBox_Usl_Bases_Sbor_VedGroup";
    this.groupBox_Usl_Bases_Sbor_VedGroup.Size = new Size(286, 64 /*0x40*/);
    this.groupBox_Usl_Bases_Sbor_VedGroup.TabIndex = 12;
    this.groupBox_Usl_Bases_Sbor_VedGroup.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_VedGroup.Text = "Групповые ведомости";
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Location = new Point(6, 42);
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Name = "checkBox_Usl_Bases_Sbor_isVedMergerIsp";
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Size = new Size(150, 17);
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.TabIndex = 3;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.Text = "Выделение общей части";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedMergerIsp, componentResourceManager.GetString("checkBox_Usl_Bases_Sbor_isVedMergerIsp.ToolTip"));
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedMergerIsp.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedMergerIsp_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Location = new Point(6, 19);
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Name = "checkBox_Usl_Bases_Sbor_isVedSortGroup";
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Size = new Size(179, 17);
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.TabIndex = 2;
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.Text = "Предварительная сортировка";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedSortGroup, "Сортировка полученного списка по:\r\n- Обозначению;\r\n- Наименованию\r\n- \"Куда входит\";\r\n- Полученное через спецификацию комплекта в конец списка;\r\n- Исполнение;");
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedSortGroup.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedSortGroup_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Location = new Point(18, 347);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Name = "checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku";
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Size = new Size(208 /*0xD0*/, 17);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.TabIndex = 11;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.Text = "Создание заголовков по настройке";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku, "Создание заголовков по настройке.\r\nСмотри на закладке \"Заголовки\"\r\n");
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedCreateZagolPoPriznaku_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Location = new Point(18, 324);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Name = "checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed";
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Size = new Size(300, 17);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.TabIndex = 10;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.Text = "Создание заголовков \"Ведомости составных частей\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed, "Создание заголовка \"Ведомости составных частей\"");
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedCreateZagolSvoiaVed_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Location = new Point(18, 301);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Name = "checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln";
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Size = new Size(312, 17);
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.TabIndex = 9;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.Text = "Создание заголовков исполнений (групповые формы А)";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln, "В групповой ведомости (формы А)\r\n- создаются заголовки исполнений;\r\n- создается заголовок \"Переменные данные ...\"");
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedCreateZagolIspoln_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Location = new Point(18, 161);
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Name = "checkBox_Usl_Bases_Sbor_isVedExtrectionVtor";
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Size = new Size(187, 17);
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.TabIndex = 5;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.Text = "Создавать \"Вторичные записи\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor, "Объединение одинаковых записей. \r\nСоздаются \"вторичные\" записи с информацией: \"Куда входит\" и \"Кол\"\r\n\"Вторичные записи\" присоединяются к этой \"основной\" записи");
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedExtrectionVtor_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedUnion.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Location = new Point(18, 138);
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Name = "checkBox_Usl_Bases_Sbor_isVedUnion";
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Size = new Size(287, 17);
    this.checkBox_Usl_Bases_Sbor_isVedUnion.TabIndex = 4;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.Text = "Суммирование (объединение) одинаковых записей";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedUnion, componentResourceManager.GetString("checkBox_Usl_Bases_Sbor_isVedUnion.ToolTip"));
    this.checkBox_Usl_Bases_Sbor_isVedUnion.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedUnion.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedUnion_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedSort1.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Location = new Point(18, 115);
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Name = "checkBox_Usl_Bases_Sbor_isVedSort1";
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Size = new Size(157, 17);
    this.checkBox_Usl_Bases_Sbor_isVedSort1.TabIndex = 3;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.Text = "Сортировка по настройке";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedSort1, "Сортировка по настройке.\r\nСмотри на закладке \"Правила сортировки\"");
    this.checkBox_Usl_Bases_Sbor_isVedSort1.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedSort1.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedSort1_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Location = new Point(18, 92);
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Name = "checkBox_Usl_Bases_Sbor_isVedAddFuncGroup";
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Size = new Size(254, 17);
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.TabIndex = 2;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.Text = "Дополнение поля \"Функциональная группа\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup, "Для записей, у которых нет \"функциональной группы\",\r\nэто поле заполняется информацией, соответствующей разделу ведомости");
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isVedAddFuncGroup_CheckedChanged);
    this.tabPage_Usl_Bases_SborDialog.BackColor = Color.LightYellow;
    this.tabPage_Usl_Bases_SborDialog.Controls.Add((Control) this.groupBox_Usl_Bases_ImbaseCatalog);
    this.tabPage_Usl_Bases_SborDialog.Controls.Add((Control) this.groupBox_Usl_Bases_Sbor_Input);
    this.tabPage_Usl_Bases_SborDialog.Location = new Point(4, 22);
    this.tabPage_Usl_Bases_SborDialog.Name = "tabPage_Usl_Bases_SborDialog";
    this.tabPage_Usl_Bases_SborDialog.Padding = new Padding(3);
    this.tabPage_Usl_Bases_SborDialog.Size = new Size(1562, 699);
    this.tabPage_Usl_Bases_SborDialog.TabIndex = 2;
    this.tabPage_Usl_Bases_SborDialog.Text = "Ввод данных в диалоге";
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.label_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.label_CatalogsImbase);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.button_Delete_From_To_listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.button_Add_To_listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.listBox_CatalogsImbase);
    this.groupBox_Usl_Bases_ImbaseCatalog.Controls.Add((Control) this.listBox_QuickObjectInfo);
    this.groupBox_Usl_Bases_ImbaseCatalog.Location = new Point(6, 125);
    this.groupBox_Usl_Bases_ImbaseCatalog.Name = "groupBox_Usl_Bases_ImbaseCatalog";
    this.groupBox_Usl_Bases_ImbaseCatalog.Size = new Size(434, 193);
    this.groupBox_Usl_Bases_ImbaseCatalog.TabIndex = 1;
    this.groupBox_Usl_Bases_ImbaseCatalog.TabStop = false;
    this.groupBox_Usl_Bases_ImbaseCatalog.Text = "Ввод в диалоге из Imbase";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_ImbaseCatalog, "Выбор Каталогов Imbase, доступных для выбора данных");
    this.label_QuickObjectInfo.AutoSize = true;
    this.label_QuickObjectInfo.Location = new Point(263, 20);
    this.label_QuickObjectInfo.Name = "label_QuickObjectInfo";
    this.label_QuickObjectInfo.Size = new Size(115, 13);
    this.label_QuickObjectInfo.TabIndex = 8;
    this.label_QuickObjectInfo.Text = "Выбранные каталоги";
    this.label_CatalogsImbase.AutoSize = true;
    this.label_CatalogsImbase.Location = new Point(15, 20);
    this.label_CatalogsImbase.Name = "label_CatalogsImbase";
    this.label_CatalogsImbase.Size = new Size(112 /*0x70*/, 13);
    this.label_CatalogsImbase.TabIndex = 7;
    this.label_CatalogsImbase.Text = "Все каталоги Imbase";
    this.button_Delete_From_To_listBox_QuickObjectInfo.Image = (Image) Resources.arrow_left_green;
    this.button_Delete_From_To_listBox_QuickObjectInfo.Location = new Point(196, 89);
    this.button_Delete_From_To_listBox_QuickObjectInfo.Name = "button_Delete_From_To_listBox_QuickObjectInfo";
    this.button_Delete_From_To_listBox_QuickObjectInfo.Size = new Size(39, 23);
    this.button_Delete_From_To_listBox_QuickObjectInfo.TabIndex = 6;
    this.toolTip1.SetToolTip((Control) this.button_Delete_From_To_listBox_QuickObjectInfo, "Удалить из списка выбранных каталогов");
    this.button_Delete_From_To_listBox_QuickObjectInfo.UseVisualStyleBackColor = true;
    this.button_Delete_From_To_listBox_QuickObjectInfo.Click += new EventHandler(this.button_Delete_From_To_listBox_QuickObjectInfo_Click);
    this.button_Add_To_listBox_QuickObjectInfo.Image = (Image) Resources.arrow_right_green;
    this.button_Add_To_listBox_QuickObjectInfo.Location = new Point(196, 56);
    this.button_Add_To_listBox_QuickObjectInfo.Name = "button_Add_To_listBox_QuickObjectInfo";
    this.button_Add_To_listBox_QuickObjectInfo.Size = new Size(39, 23);
    this.button_Add_To_listBox_QuickObjectInfo.TabIndex = 5;
    this.toolTip1.SetToolTip((Control) this.button_Add_To_listBox_QuickObjectInfo, "Внести в список выбранных каталогов");
    this.button_Add_To_listBox_QuickObjectInfo.UseVisualStyleBackColor = true;
    this.button_Add_To_listBox_QuickObjectInfo.Click += new EventHandler(this.button_Add_To_listBox_QuickObjectInfo_Click);
    this.listBox_CatalogsImbase.BackColor = Color.FloralWhite;
    this.listBox_CatalogsImbase.FormattingEnabled = true;
    this.listBox_CatalogsImbase.Location = new Point(6, 37);
    this.listBox_CatalogsImbase.Name = "listBox_CatalogsImbase";
    this.listBox_CatalogsImbase.Size = new Size(170, 147);
    this.listBox_CatalogsImbase.TabIndex = 4;
    this.toolTip1.SetToolTip((Control) this.listBox_CatalogsImbase, "Каталоги Imbase");
    this.listBox_QuickObjectInfo.BackColor = Color.FloralWhite;
    this.listBox_QuickObjectInfo.FormattingEnabled = true;
    this.listBox_QuickObjectInfo.Location = new Point(256 /*0x0100*/, 37);
    this.listBox_QuickObjectInfo.Name = "listBox_QuickObjectInfo";
    this.listBox_QuickObjectInfo.Size = new Size(170, 147);
    this.listBox_QuickObjectInfo.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_QuickObjectInfo, "Каталоги Imbase доступные для выбора данных");
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputMat);
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputIzd);
    this.groupBox_Usl_Bases_Sbor_Input.Controls.Add((Control) this.checkBox_Usl_Bases_Sbor_isInputDoc);
    this.groupBox_Usl_Bases_Sbor_Input.Location = new Point(6, 6);
    this.groupBox_Usl_Bases_Sbor_Input.Name = "groupBox_Usl_Bases_Sbor_Input";
    this.groupBox_Usl_Bases_Sbor_Input.Size = new Size(434, 101);
    this.groupBox_Usl_Bases_Sbor_Input.TabIndex = 0;
    this.groupBox_Usl_Bases_Sbor_Input.TabStop = false;
    this.groupBox_Usl_Bases_Sbor_Input.Text = "Ввод в диалоге существующих объектов";
    this.toolTip1.SetToolTip((Control) this.groupBox_Usl_Bases_Sbor_Input, "При выполнении команды \"Добавить запись с существующим объектом\" давать доступ к объектам данного типа");
    this.checkBox_Usl_Bases_Sbor_isInputMat.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Location = new Point(18, 66);
    this.checkBox_Usl_Bases_Sbor_isInputMat.Name = "checkBox_Usl_Bases_Sbor_isInputMat";
    this.checkBox_Usl_Bases_Sbor_isInputMat.Size = new Size(84, 17);
    this.checkBox_Usl_Bases_Sbor_isInputMat.TabIndex = 2;
    this.checkBox_Usl_Bases_Sbor_isInputMat.Text = "Материалы";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputMat, "При выполнении команды \"Добавить существующий объект\" давать доступ к Материалам");
    this.checkBox_Usl_Bases_Sbor_isInputMat.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputMat.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputMat_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Location = new Point(18, 43);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Name = "checkBox_Usl_Bases_Sbor_isInputIzd";
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Size = new Size(70, 17);
    this.checkBox_Usl_Bases_Sbor_isInputIzd.TabIndex = 1;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.Text = "Изделия";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputIzd, "При выполнении команды \"Добавить существующий объект\" давать доступ к Изделиям");
    this.checkBox_Usl_Bases_Sbor_isInputIzd.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputIzd.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputIzd_CheckedChanged);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.AutoSize = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Checked = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.CheckState = CheckState.Checked;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Location = new Point(18, 19);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Name = "checkBox_Usl_Bases_Sbor_isInputDoc";
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Size = new Size(85, 17);
    this.checkBox_Usl_Bases_Sbor_isInputDoc.TabIndex = 0;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.Text = "Документы";
    this.toolTip1.SetToolTip((Control) this.checkBox_Usl_Bases_Sbor_isInputDoc, "При выполнении команды \"Добавить существующий объект\" давать доступ к Документам");
    this.checkBox_Usl_Bases_Sbor_isInputDoc.UseVisualStyleBackColor = true;
    this.checkBox_Usl_Bases_Sbor_isInputDoc.CheckedChanged += new EventHandler(this.checkBox_Usl_Bases_Sbor_isInputDoc_CheckedChanged);
    this.tabPage_Sbor.Controls.Add((Control) this.tabControl_Page_Sbor);
    this.tabPage_Sbor.Location = new Point(4, 22);
    this.tabPage_Sbor.Name = "tabPage_Sbor";
    this.tabPage_Sbor.Padding = new Padding(3);
    this.tabPage_Sbor.Size = new Size(1576, 731);
    this.tabPage_Sbor.TabIndex = 1;
    this.tabPage_Sbor.Text = "Правила сбора";
    this.tabPage_Sbor.UseVisualStyleBackColor = true;
    this.tabControl_Page_Sbor.Controls.Add((Control) this.tabPage_Sbor_Usl);
    this.tabControl_Page_Sbor.Controls.Add((Control) this.tabPage_Sbor_Peredatha);
    this.tabControl_Page_Sbor.Controls.Add((Control) this.tabPage_Sbor_Others);
    this.tabControl_Page_Sbor.Controls.Add((Control) this.tabPage_Sbor_Usl_Reference);
    this.tabControl_Page_Sbor.Controls.Add((Control) this.tabPage_ESPD);
    this.tabControl_Page_Sbor.Dock = DockStyle.Fill;
    this.tabControl_Page_Sbor.Location = new Point(3, 3);
    this.tabControl_Page_Sbor.Name = "tabControl_Page_Sbor";
    this.tabControl_Page_Sbor.SelectedIndex = 0;
    this.tabControl_Page_Sbor.Size = new Size(1570, 725);
    this.tabControl_Page_Sbor.TabIndex = 6;
    this.tabPage_Sbor_Usl.AutoScroll = true;
    this.tabPage_Sbor_Usl.BackColor = Color.Transparent;
    this.tabPage_Sbor_Usl.Controls.Add((Control) this.Sbor_Usl_Panel);
    this.tabPage_Sbor_Usl.Location = new Point(4, 22);
    this.tabPage_Sbor_Usl.Name = "tabPage_Sbor_Usl";
    this.tabPage_Sbor_Usl.Padding = new Padding(3);
    this.tabPage_Sbor_Usl.Size = new Size(1562, 699);
    this.tabPage_Sbor_Usl.TabIndex = 0;
    this.tabPage_Sbor_Usl.Text = "Условия ввода данных";
    this.Sbor_Usl_Panel.AutoScroll = true;
    this.Sbor_Usl_Panel.Controls.Add((Control) this.label_UsloviaSbora_Current);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_I_ILI);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_Text);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_Sravnenie);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_AttributeControl1);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.button_Sbor_Usl_NeVvodit);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.button_Sbor_Usl_BezUsl);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.button_Sbor_Usl_Delete1);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.button_Sbor_Usl_Edit1);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.button_Sbor_Usl_Add1);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_CollapsedTreeView);
    this.Sbor_Usl_Panel.Controls.Add((Control) this.groupBox_UsloviaVvoda);
    this.Sbor_Usl_Panel.Dock = DockStyle.Fill;
    this.Sbor_Usl_Panel.Location = new Point(3, 3);
    this.Sbor_Usl_Panel.Name = "Sbor_Usl_Panel";
    this.Sbor_Usl_Panel.Size = new Size(1556, 693);
    this.Sbor_Usl_Panel.TabIndex = 1;
    this.label_UsloviaSbora_Current.AutoSize = true;
    this.label_UsloviaSbora_Current.Location = new Point(551, 616);
    this.label_UsloviaSbora_Current.Name = "label_UsloviaSbora_Current";
    this.label_UsloviaSbora_Current.Size = new Size(0, 13);
    this.label_UsloviaSbora_Current.TabIndex = 36;
    this.label_UsloviaSbora_Current.Visible = false;
    this.groupBox_Sbor_Usl_I_ILI.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_I_ILI.Controls.Add((Control) this.radioButton_Sbor_Usl_Ili);
    this.groupBox_Sbor_Usl_I_ILI.Controls.Add((Control) this.radioButton_Sbor_Usl_I);
    this.groupBox_Sbor_Usl_I_ILI.Location = new Point(281, 614);
    this.groupBox_Sbor_Usl_I_ILI.Name = "groupBox_Sbor_Usl_I_ILI";
    this.groupBox_Sbor_Usl_I_ILI.Size = new Size(250, 67);
    this.groupBox_Sbor_Usl_I_ILI.TabIndex = 35;
    this.groupBox_Sbor_Usl_I_ILI.TabStop = false;
    this.groupBox_Sbor_Usl_I_ILI.Text = "Условие соединения";
    this.radioButton_Sbor_Usl_Ili.AutoSize = true;
    this.radioButton_Sbor_Usl_Ili.Location = new Point(6, 37);
    this.radioButton_Sbor_Usl_Ili.Name = "radioButton_Sbor_Usl_Ili";
    this.radioButton_Sbor_Usl_Ili.Size = new Size(45, 17);
    this.radioButton_Sbor_Usl_Ili.TabIndex = 1;
    this.radioButton_Sbor_Usl_Ili.TabStop = true;
    this.radioButton_Sbor_Usl_Ili.Text = "Или";
    this.radioButton_Sbor_Usl_Ili.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_I.AutoSize = true;
    this.radioButton_Sbor_Usl_I.Location = new Point(6, 17);
    this.radioButton_Sbor_Usl_I.Name = "radioButton_Sbor_Usl_I";
    this.radioButton_Sbor_Usl_I.Size = new Size(33, 17);
    this.radioButton_Sbor_Usl_I.TabIndex = 0;
    this.radioButton_Sbor_Usl_I.TabStop = true;
    this.radioButton_Sbor_Usl_I.Text = "И";
    this.radioButton_Sbor_Usl_I.UseVisualStyleBackColor = true;
    this.groupBox_Sbor_Usl_Text.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_Text.Controls.Add((Control) this.textBox_Sbor_Usl_TextDliaSravnenia);
    this.groupBox_Sbor_Usl_Text.Location = new Point(281, 560);
    this.groupBox_Sbor_Usl_Text.Name = "groupBox_Sbor_Usl_Text";
    this.groupBox_Sbor_Usl_Text.Size = new Size(250, 44);
    this.groupBox_Sbor_Usl_Text.TabIndex = 34;
    this.groupBox_Sbor_Usl_Text.TabStop = false;
    this.groupBox_Sbor_Usl_Text.Text = "Текст";
    this.textBox_Sbor_Usl_TextDliaSravnenia.Location = new Point(6, 14);
    this.textBox_Sbor_Usl_TextDliaSravnenia.Name = "textBox_Sbor_Usl_TextDliaSravnenia";
    this.textBox_Sbor_Usl_TextDliaSravnenia.Size = new Size(238, 20);
    this.textBox_Sbor_Usl_TextDliaSravnenia.TabIndex = 0;
    this.groupBox_Sbor_Usl_Sravnenie.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_Sravnenie.Controls.Add((Control) this.radioButton_Sbor_Usl_Nathinaetsia);
    this.groupBox_Sbor_Usl_Sravnenie.Controls.Add((Control) this.radioButton_Sbor_Usl_NeSoderzit);
    this.groupBox_Sbor_Usl_Sravnenie.Controls.Add((Control) this.radioButton_Sbor_Usl_Soderzit);
    this.groupBox_Sbor_Usl_Sravnenie.Controls.Add((Control) this.radioButton_Sbor_Usl_NeRavno);
    this.groupBox_Sbor_Usl_Sravnenie.Controls.Add((Control) this.radioButton_Sbor_Usl_Ravno);
    this.groupBox_Sbor_Usl_Sravnenie.Location = new Point(9, 560);
    this.groupBox_Sbor_Usl_Sravnenie.Name = "groupBox_Sbor_Usl_Sravnenie";
    this.groupBox_Sbor_Usl_Sravnenie.Size = new Size(250, 121);
    this.groupBox_Sbor_Usl_Sravnenie.TabIndex = 33;
    this.groupBox_Sbor_Usl_Sravnenie.TabStop = false;
    this.groupBox_Sbor_Usl_Sravnenie.Text = "Условие сравнения";
    this.radioButton_Sbor_Usl_Nathinaetsia.AutoSize = true;
    this.radioButton_Sbor_Usl_Nathinaetsia.Location = new Point(6, 94);
    this.radioButton_Sbor_Usl_Nathinaetsia.Name = "radioButton_Sbor_Usl_Nathinaetsia";
    this.radioButton_Sbor_Usl_Nathinaetsia.Size = new Size(106, 17);
    this.radioButton_Sbor_Usl_Nathinaetsia.TabIndex = 4;
    this.radioButton_Sbor_Usl_Nathinaetsia.Text = "Начинается с ...";
    this.radioButton_Sbor_Usl_Nathinaetsia.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_NeSoderzit.AutoSize = true;
    this.radioButton_Sbor_Usl_NeSoderzit.Location = new Point(6, 74);
    this.radioButton_Sbor_Usl_NeSoderzit.Name = "radioButton_Sbor_Usl_NeSoderzit";
    this.radioButton_Sbor_Usl_NeSoderzit.Size = new Size(91, 17);
    this.radioButton_Sbor_Usl_NeSoderzit.TabIndex = 3;
    this.radioButton_Sbor_Usl_NeSoderzit.Text = "Не содержит";
    this.radioButton_Sbor_Usl_NeSoderzit.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_Soderzit.AutoSize = true;
    this.radioButton_Sbor_Usl_Soderzit.Location = new Point(6, 54);
    this.radioButton_Sbor_Usl_Soderzit.Name = "radioButton_Sbor_Usl_Soderzit";
    this.radioButton_Sbor_Usl_Soderzit.Size = new Size(75, 17);
    this.radioButton_Sbor_Usl_Soderzit.TabIndex = 2;
    this.radioButton_Sbor_Usl_Soderzit.Text = "Содержит";
    this.radioButton_Sbor_Usl_Soderzit.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_NeRavno.AutoSize = true;
    this.radioButton_Sbor_Usl_NeRavno.Location = new Point(6, 34);
    this.radioButton_Sbor_Usl_NeRavno.Name = "radioButton_Sbor_Usl_NeRavno";
    this.radioButton_Sbor_Usl_NeRavno.Size = new Size(72, 17);
    this.radioButton_Sbor_Usl_NeRavno.TabIndex = 1;
    this.radioButton_Sbor_Usl_NeRavno.Text = "Не равно";
    this.radioButton_Sbor_Usl_NeRavno.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_Ravno.AutoSize = true;
    this.radioButton_Sbor_Usl_Ravno.Checked = true;
    this.radioButton_Sbor_Usl_Ravno.Location = new Point(6, 14);
    this.radioButton_Sbor_Usl_Ravno.Name = "radioButton_Sbor_Usl_Ravno";
    this.radioButton_Sbor_Usl_Ravno.Size = new Size(56, 17);
    this.radioButton_Sbor_Usl_Ravno.TabIndex = 0;
    this.radioButton_Sbor_Usl_Ravno.TabStop = true;
    this.radioButton_Sbor_Usl_Ravno.Text = "Равно";
    this.radioButton_Sbor_Usl_Ravno.UseVisualStyleBackColor = true;
    this.groupBox_Sbor_Usl_AttributeControl1.AutoSize = true;
    this.groupBox_Sbor_Usl_AttributeControl1.Controls.Add((Control) this.select_Sbor_Usl_AttributeControl1);
    this.groupBox_Sbor_Usl_AttributeControl1.Location = new Point(6, 6);
    this.groupBox_Sbor_Usl_AttributeControl1.Name = "groupBox_Sbor_Usl_AttributeControl1";
    this.groupBox_Sbor_Usl_AttributeControl1.Size = new Size(528, 545);
    this.groupBox_Sbor_Usl_AttributeControl1.TabIndex = 25;
    this.groupBox_Sbor_Usl_AttributeControl1.TabStop = false;
    this.groupBox_Sbor_Usl_AttributeControl1.Text = "Выбор атрибутов";
    this.select_Sbor_Usl_AttributeControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this.select_Sbor_Usl_AttributeControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.select_Sbor_Usl_AttributeControl1.Font = new Font("Tahoma", 8.25f);
    this.select_Sbor_Usl_AttributeControl1.Location = new Point(3, 16 /*0x10*/);
    this.select_Sbor_Usl_AttributeControl1.Name = "select_Sbor_Usl_AttributeControl1";
    this.select_Sbor_Usl_AttributeControl1.Size = new Size(519, 510);
    this.select_Sbor_Usl_AttributeControl1.TabIndex = 1;
    this.select_Sbor_Usl_AttributeControl1.ViewType = ViewType.All;
    this.button_Sbor_Usl_NeVvodit.Enabled = false;
    this.button_Sbor_Usl_NeVvodit.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_NeVvodit.Image");
    this.button_Sbor_Usl_NeVvodit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_NeVvodit.Location = new Point(565, 334);
    this.button_Sbor_Usl_NeVvodit.Name = "button_Sbor_Usl_NeVvodit";
    this.button_Sbor_Usl_NeVvodit.Size = new Size(121, 27);
    this.button_Sbor_Usl_NeVvodit.TabIndex = 32 /*0x20*/;
    this.button_Sbor_Usl_NeVvodit.Text = "Не вводить";
    this.button_Sbor_Usl_NeVvodit.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_NeVvodit.Click += new EventHandler(this.button_Sbor_Usl_NeVvodit_Click);
    this.button_Sbor_Usl_BezUsl.Enabled = false;
    this.button_Sbor_Usl_BezUsl.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_BezUsl.Image");
    this.button_Sbor_Usl_BezUsl.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_BezUsl.Location = new Point(565, 284);
    this.button_Sbor_Usl_BezUsl.Name = "button_Sbor_Usl_BezUsl";
    this.button_Sbor_Usl_BezUsl.Size = new Size(121, 27);
    this.button_Sbor_Usl_BezUsl.TabIndex = 31 /*0x1F*/;
    this.button_Sbor_Usl_BezUsl.Text = "Без условий";
    this.button_Sbor_Usl_BezUsl.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_BezUsl.Click += new EventHandler(this.button_Sbor_Usl_BezUsl_Click);
    this.button_Sbor_Usl_Delete1.Enabled = false;
    this.button_Sbor_Usl_Delete1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Delete1.Image");
    this.button_Sbor_Usl_Delete1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Delete1.Location = new Point(565, 234);
    this.button_Sbor_Usl_Delete1.Name = "button_Sbor_Usl_Delete1";
    this.button_Sbor_Usl_Delete1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Delete1.TabIndex = 30;
    this.button_Sbor_Usl_Delete1.Text = "Удалить";
    this.button_Sbor_Usl_Delete1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Delete1.Click += new EventHandler(this.button_Sbor_Usl_Delete1_Click);
    this.button_Sbor_Usl_Edit1.Enabled = false;
    this.button_Sbor_Usl_Edit1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Edit1.Image");
    this.button_Sbor_Usl_Edit1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Edit1.Location = new Point(565, 184);
    this.button_Sbor_Usl_Edit1.Name = "button_Sbor_Usl_Edit1";
    this.button_Sbor_Usl_Edit1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Edit1.TabIndex = 29;
    this.button_Sbor_Usl_Edit1.Text = "Изменить";
    this.button_Sbor_Usl_Edit1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Edit1.Click += new EventHandler(this.button_Sbor_Usl_Edit1_Click);
    this.button_Sbor_Usl_Add1.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Sbor_Usl_Add1.Enabled = false;
    this.button_Sbor_Usl_Add1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Add1.Image");
    this.button_Sbor_Usl_Add1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Add1.Location = new Point(565, 134);
    this.button_Sbor_Usl_Add1.Name = "button_Sbor_Usl_Add1";
    this.button_Sbor_Usl_Add1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Add1.TabIndex = 28;
    this.button_Sbor_Usl_Add1.Text = "Добавить";
    this.button_Sbor_Usl_Add1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Add1.Click += new EventHandler(this.button_Sbor_Usl_Add1_Click);
    this.groupBox_Sbor_Usl_CollapsedTreeView.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_CollapsedTreeView.Controls.Add((Control) this.radioButtonCollapsedEmpty);
    this.groupBox_Sbor_Usl_CollapsedTreeView.Controls.Add((Control) this.radioButtonExpanded);
    this.groupBox_Sbor_Usl_CollapsedTreeView.Controls.Add((Control) this.radioButtonCollapseAll);
    this.groupBox_Sbor_Usl_CollapsedTreeView.Location = new Point(548, 29);
    this.groupBox_Sbor_Usl_CollapsedTreeView.Name = "groupBox_Sbor_Usl_CollapsedTreeView";
    this.groupBox_Sbor_Usl_CollapsedTreeView.Size = new Size(159, 83);
    this.groupBox_Sbor_Usl_CollapsedTreeView.TabIndex = 27;
    this.groupBox_Sbor_Usl_CollapsedTreeView.TabStop = false;
    this.groupBox_Sbor_Usl_CollapsedTreeView.Text = "Условия ввода";
    this.radioButtonCollapsedEmpty.AutoSize = true;
    this.radioButtonCollapsedEmpty.Checked = true;
    this.radioButtonCollapsedEmpty.Location = new Point(6, 54);
    this.radioButtonCollapsedEmpty.Name = "radioButtonCollapsedEmpty";
    this.radioButtonCollapsedEmpty.Size = new Size(111, 17);
    this.radioButtonCollapsedEmpty.TabIndex = 2;
    this.radioButtonCollapsedEmpty.TabStop = true;
    this.radioButtonCollapsedEmpty.Text = "Свернуть пустые";
    this.radioButtonCollapsedEmpty.UseVisualStyleBackColor = true;
    this.radioButtonCollapsedEmpty.CheckedChanged += new EventHandler(this.radioButtonCollapsedEmpty_CheckedChanged);
    this.radioButtonExpanded.AutoSize = true;
    this.radioButtonExpanded.Location = new Point(6, 34);
    this.radioButtonExpanded.Name = "radioButtonExpanded";
    this.radioButtonExpanded.Size = new Size(105, 17);
    this.radioButtonExpanded.TabIndex = 1;
    this.radioButtonExpanded.Text = "Развернуть все";
    this.radioButtonExpanded.UseVisualStyleBackColor = true;
    this.radioButtonExpanded.CheckedChanged += new EventHandler(this.radioButtonExpanded_CheckedChanged);
    this.radioButtonCollapseAll.AutoSize = true;
    this.radioButtonCollapseAll.Location = new Point(6, 14);
    this.radioButtonCollapseAll.Name = "radioButtonCollapseAll";
    this.radioButtonCollapseAll.Size = new Size(93, 17);
    this.radioButtonCollapseAll.TabIndex = 0;
    this.radioButtonCollapseAll.Text = "Свернуть все";
    this.radioButtonCollapseAll.UseVisualStyleBackColor = true;
    this.radioButtonCollapseAll.CheckedChanged += new EventHandler(this.radioButtonCollapseAll_CheckedChanged);
    this.groupBox_UsloviaVvoda.AutoSize = true;
    this.groupBox_UsloviaVvoda.Controls.Add((Control) this.treeView_UsloviaSbora);
    this.groupBox_UsloviaVvoda.Location = new Point(722, 6);
    this.groupBox_UsloviaVvoda.Name = "groupBox_UsloviaVvoda";
    this.groupBox_UsloviaVvoda.Size = new Size(525, 670);
    this.groupBox_UsloviaVvoda.TabIndex = 26;
    this.groupBox_UsloviaVvoda.TabStop = false;
    this.groupBox_UsloviaVvoda.Text = "Условия ввода данных из разделов спецификации";
    this.treeView_UsloviaSbora.Dock = DockStyle.Fill;
    this.treeView_UsloviaSbora.HideSelection = false;
    this.treeView_UsloviaSbora.ImageIndex = 0;
    this.treeView_UsloviaSbora.ImageList = this.imageList1;
    this.treeView_UsloviaSbora.Location = new Point(3, 16 /*0x10*/);
    this.treeView_UsloviaSbora.Name = "treeView_UsloviaSbora";
    this.treeView_UsloviaSbora.SelectedImageIndex = 0;
    this.treeView_UsloviaSbora.Size = new Size(519, 651);
    this.treeView_UsloviaSbora.TabIndex = 1;
    this.treeView_UsloviaSbora.AfterSelect += new TreeViewEventHandler(this.treeViewUsloviaVVoda_AfterSelect);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.tabPage_Sbor_Peredatha.AutoScroll = true;
    this.tabPage_Sbor_Peredatha.BackColor = Color.Transparent;
    this.tabPage_Sbor_Peredatha.Controls.Add((Control) this.button_Sbor_Peredatha_Delete2);
    this.tabPage_Sbor_Peredatha.Controls.Add((Control) this.button_Sbor_Peredatha_Add2);
    this.tabPage_Sbor_Peredatha.Controls.Add((Control) this.groupBox_Sbor_Peredatha_AttributeControl1);
    this.tabPage_Sbor_Peredatha.Controls.Add((Control) this.groupBox_Sbor_Peredatha_ListId);
    this.tabPage_Sbor_Peredatha.Location = new Point(4, 22);
    this.tabPage_Sbor_Peredatha.Name = "tabPage_Sbor_Peredatha";
    this.tabPage_Sbor_Peredatha.Padding = new Padding(3);
    this.tabPage_Sbor_Peredatha.Size = new Size(192 /*0xC0*/, 74);
    this.tabPage_Sbor_Peredatha.TabIndex = 1;
    this.tabPage_Sbor_Peredatha.Text = "Передача данных в документ";
    this.button_Sbor_Peredatha_Delete2.Image = (Image) componentResourceManager.GetObject("button_Sbor_Peredatha_Delete2.Image");
    this.button_Sbor_Peredatha_Delete2.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Peredatha_Delete2.Location = new Point(557, 177);
    this.button_Sbor_Peredatha_Delete2.Name = "button_Sbor_Peredatha_Delete2";
    this.button_Sbor_Peredatha_Delete2.Size = new Size(121, 27);
    this.button_Sbor_Peredatha_Delete2.TabIndex = 13;
    this.button_Sbor_Peredatha_Delete2.Text = "Удалить";
    this.button_Sbor_Peredatha_Delete2.UseVisualStyleBackColor = true;
    this.button_Sbor_Peredatha_Delete2.Click += new EventHandler(this.button_Sbor_Peredatha_Delete2_Click);
    this.button_Sbor_Peredatha_Add2.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Sbor_Peredatha_Add2.Image = (Image) componentResourceManager.GetObject("button_Sbor_Peredatha_Add2.Image");
    this.button_Sbor_Peredatha_Add2.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Peredatha_Add2.Location = new Point(557, (int) sbyte.MaxValue);
    this.button_Sbor_Peredatha_Add2.Name = "button_Sbor_Peredatha_Add2";
    this.button_Sbor_Peredatha_Add2.Size = new Size(121, 27);
    this.button_Sbor_Peredatha_Add2.TabIndex = 12;
    this.button_Sbor_Peredatha_Add2.Text = "Добавить";
    this.button_Sbor_Peredatha_Add2.UseVisualStyleBackColor = true;
    this.button_Sbor_Peredatha_Add2.Click += new EventHandler(this.button_Sbor_Peredatha_Add2_Click);
    this.groupBox_Sbor_Peredatha_AttributeControl1.AutoSize = true;
    this.groupBox_Sbor_Peredatha_AttributeControl1.Controls.Add((Control) this.select_Sbor_Peredatha_AttributeControl2);
    this.groupBox_Sbor_Peredatha_AttributeControl1.Location = new Point(6, 6);
    this.groupBox_Sbor_Peredatha_AttributeControl1.Name = "groupBox_Sbor_Peredatha_AttributeControl1";
    this.groupBox_Sbor_Peredatha_AttributeControl1.Size = new Size(525, 680);
    this.groupBox_Sbor_Peredatha_AttributeControl1.TabIndex = 1;
    this.groupBox_Sbor_Peredatha_AttributeControl1.TabStop = false;
    this.groupBox_Sbor_Peredatha_AttributeControl1.Text = "Выбор атрибутов";
    this.select_Sbor_Peredatha_AttributeControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.select_Sbor_Peredatha_AttributeControl2.Dock = DockStyle.Fill;
    this.select_Sbor_Peredatha_AttributeControl2.Font = new Font("Tahoma", 8.25f);
    this.select_Sbor_Peredatha_AttributeControl2.Location = new Point(3, 16 /*0x10*/);
    this.select_Sbor_Peredatha_AttributeControl2.Name = "select_Sbor_Peredatha_AttributeControl2";
    this.select_Sbor_Peredatha_AttributeControl2.Size = new Size(519, 661);
    this.select_Sbor_Peredatha_AttributeControl2.TabIndex = 1;
    this.select_Sbor_Peredatha_AttributeControl2.ViewType = ViewType.All;
    this.groupBox_Sbor_Peredatha_ListId.AutoSize = true;
    this.groupBox_Sbor_Peredatha_ListId.Controls.Add((Control) this.listBox_Sbor_Peredatha_ListId);
    this.groupBox_Sbor_Peredatha_ListId.Location = new Point(710, 6);
    this.groupBox_Sbor_Peredatha_ListId.Name = "groupBox_Sbor_Peredatha_ListId";
    this.groupBox_Sbor_Peredatha_ListId.Size = new Size(525, 680);
    this.groupBox_Sbor_Peredatha_ListId.TabIndex = 2;
    this.groupBox_Sbor_Peredatha_ListId.TabStop = false;
    this.groupBox_Sbor_Peredatha_ListId.Text = "Список передаваемых атрибутов";
    this.listBox_Sbor_Peredatha_ListId.Dock = DockStyle.Fill;
    this.listBox_Sbor_Peredatha_ListId.FormattingEnabled = true;
    this.listBox_Sbor_Peredatha_ListId.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Sbor_Peredatha_ListId.Name = "listBox_Sbor_Peredatha_ListId";
    this.listBox_Sbor_Peredatha_ListId.Size = new Size(519, 661);
    this.listBox_Sbor_Peredatha_ListId.TabIndex = 1;
    this.listBox_Sbor_Peredatha_ListId.KeyDown += new KeyEventHandler(this.listBox_Sbor_Peredatha_ListId_KeyDown);
    this.tabPage_Sbor_Others.Controls.Add((Control) this.checkBox_Others_Reference_Show);
    this.tabPage_Sbor_Others.Controls.Add((Control) this.groupBox_Sbor_Others_DopZam);
    this.tabPage_Sbor_Others.Controls.Add((Control) this.groupBox_Sbor_Others_Complecty);
    this.tabPage_Sbor_Others.Controls.Add((Control) this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved);
    this.tabPage_Sbor_Others.Controls.Add((Control) this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit);
    this.tabPage_Sbor_Others.Location = new Point(4, 22);
    this.tabPage_Sbor_Others.Name = "tabPage_Sbor_Others";
    this.tabPage_Sbor_Others.Padding = new Padding(3);
    this.tabPage_Sbor_Others.Size = new Size(192 /*0xC0*/, 74);
    this.tabPage_Sbor_Others.TabIndex = 2;
    this.tabPage_Sbor_Others.Text = "Прочее";
    this.checkBox_Others_Reference_Show.AutoSize = true;
    this.checkBox_Others_Reference_Show.Location = new Point(21, 314);
    this.checkBox_Others_Reference_Show.Name = "checkBox_Others_Reference_Show";
    this.checkBox_Others_Reference_Show.Size = new Size(132, 17);
    this.checkBox_Others_Reference_Show.TabIndex = 15;
    this.checkBox_Others_Reference_Show.Text = "Работа со ссылками";
    this.toolTip1.SetToolTip((Control) this.checkBox_Others_Reference_Show, "Используются ссылки на документы");
    this.checkBox_Others_Reference_Show.UseVisualStyleBackColor = true;
    this.checkBox_Others_Reference_Show.CheckedChanged += new EventHandler(this.checkBox_Others_Reference_Show_CheckedChanged);
    this.groupBox_Sbor_Others_DopZam.Controls.Add((Control) this.checkBox_Sbor_Others_IsAllocateDopZam);
    this.groupBox_Sbor_Others_DopZam.Controls.Add((Control) this.checkBox_Sbor_Others_IsDopZam);
    this.groupBox_Sbor_Others_DopZam.Location = new Point(21, 197);
    this.groupBox_Sbor_Others_DopZam.Name = "groupBox_Sbor_Others_DopZam";
    this.groupBox_Sbor_Others_DopZam.Size = new Size(562, 95);
    this.groupBox_Sbor_Others_DopZam.TabIndex = 3;
    this.groupBox_Sbor_Others_DopZam.TabStop = false;
    this.groupBox_Sbor_Others_DopZam.Text = "Допустимые замены";
    this.checkBox_Sbor_Others_IsAllocateDopZam.AutoSize = true;
    this.checkBox_Sbor_Others_IsAllocateDopZam.Location = new Point(32 /*0x20*/, 55);
    this.checkBox_Sbor_Others_IsAllocateDopZam.Name = "checkBox_Sbor_Others_IsAllocateDopZam";
    this.checkBox_Sbor_Others_IsAllocateDopZam.Size = new Size(182, 17);
    this.checkBox_Sbor_Others_IsAllocateDopZam.TabIndex = 1;
    this.checkBox_Sbor_Others_IsAllocateDopZam.Text = "Выделять в отдельный раздел";
    this.toolTip1.SetToolTip((Control) this.checkBox_Sbor_Others_IsAllocateDopZam, "Выделять в отдельный раздел после основных записей");
    this.checkBox_Sbor_Others_IsAllocateDopZam.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_IsAllocateDopZam.Visible = false;
    this.checkBox_Sbor_Others_IsAllocateDopZam.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_IsAllocateDopZam_CheckedChanged);
    this.checkBox_Sbor_Others_IsDopZam.AutoSize = true;
    this.checkBox_Sbor_Others_IsDopZam.Location = new Point(32 /*0x20*/, 23);
    this.checkBox_Sbor_Others_IsDopZam.Name = "checkBox_Sbor_Others_IsDopZam";
    this.checkBox_Sbor_Others_IsDopZam.Size = new Size(183, 17);
    this.checkBox_Sbor_Others_IsDopZam.TabIndex = 0;
    this.checkBox_Sbor_Others_IsDopZam.Text = "Включать допустимые замены";
    this.checkBox_Sbor_Others_IsDopZam.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_IsDopZam.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_IsDopZam_CheckedChanged);
    this.groupBox_Sbor_Others_Complecty.Controls.Add((Control) this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty);
    this.groupBox_Sbor_Others_Complecty.Controls.Add((Control) this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty);
    this.groupBox_Sbor_Others_Complecty.Location = new Point(21, 117);
    this.groupBox_Sbor_Others_Complecty.Name = "groupBox_Sbor_Others_Complecty";
    this.groupBox_Sbor_Others_Complecty.Size = new Size(562, 74);
    this.groupBox_Sbor_Others_Complecty.TabIndex = 2;
    this.groupBox_Sbor_Others_Complecty.TabStop = false;
    this.groupBox_Sbor_Others_Complecty.Text = "Комплекты";
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.AutoSize = true;
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Location = new Point(32 /*0x20*/, 46);
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Name = "checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty";
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Size = new Size(271, 17);
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.TabIndex = 1;
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.Text = "Сами комплекты выделять в отдельный раздел";
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_Is_Vydeliat_Sami_Komplekty_CheckedChanged);
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.AutoSize = true;
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Location = new Point(32 /*0x20*/, 23);
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Name = "checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty";
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Size = new Size(302, 17);
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.TabIndex = 0;
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.Text = "Получаемое через комплекты выделять в количестве";
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_Is_Vydeliat_Therez_Komplekty_CheckedChanged);
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Controls.Add((Control) this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved);
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Location = new Point(21, 54);
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Name = "groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved";
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Size = new Size(562, 57);
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.TabIndex = 1;
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.TabStop = false;
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Text = "Ведомости составных частей";
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.AutoSize = true;
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Location = new Point(32 /*0x20*/, 23);
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Name = "checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved";
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Size = new Size(364, 17);
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.TabIndex = 0;
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.Text = "Раскрывать спецификации, имеющие в себе такую же ведомость";
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_IsRaskrSP_s_takoi_Ved_CheckedChanged);
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.AutoSize = true;
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Location = new Point(53, 28);
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Name = "checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit";
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Size = new Size(272, 17);
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.TabIndex = 0;
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.Text = "Головную спецификацию включать в ведомость";
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.UseVisualStyleBackColor = true;
    this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit.CheckedChanged += new EventHandler(this.checkBox_Sbor_Others_IsSamuSP_ne_iz_spiska_zanosit_CheckedChanged);
    this.tabPage_Sbor_Usl_Reference.AutoScroll = true;
    this.tabPage_Sbor_Usl_Reference.Controls.Add((Control) this.Sbor_Usl_Reference_Panel);
    this.tabPage_Sbor_Usl_Reference.Location = new Point(4, 22);
    this.tabPage_Sbor_Usl_Reference.Name = "tabPage_Sbor_Usl_Reference";
    this.tabPage_Sbor_Usl_Reference.Padding = new Padding(3);
    this.tabPage_Sbor_Usl_Reference.Size = new Size(192 /*0xC0*/, 74);
    this.tabPage_Sbor_Usl_Reference.TabIndex = 3;
    this.tabPage_Sbor_Usl_Reference.Text = "Условия ввода по ссылкам";
    this.Sbor_Usl_Reference_Panel.AutoScroll = true;
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_I_ILI_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_Text_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_Sravnenie_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_CollapsedTreeView_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_UsloviaVvoda_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.groupBox_Sbor_Usl_AttributeControl_Reference);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.button_Sbor_Usl_Reference_NeVvodit);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.button_Sbor_Usl_Reference_BezUsl);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.button_Sbor_Usl_Reference_Delete1);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.button_Sbor_Usl_Reference_Edit1);
    this.Sbor_Usl_Reference_Panel.Controls.Add((Control) this.button_Sbor_Usl_Reference_Add1);
    this.Sbor_Usl_Reference_Panel.Location = new Point(3, 3);
    this.Sbor_Usl_Reference_Panel.Name = "Sbor_Usl_Reference_Panel";
    this.Sbor_Usl_Reference_Panel.Size = new Size(1556, 728);
    this.Sbor_Usl_Reference_Panel.TabIndex = 17;
    this.groupBox_Sbor_Usl_I_ILI_Reference.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_I_ILI_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_Ili_Reference);
    this.groupBox_Sbor_Usl_I_ILI_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_I_Reference);
    this.groupBox_Sbor_Usl_I_ILI_Reference.Location = new Point(281, 614);
    this.groupBox_Sbor_Usl_I_ILI_Reference.Name = "groupBox_Sbor_Usl_I_ILI_Reference";
    this.groupBox_Sbor_Usl_I_ILI_Reference.Size = new Size(250, 67);
    this.groupBox_Sbor_Usl_I_ILI_Reference.TabIndex = 27;
    this.groupBox_Sbor_Usl_I_ILI_Reference.TabStop = false;
    this.groupBox_Sbor_Usl_I_ILI_Reference.Text = "Условие соединения";
    this.radioButton_Sbor_Usl_Ili_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_Ili_Reference.Location = new Point(6, 37);
    this.radioButton_Sbor_Usl_Ili_Reference.Name = "radioButton_Sbor_Usl_Ili_Reference";
    this.radioButton_Sbor_Usl_Ili_Reference.Size = new Size(45, 17);
    this.radioButton_Sbor_Usl_Ili_Reference.TabIndex = 1;
    this.radioButton_Sbor_Usl_Ili_Reference.TabStop = true;
    this.radioButton_Sbor_Usl_Ili_Reference.Text = "Или";
    this.radioButton_Sbor_Usl_Ili_Reference.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_I_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_I_Reference.Location = new Point(6, 17);
    this.radioButton_Sbor_Usl_I_Reference.Name = "radioButton_Sbor_Usl_I_Reference";
    this.radioButton_Sbor_Usl_I_Reference.Size = new Size(33, 17);
    this.radioButton_Sbor_Usl_I_Reference.TabIndex = 0;
    this.radioButton_Sbor_Usl_I_Reference.TabStop = true;
    this.radioButton_Sbor_Usl_I_Reference.Text = "И";
    this.radioButton_Sbor_Usl_I_Reference.UseVisualStyleBackColor = true;
    this.groupBox_Sbor_Usl_Text_Reference.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_Text_Reference.Controls.Add((Control) this.textBox_Sbor_Usl_TextDliaSravnenia_Reference);
    this.groupBox_Sbor_Usl_Text_Reference.Location = new Point(281, 560);
    this.groupBox_Sbor_Usl_Text_Reference.Name = "groupBox_Sbor_Usl_Text_Reference";
    this.groupBox_Sbor_Usl_Text_Reference.Size = new Size(250, 44);
    this.groupBox_Sbor_Usl_Text_Reference.TabIndex = 26;
    this.groupBox_Sbor_Usl_Text_Reference.TabStop = false;
    this.groupBox_Sbor_Usl_Text_Reference.Text = "Текст";
    this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Location = new Point(6, 14);
    this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Name = "textBox_Sbor_Usl_TextDliaSravnenia_Reference";
    this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.Size = new Size(238, 20);
    this.textBox_Sbor_Usl_TextDliaSravnenia_Reference.TabIndex = 0;
    this.groupBox_Sbor_Usl_Sravnenie_Reference.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_Nathinaetsia_Reference);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_NeSoderzit_Reference);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_Soderzit_Reference);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_NeRavno_Reference);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Controls.Add((Control) this.radioButton_Sbor_Usl_Ravno_Reference);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Location = new Point(9, 560);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Name = "groupBox_Sbor_Usl_Sravnenie_Reference";
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Size = new Size(250, 121);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.TabIndex = 25;
    this.groupBox_Sbor_Usl_Sravnenie_Reference.TabStop = false;
    this.groupBox_Sbor_Usl_Sravnenie_Reference.Text = "Условие сравнения";
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Location = new Point(6, 94);
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Name = "radioButton_Sbor_Usl_Nathinaetsia_Reference";
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Size = new Size(106, 17);
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.TabIndex = 4;
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.Text = "Начинается с ...";
    this.radioButton_Sbor_Usl_Nathinaetsia_Reference.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.Location = new Point(6, 74);
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.Name = "radioButton_Sbor_Usl_NeSoderzit_Reference";
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.Size = new Size(91, 17);
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.TabIndex = 3;
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.Text = "Не содержит";
    this.radioButton_Sbor_Usl_NeSoderzit_Reference.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_Soderzit_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_Soderzit_Reference.Location = new Point(6, 54);
    this.radioButton_Sbor_Usl_Soderzit_Reference.Name = "radioButton_Sbor_Usl_Soderzit_Reference";
    this.radioButton_Sbor_Usl_Soderzit_Reference.Size = new Size(75, 17);
    this.radioButton_Sbor_Usl_Soderzit_Reference.TabIndex = 2;
    this.radioButton_Sbor_Usl_Soderzit_Reference.Text = "Содержит";
    this.radioButton_Sbor_Usl_Soderzit_Reference.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_NeRavno_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_NeRavno_Reference.Location = new Point(6, 34);
    this.radioButton_Sbor_Usl_NeRavno_Reference.Name = "radioButton_Sbor_Usl_NeRavno_Reference";
    this.radioButton_Sbor_Usl_NeRavno_Reference.Size = new Size(72, 17);
    this.radioButton_Sbor_Usl_NeRavno_Reference.TabIndex = 1;
    this.radioButton_Sbor_Usl_NeRavno_Reference.Text = "Не равно";
    this.radioButton_Sbor_Usl_NeRavno_Reference.UseVisualStyleBackColor = true;
    this.radioButton_Sbor_Usl_Ravno_Reference.AutoSize = true;
    this.radioButton_Sbor_Usl_Ravno_Reference.Checked = true;
    this.radioButton_Sbor_Usl_Ravno_Reference.Location = new Point(6, 14);
    this.radioButton_Sbor_Usl_Ravno_Reference.Name = "radioButton_Sbor_Usl_Ravno_Reference";
    this.radioButton_Sbor_Usl_Ravno_Reference.Size = new Size(56, 17);
    this.radioButton_Sbor_Usl_Ravno_Reference.TabIndex = 0;
    this.radioButton_Sbor_Usl_Ravno_Reference.TabStop = true;
    this.radioButton_Sbor_Usl_Ravno_Reference.Text = "Равно";
    this.radioButton_Sbor_Usl_Ravno_Reference.UseVisualStyleBackColor = true;
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.BackColor = Color.Transparent;
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Controls.Add((Control) this.radioButtonCollapsedEmpty_Reference);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Controls.Add((Control) this.radioButtonExpanded_Reference);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Controls.Add((Control) this.radioButtonCollapseAll_Reference);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Location = new Point(548, 29);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Name = "groupBox_Sbor_Usl_CollapsedTreeView_Reference";
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Size = new Size(159, 83);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.TabIndex = 19;
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.TabStop = false;
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.Text = "Условия ввода";
    this.radioButtonCollapsedEmpty_Reference.AutoSize = true;
    this.radioButtonCollapsedEmpty_Reference.Checked = true;
    this.radioButtonCollapsedEmpty_Reference.Location = new Point(6, 54);
    this.radioButtonCollapsedEmpty_Reference.Name = "radioButtonCollapsedEmpty_Reference";
    this.radioButtonCollapsedEmpty_Reference.Size = new Size(111, 17);
    this.radioButtonCollapsedEmpty_Reference.TabIndex = 2;
    this.radioButtonCollapsedEmpty_Reference.TabStop = true;
    this.radioButtonCollapsedEmpty_Reference.Text = "Свернуть пустые";
    this.radioButtonCollapsedEmpty_Reference.UseVisualStyleBackColor = true;
    this.radioButtonExpanded_Reference.AutoSize = true;
    this.radioButtonExpanded_Reference.Location = new Point(6, 34);
    this.radioButtonExpanded_Reference.Name = "radioButtonExpanded_Reference";
    this.radioButtonExpanded_Reference.Size = new Size(105, 17);
    this.radioButtonExpanded_Reference.TabIndex = 1;
    this.radioButtonExpanded_Reference.Text = "Развернуть все";
    this.radioButtonExpanded_Reference.UseVisualStyleBackColor = true;
    this.radioButtonCollapseAll_Reference.AutoSize = true;
    this.radioButtonCollapseAll_Reference.Location = new Point(6, 14);
    this.radioButtonCollapseAll_Reference.Name = "radioButtonCollapseAll_Reference";
    this.radioButtonCollapseAll_Reference.Size = new Size(93, 17);
    this.radioButtonCollapseAll_Reference.TabIndex = 0;
    this.radioButtonCollapseAll_Reference.Text = "Свернуть все";
    this.radioButtonCollapseAll_Reference.UseVisualStyleBackColor = true;
    this.groupBox_UsloviaVvoda_Reference.Controls.Add((Control) this.treeView_UsloviaSbora_Reference);
    this.groupBox_UsloviaVvoda_Reference.Location = new Point(722, 6);
    this.groupBox_UsloviaVvoda_Reference.Name = "groupBox_UsloviaVvoda_Reference";
    this.groupBox_UsloviaVvoda_Reference.Size = new Size(525, 670);
    this.groupBox_UsloviaVvoda_Reference.TabIndex = 18;
    this.groupBox_UsloviaVvoda_Reference.TabStop = false;
    this.groupBox_UsloviaVvoda_Reference.Text = "Условия ввода данных из разделов спецификации";
    this.treeView_UsloviaSbora_Reference.Dock = DockStyle.Fill;
    this.treeView_UsloviaSbora_Reference.HideSelection = false;
    this.treeView_UsloviaSbora_Reference.ImageIndex = 0;
    this.treeView_UsloviaSbora_Reference.ImageList = this.imageList1;
    this.treeView_UsloviaSbora_Reference.Location = new Point(3, 16 /*0x10*/);
    this.treeView_UsloviaSbora_Reference.Name = "treeView_UsloviaSbora_Reference";
    this.treeView_UsloviaSbora_Reference.SelectedImageIndex = 0;
    this.treeView_UsloviaSbora_Reference.Size = new Size(519, 651);
    this.treeView_UsloviaSbora_Reference.TabIndex = 1;
    this.treeView_UsloviaSbora_Reference.AfterSelect += new TreeViewEventHandler(this.treeView_UsloviaSbora_Reference_AfterSelect);
    this.groupBox_Sbor_Usl_AttributeControl_Reference.AutoSize = true;
    this.groupBox_Sbor_Usl_AttributeControl_Reference.Controls.Add((Control) this.select_Sbor_Usl_AttributeControl_Reference);
    this.groupBox_Sbor_Usl_AttributeControl_Reference.Location = new Point(6, 6);
    this.groupBox_Sbor_Usl_AttributeControl_Reference.Name = "groupBox_Sbor_Usl_AttributeControl_Reference";
    this.groupBox_Sbor_Usl_AttributeControl_Reference.Size = new Size(528, 545);
    this.groupBox_Sbor_Usl_AttributeControl_Reference.TabIndex = 17;
    this.groupBox_Sbor_Usl_AttributeControl_Reference.TabStop = false;
    this.groupBox_Sbor_Usl_AttributeControl_Reference.Text = "Выбор атрибутов";
    this.select_Sbor_Usl_AttributeControl_Reference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.select_Sbor_Usl_AttributeControl_Reference.Dock = DockStyle.Fill;
    this.select_Sbor_Usl_AttributeControl_Reference.Font = new Font("Tahoma", 8.25f);
    this.select_Sbor_Usl_AttributeControl_Reference.Location = new Point(3, 16 /*0x10*/);
    this.select_Sbor_Usl_AttributeControl_Reference.Name = "select_Sbor_Usl_AttributeControl_Reference";
    this.select_Sbor_Usl_AttributeControl_Reference.Size = new Size(522, 526);
    this.select_Sbor_Usl_AttributeControl_Reference.TabIndex = 1;
    this.select_Sbor_Usl_AttributeControl_Reference.ViewType = ViewType.All;
    this.button_Sbor_Usl_Reference_NeVvodit.Enabled = false;
    this.button_Sbor_Usl_Reference_NeVvodit.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Reference_NeVvodit.Image");
    this.button_Sbor_Usl_Reference_NeVvodit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Reference_NeVvodit.Location = new Point(557, 327);
    this.button_Sbor_Usl_Reference_NeVvodit.Name = "button_Sbor_Usl_Reference_NeVvodit";
    this.button_Sbor_Usl_Reference_NeVvodit.Size = new Size(121, 27);
    this.button_Sbor_Usl_Reference_NeVvodit.TabIndex = 24;
    this.button_Sbor_Usl_Reference_NeVvodit.Text = "Не вводить";
    this.button_Sbor_Usl_Reference_NeVvodit.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Reference_BezUsl.Enabled = false;
    this.button_Sbor_Usl_Reference_BezUsl.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Reference_BezUsl.Image");
    this.button_Sbor_Usl_Reference_BezUsl.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Reference_BezUsl.Location = new Point(557, 277);
    this.button_Sbor_Usl_Reference_BezUsl.Name = "button_Sbor_Usl_Reference_BezUsl";
    this.button_Sbor_Usl_Reference_BezUsl.Size = new Size(121, 27);
    this.button_Sbor_Usl_Reference_BezUsl.TabIndex = 23;
    this.button_Sbor_Usl_Reference_BezUsl.Text = "Без условий";
    this.button_Sbor_Usl_Reference_BezUsl.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Reference_BezUsl.Click += new EventHandler(this.button_Sbor_Usl_Reference_BezUsl_Click);
    this.button_Sbor_Usl_Reference_Delete1.Enabled = false;
    this.button_Sbor_Usl_Reference_Delete1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Reference_Delete1.Image");
    this.button_Sbor_Usl_Reference_Delete1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Reference_Delete1.Location = new Point(557, 227);
    this.button_Sbor_Usl_Reference_Delete1.Name = "button_Sbor_Usl_Reference_Delete1";
    this.button_Sbor_Usl_Reference_Delete1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Reference_Delete1.TabIndex = 22;
    this.button_Sbor_Usl_Reference_Delete1.Text = "Удалить";
    this.button_Sbor_Usl_Reference_Delete1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Reference_Edit1.Enabled = false;
    this.button_Sbor_Usl_Reference_Edit1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Reference_Edit1.Image");
    this.button_Sbor_Usl_Reference_Edit1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Reference_Edit1.Location = new Point(557, 177);
    this.button_Sbor_Usl_Reference_Edit1.Name = "button_Sbor_Usl_Reference_Edit1";
    this.button_Sbor_Usl_Reference_Edit1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Reference_Edit1.TabIndex = 21;
    this.button_Sbor_Usl_Reference_Edit1.Text = "Изменить";
    this.button_Sbor_Usl_Reference_Edit1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Reference_Add1.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Sbor_Usl_Reference_Add1.Enabled = false;
    this.button_Sbor_Usl_Reference_Add1.Image = (Image) componentResourceManager.GetObject("button_Sbor_Usl_Reference_Add1.Image");
    this.button_Sbor_Usl_Reference_Add1.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Sbor_Usl_Reference_Add1.Location = new Point(557, (int) sbyte.MaxValue);
    this.button_Sbor_Usl_Reference_Add1.Name = "button_Sbor_Usl_Reference_Add1";
    this.button_Sbor_Usl_Reference_Add1.Size = new Size(121, 27);
    this.button_Sbor_Usl_Reference_Add1.TabIndex = 20;
    this.button_Sbor_Usl_Reference_Add1.Text = "Добавить";
    this.button_Sbor_Usl_Reference_Add1.UseVisualStyleBackColor = true;
    this.button_Sbor_Usl_Reference_Add1.Click += new EventHandler(this.button_Sbor_Usl_Reference_Add1_Click);
    this.tabPage_ESPD.Controls.Add((Control) this.groupBox_Remark);
    this.tabPage_ESPD.Controls.Add((Control) this.groupBox_AddToSP);
    this.tabPage_ESPD.Controls.Add((Control) this.groupBox_FirstOpen);
    this.tabPage_ESPD.Location = new Point(4, 22);
    this.tabPage_ESPD.Name = "tabPage_ESPD";
    this.tabPage_ESPD.Size = new Size(192 /*0xC0*/, 74);
    this.tabPage_ESPD.TabIndex = 4;
    this.tabPage_ESPD.Text = "Программные спецификации";
    this.groupBox_Remark.Controls.Add((Control) this.textBox_textRemark);
    this.groupBox_Remark.Controls.Add((Control) this.checkBox_isAddRemark);
    this.groupBox_Remark.Location = new Point(18, 207);
    this.groupBox_Remark.Name = "groupBox_Remark";
    this.groupBox_Remark.Size = new Size(406, 93);
    this.groupBox_Remark.TabIndex = 6;
    this.groupBox_Remark.TabStop = false;
    this.groupBox_Remark.Text = "При добавлении записи о Листе утверждения документа";
    this.toolTip1.SetToolTip((Control) this.groupBox_Remark, "При добавлении записи о Листе утверждения ljrevtynf, добалять текст в Примечание");
    this.textBox_textRemark.Location = new Point(18, 51);
    this.textBox_textRemark.Name = "textBox_textRemark";
    this.textBox_textRemark.Size = new Size(365, 20);
    this.textBox_textRemark.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.textBox_textRemark, "Текст примечания, например \"Размножать по указанию\"");
    this.textBox_textRemark.TextChanged += new EventHandler(this.textBox_textRemark_TextChanged);
    this.textBox_textRemark.Leave += new EventHandler(this.textBox_textRemark_Leave);
    this.checkBox_isAddRemark.AutoSize = true;
    this.checkBox_isAddRemark.Location = new Point(18, 19);
    this.checkBox_isAddRemark.Name = "checkBox_isAddRemark";
    this.checkBox_isAddRemark.Size = new Size(180, 17);
    this.checkBox_isAddRemark.TabIndex = 0;
    this.checkBox_isAddRemark.Text = "Заносить в Примечание текст";
    this.toolTip1.SetToolTip((Control) this.checkBox_isAddRemark, "При добавлении записи о Листе утверждения, добалять текст в Примечание");
    this.checkBox_isAddRemark.UseVisualStyleBackColor = true;
    this.checkBox_isAddRemark.CheckedChanged += new EventHandler(this.checkBox_isAddRemark_CheckedChanged);
    this.groupBox_AddToSP.Controls.Add((Control) this.checkBox_isAddToSpLU);
    this.groupBox_AddToSP.Location = new Point(18, 131);
    this.groupBox_AddToSP.Name = "groupBox_AddToSP";
    this.groupBox_AddToSP.Size = new Size(406, 52);
    this.groupBox_AddToSP.TabIndex = 5;
    this.groupBox_AddToSP.TabStop = false;
    this.groupBox_AddToSP.Text = "При добавлении в спецификацию записи";
    this.toolTip1.SetToolTip((Control) this.groupBox_AddToSP, "Действия при добавлении записи в спецификацию");
    this.checkBox_isAddToSpLU.AutoSize = true;
    this.checkBox_isAddToSpLU.Location = new Point(18, 19);
    this.checkBox_isAddToSpLU.Name = "checkBox_isAddToSpLU";
    this.checkBox_isAddToSpLU.Size = new Size(316, 17);
    this.checkBox_isAddToSpLU.TabIndex = 0;
    this.checkBox_isAddToSpLU.Text = "Заносить в спецификацию Лист утверждения документа";
    this.toolTip1.SetToolTip((Control) this.checkBox_isAddToSpLU, "Автоматически заносить в спецификацию и Лист утвержения");
    this.checkBox_isAddToSpLU.UseVisualStyleBackColor = true;
    this.checkBox_isAddToSpLU.CheckedChanged += new EventHandler(this.checkBox_isAddToSpLU_CheckedChanged);
    this.groupBox_FirstOpen.Controls.Add((Control) this.checkBox_isOpenLU);
    this.groupBox_FirstOpen.Controls.Add((Control) this.checkBox_isCreateLU);
    this.groupBox_FirstOpen.Controls.Add((Control) this.checkBox_isAddLU);
    this.groupBox_FirstOpen.Location = new Point(18, 14);
    this.groupBox_FirstOpen.Name = "groupBox_FirstOpen";
    this.groupBox_FirstOpen.Size = new Size(406, 99);
    this.groupBox_FirstOpen.TabIndex = 4;
    this.groupBox_FirstOpen.TabStop = false;
    this.groupBox_FirstOpen.Text = "При первом открытии спецификации ЕСПД";
    this.toolTip1.SetToolTip((Control) this.groupBox_FirstOpen, "Действия при первом открытии спецификации");
    this.checkBox_isOpenLU.AutoSize = true;
    this.checkBox_isOpenLU.Location = new Point(18, 65);
    this.checkBox_isOpenLU.Name = "checkBox_isOpenLU";
    this.checkBox_isOpenLU.Size = new Size(235, 17);
    this.checkBox_isOpenLU.TabIndex = 2;
    this.checkBox_isOpenLU.Text = "Открывать редактор Листа утверждения";
    this.toolTip1.SetToolTip((Control) this.checkBox_isOpenLU, "После создания Листа утверждения открывать его в редакторе");
    this.checkBox_isOpenLU.UseVisualStyleBackColor = true;
    this.checkBox_isOpenLU.Visible = false;
    this.checkBox_isOpenLU.CheckedChanged += new EventHandler(this.checkBox_isOpenLU_CheckedChanged);
    this.checkBox_isCreateLU.AutoSize = true;
    this.checkBox_isCreateLU.Location = new Point(18, 42);
    this.checkBox_isCreateLU.Name = "checkBox_isCreateLU";
    this.checkBox_isCreateLU.Size = new Size(250, 17);
    this.checkBox_isCreateLU.TabIndex = 1;
    this.checkBox_isCreateLU.Text = "Создавать Лист утверждения (если его нет)";
    this.toolTip1.SetToolTip((Control) this.checkBox_isCreateLU, "Если Листа утверждения нет, то создавать его");
    this.checkBox_isCreateLU.UseVisualStyleBackColor = true;
    this.checkBox_isCreateLU.Visible = false;
    this.checkBox_isCreateLU.CheckedChanged += new EventHandler(this.checkBox_isCreateLU_CheckedChanged);
    this.checkBox_isAddLU.AutoSize = true;
    this.checkBox_isAddLU.Location = new Point(18, 19);
    this.checkBox_isAddLU.Name = "checkBox_isAddLU";
    this.checkBox_isAddLU.Size = new Size(260, 17);
    this.checkBox_isAddLU.TabIndex = 0;
    this.checkBox_isAddLU.Text = "Заносить в спецификацию Лист урверждения";
    this.toolTip1.SetToolTip((Control) this.checkBox_isAddLU, "При первом открытии программной спецификации автоматичести заносить в нее Лист утверждения");
    this.checkBox_isAddLU.UseVisualStyleBackColor = true;
    this.checkBox_isAddLU.CheckedChanged += new EventHandler(this.checkBox_isAddLU_CheckedChanged);
    this.tabPage_Sorting.AutoScroll = true;
    this.tabPage_Sorting.Controls.Add((Control) this.dataGridView_Sorting_Doc);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_List_Ved_Graf);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_List_Ved_Id);
    this.tabPage_Sorting.Controls.Add((Control) this._btnMoveDown_Sorting);
    this.tabPage_Sorting.Controls.Add((Control) this._btnMoveUp_Sorting);
    this.tabPage_Sorting.Controls.Add((Control) this.buttonDelete_Sorting_1);
    this.tabPage_Sorting.Controls.Add((Control) this.buttonAdd_Sorting_1);
    this.tabPage_Sorting.Controls.Add((Control) this.buttonEdit_Sorting_1);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_PoriadokSortirovki);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_PustyeStroki);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_Sravnenie);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_End);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_Begin);
    this.tabPage_Sorting.Controls.Add((Control) this.dataGridView_Sorting);
    this.tabPage_Sorting.Controls.Add((Control) this.groupBox_Sorting_AttribVedRec1);
    this.tabPage_Sorting.Location = new Point(4, 22);
    this.tabPage_Sorting.Name = "tabPage_Sorting";
    this.tabPage_Sorting.Padding = new Padding(3);
    this.tabPage_Sorting.Size = new Size(1576, 731);
    this.tabPage_Sorting.TabIndex = 2;
    this.tabPage_Sorting.Text = "Правила сортировки";
    this.tabPage_Sorting.UseVisualStyleBackColor = true;
    this.dataGridView_Sorting_Doc.AllowUserToAddRows = false;
    this.dataGridView_Sorting_Doc.AllowUserToDeleteRows = false;
    this.dataGridView_Sorting_Doc.AllowUserToResizeColumns = false;
    this.dataGridView_Sorting_Doc.AllowUserToResizeRows = false;
    this.dataGridView_Sorting_Doc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle4.BackColor = SystemColors.Control;
    gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle4.ForeColor = SystemColors.WindowText;
    gridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle4.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Sorting_Doc.ColumnHeadersDefaultCellStyle = gridViewCellStyle4;
    this.dataGridView_Sorting_Doc.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Sorting_Doc.Columns.AddRange((DataGridViewColumn) this.dataGridViewImageColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn16, (DataGridViewColumn) this.dataGridViewTextBoxColumn17, (DataGridViewColumn) this.dataGridViewTextBoxColumn18, (DataGridViewColumn) this.dataGridViewTextBoxColumn19, (DataGridViewColumn) this.dataGridViewTextBoxColumn20);
    gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle5.BackColor = SystemColors.Window;
    gridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle5.ForeColor = SystemColors.ControlText;
    gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle5.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Sorting_Doc.DefaultCellStyle = gridViewCellStyle5;
    this.dataGridView_Sorting_Doc.GridColor = SystemColors.ControlLightLight;
    this.dataGridView_Sorting_Doc.Location = new Point(460, 6);
    this.dataGridView_Sorting_Doc.MultiSelect = false;
    this.dataGridView_Sorting_Doc.Name = "dataGridView_Sorting_Doc";
    this.dataGridView_Sorting_Doc.ReadOnly = true;
    gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle6.BackColor = SystemColors.Control;
    gridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle6.ForeColor = SystemColors.WindowText;
    gridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle6.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Sorting_Doc.RowHeadersDefaultCellStyle = gridViewCellStyle6;
    this.dataGridView_Sorting_Doc.RowHeadersVisible = false;
    this.dataGridView_Sorting_Doc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Sorting_Doc.Size = new Size(1069, 440);
    this.dataGridView_Sorting_Doc.TabIndex = 37;
    this.toolTip1.SetToolTip((Control) this.dataGridView_Sorting_Doc, "Порядок сортировки каждого раздела");
    this.dataGridView_Sorting_Doc.CellEnter += new DataGridViewCellEventHandler(this.dataGridView_Sorting_Doc_CellEnter);
    this.dataGridView_Sorting_Doc.KeyDown += new KeyEventHandler(this.dataGridView_Sorting_Doc_KeyDown);
    this.dataGridViewImageColumn1.FillWeight = 30f;
    this.dataGridViewImageColumn1.HeaderText = "";
    this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
    this.dataGridViewImageColumn1.ReadOnly = true;
    this.dataGridViewImageColumn1.Resizable = DataGridViewTriState.True;
    this.dataGridViewImageColumn1.Width = 20;
    this.dataGridViewTextBoxColumn16.HeaderText = "Графа";
    this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
    this.dataGridViewTextBoxColumn16.ReadOnly = true;
    this.dataGridViewTextBoxColumn16.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn16.Width = 360;
    this.dataGridViewTextBoxColumn17.HeaderText = "От";
    this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
    this.dataGridViewTextBoxColumn17.ReadOnly = true;
    this.dataGridViewTextBoxColumn17.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn17.Width = 210;
    this.dataGridViewTextBoxColumn18.HeaderText = "До";
    this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
    this.dataGridViewTextBoxColumn18.ReadOnly = true;
    this.dataGridViewTextBoxColumn18.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn18.Width = 210;
    this.dataGridViewTextBoxColumn19.HeaderText = "Сравнение";
    this.dataGridViewTextBoxColumn19.Name = "dataGridViewTextBoxColumn19";
    this.dataGridViewTextBoxColumn19.ReadOnly = true;
    this.dataGridViewTextBoxColumn19.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn19.Width = 130;
    this.dataGridViewTextBoxColumn20.HeaderText = "Пустые строки";
    this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
    this.dataGridViewTextBoxColumn20.ReadOnly = true;
    this.dataGridViewTextBoxColumn20.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn20.Width = 130;
    this.groupBox_Sorting_List_Ved_Graf.Controls.Add((Control) this.listBox_Sorting_List_Ved_Graf);
    this.groupBox_Sorting_List_Ved_Graf.Location = new Point(3, 6);
    this.groupBox_Sorting_List_Ved_Graf.Name = "groupBox_Sorting_List_Ved_Graf";
    this.groupBox_Sorting_List_Ved_Graf.Size = new Size(445, 440);
    this.groupBox_Sorting_List_Ved_Graf.TabIndex = 36;
    this.groupBox_Sorting_List_Ved_Graf.TabStop = false;
    this.groupBox_Sorting_List_Ved_Graf.Text = "Графы бланка";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_List_Ved_Graf, "Графы бланка");
    this.groupBox_Sorting_List_Ved_Graf.Visible = false;
    this.listBox_Sorting_List_Ved_Graf.Dock = DockStyle.Fill;
    this.listBox_Sorting_List_Ved_Graf.FormattingEnabled = true;
    this.listBox_Sorting_List_Ved_Graf.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Sorting_List_Ved_Graf.Name = "listBox_Sorting_List_Ved_Graf";
    this.listBox_Sorting_List_Ved_Graf.Size = new Size(439, 421);
    this.listBox_Sorting_List_Ved_Graf.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Sorting_List_Ved_Graf, "Атрибуты, собираемые из спецификаций");
    this.groupBox_Sorting_List_Ved_Id.Controls.Add((Control) this.listBox_Sorting_List_Ved_Id);
    this.groupBox_Sorting_List_Ved_Id.Location = new Point(3, 6);
    this.groupBox_Sorting_List_Ved_Id.Name = "groupBox_Sorting_List_Ved_Id";
    this.groupBox_Sorting_List_Ved_Id.Size = new Size(445, 440);
    this.groupBox_Sorting_List_Ved_Id.TabIndex = 35;
    this.groupBox_Sorting_List_Ved_Id.TabStop = false;
    this.groupBox_Sorting_List_Ved_Id.Text = "Атрибуты, собираемые из спецификаций";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Sorting_List_Ved_Id.Dock = DockStyle.Fill;
    this.listBox_Sorting_List_Ved_Id.FormattingEnabled = true;
    this.listBox_Sorting_List_Ved_Id.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Sorting_List_Ved_Id.Name = "listBox_Sorting_List_Ved_Id";
    this.listBox_Sorting_List_Ved_Id.Size = new Size(439, 421);
    this.listBox_Sorting_List_Ved_Id.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Sorting_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Sorting_List_Ved_Id.MouseClick += new MouseEventHandler(this.listBox_Sorting_List_Ved_Id_MouseClick);
    this._btnMoveDown_Sorting.Anchor = AnchorStyles.Right;
    this._btnMoveDown_Sorting.FlatStyle = FlatStyle.Popup;
    this._btnMoveDown_Sorting.Image = (Image) componentResourceManager.GetObject("_btnMoveDown_Sorting.Image");
    this._btnMoveDown_Sorting.Location = new Point(1543, 186);
    this._btnMoveDown_Sorting.Name = "_btnMoveDown_Sorting";
    this._btnMoveDown_Sorting.Size = new Size(25, 25);
    this._btnMoveDown_Sorting.TabIndex = 21;
    this.toolTip1.SetToolTip((Control) this._btnMoveDown_Sorting, "Текущее условие опустить ниже");
    this._btnMoveDown_Sorting.Click += new EventHandler(this._btnMoveDown_Sorting_Click);
    this._btnMoveUp_Sorting.Anchor = AnchorStyles.Right;
    this._btnMoveUp_Sorting.FlatStyle = FlatStyle.Popup;
    this._btnMoveUp_Sorting.Image = (Image) componentResourceManager.GetObject("_btnMoveUp_Sorting.Image");
    this._btnMoveUp_Sorting.Location = new Point(1543, 143);
    this._btnMoveUp_Sorting.Name = "_btnMoveUp_Sorting";
    this._btnMoveUp_Sorting.Size = new Size(25, 25);
    this._btnMoveUp_Sorting.TabIndex = 20;
    this.toolTip1.SetToolTip((Control) this._btnMoveUp_Sorting, "Текущее условие поднять выше");
    this._btnMoveUp_Sorting.Click += new EventHandler(this._btnMoveUp_Sorting_Click);
    this.buttonDelete_Sorting_1.Enabled = false;
    this.buttonDelete_Sorting_1.Image = (Image) componentResourceManager.GetObject("buttonDelete_Sorting_1.Image");
    this.buttonDelete_Sorting_1.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonDelete_Sorting_1.Location = new Point(1187, 682);
    this.buttonDelete_Sorting_1.Name = "buttonDelete_Sorting_1";
    this.buttonDelete_Sorting_1.Size = new Size(121, 27);
    this.buttonDelete_Sorting_1.TabIndex = 19;
    this.buttonDelete_Sorting_1.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.buttonDelete_Sorting_1, "Удалить текущее условие");
    this.buttonDelete_Sorting_1.UseVisualStyleBackColor = true;
    this.buttonDelete_Sorting_1.Click += new EventHandler(this.buttonDelete_Sorting_1_Click);
    this.buttonAdd_Sorting_1.AccessibleRole = AccessibleRole.OutlineButton;
    this.buttonAdd_Sorting_1.Image = (Image) componentResourceManager.GetObject("buttonAdd_Sorting_1.Image");
    this.buttonAdd_Sorting_1.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonAdd_Sorting_1.Location = new Point(1047, 682);
    this.buttonAdd_Sorting_1.Name = "buttonAdd_Sorting_1";
    this.buttonAdd_Sorting_1.Size = new Size(121, 27);
    this.buttonAdd_Sorting_1.TabIndex = 18;
    this.buttonAdd_Sorting_1.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.buttonAdd_Sorting_1, "Добавить новое условие");
    this.buttonAdd_Sorting_1.UseVisualStyleBackColor = true;
    this.buttonAdd_Sorting_1.Click += new EventHandler(this.buttonAdd_Sorting_1_Click);
    this.buttonEdit_Sorting_1.Enabled = false;
    this.buttonEdit_Sorting_1.Image = (Image) componentResourceManager.GetObject("buttonEdit_Sorting_1.Image");
    this.buttonEdit_Sorting_1.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonEdit_Sorting_1.Location = new Point(898, 682);
    this.buttonEdit_Sorting_1.Name = "buttonEdit_Sorting_1";
    this.buttonEdit_Sorting_1.Size = new Size(121, 27);
    this.buttonEdit_Sorting_1.TabIndex = 17;
    this.buttonEdit_Sorting_1.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.buttonEdit_Sorting_1, "Изменить текущее условие согласно выбранным параметрам");
    this.buttonEdit_Sorting_1.UseVisualStyleBackColor = true;
    this.buttonEdit_Sorting_1.Click += new EventHandler(this.buttonEdit_Sorting_1_Click);
    this.groupBox_Sorting_PoriadokSortirovki.Controls.Add((Control) this.radioButton_Sorting_PoriadokSortirovkiUbyvanie);
    this.groupBox_Sorting_PoriadokSortirovki.Controls.Add((Control) this.radioButton_Sorting_PoriadokSortirovkiVozrastanie);
    this.groupBox_Sorting_PoriadokSortirovki.Location = new Point(1268, 611);
    this.groupBox_Sorting_PoriadokSortirovki.Name = "groupBox_Sorting_PoriadokSortirovki";
    this.groupBox_Sorting_PoriadokSortirovki.Size = new Size(300, 60);
    this.groupBox_Sorting_PoriadokSortirovki.TabIndex = 16 /*0x10*/;
    this.groupBox_Sorting_PoriadokSortirovki.TabStop = false;
    this.groupBox_Sorting_PoriadokSortirovki.Text = "Расположение:";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_PoriadokSortirovki, "Сортировать в порядке возрастания или убывания");
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.AutoSize = true;
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Location = new Point(6, 34);
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Name = "radioButton_Sorting_PoriadokSortirovkiUbyvanie";
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Size = new Size(93, 17);
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.TabIndex = 1;
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.Text = "По убыванию";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_PoriadokSortirovkiUbyvanie, "Сортировать в порядке убывания");
    this.radioButton_Sorting_PoriadokSortirovkiUbyvanie.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.AutoSize = true;
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Checked = true;
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Location = new Point(6, 15);
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Name = "radioButton_Sorting_PoriadokSortirovkiVozrastanie";
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Size = new Size(109, 17);
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.TabIndex = 0;
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.TabStop = true;
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.Text = "По возрастанию";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_PoriadokSortirovkiVozrastanie, "Сортировать в порядке возрастания");
    this.radioButton_Sorting_PoriadokSortirovkiVozrastanie.UseVisualStyleBackColor = true;
    this.groupBox_Sorting_PustyeStroki.Controls.Add((Control) this.radioButton_Sorting_PustyeStrokiVkonce);
    this.groupBox_Sorting_PustyeStroki.Controls.Add((Control) this.radioButton_Sorting_PustyeStrokiVnathale);
    this.groupBox_Sorting_PustyeStroki.Location = new Point(1268, 536);
    this.groupBox_Sorting_PustyeStroki.Name = "groupBox_Sorting_PustyeStroki";
    this.groupBox_Sorting_PustyeStroki.Size = new Size(300, 60);
    this.groupBox_Sorting_PustyeStroki.TabIndex = 15;
    this.groupBox_Sorting_PustyeStroki.TabStop = false;
    this.groupBox_Sorting_PustyeStroki.Text = "Пустая запись:";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_PustyeStroki, "Куда помещеть запись с пустым текстом в данном атрибуте");
    this.radioButton_Sorting_PustyeStrokiVkonce.AutoSize = true;
    this.radioButton_Sorting_PustyeStrokiVkonce.Checked = true;
    this.radioButton_Sorting_PustyeStrokiVkonce.Location = new Point(6, 34);
    this.radioButton_Sorting_PustyeStrokiVkonce.Name = "radioButton_Sorting_PustyeStrokiVkonce";
    this.radioButton_Sorting_PustyeStrokiVkonce.Size = new Size(65, 17);
    this.radioButton_Sorting_PustyeStrokiVkonce.TabIndex = 1;
    this.radioButton_Sorting_PustyeStrokiVkonce.TabStop = true;
    this.radioButton_Sorting_PustyeStrokiVkonce.Text = "В конце";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_PustyeStrokiVkonce, "Запись с пустым текстом в данном атрибуте помещать в конце группы записей");
    this.radioButton_Sorting_PustyeStrokiVkonce.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_PustyeStrokiVnathale.AutoSize = true;
    this.radioButton_Sorting_PustyeStrokiVnathale.Location = new Point(6, 15);
    this.radioButton_Sorting_PustyeStrokiVnathale.Name = "radioButton_Sorting_PustyeStrokiVnathale";
    this.radioButton_Sorting_PustyeStrokiVnathale.Size = new Size(70, 17);
    this.radioButton_Sorting_PustyeStrokiVnathale.TabIndex = 0;
    this.radioButton_Sorting_PustyeStrokiVnathale.Text = "В начале";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_PustyeStrokiVnathale, "Запись с пустым текстом в данном атрибуте помещать в начале группы записей");
    this.radioButton_Sorting_PustyeStrokiVnathale.UseVisualStyleBackColor = true;
    this.groupBox_Sorting_Sravnenie.Controls.Add((Control) this.radioButton_Sorting_SravnenieNumber);
    this.groupBox_Sorting_Sravnenie.Controls.Add((Control) this.radioButton_Sorting_SravnenieSymbol);
    this.groupBox_Sorting_Sravnenie.Location = new Point(1268, 465);
    this.groupBox_Sorting_Sravnenie.Name = "groupBox_Sorting_Sravnenie";
    this.groupBox_Sorting_Sravnenie.Size = new Size(300, 60);
    this.groupBox_Sorting_Sravnenie.TabIndex = 14;
    this.groupBox_Sorting_Sravnenie.TabStop = false;
    this.groupBox_Sorting_Sravnenie.Text = "Тип сравнения:";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_Sravnenie, "Тип сравнения");
    this.radioButton_Sorting_SravnenieNumber.AutoSize = true;
    this.radioButton_Sorting_SravnenieNumber.Location = new Point(6, 34);
    this.radioButton_Sorting_SravnenieNumber.Name = "radioButton_Sorting_SravnenieNumber";
    this.radioButton_Sorting_SravnenieNumber.Size = new Size(75, 17);
    this.radioButton_Sorting_SravnenieNumber.TabIndex = 1;
    this.radioButton_Sorting_SravnenieNumber.Text = "Числовое";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_SravnenieNumber, "Группы цифр в строке определяются как числа");
    this.radioButton_Sorting_SravnenieNumber.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_SravnenieSymbol.AutoSize = true;
    this.radioButton_Sorting_SravnenieSymbol.Checked = true;
    this.radioButton_Sorting_SravnenieSymbol.Location = new Point(6, 15);
    this.radioButton_Sorting_SravnenieSymbol.Name = "radioButton_Sorting_SravnenieSymbol";
    this.radioButton_Sorting_SravnenieSymbol.Size = new Size(88, 17);
    this.radioButton_Sorting_SravnenieSymbol.TabIndex = 0;
    this.radioButton_Sorting_SravnenieSymbol.TabStop = true;
    this.radioButton_Sorting_SravnenieSymbol.Text = "Символьное";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_SravnenieSymbol, "Цифры в строке определяются как отдельные символы");
    this.radioButton_Sorting_SravnenieSymbol.UseVisualStyleBackColor = true;
    this.groupBox_Sorting_End.Controls.Add((Control) this.comboBox_Sorting_SymbolEnd);
    this.groupBox_Sorting_End.Controls.Add((Control) this.labelEnd_Sorting_2);
    this.groupBox_Sorting_End.Controls.Add((Control) this.numericUpDown_Sorting_NumberEnd);
    this.groupBox_Sorting_End.Controls.Add((Control) this.labelEnd_Sorting_1);
    this.groupBox_Sorting_End.Controls.Add((Control) this.groupBox_Sorting_Do);
    this.groupBox_Sorting_End.Location = new Point(858, 465);
    this.groupBox_Sorting_End.Name = "groupBox_Sorting_End";
    this.groupBox_Sorting_End.Size = new Size(404, 206);
    this.groupBox_Sorting_End.TabIndex = 13;
    this.groupBox_Sorting_End.TabStop = false;
    this.groupBox_Sorting_End.Text = "Окончание подстроки";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_End, "Выбор, до куда начинать сравнения строки");
    this.comboBox_Sorting_SymbolEnd.FormattingEnabled = true;
    this.comboBox_Sorting_SymbolEnd.Items.AddRange(new object[5]
    {
      (object) "  (пробел)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)"
    });
    this.comboBox_Sorting_SymbolEnd.Location = new Point(77, 167);
    this.comboBox_Sorting_SymbolEnd.Name = "comboBox_Sorting_SymbolEnd";
    this.comboBox_Sorting_SymbolEnd.Size = new Size(121, 21);
    this.comboBox_Sorting_SymbolEnd.TabIndex = 3;
    this.labelEnd_Sorting_2.Location = new Point(10, 168);
    this.labelEnd_Sorting_2.Name = "labelEnd_Sorting_2";
    this.labelEnd_Sorting_2.Size = new Size(61, 18);
    this.labelEnd_Sorting_2.TabIndex = 9;
    this.labelEnd_Sorting_2.Text = "Символ:";
    this.labelEnd_Sorting_2.TextAlign = ContentAlignment.TopRight;
    this.numericUpDown_Sorting_NumberEnd.Location = new Point(77, 132);
    this.numericUpDown_Sorting_NumberEnd.Maximum = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this.numericUpDown_Sorting_NumberEnd.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown_Sorting_NumberEnd.Name = "numericUpDown_Sorting_NumberEnd";
    this.numericUpDown_Sorting_NumberEnd.Size = new Size(53, 20);
    this.numericUpDown_Sorting_NumberEnd.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.numericUpDown_Sorting_NumberEnd, "Порядковый номер символа");
    this.numericUpDown_Sorting_NumberEnd.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.labelEnd_Sorting_1.Location = new Point(10, 134);
    this.labelEnd_Sorting_1.Name = "labelEnd_Sorting_1";
    this.labelEnd_Sorting_1.Size = new Size(61, 18);
    this.labelEnd_Sorting_1.TabIndex = 7;
    this.labelEnd_Sorting_1.Text = "Номер:";
    this.labelEnd_Sorting_1.TextAlign = ContentAlignment.TopRight;
    this.groupBox_Sorting_Do.Controls.Add((Control) this.radioButton_Sorting_DoSymbolNumbEnd);
    this.groupBox_Sorting_Do.Controls.Add((Control) this.radioButton_Sorting_DoSymbolNumb);
    this.groupBox_Sorting_Do.Controls.Add((Control) this.radioButton_Sorting_DoBukvyNumb);
    this.groupBox_Sorting_Do.Controls.Add((Control) this.radioButton_Sorting_DoEnd);
    this.groupBox_Sorting_Do.Location = new Point(6, 19);
    this.groupBox_Sorting_Do.Name = "groupBox_Sorting_Do";
    this.groupBox_Sorting_Do.Size = new Size(392, 96 /*0x60*/);
    this.groupBox_Sorting_Do.TabIndex = 0;
    this.groupBox_Sorting_Do.TabStop = false;
    this.groupBox_Sorting_Do.Text = "До:";
    this.radioButton_Sorting_DoSymbolNumbEnd.AutoSize = true;
    this.radioButton_Sorting_DoSymbolNumbEnd.Location = new Point(6, 72);
    this.radioButton_Sorting_DoSymbolNumbEnd.Name = "radioButton_Sorting_DoSymbolNumbEnd";
    this.radioButton_Sorting_DoSymbolNumbEnd.Size = new Size(211, 17);
    this.radioButton_Sorting_DoSymbolNumbEnd.TabIndex = 3;
    this.radioButton_Sorting_DoSymbolNumbEnd.Text = "Символа номер (с конца параметра)";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_DoSymbolNumbEnd, "До символа (символ установите ниже) №(номер выберите ниже) от конца строки текста");
    this.radioButton_Sorting_DoSymbolNumbEnd.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_DoSymbolNumbEnd.MouseClick += new MouseEventHandler(this.radioButton_Sorting_DoSymbolNumbEnd_MouseClick);
    this.radioButton_Sorting_DoSymbolNumb.AutoSize = true;
    this.radioButton_Sorting_DoSymbolNumb.Location = new Point(6, 53);
    this.radioButton_Sorting_DoSymbolNumb.Name = "radioButton_Sorting_DoSymbolNumb";
    this.radioButton_Sorting_DoSymbolNumb.Size = new Size(105, 17);
    this.radioButton_Sorting_DoSymbolNumb.TabIndex = 2;
    this.radioButton_Sorting_DoSymbolNumb.Text = "Символа номер";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_DoSymbolNumb, "До символа (символ установите ниже) №(номер выберите ниже) от начала строки текста");
    this.radioButton_Sorting_DoSymbolNumb.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_DoSymbolNumb.MouseClick += new MouseEventHandler(this.radioButton_Sorting_DoSymbolNumb_MouseClick);
    this.radioButton_Sorting_DoBukvyNumb.AutoSize = true;
    this.radioButton_Sorting_DoBukvyNumb.Location = new Point(6, 34);
    this.radioButton_Sorting_DoBukvyNumb.Name = "radioButton_Sorting_DoBukvyNumb";
    this.radioButton_Sorting_DoBukvyNumb.Size = new Size(92, 17);
    this.radioButton_Sorting_DoBukvyNumb.TabIndex = 1;
    this.radioButton_Sorting_DoBukvyNumb.Text = "Буквы номер";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_DoBukvyNumb, "До символа №(номер выберите ниже) от начала строки текста");
    this.radioButton_Sorting_DoBukvyNumb.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_DoBukvyNumb.MouseClick += new MouseEventHandler(this.radioButton_Sorting_DoBukvyNumb_MouseClick);
    this.radioButton_Sorting_DoEnd.AutoSize = true;
    this.radioButton_Sorting_DoEnd.Checked = true;
    this.radioButton_Sorting_DoEnd.Location = new Point(6, 15);
    this.radioButton_Sorting_DoEnd.Name = "radioButton_Sorting_DoEnd";
    this.radioButton_Sorting_DoEnd.Size = new Size(114, 17);
    this.radioButton_Sorting_DoEnd.TabIndex = 0;
    this.radioButton_Sorting_DoEnd.TabStop = true;
    this.radioButton_Sorting_DoEnd.Text = "Конца параметра";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_DoEnd, "До конца текста");
    this.radioButton_Sorting_DoEnd.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_DoEnd.MouseClick += new MouseEventHandler(this.radioButton_Sorting_DoEnd_MouseClick);
    this.groupBox_Sorting_Begin.Controls.Add((Control) this.comboBox_Sorting_SymbolBegin);
    this.groupBox_Sorting_Begin.Controls.Add((Control) this.labelBegin_Sorting_2);
    this.groupBox_Sorting_Begin.Controls.Add((Control) this.numericUpDown_Sorting_NumberBegin);
    this.groupBox_Sorting_Begin.Controls.Add((Control) this.labelBegin_Sorting_1);
    this.groupBox_Sorting_Begin.Controls.Add((Control) this.groupBox_Sorting_Ot);
    this.groupBox_Sorting_Begin.Location = new Point(460, 465);
    this.groupBox_Sorting_Begin.Name = "groupBox_Sorting_Begin";
    this.groupBox_Sorting_Begin.Size = new Size(392, 206);
    this.groupBox_Sorting_Begin.TabIndex = 12;
    this.groupBox_Sorting_Begin.TabStop = false;
    this.groupBox_Sorting_Begin.Text = "Начало подстроки";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_Begin, "Выбор, откуда начинать сравнения строки");
    this.comboBox_Sorting_SymbolBegin.FormattingEnabled = true;
    this.comboBox_Sorting_SymbolBegin.Items.AddRange(new object[5]
    {
      (object) "  (пробел)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)"
    });
    this.comboBox_Sorting_SymbolBegin.Location = new Point(77, 165);
    this.comboBox_Sorting_SymbolBegin.Name = "comboBox_Sorting_SymbolBegin";
    this.comboBox_Sorting_SymbolBegin.Size = new Size(121, 21);
    this.comboBox_Sorting_SymbolBegin.TabIndex = 2;
    this.labelBegin_Sorting_2.Location = new Point(10, 166);
    this.labelBegin_Sorting_2.Name = "labelBegin_Sorting_2";
    this.labelBegin_Sorting_2.Size = new Size(61, 18);
    this.labelBegin_Sorting_2.TabIndex = 4;
    this.labelBegin_Sorting_2.Text = "Символ:";
    this.labelBegin_Sorting_2.TextAlign = ContentAlignment.TopRight;
    this.numericUpDown_Sorting_NumberBegin.Location = new Point(77, 130);
    this.numericUpDown_Sorting_NumberBegin.Maximum = new Decimal(new int[4]
    {
      50,
      0,
      0,
      0
    });
    this.numericUpDown_Sorting_NumberBegin.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown_Sorting_NumberBegin.Name = "numericUpDown_Sorting_NumberBegin";
    this.numericUpDown_Sorting_NumberBegin.Size = new Size(53, 20);
    this.numericUpDown_Sorting_NumberBegin.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.numericUpDown_Sorting_NumberBegin, "Порядковый номер символа");
    this.numericUpDown_Sorting_NumberBegin.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.labelBegin_Sorting_1.Location = new Point(10, 132);
    this.labelBegin_Sorting_1.Name = "labelBegin_Sorting_1";
    this.labelBegin_Sorting_1.Size = new Size(61, 18);
    this.labelBegin_Sorting_1.TabIndex = 2;
    this.labelBegin_Sorting_1.Text = "Номер:";
    this.labelBegin_Sorting_1.TextAlign = ContentAlignment.TopRight;
    this.groupBox_Sorting_Ot.BackColor = Color.Transparent;
    this.groupBox_Sorting_Ot.Controls.Add((Control) this.radioButton_Sorting_OtSymbolNumbEnd);
    this.groupBox_Sorting_Ot.Controls.Add((Control) this.radioButton_Sorting_OtSymbolNumb);
    this.groupBox_Sorting_Ot.Controls.Add((Control) this.radioButton_Sorting_OtBukvyNumb);
    this.groupBox_Sorting_Ot.Controls.Add((Control) this.radioButton_Sorting_OtBegin);
    this.groupBox_Sorting_Ot.Location = new Point(6, 17);
    this.groupBox_Sorting_Ot.Name = "groupBox_Sorting_Ot";
    this.groupBox_Sorting_Ot.Size = new Size(380, 96 /*0x60*/);
    this.groupBox_Sorting_Ot.TabIndex = 0;
    this.groupBox_Sorting_Ot.TabStop = false;
    this.groupBox_Sorting_Ot.Text = "От:";
    this.toolTip1.SetToolTip((Control) this.groupBox_Sorting_Ot, "Выбор, откуда начинать сравнения строки");
    this.radioButton_Sorting_OtSymbolNumbEnd.AutoSize = true;
    this.radioButton_Sorting_OtSymbolNumbEnd.Location = new Point(6, 72);
    this.radioButton_Sorting_OtSymbolNumbEnd.Name = "radioButton_Sorting_OtSymbolNumbEnd";
    this.radioButton_Sorting_OtSymbolNumbEnd.Size = new Size(211, 17);
    this.radioButton_Sorting_OtSymbolNumbEnd.TabIndex = 3;
    this.radioButton_Sorting_OtSymbolNumbEnd.Text = "Символа номер (с конца параметра)";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_OtSymbolNumbEnd, "От символа (символ установите ниже) №(номер выберите ниже) от конца строки текста");
    this.radioButton_Sorting_OtSymbolNumbEnd.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_OtSymbolNumbEnd.MouseClick += new MouseEventHandler(this.radioButton_Sorting_OtSymbolNumbEnd_MouseClick);
    this.radioButton_Sorting_OtSymbolNumb.AutoSize = true;
    this.radioButton_Sorting_OtSymbolNumb.Location = new Point(6, 53);
    this.radioButton_Sorting_OtSymbolNumb.Name = "radioButton_Sorting_OtSymbolNumb";
    this.radioButton_Sorting_OtSymbolNumb.Size = new Size(105, 17);
    this.radioButton_Sorting_OtSymbolNumb.TabIndex = 2;
    this.radioButton_Sorting_OtSymbolNumb.Text = "Символа номер";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_OtSymbolNumb, "От символа (символ установите ниже) №(номер выберите ниже) от начала строки текста");
    this.radioButton_Sorting_OtSymbolNumb.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_OtSymbolNumb.MouseClick += new MouseEventHandler(this.radioButton_Sorting_OtSymbolNumb_MouseClick);
    this.radioButton_Sorting_OtBukvyNumb.AutoSize = true;
    this.radioButton_Sorting_OtBukvyNumb.Location = new Point(6, 34);
    this.radioButton_Sorting_OtBukvyNumb.Name = "radioButton_Sorting_OtBukvyNumb";
    this.radioButton_Sorting_OtBukvyNumb.Size = new Size(92, 17);
    this.radioButton_Sorting_OtBukvyNumb.TabIndex = 1;
    this.radioButton_Sorting_OtBukvyNumb.Text = "Буквы номер";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_OtBukvyNumb, "От символа №(номер выберите ниже) от начала строки текста");
    this.radioButton_Sorting_OtBukvyNumb.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_OtBukvyNumb.MouseClick += new MouseEventHandler(this.radioButton_Sorting_OtBukvyNumb_MouseClick);
    this.radioButton_Sorting_OtBegin.AutoSize = true;
    this.radioButton_Sorting_OtBegin.Checked = true;
    this.radioButton_Sorting_OtBegin.Location = new Point(6, 15);
    this.radioButton_Sorting_OtBegin.Name = "radioButton_Sorting_OtBegin";
    this.radioButton_Sorting_OtBegin.Size = new Size(120, 17);
    this.radioButton_Sorting_OtBegin.TabIndex = 0;
    this.radioButton_Sorting_OtBegin.TabStop = true;
    this.radioButton_Sorting_OtBegin.Text = "Начала параметра";
    this.toolTip1.SetToolTip((Control) this.radioButton_Sorting_OtBegin, "От начала текста");
    this.radioButton_Sorting_OtBegin.UseVisualStyleBackColor = true;
    this.radioButton_Sorting_OtBegin.MouseClick += new MouseEventHandler(this.radioButton_Sorting_OtBegin_MouseClick);
    this.dataGridView_Sorting.AllowUserToAddRows = false;
    this.dataGridView_Sorting.AllowUserToDeleteRows = false;
    this.dataGridView_Sorting.AllowUserToResizeColumns = false;
    this.dataGridView_Sorting.AllowUserToResizeRows = false;
    this.dataGridView_Sorting.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    gridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle7.BackColor = SystemColors.Control;
    gridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle7.ForeColor = SystemColors.WindowText;
    gridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle7.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Sorting.ColumnHeadersDefaultCellStyle = gridViewCellStyle7;
    this.dataGridView_Sorting.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_Sorting.Columns.AddRange((DataGridViewColumn) this.ImgColumn, (DataGridViewColumn) this.ColumnAttribut, (DataGridViewColumn) this.ColumnOt, (DataGridViewColumn) this.ColumnDo, (DataGridViewColumn) this.ColumnSravnenie, (DataGridViewColumn) this.ColumnPustye);
    gridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle8.BackColor = SystemColors.Window;
    gridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle8.ForeColor = SystemColors.ControlText;
    gridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle8.WrapMode = DataGridViewTriState.False;
    this.dataGridView_Sorting.DefaultCellStyle = gridViewCellStyle8;
    this.dataGridView_Sorting.Location = new Point(460, 6);
    this.dataGridView_Sorting.MultiSelect = false;
    this.dataGridView_Sorting.Name = "dataGridView_Sorting";
    this.dataGridView_Sorting.ReadOnly = true;
    gridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle9.BackColor = SystemColors.Control;
    gridViewCellStyle9.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle9.ForeColor = SystemColors.WindowText;
    gridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle9.WrapMode = DataGridViewTriState.True;
    this.dataGridView_Sorting.RowHeadersDefaultCellStyle = gridViewCellStyle9;
    this.dataGridView_Sorting.RowHeadersVisible = false;
    this.dataGridView_Sorting.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_Sorting.Size = new Size(1069, 440);
    this.dataGridView_Sorting.TabIndex = 11;
    this.toolTip1.SetToolTip((Control) this.dataGridView_Sorting, "Порядок сортировки каждого раздела");
    this.dataGridView_Sorting.CellEnter += new DataGridViewCellEventHandler(this.dataGridView_Sorting_CellEnter);
    this.dataGridView_Sorting.KeyDown += new KeyEventHandler(this.dataGridView_Sorting_KeyDown);
    this.ImgColumn.FillWeight = 30f;
    this.ImgColumn.HeaderText = "";
    this.ImgColumn.Name = "ImgColumn";
    this.ImgColumn.ReadOnly = true;
    this.ImgColumn.Resizable = DataGridViewTriState.True;
    this.ImgColumn.Width = 20;
    this.ColumnAttribut.HeaderText = "Атрибут";
    this.ColumnAttribut.Name = "ColumnAttribut";
    this.ColumnAttribut.ReadOnly = true;
    this.ColumnAttribut.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColumnAttribut.Width = 360;
    this.ColumnOt.HeaderText = "От";
    this.ColumnOt.Name = "ColumnOt";
    this.ColumnOt.ReadOnly = true;
    this.ColumnOt.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColumnOt.Width = 210;
    this.ColumnDo.HeaderText = "До";
    this.ColumnDo.Name = "ColumnDo";
    this.ColumnDo.ReadOnly = true;
    this.ColumnDo.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColumnDo.Width = 210;
    this.ColumnSravnenie.HeaderText = "Сравнение";
    this.ColumnSravnenie.Name = "ColumnSravnenie";
    this.ColumnSravnenie.ReadOnly = true;
    this.ColumnSravnenie.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColumnSravnenie.Width = 130;
    this.ColumnPustye.HeaderText = "Пустые строки";
    this.ColumnPustye.Name = "ColumnPustye";
    this.ColumnPustye.ReadOnly = true;
    this.ColumnPustye.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.ColumnPustye.Width = 130;
    this.groupBox_Sorting_AttribVedRec1.Controls.Add((Control) this.listBox_Sorting_AttribVedRec);
    this.groupBox_Sorting_AttribVedRec1.Location = new Point(3, 465);
    this.groupBox_Sorting_AttribVedRec1.Name = "groupBox_Sorting_AttribVedRec1";
    this.groupBox_Sorting_AttribVedRec1.Size = new Size(451, 249);
    this.groupBox_Sorting_AttribVedRec1.TabIndex = 10;
    this.groupBox_Sorting_AttribVedRec1.TabStop = false;
    this.groupBox_Sorting_AttribVedRec1.Text = "Атрибуты записей ведомостей";
    this.listBox_Sorting_AttribVedRec.Dock = DockStyle.Fill;
    this.listBox_Sorting_AttribVedRec.FormattingEnabled = true;
    this.listBox_Sorting_AttribVedRec.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Sorting_AttribVedRec.Name = "listBox_Sorting_AttribVedRec";
    this.listBox_Sorting_AttribVedRec.Size = new Size(445, 230);
    this.listBox_Sorting_AttribVedRec.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.listBox_Sorting_AttribVedRec, "Атрибуты записей, характерные для ведомостей");
    this.listBox_Sorting_AttribVedRec.MouseClick += new MouseEventHandler(this.listBox_Sorting_AttribVedRec_MouseClick);
    this.tabPage_Merge.Controls.Add((Control) this.button_Merge_Del);
    this.tabPage_Merge.Controls.Add((Control) this.button_Merge_Add);
    this.tabPage_Merge.Controls.Add((Control) this.groupBox_Merge_List_Merge_Usl2);
    this.tabPage_Merge.Controls.Add((Control) this.groupBox_Merge_AttribVedRec1);
    this.tabPage_Merge.Controls.Add((Control) this.groupBox_Merge_List_Ved_Id);
    this.tabPage_Merge.Location = new Point(4, 22);
    this.tabPage_Merge.Name = "tabPage_Merge";
    this.tabPage_Merge.Padding = new Padding(3);
    this.tabPage_Merge.Size = new Size(1576, 731);
    this.tabPage_Merge.TabIndex = 9;
    this.tabPage_Merge.Text = "Объединение записей";
    this.tabPage_Merge.UseVisualStyleBackColor = true;
    this.button_Merge_Del.Image = (Image) componentResourceManager.GetObject("button_Merge_Del.Image");
    this.button_Merge_Del.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Merge_Del.Location = new Point(479, 165);
    this.button_Merge_Del.Name = "button_Merge_Del";
    this.button_Merge_Del.Size = new Size(121, 27);
    this.button_Merge_Del.TabIndex = 40;
    this.button_Merge_Del.Text = "Удалить";
    this.button_Merge_Del.UseVisualStyleBackColor = true;
    this.button_Merge_Del.Click += new EventHandler(this.button_Merge_Del_Click);
    this.button_Merge_Add.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Merge_Add.Image = (Image) componentResourceManager.GetObject("button_Merge_Add.Image");
    this.button_Merge_Add.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Merge_Add.Location = new Point(479, 115);
    this.button_Merge_Add.Name = "button_Merge_Add";
    this.button_Merge_Add.Size = new Size(121, 27);
    this.button_Merge_Add.TabIndex = 39;
    this.button_Merge_Add.Text = "Добавить";
    this.button_Merge_Add.UseVisualStyleBackColor = true;
    this.button_Merge_Add.Click += new EventHandler(this.button_Merge_Add_Click);
    this.groupBox_Merge_List_Merge_Usl2.AutoSize = true;
    this.groupBox_Merge_List_Merge_Usl2.Controls.Add((Control) this.listBox_Merge_List_Merge_Usl2);
    this.groupBox_Merge_List_Merge_Usl2.Location = new Point(630, 6);
    this.groupBox_Merge_List_Merge_Usl2.Name = "groupBox_Merge_List_Merge_Usl2";
    this.groupBox_Merge_List_Merge_Usl2.Size = new Size(445, 439);
    this.groupBox_Merge_List_Merge_Usl2.TabIndex = 38;
    this.groupBox_Merge_List_Merge_Usl2.TabStop = false;
    this.groupBox_Merge_List_Merge_Usl2.Text = "Список атрибутов для сравнения";
    this.toolTip1.SetToolTip((Control) this.groupBox_Merge_List_Merge_Usl2, "Если тексты в данных атрибутах одинаковые, то записи объединяются");
    this.listBox_Merge_List_Merge_Usl2.Dock = DockStyle.Fill;
    this.listBox_Merge_List_Merge_Usl2.FormattingEnabled = true;
    this.listBox_Merge_List_Merge_Usl2.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Merge_List_Merge_Usl2.Name = "listBox_Merge_List_Merge_Usl2";
    this.listBox_Merge_List_Merge_Usl2.Size = new Size(439, 420);
    this.listBox_Merge_List_Merge_Usl2.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.listBox_Merge_List_Merge_Usl2, "Если тексты в данных атрибутах одинаковые, то записи объединяются");
    this.listBox_Merge_List_Merge_Usl2.SelectedIndexChanged += new EventHandler(this.listBox_Merge_List_Merge_Usl2_SelectedIndexChanged);
    this.listBox_Merge_List_Merge_Usl2.KeyDown += new KeyEventHandler(this.listBox_Merge_List_Merge_Usl2_KeyDown);
    this.groupBox_Merge_AttribVedRec1.Controls.Add((Control) this.listBox_Merge_AttribVedRec);
    this.groupBox_Merge_AttribVedRec1.Location = new Point(3, 465);
    this.groupBox_Merge_AttribVedRec1.Name = "groupBox_Merge_AttribVedRec1";
    this.groupBox_Merge_AttribVedRec1.Size = new Size(445, 249);
    this.groupBox_Merge_AttribVedRec1.TabIndex = 37;
    this.groupBox_Merge_AttribVedRec1.TabStop = false;
    this.groupBox_Merge_AttribVedRec1.Text = "Атрибуты записей ведомостей";
    this.listBox_Merge_AttribVedRec.Dock = DockStyle.Fill;
    this.listBox_Merge_AttribVedRec.FormattingEnabled = true;
    this.listBox_Merge_AttribVedRec.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Merge_AttribVedRec.Name = "listBox_Merge_AttribVedRec";
    this.listBox_Merge_AttribVedRec.Size = new Size(439, 230);
    this.listBox_Merge_AttribVedRec.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.listBox_Merge_AttribVedRec, "Атрибуты записей, характерные для ведомостей");
    this.listBox_Merge_AttribVedRec.MouseClick += new MouseEventHandler(this.listBox_Merge_AttribVedRec_MouseClick);
    this.groupBox_Merge_List_Ved_Id.Controls.Add((Control) this.listBox_Merge_List_Ved_Id);
    this.groupBox_Merge_List_Ved_Id.Location = new Point(3, 6);
    this.groupBox_Merge_List_Ved_Id.Name = "groupBox_Merge_List_Ved_Id";
    this.groupBox_Merge_List_Ved_Id.Size = new Size(445, 442);
    this.groupBox_Merge_List_Ved_Id.TabIndex = 36;
    this.groupBox_Merge_List_Ved_Id.TabStop = false;
    this.groupBox_Merge_List_Ved_Id.Text = "Атрибуты, собираемые из спецификаций";
    this.toolTip1.SetToolTip((Control) this.groupBox_Merge_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Merge_List_Ved_Id.Dock = DockStyle.Fill;
    this.listBox_Merge_List_Ved_Id.FormattingEnabled = true;
    this.listBox_Merge_List_Ved_Id.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Merge_List_Ved_Id.Name = "listBox_Merge_List_Ved_Id";
    this.listBox_Merge_List_Ved_Id.Size = new Size(439, 423);
    this.listBox_Merge_List_Ved_Id.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Merge_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Merge_List_Ved_Id.MouseClick += new MouseEventHandler(this.listBox_Merge_List_Ved_Id_MouseClick);
    this.tabPage_Razdels.AutoScroll = true;
    this.tabPage_Razdels.Controls.Add((Control) this.groupBox_Conformity_Name_Page_for_Razdel);
    this.tabPage_Razdels.Controls.Add((Control) this.buttonAdd_PodRazdel);
    this.tabPage_Razdels.Controls.Add((Control) this.buttonDelete_PodRazdel);
    this.tabPage_Razdels.Controls.Add((Control) this.checkBox_Razdel_PodRazdel);
    this.tabPage_Razdels.Controls.Add((Control) this.Razdels_groupBoxListPodRazdelov);
    this.tabPage_Razdels.Controls.Add((Control) this.Razdels_groupBoxListRazdelov);
    this.tabPage_Razdels.Controls.Add((Control) this.buttonAdd_Razdel);
    this.tabPage_Razdels.Controls.Add((Control) this.buttonDelete_Razdel);
    this.tabPage_Razdels.Location = new Point(4, 22);
    this.tabPage_Razdels.Name = "tabPage_Razdels";
    this.tabPage_Razdels.Padding = new Padding(3);
    this.tabPage_Razdels.Size = new Size(1576, 731);
    this.tabPage_Razdels.TabIndex = 3;
    this.tabPage_Razdels.Text = "Разделы";
    this.tabPage_Razdels.UseVisualStyleBackColor = true;
    this.groupBox_Conformity_Name_Page_for_Razdel.Controls.Add((Control) this.button_Add_NamePage);
    this.groupBox_Conformity_Name_Page_for_Razdel.Controls.Add((Control) this.groupBox_NamePage);
    this.groupBox_Conformity_Name_Page_for_Razdel.Controls.Add((Control) this.groupBox_RazdelVedAndNamePage);
    this.groupBox_Conformity_Name_Page_for_Razdel.Location = new Point(4, 410);
    this.groupBox_Conformity_Name_Page_for_Razdel.Name = "groupBox_Conformity_Name_Page_for_Razdel";
    this.groupBox_Conformity_Name_Page_for_Razdel.Size = new Size(1191, 307);
    this.groupBox_Conformity_Name_Page_for_Razdel.TabIndex = 27;
    this.groupBox_Conformity_Name_Page_for_Razdel.TabStop = false;
    this.groupBox_Conformity_Name_Page_for_Razdel.Text = "Настройка соответствия раздела документа и страницы шаблона";
    this.groupBox_Conformity_Name_Page_for_Razdel.Visible = false;
    this.button_Add_NamePage.Image = (Image) Resources.arrow_left_green;
    this.button_Add_NamePage.Location = new Point(673, 132);
    this.button_Add_NamePage.Name = "button_Add_NamePage";
    this.button_Add_NamePage.Size = new Size(120, 27);
    this.button_Add_NamePage.TabIndex = 30;
    this.toolTip1.SetToolTip((Control) this.button_Add_NamePage, "Занести имя страницы в список разделов");
    this.button_Add_NamePage.UseVisualStyleBackColor = true;
    this.button_Add_NamePage.Click += new EventHandler(this.button_Add_NamePage_Click);
    this.groupBox_NamePage.Controls.Add((Control) this.dataGridView_NamePage);
    this.groupBox_NamePage.Location = new Point(800, 19);
    this.groupBox_NamePage.Name = "groupBox_NamePage";
    this.groupBox_NamePage.Size = new Size(380, 275);
    this.groupBox_NamePage.TabIndex = 28;
    this.groupBox_NamePage.TabStop = false;
    this.groupBox_NamePage.Text = "Имена страниц шаблона";
    this.dataGridView_NamePage.AllowDrop = true;
    this.dataGridView_NamePage.AllowUserToAddRows = false;
    this.dataGridView_NamePage.AllowUserToDeleteRows = false;
    this.dataGridView_NamePage.AllowUserToResizeColumns = false;
    this.dataGridView_NamePage.AllowUserToResizeRows = false;
    this.dataGridView_NamePage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    gridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle10.BackColor = SystemColors.Control;
    gridViewCellStyle10.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle10.ForeColor = SystemColors.WindowText;
    gridViewCellStyle10.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle10.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle10.WrapMode = DataGridViewTriState.True;
    this.dataGridView_NamePage.ColumnHeadersDefaultCellStyle = gridViewCellStyle10;
    this.dataGridView_NamePage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_NamePage.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn14);
    gridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle11.BackColor = SystemColors.Window;
    gridViewCellStyle11.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle11.ForeColor = SystemColors.ControlText;
    gridViewCellStyle11.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle11.WrapMode = DataGridViewTriState.False;
    this.dataGridView_NamePage.DefaultCellStyle = gridViewCellStyle11;
    this.dataGridView_NamePage.Dock = DockStyle.Fill;
    this.dataGridView_NamePage.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_NamePage.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_NamePage.MultiSelect = false;
    this.dataGridView_NamePage.Name = "dataGridView_NamePage";
    this.dataGridView_NamePage.ReadOnly = true;
    gridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle12.BackColor = SystemColors.Control;
    gridViewCellStyle12.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle12.ForeColor = SystemColors.WindowText;
    gridViewCellStyle12.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle12.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle12.WrapMode = DataGridViewTriState.True;
    this.dataGridView_NamePage.RowHeadersDefaultCellStyle = gridViewCellStyle12;
    this.dataGridView_NamePage.RowHeadersVisible = false;
    this.dataGridView_NamePage.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_NamePage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_NamePage.Size = new Size(374, 256 /*0x0100*/);
    this.dataGridView_NamePage.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.dataGridView_NamePage, "Номер и наименование разделов ведомости");
    this.dataGridViewTextBoxColumn14.HeaderText = "Страницы шаблона";
    this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
    this.dataGridViewTextBoxColumn14.ReadOnly = true;
    this.dataGridViewTextBoxColumn14.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn14.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn14.Width = 350;
    this.groupBox_RazdelVedAndNamePage.Controls.Add((Control) this.dataGridView_RazdelVedAndNamePage);
    this.groupBox_RazdelVedAndNamePage.Location = new Point(6, 19);
    this.groupBox_RazdelVedAndNamePage.Name = "groupBox_RazdelVedAndNamePage";
    this.groupBox_RazdelVedAndNamePage.Size = new Size(660, 275);
    this.groupBox_RazdelVedAndNamePage.TabIndex = 27;
    this.groupBox_RazdelVedAndNamePage.TabStop = false;
    this.groupBox_RazdelVedAndNamePage.Text = "Раздела документа и страница шаблона";
    this.dataGridView_RazdelVedAndNamePage.AllowDrop = true;
    this.dataGridView_RazdelVedAndNamePage.AllowUserToAddRows = false;
    this.dataGridView_RazdelVedAndNamePage.AllowUserToDeleteRows = false;
    this.dataGridView_RazdelVedAndNamePage.AllowUserToResizeColumns = false;
    this.dataGridView_RazdelVedAndNamePage.AllowUserToResizeRows = false;
    this.dataGridView_RazdelVedAndNamePage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    gridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle13.BackColor = SystemColors.Control;
    gridViewCellStyle13.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle13.ForeColor = SystemColors.WindowText;
    gridViewCellStyle13.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle13.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle13.WrapMode = DataGridViewTriState.True;
    this.dataGridView_RazdelVedAndNamePage.ColumnHeadersDefaultCellStyle = gridViewCellStyle13;
    this.dataGridView_RazdelVedAndNamePage.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_RazdelVedAndNamePage.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewTextBoxColumn13);
    gridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle14.BackColor = SystemColors.Window;
    gridViewCellStyle14.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle14.ForeColor = SystemColors.ControlText;
    gridViewCellStyle14.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle14.WrapMode = DataGridViewTriState.False;
    this.dataGridView_RazdelVedAndNamePage.DefaultCellStyle = gridViewCellStyle14;
    this.dataGridView_RazdelVedAndNamePage.Dock = DockStyle.Fill;
    this.dataGridView_RazdelVedAndNamePage.EditMode = DataGridViewEditMode.EditProgrammatically;
    this.dataGridView_RazdelVedAndNamePage.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_RazdelVedAndNamePage.MultiSelect = false;
    this.dataGridView_RazdelVedAndNamePage.Name = "dataGridView_RazdelVedAndNamePage";
    this.dataGridView_RazdelVedAndNamePage.ReadOnly = true;
    gridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle15.BackColor = SystemColors.Control;
    gridViewCellStyle15.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle15.ForeColor = SystemColors.WindowText;
    gridViewCellStyle15.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle15.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle15.WrapMode = DataGridViewTriState.True;
    this.dataGridView_RazdelVedAndNamePage.RowHeadersDefaultCellStyle = gridViewCellStyle15;
    this.dataGridView_RazdelVedAndNamePage.RowHeadersVisible = false;
    this.dataGridView_RazdelVedAndNamePage.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_RazdelVedAndNamePage.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    this.dataGridView_RazdelVedAndNamePage.Size = new Size(654, 256 /*0x0100*/);
    this.dataGridView_RazdelVedAndNamePage.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.dataGridView_RazdelVedAndNamePage, "Номер и наименование разделов ведомости");
    this.dataGridViewTextBoxColumn12.HeaderText = "Наименование раздела";
    this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
    this.dataGridViewTextBoxColumn12.ReadOnly = true;
    this.dataGridViewTextBoxColumn12.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn12.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn12.Width = 240 /*0xF0*/;
    this.dataGridViewTextBoxColumn13.HeaderText = "Страницы шаблона";
    this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
    this.dataGridViewTextBoxColumn13.ReadOnly = true;
    this.dataGridViewTextBoxColumn13.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn13.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn13.Width = 390;
    this.buttonAdd_PodRazdel.AccessibleRole = AccessibleRole.OutlineButton;
    this.buttonAdd_PodRazdel.Image = (Image) componentResourceManager.GetObject("buttonAdd_PodRazdel.Image");
    this.buttonAdd_PodRazdel.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonAdd_PodRazdel.Location = new Point(938, 355);
    this.buttonAdd_PodRazdel.Name = "buttonAdd_PodRazdel";
    this.buttonAdd_PodRazdel.Size = new Size(121, 27);
    this.buttonAdd_PodRazdel.TabIndex = 25;
    this.buttonAdd_PodRazdel.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.buttonAdd_PodRazdel, "Добавить строку выше текущей");
    this.buttonAdd_PodRazdel.UseVisualStyleBackColor = true;
    this.buttonAdd_PodRazdel.Click += new EventHandler(this.buttonAdd_PodRazdel_Click);
    this.buttonDelete_PodRazdel.Image = (Image) componentResourceManager.GetObject("buttonDelete_PodRazdel.Image");
    this.buttonDelete_PodRazdel.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonDelete_PodRazdel.Location = new Point(1079, 355);
    this.buttonDelete_PodRazdel.Name = "buttonDelete_PodRazdel";
    this.buttonDelete_PodRazdel.Size = new Size(121, 27);
    this.buttonDelete_PodRazdel.TabIndex = 24;
    this.buttonDelete_PodRazdel.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.buttonDelete_PodRazdel, "Удалить текущую строку");
    this.buttonDelete_PodRazdel.UseVisualStyleBackColor = true;
    this.buttonDelete_PodRazdel.Click += new EventHandler(this.buttonDelete_PodRazdel_Click);
    this.checkBox_Razdel_PodRazdel.AutoSize = true;
    this.checkBox_Razdel_PodRazdel.Location = new Point(520, 315);
    this.checkBox_Razdel_PodRazdel.Name = "checkBox_Razdel_PodRazdel";
    this.checkBox_Razdel_PodRazdel.Size = new Size(90, 17);
    this.checkBox_Razdel_PodRazdel.TabIndex = 23;
    this.checkBox_Razdel_PodRazdel.Text = "Подразделы";
    this.toolTip1.SetToolTip((Control) this.checkBox_Razdel_PodRazdel, "Есть ли в данном разделе подразделы");
    this.checkBox_Razdel_PodRazdel.UseVisualStyleBackColor = true;
    this.checkBox_Razdel_PodRazdel.CheckedChanged += new EventHandler(this.checkBox_Razdel_PodRazdel_CheckedChanged);
    this.Razdels_groupBoxListPodRazdelov.AutoSize = true;
    this.Razdels_groupBoxListPodRazdelov.Controls.Add((Control) this.Razdels_dataGridViewListPodRazdels);
    this.Razdels_groupBoxListPodRazdelov.Location = new Point(698, 6);
    this.Razdels_groupBoxListPodRazdelov.Name = "Razdels_groupBoxListPodRazdelov";
    this.Razdels_groupBoxListPodRazdelov.Size = new Size(500, 330);
    this.Razdels_groupBoxListPodRazdelov.TabIndex = 22;
    this.Razdels_groupBoxListPodRazdelov.TabStop = false;
    this.Razdels_groupBoxListPodRazdelov.Text = "Список подразделов";
    this.Razdels_groupBoxListPodRazdelov.Visible = false;
    this.Razdels_dataGridViewListPodRazdels.AllowUserToResizeColumns = false;
    this.Razdels_dataGridViewListPodRazdels.AllowUserToResizeRows = false;
    gridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle16.BackColor = SystemColors.Control;
    gridViewCellStyle16.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle16.ForeColor = SystemColors.WindowText;
    gridViewCellStyle16.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle16.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle16.WrapMode = DataGridViewTriState.True;
    this.Razdels_dataGridViewListPodRazdels.ColumnHeadersDefaultCellStyle = gridViewCellStyle16;
    this.Razdels_dataGridViewListPodRazdels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.Razdels_dataGridViewListPodRazdels.Columns.AddRange((DataGridViewColumn) this.PodRazdels_Column1, (DataGridViewColumn) this.PodRazdels_Column2);
    gridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle17.BackColor = SystemColors.Window;
    gridViewCellStyle17.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle17.ForeColor = SystemColors.ControlText;
    gridViewCellStyle17.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle17.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle17.WrapMode = DataGridViewTriState.False;
    this.Razdels_dataGridViewListPodRazdels.DefaultCellStyle = gridViewCellStyle17;
    this.Razdels_dataGridViewListPodRazdels.Dock = DockStyle.Fill;
    this.Razdels_dataGridViewListPodRazdels.Location = new Point(3, 16 /*0x10*/);
    this.Razdels_dataGridViewListPodRazdels.Name = "Razdels_dataGridViewListPodRazdels";
    gridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle18.BackColor = SystemColors.Control;
    gridViewCellStyle18.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle18.ForeColor = SystemColors.WindowText;
    gridViewCellStyle18.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle18.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle18.WrapMode = DataGridViewTriState.True;
    this.Razdels_dataGridViewListPodRazdels.RowHeadersDefaultCellStyle = gridViewCellStyle18;
    this.Razdels_dataGridViewListPodRazdels.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.Razdels_dataGridViewListPodRazdels.Size = new Size(494, 311);
    this.Razdels_dataGridViewListPodRazdels.StandardTab = true;
    this.Razdels_dataGridViewListPodRazdels.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.Razdels_dataGridViewListPodRazdels, "Номер и наименование подразделов ведомости");
    this.Razdels_dataGridViewListPodRazdels.CellClick += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListPodRazdels_CellClick);
    this.Razdels_dataGridViewListPodRazdels.CellEnter += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListPodRazdels_CellEnter);
    this.Razdels_dataGridViewListPodRazdels.CellValidating += new DataGridViewCellValidatingEventHandler(this.Razdels_dataGridViewListPodRazdels_CellValidating);
    this.Razdels_dataGridViewListPodRazdels.CellValueChanged += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListPodRazdels_CellValueChanged);
    this.Razdels_dataGridViewListPodRazdels.RowsAdded += new DataGridViewRowsAddedEventHandler(this.Razdels_dataGridViewListPodRazdels_RowsAdded);
    this.Razdels_dataGridViewListPodRazdels.RowValidating += new DataGridViewCellCancelEventHandler(this.Razdels_dataGridViewListPodRazdels_RowValidating);
    this.PodRazdels_Column1.HeaderText = "Номер";
    this.PodRazdels_Column1.Name = "PodRazdels_Column1";
    this.PodRazdels_Column1.Resizable = DataGridViewTriState.False;
    this.PodRazdels_Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.PodRazdels_Column1.Width = 60;
    this.PodRazdels_Column2.HeaderText = "Наименование подраздела";
    this.PodRazdels_Column2.Name = "PodRazdels_Column2";
    this.PodRazdels_Column2.Resizable = DataGridViewTriState.False;
    this.PodRazdels_Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.PodRazdels_Column2.Width = 390;
    this.Razdels_groupBoxListRazdelov.AutoSize = true;
    this.Razdels_groupBoxListRazdelov.Controls.Add((Control) this.Razdels_dataGridViewListRazdels);
    this.Razdels_groupBoxListRazdelov.Location = new Point(1, 6);
    this.Razdels_groupBoxListRazdelov.Name = "Razdels_groupBoxListRazdelov";
    this.Razdels_groupBoxListRazdelov.Size = new Size(500, 330);
    this.Razdels_groupBoxListRazdelov.TabIndex = 5;
    this.Razdels_groupBoxListRazdelov.TabStop = false;
    this.Razdels_groupBoxListRazdelov.Text = "Список разделов";
    this.Razdels_dataGridViewListRazdels.AllowUserToResizeColumns = false;
    this.Razdels_dataGridViewListRazdels.AllowUserToResizeRows = false;
    gridViewCellStyle19.NullValue = (object) null;
    this.Razdels_dataGridViewListRazdels.AlternatingRowsDefaultCellStyle = gridViewCellStyle19;
    gridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle20.BackColor = SystemColors.Control;
    gridViewCellStyle20.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle20.ForeColor = SystemColors.WindowText;
    gridViewCellStyle20.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle20.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle20.WrapMode = DataGridViewTriState.True;
    this.Razdels_dataGridViewListRazdels.ColumnHeadersDefaultCellStyle = gridViewCellStyle20;
    this.Razdels_dataGridViewListRazdels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.Razdels_dataGridViewListRazdels.Columns.AddRange((DataGridViewColumn) this.Razdels_Column1, (DataGridViewColumn) this.Razdels_Column2);
    gridViewCellStyle21.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle21.BackColor = SystemColors.Window;
    gridViewCellStyle21.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle21.ForeColor = SystemColors.ControlText;
    gridViewCellStyle21.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle21.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle21.WrapMode = DataGridViewTriState.False;
    this.Razdels_dataGridViewListRazdels.DefaultCellStyle = gridViewCellStyle21;
    this.Razdels_dataGridViewListRazdels.Dock = DockStyle.Fill;
    this.Razdels_dataGridViewListRazdels.Location = new Point(3, 16 /*0x10*/);
    this.Razdels_dataGridViewListRazdels.Name = "Razdels_dataGridViewListRazdels";
    gridViewCellStyle22.Alignment = DataGridViewContentAlignment.MiddleLeft;
    gridViewCellStyle22.BackColor = SystemColors.Control;
    gridViewCellStyle22.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    gridViewCellStyle22.ForeColor = SystemColors.WindowText;
    gridViewCellStyle22.SelectionBackColor = SystemColors.Highlight;
    gridViewCellStyle22.SelectionForeColor = SystemColors.HighlightText;
    gridViewCellStyle22.WrapMode = DataGridViewTriState.True;
    this.Razdels_dataGridViewListRazdels.RowHeadersDefaultCellStyle = gridViewCellStyle22;
    this.Razdels_dataGridViewListRazdels.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.Razdels_dataGridViewListRazdels.Size = new Size(494, 311);
    this.Razdels_dataGridViewListRazdels.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.Razdels_dataGridViewListRazdels, "Номер и наименование разделов ведомости");
    this.Razdels_dataGridViewListRazdels.CellClick += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListRazdels_CellClick);
    this.Razdels_dataGridViewListRazdels.CellEnter += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListRazdels_CellEnter);
    this.Razdels_dataGridViewListRazdels.CellValidating += new DataGridViewCellValidatingEventHandler(this.Razdels_dataGridViewListRazdels_CellValidating);
    this.Razdels_dataGridViewListRazdels.CellValueChanged += new DataGridViewCellEventHandler(this.Razdels_dataGridViewListRazdels_CellValueChanged);
    this.Razdels_dataGridViewListRazdels.RowsAdded += new DataGridViewRowsAddedEventHandler(this.Razdels_dataGridViewListRazdels_RowsAdded);
    this.Razdels_dataGridViewListRazdels.RowValidating += new DataGridViewCellCancelEventHandler(this.Razdels_dataGridViewListRazdels_RowValidating);
    this.Razdels_dataGridViewListRazdels.KeyDown += new KeyEventHandler(this.Razdels_dataGridViewListRazdels_KeyDown);
    this.Razdels_Column1.HeaderText = "Номер";
    this.Razdels_Column1.Name = "Razdels_Column1";
    this.Razdels_Column1.Resizable = DataGridViewTriState.False;
    this.Razdels_Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Razdels_Column1.Width = 60;
    this.Razdels_Column2.HeaderText = "Наименование раздела";
    this.Razdels_Column2.Name = "Razdels_Column2";
    this.Razdels_Column2.Resizable = DataGridViewTriState.False;
    this.Razdels_Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Razdels_Column2.Width = 390;
    this.buttonAdd_Razdel.AccessibleRole = AccessibleRole.OutlineButton;
    this.buttonAdd_Razdel.AutoSize = true;
    this.buttonAdd_Razdel.Image = (Image) componentResourceManager.GetObject("buttonAdd_Razdel.Image");
    this.buttonAdd_Razdel.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonAdd_Razdel.Location = new Point(239, 355);
    this.buttonAdd_Razdel.Name = "buttonAdd_Razdel";
    this.buttonAdd_Razdel.Size = new Size(121, 27);
    this.buttonAdd_Razdel.TabIndex = 21;
    this.buttonAdd_Razdel.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.buttonAdd_Razdel, "Добавить строку выше текущей");
    this.buttonAdd_Razdel.UseVisualStyleBackColor = true;
    this.buttonAdd_Razdel.Click += new EventHandler(this.buttonAdd_Razdel_Click);
    this.buttonDelete_Razdel.Image = (Image) componentResourceManager.GetObject("buttonDelete_Razdel.Image");
    this.buttonDelete_Razdel.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonDelete_Razdel.Location = new Point(380, 355);
    this.buttonDelete_Razdel.Name = "buttonDelete_Razdel";
    this.buttonDelete_Razdel.Size = new Size(121, 27);
    this.buttonDelete_Razdel.TabIndex = 20;
    this.buttonDelete_Razdel.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.buttonDelete_Razdel, "Удалить текущую строку");
    this.buttonDelete_Razdel.UseVisualStyleBackColor = true;
    this.buttonDelete_Razdel.Click += new EventHandler(this.buttonDelete_Razdel_Click);
    this.tabPage_Zagolovki.AutoScroll = true;
    this.tabPage_Zagolovki.BackColor = Color.Transparent;
    this.tabPage_Zagolovki.Controls.Add((Control) this.checkBox_LocationZagolovki);
    this.tabPage_Zagolovki.Controls.Add((Control) this.checkBox_UserZagolovki);
    this.tabPage_Zagolovki.Controls.Add((Control) this.groupBox_Include_Name);
    this.tabPage_Zagolovki.Controls.Add((Control) this.groupBox_Zagolovki_List_Ved_Id);
    this.tabPage_Zagolovki.Controls.Add((Control) this.groupBox_Zagolovki_AttribVedRec1);
    this.tabPage_Zagolovki.Controls.Add((Control) this.groupBox_Zagolovki_TypeCompare);
    this.tabPage_Zagolovki.Controls.Add((Control) this.button_Zagolovki_FromList);
    this.tabPage_Zagolovki.Controls.Add((Control) this.label_NoZgolovki);
    this.tabPage_Zagolovki.Controls.Add((Control) this.button_Zagolovki_EditKeyAttribut);
    this.tabPage_Zagolovki.Controls.Add((Control) this.label_Zagolovki_Attribut);
    this.tabPage_Zagolovki.Controls.Add((Control) this.label_Zagolovki_SlevaVverhu);
    this.tabPage_Zagolovki.Controls.Add((Control) this.label_Zagolovki_SpravaVnizu);
    this.tabPage_Zagolovki.Controls.Add((Control) this.buttonDelete_Zagolovki);
    this.tabPage_Zagolovki.Controls.Add((Control) this.buttonAdd_Zagolovki);
    this.tabPage_Zagolovki.Controls.Add((Control) this.checkBox_Zagolovki_VyvoditPodrazdely);
    this.tabPage_Zagolovki.Controls.Add((Control) this.groupBox_ListZagolovkov);
    this.tabPage_Zagolovki.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.tabPage_Zagolovki.ForeColor = SystemColors.ControlText;
    this.tabPage_Zagolovki.Location = new Point(4, 22);
    this.tabPage_Zagolovki.Name = "tabPage_Zagolovki";
    this.tabPage_Zagolovki.Padding = new Padding(3);
    this.tabPage_Zagolovki.Size = new Size(1576, 731);
    this.tabPage_Zagolovki.TabIndex = 4;
    this.tabPage_Zagolovki.Text = "Заголовки";
    this.checkBox_LocationZagolovki.AutoSize = true;
    this.checkBox_LocationZagolovki.Checked = true;
    this.checkBox_LocationZagolovki.CheckState = CheckState.Checked;
    this.checkBox_LocationZagolovki.Location = new Point(1093, 525);
    this.checkBox_LocationZagolovki.Name = "checkBox_LocationZagolovki";
    this.checkBox_LocationZagolovki.Size = new Size(231, 17);
    this.checkBox_LocationZagolovki.TabIndex = 39;
    this.checkBox_LocationZagolovki.Text = "Контролировать порядок расположения";
    this.toolTip1.SetToolTip((Control) this.checkBox_LocationZagolovki, "Контролировать порядок расположения заголовков, не допуская их дублирования");
    this.checkBox_LocationZagolovki.UseVisualStyleBackColor = true;
    this.checkBox_LocationZagolovki.CheckedChanged += new EventHandler(this.checkBox_LocationZagolovki_CheckedChanged);
    this.checkBox_UserZagolovki.AutoSize = true;
    this.checkBox_UserZagolovki.Location = new Point(1093, 493);
    this.checkBox_UserZagolovki.Name = "checkBox_UserZagolovki";
    this.checkBox_UserZagolovki.Size = new Size(208 /*0xD0*/, 17);
    this.checkBox_UserZagolovki.TabIndex = 38;
    this.checkBox_UserZagolovki.Text = "Разрешать собственные заголовки";
    this.toolTip1.SetToolTip((Control) this.checkBox_UserZagolovki, "Разрешать создавать собственные  (произвольные) заголовки");
    this.checkBox_UserZagolovki.UseVisualStyleBackColor = true;
    this.checkBox_UserZagolovki.CheckedChanged += new EventHandler(this.checkBox_UserZagolovki_CheckedChanged);
    this.groupBox_Include_Name.BackColor = Color.Transparent;
    this.groupBox_Include_Name.Controls.Add((Control) this.textBox_Include_Name);
    this.groupBox_Include_Name.Location = new Point(560, 628);
    this.groupBox_Include_Name.Name = "groupBox_Include_Name";
    this.groupBox_Include_Name.Size = new Size(500, 44);
    this.groupBox_Include_Name.TabIndex = 37;
    this.groupBox_Include_Name.TabStop = false;
    this.groupBox_Include_Name.Text = "Заголовок ведомости составных частей";
    this.toolTip1.SetToolTip((Control) this.groupBox_Include_Name, "Редактирование текста заголовка \"Ведомости составных частей\"");
    this.textBox_Include_Name.Location = new Point(6, 14);
    this.textBox_Include_Name.Name = "textBox_Include_Name";
    this.textBox_Include_Name.Size = new Size(488, 20);
    this.textBox_Include_Name.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.textBox_Include_Name, "Редактирование текста заголовка \"Ведомости составных частей\"");
    this.textBox_Include_Name.KeyDown += new KeyEventHandler(this.textBox_Include_Name_KeyDown);
    this.groupBox_Zagolovki_List_Ved_Id.Controls.Add((Control) this.listBox_Zagolovki_List_Ved_Id);
    this.groupBox_Zagolovki_List_Ved_Id.Location = new Point(3, 52);
    this.groupBox_Zagolovki_List_Ved_Id.Name = "groupBox_Zagolovki_List_Ved_Id";
    this.groupBox_Zagolovki_List_Ved_Id.Size = new Size(525, 412);
    this.groupBox_Zagolovki_List_Ved_Id.TabIndex = 34;
    this.groupBox_Zagolovki_List_Ved_Id.TabStop = false;
    this.groupBox_Zagolovki_List_Ved_Id.Text = "Атрибуты, собираемые из спецификаций";
    this.toolTip1.SetToolTip((Control) this.groupBox_Zagolovki_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Zagolovki_List_Ved_Id.Dock = DockStyle.Fill;
    this.listBox_Zagolovki_List_Ved_Id.FormattingEnabled = true;
    this.listBox_Zagolovki_List_Ved_Id.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Zagolovki_List_Ved_Id.Name = "listBox_Zagolovki_List_Ved_Id";
    this.listBox_Zagolovki_List_Ved_Id.Size = new Size(519, 393);
    this.listBox_Zagolovki_List_Ved_Id.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Zagolovki_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Zagolovki_List_Ved_Id.MouseClick += new MouseEventHandler(this.listBox_Zagolovki_List_Ved_Id_MouseClick);
    this.listBox_Zagolovki_List_Ved_Id.DoubleClick += new EventHandler(this.listBox_Zagolovki_List_Ved_Id_DoubleClick);
    this.groupBox_Zagolovki_AttribVedRec1.BackColor = SystemColors.Control;
    this.groupBox_Zagolovki_AttribVedRec1.Controls.Add((Control) this.listBox_Zagolovki_AttribVedRec);
    this.groupBox_Zagolovki_AttribVedRec1.Location = new Point(3, 477);
    this.groupBox_Zagolovki_AttribVedRec1.Name = "groupBox_Zagolovki_AttribVedRec1";
    this.groupBox_Zagolovki_AttribVedRec1.Size = new Size(525, 198);
    this.groupBox_Zagolovki_AttribVedRec1.TabIndex = 33;
    this.groupBox_Zagolovki_AttribVedRec1.TabStop = false;
    this.groupBox_Zagolovki_AttribVedRec1.Text = "Атрибуты записей ведомостей";
    this.toolTip1.SetToolTip((Control) this.groupBox_Zagolovki_AttribVedRec1, "Атрибуты (имена данных) записей характерные для ведомостей");
    this.listBox_Zagolovki_AttribVedRec.Dock = DockStyle.Fill;
    this.listBox_Zagolovki_AttribVedRec.FormattingEnabled = true;
    this.listBox_Zagolovki_AttribVedRec.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Zagolovki_AttribVedRec.Name = "listBox_Zagolovki_AttribVedRec";
    this.listBox_Zagolovki_AttribVedRec.Size = new Size(519, 179);
    this.listBox_Zagolovki_AttribVedRec.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Zagolovki_AttribVedRec, "Атрибуты записей, характерные для ведомостей");
    this.listBox_Zagolovki_AttribVedRec.MouseClick += new MouseEventHandler(this.listBox_Zagolovki_AttribVedRec_MouseClick);
    this.groupBox_Zagolovki_TypeCompare.Controls.Add((Control) this.radioButton_Zagolovki_Compare_Symbol);
    this.groupBox_Zagolovki_TypeCompare.Controls.Add((Control) this.radioButton_Zagolovki_Compare_Int);
    this.groupBox_Zagolovki_TypeCompare.Location = new Point(241, 681);
    this.groupBox_Zagolovki_TypeCompare.Name = "groupBox_Zagolovki_TypeCompare";
    this.groupBox_Zagolovki_TypeCompare.Size = new Size(286, 35);
    this.groupBox_Zagolovki_TypeCompare.TabIndex = 32 /*0x20*/;
    this.groupBox_Zagolovki_TypeCompare.TabStop = false;
    this.groupBox_Zagolovki_TypeCompare.Text = "Тип сравнения";
    this.toolTip1.SetToolTip((Control) this.groupBox_Zagolovki_TypeCompare, "Тип сравнения числовой или символьный");
    this.radioButton_Zagolovki_Compare_Symbol.AutoSize = true;
    this.radioButton_Zagolovki_Compare_Symbol.Location = new Point(153, 12);
    this.radioButton_Zagolovki_Compare_Symbol.Name = "radioButton_Zagolovki_Compare_Symbol";
    this.radioButton_Zagolovki_Compare_Symbol.Size = new Size(90, 17);
    this.radioButton_Zagolovki_Compare_Symbol.TabIndex = 1;
    this.radioButton_Zagolovki_Compare_Symbol.Text = "Символьный";
    this.toolTip1.SetToolTip((Control) this.radioButton_Zagolovki_Compare_Symbol, "Тип сравнения - символьный");
    this.radioButton_Zagolovki_Compare_Symbol.UseVisualStyleBackColor = true;
    this.radioButton_Zagolovki_Compare_Symbol.CheckedChanged += new EventHandler(this.radioButton_Zagolovki_Compare_Symbol_CheckedChanged);
    this.radioButton_Zagolovki_Compare_Int.AutoSize = true;
    this.radioButton_Zagolovki_Compare_Int.Checked = true;
    this.radioButton_Zagolovki_Compare_Int.Location = new Point(6, 12);
    this.radioButton_Zagolovki_Compare_Int.Name = "radioButton_Zagolovki_Compare_Int";
    this.radioButton_Zagolovki_Compare_Int.Size = new Size(75, 17);
    this.radioButton_Zagolovki_Compare_Int.TabIndex = 0;
    this.radioButton_Zagolovki_Compare_Int.TabStop = true;
    this.radioButton_Zagolovki_Compare_Int.Text = "Числовой";
    this.toolTip1.SetToolTip((Control) this.radioButton_Zagolovki_Compare_Int, "Тип сравнения - числовой");
    this.radioButton_Zagolovki_Compare_Int.UseVisualStyleBackColor = true;
    this.radioButton_Zagolovki_Compare_Int.CheckedChanged += new EventHandler(this.radioButton_Zagolovki_Compare_Int_CheckedChanged);
    this.button_Zagolovki_FromList.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Zagolovki_FromList.Location = new Point(560, 686);
    this.button_Zagolovki_FromList.Name = "button_Zagolovki_FromList";
    this.button_Zagolovki_FromList.Size = new Size(167, 27);
    this.button_Zagolovki_FromList.TabIndex = 30;
    this.button_Zagolovki_FromList.Text = "По списку разделов";
    this.toolTip1.SetToolTip((Control) this.button_Zagolovki_FromList, "За основу списка заголовков взять список разделов");
    this.button_Zagolovki_FromList.UseVisualStyleBackColor = true;
    this.button_Zagolovki_FromList.Visible = false;
    this.button_Zagolovki_FromList.Click += new EventHandler(this.button_Zagolovki_FromList_Click);
    this.label_NoZgolovki.AutoSize = true;
    this.label_NoZgolovki.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label_NoZgolovki.Location = new Point(742, 477);
    this.label_NoZgolovki.Name = "label_NoZgolovki";
    this.label_NoZgolovki.Size = new Size(147, 13);
    this.label_NoZgolovki.TabIndex = 29;
    this.label_NoZgolovki.Text = "Заголовки отсутствуют";
    this.label_NoZgolovki.Visible = false;
    this.button_Zagolovki_EditKeyAttribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Zagolovki_EditKeyAttribut.Location = new Point(9, 686);
    this.button_Zagolovki_EditKeyAttribut.Name = "button_Zagolovki_EditKeyAttribut";
    this.button_Zagolovki_EditKeyAttribut.Size = new Size(214, 27);
    this.button_Zagolovki_EditKeyAttribut.TabIndex = 27;
    this.button_Zagolovki_EditKeyAttribut.Text = "Изменить ключевой атрибут";
    this.toolTip1.SetToolTip((Control) this.button_Zagolovki_EditKeyAttribut, "Изменить ключевой атрибут");
    this.button_Zagolovki_EditKeyAttribut.UseVisualStyleBackColor = true;
    this.button_Zagolovki_EditKeyAttribut.Click += new EventHandler(this.button_Zagolovki_EditKeyAttribut_Click);
    this.label_Zagolovki_Attribut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
    this.label_Zagolovki_Attribut.Location = new Point(3, 25);
    this.label_Zagolovki_Attribut.Name = "label_Zagolovki_Attribut";
    this.label_Zagolovki_Attribut.Size = new Size(525, 24);
    this.label_Zagolovki_Attribut.TabIndex = 25;
    this.label_Zagolovki_Attribut.TextAlign = ContentAlignment.TopCenter;
    this.toolTip1.SetToolTip((Control) this.label_Zagolovki_Attribut, "Наименование ключевого атрибута");
    this.label_Zagolovki_SlevaVverhu.AutoEllipsis = true;
    this.label_Zagolovki_SlevaVverhu.BackColor = Color.LightYellow;
    this.label_Zagolovki_SlevaVverhu.Location = new Point(3, 2);
    this.label_Zagolovki_SlevaVverhu.Name = "label_Zagolovki_SlevaVverhu";
    this.label_Zagolovki_SlevaVverhu.RightToLeft = RightToLeft.Yes;
    this.label_Zagolovki_SlevaVverhu.Size = new Size(525, 16 /*0x10*/);
    this.label_Zagolovki_SlevaVverhu.TabIndex = 24;
    this.label_Zagolovki_SlevaVverhu.Text = "Атрибут, по которому производится создание заголовков";
    this.label_Zagolovki_SlevaVverhu.TextAlign = ContentAlignment.TopCenter;
    this.label_Zagolovki_SpravaVnizu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
    this.label_Zagolovki_SpravaVnizu.Location = new Point(560, 493);
    this.label_Zagolovki_SpravaVnizu.Name = "label_Zagolovki_SpravaVnizu";
    this.label_Zagolovki_SpravaVnizu.Size = new Size(500, 126);
    this.label_Zagolovki_SpravaVnizu.TabIndex = 21;
    this.buttonDelete_Zagolovki.Image = (Image) componentResourceManager.GetObject("buttonDelete_Zagolovki.Image");
    this.buttonDelete_Zagolovki.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonDelete_Zagolovki.Location = new Point(936, 686);
    this.buttonDelete_Zagolovki.Name = "buttonDelete_Zagolovki";
    this.buttonDelete_Zagolovki.Size = new Size(121, 27);
    this.buttonDelete_Zagolovki.TabIndex = 23;
    this.buttonDelete_Zagolovki.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.buttonDelete_Zagolovki, "Удалить текущую строку");
    this.buttonDelete_Zagolovki.UseVisualStyleBackColor = true;
    this.buttonDelete_Zagolovki.Click += new EventHandler(this.button_Zagolovki_Delete_Click);
    this.buttonAdd_Zagolovki.Image = (Image) componentResourceManager.GetObject("buttonAdd_Zagolovki.Image");
    this.buttonAdd_Zagolovki.ImageAlign = ContentAlignment.MiddleRight;
    this.buttonAdd_Zagolovki.Location = new Point(768 /*0x0300*/, 686);
    this.buttonAdd_Zagolovki.Name = "buttonAdd_Zagolovki";
    this.buttonAdd_Zagolovki.Size = new Size(121, 27);
    this.buttonAdd_Zagolovki.TabIndex = 22;
    this.buttonAdd_Zagolovki.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.buttonAdd_Zagolovki, "Добавить строку выше текущей");
    this.buttonAdd_Zagolovki.UseVisualStyleBackColor = true;
    this.buttonAdd_Zagolovki.Click += new EventHandler(this.button_Zagolovki_Add_Click);
    this.checkBox_Zagolovki_VyvoditPodrazdely.AutoSize = true;
    this.checkBox_Zagolovki_VyvoditPodrazdely.Location = new Point(1093, 692);
    this.checkBox_Zagolovki_VyvoditPodrazdely.Name = "checkBox_Zagolovki_VyvoditPodrazdely";
    this.checkBox_Zagolovki_VyvoditPodrazdely.Size = new Size(205, 17);
    this.checkBox_Zagolovki_VyvoditPodrazdely.TabIndex = 31 /*0x1F*/;
    this.checkBox_Zagolovki_VyvoditPodrazdely.Text = "Создавать заголовки подразделов";
    this.checkBox_Zagolovki_VyvoditPodrazdely.UseVisualStyleBackColor = true;
    this.checkBox_Zagolovki_VyvoditPodrazdely.CheckedChanged += new EventHandler(this.checkBox_Zagolovki_VyvoditPodrazdely_CheckedChanged);
    this.groupBox_ListZagolovkov.Controls.Add((Control) this.dataGridView_ListZagolovkov);
    this.groupBox_ListZagolovkov.Location = new Point(560, 25);
    this.groupBox_ListZagolovkov.Name = "groupBox_ListZagolovkov";
    this.groupBox_ListZagolovkov.Size = new Size(500, 439);
    this.groupBox_ListZagolovkov.TabIndex = 20;
    this.groupBox_ListZagolovkov.TabStop = false;
    this.groupBox_ListZagolovkov.Text = "Список заголовков";
    this.toolTip1.SetToolTip((Control) this.groupBox_ListZagolovkov, "Граничные значения и текст заголовка.");
    this.dataGridView_ListZagolovkov.AllowUserToResizeColumns = false;
    this.dataGridView_ListZagolovkov.AllowUserToResizeRows = false;
    this.dataGridView_ListZagolovkov.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
    this.dataGridView_ListZagolovkov.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dataGridView_ListZagolovkov.Columns.AddRange((DataGridViewColumn) this.Zagolovok_Column1, (DataGridViewColumn) this.Zagolovok_Column2);
    this.dataGridView_ListZagolovkov.DefaultCellStyle = gridViewCellStyle2;
    this.dataGridView_ListZagolovkov.Dock = DockStyle.Fill;
    this.dataGridView_ListZagolovkov.Location = new Point(3, 16 /*0x10*/);
    this.dataGridView_ListZagolovkov.Name = "dataGridView_ListZagolovkov";
    this.dataGridView_ListZagolovkov.RowHeadersDefaultCellStyle = gridViewCellStyle3;
    this.dataGridView_ListZagolovkov.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
    this.dataGridView_ListZagolovkov.Size = new Size(494, 420);
    this.dataGridView_ListZagolovkov.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.dataGridView_ListZagolovkov, "Граничные значения и текст заголовка.\r\nЗначения могут состоять из букв или цифр\r\nи рассматриваются как набор сиволов.");
    this.dataGridView_ListZagolovkov.CellBeginEdit += new DataGridViewCellCancelEventHandler(this.dataGridView_ListZagolovkov_CellBeginEdit);
    this.dataGridView_ListZagolovkov.CellClick += new DataGridViewCellEventHandler(this.dataGridView_ListZagolovkov_CellClick);
    this.dataGridView_ListZagolovkov.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridView_ListZagolovkov_CellEndEdit);
    this.dataGridView_ListZagolovkov.CellEnter += new DataGridViewCellEventHandler(this.dataGridView_ListZagolovkov_CellEnter);
    this.dataGridView_ListZagolovkov.CellValidating += new DataGridViewCellValidatingEventHandler(this.dataGridView_ListZagolovkov_CellValidating);
    this.dataGridView_ListZagolovkov.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridView_ListZagolovkov_CellValueChanged);
    this.dataGridView_ListZagolovkov.KeyDown += new KeyEventHandler(this.dataGridView_ListZagolovkov_KeyDown);
    this.Zagolovok_Column1.HeaderText = "Значение";
    this.Zagolovok_Column1.Name = "Zagolovok_Column1";
    this.Zagolovok_Column1.Resizable = DataGridViewTriState.False;
    this.Zagolovok_Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Zagolovok_Column1.Width = 90;
    this.Zagolovok_Column2.HeaderText = "Заголовок";
    this.Zagolovok_Column2.Name = "Zagolovok_Column2";
    this.Zagolovok_Column2.Resizable = DataGridViewTriState.False;
    this.Zagolovok_Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.Zagolovok_Column2.Width = 310;
    this.tabPage_Vyvod.Controls.Add((Control) this.tabControl_Vyvod);
    this.tabPage_Vyvod.Location = new Point(4, 22);
    this.tabPage_Vyvod.Name = "tabPage_Vyvod";
    this.tabPage_Vyvod.Padding = new Padding(3);
    this.tabPage_Vyvod.Size = new Size(1576, 731);
    this.tabPage_Vyvod.TabIndex = 5;
    this.tabPage_Vyvod.Text = "Вывод";
    this.tabPage_Vyvod.UseVisualStyleBackColor = true;
    this.tabControl_Vyvod.Controls.Add((Control) this.tabPage_Vyvod_1);
    this.tabControl_Vyvod.Controls.Add((Control) this.tabPage_Vyvod_2);
    this.tabControl_Vyvod.Dock = DockStyle.Fill;
    this.tabControl_Vyvod.Location = new Point(3, 3);
    this.tabControl_Vyvod.Name = "tabControl_Vyvod";
    this.tabControl_Vyvod.SelectedIndex = 0;
    this.tabControl_Vyvod.Size = new Size(1570, 725);
    this.tabControl_Vyvod.TabIndex = 11;
    this.tabPage_Vyvod_1.AutoScroll = true;
    this.tabPage_Vyvod_1.Controls.Add((Control) this.buttonEditTemplate);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_List_Ved_Id);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.button_Vyvod_Obshaia);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.button_Vyvod_PoRazdelam);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.rightDock_Vyvod);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_AttribVedRec1);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_Ved_Pasport);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.bottomDock);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.topDock_Vyvod);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.leftDock_Vyvod);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.panel_Vyvod_1);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.docContainer_Vyvod);
    this.tabPage_Vyvod_1.Controls.Add((Control) this.docKcontainer_Vyvod);
    this.tabPage_Vyvod_1.Location = new Point(4, 22);
    this.tabPage_Vyvod_1.Name = "tabPage_Vyvod_1";
    this.tabPage_Vyvod_1.Padding = new Padding(3);
    this.tabPage_Vyvod_1.Size = new Size(1562, 699);
    this.tabPage_Vyvod_1.TabIndex = 0;
    this.tabPage_Vyvod_1.Text = "Вывод";
    this.tabPage_Vyvod_1.UseVisualStyleBackColor = true;
    this.buttonEditTemplate.Location = new Point(1093, 528);
    this.buttonEditTemplate.Name = "buttonEditTemplate";
    this.buttonEditTemplate.Size = new Size(202, 27);
    this.buttonEditTemplate.TabIndex = 37;
    this.buttonEditTemplate.Text = "Редактор шаблона";
    this.toolTip1.SetToolTip((Control) this.buttonEditTemplate, "Открыть окно редактирования шаблона (бланка)");
    this.buttonEditTemplate.UseVisualStyleBackColor = true;
    this.buttonEditTemplate.Click += new EventHandler(this.buttonEditTemplate_Click);
    this.groupBox_Vyvod_List_Ved_Id.Controls.Add((Control) this.listBox_Vyvod_List_Ved_Id);
    this.groupBox_Vyvod_List_Ved_Id.Location = new Point(3, 3);
    this.groupBox_Vyvod_List_Ved_Id.Name = "groupBox_Vyvod_List_Ved_Id";
    this.groupBox_Vyvod_List_Ved_Id.Size = new Size(241, 445);
    this.groupBox_Vyvod_List_Ved_Id.TabIndex = 36;
    this.groupBox_Vyvod_List_Ved_Id.TabStop = false;
    this.groupBox_Vyvod_List_Ved_Id.Text = "Атрибуты, собираемые из спецификаций";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Vyvod_List_Ved_Id.Dock = DockStyle.Fill;
    this.listBox_Vyvod_List_Ved_Id.FormattingEnabled = true;
    this.listBox_Vyvod_List_Ved_Id.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Vyvod_List_Ved_Id.Name = "listBox_Vyvod_List_Ved_Id";
    this.listBox_Vyvod_List_Ved_Id.Size = new Size(235, 426);
    this.listBox_Vyvod_List_Ved_Id.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Vyvod_List_Ved_Id, "Атрибуты, собираемые из спецификаций");
    this.listBox_Vyvod_List_Ved_Id.MouseClick += new MouseEventHandler(this.listBox_Vyvod_List_Ved_Id_MouseClick);
    this.button_Vyvod_Obshaia.Location = new Point(601, 528);
    this.button_Vyvod_Obshaia.Name = "button_Vyvod_Obshaia";
    this.button_Vyvod_Obshaia.Size = new Size(202, 27);
    this.button_Vyvod_Obshaia.TabIndex = 25;
    this.button_Vyvod_Obshaia.Text = "Общая";
    this.button_Vyvod_Obshaia.UseVisualStyleBackColor = true;
    this.button_Vyvod_Obshaia.Click += new EventHandler(this.button_Vyvod_Obshaia_Click);
    this.button_Vyvod_PoRazdelam.Location = new Point(601, 528);
    this.button_Vyvod_PoRazdelam.Name = "button_Vyvod_PoRazdelam";
    this.button_Vyvod_PoRazdelam.Size = new Size(202, 27);
    this.button_Vyvod_PoRazdelam.TabIndex = 24;
    this.button_Vyvod_PoRazdelam.Text = "По разделам";
    this.button_Vyvod_PoRazdelam.UseVisualStyleBackColor = true;
    this.button_Vyvod_PoRazdelam.Click += new EventHandler(this.button_Vyvod_PoRazdelam_Click);
    this.rightDock_Vyvod.Dock = DockStyle.Right;
    this.rightDock_Vyvod.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.rightDock_Vyvod.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.rightDock_Vyvod.Location = new Point(1559, 3);
    this.rightDock_Vyvod.Manager = (DockManager) null;
    this.rightDock_Vyvod.Name = "rightDock_Vyvod";
    this.rightDock_Vyvod.Renderer = (RendererBase) null;
    this.rightDock_Vyvod.Size = new Size(0, 693);
    this.rightDock_Vyvod.TabIndex = 20;
    this.groupBox_Vyvod_AttribVedRec1.Controls.Add((Control) this.listBox_Vyvod_AttribVedRec);
    this.groupBox_Vyvod_AttribVedRec1.Location = new Point(0, 449);
    this.groupBox_Vyvod_AttribVedRec1.Name = "groupBox_Vyvod_AttribVedRec1";
    this.groupBox_Vyvod_AttribVedRec1.Size = new Size(244, 238);
    this.groupBox_Vyvod_AttribVedRec1.TabIndex = 15;
    this.groupBox_Vyvod_AttribVedRec1.TabStop = false;
    this.groupBox_Vyvod_AttribVedRec1.Text = "Атрибуты записей ведомостей";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_AttribVedRec1, "Атрибуты (имена данных) записей характерные для ведомостей");
    this.listBox_Vyvod_AttribVedRec.Dock = DockStyle.Fill;
    this.listBox_Vyvod_AttribVedRec.FormattingEnabled = true;
    this.listBox_Vyvod_AttribVedRec.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Vyvod_AttribVedRec.Name = "listBox_Vyvod_AttribVedRec";
    this.listBox_Vyvod_AttribVedRec.Size = new Size(238, 219);
    this.listBox_Vyvod_AttribVedRec.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBox_Vyvod_AttribVedRec, "Атрибуты (имена данных) записей, характерные для ведомостей");
    this.listBox_Vyvod_AttribVedRec.MouseClick += new MouseEventHandler(this.listBox_Vyvod_AttribVedRec_MouseClick);
    this.groupBox_Vyvod_Ved_Pasport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this.groupBox_Vyvod_Ved_Pasport.Controls.Add((Control) this.listBoxAttrib_Vyvod_VedPasport);
    this.groupBox_Vyvod_Ved_Pasport.Location = new Point(0, 449);
    this.groupBox_Vyvod_Ved_Pasport.Name = "groupBox_Vyvod_Ved_Pasport";
    this.groupBox_Vyvod_Ved_Pasport.Size = new Size(244, 203);
    this.groupBox_Vyvod_Ved_Pasport.TabIndex = 19;
    this.groupBox_Vyvod_Ved_Pasport.TabStop = false;
    this.groupBox_Vyvod_Ved_Pasport.Text = "Атрибуты основной надписи ведомостей";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_Ved_Pasport, "Атрибуты (имена данных) основной надписи характерные для ведомостей");
    this.listBoxAttrib_Vyvod_VedPasport.Dock = DockStyle.Fill;
    this.listBoxAttrib_Vyvod_VedPasport.FormattingEnabled = true;
    this.listBoxAttrib_Vyvod_VedPasport.Location = new Point(3, 16 /*0x10*/);
    this.listBoxAttrib_Vyvod_VedPasport.Name = "listBoxAttrib_Vyvod_VedPasport";
    this.listBoxAttrib_Vyvod_VedPasport.Size = new Size(238, 184);
    this.listBoxAttrib_Vyvod_VedPasport.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.listBoxAttrib_Vyvod_VedPasport, "Атрибуты (имена данных) основной надписи характерные для ведомостей");
    this.bottomDock.Dock = DockStyle.Bottom;
    this.bottomDock.Guid = new Guid("fe2fb97d-ac9d-4dad-9df6-2c0bea4342e6");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.bottomDock.Location = new Point(3, 696);
    this.bottomDock.Manager = (DockManager) null;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    this.bottomDock.Size = new Size(1556, 0);
    this.bottomDock.TabIndex = 21;
    this.topDock_Vyvod.Dock = DockStyle.Top;
    this.topDock_Vyvod.Guid = new Guid("ac4b6b36-2b4c-4cf3-a3a7-f6004b97153b");
    this.topDock_Vyvod.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.topDock_Vyvod.Location = new Point(3, 3);
    this.topDock_Vyvod.Manager = (DockManager) null;
    this.topDock_Vyvod.Name = "topDock_Vyvod";
    this.topDock_Vyvod.Renderer = (RendererBase) null;
    this.topDock_Vyvod.Size = new Size(1556, 0);
    this.topDock_Vyvod.TabIndex = 22;
    this.leftDock_Vyvod.Dock = DockStyle.Left;
    this.leftDock_Vyvod.Guid = new Guid("de1bb64f-0767-401a-8518-e52184c5f999");
    this.leftDock_Vyvod.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.leftDock_Vyvod.Location = new Point(3, 3);
    this.leftDock_Vyvod.Manager = (DockManager) null;
    this.leftDock_Vyvod.Name = "leftDock_Vyvod";
    this.leftDock_Vyvod.Renderer = (RendererBase) null;
    this.leftDock_Vyvod.Size = new Size(0, 693);
    this.leftDock_Vyvod.TabIndex = 23;
    this.panel_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_Forma);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_AddAttribut);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_Delete);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_Edit);
    this.panel_Vyvod_1.Controls.Add((Control) this.button_Vyvod_AddCell);
    this.panel_Vyvod_1.Controls.Add((Control) this.groupBox_Vyvod_TextRazdelitel);
    this.panel_Vyvod_1.Controls.Add((Control) this.treeView_Vyvod);
    this.panel_Vyvod_1.Location = new Point(247, 3);
    this.panel_Vyvod_1.Name = "panel_Vyvod_1";
    this.panel_Vyvod_1.Size = new Size(348, 682);
    this.panel_Vyvod_1.TabIndex = 18;
    this.groupBox_Vyvod_Forma.Controls.Add((Control) this.radioButton_Vyvod_GroupB);
    this.groupBox_Vyvod_Forma.Controls.Add((Control) this.radioButton_Vyvod_EdOrA);
    this.groupBox_Vyvod_Forma.Controls.Add((Control) this.numeric_Vyvod_UpDownKolGraf);
    this.groupBox_Vyvod_Forma.Controls.Add((Control) this.label_Vyvod_Graf);
    this.groupBox_Vyvod_Forma.Location = new Point(11, 578);
    this.groupBox_Vyvod_Forma.Name = "groupBox_Vyvod_Forma";
    this.groupBox_Vyvod_Forma.Size = new Size(199, 96 /*0x60*/);
    this.groupBox_Vyvod_Forma.TabIndex = 17;
    this.groupBox_Vyvod_Forma.TabStop = false;
    this.groupBox_Vyvod_Forma.Text = "Форма";
    this.radioButton_Vyvod_GroupB.AutoSize = true;
    this.radioButton_Vyvod_GroupB.Location = new Point(6, 42);
    this.radioButton_Vyvod_GroupB.Name = "radioButton_Vyvod_GroupB";
    this.radioButton_Vyvod_GroupB.Size = new Size(88, 17);
    this.radioButton_Vyvod_GroupB.TabIndex = 23;
    this.radioButton_Vyvod_GroupB.Text = "Групповой Б";
    this.toolTip1.SetToolTip((Control) this.radioButton_Vyvod_GroupB, "Перейти к настройкам групповой формы Б");
    this.radioButton_Vyvod_GroupB.UseVisualStyleBackColor = true;
    this.radioButton_Vyvod_GroupB.Click += new EventHandler(this.radioButton_Vyvod_GroupB_Click);
    this.radioButton_Vyvod_EdOrA.AutoSize = true;
    this.radioButton_Vyvod_EdOrA.Checked = true;
    this.radioButton_Vyvod_EdOrA.Location = new Point(6, 19);
    this.radioButton_Vyvod_EdOrA.Name = "radioButton_Vyvod_EdOrA";
    this.radioButton_Vyvod_EdOrA.Size = new Size(112 /*0x70*/, 17);
    this.radioButton_Vyvod_EdOrA.TabIndex = 22;
    this.radioButton_Vyvod_EdOrA.TabStop = true;
    this.radioButton_Vyvod_EdOrA.Text = "Единичный или А";
    this.toolTip1.SetToolTip((Control) this.radioButton_Vyvod_EdOrA, "Перейти к настройкам единичной формы и групповой формы А");
    this.radioButton_Vyvod_EdOrA.UseVisualStyleBackColor = true;
    this.radioButton_Vyvod_EdOrA.Click += new EventHandler(this.radioButton_Vyvod_EdOrA_Click);
    this.numeric_Vyvod_UpDownKolGraf.Location = new Point(112 /*0x70*/, 66);
    this.numeric_Vyvod_UpDownKolGraf.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.numeric_Vyvod_UpDownKolGraf.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_Vyvod_UpDownKolGraf.Name = "numeric_Vyvod_UpDownKolGraf";
    this.numeric_Vyvod_UpDownKolGraf.Size = new Size(53, 20);
    this.numeric_Vyvod_UpDownKolGraf.TabIndex = 20;
    this.numeric_Vyvod_UpDownKolGraf.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_Vyvod_UpDownKolGraf.Visible = false;
    this.numeric_Vyvod_UpDownKolGraf.ValueChanged += new EventHandler(this.numeric_Vyvod_UpDownKolGraf_ValueChanged);
    this.label_Vyvod_Graf.Location = new Point(3, 68);
    this.label_Vyvod_Graf.Name = "label_Vyvod_Graf";
    this.label_Vyvod_Graf.Size = new Size(103, 18);
    this.label_Vyvod_Graf.TabIndex = 21;
    this.label_Vyvod_Graf.Text = "Граф кол.:";
    this.label_Vyvod_Graf.TextAlign = ContentAlignment.TopRight;
    this.label_Vyvod_Graf.Visible = false;
    this.button_Vyvod_AddAttribut.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Vyvod_AddAttribut.Enabled = false;
    this.button_Vyvod_AddAttribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_AddAttribut.Location = new Point(11, 456);
    this.button_Vyvod_AddAttribut.Name = "button_Vyvod_AddAttribut";
    this.button_Vyvod_AddAttribut.Size = new Size(202, 27);
    this.button_Vyvod_AddAttribut.TabIndex = 16 /*0x10*/;
    this.button_Vyvod_AddAttribut.Text = "Добавить атрибут";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_AddAttribut, "Добавить атрибут");
    this.button_Vyvod_AddAttribut.UseVisualStyleBackColor = true;
    this.button_Vyvod_AddAttribut.Click += new EventHandler(this.button_Vyvod_AddAttribut_Click);
    this.button_Vyvod_Delete.Enabled = false;
    this.button_Vyvod_Delete.Image = (Image) componentResourceManager.GetObject("button_Vyvod_Delete.Image");
    this.button_Vyvod_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_Delete.Location = new Point(11, 528);
    this.button_Vyvod_Delete.Name = "button_Vyvod_Delete";
    this.button_Vyvod_Delete.Size = new Size(202, 27);
    this.button_Vyvod_Delete.TabIndex = 15;
    this.button_Vyvod_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_Delete, "Удалить текущее условие");
    this.button_Vyvod_Delete.UseVisualStyleBackColor = true;
    this.button_Vyvod_Delete.Click += new EventHandler(this.button_Vyvod_Delete_Click);
    this.button_Vyvod_Edit.Enabled = false;
    this.button_Vyvod_Edit.Image = (Image) componentResourceManager.GetObject("button_Vyvod_Edit.Image");
    this.button_Vyvod_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_Edit.Location = new Point(11, 494);
    this.button_Vyvod_Edit.Name = "button_Vyvod_Edit";
    this.button_Vyvod_Edit.Size = new Size(202, 27);
    this.button_Vyvod_Edit.TabIndex = 14;
    this.button_Vyvod_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_Edit, "Изменить текущее условие согласно выбранным параметрам");
    this.button_Vyvod_Edit.UseVisualStyleBackColor = true;
    this.button_Vyvod_Edit.Click += new EventHandler(this.button_Vyvod_Edit_Click);
    this.button_Vyvod_AddCell.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Vyvod_AddCell.Enabled = false;
    this.button_Vyvod_AddCell.Image = (Image) componentResourceManager.GetObject("button_Vyvod_AddCell.Image");
    this.button_Vyvod_AddCell.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Vyvod_AddCell.Location = new Point(11, 418);
    this.button_Vyvod_AddCell.Name = "button_Vyvod_AddCell";
    this.button_Vyvod_AddCell.Size = new Size(202, 27);
    this.button_Vyvod_AddCell.TabIndex = 13;
    this.button_Vyvod_AddCell.Text = "Добавить ячейку";
    this.toolTip1.SetToolTip((Control) this.button_Vyvod_AddCell, "Добавить ячейку");
    this.button_Vyvod_AddCell.UseVisualStyleBackColor = true;
    this.button_Vyvod_AddCell.Click += new EventHandler(this.button_Vyvod_AddCell_Click);
    this.groupBox_Vyvod_TextRazdelitel.Controls.Add((Control) this.comboBox_Vyvod_TextRazdelitel);
    this.groupBox_Vyvod_TextRazdelitel.Location = new Point(11, 370);
    this.groupBox_Vyvod_TextRazdelitel.Name = "groupBox_Vyvod_TextRazdelitel";
    this.groupBox_Vyvod_TextRazdelitel.Size = new Size(202, 41);
    this.groupBox_Vyvod_TextRazdelitel.TabIndex = 12;
    this.groupBox_Vyvod_TextRazdelitel.TabStop = false;
    this.groupBox_Vyvod_TextRazdelitel.Text = "Текст разделитель";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_TextRazdelitel, "Текст (символ) разделитель, отделяющий отделяющий от предыдущего текста");
    this.comboBox_Vyvod_TextRazdelitel.Dock = DockStyle.Fill;
    this.comboBox_Vyvod_TextRazdelitel.FormattingEnabled = true;
    this.comboBox_Vyvod_TextRazdelitel.Items.AddRange(new object[9]
    {
      (object) "  (пробел)",
      (object) "(неразрывный пробел)",
      (object) "(без пробела)",
      (object) "(принудительный перенос)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)",
      (object) "(неразрывный дефис)"
    });
    this.comboBox_Vyvod_TextRazdelitel.Location = new Point(3, 16 /*0x10*/);
    this.comboBox_Vyvod_TextRazdelitel.Name = "comboBox_Vyvod_TextRazdelitel";
    this.comboBox_Vyvod_TextRazdelitel.Size = new Size(196, 21);
    this.comboBox_Vyvod_TextRazdelitel.TabIndex = 3;
    this.treeView_Vyvod.HideSelection = false;
    this.treeView_Vyvod.Location = new Point(6, 6);
    this.treeView_Vyvod.Name = "treeView_Vyvod";
    this.treeView_Vyvod.Size = new Size(342, 353);
    this.treeView_Vyvod.TabIndex = 11;
    this.treeView_Vyvod.AfterSelect += new TreeViewEventHandler(this.treeView_Vyvod_AfterSelect);
    this.docContainer_Vyvod.Dock = DockStyle.None;
    this.docContainer_Vyvod.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Vyvod.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docContainer_Vyvod.Location = new Point(600, 1);
    this.docContainer_Vyvod.Manager = (DockManager) null;
    this.docContainer_Vyvod.Name = "docContainer_Vyvod";
    this.docContainer_Vyvod.Renderer = (RendererBase) null;
    this.docContainer_Vyvod.Size = new Size(695, 505);
    this.docContainer_Vyvod.TabIndex = 17;
    this.docKcontainer_Vyvod.Dock = DockStyle.Right;
    this.docKcontainer_Vyvod.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.docKcontainer_Vyvod.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docKcontainer_Vyvod.Location = new Point(1559, 3);
    this.docKcontainer_Vyvod.Manager = this.dockMan_Vyvod;
    this.docKcontainer_Vyvod.Name = "docKcontainer_Vyvod";
    this.docKcontainer_Vyvod.Renderer = (RendererBase) null;
    this.docKcontainer_Vyvod.Size = new Size(0, 693);
    this.docKcontainer_Vyvod.TabIndex = 24;
    this.dockMan_Vyvod.DocumentContainer = this.docContainer_Vyvod;
    this.dockMan_Vyvod.OwnerForm = (Form) this;
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_isUnbrokenDefis);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_Check);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_ProtectionCommand);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_Protection_From_Editing);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_Vyvod_isDeleteIdenticalTexts);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_Vyvod_Additional);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.groupBox_Vyvod2_SkipRows);
    this.tabPage_Vyvod_2.Controls.Add((Control) this.group_Vyvod2_BoxLizm);
    this.tabPage_Vyvod_2.Location = new Point(4, 22);
    this.tabPage_Vyvod_2.Name = "tabPage_Vyvod_2";
    this.tabPage_Vyvod_2.Padding = new Padding(3);
    this.tabPage_Vyvod_2.Size = new Size(1562, 699);
    this.tabPage_Vyvod_2.TabIndex = 1;
    this.tabPage_Vyvod_2.Text = "Прочее";
    this.tabPage_Vyvod_2.UseVisualStyleBackColor = true;
    this.groupBox_isUnbrokenDefis.Controls.Add((Control) this.checkBox_isUnbrokenDefis);
    this.groupBox_isUnbrokenDefis.Location = new Point(10, 483);
    this.groupBox_isUnbrokenDefis.Name = "groupBox_isUnbrokenDefis";
    this.groupBox_isUnbrokenDefis.Size = new Size(359, 52);
    this.groupBox_isUnbrokenDefis.TabIndex = 32 /*0x20*/;
    this.groupBox_isUnbrokenDefis.TabStop = false;
    this.groupBox_isUnbrokenDefis.Text = "Замена дефиса на неразрывный дефис";
    this.toolTip1.SetToolTip((Control) this.groupBox_isUnbrokenDefis, "В обозначениях исполнений заменять дефис на неразрывный дефис");
    this.checkBox_isUnbrokenDefis.AutoSize = true;
    this.checkBox_isUnbrokenDefis.Checked = true;
    this.checkBox_isUnbrokenDefis.CheckState = CheckState.Checked;
    this.checkBox_isUnbrokenDefis.Location = new Point(10, 20);
    this.checkBox_isUnbrokenDefis.Name = "checkBox_isUnbrokenDefis";
    this.checkBox_isUnbrokenDefis.Size = new Size(234, 17);
    this.checkBox_isUnbrokenDefis.TabIndex = 0;
    this.checkBox_isUnbrokenDefis.Text = "Заменять дефис на неразрывный дефис";
    this.toolTip1.SetToolTip((Control) this.checkBox_isUnbrokenDefis, "В обозначениях исполнений заменять дефис на неразрывный дефис");
    this.checkBox_isUnbrokenDefis.UseVisualStyleBackColor = true;
    this.checkBox_isUnbrokenDefis.CheckedChanged += new EventHandler(this.checkBox_isUnbrokenDefis_CheckedChanged);
    this.groupBox_Check.Controls.Add((Control) this.button_Check);
    this.groupBox_Check.Controls.Add((Control) this.checkBox_isCheck);
    this.groupBox_Check.Location = new Point(534, 257);
    this.groupBox_Check.Name = "groupBox_Check";
    this.groupBox_Check.Size = new Size(472, 116);
    this.groupBox_Check.TabIndex = 31 /*0x1F*/;
    this.groupBox_Check.TabStop = false;
    this.button_Check.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Check.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Check.Location = new Point(10, 70);
    this.button_Check.Name = "button_Check";
    this.button_Check.Size = new Size(202, 27);
    this.button_Check.TabIndex = 17;
    this.button_Check.Text = "Произвести контроль";
    this.toolTip1.SetToolTip((Control) this.button_Check, "Произвести сравнение и контроль соответствия настроек и шаблона");
    this.button_Check.UseVisualStyleBackColor = true;
    this.button_Check.Click += new EventHandler(this.buttonCheck_Click);
    this.checkBox_isCheck.AutoSize = true;
    this.checkBox_isCheck.Checked = true;
    this.checkBox_isCheck.CheckState = CheckState.Checked;
    this.checkBox_isCheck.Location = new Point(10, 25);
    this.checkBox_isCheck.Name = "checkBox_isCheck";
    this.checkBox_isCheck.Size = new Size(311, 17);
    this.checkBox_isCheck.TabIndex = 0;
    this.checkBox_isCheck.Text = "Производить контроль настроек и шаблона при выводе";
    this.toolTip1.SetToolTip((Control) this.checkBox_isCheck, "В момент вывода производить сравнение и контроль соответствия настроек и шаблона");
    this.checkBox_isCheck.UseVisualStyleBackColor = true;
    this.checkBox_isCheck.CheckedChanged += new EventHandler(this.checkBox_isCheck_CheckedChanged);
    this.groupBox_ProtectionCommand.Controls.Add((Control) this.checkBox_isProtectionCommand);
    this.groupBox_ProtectionCommand.Location = new Point(534, 122);
    this.groupBox_ProtectionCommand.Name = "groupBox_ProtectionCommand";
    this.groupBox_ProtectionCommand.Size = new Size(472, 65);
    this.groupBox_ProtectionCommand.TabIndex = 30;
    this.groupBox_ProtectionCommand.TabStop = false;
    this.checkBox_isProtectionCommand.AutoSize = true;
    this.checkBox_isProtectionCommand.Location = new Point(10, 25);
    this.checkBox_isProtectionCommand.Name = "checkBox_isProtectionCommand";
    this.checkBox_isProtectionCommand.Size = new Size(245, 17);
    this.checkBox_isProtectionCommand.TabIndex = 0;
    this.checkBox_isProtectionCommand.Text = "Отображать команды \"Только для чтения\"";
    this.toolTip1.SetToolTip((Control) this.checkBox_isProtectionCommand, "Отображать команды \"Только для чтения/Разрешить редактирование");
    this.checkBox_isProtectionCommand.UseVisualStyleBackColor = true;
    this.checkBox_isProtectionCommand.CheckedChanged += new EventHandler(this.checkBox_isProtectionCommand_CheckedChanged);
    this.groupBox_Protection_From_Editing.Controls.Add((Control) this.checkBox_isProhibition_DocRowWithObj);
    this.groupBox_Protection_From_Editing.Controls.Add((Control) this.checkBox_isFullProhibition);
    this.groupBox_Protection_From_Editing.Location = new Point(534, 16 /*0x10*/);
    this.groupBox_Protection_From_Editing.Name = "groupBox_Protection_From_Editing";
    this.groupBox_Protection_From_Editing.Size = new Size(472, 100);
    this.groupBox_Protection_From_Editing.TabIndex = 29;
    this.groupBox_Protection_From_Editing.TabStop = false;
    this.groupBox_Protection_From_Editing.Text = "Запретить редактирование";
    this.toolTip1.SetToolTip((Control) this.groupBox_Protection_From_Editing, "Запрет редактирования некоторых данных документа");
    this.checkBox_isProhibition_DocRowWithObj.AutoSize = true;
    this.checkBox_isProhibition_DocRowWithObj.Location = new Point(10, 59);
    this.checkBox_isProhibition_DocRowWithObj.Name = "checkBox_isProhibition_DocRowWithObj";
    this.checkBox_isProhibition_DocRowWithObj.Size = new Size(297, 17);
    this.checkBox_isProhibition_DocRowWithObj.TabIndex = 1;
    this.checkBox_isProhibition_DocRowWithObj.Text = "Данных, соответствующих существующим объектам";
    this.toolTip1.SetToolTip((Control) this.checkBox_isProhibition_DocRowWithObj, "Запретить редактирование данных, соответствующих объектам, введенных из базы");
    this.checkBox_isProhibition_DocRowWithObj.UseVisualStyleBackColor = true;
    this.checkBox_isProhibition_DocRowWithObj.CheckedChanged += new EventHandler(this.checkBox_isProhibition_DocRowWithObj_CheckedChanged);
    this.checkBox_isFullProhibition.AutoSize = true;
    this.checkBox_isFullProhibition.Location = new Point(10, 25);
    this.checkBox_isFullProhibition.Name = "checkBox_isFullProhibition";
    this.checkBox_isFullProhibition.Size = new Size(222, 17);
    this.checkBox_isFullProhibition.TabIndex = 0;
    this.checkBox_isFullProhibition.Text = "Ведомости, созданной автоматически";
    this.toolTip1.SetToolTip((Control) this.checkBox_isFullProhibition, "Запретить редактирование содержания ведомости, созданной программой автоматически");
    this.checkBox_isFullProhibition.UseVisualStyleBackColor = true;
    this.checkBox_isFullProhibition.CheckedChanged += new EventHandler(this.checkBox_isFullProhibition_CheckedChanged);
    this.groupBox_Vyvod_isDeleteIdenticalTexts.Controls.Add((Control) this.checkBox_Vyvod_isDeleteIdenticalTexts);
    this.groupBox_Vyvod_isDeleteIdenticalTexts.Location = new Point(10, 398);
    this.groupBox_Vyvod_isDeleteIdenticalTexts.Name = "groupBox_Vyvod_isDeleteIdenticalTexts";
    this.groupBox_Vyvod_isDeleteIdenticalTexts.Size = new Size(359, 52);
    this.groupBox_Vyvod_isDeleteIdenticalTexts.TabIndex = 28;
    this.groupBox_Vyvod_isDeleteIdenticalTexts.TabStop = false;
    this.groupBox_Vyvod_isDeleteIdenticalTexts.Text = "Удаление одинаковых текстов";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_isDeleteIdenticalTexts, "При автоматическом сборе ведомости одинаковые тексты в графах заменять на \"То же\" и \" (ГОСТ 2.105-95 п.4.4.16)");
    this.checkBox_Vyvod_isDeleteIdenticalTexts.AutoSize = true;
    this.checkBox_Vyvod_isDeleteIdenticalTexts.Location = new Point(10, 20);
    this.checkBox_Vyvod_isDeleteIdenticalTexts.Name = "checkBox_Vyvod_isDeleteIdenticalTexts";
    this.checkBox_Vyvod_isDeleteIdenticalTexts.Size = new Size(173, 17);
    this.checkBox_Vyvod_isDeleteIdenticalTexts.TabIndex = 0;
    this.checkBox_Vyvod_isDeleteIdenticalTexts.Text = "Удалять одинаковые тексты";
    this.toolTip1.SetToolTip((Control) this.checkBox_Vyvod_isDeleteIdenticalTexts, "При автоматическом сборе ведомости одинаковые тексты в графах заменять на \"То же\" и \" (ГОСТ 2.105-95 п.4.4.16)");
    this.checkBox_Vyvod_isDeleteIdenticalTexts.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_isDeleteIdenticalTexts.CheckedChanged += new EventHandler(this.checkBox_Vyvod_isDeleteIdenticalTexts_CheckedChanged);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional4);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional3);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional2);
    this.groupBox_Vyvod_Additional.Controls.Add((Control) this.checkBox_Vyvod_Additional1);
    this.groupBox_Vyvod_Additional.Location = new Point(10, 257);
    this.groupBox_Vyvod_Additional.Name = "groupBox_Vyvod_Additional";
    this.groupBox_Vyvod_Additional.Size = new Size(359, 116);
    this.groupBox_Vyvod_Additional.TabIndex = 27;
    this.groupBox_Vyvod_Additional.TabStop = false;
    this.groupBox_Vyvod_Additional.Text = "Дополнительные записи";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod_Additional, "Использовать ли в редакторе \"Дополнительные\" записи");
    this.checkBox_Vyvod_Additional4.AutoSize = true;
    this.checkBox_Vyvod_Additional4.Location = new Point(10, 80 /*0x50*/);
    this.checkBox_Vyvod_Additional4.Name = "checkBox_Vyvod_Additional4";
    this.checkBox_Vyvod_Additional4.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional4.TabIndex = 3;
    this.checkBox_Vyvod_Additional4.Text = "Дополнительная 4";
    this.checkBox_Vyvod_Additional4.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional4.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional4_CheckedChanged);
    this.checkBox_Vyvod_Additional3.AutoSize = true;
    this.checkBox_Vyvod_Additional3.Location = new Point(10, 60);
    this.checkBox_Vyvod_Additional3.Name = "checkBox_Vyvod_Additional3";
    this.checkBox_Vyvod_Additional3.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional3.TabIndex = 2;
    this.checkBox_Vyvod_Additional3.Text = "Дополнительная 3";
    this.checkBox_Vyvod_Additional3.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional3.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional3_CheckedChanged);
    this.checkBox_Vyvod_Additional2.AutoSize = true;
    this.checkBox_Vyvod_Additional2.Location = new Point(10, 40);
    this.checkBox_Vyvod_Additional2.Name = "checkBox_Vyvod_Additional2";
    this.checkBox_Vyvod_Additional2.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional2.TabIndex = 1;
    this.checkBox_Vyvod_Additional2.Text = "Дополнительная 2";
    this.checkBox_Vyvod_Additional2.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional2.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional2_CheckedChanged);
    this.checkBox_Vyvod_Additional1.AutoSize = true;
    this.checkBox_Vyvod_Additional1.Location = new Point(10, 20);
    this.checkBox_Vyvod_Additional1.Name = "checkBox_Vyvod_Additional1";
    this.checkBox_Vyvod_Additional1.Size = new Size(121, 17);
    this.checkBox_Vyvod_Additional1.TabIndex = 0;
    this.checkBox_Vyvod_Additional1.Text = "Дополнительная 1";
    this.checkBox_Vyvod_Additional1.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod_Additional1.CheckedChanged += new EventHandler(this.checkBox_Vyvod_Additional1_CheckedChanged);
    this.groupBox_Vyvod2_SkipRows.Controls.Add((Control) this.label_Vyvod2_AfterRemark);
    this.groupBox_Vyvod2_SkipRows.Controls.Add((Control) this.numericUpDown_Vyvod2_AfterRemark);
    this.groupBox_Vyvod2_SkipRows.Controls.Add((Control) this.label_Vyvod2_AfterInfo);
    this.groupBox_Vyvod2_SkipRows.Controls.Add((Control) this.numericUpDown_Vyvod2_AfterInfo);
    this.groupBox_Vyvod2_SkipRows.Location = new Point(10, 122);
    this.groupBox_Vyvod2_SkipRows.Name = "groupBox_Vyvod2_SkipRows";
    this.groupBox_Vyvod2_SkipRows.Size = new Size(359, 116);
    this.groupBox_Vyvod2_SkipRows.TabIndex = 1;
    this.groupBox_Vyvod2_SkipRows.TabStop = false;
    this.groupBox_Vyvod2_SkipRows.Text = "Пропуск строк";
    this.toolTip1.SetToolTip((Control) this.groupBox_Vyvod2_SkipRows, "Настроить правила вставки пустых строк");
    this.label_Vyvod2_AfterRemark.AutoSize = true;
    this.label_Vyvod2_AfterRemark.Location = new Point(68, 68);
    this.label_Vyvod2_AfterRemark.Name = "label_Vyvod2_AfterRemark";
    this.label_Vyvod2_AfterRemark.Size = new Size(103, 13);
    this.label_Vyvod2_AfterRemark.TabIndex = 11;
    this.label_Vyvod2_AfterRemark.Text = "После примечания";
    this.numericUpDown_Vyvod2_AfterRemark.Location = new Point(20, 66);
    this.numericUpDown_Vyvod2_AfterRemark.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_AfterRemark.Name = "numericUpDown_Vyvod2_AfterRemark";
    this.numericUpDown_Vyvod2_AfterRemark.Size = new Size(38, 20);
    this.numericUpDown_Vyvod2_AfterRemark.TabIndex = 10;
    this.numericUpDown_Vyvod2_AfterRemark.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_AfterRemark.ValueChanged += new EventHandler(this.numericUpDown_Vyvod2_AfterRemark_ValueChanged);
    this.label_Vyvod2_AfterInfo.AutoSize = true;
    this.label_Vyvod2_AfterInfo.Location = new Point(68, 33);
    this.label_Vyvod2_AfterInfo.Name = "label_Vyvod2_AfterInfo";
    this.label_Vyvod2_AfterInfo.Size = new Size(169, 13);
    this.label_Vyvod2_AfterInfo.TabIndex = 3;
    this.label_Vyvod2_AfterInfo.Text = "После информационной записи";
    this.numericUpDown_Vyvod2_AfterInfo.ForeColor = SystemColors.WindowFrame;
    this.numericUpDown_Vyvod2_AfterInfo.Location = new Point(20, 31 /*0x1F*/);
    this.numericUpDown_Vyvod2_AfterInfo.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_AfterInfo.Name = "numericUpDown_Vyvod2_AfterInfo";
    this.numericUpDown_Vyvod2_AfterInfo.Size = new Size(38, 20);
    this.numericUpDown_Vyvod2_AfterInfo.TabIndex = 2;
    this.numericUpDown_Vyvod2_AfterInfo.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_AfterInfo.ValueChanged += new EventHandler(this.numericUpDown_Vyvod2_AfterInfo_ValueChanged);
    this.group_Vyvod2_BoxLizm.Controls.Add((Control) this.checkBox_Vyvod2_IncludedLizmInDoc);
    this.group_Vyvod2_BoxLizm.Controls.Add((Control) this.label_Vyvod2_Lizm);
    this.group_Vyvod2_BoxLizm.Controls.Add((Control) this.numericUpDown_Vyvod2_Lizm);
    this.group_Vyvod2_BoxLizm.Controls.Add((Control) this.checkBox_Vyvod2_Lizm);
    this.group_Vyvod2_BoxLizm.Location = new Point(10, 16 /*0x10*/);
    this.group_Vyvod2_BoxLizm.Name = "group_Vyvod2_BoxLizm";
    this.group_Vyvod2_BoxLizm.Size = new Size(359, 100);
    this.group_Vyvod2_BoxLizm.TabIndex = 0;
    this.group_Vyvod2_BoxLizm.TabStop = false;
    this.group_Vyvod2_BoxLizm.Text = "Лист регистрации изменений";
    this.toolTip1.SetToolTip((Control) this.group_Vyvod2_BoxLizm, "Определение - когда выпускать бланк листа регистрации изменений");
    this.checkBox_Vyvod2_IncludedLizmInDoc.AutoSize = true;
    this.checkBox_Vyvod2_IncludedLizmInDoc.Location = new Point(20, 65);
    this.checkBox_Vyvod2_IncludedLizmInDoc.Name = "checkBox_Vyvod2_IncludedLizmInDoc";
    this.checkBox_Vyvod2_IncludedLizmInDoc.Size = new Size(233, 17);
    this.checkBox_Vyvod2_IncludedLizmInDoc.TabIndex = 3;
    this.checkBox_Vyvod2_IncludedLizmInDoc.Text = "Включать непосредственно в документе";
    this.toolTip1.SetToolTip((Control) this.checkBox_Vyvod2_IncludedLizmInDoc, "Выпуск бланка листа регистрации изменений определять непосредственно в документе");
    this.checkBox_Vyvod2_IncludedLizmInDoc.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod2_IncludedLizmInDoc.Visible = false;
    this.label_Vyvod2_Lizm.AutoSize = true;
    this.label_Vyvod2_Lizm.Location = new Point(233, 33);
    this.label_Vyvod2_Lizm.Name = "label_Vyvod2_Lizm";
    this.label_Vyvod2_Lizm.Size = new Size(42, 13);
    this.label_Vyvod2_Lizm.TabIndex = 2;
    this.label_Vyvod2_Lizm.Text = "листов";
    this.numericUpDown_Vyvod2_Lizm.Location = new Point(189, 29);
    this.numericUpDown_Vyvod2_Lizm.Maximum = new Decimal(new int[4]
    {
      99,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_Lizm.Name = "numericUpDown_Vyvod2_Lizm";
    this.numericUpDown_Vyvod2_Lizm.Size = new Size(38, 20);
    this.numericUpDown_Vyvod2_Lizm.TabIndex = 1;
    this.toolTip1.SetToolTip((Control) this.numericUpDown_Vyvod2_Lizm, "Определение - когда выпускать бланк листа регистрации изменений");
    this.numericUpDown_Vyvod2_Lizm.Value = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numericUpDown_Vyvod2_Lizm.ValueChanged += new EventHandler(this.numericUpDown_Vyvod2_Lizm_ValueChanged);
    this.checkBox_Vyvod2_Lizm.AutoSize = true;
    this.checkBox_Vyvod2_Lizm.Checked = true;
    this.checkBox_Vyvod2_Lizm.CheckState = CheckState.Checked;
    this.checkBox_Vyvod2_Lizm.Location = new Point(20, 32 /*0x20*/);
    this.checkBox_Vyvod2_Lizm.Name = "checkBox_Vyvod2_Lizm";
    this.checkBox_Vyvod2_Lizm.Size = new Size(132, 17);
    this.checkBox_Vyvod2_Lizm.TabIndex = 0;
    this.checkBox_Vyvod2_Lizm.Text = "Выводить, начиная с";
    this.toolTip1.SetToolTip((Control) this.checkBox_Vyvod2_Lizm, "Определение - когда выпускать бланк листа регистрации изменений");
    this.checkBox_Vyvod2_Lizm.UseVisualStyleBackColor = true;
    this.checkBox_Vyvod2_Lizm.CheckedChanged += new EventHandler(this.checkBox_Vyvod2_Lizm_CheckedChanged);
    this.tabPage_Xml.AutoScroll = true;
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Folder_In);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_EmptyString);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Out);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_In);
    this.tabPage_Xml.Controls.Add((Control) this.groupBox_Xml_Text);
    this.tabPage_Xml.Controls.Add((Control) this.treeView_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.docKcontainer_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.docContainer_Xml);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Delete);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Edit);
    this.tabPage_Xml.Controls.Add((Control) this.button_Xml_Add);
    this.tabPage_Xml.Location = new Point(4, 22);
    this.tabPage_Xml.Name = "tabPage_Xml";
    this.tabPage_Xml.Padding = new Padding(3);
    this.tabPage_Xml.Size = new Size(1576, 731);
    this.tabPage_Xml.TabIndex = 7;
    this.tabPage_Xml.Text = "XML";
    this.tabPage_Xml.UseVisualStyleBackColor = true;
    this.groupBox_Xml_Folder_In.BackColor = Color.Transparent;
    this.groupBox_Xml_Folder_In.Controls.Add((Control) this.button_Xml_Folder_In);
    this.groupBox_Xml_Folder_In.Controls.Add((Control) this.textBox_Xml_Folder_In);
    this.groupBox_Xml_Folder_In.Location = new Point(968, 629);
    this.groupBox_Xml_Folder_In.Name = "groupBox_Xml_Folder_In";
    this.groupBox_Xml_Folder_In.Size = new Size(339, 44);
    this.groupBox_Xml_Folder_In.TabIndex = 36;
    this.groupBox_Xml_Folder_In.TabStop = false;
    this.groupBox_Xml_Folder_In.Text = "Папка файлов Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Folder_In, "Папка файлов Xml");
    this.button_Xml_Folder_In.Image = (Image) Resources.Folder;
    this.button_Xml_Folder_In.Location = new Point(289, 11);
    this.button_Xml_Folder_In.Name = "button_Xml_Folder_In";
    this.button_Xml_Folder_In.Size = new Size(44, 25);
    this.button_Xml_Folder_In.TabIndex = 38;
    this.toolTip1.SetToolTip((Control) this.button_Xml_Folder_In, "Выбор папки файлов Xml");
    this.button_Xml_Folder_In.UseVisualStyleBackColor = true;
    this.button_Xml_Folder_In.Click += new EventHandler(this.button_Xml_Folder_In_Click);
    this.textBox_Xml_Folder_In.Location = new Point(6, 14);
    this.textBox_Xml_Folder_In.Name = "textBox_Xml_Folder_In";
    this.textBox_Xml_Folder_In.Size = new Size(277, 20);
    this.textBox_Xml_Folder_In.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.textBox_Xml_Folder_In, "Папка файлов Xml");
    this.textBox_Xml_Folder_In.Leave += new EventHandler(this.textBox_Xml_Folder_In_Leave);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.label_Xml_AfterRemark);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.numeric_UpDown_Xml_AfterRemark);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.label_Xml_AfterInfo);
    this.groupBox_Xml_EmptyString.Controls.Add((Control) this.numeric_UpDown_Xml_AfterInfo);
    this.groupBox_Xml_EmptyString.Location = new Point(612, 621);
    this.groupBox_Xml_EmptyString.Name = "groupBox_Xml_EmptyString";
    this.groupBox_Xml_EmptyString.Size = new Size(300, 80 /*0x50*/);
    this.groupBox_Xml_EmptyString.TabIndex = 35;
    this.groupBox_Xml_EmptyString.TabStop = false;
    this.groupBox_Xml_EmptyString.Text = "Пропуск строк при заполнении из Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_EmptyString, "Настроить правила вставки пустых строк");
    this.label_Xml_AfterRemark.AutoSize = true;
    this.label_Xml_AfterRemark.Location = new Point(59, 52);
    this.label_Xml_AfterRemark.Name = "label_Xml_AfterRemark";
    this.label_Xml_AfterRemark.Size = new Size(103, 13);
    this.label_Xml_AfterRemark.TabIndex = 11;
    this.label_Xml_AfterRemark.Text = "После примечания";
    this.toolTip1.SetToolTip((Control) this.label_Xml_AfterRemark, "Количество пустых строк между записями примечаний");
    this.numeric_UpDown_Xml_AfterRemark.Location = new Point(11, 50);
    this.numeric_UpDown_Xml_AfterRemark.Maximum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterRemark.Name = "numeric_UpDown_Xml_AfterRemark";
    this.numeric_UpDown_Xml_AfterRemark.Size = new Size(38, 20);
    this.numeric_UpDown_Xml_AfterRemark.TabIndex = 10;
    this.toolTip1.SetToolTip((Control) this.numeric_UpDown_Xml_AfterRemark, "Количество пустых строк между записями примечаний");
    this.numeric_UpDown_Xml_AfterRemark.ValueChanged += new EventHandler(this.numeric_UpDown_Xml_AfterRemark_ValueChanged);
    this.label_Xml_AfterInfo.AutoSize = true;
    this.label_Xml_AfterInfo.Location = new Point(57, 22);
    this.label_Xml_AfterInfo.Name = "label_Xml_AfterInfo";
    this.label_Xml_AfterInfo.Size = new Size(169, 13);
    this.label_Xml_AfterInfo.TabIndex = 3;
    this.label_Xml_AfterInfo.Text = "После информационной записи";
    this.toolTip1.SetToolTip((Control) this.label_Xml_AfterInfo, "Количество пустых строк между иформационными записями");
    this.numeric_UpDown_Xml_AfterInfo.ForeColor = SystemColors.WindowFrame;
    this.numeric_UpDown_Xml_AfterInfo.Location = new Point(9, 20);
    this.numeric_UpDown_Xml_AfterInfo.Maximum = new Decimal(new int[4]
    {
      5,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterInfo.Name = "numeric_UpDown_Xml_AfterInfo";
    this.numeric_UpDown_Xml_AfterInfo.Size = new Size(38, 20);
    this.numeric_UpDown_Xml_AfterInfo.TabIndex = 2;
    this.toolTip1.SetToolTip((Control) this.numeric_UpDown_Xml_AfterInfo, "Количество пустых строк между иформационными записями");
    this.numeric_UpDown_Xml_AfterInfo.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_UpDown_Xml_AfterInfo.ValueChanged += new EventHandler(this.numeric_UpDown_Xml_AfterInfo_ValueChanged);
    this.groupBox_Xml_Out.BackColor = Color.Transparent;
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassportOutNo);
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassportOutDialog);
    this.groupBox_Xml_Out.Controls.Add((Control) this.radioButton_Xml_PassporOutAlways);
    this.groupBox_Xml_Out.Location = new Point(968, 537);
    this.groupBox_Xml_Out.Name = "groupBox_Xml_Out";
    this.groupBox_Xml_Out.Size = new Size(339, 80 /*0x50*/);
    this.groupBox_Xml_Out.TabIndex = 34;
    this.groupBox_Xml_Out.TabStop = false;
    this.groupBox_Xml_Out.Text = "Вывод основной надписи в Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Out, "Порядок dsdjlf основной надписи в файл Xml");
    this.radioButton_Xml_PassportOutNo.AutoSize = true;
    this.radioButton_Xml_PassportOutNo.Location = new Point(14, 56);
    this.radioButton_Xml_PassportOutNo.Name = "radioButton_Xml_PassportOutNo";
    this.radioButton_Xml_PassportOutNo.Size = new Size(91, 17);
    this.radioButton_Xml_PassportOutNo.TabIndex = 2;
    this.radioButton_Xml_PassportOutNo.Text = "Не выводить";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportOutNo, "Данные основной надписи не выводить");
    this.radioButton_Xml_PassportOutNo.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportOutNo.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.radioButton_Xml_PassportOutDialog.AutoSize = true;
    this.radioButton_Xml_PassportOutDialog.Location = new Point(14, 36);
    this.radioButton_Xml_PassportOutDialog.Name = "radioButton_Xml_PassportOutDialog";
    this.radioButton_Xml_PassportOutDialog.Size = new Size(76, 17);
    this.radioButton_Xml_PassportOutDialog.TabIndex = 1;
    this.radioButton_Xml_PassportOutDialog.Text = "В диалоге";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportOutDialog, "При выводе задавать вопрос \"Выводить данные основной надписи?\"");
    this.radioButton_Xml_PassportOutDialog.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportOutDialog.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.radioButton_Xml_PassporOutAlways.AutoSize = true;
    this.radioButton_Xml_PassporOutAlways.Checked = true;
    this.radioButton_Xml_PassporOutAlways.Location = new Point(14, 16 /*0x10*/);
    this.radioButton_Xml_PassporOutAlways.Name = "radioButton_Xml_PassporOutAlways";
    this.radioButton_Xml_PassporOutAlways.Size = new Size(113, 17);
    this.radioButton_Xml_PassporOutAlways.TabIndex = 0;
    this.radioButton_Xml_PassporOutAlways.TabStop = true;
    this.radioButton_Xml_PassporOutAlways.Text = "Выводить всегда";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassporOutAlways, "Данные основной надписи выводить всегда");
    this.radioButton_Xml_PassporOutAlways.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassporOutAlways.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporOut);
    this.groupBox_Xml_In.BackColor = Color.Transparent;
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassportInNo);
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassportInDialog);
    this.groupBox_Xml_In.Controls.Add((Control) this.radioButton_Xml_PassporInAlways);
    this.groupBox_Xml_In.Location = new Point(612, 537);
    this.groupBox_Xml_In.Name = "groupBox_Xml_In";
    this.groupBox_Xml_In.Size = new Size(300, 80 /*0x50*/);
    this.groupBox_Xml_In.TabIndex = 33;
    this.groupBox_Xml_In.TabStop = false;
    this.groupBox_Xml_In.Text = "Ввод основной надписи из Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_In, "Порядок заполнения основной надписи из файла Xml");
    this.radioButton_Xml_PassportInNo.AutoSize = true;
    this.radioButton_Xml_PassportInNo.Location = new Point(14, 56);
    this.radioButton_Xml_PassportInNo.Name = "radioButton_Xml_PassportInNo";
    this.radioButton_Xml_PassportInNo.Size = new Size(75, 17);
    this.radioButton_Xml_PassportInNo.TabIndex = 5;
    this.radioButton_Xml_PassportInNo.Text = "Не читать";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportInNo, "Основную надпись из файла Xml не читать");
    this.radioButton_Xml_PassportInNo.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportInNo.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.radioButton_Xml_PassportInDialog.AutoSize = true;
    this.radioButton_Xml_PassportInDialog.Location = new Point(14, 36);
    this.radioButton_Xml_PassportInDialog.Name = "radioButton_Xml_PassportInDialog";
    this.radioButton_Xml_PassportInDialog.Size = new Size(76, 17);
    this.radioButton_Xml_PassportInDialog.TabIndex = 4;
    this.radioButton_Xml_PassportInDialog.Text = "В диалоге";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassportInDialog, "При чтении задавать вопрос \"Читать данные основной надписи?\"");
    this.radioButton_Xml_PassportInDialog.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassportInDialog.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.radioButton_Xml_PassporInAlways.AutoSize = true;
    this.radioButton_Xml_PassporInAlways.Checked = true;
    this.radioButton_Xml_PassporInAlways.Location = new Point(14, 16 /*0x10*/);
    this.radioButton_Xml_PassporInAlways.Name = "radioButton_Xml_PassporInAlways";
    this.radioButton_Xml_PassporInAlways.Size = new Size(99, 17);
    this.radioButton_Xml_PassporInAlways.TabIndex = 3;
    this.radioButton_Xml_PassporInAlways.TabStop = true;
    this.radioButton_Xml_PassporInAlways.Text = "Читать всегда";
    this.toolTip1.SetToolTip((Control) this.radioButton_Xml_PassporInAlways, "Основную надпись читать из файла Xml всегда");
    this.radioButton_Xml_PassporInAlways.UseVisualStyleBackColor = true;
    this.radioButton_Xml_PassporInAlways.MouseClick += new MouseEventHandler(this.radioButton_Xml_PassporIn);
    this.groupBox_Xml_Text.BackColor = Color.Transparent;
    this.groupBox_Xml_Text.Controls.Add((Control) this.textBox_Xml_Text);
    this.groupBox_Xml_Text.Location = new Point(320, 537);
    this.groupBox_Xml_Text.Name = "groupBox_Xml_Text";
    this.groupBox_Xml_Text.Size = new Size(250, 44);
    this.groupBox_Xml_Text.TabIndex = 30;
    this.groupBox_Xml_Text.TabStop = false;
    this.groupBox_Xml_Text.Text = "Имя Xml";
    this.toolTip1.SetToolTip((Control) this.groupBox_Xml_Text, "Имя поля в файде Xml");
    this.textBox_Xml_Text.Location = new Point(6, 14);
    this.textBox_Xml_Text.Name = "textBox_Xml_Text";
    this.textBox_Xml_Text.Size = new Size(238, 20);
    this.textBox_Xml_Text.TabIndex = 0;
    this.toolTip1.SetToolTip((Control) this.textBox_Xml_Text, "Имя поля в файде Xml");
    this.textBox_Xml_Text.TextChanged += new EventHandler(this.textBox_Xml_Text_TextChanged);
    this.textBox_Xml_Text.KeyDown += new KeyEventHandler(this.textBox_Xml_Text_KeyDown);
    this.treeView_Xml.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.treeView_Xml.HideSelection = false;
    this.treeView_Xml.Location = new Point(10, 3);
    this.treeView_Xml.Name = "treeView_Xml";
    this.treeView_Xml.Size = new Size(586, 525);
    this.treeView_Xml.TabIndex = 26;
    this.treeView_Xml.AfterSelect += new TreeViewEventHandler(this.treeView_Xml_AfterSelect);
    this.treeView_Xml.KeyDown += new KeyEventHandler(this.treeView_Xml_KeyDown);
    this.docKcontainer_Xml.Dock = DockStyle.Right;
    this.docKcontainer_Xml.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.docKcontainer_Xml.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docKcontainer_Xml.Location = new Point(1573, 3);
    this.docKcontainer_Xml.Manager = this.dockMan_Xml;
    this.docKcontainer_Xml.Name = "docKcontainer_Xml";
    this.docKcontainer_Xml.Renderer = (RendererBase) null;
    this.docKcontainer_Xml.Size = new Size(0, 725);
    this.docKcontainer_Xml.TabIndex = 25;
    this.dockMan_Xml.DocumentContainer = this.docContainer_Xml;
    this.dockMan_Xml.OwnerForm = (Form) this;
    this.docContainer_Xml.Dock = DockStyle.None;
    this.docContainer_Xml.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Xml.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docContainer_Xml.Location = new Point(612, 1);
    this.docContainer_Xml.Manager = (DockManager) null;
    this.docContainer_Xml.Name = "docContainer_Xml";
    this.docContainer_Xml.Renderer = (RendererBase) null;
    this.docContainer_Xml.Size = new Size(695, 525);
    this.docContainer_Xml.TabIndex = 18;
    this.button_Xml_Delete.Image = (Image) componentResourceManager.GetObject("button_Xml_Delete.Image");
    this.button_Xml_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Delete.Location = new Point(112 /*0x70*/, 629);
    this.button_Xml_Delete.Name = "button_Xml_Delete";
    this.button_Xml_Delete.Size = new Size(202, 27);
    this.button_Xml_Delete.TabIndex = 29;
    this.button_Xml_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Delete, "Удалить текущую строку");
    this.button_Xml_Delete.UseVisualStyleBackColor = true;
    this.button_Xml_Delete.Click += new EventHandler(this.button_Xml_Delete_Click);
    this.button_Xml_Edit.Image = (Image) componentResourceManager.GetObject("button_Xml_Edit.Image");
    this.button_Xml_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Edit.Location = new Point(112 /*0x70*/, 547);
    this.button_Xml_Edit.Name = "button_Xml_Edit";
    this.button_Xml_Edit.Size = new Size(202, 27);
    this.button_Xml_Edit.TabIndex = 28;
    this.button_Xml_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Edit, "Изменить текущую строку");
    this.button_Xml_Edit.UseVisualStyleBackColor = true;
    this.button_Xml_Edit.Click += new EventHandler(this.button_Xml_Edit_Click);
    this.button_Xml_Add.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Xml_Add.Image = (Image) componentResourceManager.GetObject("button_Xml_Add.Image");
    this.button_Xml_Add.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Xml_Add.Location = new Point(112 /*0x70*/, 587);
    this.button_Xml_Add.Name = "button_Xml_Add";
    this.button_Xml_Add.Size = new Size(202, 27);
    this.button_Xml_Add.TabIndex = 27;
    this.button_Xml_Add.Text = "Добавить";
    this.toolTip1.SetToolTip((Control) this.button_Xml_Add, "Добавить");
    this.button_Xml_Add.UseVisualStyleBackColor = true;
    this.button_Xml_Add.Click += new EventHandler(this.button_Xml_Add_Click);
    this.tabPage_Avs6.AutoScroll = true;
    this.tabPage_Avs6.Controls.Add((Control) this.button_Avs_Obshaia);
    this.tabPage_Avs6.Controls.Add((Control) this.button_Avs_PoRazdelam);
    this.tabPage_Avs6.Controls.Add((Control) this.panel_Avs_1);
    this.tabPage_Avs6.Controls.Add((Control) this.groupBox_Avs6_Fields);
    this.tabPage_Avs6.Controls.Add((Control) this.docContainer_Avs);
    this.tabPage_Avs6.Controls.Add((Control) this.dockContainer_Avs);
    this.tabPage_Avs6.Location = new Point(4, 22);
    this.tabPage_Avs6.Name = "tabPage_Avs6";
    this.tabPage_Avs6.Padding = new Padding(3);
    this.tabPage_Avs6.Size = new Size(1576, 731);
    this.tabPage_Avs6.TabIndex = 8;
    this.tabPage_Avs6.Text = "Ввод документов AVS6";
    this.tabPage_Avs6.UseVisualStyleBackColor = true;
    this.button_Avs_Obshaia.Location = new Point(620, 655);
    this.button_Avs_Obshaia.Name = "button_Avs_Obshaia";
    this.button_Avs_Obshaia.Size = new Size(202, 27);
    this.button_Avs_Obshaia.TabIndex = 27;
    this.button_Avs_Obshaia.Text = "Общая";
    this.button_Avs_Obshaia.UseVisualStyleBackColor = true;
    this.button_Avs_Obshaia.Visible = false;
    this.button_Avs_Obshaia.Click += new EventHandler(this.button_Avs_Obshaia_Click);
    this.button_Avs_PoRazdelam.Location = new Point(620, 655);
    this.button_Avs_PoRazdelam.Name = "button_Avs_PoRazdelam";
    this.button_Avs_PoRazdelam.Size = new Size(202, 27);
    this.button_Avs_PoRazdelam.TabIndex = 26;
    this.button_Avs_PoRazdelam.Text = "По разделам";
    this.button_Avs_PoRazdelam.UseVisualStyleBackColor = true;
    this.button_Avs_PoRazdelam.Click += new EventHandler(this.button_Avs_PoRazdelam_Click);
    this.panel_Avs_1.Controls.Add((Control) this.groupBox_Avs_Forma);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_AddAttribut);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_Delete);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_Edit);
    this.panel_Avs_1.Controls.Add((Control) this.button_Avs_AddCell);
    this.panel_Avs_1.Controls.Add((Control) this.groupBox_Avs_TextRazdelitel);
    this.panel_Avs_1.Controls.Add((Control) this.treeView_Avs);
    this.panel_Avs_1.Location = new Point(266, 6);
    this.panel_Avs_1.Name = "panel_Avs_1";
    this.panel_Avs_1.Size = new Size(348, 717);
    this.panel_Avs_1.TabIndex = 19;
    this.groupBox_Avs_Forma.Controls.Add((Control) this.radioButton_Avs_GroupB);
    this.groupBox_Avs_Forma.Controls.Add((Control) this.radioButton_Avs_EdOrA);
    this.groupBox_Avs_Forma.Controls.Add((Control) this.numeric_Avs_UpDownKolGraf);
    this.groupBox_Avs_Forma.Controls.Add((Control) this.label_Avs_Graf);
    this.groupBox_Avs_Forma.Location = new Point(11, 613);
    this.groupBox_Avs_Forma.Name = "groupBox_Avs_Forma";
    this.groupBox_Avs_Forma.Size = new Size(199, 96 /*0x60*/);
    this.groupBox_Avs_Forma.TabIndex = 17;
    this.groupBox_Avs_Forma.TabStop = false;
    this.groupBox_Avs_Forma.Text = "Форма";
    this.radioButton_Avs_GroupB.AutoSize = true;
    this.radioButton_Avs_GroupB.Location = new Point(6, 42);
    this.radioButton_Avs_GroupB.Name = "radioButton_Avs_GroupB";
    this.radioButton_Avs_GroupB.Size = new Size(88, 17);
    this.radioButton_Avs_GroupB.TabIndex = 23;
    this.radioButton_Avs_GroupB.Text = "Групповой Б";
    this.toolTip1.SetToolTip((Control) this.radioButton_Avs_GroupB, "Перейти к настройкам групповой формы Б");
    this.radioButton_Avs_GroupB.UseVisualStyleBackColor = true;
    this.radioButton_Avs_GroupB.Click += new EventHandler(this.radioButton_Avs_GroupB_Click);
    this.radioButton_Avs_EdOrA.AutoSize = true;
    this.radioButton_Avs_EdOrA.Checked = true;
    this.radioButton_Avs_EdOrA.Location = new Point(6, 19);
    this.radioButton_Avs_EdOrA.Name = "radioButton_Avs_EdOrA";
    this.radioButton_Avs_EdOrA.Size = new Size(112 /*0x70*/, 17);
    this.radioButton_Avs_EdOrA.TabIndex = 22;
    this.radioButton_Avs_EdOrA.TabStop = true;
    this.radioButton_Avs_EdOrA.Text = "Единичный или А";
    this.toolTip1.SetToolTip((Control) this.radioButton_Avs_EdOrA, "Перейти к настройкам единичной формы и групповой формы А");
    this.radioButton_Avs_EdOrA.UseVisualStyleBackColor = true;
    this.radioButton_Avs_EdOrA.Click += new EventHandler(this.radioButton_Avs_EdOrA_Click);
    this.numeric_Avs_UpDownKolGraf.Location = new Point(112 /*0x70*/, 66);
    this.numeric_Avs_UpDownKolGraf.Maximum = new Decimal(new int[4]
    {
      10,
      0,
      0,
      0
    });
    this.numeric_Avs_UpDownKolGraf.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_Avs_UpDownKolGraf.Name = "numeric_Avs_UpDownKolGraf";
    this.numeric_Avs_UpDownKolGraf.Size = new Size(53, 20);
    this.numeric_Avs_UpDownKolGraf.TabIndex = 20;
    this.numeric_Avs_UpDownKolGraf.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numeric_Avs_UpDownKolGraf.Visible = false;
    this.label_Avs_Graf.Location = new Point(3, 68);
    this.label_Avs_Graf.Name = "label_Avs_Graf";
    this.label_Avs_Graf.Size = new Size(103, 18);
    this.label_Avs_Graf.TabIndex = 21;
    this.label_Avs_Graf.Text = "Граф кол.:";
    this.label_Avs_Graf.TextAlign = ContentAlignment.TopRight;
    this.label_Avs_Graf.Visible = false;
    this.button_Avs_AddAttribut.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Avs_AddAttribut.Enabled = false;
    this.button_Avs_AddAttribut.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_AddAttribut.Location = new Point(11, 491);
    this.button_Avs_AddAttribut.Name = "button_Avs_AddAttribut";
    this.button_Avs_AddAttribut.Size = new Size(202, 27);
    this.button_Avs_AddAttribut.TabIndex = 16 /*0x10*/;
    this.button_Avs_AddAttribut.Text = "Добавить поле Avs";
    this.toolTip1.SetToolTip((Control) this.button_Avs_AddAttribut, "Добавить поле Avs");
    this.button_Avs_AddAttribut.UseVisualStyleBackColor = true;
    this.button_Avs_AddAttribut.Click += new EventHandler(this.button_Avs_AddAttribut_Click);
    this.button_Avs_Delete.Enabled = false;
    this.button_Avs_Delete.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_Delete.Location = new Point(11, 563);
    this.button_Avs_Delete.Name = "button_Avs_Delete";
    this.button_Avs_Delete.Size = new Size(202, 27);
    this.button_Avs_Delete.TabIndex = 15;
    this.button_Avs_Delete.Text = "Удалить";
    this.toolTip1.SetToolTip((Control) this.button_Avs_Delete, "Удалить текущее условие");
    this.button_Avs_Delete.UseVisualStyleBackColor = true;
    this.button_Avs_Delete.Click += new EventHandler(this.button_Avs_Delete_Click);
    this.button_Avs_Edit.Enabled = false;
    this.button_Avs_Edit.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_Edit.Location = new Point(11, 529);
    this.button_Avs_Edit.Name = "button_Avs_Edit";
    this.button_Avs_Edit.Size = new Size(202, 27);
    this.button_Avs_Edit.TabIndex = 14;
    this.button_Avs_Edit.Text = "Изменить";
    this.toolTip1.SetToolTip((Control) this.button_Avs_Edit, "Изменить текущее условие согласно выбранным параметрам");
    this.button_Avs_Edit.UseVisualStyleBackColor = true;
    this.button_Avs_Edit.Click += new EventHandler(this.button_Avs_Edit_Click);
    this.button_Avs_AddCell.AccessibleRole = AccessibleRole.OutlineButton;
    this.button_Avs_AddCell.Enabled = false;
    this.button_Avs_AddCell.ImageAlign = ContentAlignment.MiddleRight;
    this.button_Avs_AddCell.Location = new Point(11, 453);
    this.button_Avs_AddCell.Name = "button_Avs_AddCell";
    this.button_Avs_AddCell.Size = new Size(202, 27);
    this.button_Avs_AddCell.TabIndex = 13;
    this.button_Avs_AddCell.Text = "Добавить ячейку";
    this.toolTip1.SetToolTip((Control) this.button_Avs_AddCell, "Добавить ячейку");
    this.button_Avs_AddCell.UseVisualStyleBackColor = true;
    this.button_Avs_AddCell.Click += new EventHandler(this.button_Avs_AddCell_Click);
    this.groupBox_Avs_TextRazdelitel.Controls.Add((Control) this.comboBox_Avs_TextRazdelitel);
    this.groupBox_Avs_TextRazdelitel.Location = new Point(11, 405);
    this.groupBox_Avs_TextRazdelitel.Name = "groupBox_Avs_TextRazdelitel";
    this.groupBox_Avs_TextRazdelitel.Size = new Size(202, 41);
    this.groupBox_Avs_TextRazdelitel.TabIndex = 12;
    this.groupBox_Avs_TextRazdelitel.TabStop = false;
    this.groupBox_Avs_TextRazdelitel.Text = "Текст разделитель";
    this.toolTip1.SetToolTip((Control) this.groupBox_Avs_TextRazdelitel, "Текст (символ) разделитель, отделяющий отделяющий от предыдущего текста");
    this.comboBox_Avs_TextRazdelitel.Dock = DockStyle.Fill;
    this.comboBox_Avs_TextRazdelitel.FormattingEnabled = true;
    this.comboBox_Avs_TextRazdelitel.Items.AddRange(new object[9]
    {
      (object) "  (пробел)",
      (object) "(неразрывный пробел)",
      (object) "(без пробела)",
      (object) "(принудительный перенос)",
      (object) ". (точка)",
      (object) ", (запятая)",
      (object) "* (звездочка)",
      (object) "- (минус)",
      (object) "(неразрывный дефис)"
    });
    this.comboBox_Avs_TextRazdelitel.Location = new Point(3, 16 /*0x10*/);
    this.comboBox_Avs_TextRazdelitel.Name = "comboBox_Avs_TextRazdelitel";
    this.comboBox_Avs_TextRazdelitel.Size = new Size(196, 21);
    this.comboBox_Avs_TextRazdelitel.TabIndex = 3;
    this.treeView_Avs.HideSelection = false;
    this.treeView_Avs.Location = new Point(6, 6);
    this.treeView_Avs.Name = "treeView_Avs";
    this.treeView_Avs.Size = new Size(342, 388);
    this.treeView_Avs.TabIndex = 11;
    this.treeView_Avs.AfterSelect += new TreeViewEventHandler(this.treeView_Avs_AfterSelect);
    this.groupBox_Avs6_Fields.Controls.Add((Control) this.listBox_Avs6_Fields);
    this.groupBox_Avs6_Fields.Location = new Point(10, 6);
    this.groupBox_Avs6_Fields.Name = "groupBox_Avs6_Fields";
    this.groupBox_Avs6_Fields.Size = new Size(250, 717);
    this.groupBox_Avs6_Fields.TabIndex = 7;
    this.groupBox_Avs6_Fields.TabStop = false;
    this.groupBox_Avs6_Fields.Text = "Список полей записей AVS6";
    this.listBox_Avs6_Fields.Dock = DockStyle.Fill;
    this.listBox_Avs6_Fields.FormattingEnabled = true;
    this.listBox_Avs6_Fields.Location = new Point(3, 16 /*0x10*/);
    this.listBox_Avs6_Fields.Name = "listBox_Avs6_Fields";
    this.listBox_Avs6_Fields.Size = new Size(244, 698);
    this.listBox_Avs6_Fields.TabIndex = 0;
    this.docContainer_Avs.Dock = DockStyle.None;
    this.docContainer_Avs.Guid = new Guid("adadfb01-16c4-4a32-8733-a11ec038a68c");
    this.docContainer_Avs.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docContainer_Avs.Location = new Point(620, 1);
    this.docContainer_Avs.Manager = (DockManager) null;
    this.docContainer_Avs.Name = "docContainer_Avs";
    this.docContainer_Avs.Renderer = (RendererBase) null;
    this.docContainer_Avs.Size = new Size(690, 633);
    this.docContainer_Avs.TabIndex = 20;
    this.dockContainer_Avs.Dock = DockStyle.Right;
    this.dockContainer_Avs.Guid = new Guid("6c63e3af-951f-4d98-b09e-be3ffba040c1");
    this.dockContainer_Avs.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.dockContainer_Avs.Location = new Point(1573, 3);
    this.dockContainer_Avs.Manager = this.dockMan_Avs;
    this.dockContainer_Avs.Name = "dockContainer_Avs";
    this.dockContainer_Avs.Renderer = (RendererBase) null;
    this.dockContainer_Avs.Size = new Size(0, 725);
    this.dockContainer_Avs.TabIndex = 26;
    this.dockMan_Avs.DocumentContainer = this.docContainer_Avs;
    this.dockMan_Avs.OwnerForm = (Form) this;
    this.tabPage_Service.AutoScroll = true;
    this.tabPage_Service.Controls.Add((Control) this.checkBox_Services_autoSbor);
    this.tabPage_Service.Controls.Add((Control) this.label_DumpFolder);
    this.tabPage_Service.Controls.Add((Control) this.groupBox_AccessLevel);
    this.tabPage_Service.Controls.Add((Control) this.checkBox_Services_CreateDump);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesFileOpen);
    this.tabPage_Service.Controls.Add((Control) this.label_ServiceCreateDump);
    this.tabPage_Service.Controls.Add((Control) this.label_SevicesForGroupB);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesTypeVedTo);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesCopyAll);
    this.tabPage_Service.Controls.Add((Control) this.label_ServicesDefaultAll);
    this.tabPage_Service.Controls.Add((Control) this.buttonSevicesForGroupB);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesFileOpen);
    this.tabPage_Service.Controls.Add((Control) this.buttonServiceCreateDump);
    this.tabPage_Service.Controls.Add((Control) this.labelService2);
    this.tabPage_Service.Controls.Add((Control) this.labelService1);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesTypeVedTo);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesCopyAll);
    this.tabPage_Service.Controls.Add((Control) this.buttonServicesDefaultAll);
    this.tabPage_Service.Controls.Add((Control) this.groupBox_Dump);
    this.tabPage_Service.Location = new Point(4, 22);
    this.tabPage_Service.Name = "tabPage_Service";
    this.tabPage_Service.Padding = new Padding(3);
    this.tabPage_Service.Size = new Size(1576, 731);
    this.tabPage_Service.TabIndex = 6;
    this.tabPage_Service.Text = "Сервис";
    this.tabPage_Service.UseVisualStyleBackColor = true;
    this.checkBox_Services_autoSbor.AutoSize = true;
    this.checkBox_Services_autoSbor.Checked = true;
    this.checkBox_Services_autoSbor.CheckState = CheckState.Checked;
    this.checkBox_Services_autoSbor.Location = new Point(818, 35);
    this.checkBox_Services_autoSbor.Name = "checkBox_Services_autoSbor";
    this.checkBox_Services_autoSbor.Size = new Size(218, 17);
    this.checkBox_Services_autoSbor.TabIndex = 36;
    this.checkBox_Services_autoSbor.Text = "Создавать ведомость автоматически";
    this.toolTip1.SetToolTip((Control) this.checkBox_Services_autoSbor, "Возможность автоматического создания ведомости для данной спецификации (изделия)");
    this.checkBox_Services_autoSbor.UseVisualStyleBackColor = true;
    this.checkBox_Services_autoSbor.MouseClick += new MouseEventHandler(this.checkBox_Services_autoSbor_MouseClick);
    this.label_DumpFolder.AutoSize = true;
    this.label_DumpFolder.Location = new Point(679, 416);
    this.label_DumpFolder.Name = "label_DumpFolder";
    this.label_DumpFolder.Size = new Size(0, 13);
    this.label_DumpFolder.TabIndex = 35;
    this.label_DumpFolder.Visible = false;
    this.groupBox_AccessLevel.BackColor = Color.Transparent;
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel2);
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel1);
    this.groupBox_AccessLevel.Controls.Add((Control) this.radioButton_AccessLevel0);
    this.groupBox_AccessLevel.Location = new Point(25, 510);
    this.groupBox_AccessLevel.Name = "groupBox_AccessLevel";
    this.groupBox_AccessLevel.Size = new Size(650, 83);
    this.groupBox_AccessLevel.TabIndex = 33;
    this.groupBox_AccessLevel.TabStop = false;
    this.groupBox_AccessLevel.Text = "Уровень доступа к настройке";
    this.radioButton_AccessLevel2.AutoSize = true;
    this.radioButton_AccessLevel2.Checked = true;
    this.radioButton_AccessLevel2.Location = new Point(6, 54);
    this.radioButton_AccessLevel2.Name = "radioButton_AccessLevel2";
    this.radioButton_AccessLevel2.Size = new Size(44, 17);
    this.radioButton_AccessLevel2.TabIndex = 2;
    this.radioButton_AccessLevel2.TabStop = true;
    this.radioButton_AccessLevel2.Text = "Все";
    this.radioButton_AccessLevel2.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel2.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel2_MouseClick);
    this.radioButton_AccessLevel1.AutoSize = true;
    this.radioButton_AccessLevel1.Location = new Point(6, 34);
    this.radioButton_AccessLevel1.Name = "radioButton_AccessLevel1";
    this.radioButton_AccessLevel1.Size = new Size(234, 17);
    this.radioButton_AccessLevel1.TabIndex = 1;
    this.radioButton_AccessLevel1.Text = "Пользователь с ролью \"Администратор\"";
    this.radioButton_AccessLevel1.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel1.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel1_MouseClick);
    this.radioButton_AccessLevel0.AutoSize = true;
    this.radioButton_AccessLevel0.Location = new Point(6, 14);
    this.radioButton_AccessLevel0.Name = "radioButton_AccessLevel0";
    this.radioButton_AccessLevel0.Size = new Size(302, 17);
    this.radioButton_AccessLevel0.TabIndex = 0;
    this.radioButton_AccessLevel0.Text = "Пользователь с именем \"Системный администратор\"";
    this.radioButton_AccessLevel0.UseVisualStyleBackColor = true;
    this.radioButton_AccessLevel0.MouseClick += new MouseEventHandler(this.radioButton_AccessLevel0_MouseClick);
    this.checkBox_Services_CreateDump.AutoSize = true;
    this.checkBox_Services_CreateDump.Location = new Point(43, 415);
    this.checkBox_Services_CreateDump.Name = "checkBox_Services_CreateDump";
    this.checkBox_Services_CreateDump.Size = new Size(314, 17);
    this.checkBox_Services_CreateDump.TabIndex = 32 /*0x20*/;
    this.checkBox_Services_CreateDump.Text = "Создавать протоколы и Dump в текущем сеансе работы";
    this.checkBox_Services_CreateDump.UseVisualStyleBackColor = true;
    this.checkBox_Services_CreateDump.CheckedChanged += new EventHandler(this.checkBox_Services_CreateDump_CheckedChanged);
    this.label_ServicesFileOpen.AutoSize = true;
    this.label_ServicesFileOpen.Location = new Point(243, 355);
    this.label_ServicesFileOpen.Name = "label_ServicesFileOpen";
    this.label_ServicesFileOpen.Size = new Size(257, 13);
    this.label_ServicesFileOpen.TabIndex = 25;
    this.label_ServicesFileOpen.Text = "Прочитать параметры настроек из файла (Dump)";
    this.label_ServiceCreateDump.AutoSize = true;
    this.label_ServiceCreateDump.Location = new Point(243, 305);
    this.label_ServiceCreateDump.Name = "label_ServiceCreateDump";
    this.label_ServiceCreateDump.Size = new Size(256 /*0x0100*/, 13);
    this.label_ServiceCreateDump.TabIndex = 24;
    this.label_ServiceCreateDump.Text = "Создать Dump (Сохраняются настройки, шаблон)";
    this.label_SevicesForGroupB.AutoSize = true;
    this.label_SevicesForGroupB.Location = new Point(230, 185);
    this.label_SevicesForGroupB.Name = "label_SevicesForGroupB";
    this.label_SevicesForGroupB.Size = new Size(270, 13);
    this.label_SevicesForGroupB.TabIndex = 23;
    this.label_SevicesForGroupB.Text = "Выбор шаблона для групповой ведомости формы Б";
    this.label_SevicesForGroupB.Visible = false;
    this.label_ServicesTypeVedTo.Location = new Point(230, 132);
    this.label_ServicesTypeVedTo.Name = "label_ServicesTypeVedTo";
    this.label_ServicesTypeVedTo.Size = new Size(510, 36);
    this.label_ServicesTypeVedTo.TabIndex = 22;
    this.label_ServicesTypeVedTo.Text = "Текущему виду ведомости присвоить свойства определенной системной ведомости, например \"Ведомость покупных изделий\"";
    this.label_ServicesCopyAll.Location = new Point(230, 82);
    this.label_ServicesCopyAll.Name = "label_ServicesCopyAll";
    this.label_ServicesCopyAll.Size = new Size(510, 34);
    this.label_ServicesCopyAll.TabIndex = 21;
    this.label_ServicesCopyAll.Text = "Всем значениям настройки для текущего типа ведомости копировать значения из другой ведомости";
    this.label_ServicesDefaultAll.Location = new Point(230, 32 /*0x20*/);
    this.label_ServicesDefaultAll.Name = "label_ServicesDefaultAll";
    this.label_ServicesDefaultAll.Size = new Size(510, 32 /*0x20*/);
    this.label_ServicesDefaultAll.TabIndex = 20;
    this.label_ServicesDefaultAll.Text = "Всем значениям настройки для текущего типа ведомости присвоить значения по умолчанию";
    this.buttonSevicesForGroupB.Location = new Point(20, 178);
    this.buttonSevicesForGroupB.Name = "buttonSevicesForGroupB";
    this.buttonSevicesForGroupB.Size = new Size(168, 27);
    this.buttonSevicesForGroupB.TabIndex = 19;
    this.buttonSevicesForGroupB.Text = "Шаблон Б";
    this.toolTip1.SetToolTip((Control) this.buttonSevicesForGroupB, "Выбор шаблона для групповой ведомости формы Б");
    this.buttonSevicesForGroupB.UseVisualStyleBackColor = true;
    this.buttonSevicesForGroupB.Visible = false;
    this.buttonSevicesForGroupB.Click += new EventHandler(this.buttonSevicesForGroupB_Click);
    this.buttonServicesFileOpen.Location = new Point(33, 348);
    this.buttonServicesFileOpen.Name = "buttonServicesFileOpen";
    this.buttonServicesFileOpen.Size = new Size(168, 27);
    this.buttonServicesFileOpen.TabIndex = 18;
    this.buttonServicesFileOpen.Text = "Читать из файла";
    this.toolTip1.SetToolTip((Control) this.buttonServicesFileOpen, "Прочитать параметры настроек из файла (Dump)");
    this.buttonServicesFileOpen.UseVisualStyleBackColor = true;
    this.buttonServicesFileOpen.Click += new EventHandler(this.buttonServicesFileOpen_Click);
    this.buttonServiceCreateDump.Location = new Point(33, 298);
    this.buttonServiceCreateDump.Name = "buttonServiceCreateDump";
    this.buttonServiceCreateDump.Size = new Size(168, 27);
    this.buttonServiceCreateDump.TabIndex = 17;
    this.buttonServiceCreateDump.Text = "Создать Dump";
    this.buttonServiceCreateDump.UseVisualStyleBackColor = true;
    this.buttonServiceCreateDump.Click += new EventHandler(this.buttonServiceDump_Click);
    this.labelService2.AutoSize = true;
    this.labelService2.Location = new Point(300, 707);
    this.labelService2.Name = "labelService2";
    this.labelService2.Size = new Size(0, 13);
    this.labelService2.TabIndex = 15;
    this.labelService1.AutoSize = true;
    this.labelService1.Location = new Point(17, 707);
    this.labelService1.Name = "labelService1";
    this.labelService1.Size = new Size(0, 13);
    this.labelService1.TabIndex = 14;
    this.buttonServicesTypeVedTo.Location = new Point(20, 125);
    this.buttonServicesTypeVedTo.Name = "buttonServicesTypeVedTo";
    this.buttonServicesTypeVedTo.Size = new Size(168, 27);
    this.buttonServicesTypeVedTo.TabIndex = 13;
    this.buttonServicesTypeVedTo.Text = "Тип ведомости";
    this.toolTip1.SetToolTip((Control) this.buttonServicesTypeVedTo, "Текущему виду ведомости присвоить свойства определенной системной ведомости, например \"Ведомость покупных изделий\"");
    this.buttonServicesTypeVedTo.UseVisualStyleBackColor = true;
    this.buttonServicesTypeVedTo.Click += new EventHandler(this.buttonTypeVedTo_Click);
    this.buttonServicesCopyAll.Location = new Point(20, 75);
    this.buttonServicesCopyAll.Name = "buttonServicesCopyAll";
    this.buttonServicesCopyAll.Size = new Size(168, 27);
    this.buttonServicesCopyAll.TabIndex = 12;
    this.buttonServicesCopyAll.Text = "Копировать все из ...";
    this.toolTip1.SetToolTip((Control) this.buttonServicesCopyAll, "Все значениям настройки для текущего типа ведомости копировать значения из другой ведомости");
    this.buttonServicesCopyAll.UseVisualStyleBackColor = true;
    this.buttonServicesCopyAll.Click += new EventHandler(this.buttonCopyAll_Click);
    this.buttonServicesDefaultAll.Location = new Point(20, 25);
    this.buttonServicesDefaultAll.Name = "buttonServicesDefaultAll";
    this.buttonServicesDefaultAll.Size = new Size(168, 27);
    this.buttonServicesDefaultAll.TabIndex = 11;
    this.buttonServicesDefaultAll.Text = "По умолчанию все";
    this.toolTip1.SetToolTip((Control) this.buttonServicesDefaultAll, "Всем значениям настройки для текущего типа ведомости присвоить значения по умолчанию");
    this.buttonServicesDefaultAll.UseVisualStyleBackColor = true;
    this.buttonServicesDefaultAll.Click += new EventHandler(this.buttonDefaultAll_Click);
    this.groupBox_Dump.Controls.Add((Control) this.checkBox_Services_isCreateDumpAuto);
    this.groupBox_Dump.Controls.Add((Control) this.button_OpenDumpFolder);
    this.groupBox_Dump.Location = new Point(23, 276);
    this.groupBox_Dump.Name = "groupBox_Dump";
    this.groupBox_Dump.Size = new Size(650, 210);
    this.groupBox_Dump.TabIndex = 34;
    this.groupBox_Dump.TabStop = false;
    this.groupBox_Dump.Text = "Dump";
    this.checkBox_Services_isCreateDumpAuto.AutoSize = true;
    this.checkBox_Services_isCreateDumpAuto.Checked = true;
    this.checkBox_Services_isCreateDumpAuto.CheckState = CheckState.Checked;
    this.checkBox_Services_isCreateDumpAuto.Location = new Point(20, 175);
    this.checkBox_Services_isCreateDumpAuto.Name = "checkBox_Services_isCreateDumpAuto";
    this.checkBox_Services_isCreateDumpAuto.Size = new Size(218, 17);
    this.checkBox_Services_isCreateDumpAuto.TabIndex = 34;
    this.checkBox_Services_isCreateDumpAuto.Text = "Создавать протоколы автоматически";
    this.toolTip1.SetToolTip((Control) this.checkBox_Services_isCreateDumpAuto, "При ошибке сбора предлагать автоматическое создание протокола");
    this.checkBox_Services_isCreateDumpAuto.UseVisualStyleBackColor = true;
    this.checkBox_Services_isCreateDumpAuto.MouseClick += new MouseEventHandler(this.checkBox_Services_isCreateDumpAuto_MouseClick);
    this.button_OpenDumpFolder.Location = new Point(500, 133);
    this.button_OpenDumpFolder.Name = "button_OpenDumpFolder";
    this.button_OpenDumpFolder.Size = new Size(116, 27);
    this.button_OpenDumpFolder.TabIndex = 19;
    this.button_OpenDumpFolder.Text = "Открыть папку";
    this.toolTip1.SetToolTip((Control) this.button_OpenDumpFolder, "Открыть папку с Dump");
    this.button_OpenDumpFolder.UseVisualStyleBackColor = true;
    this.button_OpenDumpFolder.Visible = false;
    this.button_OpenDumpFolder.Click += new EventHandler(this.button_OpenDumpFolder_Click);
    this.toolTip1.IsBalloon = true;
    this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
    this.toolTip1.ToolTipTitle = "Подсказка";
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "");
    this.imagesToolbars.Images.SetKeyName(2, "");
    this.imagesToolbars.Images.SetKeyName(3, "");
    this.imagesToolbars.Images.SetKeyName(4, "");
    this.imagesToolbars.Images.SetKeyName(5, "");
    this.imagesToolbars.Images.SetKeyName(6, "");
    this.imagesToolbars.Images.SetKeyName(7, "Связь.ico");
    this.imagesToolbars.Images.SetKeyName(8, "object_16x16.ico");
    this.imagesToolbars.Images.SetKeyName(9, "WithoutDrawing.ico");
    this.imageListSort.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageListSort.ImageStream");
    this.imageListSort.TransparentColor = Color.Transparent;
    this.imageListSort.Images.SetKeyName(0, "");
    this.imageListSort.Images.SetKeyName(1, "");
    this.imageListSort.Images.SetKeyName(2, "");
    this.dataGridViewTextBoxColumn1.HeaderText = "Атрибут";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn1.Width = 360;
    this.dataGridViewTextBoxColumn2.HeaderText = "От";
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn2.Width = 210;
    this.dataGridViewTextBoxColumn3.HeaderText = "До";
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn3.Width = 210;
    this.dataGridViewTextBoxColumn4.HeaderText = "Сравнение";
    this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
    this.dataGridViewTextBoxColumn4.ReadOnly = true;
    this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn4.Width = 130;
    this.dataGridViewTextBoxColumn5.HeaderText = "Пустые строки";
    this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
    this.dataGridViewTextBoxColumn5.ReadOnly = true;
    this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn5.Width = 130;
    this.dataGridViewTextBoxColumn6.HeaderText = "Номер";
    this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
    this.dataGridViewTextBoxColumn6.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn6.Width = 90;
    this.dataGridViewTextBoxColumn7.HeaderText = "Наименование раздела";
    this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
    this.dataGridViewTextBoxColumn7.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn7.Width = 500;
    this.dataGridViewTextBoxColumn8.HeaderText = "Значение";
    this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
    this.dataGridViewTextBoxColumn8.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn8.Width = 90;
    this.dataGridViewTextBoxColumn9.HeaderText = "Заголовок";
    this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
    this.dataGridViewTextBoxColumn9.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn9.Width = 310;
    this.dataGridViewTextBoxColumn10.HeaderText = "Значение";
    this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
    this.dataGridViewTextBoxColumn10.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn10.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn10.Width = 90;
    this.dataGridViewTextBoxColumn11.HeaderText = "Заголовок";
    this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
    this.dataGridViewTextBoxColumn11.Resizable = DataGridViewTriState.False;
    this.dataGridViewTextBoxColumn11.SortMode = DataGridViewColumnSortMode.NotSortable;
    this.dataGridViewTextBoxColumn11.Width = 310;
    this.AutoScaleMode = AutoScaleMode.None;
    this.AutoScroll = true;
    this.AutoSize = true;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(1584, 796);
    this.Controls.Add((Control) this.tabControl_Nastr);
    this.Controls.Add((Control) this.panelForButtons);
    this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.ForeColor = SystemColors.ControlText;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (A_NastrVed);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Настройка ведомости:";
    this.FormClosing += new FormClosingEventHandler(this.A_NastrVed_FormClosing);
    this.Load += new EventHandler(this.A_NastrVed_Load);
    this.Shown += new EventHandler(this.A_NastrVed_Shown);
    this.panelForButtons.ResumeLayout(false);
    this.tabControl_Nastr.ResumeLayout(false);
    this.tabPage_Bases.ResumeLayout(false);
    this.tabControl_Usl_Bases.ResumeLayout(false);
    this.tabPage_Bases_Main.ResumeLayout(false);
    this.tabPage_Bases_Main.PerformLayout();
    this.groupBox_SpecificationSections.ResumeLayout(false);
    this.groupBox_SpecificationSections.PerformLayout();
    ((ISupportInitialize) this.drawGrid_SpecificationSections).EndInit();
    this.groupBox_Usl_Bases_MainStep.ResumeLayout(false);
    this.groupBox_Usl_Bases_MainStep.PerformLayout();
    this.tabPage_Usl_Bases_Sbor.ResumeLayout(false);
    this.tabPage_Usl_Bases_Sbor.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_For_ZIP.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_COMPL.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_For_ZIP_SB.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_isVedAddToRazdel.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_VedStep.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_VedStep.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_isVedExtrectionVtor.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_VedGroup.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_VedGroup.PerformLayout();
    this.tabPage_Usl_Bases_SborDialog.ResumeLayout(false);
    this.groupBox_Usl_Bases_ImbaseCatalog.ResumeLayout(false);
    this.groupBox_Usl_Bases_ImbaseCatalog.PerformLayout();
    this.groupBox_Usl_Bases_Sbor_Input.ResumeLayout(false);
    this.groupBox_Usl_Bases_Sbor_Input.PerformLayout();
    this.tabPage_Sbor.ResumeLayout(false);
    this.tabControl_Page_Sbor.ResumeLayout(false);
    this.tabPage_Sbor_Usl.ResumeLayout(false);
    this.Sbor_Usl_Panel.ResumeLayout(false);
    this.Sbor_Usl_Panel.PerformLayout();
    this.groupBox_Sbor_Usl_I_ILI.ResumeLayout(false);
    this.groupBox_Sbor_Usl_I_ILI.PerformLayout();
    this.groupBox_Sbor_Usl_Text.ResumeLayout(false);
    this.groupBox_Sbor_Usl_Text.PerformLayout();
    this.groupBox_Sbor_Usl_Sravnenie.ResumeLayout(false);
    this.groupBox_Sbor_Usl_Sravnenie.PerformLayout();
    this.groupBox_Sbor_Usl_AttributeControl1.ResumeLayout(false);
    this.groupBox_Sbor_Usl_CollapsedTreeView.ResumeLayout(false);
    this.groupBox_Sbor_Usl_CollapsedTreeView.PerformLayout();
    this.groupBox_UsloviaVvoda.ResumeLayout(false);
    this.tabPage_Sbor_Peredatha.ResumeLayout(false);
    this.tabPage_Sbor_Peredatha.PerformLayout();
    this.groupBox_Sbor_Peredatha_AttributeControl1.ResumeLayout(false);
    this.groupBox_Sbor_Peredatha_ListId.ResumeLayout(false);
    this.tabPage_Sbor_Others.ResumeLayout(false);
    this.tabPage_Sbor_Others.PerformLayout();
    this.groupBox_Sbor_Others_DopZam.ResumeLayout(false);
    this.groupBox_Sbor_Others_DopZam.PerformLayout();
    this.groupBox_Sbor_Others_Complecty.ResumeLayout(false);
    this.groupBox_Sbor_Others_Complecty.PerformLayout();
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.ResumeLayout(false);
    this.groupBox_Sbor_Others_IsRaskrSP_s_takoi_Ved.PerformLayout();
    this.tabPage_Sbor_Usl_Reference.ResumeLayout(false);
    this.Sbor_Usl_Reference_Panel.ResumeLayout(false);
    this.Sbor_Usl_Reference_Panel.PerformLayout();
    this.groupBox_Sbor_Usl_I_ILI_Reference.ResumeLayout(false);
    this.groupBox_Sbor_Usl_I_ILI_Reference.PerformLayout();
    this.groupBox_Sbor_Usl_Text_Reference.ResumeLayout(false);
    this.groupBox_Sbor_Usl_Text_Reference.PerformLayout();
    this.groupBox_Sbor_Usl_Sravnenie_Reference.ResumeLayout(false);
    this.groupBox_Sbor_Usl_Sravnenie_Reference.PerformLayout();
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.ResumeLayout(false);
    this.groupBox_Sbor_Usl_CollapsedTreeView_Reference.PerformLayout();
    this.groupBox_UsloviaVvoda_Reference.ResumeLayout(false);
    this.groupBox_Sbor_Usl_AttributeControl_Reference.ResumeLayout(false);
    this.tabPage_ESPD.ResumeLayout(false);
    this.groupBox_Remark.ResumeLayout(false);
    this.groupBox_Remark.PerformLayout();
    this.groupBox_AddToSP.ResumeLayout(false);
    this.groupBox_AddToSP.PerformLayout();
    this.groupBox_FirstOpen.ResumeLayout(false);
    this.groupBox_FirstOpen.PerformLayout();
    this.tabPage_Sorting.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_Sorting_Doc).EndInit();
    this.groupBox_Sorting_List_Ved_Graf.ResumeLayout(false);
    this.groupBox_Sorting_List_Ved_Id.ResumeLayout(false);
    this.groupBox_Sorting_PoriadokSortirovki.ResumeLayout(false);
    this.groupBox_Sorting_PoriadokSortirovki.PerformLayout();
    this.groupBox_Sorting_PustyeStroki.ResumeLayout(false);
    this.groupBox_Sorting_PustyeStroki.PerformLayout();
    this.groupBox_Sorting_Sravnenie.ResumeLayout(false);
    this.groupBox_Sorting_Sravnenie.PerformLayout();
    this.groupBox_Sorting_End.ResumeLayout(false);
    this.numericUpDown_Sorting_NumberEnd.EndInit();
    this.groupBox_Sorting_Do.ResumeLayout(false);
    this.groupBox_Sorting_Do.PerformLayout();
    this.groupBox_Sorting_Begin.ResumeLayout(false);
    this.numericUpDown_Sorting_NumberBegin.EndInit();
    this.groupBox_Sorting_Ot.ResumeLayout(false);
    this.groupBox_Sorting_Ot.PerformLayout();
    ((ISupportInitialize) this.dataGridView_Sorting).EndInit();
    this.groupBox_Sorting_AttribVedRec1.ResumeLayout(false);
    this.tabPage_Merge.ResumeLayout(false);
    this.tabPage_Merge.PerformLayout();
    this.groupBox_Merge_List_Merge_Usl2.ResumeLayout(false);
    this.groupBox_Merge_AttribVedRec1.ResumeLayout(false);
    this.groupBox_Merge_List_Ved_Id.ResumeLayout(false);
    this.tabPage_Razdels.ResumeLayout(false);
    this.tabPage_Razdels.PerformLayout();
    this.groupBox_Conformity_Name_Page_for_Razdel.ResumeLayout(false);
    this.groupBox_NamePage.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_NamePage).EndInit();
    this.groupBox_RazdelVedAndNamePage.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_RazdelVedAndNamePage).EndInit();
    this.Razdels_groupBoxListPodRazdelov.ResumeLayout(false);
    ((ISupportInitialize) this.Razdels_dataGridViewListPodRazdels).EndInit();
    this.Razdels_groupBoxListRazdelov.ResumeLayout(false);
    ((ISupportInitialize) this.Razdels_dataGridViewListRazdels).EndInit();
    this.tabPage_Zagolovki.ResumeLayout(false);
    this.tabPage_Zagolovki.PerformLayout();
    this.groupBox_Include_Name.ResumeLayout(false);
    this.groupBox_Include_Name.PerformLayout();
    this.groupBox_Zagolovki_List_Ved_Id.ResumeLayout(false);
    this.groupBox_Zagolovki_AttribVedRec1.ResumeLayout(false);
    this.groupBox_Zagolovki_TypeCompare.ResumeLayout(false);
    this.groupBox_Zagolovki_TypeCompare.PerformLayout();
    this.groupBox_ListZagolovkov.ResumeLayout(false);
    ((ISupportInitialize) this.dataGridView_ListZagolovkov).EndInit();
    this.tabPage_Vyvod.ResumeLayout(false);
    this.tabControl_Vyvod.ResumeLayout(false);
    this.tabPage_Vyvod_1.ResumeLayout(false);
    this.groupBox_Vyvod_List_Ved_Id.ResumeLayout(false);
    this.groupBox_Vyvod_AttribVedRec1.ResumeLayout(false);
    this.groupBox_Vyvod_Ved_Pasport.ResumeLayout(false);
    this.panel_Vyvod_1.ResumeLayout(false);
    this.groupBox_Vyvod_Forma.ResumeLayout(false);
    this.groupBox_Vyvod_Forma.PerformLayout();
    this.numeric_Vyvod_UpDownKolGraf.EndInit();
    this.groupBox_Vyvod_TextRazdelitel.ResumeLayout(false);
    this.tabPage_Vyvod_2.ResumeLayout(false);
    this.groupBox_isUnbrokenDefis.ResumeLayout(false);
    this.groupBox_isUnbrokenDefis.PerformLayout();
    this.groupBox_Check.ResumeLayout(false);
    this.groupBox_Check.PerformLayout();
    this.groupBox_ProtectionCommand.ResumeLayout(false);
    this.groupBox_ProtectionCommand.PerformLayout();
    this.groupBox_Protection_From_Editing.ResumeLayout(false);
    this.groupBox_Protection_From_Editing.PerformLayout();
    this.groupBox_Vyvod_isDeleteIdenticalTexts.ResumeLayout(false);
    this.groupBox_Vyvod_isDeleteIdenticalTexts.PerformLayout();
    this.groupBox_Vyvod_Additional.ResumeLayout(false);
    this.groupBox_Vyvod_Additional.PerformLayout();
    this.groupBox_Vyvod2_SkipRows.ResumeLayout(false);
    this.groupBox_Vyvod2_SkipRows.PerformLayout();
    this.numericUpDown_Vyvod2_AfterRemark.EndInit();
    this.numericUpDown_Vyvod2_AfterInfo.EndInit();
    this.group_Vyvod2_BoxLizm.ResumeLayout(false);
    this.group_Vyvod2_BoxLizm.PerformLayout();
    this.numericUpDown_Vyvod2_Lizm.EndInit();
    this.tabPage_Xml.ResumeLayout(false);
    this.groupBox_Xml_Folder_In.ResumeLayout(false);
    this.groupBox_Xml_Folder_In.PerformLayout();
    this.groupBox_Xml_EmptyString.ResumeLayout(false);
    this.groupBox_Xml_EmptyString.PerformLayout();
    this.numeric_UpDown_Xml_AfterRemark.EndInit();
    this.numeric_UpDown_Xml_AfterInfo.EndInit();
    this.groupBox_Xml_Out.ResumeLayout(false);
    this.groupBox_Xml_Out.PerformLayout();
    this.groupBox_Xml_In.ResumeLayout(false);
    this.groupBox_Xml_In.PerformLayout();
    this.groupBox_Xml_Text.ResumeLayout(false);
    this.groupBox_Xml_Text.PerformLayout();
    this.tabPage_Avs6.ResumeLayout(false);
    this.panel_Avs_1.ResumeLayout(false);
    this.groupBox_Avs_Forma.ResumeLayout(false);
    this.groupBox_Avs_Forma.PerformLayout();
    this.numeric_Avs_UpDownKolGraf.EndInit();
    this.groupBox_Avs_TextRazdelitel.ResumeLayout(false);
    this.groupBox_Avs6_Fields.ResumeLayout(false);
    this.tabPage_Service.ResumeLayout(false);
    this.tabPage_Service.PerformLayout();
    this.groupBox_AccessLevel.ResumeLayout(false);
    this.groupBox_AccessLevel.PerformLayout();
    this.groupBox_Dump.ResumeLayout(false);
    this.groupBox_Dump.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary> Класс типа TreeNode дополненный </summary>
  public class UsloviaNode : TreeNode
  {
    public int _i_Razd;
    public int _i_usl = -1;
    public int _uroven = -1;
    public SpecificationSectionInfo _specificationSectionInfo;
    public A_NastrVed.UsloviaNode _usloviaNodeParent;
    public A_NastrVed.UsloviaNode _usloviaNodeRazdel;
  }

  public enum TypeSortRec
  {
    Undefined,
    Zagolovok,
    Info,
  }

  public class OneVyvodNode : TreeNode
  {
    public Vedomost_VB.OneRecordToPrint _oneRecordToPrint;
    public Vedomost_VB.OneGrafaToPrint _oneGrafaToPrint;
    public Vedomost_VB.OneDataFieldToPrint _oneDataFieldToPrint;
    public A_NastrVed.OneVyvodNode _oneVyvodNode_Parent;
    public Vedomost_VB_Static.TypeNode_Tree _typeNode;
    public int _iData = -1;
  }

  public class OneAvsNode : TreeNode
  {
    public Vedomost_VB.OneRecord_Avs6_To_Ips _oneRecord_Avs;
    public Vedomost_VB.OneGrafa_Avs6_To_Ips _oneGrafa_Avs;
    public Vedomost_VB.OneDataField_Avs6_To_Ips _oneDataField_Avs;
    public A_NastrVed.OneAvsNode _oneAvsNode_Parent;
    public Vedomost_VB_Static.TypeNode_Tree _typeNode;
    public int _iData = -1;
  }
}
