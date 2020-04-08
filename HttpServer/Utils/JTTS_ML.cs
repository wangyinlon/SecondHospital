using System;
using System.Runtime.InteropServices;

namespace InfoQuick.SinoVoice.Tts
{
	/// <summary>
	/// ά����¼
	/// -------------------------------------------------------------
	/// 2004-4-5
	/// ����jTTS_ML.h��д����װ�˴󲿷ֵĳ������ṹ�ͽӿں�����
	/// ȱ����Ϣ�ͻص����������Լ��ײ�ϳɺ������֡�
	/// �˴���Ҫ����jTTS_ML.h�ĸĶ����Ķ���
	///								WangYi
	/// </summary>
	public class Jtts
	{
		//-----------------------------------------------------------
		//ERR_XXX �����ķ���ֵ
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
		// ϵͳ������ѡ��

		//֧�ֶ�����
		//
		//�����г�����ϵͳ�ڽ������Զ��壬��Ҫ��װ��Ӧ�����������֧��, 
		//��Ŀǰ�����������Զ�����Ӧ������
		//
		//��������û���г������ԣ�����Ҳ���ܻᷢ����Ӧ�����⣬ͬʱ�����һ����ֵ��
		//ֻҪ��װ������󣬾Ϳ���ʹ�á�����û���г������ԣ������ʹ�ã�����ֱ��ʹ����ֵ
		//
		//����ͨ��Langϵ�к����õ�����ϵͳ�ж���ģ�����������չ�ģ�������ֵ������������Ϣ
		//
		//����ϵͳ������֧�ֵ����ԣ�����ͨ��jTTS_GetVoiceCount, jTTS_GetVoiceAttribute����
		//�õ����а�װ�����⣬������������֪��������
		public const int LANGUAGE_MANDARIN				= 0;	// ������ͨ��
		public const int LANGUAGE_CANTONESE				= 1;	// �㶫��
		public const int LANGUAGE_CHINESE				= LANGUAGE_MANDARIN;

		public const int LANGUAGE_US_ENGLISH			= 10;	// ����Ӣ��
		public const int LANGUAGE_BRITISH_ENGLISH		= 11;	// Ӣ��Ӣ��
		public const int LANGUAGE_ENGLISH				= LANGUAGE_US_ENGLISH;

		public const int LANGUAGE_FRENCH				= 20;	// ����
		public const int LANGUAGE_CANADIAN_FRENCH		= 21;	// ���ô���

		public const int LANGUAGE_SPANISH				= 30;	// ��������
		public const int LANGUAGE_LATINAMERICAN_SPANISH	= 31;	// ����������������

		public const int LANGUAGE_PORTUGUESE			= 40;	// ��������
		public const int LANGUAGE_BRAZILIAN_PORTUGUESE	= 41;	// ������������

		public const int LANGUAGE_DUTCH					= 50;	// ������
		public const int LANGUAGE_BELGIAN_DUTCH			= 51;	// ����ʱ������

		public const int LANGUAGE_GERMAN				= 60;	// ����
		public const int LANGUAGE_ITALIAN				= 70;	// �������
		public const int LANGUAGE_SWEDISH				= 80;	// �����
		public const int LANGUAGE_NORWEGIAN				= 90;	// Ų����
		public const int LANGUAGE_DANISH				= 100;	// ������
		public const int LANGUAGE_POLISH				= 110;	// ������
		public const int LANGUAGE_GREEK					= 120;	// ϣ����
		public const int LANGUAGE_HUNGARIAN				= 130;	// ��������
		public const int LANGUAGE_CZECH					= 140;	// �ݿ���
		public const int LANGUAGE_TURKISH				= 150;	// ��������

		public const int LANGUAGE_RUSSIAN				= 500;	// ����

		public const int LANGUAGE_ARABIC				= 600;	// ��������

		public const int LANGUAGE_JAPANESE				= 700;	// ����
		public const int LANGUAGE_KOREAN				= 710;	// ����

		public const int LANGUAGE_VIETNAMESE			= 720;	// Խ����
		public const int LANGUAGE_MALAY					= 730;	// ������
		public const int LANGUAGE_THAI					= 740;	// ̩��


