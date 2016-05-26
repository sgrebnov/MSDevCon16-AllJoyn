# MSDevCon16-AllJoyn

# Environment

__Wifi: sgrebnov__

__Password: akvelon1__

# Steps to create the app

## Step1 Install required tools

1. Install [AllJoyn Extension for Visual Studio](https://visualstudiogallery.msdn.microsoft.com/064e58a7-fb56-464b-bed5-f85914c89286)

2. Install [IoT Explorer for AllJoyn](https://www.microsoft.com/en-us/store/apps/iot-explorer-for-alljoyn/9nblggh6gpxl)

## Step2 Create App

1. Create new `AllJoyn App (Universal Windows App)`

## Step3. Add logic to control the Lamp

1. Generate AllJoyn Interfaces for `org.allseen.LSF.LampState`

## Step4. Use the following code snippet to find and control the lamp

```
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
```


