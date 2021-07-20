// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using XRTK.Definitions.Anchors;

namespace XRTK.Interfaces.Anchors
{
    /// <summary>
    /// Interface contract for specific identity provider implementations for use in the <see cref="IMixedRealityAnchorSystem"/>.
    /// </summary>
    public interface IMixedRealityAnchorDataProvider : IMixedRealityExtensionDataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        SupportedAnchorType AnchorType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsRunning { get; }

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        void StartAnchorProvider();

        /// <summary>
        /// 
        /// </summary>
        void StopAnchorProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectToAnchor"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="timeToLive"></param>
        void CreateAnchor(GameObject objectToAnchor, Vector3 position, Quaternion rotation, System.DateTimeOffset timeToLive);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool FindAnchor(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool FindAnchors(string[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        bool FindAnchors(AnchorSearchType searchType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objectToAnchorPrefab"></param>
        /// <returns></returns>
        bool PlaceAnchor(string id, GameObject objectToAnchorPrefab);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anchoredObject"></param>
        /// <returns></returns>
        bool HasAnchor(ref GameObject anchoredObject);

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
        /// 
        /// </summary>
        /// <param name="id"></param>
        void DeleteAnchor(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        void DeleteAnchors(string[] ids);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool ClearAnchors();

        #endregion Public Methods

        #region Internal Anchor Provider Events

        /// <summary>
        /// 
        /// </summary>
        event Action SessionInitialised;

        /// <summary>
        /// 
        /// </summary>
        event Action SessionStarted;

        /// <summary>
        /// 
        /// </summary>
        event Action SessionEnded;

        /// <summary>
        /// 
        /// </summary>
        event Action CreateAnchorStarted;

        /// <summary>
        /// 
        /// </summary>
        event Action LocateAnchor;

        /// <summary>
        /// 
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
