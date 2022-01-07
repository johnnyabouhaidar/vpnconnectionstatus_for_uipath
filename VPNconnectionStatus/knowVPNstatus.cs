using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace VPNconnectionStatus
{
    public class knowVPNstatus:CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> vpn_adapter { get; set; }


        [Category("Output")]
        [RequiredArgument]
        public OutArgument<Boolean> isVPNestablished { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var vpnAdapterName = vpn_adapter.Get(context);
            Boolean isVPNok = false;
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces =
                NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface Interface in interfaces)
                {
                    //Console.Write(Interface.Description + "\n");
                    if (Interface.Description.ToLower().Contains(vpnAdapterName)
                      && Interface.OperationalStatus == OperationalStatus.Up)
                    {
                        isVPNok=true;
                        
                    }
                }
            }
            isVPNestablished.Set(context, isVPNok);
            
        }
    }
}
