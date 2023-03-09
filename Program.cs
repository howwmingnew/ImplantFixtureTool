using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace PlayGround
{
    internal class Program
    {
        private static readonly string JsonExportPath = @"D:\Fixture.json";

        static void Main(string[] args)
        {
            Fixture fixture = new Fixture
            {
                Name = "Anker",
                ImplantSystemList = new List<Fixture.ImplantSystem>
                {
                    new Fixture.ImplantSystem
                    {
                        Name = "SBI",
                        ImplantList = new List<Fixture.ImplantSystem.Implant>
                        {
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3409",
                                Platform = 3.4f, Diameter = 2.5f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3410",
                                Platform = 3.4f, Diameter = 2.5f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3411",
                                Platform = 3.4f, Diameter = 2.5f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3413",
                                Platform = 3.4f, Diameter = 2.5f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3414",
                                Platform = 3.4f, Diameter = 2.5f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3807",
                                Platform = 3.8f, Diameter = 2.8f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3809",
                                Platform = 3.8f, Diameter = 2.8f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3810",
                                Platform = 3.8f, Diameter = 2.8f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3811",
                                Platform = 3.8f, Diameter = 2.8f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3813",
                                Platform = 3.8f, Diameter = 2.8f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS3815",
                                Platform = 3.8f, Diameter = 2.8f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4307",
                                Platform = 4.3f, Diameter = 3.1f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4309",
                                Platform = 4.3f, Diameter = 3.1f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4310",
                                Platform = 4.3f, Diameter = 3.1f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4311",
                                Platform = 4.3f, Diameter = 3.1f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4313",
                                Platform = 4.3f, Diameter = 3.1f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4315",
                                Platform = 4.3f, Diameter = 3.1f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4807",
                                Platform = 4.3f, Diameter = 3.7f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4809",
                                Platform = 4.3f, Diameter = 3.7f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4810",
                                Platform = 4.3f, Diameter = 3.7f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4811",
                                Platform = 4.3f, Diameter = 3.7f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4813",
                                Platform = 4.3f, Diameter = 3.7f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4815",
                                Platform = 4.3f, Diameter = 3.7f, Length = 15f
                            }
                        }
                    },
                    new Fixture.ImplantSystem
                    {
                        Name = "SBII",
                        ImplantList = new List<Fixture.ImplantSystem.Implant>
                        {
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3010",
                                Platform = 3.3f, Diameter = 2.2f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3011",
                                Platform = 3.3f, Diameter = 2.2f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3013",
                                Platform = 3.3f, Diameter = 2.2f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3015",
                                Platform = 3.3f, Diameter = 2.2f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3507",
                                Platform = 3.7f, Diameter = 2.5f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3508",
                                Platform = 3.7f, Diameter = 2.5f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3510",
                                Platform = 3.7f, Diameter = 2.5f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3511",
                                Platform = 3.7f, Diameter = 2.5f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3513",
                                Platform = 3.7f, Diameter = 2.5f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3515",
                                Platform = 3.7f, Diameter = 2.5f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4007",
                                Platform = 4.2f, Diameter = 2.8f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4008",
                                Platform = 4.2f, Diameter = 2.8f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4010",
                                Platform = 4.2f, Diameter = 2.8f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4011",
                                Platform = 4.2f, Diameter = 2.8f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4013",
                                Platform = 4.2f, Diameter = 2.8f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4015",
                                Platform = 4.2f, Diameter = 2.8f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4507",
                                Platform = 4.6f, Diameter = 3.1f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4508",
                                Platform = 4.6f, Diameter = 3.1f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4510",
                                Platform = 4.6f, Diameter = 3.1f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4511",
                                Platform = 4.6f, Diameter = 3.1f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4513",
                                Platform = 4.6f, Diameter = 3.1f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4515",
                                Platform = 4.6f, Diameter = 3.1f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5007",
                                Platform = 5.1f, Diameter = 3.7f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5008",
                                Platform = 5.1f, Diameter = 3.7f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5010",
                                Platform = 5.1f, Diameter = 3.7f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5011",
                                Platform = 5.1f, Diameter = 3.7f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5013",
                                Platform = 5.1f, Diameter = 3.7f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5015",
                                Platform = 5.1f, Diameter = 3.7f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS6007",
                                Platform = 6.1f, Diameter = 4.7f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS6008",
                                Platform = 6.1f, Diameter = 4.7f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS6010",
                                Platform = 6.1f, Diameter = 4.7f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS6011",
                                Platform = 6.1f, Diameter = 4.7f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS7007",
                                Platform = 7.1f, Diameter = 5.7f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS7008",
                                Platform = 7.1f, Diameter = 5.7f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS7010",
                                Platform = 7.1f, Diameter = 5.7f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS7011",
                                Platform = 7.1f, Diameter = 5.7f, Length = 11.5f
                            }
                        }
                    },
                    new Fixture.ImplantSystem
                    {
                        Name = "SBIII",
                        ImplantList = new List<Fixture.ImplantSystem.Implant>
                        {
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3508N",
                                Platform = 3.65f, Diameter = 2.5f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3510N",
                                Platform = 3.65f, Diameter = 2.5f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3511N",
                                Platform = 3.65f, Diameter = 2.5f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3513N",
                                Platform = 3.65f, Diameter = 2.5f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBN3515N",
                                Platform = 3.65f, Diameter = 2.5f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4007N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4008N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4010N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4011N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4013N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4015N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4018N",
                                Platform = 4.15f, Diameter = 2.8f, Length = 18f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4507N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4508N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4510N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4511N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4513N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4515N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 15f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS4518N",
                                Platform = 4.55f, Diameter = 3.1f, Length = 18f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5007N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 7f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5008N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 8.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5010N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 10f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5011N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 11.5f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5013N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 13f
                            },
                            new Fixture.ImplantSystem.Implant
                            {
                                Name = "SBS5015N",
                                Platform = 5.05f, Diameter = 3.7f, Length = 15f
                            }
                        }
                    }
                }
            };

            //Export json
            string jsonString = JsonConvert.SerializeObject(fixture, Formatting.Indented);
            File.WriteAllText(@"D:\Fixture.json", jsonString);

            //Import json
            //StreamReader streamReader = new StreamReader(@"D:\Fixture.json");
            //string jsonString = streamReader.ReadToEnd();
            //streamReader.Close();
            //Fixture importFixture = JsonConvert.DeserializeObject<Fixture>(jsonString);
        }
    }
}
