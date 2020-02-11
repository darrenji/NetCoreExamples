using System;
using System.Collections.Generic;
using System.Linq;

namespace TotalNetCore.GetChildNodes
{
    class Program
    {
        static void Main(string[] args)
        {
            var childLocationIds = LocationHelper.GetLocationIdsByParent(4);
            foreach(var child in childLocationIds)
            {
                Console.WriteLine(child);
            }
            Console.ReadKey();
        }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int ParentId { get; set; }
    }

    public class LocationHelper
    {
        public static List<Location> GetLocations()
        {
            return new List <Location>{
                new Location{Id=1, Name="根节点", ParentId=-1, Level=0},
                new Location{Id=2, Name="楼层一", ParentId=1, Level=1},
                new Location{Id=3, Name="楼层二", ParentId=1, Level=1},
                new Location{Id=4, Name="楼层一区域A", ParentId=2, Level=2},
                new Location{Id=5, Name="楼层一区域B", ParentId=2, Level=2},
                new Location{Id=6, Name="楼层二区域C", ParentId=3, Level=2},
                new Location{Id=2, Name="楼层二区域D", ParentId=3, Level=2}
            };
        }

        public static List<int> GetLocationIdsByParent(int parentLocationId)
        {
            var result = new List<int>();

            GetLocationIdsByParentRecursively(parentLocationId, result);

            return result;
        }

        public static void GetLocationIdsByParentRecursively(int parentLocationId, List<int> result)
        {
            if(!result.Any(t=>t==parentLocationId))
            {
                result.Add(parentLocationId);
            }

            var locations = GetLocations();
            var childLocations = locations.Where(t => t.ParentId == parentLocationId);

            if(childLocations.Any())
            {
                foreach(var child in childLocations)
                {

                    if (!result.Any(t => t == child.Id))
                    {
                        result.Add(child.Id);
                    }
                    GetLocationIdsByParentRecursively(child.Id, result);
                }
            }

            
        }

    }

}
