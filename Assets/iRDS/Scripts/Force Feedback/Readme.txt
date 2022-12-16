To use forcefeedback with iRDS follow this instructions:

1. Make sure to import the Logitech SDK into your project, download it from the asset store
1.1. Make sure the right Logitech Dll is on the plugins folder under Assets/Plugins
2. Extract the IRDSLogitechFFB.cs into the Force Feedback folder
3. Attach the IRDSLogitechFFB.cs script to the car
4. Setup the car to use external input (if you want to manually modify the IRDSLogitechFFB.cs script to 
   add your inputs, you need to disable the justUseForceFeedback option on the IRDSLogitechFFB.cs script) 
   or use the Analog input in the Player controls settings in the control type option
   and assign the wheel axis and buttons to the correct unity settings 
   on the car directly if IRDSManager is not used And in levelload if IRDSManager is used
5. Enjoy!