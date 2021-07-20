// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Interfaces.Anchors;

namespace XRTK.Definitions.Anchors
{
    public class AnchorSystemProfile : BaseMixedRealityExtensionServiceProfile
    {
        [SerializeField]
        [Tooltip("The ARSession GameObject for the AR Scene")]
        private GameObject spatialManagerGameObject;

        /// <summary>
        /// The GameObject to attach the provider manager to.
        /// </summary>
        public GameObject SpatialManagerGameObject => spatialManagerGameObject;
    }
}
