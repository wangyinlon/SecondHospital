using System;
using System.Runtime.InteropServices;

namespace InfoQuick.SinoVoice.Tts
{
	/// <summary>
	/// 维护记录
	/// -------------------------------------------------------------
	/// 2004-4-5
	/// 根据jTTS_ML.h改写，封装了大部分的常量、结构和接口函数。
	/// 缺少消息和回调函数部分以及底层合成函数部分。
	/// 此代码要随着jTTS_ML.h的改动而改动。
	///								WangYi
	/// </summary>
	public class Jtts
	{
		//-----------------------------------------------------------
		//ERR_XXX 函数的返回值
		public const int ERR_NONE				= 0;
		public const int ERR_ALREADYINIT		= 1;
		public const int ERR_NOTINIT			= 2;
		public const int ERR_MEMORY				= 3;
		public const int ERR_INVALIDHWND		= 4;
		public const int ERR_INVALIDFUNC		= 5;
		public const int ERR_OPENLIB			= 6;
		public const int ERR_READLIB			= 7;
		public const int ERR_PLAYING			= 8;
		public const int ERR_DONOTHING			= 9;
		public const int ERR_INVALIDTEXT		= 10;
		public const int ERR_CREATEFILE			= 11;
		public const int ERR_WRITEFILE			= 12;
		public const int ERR_FORMAT				= 13;
		public const int ERR_INVALIDSESSION		= 14;
		public const int ERR_TOOMANYSESSION		= 15;
		public const int ERR_MORETEXT			= 16;
		public const int ERR_CONFIG				= 17;
		public const int ERR_OPENDEVICE			= 18;
		public const int ERR_RESETDEVICE		= 19;
		public const int ERR_PAUSEDEVICE		= 20;
		public const int ERR_RESTARTDEVICE		= 21;
		public const int ERR_STARTTHREAD		= 22;
		public const int ERR_BEGINOLE			= 23;
		public const int ERR_NOTSUPPORT			= 24;
		public const int ERR_SECURITY			= 25;
		public const int ERR_CONVERT			= 26;
		public const int ERR_PARAM				= 27;
		public const int ERR_INPROGRESS			= 28;
		public const int ERR_INITSOCK			= 29;
		public const int ERR_CREATESOCK			= 30;
		public const int ERR_CONNECTSOCK		= 31;
		public const int ERR_TOOMANYCON			= 32;
		public const int ERR_CONREFUSED			= 33;
		public const int ERR_SEND				= 34;
		public const int ERR_RECEIVE			= 35;
		public const int ERR_SERVERSHUTDOWN		= 36;
		public const int ERR_OUTOFTIME			= 37;
		public const int ERR_CONFIGTTS			= 38;
		public const int ERR_SYNTHTEXT			= 39;
		public const int ERR_CONFIGVERSION		= 40;
		public const int ERR_EXPIRED			= 41;
		public const int ERR_NEEDRESTART		= 42;
		public const int ERR_CODEPAGE			= 43;
		public const int ERR_ENGINE				= 44;
		public const int ERR_CREATEEVENT		= 45;
		public const int ERR_PLAYMODE			= 46;
		public const int ERR_OPENFILE			= 47;
		public const int ERR_USERABORT			= 48;

		//---------------------------------------------------------------------------
		// 系统的设置选项

		//支持多语种
		//
		//这里列出的是系统内建的语言定义，需要安装相应音库才能真正支持, 
		//但目前并非所有语言都有相应的音库
		//
		//对于这里没有列出的语言，将来也可能会发布相应的音库，同时会分配一个数值，
		//只要安装此音库后，就可以使用。对于没有列出的语言，如果想使用，可以直接使用数值
		//
		//可以通过Lang系列函数得到所有系统中定义的（包括将来扩展的）语言数值及其描述的信息
		//
		//对于系统中真正支持的语言，可以通过jTTS_GetVoiceCount, jTTS_GetVoiceAttribute函数
		//得到所有安装的音库，并从其属性中知道其语言
		public const int LANGUAGE_MANDARIN				= 0;	// 汉语普通话
		public const int LANGUAGE_CANTONESE				= 1;	// 广东话
		public const int LANGUAGE_CHINESE				= LANGUAGE_MANDARIN;

		public const int LANGUAGE_US_ENGLISH			= 10;	// 美国英语
		public const int LANGUAGE_BRITISH_ENGLISH		= 11;	// 英国英语
		public const int LANGUAGE_ENGLISH				= LANGUAGE_US_ENGLISH;

