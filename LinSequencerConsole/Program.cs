using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LINSequencerLib;
using LINSequencerLib.BabyLinWrapper;
using LINSequencerLib.Sequence;

namespace LinSequencerConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            LinSequencer.InitializeLinSequencer();
            DeviceModel bl = null;           

            try
            {
                foreach (DeviceModel dev in LinSequencer.DeviceList)
                {
                    Console.WriteLine(dev);
                    foreach (ChannelModel channel in dev.ChannelList)
                    {
                        Console.WriteLine(channel);
                    }
                    bl = dev;                    
                }
                //Console.ReadLine();
                //bl.Connect();
                //bl.FrameChangedEvent += Dev_FrameChanged;
                //bl.RegisterFrameCallback();                
                //Console.WriteLine(bl.LoadSdf(Environment.CurrentDirectory + "\\sdf\\VW.sdf"));
                //Console.WriteLine(bl.FrameRaporting(0xff, 1));
                //Console.WriteLine(bl.Start());
                //Console.WriteLine($"Connection with {bl.Name} is established");
                //Console.ReadLine();
                //bl.GoToSleep();
                //Console.WriteLine($"Connection with {bl.Name} is sleep");
                //Thread.Sleep(5000);
                //Console.WriteLine($"Connection with {bl.Name} is starting");
                //bl.Start();
                //Console.ReadLine();
                //bl.Stop();
                //Console.WriteLine($"Connection with {bl.Name} is stop");
                //Console.ReadLine();
                //bl.Disconnect();
                //Console.WriteLine($"Connection with {bl.Name} is closed");
                //Console.ReadLine();
                //foreach (SeqFunction func in LinSequencer.FunctionList)
                //{
                //    Console.WriteLine($"{func} - {func.Description}");
                //    foreach(PropertyInfo prop in func.Properties)
                //    {
                //        Console.WriteLine($"\t{prop.Name} - type: {prop.PropertyType.Name}");
                //    }
                //}
                
                foreach (SequenceModel seq in LinSequencer.SequenceList)
                {
                    Console.WriteLine( seq.Name );
                }

                Console.WriteLine("Seq start");
                List<Task> tasks = new List<Task>
                {
                    //LinSequencer.RunSequenceAsync(bl, LinSequencer.SequenceList[0]),
                    //LinSequencer.RunSequenceAsync(bl, LinSequencer.SequenceList[1]),
                    //LinSequencer.RunSequenceAsync(bl, LinSequencer.SequenceList[2])
                };

                var watch = System.Diagnostics.Stopwatch.StartNew();
                await Task.WhenAll(tasks);
                watch.Stop();
                Console.WriteLine($"Stopwatch: {watch.ElapsedMilliseconds}");
                Console.WriteLine("Seq stop");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }            
        }

        private static void Bl_ErrorChangedEvent(object sender, ErrorModel e)
        {
            Console.WriteLine(e.ToString());
        }

        private static void Dev_FrameChanged(object sender, FrameModel e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
