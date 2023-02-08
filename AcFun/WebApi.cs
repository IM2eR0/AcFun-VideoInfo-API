using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static AcFun.Util;

namespace AcFun
{
    class WebApi
    {
        private HttpListener _listener;
        public void Start()
        {
            try
            {
                Thread t = new Thread(() =>
                {
                    _listener = new HttpListener();
                    _listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                    _listener.Prefixes.Add("http://+:9966/");
                    _listener.Start();
                    Print("服务器已在 http://localhost:9966/ 上开启监听");

                    while (true)
                    {
                        //等待请求连接
                        //没有请求则GetContext处于阻塞状态
                        HttpListenerContext ctx = _listener.GetContext();

                        ThreadPool.QueueUserWorkItem(new WaitCallback(ListenerHandle), ctx);
                        //_listener.BeginGetContext(ListenerHandle, ctx);
                    }
                    //
                });

                t.Start();
            }
            catch (Exception ex)
            {
                Print("监听开启失败：" + ex.Message, PrintType.ERROR);
            }
        }
        private async void ListenerHandle(object o)
        {
            HttpListenerContext ctx = (HttpListenerContext)o;

            var _Method = ctx.Request.HttpMethod;

            if (_Method == "GET")
            {
                string url = ctx.Request.Url.ToString();
                string _vid = "";
                string _ttt = "";

                if (url.Contains("?"))
                {

                    string[] pars = url.Split('?');

                    if (pars.Length == 0)
                    {
                        return;
                    }
                    string canshus = pars[1];

                    if (canshus.Length > 0)
                    {
                        string[] canshu = canshus.Split('&');
                        foreach (string i in canshu)
                        {
                            string[] messages = i.Split('=');
                            _ttt = messages[0];
                            _vid = messages[1];
                        }
                    }
                }

                if (_ttt == "id")
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.ContentType = "application/json;charset=UTF-8";
                    ctx.Response.ContentEncoding = Encoding.UTF8;
                    ctx.Response.AppendHeader("Content-Type", "application/json;charset=UTF-8");

                    var _title = await HttpRequest.GetTitle(_vid);
                    var _duration = await HttpRequest.GetDuration(_vid);

                    if (_title == "404" && _duration == 404)
                    {
                        using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream, Encoding.UTF8))
                        {

                            writer.Write("{" +
                                $"\"code\" : {ctx.Response.StatusCode}," +
                                "\"message\" : \"failed\"," +
                                "\"error\" : \"404 NotFound\"" +
                                "}");
                            writer.Close();
                            ctx.Response.Close();
                        }
                    }
                    else
                    {
                        HttpData.Write(_title, _duration);

                        using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream, Encoding.UTF8))
                        {

                            writer.Write("{" +
                                $"\"code\" : {ctx.Response.StatusCode}," +
                                "\"message\" : \"success\"," +
                                "\"data\" : " +
                                HttpData.Read() +
                                "}");
                            writer.Close();
                            ctx.Response.Close();
                        }
                    }
                }
                else if (_ttt == "play")
                {
                    string _url = await HttpRequest.GetLink(_vid);
                    string html = "<!DOCTYPE html>" +
                        "<html lang=\"zh-CN\">" +
                        "<head>" +
                        "<meta charset=\"UTF-8\">" +
                        "<title>前端播放m3u8格式视频</title>" +
                        "<link href=\"https://vjs.zencdn.net/7.4.1/video-js.css\" rel=\"stylesheet\">" +
                        "<script src='https://vjs.zencdn.net/7.4.1/video.js'></script>" +
                        "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/videojs-contrib-hls/5.15.0/videojs-contrib-hls.min.js\" type=\"text/javascript\"></script>" +
                        "</head>" +
                        "<body>" +
                        "<style>" +
                        ".video-js .vjs-tech {position: relative !important;}" +
                        "</style>" +
                        "<div>" +
                        "<video id=\"myVideo\" class=\"video-js vjs-default-skin vjs-big-play-centered\" controls preload=\"auto\" data-setup='{}' style='width: 100%;height: auto'>" +
                        "<source id=\"source\" src=" +
                        _url +
                        " type=\"application/x-mpegURL\"></source>" +
                        "</video>" +
                        "</div>" +
                        "</body>" +
                        "<script>" +
                        "var myVideo = videojs('myVideo', {bigPlayButton: true,textTrackDisplay: false,posterImage: false,errorDisplay: false,})" +
                        "myVideo.play()" +
                        "var changeVideo = function (vdoSrc) {" +
                        "if (/\\.m3u8$/.test(vdoSrc)) {" +
                        "myVideo.src({" +
                        "src: vdoSrc," +
                        "type: 'application/x-mpegURL'" +
                        "})" +
                        "} else {" +
                        "myVideo.src(vdoSrc)" +
                        "}myVideo.load();myVideo.play();" +
                        "var src = ''; myVideo.play();" +
                        "</script>";

                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);

                    ctx.Response.ContentLength64 = buffer.Length;
                    ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.ContentType = "application/json;charset=UTF-8";
                    ctx.Response.ContentEncoding = Encoding.UTF8;
                    ctx.Response.AppendHeader("Content-Type", "application/json;charset=UTF-8");

                    using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream, Encoding.UTF8))
                    {

                        writer.Write("{" +
                            $"\"code\" : {ctx.Response.StatusCode}," +
                            "\"message\" : \"failed\"," +
                            "\"error\" : \"parm error\"" +
                            "}");
                        writer.Close();
                        ctx.Response.Close();
                    }
                }
            }
        }
    }
}
