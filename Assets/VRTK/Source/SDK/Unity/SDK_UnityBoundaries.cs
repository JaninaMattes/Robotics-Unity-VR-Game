﻿// Unity Boundaries|SDK_Unity|004
namespace VRTK
{
    using UnityEngine;

    /// <summary>
    /// The Unity Boundaries SDK script provides a bridge to a default Unity play area.
    /// </summary>
    [SDK_Description(typeof(SDK_UnitySystem))]
    [SDK_Description(typeof(SDK_UnitySystem), 1)]
    [SDK_Description(typeof(SDK_UnitySystem), 2)]
    [SDK_Description(typeof(SDK_UnitySystem), 3)]
    [SDK_Description(typeof(SDK_UnitySystem), 4)]
    [SDK_Description(typeof(SDK_UnitySystem), 5)]
    public class SDK_UnityBoundaries : SDK_BaseBoundaries
    {
        /// <summary>
        /// The InitBoundaries method is run on start of scene and can be used to initialse anything on game start.
        /// </summary>
        public override void InitBoundaries()
        {
        }

        /// <summary>
        /// The GetPlayArea method returns the Transform of the object that is used to represent the play area in the scene.
        /// </summary>
        /// <returns>A transform of the object representing the play area in the scene.</returns>
        public override Transform GetPlayArea()
        {
            cachedPlayArea = GetSDKManagerPlayArea();
            if (cachedPlayArea == null)
            {
                GameObject foundCameraRig = VRTK_SharedMethods.FindEvenInactiveGameObject<SDK_UnityCameraRig>(null, true);
                if (foundCameraRig != null)
                {
                    cachedPlayArea = foundCameraRig.transform;
                }
            }

            return cachedPlayArea;
        }

        /// <summary>
        /// The GetPlayAreaVertices method returns the sonarHits of the play area boundaries.
        /// </summary>
        /// <returns>A Vector3 array of the sonarHits in the scene that represent the play area boundaries.</returns>
        public override Vector3[] GetPlayAreaVertices()
        {
            return null;
        }

        /// <summary>
        /// The GetPlayAreaBorderThickness returns the thickness of the drawn border for the given play area.
        /// </summary>
        /// <returns>The thickness of the drawn border.</returns>
        public override float GetPlayAreaBorderThickness()
        {
            return 0.1f;
        }

        /// <summary>
        /// The IsPlayAreaSizeCalibrated method returns whether the given play area size has been auto calibrated by external sensors.
        /// </summary>
        /// <returns>Returns true if the play area size has been auto calibrated and set by external sensors.</returns>
        public override bool IsPlayAreaSizeCalibrated()
        {
            return false;
        }

        /// <summary>
        /// The GetDrawAtRuntime method returns whether the given play area drawn border is being displayed.
        /// </summary>
        /// <returns>Returns true if the drawn border is being displayed.</returns>
        public override bool GetDrawAtRuntime()
        {
            return false;
        }

        /// <summary>
        /// The SetDrawAtRuntime method sets whether the given play area drawn border should be displayed at runtime.
        /// </summary>
        /// <param name="value">The state of whether the drawn border should be displayed or not.</param>
        public override void SetDrawAtRuntime(bool value)
        {
        }
    }
}