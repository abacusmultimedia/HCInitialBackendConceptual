using Newtonsoft.Json;

namespace zero.Shared.Models;

public abstract class LogableModel
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
    }
}