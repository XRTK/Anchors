// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using XRTK.Definitions.Anchors;
using XRTK.Interfaces.Anchors;

namespace XRTK.Services.Anchors
{
    /// <summary>
    /// Concrete implementation of the <see cref="IMixedRealityAuthenticationSystem"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("C055102F-5204-42ED-A4D8-F80D129B6BBD")]
    public class AnchorSystem : BaseExtensionService, IMixedRealityAnchorSystem
    {
        private IMixedRealityAnchorDataProvider currentAnchorProvider;
        private GameObject spatialManagerGameObject;
        public GameObject SpatialManagerGameObject => spatialManagerGameObject;

        [HideInInspector]
        public IMixedRealityAnchorDataProvider CurrentAnchorProvider => currentAnchorProvider;

        /// <inheritdoc />
        public AnchorSystem(string name, uint priority, AnchorSystemProfile profile) : base(name, priority, profile)
        {
            spatialManagerGameObject = profile.SpatialManagerGameObject;
        }

        #region IMixedRealityAnchorSystem Implementation

        public void CreateAnchor(GameObject objectToAnchorPrefab, Vector3 position, Quaternion rotation, DateTimeOffset timeToLive)
        {
            Debug.Assert(objectToAnchorPrefab, "Prefab Missing");
            Debug.Assert(timeToLive == new DateTimeOffset(), "Lifetime of Anchor required");

            currentAnchorProvider.CreateAnchor(objectToAnchorPrefab, position, rotation, timeToLive);
        }

        public bool FindAnchor(string id)
        {
            Debug.Assert(string.IsNullOrEmpty(id), "ID required for Anchor search");

            return currentAnchorProvider.FindAnchor(id);
        }

        public bool FindAnchors(string[] ids)
        {
            Debug.Assert(ids.Length > 0, "IDs required for Anchor search");

            return currentAnchorProvider.FindAnchors(ids);
        }

        public bool PlaceAnchor(string id, GameObject objectToAnchorPrefab)
        {
            Debug.Assert(!string.IsNullOrEmpty(id), "Anchor ID is null");
            Debug.Assert(objectToAnchorPrefab != null, "Object To Anchor Prefab is null");

            return currentAnchorProvider.PlaceAnchor(id, objectToAnchorPrefab);
        }

        public bool MoveAnchor(ref GameObject anchoredObject, Vector3 worldPos, Quaternion worldRot, string cloudAnchorID = "")
        {
            Debug.Assert(anchoredObject != null, "Currently Anchored GameObject reference required");

            return currentAnchorProvider.MoveAnchor(ref anchoredObject, worldPos, worldRot, cloudAnchorID);
        }

        public bool TryClearAnchors() => currentAnchorProvider.TryClearAnchors();

        /// <inheritdoc />
        public event Action CreateAnchorFailed;

        /// <inheritdoc />
        public event Action<string, GameObject> CreateAnchorSucceeded;

        /// <inheritdoc />
        public event Action<string> AnchorStatusMessage;

        /// <inheritdoc />
        public event Action<string, GameObject> AnchorUpdated;

        /// <inheritdoc />
        public event Action<string> AnchorLocated;

        /// <inheritdoc />
        public event Action<string> AnchorError;
        #endregion IMixedRealityAnchorSystem Implementation

        #region BaseSystem Implementation

        private readonly HashSet<IMixedRealityAnchorDataProvider> activeDataProviders = new HashSet<IMixedRealityAnchorDataProvider>();

        /// <inheritdoc />
        public IReadOnlyCollection<IMixedRealityAnchorDataProvider> ActiveAnchorProviders => activeDataProviders;

        /// <inheritdoc />
        public bool RegisterAnchorDataProvider(IMixedRealityAnchorDataProvider provider)
        {
            if (activeDataProviders.Contains(provider))
            {
                return false;
            }

            activeDataProviders.Add(provider);
            AnchorEvents(provider, true);
            return true;
        }

        /// <inheritdoc />
        public bool UnregisterAnchorDataProvider(IMixedRealityAnchorDataProvider provider)
        {
            if (!activeDataProviders.Contains(provider))
            {
                return false;
            }

            AnchorEvents(provider, false);
            activeDataProviders.Remove(provider);
            return true;
        }

        private void AnchorEvents(IMixedRealityAnchorDataProvider provider, bool isRegistered)
        {
            if (activeDataProviders != null && activeDataProviders.Contains(provider))
            {
                if (isRegistered)
                {
                    provider.CreateAnchorSucceeded += CreateAnchorSucceeded;
                    provider.CreateAnchorFailed += CreateAnchorFailed;
                    provider.AnchorStatusMessage += AnchorStatusMessage;
                    provider.AnchorUpdated += AnchorUpdated;
                    provider.AnchorLocated += AnchorLocated;
                    provider.AnchorError += AnchorError;
                }
                else
                {
                    provider.CreateAnchorSucceeded -= CreateAnchorSucceeded;
                    provider.CreateAnchorFailed -= CreateAnchorFailed;
                    provider.AnchorStatusMessage -= AnchorStatusMessage;
                    provider.AnchorUpdated -= AnchorUpdated;
                    provider.AnchorLocated -= AnchorLocated;
                    provider.AnchorError -= AnchorError;
                }
            }
        }

        #endregion BaseSystem Implementation
    }
}