		public const int LANGUAGE_FRENCH				= 20;	// 法语
		public const int LANGUAGE_CANADIAN_FRENCH		= 21;	// 加拿大法语

		public const int LANGUAGE_SPANISH				= 30;	// 西班牙语
		public const int LANGUAGE_LATINAMERICAN_SPANISH	= 31;	// 拉丁美洲西班牙语

		public const int LANGUAGE_PORTUGUESE			= 40;	// 葡萄牙语
		public const int LANGUAGE_BRAZILIAN_PORTUGUESE	= 41;	// 巴西葡萄牙语

		public const int LANGUAGE_DUTCH					= 50;	// 荷兰语
		public const int LANGUAGE_BELGIAN_DUTCH			= 51;	// 比利时荷兰语

		public const int LANGUAGE_GERMAN				= 60;	// 德语
		public const int LANGUAGE_ITALIAN				= 70;	// 意大利语
		public const int LANGUAGE_SWEDISH				= 80;	// 瑞典语
		public const int LANGUAGE_NORWEGIAN				= 90;	// 挪威语
		public const int LANGUAGE_DANISH				= 100;	// 丹麦语
		public const int LANGUAGE_POLISH				= 110;	// 波兰语
		public const int LANGUAGE_GREEK					= 120;	// 希腊语
		public const int LANGUAGE_HUNGARIAN				= 130;	// 匈牙利语
		public const int LANGUAGE_CZECH					= 140;	// 捷克语
		public const int LANGUAGE_TURKISH				= 150;	// 土耳其语

		public const int LANGUAGE_RUSSIAN				= 500;	// 俄语

		public const int LANGUAGE_ARABIC				= 600;	// 阿拉伯语

		public const int LANGUAGE_JAPANESE				= 700;	// 日语
		public const int LANGUAGE_KOREAN				= 710;	// 韩语

		public const int LANGUAGE_VIETNAMESE			= 720;	// 越南语
		public const int LANGUAGE_MALAY					= 730;	// 马来语
		public const int LANGUAGE_THAI					= 740;	// 泰语


		//--------------------------------------------------------------------------------
		//支持多领域
		// 
		//这里列出的是系统内建的领域定义，需要安装相应音库的资源包才能真正支持。
		//
		//对于这里没有列出的领域，将来也可能会发布相应的资源包，同时会分配一个数值，
		//只要安装此资源包后，就可以使用。对于没有列出的领域，如果想使用，可以直接使用数值
		//
		//可以通过Domain系列函数得到所有系统中定义的（包括将来扩展的）领域数值及其描述的信息
		//
		//对于系统中真正支持的语言，可以通过jTTS_GetVoiceCount, jTTS_GetVoiceAttribute函数
		//得到所有安装的音库，并从其属性中知道其支持的领域
		public const int DOMAIN_COMMON			= 0;		// 通用领域，新闻
		public const int DOMAIN_FINANCE			= 1;		// 金融证券
		public const int DOMAIN_WEATHER			= 2;		// 天气预报
		public const int DOMAIN_SPORTS			= 3;		// 体育赛事
		public const int DOMAIN_TRAFFIC			= 4;		// 公交信息
		public const int DOMAIN_TRAVEL			= 5;		// 旅游餐饮

		public const int DOMAIN_MIN				= 0;
		public const int DOMAIN_MAX				= 31;

		//----------------------------------------------------------------------------------
		//支持的CODEPAGE
		public const ushort CODEPAGE_GB			= 936;		// 包括GB18030, GBK, GB2312
		public const ushort CODEPAGE_BIG5			= 950;
		public const ushort CODEPAGE_SHIFTJIS		= 932;
		public const ushort CODEPAGE_ISO8859_1		= 1252;
		public const ushort CODEPAGE_UNICODE		= 1200;
		public const ushort CODEPAGE_UNICODE_BIGE	= 1201;		// BIG Endian
		public const ushort CODEPAGE_UTF8 			= 65001;
		
		//----------------------------------------------------------------------------------
		//支持的TAG
		public const int TAG_AUTO				= 0x00;	// 自动判断
		public const int TAG_JTTS				= 0x01;	// 仅处理含有jTTS 3.0支持的TAG: \read=\  
		public const int TAG_SSML				= 0x02;	// 仅处理含有SSML 的TAG: <voice gender="female" />
		public const int TAG_NONE				= 0x03;	// 没有TAG

