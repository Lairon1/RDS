Note:  
First of all, you need to add the following scenes to your build settings scene list:

Initial Scene
CarChooseScene
CompleteScene



Second and last, For using the DemoScene1 you need to open the "Initial Scene" and play from there, this scene would load then the "CarChooseScene" and this last scene would then call the "CompleteScene" which is the race track scene, you can not play directly this last two scenes, since they depend on the LevelLoad object, which is on the first scene.

Thanks to 3D AutoSports for their Free Celica GT4 Model.