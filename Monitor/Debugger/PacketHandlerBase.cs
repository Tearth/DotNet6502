using Monitor.ViewModels;
using Protocol.Packets;

namespace Monitor.Debugger
{
    public abstract class PacketHandlerBase
    {
        protected MainWindowViewModel ViewModel;

        protected PacketHandlerBase(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public abstract byte[] Handle(PacketBase packet);
    }
}
