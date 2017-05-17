using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Common.Logging.Simple;
using Quartz.Impl;

namespace Quartz.Topshelf.Integration
{
    /// <summary>
    /// Quartz启动类
    /// </summary>
    public class QuartzService
    {
        private readonly IScheduler _scheduler;
        private static readonly NameValueCollection Config = new NameValueCollection
        {
            {"quartz.plugin.xml.fileNames", "~/quartz_jobs.xml"},
            {"quartz.plugin.xml.type", "Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin, Quartz"}
        };

        public QuartzService() : this(Config)
        {

        }

        public QuartzService(NameValueCollection config)
        {
            var factory = new StdSchedulerFactory(config);
            _scheduler = factory.GetScheduler();
        }

        public void Start()
        {
            _scheduler.Start();
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }

}
