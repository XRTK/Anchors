// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Definitions.Anchors;
using XRTK.Definitions.Utilities;

namespace XRTK.Interfaces.Anchors
{
    /// <summary>
    /// Interface contract for specific identity provider implementations for use in the <see cref="IMixedRealityAnchorSystem"/>.
    /// </summary>
    public interface IMixedRealityAnchorDataProvider : IMixedRealityExtensionDataProvider
    {
        /// <summary>
        /// The Instance Type for the Anchor
        /// </summary>
        SystemType AnchorType { get; }

        /// <summary>
        /// Is the current Anchor provider running
        /// </summary>
        bool IsRunning { get; }

        #region Public Methods

        /// <summary>
        /// Command the Anchor service to connect and enter a running state
        /// </summary>
        void StartAnchorProvider();

        /// <summary>
        /// Command the Anchor service to stop and disconnect from its cloud backend
        /// </summary>
        void StopAnchorProvider();

        /// <summary>
        /// Create a cloud anchor at a specified location using the slected prefab GameObject
        /// </summary>
        /// <param name="objectToAnchorPrefab">Prefab to place in the scene</param>
        /// <param name="position">Raycast position to place the prefab and localise the Cloud Anchor</param>
        /// <param name="rotation">Raycast rotation to place the prefab and localise the Cloud Anchor</param>
        /// <param name="timeToLive">Defined lifetime of the placed Cloud Anchor, informs the backend service to set a cache retention timeout</param>
        /// <remarks>The Position and Rotation are usually the result of a Raycast hit in to the AR scene for placement</remarks>
        void CreateAnchor(GameObject objectToAnchor, Vector3 position, Quaternion rotation, System.DateTimeOffset timeToLive);

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
        /// Instruct the cloud provider to locate a collection of Cloud Anchors using a specific type of search, e.g. Nearby
        /// </summary>
        /// <param name="searchType">The type of search to perform, specified by the <see cref="AnchorSearchType"/> type</param>
        /// <returns>Returns true of the location request to the service was successful</returns>
        /// <remarks>Does not return anchors, the <see cref="AnchorLocated"/> event will respond with discovered Anchors</remarks>
        bool FindAnchors(AnchorSearchType searchType);

        /// <summary>
        /// Places a GameObject or Prefab in the scene and wires up the CLoud anchor with its localised position
        /// </summary>
        /// <param name="id">String identifier for the cloud anchor platform to place</param>
        /// <param name="objectToAnchorPrefab">Prefab to place in the scene</param>
        /// <returns>Returns true of the object placement and anchor hook was successful, fails if the Anchor ID is unknown</returns>
        bool PlaceAnchor(string id, GameObject objectToAnchorPrefab);

        /// <summary>
        /// Does the selected GameObject currently have an anchor attached
        /// </summary>
        /// <param name="anchoredObject"></param>
        /// <returns></returns>
        bool HasAnchor(GameObject anchoredObject);

        /// <summary>
        /// Move Native anchor to a new position
        /// </summary>
        /// <remarks>
        /// Moving an object with an existing cloud anchor without referencing it's cached ID destroys the cloud position for the object
        /// </remarks>
        /// <param name="anchoredObject">Existing GameObject reference of an object to move</param>
        /// <param name="worldPos">New world position to move to</param>
        /// <param name="worldRot">New rotation to apply</param>
        /// <param name="cloudAnchorID">Preexisting cloud anchorID</param>
        /// <returns></returns>
        bool MoveAnchor(ref GameObject anchoredObject, Vector3 position, Quaternion rotation, string cloudAnchorID = "");

        /// <summary>
        /// Instruct the cloud provider to delete an individual Cloud Anchor by its ID/UID
        /// </summary>
        /// <param name="id"></param>
        void DeleteAnchor(string id);

        /// <summary>
        /// Instruct the cloud provider to delete a collection of Cloud Anchors by their ID/UID
        /// </summary>
        /// <param name="ids"></param>
        void DeleteAnchors(string[] ids);

        /// <summary>
        /// Instruct the anchor system to clear its cache of downloaded anchors.  Does not delete the anchors from the cloud service
        /// </summary>
        /// <returns></returns>
        bool TryClearAnchors();

        #endregion Public Methods

        #region Internal Anchor Provider Events

        /// <summary>
        /// Event fired when the Cloud Provider has finished initialising
        /// </summary>
        event Action SessionInitialised;

        /// <summary>
        /// Event fired when the Cloud Provider has begun searching for anchors
        /// </summary>
        event Action SessionStarted;

        /// <summary>
        /// Event fired when the Cloud Provider has stopped searching for anchors
        /// </summary>
        event Action SessionEnded;

        /// <summary>
        /// Event fired when the Cloud Provider has started creating an anchor.  Finishes when the Anchor is located.
        /// </summary>
        event Action CreateAnchorStarted;

        /// <summary>
        /// Event fired when the Cloud Provider has located an anchor from searching, may occur several times during a single search as they are located by the device.
        /// </summary>
        event Action LocateAnchor;

        /// <summary>
        /// Event fired when the Cloud Provider has deleted an anchor. 
        /// </summary>
        event Action<string> AnchorDeleted;

        #endregion Internal Anchor Provider Events

        #region Anchor Service Events

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

        #endregion Anchor Service Events
    }
}
