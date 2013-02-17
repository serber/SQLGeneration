﻿using System;
using SQLGeneration.Parsing;

namespace SQLGeneration.Builders
{
    /// <summary>
    /// Defines the limits of a function window whose frame ranges before the current row.
    /// </summary>
    public class PrecedingOnlyWindowFrame : WindowFrame
    {
        /// <summary>
        /// Initializes a new instance of a PrecedingOnlyWindowFrame.
        /// </summary>
        /// <param name="precedingFrame">The object describing the preceding frame.</param>
        public PrecedingOnlyWindowFrame(IPrecedingFrame precedingFrame)
        {
            if (precedingFrame == null)
            {
                throw new ArgumentNullException("precedingFrame");
            }
            PrecedingFrame = precedingFrame;
        }

        /// <summary>
        /// Gets the preceding window frame.
        /// </summary>
        public IPrecedingFrame PrecedingFrame
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the tokens for specifying a window frame.
        /// </summary>
        /// <param name="options">The configuration settings to use when generating tokens.</param>
        /// <returns>The tokens making up the window frame.</returns>
        protected override TokenStream GetWindowFrameTokens(CommandOptions options)
        {
            return PrecedingFrame.GetFrameTokens(options);
        }
    }
}
