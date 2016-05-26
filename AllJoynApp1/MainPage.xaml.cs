using org.allseen.LSF.LampState;
using System;
using Windows.Devices.AllJoyn;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AllJoynApp1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            FindLamp();
        }

        private LampStateConsumer _lamp;

        private void FindLamp()
        {
            LampStateWatcher watcher = new LampStateWatcher(new AllJoynBusAttachment());
            watcher.Added += async (sender, args) =>
            {
                var joinResult = await LampStateConsumer.JoinSessionAsync(args, sender);

                if (joinResult.Status == AllJoynStatus.Ok)
                {
                    this._lamp = joinResult.Consumer;
                    txtLamp.Text = "LIFX Color 1000 BR30: найдена";
                };
            };
            watcher.Start();
        }

        private async void LampOff(object sender, RoutedEventArgs e)
        {
            if (_lamp == null) return;
            await this._lamp.SetBrightnessAsync(0);
        }

        private async void LampOn(object sender, RoutedEventArgs e)
        {
            if (_lamp == null) return;
            // 10%
            await this._lamp.SetBrightnessAsync(/* 10% */ Convert.ToUInt32(UInt32.MaxValue * 0.1));
        }    

    }
}
