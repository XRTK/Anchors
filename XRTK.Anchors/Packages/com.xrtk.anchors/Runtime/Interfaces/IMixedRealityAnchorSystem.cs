// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace XRTK.Interfaces.Anchors
{
    /// <summary>
    /// Provider agnostic Interface contract for Cloud Anchor system integration</see>.
    /// </summary>
    public interface IMixedRealityAnchorSystem : IMixedRealitySystem
    {
        /// <summary>
        /// The current <see cref="IMixedRealityAnchorDataProvider"/>
        /// </summary>
        IMixedRealityAnchorDataProvider CurrentAnchorProvider { get; }
        GameObject SpatialManagerGameObject { get; }

        #region Public Methods

        /// <summary>
        /// Create a cloud anchor at a specified location using the slected prefab GameObject
        /// </summary>
        /// <param name="objectToAnchorPrefab">Prefab to place in the scene</param>
        /// <param name="position">Raycast position to place the prefab and localise the Cloud Anchor</param>
        /// <param name="rotation">Raycast rotation to place the prefab and localise the Cloud Anchor</param>
        /// <param name="timeToLive">Defined lifetime of the placed Cloud Anchor, informs the backend service to set a cache retention timeout</param>
        /// <remarks>The Position and Rotation are usually the result of a Raycast hit in to the AR scene for placement</remarks>
        void CreateAnchor(GameObject objectToAnchorPrefab, Vector3 position, Quaternion rotation, DateTimeOffset timeToLive);

        /// <summary>
        /// Instruct the cloud provider to locate an individual Cloud Anchor by its ID/UID
        /// </summary>
        /// <param name="id">String identifier for the cloud anchor platform to locate</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        /// <remarks>Does not return an anchor, the <see cref="AnchorLocated"/> event will respond with discovered Anchors</remarks>
        bool FindAnchor(string id);

        /// <summary>
        /// Instruct the cloud provider to locate a collection of Cloud Anchors by their ID/UID
        /// </summary>
        /// <param name="ids">Array of string identifiers for the cloud anchor platform to locate</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        /// <remarks>Does not return anchors, the <see cref="AnchorLocated"/> event will respond with discovered Anchors</remarks>
        bool FindAnchors(string[] ids);

        /// <summary>
        /// Places a GameObject or Prefab in the scene and wires up the CLoud anchor with its localised position
        /// </summary>
        /// <param name="id">String identifier for the cloud anchor platform to place</param>
        /// <param name="objectToAnchorPrefab">Prefab to place in the scene</param>
        /// <returns>Returns true of the object placement and anchor hook was successful, fails if the Anchor ID is unknown</returns>
        bool PlaceAnchor(string id, GameObject objectToAnchorPrefab);

        /// <summary>
        /// Moves a currently anchored object to a new localised position.
        /// *Note, this in effect destroys the old anchor and creates a new one.
        /// </summary>
        /// <param name="anchoredObject">Object in the scene to move</param>
        /// <param name="position">Raycast position to move the prefab to and relocalise the Cloud Anchor</param>
        /// <param name="rotation">Raycast rotation to move the prefab to and relocalise the Cloud Anchor</param>
        /// <param name="cloudAnchorID">String identifier for the cloud anchor platform to place</param>
        /// <returns></returns>
        bool MoveAnchor(ref GameObject anchoredObject, Vector3 position, Quaternion rotation, string cloudAnchorID = "");

        /// <summary>
        /// Clear the current cache of located Anchors from the provider services
        /// </summary>
        /// <returns></returns>
        bool TryClearAnchors();

        #endregion Public Methods

        #region Public Events

        /// <summary>
        /// The Anchor provider reports that creation of the Anchor failed
        /// </summary>
        event Action CreateAnchorFailed;

        /// <summary>
        /// The Anchor provider reports that creation of the Anchor succeeded
        /// </summary>
        event Action<string, GameObject> CreateAnchorSucceeded;

        /// <summary>
        /// Status message whilst the Anchor service is localising the anchor in place, continues until complete or a failure occurs
        /// </summary>
        event Action<string> AnchorStatusMessage;

        /// <summary>
        /// General service failure from the Anchor provider
        /// </summary>
        event Action<string> AnchorError;

        /// <summary>
        /// Notification that the provider has performed an operation on an object in the scene
        /// </summary>
        event Action<string, GameObject> AnchorUpdated;

        /// <summary>
        /// Location request to Anchor service successful and a localised anchor was found and cached.
        /// </summary>
        event Action<string> AnchorLocated;

        #endregion Public Events
    }
}
