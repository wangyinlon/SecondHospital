using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace WebAppReadCard.Utils
{
    public class dcrf
    {
        /// <summary>
        /// 设置前端 进卡模式
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceConfigFront(IntPtr icdev,byte mode);

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
        /// <param name="pos">位置状态</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardStatus(IntPtr icdev,ref byte pos);

        /// <summary>
        /// 移动卡片到相应位置
        /// </summary>
        /// <param name="icdev">设备标识符。</param>
        /// <param name="time_s">设备超时值，单位为秒</param>
        /// <param name="mode">模式</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceCardMove(IntPtr icdev,  byte time_s,byte mode);

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
        /// 获取电动卡机传感器的状态
        /// </summary>
        /// <param name="icdev">设备标识符</param>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        [DllImport("dcrf32.dll")]
        public static extern int dc_SelfServiceDeviceSensorStatus(IntPtr icdev,ref byte value);

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