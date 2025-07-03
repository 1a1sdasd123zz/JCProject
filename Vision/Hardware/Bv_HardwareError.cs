using System.Collections.Generic;

namespace Vision.Hardware
{
    public class Bv_HardwareError
    {
        public static Dictionary<byte, string> Errors = new Dictionary<byte, string>
    {
        { 0, "无错误！" },
        { 1, "硬件开启失败！" },
        { 2, "操作的硬件序列号在字典中不存在！" },
        { 3, "硬件配置文件路径不存在！" },
        { 4, "软触发采集单帧图像失败！" },
        { 5, "采集图像超时！" },
        { 6, "开始采集失败！" },
        { 7, "停止采集失败！" },
        { 8, "相机串口开启失败！" }
    };
    }
}
