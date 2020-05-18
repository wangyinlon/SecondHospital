using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace CardService.Utils
{
    public class dcrf
    {

     
        /// <summary>
        /// 配置端口名称
        /// </summary>
        /// <param name="port">端口号</param>
        /// <param name="name">物理名称</param>
        /// <returns></returns>

        [DllImport("dcrf32.dll")]
        public static extern int dc_config_port_name(int port, ref string name);

        /// <summary>
        /// 打开设备
        ///说明：
        /// 建立设备的通讯并且分配相应的资源，大部分功能接口都需要在此过程后才能进行，在不需要使用设备后，必须使用 ::dc_exit 去关闭设备的通讯和释放资源。
        /// </summary>
        /// <param name="port">端口号
        ///0~99 - 表示串口模式（编号物理对应），编号0表示第一个串口合法设备，编号1表示第二个串口合法设备，以此类推
        ///100~199 - 表示USB模式（编号逻辑对应），编号100表示第一个USB合法设备，编号1表示第二个USB合法设备，以此类推
        ///200~299 - 表示PCSC模式（编号逻辑对应），编号200表示第一个PCSC合法设备，编号201表示第二个PCSC合法设备，以此类推
        ///300~399 - 表示蓝牙模式（编号逻辑对应），编号300表示第一个蓝牙合法设备，编号301表示第二个蓝牙合法设备，以此类推
        /// </param>
        /// <param name="baud">波特率，只针对串口模式有效</param>
        /// <returns> 小于0表示失败，否则为设备标识符</returns>
        [DllImport("dcrf32.dll")]
        public static extern IntPtr dc_init(int port, int baud);

     
       
        /*
       * @brief  关闭设备。
       * @par    说明：
       * 关闭设备的通讯和释放资源。
       * @param[in] icdev 设备标识符。
       * @return <0表示失败，==0表示成功。
       */

        /// <summary>
        /// 关闭设备
        /// 说明：
        /// 关闭设备的通讯和释放资源。
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <returns>小于0表示失败，==0表示成功</returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_exit(IntPtr icdev);


        [DllImport("dcrf32.dll")]
        public static extern short dc_config_card(IntPtr icdev, char cardtype);  //初试化

        /// <summary>
        /// 读取磁条卡
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="pTrack1Data"></param>
        /// <param name="pTrack1Len"></param>
        /// <param name="pTrack2Data"></param>
        /// <param name="pTrack2Len"></param>
        /// <param name="pTrack3Data"></param>
        /// <param name="pTrack3Len"></param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern IntPtr dc_readmag(IntPtr icdev, byte[] pTrack1Data, ref uint pTrack1Len,  byte[] pTrack2Data, ref uint pTrack2Len,  byte[] pTrack3Data, ref uint pTrack3Len);
       
        /// <summary>
        /// 读取id卡
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="time_ms"></param>
        /// <param name="rlen"></param>
        /// <param name="rdata"></param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern IntPtr dc_ReadIdCardInfo(IntPtr icdev, int time_ms,ref int rlen, byte[] rdata);
        [DllImport("dcrf32.dll")]
        public static extern IntPtr  dc_read_idcard(IntPtr icdev, byte times, byte[] _Data);
        [DllImport("dcrf32.dll")]
        public static extern IntPtr dc_read(IntPtr icdev ,byte __Adr ,ref byte Data);

        /// <summary>
        /// 设置前端 进卡模式
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceConfigFront(IntPtr icdev, byte mode);

        /// <summary>
        /// 设置后端进卡模式
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceConfigBack(IntPtr icdev, byte mode);

        /// <summary>
        /// 设置停卡位置
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceConfigPlace(IntPtr icdev, byte mode);

        /// <summary>
        /// 设置掉电退卡模式
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceConfig(IntPtr icdev, byte mode);

        /// <summary>
        /// 检测电动卡机当前的卡片状态
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="pos">位置状态 0x00 -无卡。 0x01 -无卡，卡在前门口。 0x10 -有卡，不 可操作任何卡。 0x11 -有卡，可操作磁条。0x12 -有卡，可操作接触。 0x14 -有卡，可操 作非接触。</param>
        /// <returns>返回 小于0表示失败，==0表示成功。</returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardStatus(IntPtr icdev, ref byte pos);

        /// <summary>
        /// 移动卡片到相应位置
        /// </summary>
        /// <param name="icdev">设备标识符。</param>
        /// <param name="time_s">设备超时值，单位为秒</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardMove(IntPtr icdev, byte time_s, byte mode);

        /// <summary>
        /// 弹出卡片，操作前设备内无卡则错误
        /// </summary>
        /// <param name="icdev">设备标识符。</param>
        /// <param name="time_s">设备超时值，单位为秒</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardEject(IntPtr icdev, byte time_s, byte mode);

        /// <summary>
        /// 卡机内有接触或非接 触卡时，自动检测卡片类型
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCheckCardType(IntPtr icdev);

        /// <summary>
        /// 获取电动卡机传感器的状态
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceSensorStatus(IntPtr icdev,  byte[] value);

        /// <summary>
        /// 使自助设备进入上电初 始状态，设置参数为缺省参数
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceReset(IntPtr icdev);

        /// <summary>
        /// 等待进入卡片，超时退出
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="time_s">设备超时值，单位为秒</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardInject(IntPtr icdev, byte time_s, byte mode);
    }
}