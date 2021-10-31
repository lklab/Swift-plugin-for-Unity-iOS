using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public static class BuildScript
{
#if UNITY_IOS
    private static string[] publicHeaderPaths = new string[]
    {
        "Libraries/Plugins/iOS/objc_bridge.h",
    };

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        string projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
        PBXProject proj = new PBXProject();
        proj.ReadFromFile(projPath);

        string frameworkTarget = proj.GetUnityFrameworkTargetGuid();

        string unityFrameworkHeaderText = File.ReadAllText(buildPath + "/UnityFramework/UnityFramework.h");
        unityFrameworkHeaderText += "\n";
        foreach (string headerPath in publicHeaderPaths)
        {
            string headerGuid = proj.FindFileGuidByProjectPath(headerPath);
            proj.AddPublicHeaderToBuild(frameworkTarget, headerGuid);

            unityFrameworkHeaderText += "#import \"" + Path.GetFileName(headerPath) + "\"\n";
        }
        File.WriteAllText(buildPath + "/UnityFramework/UnityFramework.h", unityFrameworkHeaderText);

        proj.WriteToFile(projPath);
    }
#endif
}
