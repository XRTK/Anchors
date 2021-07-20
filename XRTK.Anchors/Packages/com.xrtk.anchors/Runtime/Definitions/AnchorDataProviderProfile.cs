// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using XRTK.Definitions.Utilities;
using XRTK.Interfaces.Anchors;

namespace XRTK.Definitions.Anchors
{
    /// <summary>
    /// The configuration profile for <see cref="IMixedRealityAnchorDataProvider"/>.
    /// </summary>
    [CreateAssetMenu(menuName = "Mixed Reality Toolkit/Anchors/Anchor Data Provider", fileName = "AnchorDataProviderProfile", order = (int)CreateProfileMenuItemIndices.RegisteredServiceProviders)]
    public class AnchorDataProviderProfile : BaseMixedRealityExtensionDataProviderProfile
    { }
}