		//--------------------------------------------------------------------------------
		//֧�ֶ�����
		// 
		//�����г�����ϵͳ�ڽ��������壬��Ҫ��װ��Ӧ�������Դ����������֧�֡�
		//
		//��������û���г������򣬽���Ҳ���ܻᷢ����Ӧ����Դ����ͬʱ�����һ����ֵ��
		//ֻҪ��װ����Դ���󣬾Ϳ���ʹ�á�����û���г������������ʹ�ã�����ֱ��ʹ����ֵ
		//
		//����ͨ��Domainϵ�к����õ�����ϵͳ�ж���ģ�����������չ�ģ�������ֵ������������Ϣ
		//
		//����ϵͳ������֧�ֵ����ԣ�����ͨ��jTTS_GetVoiceCount, jTTS_GetVoiceAttribute����
		//�õ����а�װ�����⣬������������֪����֧�ֵ�����
		public const int DOMAIN_COMMON			= 0;		// ͨ����������
		public const int DOMAIN_FINANCE			= 1;		// ����֤ȯ
		public const int DOMAIN_WEATHER			= 2;		// ����Ԥ��
		public const int DOMAIN_SPORTS			= 3;		// ��������
		public const int DOMAIN_TRAFFIC			= 4;		// ������Ϣ
		public const int DOMAIN_TRAVEL			= 5;		// ���β���

		public const int DOMAIN_MIN				= 0;
		public const int DOMAIN_MAX				= 31;

		//----------------------------------------------------------------------------------
		//֧�ֵ�CODEPAGE
		public const ushort CODEPAGE_GB			= 936;		// ����GB18030, GBK, GB2312
		public const ushort CODEPAGE_BIG5			= 950;
		public const ushort CODEPAGE_SHIFTJIS		= 932;
		public const ushort CODEPAGE_ISO8859_1		= 1252;
		public const ushort CODEPAGE_UNICODE		= 1200;
		public const ushort CODEPAGE_UNICODE_BIGE	= 1201;		// BIG Endian
		public const ushort CODEPAGE_UTF8 			= 65001;
		
		//----------------------------------------------------------------------------------
		//֧�ֵ�TAG
		public const int TAG_AUTO				= 0x00;	// �Զ��ж�
		public const int TAG_JTTS				= 0x01;	// ��������jTTS 3.0֧�ֵ�TAG: \read=\  
		public const int TAG_SSML				= 0x02;	// ��������SSML ��TAG: <voice gender="female" />
		public const int TAG_NONE				= 0x03;	// û��TAG

		//-----------------------------------------------------------------------------------
		//���ֶ���
		public const int DIGIT_AUTO_NUMBER		= 0;
		public const int DIGIT_TELEGRAM			= 1;
		public const int DIGIT_NUMBER			= 2;
		public const int DIGIT_AUTO_TELEGRAM	= 3;
		public const int DIGIT_AUTO				= DIGIT_AUTO_NUMBER;

		//------------------------------------------------------------------------------------
		// Punc Mode
		public const short PUNC_OFF				= 0;	/* �������ţ��Զ��жϻس������Ƿ�ָ���*/
		public const short PUNC_ON				= 1;	/* �����ţ�  �Զ��жϻس������Ƿ�ָ���*/
		public const short PUNC_OFF_RTN			= 2;	/* �������ţ�ǿ�ƽ��س�������Ϊ�ָ���*/
		public const short PUNC_ON_RTN			= 3;	/* �����ţ�  ǿ�ƽ��س�������Ϊ�ָ���*/

		//------------------------------------------------------------------------------------
		// EngMode
		public const int ENG_AUTO				= 0;	/* �Զ���ʽ */
		public const int ENG_SAPI				= 1;	/* �˰汾��Ч����ͬ��ENG_AUTO */
		public const int ENG_LETTER				= 2;	/* ǿ�Ƶ���ĸ��ʽ */
		public const int ENG_LETTER_PHRASE		= 3;	/* ǿ�Ʋ�����ĸ����¼���ʻ�ķ�ʽ */

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
		//jTTS_Play״̬	
		public const int STATUS_NOTINIT			= 0;
		public const int STATUS_READING			= 1;
		public const int STATUS_PAUSE			= 2;
		public const int STATUS_IDLE			= 3;

