using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETFrame
{
    class UserTokenPool
    {
        private Stack<UserToken> Pool;//可变大小的后进先出池
        public UserTokenPool(int max)
        {
            Pool = new Stack<UserToken>(max);//
        }
        /// <summary>
        /// 取出一个链接对象-----创建链接
        /// </summary>
        /// <returns></returns>
        public UserToken pop()
        {

            return Pool.Pop();//Pop:移除并返回对象
        }
        /// <summary>
        /// 插入一个链接对象-----释放链接
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>    //有参数会自动出来
        public void push(UserToken token)
        {
            if (token!=null)
            {
                Pool.Push(token);
            }
         }
        public int Size
        {
            get { return Pool.Count; } 
        }
    }
}
