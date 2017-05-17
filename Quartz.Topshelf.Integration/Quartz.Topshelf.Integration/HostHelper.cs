using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Quartz.Topshelf.Integration
{
    /// <summary>
    /// 启动服务类
    /// </summary>
    public class HostHelper
    {
        private readonly Func<QuartzService> _constructFunc = () => new QuartzService();

        public HostHelper(Func<QuartzService> constructFunc)
        {
            this._constructFunc = constructFunc;
        }

        public HostHelper()
        {

        }

        public void Start(string serviceName, string displayName = null, string description = null)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new Exception("错误：未定义服务名称！需定义有效的服务名称");
            }
            if (string.IsNullOrEmpty(displayName))
            {
                serviceName = displayName;
            }
            if (string.IsNullOrEmpty(description))
            {
                description = displayName;
            }

            HostFactory.Run(x =>
            {
                x.Service<QuartzService>(s =>
                {
                    s.ConstructUsing(name => _constructFunc());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription(description);
                x.SetDisplayName(displayName);
                x.SetServiceName(serviceName);
            });
        }
    }
}
