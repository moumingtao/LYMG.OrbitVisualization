using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky5.IO
{
    // 博客园：https://www.cnblogs.com/walkerwang/p/3690768.html
    // 原文：https://devblogs.microsoft.com/pfxteam/building-async-coordination-primitives-part-5-asyncsemaphore/
    public class AsyncSemaphore
    {
        private readonly Queue<TaskCompletionSource<bool>> m_waiters = new Queue<TaskCompletionSource<bool>>();
        private int m_currentCount;// 剩余可并行数量

        /// <summary>
        /// 创建一个异步的信号量
        /// </summary>
        /// <param name="initialCount">允许并行的数量</param>
        public AsyncSemaphore(int initialCount)
        {
            if (initialCount < 0) throw new ArgumentOutOfRangeException("initialCount");
            m_currentCount = initialCount;
        }
        // 当m_currentCount为0时阻塞
        public Task WaitAsync()
        {
            lock (m_waiters)
            {
                if (m_currentCount > 0)
                {
                    --m_currentCount;
                    return Task.CompletedTask;
                }
                else
                {
                    var waiter = new TaskCompletionSource<bool>();
                    m_waiters.Enqueue(waiter);
                    return waiter.Task;
                }
            }
        }
        // 一个操作结束，释放占有的并行数
        public void Release()
        {
            TaskCompletionSource<bool> toRelease = null;
            lock (m_waiters)
            {
                if (m_waiters.Count > 0)
                    toRelease = m_waiters.Dequeue();
                else
                    ++m_currentCount;
            }
            if (toRelease != null)
                toRelease.SetResult(true);
        }
    }
}
