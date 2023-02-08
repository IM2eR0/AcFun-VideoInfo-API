using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcFun
{
    public class HttpData
    {
        static JObject _JSON = new JObject();
        static int ind = 0;
        static public int Write(string t, int i = 0)
        {
            _JSON = new JObject();

            JObject obj1 = JObject.FromObject(new
            {
                title = t,
                duration = i,
            });

            _JSON.Add("vedioInfo",obj1);

            return 0;
        }
        static public string Read()
        {
            String paramString = _JSON.ToString(Newtonsoft.Json.Formatting.None, null);
            return paramString;
        }
    }
}
