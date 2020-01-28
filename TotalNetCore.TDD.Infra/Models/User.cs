using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.TDD.Infra.Models
{
    public class User
    {
        public User(int id, string name, int age, bool isActive)
        {
            this.Id = id;
            this.Name = name;
            this.Age = Age;
            this.IsActive = isActive;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
    }
}
