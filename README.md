# IdleCUE
Small application for turning the light off Corsair's ICUE compatible devices.

As an owner of both a Corsair Keyboard and Mouse, one thing that always bothered me is how you can't turn off the lights when the system is in an idle state. 
The solution was simply enough: create a task that starts when the system is idle, take control of the devices via the CUE API and listen for any peripheral activity using global hooks through Windows API.

This small piece of software uses [DarthAffe's CUE.NET](https://github.com/DarthAffe/CUE.NET), a wrapper to the [official CUE-SDK](https://github.com/CorsairOfficial/cue-sdk), and is not meant for general use. If you know a bit about programming, please feel free to clone the repository and modify it to your needs.