		//-----------------------------------------------------------------------------------
		//数字读法
		public const int DIGIT_AUTO_NUMBER		= 0;
		public const int DIGIT_TELEGRAM			= 1;
		public const int DIGIT_NUMBER			= 2;
		public const int DIGIT_AUTO_TELEGRAM	= 3;
		public const int DIGIT_AUTO				= DIGIT_AUTO_NUMBER;

		//------------------------------------------------------------------------------------
		// Punc Mode
		public const short PUNC_OFF				= 0;	/* 不读符号，自动判断回车换行是否分隔符*/
		public const short PUNC_ON				= 1;	/* 读符号，  自动判断回车换行是否分隔符*/
		public const short PUNC_OFF_RTN			= 2;	/* 不读符号，强制将回车换行作为分隔符*/
		public const short PUNC_ON_RTN			= 3;	/* 读符号，  强制将回车换行作为分隔符*/

		//------------------------------------------------------------------------------------
		// EngMode
		public const int ENG_AUTO				= 0;	/* 自动方式 */
		public const int ENG_SAPI				= 1;	/* 此版本无效，等同于ENG_AUTO */
		public const int ENG_LETTER				= 2;	/* 强制单字母方式 */
		public const int ENG_LETTER_PHRASE		= 3;	/* 强制采用字母＋自录音词汇的方式 */

		//------------------------------------------------------------------------------------
		//Gender
		public const int GENDER_FEMALE			= 0;
		public const int GENDER_MALE			= 1;
		public const int GENDER_NEUTRAL			= 2;

		//------------------------------------------------------------------------------------
		//AGE
		public const int AGE_BABY				= 0;		//0 - 3
		public const int AGE_CHILD				= 1;		//3 - 12
		public const int AGE_YOUNG				= 2;		//12 - 18
		public const int AGE_ADULT				= 3;		//18 - 60
		public const int AGE_OLD				= 4;		//60 -

		//------------------------------------------------------------------------------------
		//PITCH
		public const int PITCH_MIN				= 0;
		public const int PITCH_MAX				= 9;

		//------------------------------------------------------------------------------------
		//VOLUME
		public const int VOLUME_MIN				= 0;
		public const int VOLUME_MAX				= 9;

		//------------------------------------------------------------------------------------
		//SPEED
		public const int SPEED_MIN				= 0;
		public const int SPEED_MAX				= 9;


		//---------------------------------------------------------------------------
		//jTTS_Play状态	
		public const int STATUS_NOTINIT			= 0;
		public const int STATUS_READING			= 1;
		public const int STATUS_PAUSE			= 2;
		public const int STATUS_IDLE			= 3;

		//------------------------------------------------------------------------------------
		//jTTS_PlayToFile的文件格式
		public const int FORMAT_WAV				= 0;	// PCM Native (和音库一致，目前为16KHz, 16Bit)
		public const int FORMAT_VOX_6K			= 1;	// OKI ADPCM, 6KHz, 4bit (Dialogic Vox)
		public const int FORMAT_VOX_8K			= 2;	// OKI ADPCM, 8KHz, 4bit (Dialogic Vox)
		public const int FORMAT_ALAW_8K			= 3;	// A律, 8KHz, 8Bit
		public const int FORMAT_uLAW_8K			= 4;	// u律, 8KHz, 8Bit
		public const int FORMAT_WAV_8K8B		= 5;	// PCM, 8KHz, 8Bit
		public const int FORMAT_WAV_8K16B		= 6;	// PCM, 8KHz, 16Bit
		public const int FORMAT_WAV_16K8B		= 7;	// PCM, 16KHz, 8Bit
		public const int FORMAT_WAV_16K16B		= 8;	// PCM, 16KHz, 16Bit
		public const int FORMAT_WAV_11K8B		= 9;	// PCM, 11.025KHz, 8Bit
		public const int FORMAT_WAV_11K16B		= 10;	// PCM, 11.025KHz, 16Bit

		public const int FORMAT_FIRST			= 0;
		public const int FORMAT_LAST			= 10;

		//------------------------------------------------------------------------------------
		// jTTS_Play / jTTS_PlayToFile / jTTS_SessionStart 函数支持的dwFlag定义

