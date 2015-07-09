using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class Container
    {
        public int ContainerID { get; set; }
        public string Name { get; set; }

        public static IEnumerable<string> GetPossibleContainers(int ViewID, IRepository SqlRepository)
        {
            string Procedure = "Report.spGetPossibleContainers";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@ViewID", ViewID}
            };

            return
                SqlRepository.Get<string>(Procedure, Parameters, true);
        }
    }
}
