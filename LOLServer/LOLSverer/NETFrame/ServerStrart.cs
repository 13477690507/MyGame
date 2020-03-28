using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETFrame
{
   public class ServerStrart
    {
        Socket server;//服务器soket监听对象
        Semaphore acceptClients; //限制可同时访问某一资源或者资源对象的线程数
        UserTokenPool pool;
        int MaxClient = 0;//最大客户端连接数
        /// <summary>
        /// 初始化通讯监听
        /// </summary>
        ///<param name="max">监听端口</param>
        public ServerStrart(int max)
        {
            //服务器的         寻址. ip4地址（国际互联网络）通讯类型.链接      通讯协议.tcp
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//实例化监听对象
            //设定服务器最大链接人数
            MaxClient = max;//最大客户端数
            //创建对象池
            pool = new UserTokenPool(max);
            //链接用户量
            acceptClients = new Semaphore(max, max);
            for (int i = 0; i < max; i++)
            {
                UserToken token = new UserToken();
                //初始化token信息
                pool.push(token);
            }
        }
        public void Start(int port)
        {
            //监听服务器所有端口
            server.Bind(new IPEndPoint(IPAddress.Any, port));
            //置于监听状态
            server.Listen(10);
            StartAccept(null);
        }
        /// <summary>
        /// 开始连接监听
        /// </summary>
        /// <param name=""></param>
        public void StartAccept(SocketAsyncEventArgs E)
        {
            //如果传入为空的话说明调用新的客户端连接监听事件，否则 移除当前客户端。
            if (E==null)
            {
                E = new SocketAsyncEventArgs();
                E.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Comleted);
            }
            else 
            {
                E.AcceptSocket= null; 
            }
            acceptClients.WaitOne();//信号量-1
            bool result = server.AcceptAsync(E);//判断异步事件是否挂起

        }
        public void Accept_Comleted(object sender, SocketAsyncEventArgs e)
        {

        }
    }
}
