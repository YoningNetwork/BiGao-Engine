namespace BiGaoInterface;

public interface IPlugin
{
    /// <summary>
    /// 插件名称
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// 插件描述
    /// </summary>
    public string Description { get; }
    /// <summary>
    /// 插件版本
    /// </summary>
    public string Version { get; }
    /// <summary>
    /// 插件类型
    /// </summary>
    public PluginType Type { get; }
    /// <summary>
    /// 入口程序
    /// </summary>
    /// <returns></returns>
    public object DoorFun();
}