		// 此项仅对jTTS_PlayToFile适用
		public const int PLAYTOFILE_DEFAULT		= 0x0000;	//默认值,写文件时只增加FORMAT_WAV_...格式的文件头
		public const int PLAYTOFILE_NOHEAD		= 0x0001;	//所有的格式都不增加文件头
		public const int PLAYTOFILE_ADDHEAD		= 0x0002;	//增加FORMAT_WAV_...格式和FORMAT_ALAW_8K,FORMAT_uLAW_8K格式的文件头

		public const int PLAYTOFILE_MASK		= 0x000F;

		// 此项仅对jTTS_Play适用
		public const int PLAY_RETURN			= 0x0000;	// 如果正在播放，返回错误
		public const int PLAY_INTERRUPT			= 0x0010;	// 如果正在播放，打断原来的播放，立即播放新的内容

		public const int PLAY_MASK				= 0x00F0;

		// 播放的内容
		public const int PLAYCONTENT_TEXT		= 0x0000;	// 播放内容为文本
		public const int PLAYCONTENT_TEXTFILE	= 0x0100;	// 播放内容为文本文件
		public const int PLAYCONTENT_AUTOFILE	= 0x0200;	// 播放内容为文件，根据后缀名采用外界Filter DLL抽取
		// 无法判断的当作文本文件

		public const int PLAYCONTENT_MASK		= 0x0F00;

		// 播放的模式，同时用于SessionStart
		public const int PLAYMODE_DEFAULT		= 0x0000;	// 在jTTS_Play下缺省异步，在jTTS_PlayToFile下缺省同步
		// jTTS_SessionStart下为主动获取数据方式
		public const int PLAYMODE_ASYNC			= 0x1000;	// 异步播放，函数立即退出
		public const int PLAYMODE_SYNC			= 0x2000;	// 同步播放，播放完成后退出

		public const int PLAYMODE_MASK			= 0xF000;


		//------------------------------------------------------------------------------------
		// jTTS_FindVoice返回的匹配级别
		public const int MATCH_LANGUAGE			= 0;	// 满足LANGUAGE，
		public const int MATCH_GENDER			= 1;	// 满足LANGUAGE, GENDER
		public const int MATCH_AGE				= 2;	// 满足LANGUAGE, GENDER, AGE
		public const int MATCH_NAME				= 3;	// 满足LANGUAGE, GENDER，AGE，NAME
		public const int MATCH_DOMAIN			= 4;	// 满足LANGUAGE, GENDER，AGE，NAME, DOMAIN，也即满足所有条件
		public const int MATCH_ALL				= 4;	// 满足所有条件

		//------------------------------------------------------------------------------------
		// InsertInfo信息
		public const int INFO_MARK				= 0;
		public const int INFO_VISEME			= 1;

		//------------------------------------------------------------------------------------
		//各种信息串的长度
		public const int VOICENAME_LEN			= 32;
		public const int VOICEID_LEN			= 40;
		public const int VENDOR_LEN				= 32;
		public const int DLLNAME_LEN			= 256;

		public const int ATTRNAME_LEN			= 32;
		public const int XMLLANG_LEN			= 256;


