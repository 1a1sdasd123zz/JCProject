using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace csDmc1000B
{
	 public class Dmc1000B
	 {
		 //////////////////初始化函数////////////////////
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_board_init();
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_board_close();
		
		//////////////////脉冲输出设置函数////////////// 
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_set_pls_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_pls_outmode(int axis,int pls_outmode);
		
		//////////////////速度模式运动函数//////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_tv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_tv_move(int axis, int StrVel, int MaxVel, double Tacc);
		
		/////////////////速度设置函数//////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_get_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_speed(int axis);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_change_speed(int axis, int NewVel);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_decel_stop(int axis);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_immediate_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_immediate_stop(int axis);
		
		//////////////////单轴位置模式函数//////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_t_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_t_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_ta_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_ta_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_s_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_s_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_sa_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_sa_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_sv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_sv_move(int axis, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_s_profile(int axis, double s_para);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_s_profile(int axis,ref double s_para);
		
		//////////////////线性插补函数//////////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_t_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_t_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_start_ta_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_ta_line(int TotalAxis, UInt16[] AxisArray, int[] DistArray, int StrVel, int MaxVel, double Tacc);
		
		//////////////////回原点函数////////////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_home_move(int axis, int StrVel, int MaxVel, double Tacc);
		
		//////////////////运动状态检测函数//////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_check_done(int axis);
		
		//////////////////位置设定和读取函数////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_get_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_command_pos(int axis);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_set_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_command_pos(int axis,double Pos);
		
		//////////////////通用I/O函数///////////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_out_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_out_bit(int BitNo, int BitData);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_in_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_in_bit(int BitNo);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_get_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_outbit(int BitNo);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_in_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_in_enable(int cardno);
		
		//////////////////专用I/O接口函数///////////////
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_set_sd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_sd(int axis, int SdMode);
		[DllImport("Dmc1000.dll", EntryPoint = "d1000_get_axis_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_axis_status(int axis);
		//[DllImport("Dmc1000.dll", EntryPoint = "d1000_WriteDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern int d1000_WriteDWord();
		//[DllImport("Dmc1000.dll", EntryPoint = "d1000_ReadDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //public static extern int d1000_ReadDWord();
		

		
	 }
}