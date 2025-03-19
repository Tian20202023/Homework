using System;
namespace MyApp2
{
    using System.Timers;
    public class AlarmClock
    {
        public event Action Tick;
        public event Action Alarm;

        private Timer timer;
        private DateTime alarmTime;
        private bool alarmSet = false;

        public AlarmClock()
        {
            timer = new Timer(1000); // 1秒触发一次
            timer.Elapsed += OnTick;
        }

        public void Start()
        {
            timer.Start();
            Console.WriteLine("闹钟已启动...");
        }

        public void Stop()
        {
            timer.Stop();
            Console.WriteLine("闹钟已停止。");
        }

        public void SetAlarm(DateTime time)
        {
            alarmTime = time;
            alarmSet = true;
            Console.WriteLine($"闹钟已设置，响铃时间: {alarmTime:HH:mm:ss}");
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            Tick?.Invoke();
            Console.WriteLine($"嘀嗒... {DateTime.Now:HH:mm:ss}");
        
            if (alarmSet && DateTime.Now >= alarmTime)
            {
                Alarm?.Invoke();
                Console.WriteLine("响铃！！！");
                alarmSet = false; // 只响一次
            }
        }
    }

    class Program
    {
        static void Main()
        {
            AlarmClock clock = new AlarmClock();
        
            // 订阅事件
            clock.Tick += () => Console.WriteLine("闹钟滴答作响...");
            clock.Alarm += () => Console.WriteLine("闹钟响了，起床！");
        
            clock.Start();
        
            // 设置一个 5 秒后的闹钟
            DateTime alarmTime = DateTime.Now.AddSeconds(5);
            clock.SetAlarm(alarmTime);
        
            Console.ReadLine(); // 阻止程序立即退出
            clock.Stop();
        }
    }

}