		//------------------------------------------------------------------------------------
		//JTTS_PARAM 在jTTS_SetParam中使用
		public const int PARAM_CODEPAGE			= 0;	// CODEPAGE_xxx
		public const int PARAM_VOICEID			= 1;	// Voice ID
		public const int PARAM_PITCH			= 2;	// PITCH_MIN - PITCH_MAX
		public const int PARAM_VOLUME		    = 3;	// VOLUME_MIN - VOLUME_MAX
		public const int PARAM_SPEED			= 4;	// SPEED_MIN - SPEED_MAX
		public const int PARAM_PUNCMODE			= 5;	// PUNC_xxx
		public const int PARAM_DIGITMODE		= 6;	// DIGIT_xxx
		public const int PARAM_ENGMODE			= 7;	// ENG_xxx
		public const int PARAM_TAGMODE			= 8;	// TAG_xxx
		public const int PARAM_DOMAIN		    = 9;	// DOMAIN_xxx
		public const int PARAM_TRYTIMES			= 10;	// 连接服务器重式次数
		public const int PARAM_LOADBALANCE		= 11;	// 是否使用负载均衡



		
		//------------------------------------------------------------------------
		//JTTS_CONFIG
		public const int JTTS_VERSION4	= 0x0004;	// version 4.0
		[StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi )]
			public struct JTTS_CONFIG
		{
			public ushort	wVersion;		// JTTS_VERSION4
			public ushort	nCodePage;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICEID_LEN)] 
			public string	szVoiceID;		// 使用的音色
			public short	nDomain;	
			public short	nPitch;
			public short	nVolume;
			public short	nSpeed;
			public short	nPuncMode;
			public short	nDigitMode;
			public short	nEngMode;
			public short	nTagMode;
			public short	nTryTimes;	    //重试次数,此成员仅用于远程合成
			public int		bLoadBalance;	//负载平衡,此成员仅用于远程合成
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)] 
			public short[]	nReserved;		// 保留
		}

		//---------------------------------------------------------------------------
		//JTTS_VOICEATTRIBUTE
		public struct JTTS_VOICEATTRIBUTE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICENAME_LEN)] 
			public string		szName;					// 只能为英文名称
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICEID_LEN)] 
			public string		szVoiceID;				// 音色的唯一标识
			public short		nGender;				// GENDER_xxx
			public short		nAge;					// AGE_xx
			public uint		dwDomainArray;			// 由低位向高位，分别表示DOMAIN_xxx
			public uint		nLanguage;				// 支持的语言, LANGUAGE_xxx
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VENDOR_LEN)] 
			public string		szVendor;				// 提供厂商
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=DLLNAME_LEN)] 
			public string		szDLLName;				// 对应的DLL
			public uint		dwVersionMS;			// 引擎的版本号，对应"3.75.0.31"的前两节
			// e.g. 0x00030075 = "3.75"
			public uint		dwVersionLS;			// e.g. 0x00000031 = "0.31"
		}

		//---------------------------------------------------------------------
		// 插入信息
		public struct INSERTINFO
		{
			public int		nTag;		// 有二种：INFO_MARK, INFO_VISEME
			public uint	dwValue; 	// 具体信息：
			// MARK时，高24位mark文本偏移，低8位文本长度
			// VISEME时，表示唇型
			public uint	dwBytes;	// 在语音流的什么地方插入，必须按顺序增加
		}

		//---------------------------------------------------------------------
		//JTTS_LANGATTRIBUTE
		public struct JTTS_LANGATTRIBUTE
		{
			public int	  nValue;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=ATTRNAME_LEN)] 
			public string  szName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=ATTRNAME_LEN)] 
			public string  szEngName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=XMLLANG_LEN)] 
			public string  szXmlLang;
		}

		//---------------------------------------------------------------------
		//JTTS_DOMAINATTRIBUTE
		public struct JTTS_DOMAINATTRIBUTE
		{
			public int   nValue;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=ATTRNAME_LEN)] 
			public string  szName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=ATTRNAME_LEN)] 
			public string  szEngName;
		}



		//----------------------------------------------------------------------
		//系统函数
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Init(string pszLibPath, string pszSerialNO);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_End();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_PreLoad (string pszVoiceID);

		//----------------------------------------------------------------------
		//语言和领域函数
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetLangCount();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetLangAttribute(int nIndex, out JTTS_LANGATTRIBUTE pAttribute);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetLangAttributeByValue(uint nValue, out JTTS_LANGATTRIBUTE pAttribute);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetDomainCount();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetDomainAttribute(uint nIndex, out JTTS_DOMAINATTRIBUTE pAttribute);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetDomainAttributeByValue(uint nValue, out JTTS_DOMAINATTRIBUTE pAttribute);
	
		//-------------------------------------------------------------
		// 音库信息函数
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetVoiceCount();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetVoiceAttribute(int nIndex, out JTTS_VOICEATTRIBUTE Attribute);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetVoiceAttributeByID(string pszVoiceID, out JTTS_VOICEATTRIBUTE pAttribute);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_IsVoiceSupported(string strVoiceID);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_FindVoice(int nLanguage, int nGender, int nAge, string pszName, int nDomain, 
			string pszVoiceID, out ushort pwMatchFlag);

		//------------------------------------------------------------------------
		// 设置函数 
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Set(ref JTTS_CONFIG pConfig);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Get(out JTTS_CONFIG pConfig);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_SetParam(int nParam, uint dwValue);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetParam(int nParam, out uint pdwValue);

		//------------------------------------------------------------------------
		// 播放函数
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Play(string pcszText, uint dwFlag);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Stop();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Pause();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Resume();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetStatus();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_PlayToFile(string pcszText, string pcszFileName, 
			uint nFormat, ref JTTS_CONFIG pConfig, 
			uint dwFlag, uint lpfnCallback, 
			uint dwUserData);
	}
}