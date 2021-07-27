// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace XRTK.Interfaces.SpatialPersistence
{
    /// <summary>
    /// Provider agnostic Interface contract for SpatialPersistence system integration.
    /// </summary>
    public interface IMixedRealitySpatialPersistenceSystem : IMixedRealitySystem
    {
        /// <summary>
        /// Create a anchor at a specified location using the selected prefab GameObject.
        /// </summary>
        /// <param name="prefab">Prefab to place in the scene when the anchor is recognized.</param>
        /// <param name="position">Raycast position to place the prefab and localize the anchor.</param>
        /// <param name="rotation">Raycast rotation to place the prefab and localize the anchor.</param>
        /// <param name="timeToLive">Defined lifetime of the placed anchor, informs the service to set a cache retention timeout.</param>
        /// <remarks>The Position and Rotation are usually the result of a Raycast hit in to the AR scene for placement.</remarks>
        bool TryCreateAnchoredObject(GameObject prefab, Vector3 position, Quaternion rotation, DateTimeOffset timeToLive);

        /// <summary>
        /// Instruct the cloud provider to locate a collection of anchors by their uuid.
        /// </summary>
        /// <param name="ids">Array of string identifiers for the cloud SpatialPersistence platform to locate</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        bool TryFindAnchorPoints(params Guid[] ids);

        /// <summary>
        /// Moves a currently anchored object to a new localized position.
        /// *Note, this in effect destroys the old SpatialPersistence and creates a new one.
        /// </summary>
        /// <param name="id">String identifier for the cloud SpatialPersistence platform to place</param>
        /// <param name="position">Raycast position to move the prefab to and re-localize the SpatialPersistence</param>
        /// <param name="rotation">Raycast rotation to move the prefab to and re-localize the SpatialPersistence</param>
        bool TryMoveSpatialPersistence(Guid id, Vector3 position, Quaternion rotation);

        /// <summary>
        /// Deletes the anchors with the specified ids.
        /// </summary>
        /// <param name="ids"></param>
        bool TryDeleteAnchors(params Guid[] ids);

        /// <summary>
        /// Clear the current cache of located Anchors from the provider services
        /// </summary>
        bool TryClearAnchorCache();

        /// <summary>
        /// The list of registered <see cref="IMixedRealitySpatialPersistenceDataProvider"/>s.
        /// </summary>
        IReadOnlyCollection<IMixedRealitySpatialPersistenceDataProvider> ActiveSpatialPersistenceProviders { get; }

        /// <summary>
        /// Registers the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> with the <see cref="IMixedRealitySpatialPersistenceSystem"/>.
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        bool RegisterSpatialPersistenceDataProvider(IMixedRealitySpatialPersistenceDataProvider dataProvider);

        /// <summary>
        /// UnRegisters the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> with the <see cref="IMixedRealitySpatialPersistenceSystem"/>.
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <returns></returns>
        bool UnRegisterSpatialPersistenceDataProvider(IMixedRealitySpatialPersistenceDataProvider dataProvider);
    }
}
