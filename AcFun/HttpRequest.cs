using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AcFun
{
    class HttpRequest
    {
        private static readonly HttpClient client = new HttpClient();
        static public async Task<string> GetTitle(string _vid)
        {
            try
            {
                var result = await client.GetStringAsync("https://www.acfun.cn/v/" + _vid + "?quickViewId=videoInfo_new&ajaxpipe=1");

                string pattern = @"<span>(.*?)</span></h1>";
                Match _title = Regex.Match(result, pattern);
                return _title.Groups[1].Value;
            }
            catch(Exception ex)
            {
                return "404";
            }
        }
        static public async Task<int> GetDuration(string _vid)
        {
            try
            {
                var result = await client.GetStringAsync("https://www.acfun.cn/v/" + _vid + "?quickViewId=videoInfo_new&ajaxpipe=1");
                string br = result.Replace(@"\\\""", "\"");
                string br2 = br.Replace(@"\""", "\"");

                Match match = Regex.Match(br2, @"""duration"":(\d+)");
                return int.Parse(match.Groups[1].Value);
            }
            catch(Exception ex)
            {
                return 404;
            }
        }
        static public async Task<string> GetLink(string _vid)
        {
            try
            {
                var result = await client.GetStringAsync("https://www.acfun.cn/v/" + _vid + "?quickViewId=videoInfo_new&ajaxpipe=1");
                string br = result.Replace(@"\\\""", "\"");
                string br2 = br.Replace(@"\""", "\"");
                string pattern = @"""url"":""(.*?)"",";

                Match match = Regex.Match(br2, pattern);
                return match.Groups[1].Value;
            }
            catch (Exception ex)
            {
                return "404";
            }
        }
    }
}
