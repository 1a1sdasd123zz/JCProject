using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vision.BaseClass.Module;

namespace Vision.Communication
{
    public interface IFlowState
    {
        string SerialNum { get; }

        bool IsConnected { get; }

        EndianEnum Endian { get; set; }

        string CommTypeName { get; }

        event Action<int> JobChanged;

        event ConnectedEventHandler CommConnected;

        event TriggerEventHandler Trigger;

        void SetInputsOutputs(InputsOutputs<Comm_Element, Info> commConfigData);

        void SendCommElements(List<Comm_Element> elements, string remoteIp = null);

        void ResetSystemState();

        void AcqCompeleted(Comm_Element comm_Element);

        void InspectCompeleted(List<Comm_Element> comm_Element);

        void JobChangeCompeleted(int currentJobId);

        void SystemOffLine();

        void SystemOnLine();

        void Close();
    }
}
