﻿using System;

namespace SQLGeneration
{
    /// <summary>
    /// Generates the intersection among all of the queries.
    /// </summary>
    public class Intersect : SelectCombiner
    {
        /// <summary>
        /// Initializes a new instance of a Intersect.
        /// </summary>
        /// <param name="leftHand">The left hand SELECT command.</param>
        /// <param name="rightHand">The right hand SELECT command.</param>
        public Intersect(ISelectBuilder leftHand, ISelectBuilder rightHand)
            : base(leftHand, rightHand)
        {
        }

        /// <summary>
        /// Retrieves the text used to combine two queries.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        /// <returns>The text used to combine two queries.</returns>
        protected override string GetCombinationType(CommandOptions options)
        {
            // "INTERSECT"
            return "INTERSECT";
        }
    }
}
