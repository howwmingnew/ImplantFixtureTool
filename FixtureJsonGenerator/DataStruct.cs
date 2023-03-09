using System.Collections.Generic;
using System.Windows.Media;

namespace FixtureJsonGenerator
{
    public class Fixture
    {
        public class ImplantSystem
        {
            public class Implant
            {
                public string Name { get; set; }
                public float Platform { get; set; }
                public float Diameter { get; set; }
                public float Length { get; set; }
                public SolidColorBrush Color { get; set; }

                public Implant()
                {
                    Name = "";
                    Platform = Diameter = Length = 0;
                    Color = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF000000");
                }
            }

            public string Name { get; set; }
            public List<Implant> ImplantList { get; set; }

            public ImplantSystem()
            {
                Name = "";
                ImplantList = new List<Implant>();
            }
        }

        public string Name { get; set; }
        public List<ImplantSystem> ImplantSystemList { get; set; }
        public Fixture()
        {
            Name = "";
            ImplantSystemList = new List<ImplantSystem>();
        }
    }
}