		//------------------------------------------------------------------------------------
		//jTTS_PlayToFile���ļ���ʽ
		public const int FORMAT_WAV				= 0;	// PCM Native (������һ�£�ĿǰΪ16KHz, 16Bit)
		public const int FORMAT_VOX_6K			= 1;	// OKI ADPCM, 6KHz, 4bit (Dialogic Vox)
		public const int FORMAT_VOX_8K			= 2;	// OKI ADPCM, 8KHz, 4bit (Dialogic Vox)
		public const int FORMAT_ALAW_8K			= 3;	// A��, 8KHz, 8Bit
		public const int FORMAT_uLAW_8K			= 4;	// u��, 8KHz, 8Bit
		public const int FORMAT_WAV_8K8B		= 5;	// PCM, 8KHz, 8Bit
		public const int FORMAT_WAV_8K16B		= 6;	// PCM, 8KHz, 16Bit
		public const int FORMAT_WAV_16K8B		= 7;	// PCM, 16KHz, 8Bit
		public const int FORMAT_WAV_16K16B		= 8;	// PCM, 16KHz, 16Bit
		public const int FORMAT_WAV_11K8B		= 9;	// PCM, 11.025KHz, 8Bit
		public const int FORMAT_WAV_11K16B		= 10;	// PCM, 11.025KHz, 16Bit

		public const int FORMAT_FIRST			= 0;
		public const int FORMAT_LAST			= 10;

		//------------------------------------------------------------------------------------
		// jTTS_Play / jTTS_PlayToFile / jTTS_SessionStart ����֧�ֵ�dwFlag����

		// �������jTTS_PlayToFile����
		public const int PLAYTOFILE_DEFAULT		= 0x0000;	//Ĭ��ֵ,д�ļ�ʱֻ����FORMAT_WAV_...��ʽ���ļ�ͷ
		public const int PLAYTOFILE_NOHEAD		= 0x0001;	//���еĸ�ʽ���������ļ�ͷ
		public const int PLAYTOFILE_ADDHEAD		= 0x0002;	//����FORMAT_WAV_...��ʽ��FORMAT_ALAW_8K,FORMAT_uLAW_8K��ʽ���ļ�ͷ

		public const int PLAYTOFILE_MASK		= 0x000F;

		// �������jTTS_Play����
		public const int PLAY_RETURN			= 0x0000;	// ������ڲ��ţ����ش���
		public const int PLAY_INTERRUPT			= 0x0010;	// ������ڲ��ţ����ԭ���Ĳ��ţ����������µ�����

		public const int PLAY_MASK				= 0x00F0;

		// ���ŵ�����
		public const int PLAYCONTENT_TEXT		= 0x0000;	// ��������Ϊ�ı�
		public const int PLAYCONTENT_TEXTFILE	= 0x0100;	// ��������Ϊ�ı��ļ�
		public const int PLAYCONTENT_AUTOFILE	= 0x0200;	// ��������Ϊ�ļ������ݺ�׺���������Filter DLL��ȡ
		// �޷��жϵĵ����ı��ļ�

		public const int PLAYCONTENT_MASK		= 0x0F00;

		// ���ŵ�ģʽ��ͬʱ����SessionStart
		public const int PLAYMODE_DEFAULT		= 0x0000;	// ��jTTS_Play��ȱʡ�첽����jTTS_PlayToFile��ȱʡͬ��
		// jTTS_SessionStart��Ϊ������ȡ���ݷ�ʽ
		public const int PLAYMODE_ASYNC			= 0x1000;	// �첽���ţ����������˳�
		public const int PLAYMODE_SYNC			= 0x2000;	// ͬ�����ţ�������ɺ��˳�

		public const int PLAYMODE_MASK			= 0xF000;


		//------------------------------------------------------------------------------------
		// jTTS_FindVoice���ص�ƥ�伶��
		public const int MATCH_LANGUAGE			= 0;	// ����LANGUAGE��
		public const int MATCH_GENDER			= 1;	// ����LANGUAGE, GENDER
		public const int MATCH_AGE				= 2;	// ����LANGUAGE, GENDER, AGE
		public const int MATCH_NAME				= 3;	// ����LANGUAGE, GENDER��AGE��NAME
		public const int MATCH_DOMAIN			= 4;	// ����LANGUAGE, GENDER��AGE��NAME, DOMAIN��Ҳ��������������
		public const int MATCH_ALL				= 4;	// ������������

