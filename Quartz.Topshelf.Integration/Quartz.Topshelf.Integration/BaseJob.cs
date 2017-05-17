using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace Quartz.Topshelf.Integration
{
    /// <summary>
    /// Job父类
    /// </summary>
    public abstract class BaseJob : IJob
    {
        public abstract void Do(IJobExecutionContext context);

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Info("正在调用:" + GetType().FullName);
                Do(context);
                Info("调用成功:" + GetType().FullName);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        private void Error(Exception ex)
        {
            LogManager.GetLogger(GetType().Name).Error(ex);
        }

        private void Info(string msg)
        {
            LogManager.GetLogger(GetType().Name).Info(msg);
        }
    }
}
