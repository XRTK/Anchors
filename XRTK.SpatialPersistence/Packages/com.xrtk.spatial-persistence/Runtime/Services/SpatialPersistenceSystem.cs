// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using XRTK.Definitions.SpatialPersistence;
using XRTK.Interfaces.SpatialPersistence;

namespace XRTK.Services.SpatialPersistence
{
    /// <summary>
    /// Concrete implementation of the <see cref="IMixedRealitySpatialPersistenceSystem"/>
    /// </summary>
    [System.Runtime.InteropServices.Guid("C055102F-5204-42ED-A4D8-F80D129B6BBD")]
    public class SpatialPersistenceSystem : BaseSystem, IMixedRealitySpatialPersistenceSystem
    {
        /// <inheritdoc />
        public SpatialPersistenceSystem(SpatialPersistenceSystemProfile profile)
            : base(profile)
        {
        }

        /// <inheritdoc />
        public bool TryCreateAnchoredObject(GameObject prefab, Vector3 position, Quaternion rotation, DateTimeOffset timeToLive)
        {
            Debug.Assert(prefab, "Prefab Missing");

            // TODO Keep an internal reference to prefabs and their persistence data provider ids.

            foreach (var persistenceDataProvider in activeDataProviders)
            {
                var guid = persistenceDataProvider.CreateAnchoredObject(position, rotation, timeToLive);

                if (guid != Guid.Empty)
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryFindAnchorPoints(params Guid[] ids)
        {
            Debug.Assert(ids.Length > 0, "IDs required for SpatialPersistence search");

            foreach (var persistenceDataProvider in activeDataProviders)
            {
                if (persistenceDataProvider.FindAnchorPoints(ids))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryMoveSpatialPersistence(Guid id, Vector3 position, Quaternion rotation)
        {
            foreach (var persistenceDataProvider in activeDataProviders)
            {
                if (persistenceDataProvider.MoveAnchoredObject(id, position, rotation))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryDeleteAnchors(params Guid[] ids)
        {
            foreach (var persistenceDataProvider in activeDataProviders)
            {
                if (persistenceDataProvider.DeleteAnchors(ids))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryClearAnchorCache()
        {
            var anyClear = false;

            foreach (var persistenceDataProvider in activeDataProviders)
            {
                if (persistenceDataProvider.TryClearAnchorCache())
                {
                    anyClear = true;
                }
            }

            return anyClear;
        }

        private readonly HashSet<IMixedRealitySpatialPersistenceDataProvider> activeDataProviders = new HashSet<IMixedRealitySpatialPersistenceDataProvider>();

        /// <inheritdoc />
        public IReadOnlyCollection<IMixedRealitySpatialPersistenceDataProvider> ActiveSpatialPersistenceProviders => activeDataProviders;

        /// <inheritdoc />
        public bool RegisterSpatialPersistenceDataProvider(IMixedRealitySpatialPersistenceDataProvider provider)
        {
            if (activeDataProviders.Contains(provider))
            {
                return false;
            }

            activeDataProviders.Add(provider);
            return true;
        }

        /// <inheritdoc />
        public bool UnRegisterSpatialPersistenceDataProvider(IMixedRealitySpatialPersistenceDataProvider provider)
        {
            if (!activeDataProviders.Contains(provider))
            {
                return false;
            }

            activeDataProviders.Remove(provider);
            return true;
        }
    }
}
