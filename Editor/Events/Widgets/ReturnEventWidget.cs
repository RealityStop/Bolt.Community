﻿
using Bolt;
using Unity.VisualScripting;

namespace Bolt.Addons.Community.ReturnEvents.Editor
{
    /// <summary>
    /// The visuals for the ReturnEvent Unit.
    /// </summary>
    [Widget(typeof(ReturnEvent))]
    public sealed class ReturnEventWidget : UnitWidget<ReturnEvent>
    {
        public ReturnEventWidget(FlowCanvas canvas, ReturnEvent unit) : base(canvas, unit)
        {
        }

        /// <summary>
        /// Sets the color of the ReturnEvent Unit to green.
        /// </summary>
        protected override NodeColorMix baseColor => NodeColor.Green;
    }
}