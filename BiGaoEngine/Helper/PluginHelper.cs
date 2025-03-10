using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BiGaoInterface;
using BiGaoEngine.Model;

namespace BiGaoEngine.Helper;

/// <summary>
/// 古希腊掌管插件的神
/// </summary>
public class PluginHelper
{
    // 插件列表
    public static List<IPlugin> PluginsList = new List<IPlugin>();
    public static string PluginsPath { get; } = 
        Path.Combine(System.Environment.CurrentDirectory,"Plugins");
    
    /// <summary>
    /// 获取所有插件(Dll)
    /// </summary>
    /// <returns>返回所有Dll文件路径</returns>
    public static List<string> FindAllPlugin
        ()
    {
        // 获取所有DLL文件
        var DllPath = Directory.GetFiles(PluginsPath,"*.dll").ToList();
        return DllPath;
    }
    
    
    /// <summary>
    /// 获取制定插件
    /// </summary>
    /// <returns>返回插件列表</returns>
    public static List<IPlugin> FindPlugin
        (string PluginName)
    {
        var TempList = new List<IPlugin>();
        // 获取所有插件
        foreach (var plugin in PluginsList)
        {
            if (plugin.Name == PluginName)
            {
                TempList.Add(plugin);
            }
        }
        return TempList;
    }
    
    
    /// <summary>
    /// 加载Dll插件,加载完成把插件相关信息存储到列表
    /// </summary>
    public static void LoadPlugin
        (string DllPath)
    {
        // 动态加载Dll
        var asm = Assembly.LoadFrom(DllPath);
        // 获取所有类并获取里头继承于插件PluginBase接口的
        foreach (var Type in asm.GetTypes())
        {
            if (Type.GetInterface("IPlugin") != null)
            {
                // 创建插件类并把信息存储到列表
                var PluginInfo = Activator.CreateInstance(Type) as IPlugin;
                // 显示插件信息
                var pluginInfo = LoadPluginsInfo(PluginInfo);
                Console.WriteLine($"插件名称: {pluginInfo.Name}");
                Console.WriteLine($"插件描述: {pluginInfo.Description}");
                Console.WriteLine($"插件版本: {pluginInfo.Version}");
                Console.WriteLine($"插件类型: {pluginInfo.GetType()}");

                PluginsList.Add(PluginInfo);
            }
        }
    }
    
    
    /// <summary>
    /// 加载指定插件信息
    /// </summary>
    public static PluginInfo LoadPluginsInfo
        (IPlugin Plugin)
    { 
        return PluginInfo.CreatePluginInfo(Plugin.Name,Plugin.Description,Plugin.Version,Plugin.Type);
    }
    
    
    /// <summary>
    /// 加载插件入口程序
    /// </summary>
    /// <returns>返回插件入口程序的返回值</returns>
    public static object LoadPluginDoorFun
        (IPlugin Plugin)
    {
        // 执行入口程序
        Console.WriteLine();
        return Plugin.DoorFun();
    }
    
    
    /// <summary>
    /// 调用其他插件的Api接口(通过插件Name)
    /// </summary>
    public static object UsePluginMethod(List<object> ArgsList,IPlugin Plugin,string MethodName)
    {
        try
        {
            return Plugin.GetType().GetMethod(MethodName).Invoke(Plugin, ArgsList.ToArray());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
            return null;
        }
    }
    
}