using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sciencecom.Models
{
    using Models;
    public class Constructions
    {
        public List<MetalConstruction> MetalConstructions { get; set; }
        public List<LightConstruction> LightConstructions { get; set; }
        public List<IllegalConstruction> IllegalConstructions { get; set; }
    }
}