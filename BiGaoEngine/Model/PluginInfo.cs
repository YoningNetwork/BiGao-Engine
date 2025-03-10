using System.Runtime.CompilerServices;

namespace BiGaoEngine.Model;
using BiGaoInterface;

public class PluginInfo
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Version { get; set; }
    public PluginType Type { get; set; }

    public string GetType()
    {
        if (this.Type == PluginType.GUI)
        {
            return "UI界面插件";
        }
        else
        {
            return "类库插件(依赖)";
        }
    }
    public static PluginInfo CreatePluginInfo(string name, string description, string version, PluginType type) =>
        new PluginInfo()
        {
            Name = name,
            Description = description,
            Version = version,
            Type = type
        };
}