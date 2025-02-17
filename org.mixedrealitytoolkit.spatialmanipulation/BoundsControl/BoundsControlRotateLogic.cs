// Copyright (c) Mixed Reality Toolkit Contributors
// Licensed under the BSD 3-Clause

using System.Collections.Generic;
using UnityEngine;


namespace MixedReality.Toolkit.SpatialManipulation
{
    /// <summary>
    /// This class defines the default manipulation logic for rotation handles of bounds control
    /// </summary>
    public class BoundsControlRotateLogic : ManipulationLogic<Quaternion>
    {
        private BoundsControl boundsCont;
        private BoundsHandleInteractable currentHandle;
        private Vector3 initialGrabPoint;
        private Vector3 currentManipulationAxis;
        private MixedRealityTransform initialTransformOnGrabStart;

        /// <inheritdoc />
        public override void Setup(List<UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor> interactors, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable, MixedRealityTransform currentTarget)
        {
            base.Setup(interactors, interactable, currentTarget);

            currentHandle = interactable.transform.GetComponent<BoundsHandleInteractable>();
            boundsCont = currentHandle.BoundsControlRoot;
            initialGrabPoint = currentHandle.interactorsSelecting[0].GetAttachTransform(currentHandle).position;
            currentManipulationAxis = currentHandle.transform.forward;
            initialTransformOnGrabStart = new MixedRealityTransform(boundsCont.Target.transform);
        }

        /// <inheritdoc />
        public override Quaternion Update(List<UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor> interactors, UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable interactable, MixedRealityTransform currentTarget, bool centeredAnchor)
        {
            base.Update(interactors, interactable, currentTarget, centeredAnchor);

            // Compute the anchor around which we will be rotating the object, based
            // on the desired RotateAnchorType.
            Vector3 anchorPoint = centeredAnchor ? boundsCont.Target.transform.TransformPoint(boundsCont.CurrentBounds.center) : boundsCont.Target.transform.position;
            Vector3 currentGrabPoint = currentHandle.interactorsSelecting[0].GetAttachTransform(currentHandle).position;
            Vector3 initDir = Vector3.ProjectOnPlane(initialGrabPoint - anchorPoint, currentManipulationAxis).normalized;
            Vector3 currentDir = Vector3.ProjectOnPlane(currentGrabPoint - anchorPoint, currentManipulationAxis).normalized;

            Quaternion initQuat = Quaternion.LookRotation(initDir, currentManipulationAxis);
            Quaternion currentQuat = Quaternion.LookRotation(currentDir, currentManipulationAxis);
            Quaternion goalRotation = (currentQuat * Quaternion.Inverse(initQuat)) * initialTransformOnGrabStart.Rotation;

            return goalRotation;
        }
    }
}
