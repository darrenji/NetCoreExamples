using System;
using System.Collections.Generic;
using System.Linq;

namespace TotalNetCore.AboutTime
{
    class Program
    {
        static void Main(string[] args)
        {
            // string str = "2020-02-14 10:10:01";

            //var t1 =  DateTime.Parse(str).Date;
            // var t2 = DateTime.Now.Date;

            // Console.WriteLine(t1);
            // Console.WriteLine(t2);
            // Console.WriteLine(t1 == t2);


            List<Scene> scenes = new List<Scene> {
            new Scene{ Id=1, Name="场景1"},
            new Scene{Id =2, Name="场景2"}
        };

            List<SceneResult> sceneResults = new List<SceneResult>
        {
            new SceneResult{Id=1, StartTime="2020-02-14 10:10:01",SceneId=1},
            new SceneResult{Id=2, StartTime="2020-02-14 10:27:01", SceneId=2}
        };


            var todaySceneResults = sceneResults.Where(t => DateTime.Parse(t.StartTime).Date == DateTime.Now.Date);
            foreach(var scene in scenes)
            {
                if(todaySceneResults.Any(t=>t.SceneId==scene.Id))
                {
                    var item = new MyViewModel { Name = scene.Name, StartTime = "" };
                }
                else
                {

                }
            }





            Console.ReadKey();

        }





    }

    public class Scene
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SceneResult
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public int SceneId { get; set; }
    }

    public class MyViewModel
    {
        public string Name { get; set; }
        public string StartTime { get; set; }
    }


}
