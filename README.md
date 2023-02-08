# AcFun-VideoInfo-API
AcFun视频基本信息解析
## 程序运行要求
- Windows
- .NET 7 Runtime

## 使用方式 HTTP/GET
- 默认情况下，AcFun程序将会在`http://localhost:9966`上开启监听
### 信息获取
- 访问示例：
浏览器打开 `http://localhost:9966/?id=ac40594323`
- 返回结果(json)：
```json
{
  "code": 200,
  "message": "success",
  "data": {
    "vedioInfo": {
      "title": "《动物迷惑行为大赏140》",
      "duration": 320085
    }
  }
}
```
### 直接进行播放
- 访问示例：
浏览器 `http://localhost:9966/?play=ac40594323`
- 返回结果：HTML页面