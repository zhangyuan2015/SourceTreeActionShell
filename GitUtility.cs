using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceTreeActionShell
{
    public class GitUtility
    {
        public GitUtility(string workingDir, bool isWaitForExit = true)
        {
            WorkingDir = workingDir;
            IsWaitForExit = isWaitForExit;
        }

        public GitUtility(string workingDir, Action<string> dataReceived, bool isWaitForExit = true)
        {
            WorkingDir = workingDir;
            IsWaitForExit = isWaitForExit;
            DataReceived = dataReceived;
        }

        /// <summary>
        /// 获取环境git.ext的环境变量路径
        /// </summary>
        private string GetEnvironmentVariable
        {
            get
            {
                string strPath = Environment.GetEnvironmentVariable("Path");
                if (string.IsNullOrEmpty(strPath))
                {
                    return null;
                }

                string[] strResults = strPath.Split(';');
                for (int i = 0; i < strResults.Length; i++)
                {
                    if (!strResults[i].Contains(@"Git\cmd"))
                        continue;

                    strPath = strResults[i];
                }

                return strPath;
            }
        }

        /// <summary>        
        /// git工作路径
        /// </summary>
        public string WorkingDir { get; set; }
        public bool IsWaitForExit { get; set; }

        /// <summary>
        /// 执行git指令
        /// </summary>
        public string ExcuteGitCommand(string strCommnad)
        {
            string strGitPath = Path.Combine(GetEnvironmentVariable, "git.exe");
            if (string.IsNullOrEmpty(strGitPath))
            {
                return "Git环境错误";
            }

            Process p = new Process();
            p.StartInfo.FileName = strGitPath;
            p.StartInfo.Arguments = strCommnad;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.WorkingDirectory = WorkingDir;

            p.Start();
            if (IsWaitForExit)
                p.WaitForExit();

            string returnMsg = p.StandardOutput.ReadToEnd();

            return returnMsg;
        }

        /// <summary>
        /// 
        /// </summary>
        public Action<string> DataReceived { get; set; }

        public void ExcuteGitCommandReceived(string strCommnad)
        {
            string strGitPath = Path.Combine(GetEnvironmentVariable, "git.exe");
            if (string.IsNullOrEmpty(strGitPath))
            {
                DataReceived($"{DateTime.Now.ToShortTimeString()} - Git环境错误");
            }

            DataReceived($"{DateTime.Now.ToShortTimeString()} - 开始执行 - {strCommnad}{Environment.NewLine}");

            try
            {
                //string strInput = Console.ReadLine();
                Process p = new Process();
                //设置要启动的应用程序
                p.StartInfo.FileName = strGitPath;
                p.StartInfo.Arguments = strCommnad;
                p.StartInfo.WorkingDirectory = WorkingDir;
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = true;
                //输出信息
                p.StartInfo.RedirectStandardOutput = true;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;

                p.OutputDataReceived += Process_OutputDataReceived;
                p.ErrorDataReceived += Process_ErrorDataReceived;
                //启用Exited事件
                p.EnableRaisingEvents = true;
                p.Exited += new EventHandler(Process_Exited);

                //启动程序
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.StandardInput.AutoFlush = true;
                //输入命令
                //p.StandardInput.WriteLine(strCommnad);
                //p.StandardInput.WriteLine("exit");

                //获取输出信息
                // string strOuput = p.StandardOutput.ReadToEnd();
                //等待程序执行完退出进程
                p.WaitForExit();
                p.Close();
                // return strOuput;
            }
            catch (Exception ex)
            {
                DataReceived($"{DateTime.Now.ToShortTimeString()} - 异常:{ex.Message}");
            }
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            DataReceived($"{DateTime.Now.ToShortTimeString()} - 执行完毕");
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                DataReceived($"{DateTime.Now.ToShortTimeString()} - {e.Data}");
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                DataReceived($"{DateTime.Now.ToShortTimeString()} - {e.Data}");
            }
        }
    }
}
