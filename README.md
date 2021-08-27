# aliyun-fast-oss-sdk
用于Aliyun OSS接口的SDK, 完全支持异步.

## 目标
由于阿里云官方提供的`CSharp SDK`太过于拉胯(可能出于历史原因), 底层竟然是`WebRequest`请求, 并且很多`IO`都是同步的, 出于性能和易用性考虑, 便有了此项目. 此项目旨在:

- [x] 完全的异步接口
- [ ] 完整的 [阿里云`OSS API`](https://help.aliyun.com/document_detail/31948.html) 接口支持
