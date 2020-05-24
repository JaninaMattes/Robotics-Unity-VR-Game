using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript
{
  // Method needs to be static
  static void PerformBuild()
  {
    // list of scenes that will be injected into the build pipeline
    string[] defaultScene = {"Assets/Scenes/"};
    BuildPipeline.BuildPlayer(defaultScene, "./Builds/SensorSensation.", // TODO
    BuildTarget.StandaloneLinux64, BuildOptions.None);
  }
}
