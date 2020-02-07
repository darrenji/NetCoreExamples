using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace TotalNetCore.ByMrXiao.配置
{
    public class MyConfigurationProvider : ConfigurationProvider
    {

        System.Timers.Timer timer;

        public MyConfigurationProvider() : base()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 3000;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //每隔一段时间重新加载配置
            Load(true);//本类的方法
        }

        //重写基类的方法，采用本类的逻辑
        public override void Load()
        {
            Load(false);
        }

        //本类的实现方式
        void Load(bool reload)
        {
            this.Data["lastTime"] = DateTime.Now.ToString();//把当前时间保存起来
            if(reload)
            {
                base.OnReload();//调用基类方法
            }
        }
       
    }
}
