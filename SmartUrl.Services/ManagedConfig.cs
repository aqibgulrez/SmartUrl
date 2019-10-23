using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUrl.Services
{
    public class ManagedConfig
    {
        public int KeyLength { get; set; }
        public int CollisionIterations { get; set; }
        public int UniqueUrlKeyIterations { get; set; }
        public string ApplicationPath { get; set; }
    }
}
