# SourceTreeActionShell

| 命令 | 说明 |
|--|--|
| --Help | 帮助 |
| --merge-request | 发起一个合并请求 |

## 命令：--merge-request
说明：发起一个合并请求 

参数：
* ProjectPath=项目路径; 
* TargetBranch=目标分支; 
* GitScheme=Git方案：GitLab (默认) / GitHub

实现逻辑：
* 获取`ProjectPath=项目路径`的当前分支
* 获取`ProjectPath=项目路径`的远端地址
* 重新处理远端地址
  * 场景1 github ssh：git@github.com:zhangyuan2015/SourceTreeActionShell.git -> https://github.com:zhangyuan2015/SourceTreeActionShell
  * 场景2 github https：https://github.com/zhangyuan2015/SourceTreeActionShell.git -> https://github.com/zhangyuan2015/SourceTreeActionShell
  * 场景3 gitlab ssh：git@gitlab.com:zhangyuan20151/SourceTreeActionShell.git -> https://gitlab.com:zhangyuan20151/SourceTreeActionShell
  * 场景4 gitlab https：https://gitlab.com/zhangyuan20151/SourceTreeActionShell.git -> https://gitlab.com/zhangyuan20151/SourceTreeActionShell
* 构造合并请求URL
  * 场景1 github：https://github.com/zhangyuan2015/SourceTreeActionShell/compare/目标分支...源分支
  * 场景2 gitlab：https://gitlab.com/zhangyuan20151/SourceTreeActionShell/merge_requests/new?merge_request[source_branch]=源分支&merge_request[target_branch]=目标分支
* 通过系统设置的默认浏览器打开上述：合并请求URL