		//------------------------------------------------------------------------------------
		// InsertInfo��Ϣ
		public const int INFO_MARK				= 0;
		public const int INFO_VISEME			= 1;

		//------------------------------------------------------------------------------------
		//������Ϣ���ĳ���
		public const int VOICENAME_LEN			= 32;
		public const int VOICEID_LEN			= 40;
		public const int VENDOR_LEN				= 32;
		public const int DLLNAME_LEN			= 256;

		public const int ATTRNAME_LEN			= 32;
		public const int XMLLANG_LEN			= 256;


		//------------------------------------------------------------------------------------
		//JTTS_PARAM ��jTTS_SetParam��ʹ��
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
		public const int PARAM_TRYTIMES			= 10;	// ���ӷ�������ʽ����
		public const int PARAM_LOADBALANCE		= 11;	// �Ƿ�ʹ�ø��ؾ���



		
		//------------------------------------------------------------------------
		//JTTS_CONFIG
		public const int JTTS_VERSION4	= 0x0004;	// version 4.0
		[StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi )]
			public struct JTTS_CONFIG
		{
			public ushort	wVersion;		// JTTS_VERSION4
			public ushort	nCodePage;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICEID_LEN)] 
			public string	szVoiceID;		// ʹ�õ���ɫ
			public short	nDomain;	
			public short	nPitch;
			public short	nVolume;
			public short	nSpeed;
			public short	nPuncMode;
			public short	nDigitMode;
			public short	nEngMode;
			public short	nTagMode;
			public short	nTryTimes;	    //���Դ���,�˳�Ա������Զ�̺ϳ�
			public int		bLoadBalance;	//����ƽ��,�˳�Ա������Զ�̺ϳ�
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=12)] 
			public short[]	nReserved;		// ����
		}

		//---------------------------------------------------------------------------
		//JTTS_VOICEATTRIBUTE
		public struct JTTS_VOICEATTRIBUTE
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICENAME_LEN)] 
			public string		szName;					// ֻ��ΪӢ������
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VOICEID_LEN)] 
			public string		szVoiceID;				// ��ɫ��Ψһ��ʶ
			public short		nGender;				// GENDER_xxx
			public short		nAge;					// AGE_xx
			public uint		dwDomainArray;			// �ɵ�λ���λ���ֱ��ʾDOMAIN_xxx
			public uint		nLanguage;				// ֧�ֵ�����, LANGUAGE_xxx
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=VENDOR_LEN)] 
			public string		szVendor;				// �ṩ����
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=DLLNAME_LEN)] 
			public string		szDLLName;				// ��Ӧ��DLL
			public uint		dwVersionMS;			// ����İ汾�ţ���Ӧ"3.75.0.31"��ǰ����
			// e.g. 0x00030075 = "3.75"
			public uint		dwVersionLS;			// e.g. 0x00000031 = "0.31"
		}

		//---------------------------------------------------------------------
		// ������Ϣ
		public struct INSERTINFO
		{
			public int		nTag;		// �ж��֣�INFO_MARK, INFO_VISEME
			public uint	dwValue; 	// ������Ϣ��
			// MARKʱ����24λmark�ı�ƫ�ƣ���8λ�ı�����
			// VISEMEʱ����ʾ����
			public uint	dwBytes;	// ����������ʲô�ط����룬���밴˳������
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
		//ϵͳ����
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Init(string pszLibPath, string pszSerialNO);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_End();
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_PreLoad (string pszVoiceID);

		//----------------------------------------------------------------------
		//���Ժ�������
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
		// ������Ϣ����
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
		// ���ú��� 
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Set(ref JTTS_CONFIG pConfig);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_Get(out JTTS_CONFIG pConfig);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_SetParam(int nParam, uint dwValue);
		[DllImport("jTTS_ML.dll")]
		public static extern int jTTS_GetParam(int nParam, out uint pdwValue);

		//------------------------------------------------------------------------
		// ���ź���
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