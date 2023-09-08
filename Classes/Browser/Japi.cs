using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;

namespace PokeCard;
public class Japi
{
    public struct JsonCommandStruct
    {
        public string ObjName;
        public string FuncName;
        public Dictionary<string, string> Data;
    }

    public JsonCommandStruct JsonCommand;
    public string Response = "";

    public Japi(string json)
    {
        Debug.WriteLine(string.Format("New js command: {0}", json));

        JsonCommand = JsonConvert.DeserializeObject<JsonCommandStruct>(json);

        Type t = Type.GetType(@"PokeCard." + JsonCommand.ObjName);

        object[] Params = { JsonCommand.Data };

        MethodInfo method = t.GetMethod(JsonCommand.FuncName);

        var resp = method.Invoke(this, Params);

        if (resp != null && resp.GetType().Equals(typeof(string))) Response = (string)resp;

    }

}
