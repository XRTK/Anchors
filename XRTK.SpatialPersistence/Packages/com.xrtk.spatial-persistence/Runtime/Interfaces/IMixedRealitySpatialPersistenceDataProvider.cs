// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Definitions.SpatialPersistence;

namespace XRTK.Interfaces.SpatialPersistence
{
    /// <summary>
    /// Interface contract for specific identity provider implementations for use in the <see cref="IMixedRealitySpatialPersistenceSystem"/>.
    /// </summary>
    public interface IMixedRealitySpatialPersistenceDataProvider : IMixedRealityDataProvider
    {
        /// <summary>
        /// Is the current Spatial Persistence provider running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Command the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> to start.
        /// </summary>
        void StartSpatialPersistenceProvider();

        /// <summary>
        /// Command the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> to stop.
        /// </summary>
        void StopSpatialPersistenceProvider();

        /// <summary>
        /// Create a anchor at a specified location.
        /// </summary>
        /// <param name="position">Raycast position to place the prefab and localize the anchor.</param>
        /// <param name="rotation">Raycast rotation to place the prefab and localize the anchor.</param>
        /// <param name="timeToLive">Defined lifetime of the placed anchor, informs the service to set a cache retention timeout.</param>
        /// <remarks>The Position and Rotation are usually the result of a Raycast hit in to the AR scene for placement.</remarks>
        Guid CreateAnchoredObject(Vector3 position, Quaternion rotation, DateTimeOffset timeToLive);

        /// <summary>
        /// Instruct the provider to locate a collection of anchors by their ids.
        /// </summary>
        /// <param name="ids">Array of string identifiers for the anchor platform to locate</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        bool FindAnchorPoints(params Guid[] ids);

        /// <summary>
        /// Instruct the provider to locate a collection of anchors using a specific type of search, e.g. Local or Nearby
        /// </summary>
        /// <param name="searchType">The type of search to perform</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        bool FindAnchorPoints(SpatialPersistenceSearchType searchType);

        /// <summary>
        /// Moves the specified anchor to a new position and rotation.
        /// </summary>
        /// <param name="id">The id of the anchor to move.</param>
        /// <param name="position">New world position to move to.</param>
        /// <param name="rotation">New rotation to apply.</param>
        /// <returns>True, if the operation finished successfully.</returns>
        bool MoveAnchoredObject(Guid id, Vector3 position, Quaternion rotation);

        /// <summary>
        /// Instruct the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> to delete the specified anchor ids.
        /// </summary>
        /// <param name="ids">The collection of anchor ids to delete.</param>
        bool DeleteAnchors(params Guid[] ids);

        /// <summary>
        /// Instruct the <see cref="IMixedRealitySpatialPersistenceDataProvider"/> to clear its local anchor cache.
        /// </summary>
        bool TryClearAnchorCache();
    }
}
