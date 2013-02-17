﻿using System;
using SQLGeneration.Parsing;

namespace SQLGeneration.Builders
{
    /// <summary>
    /// Adds a condition to a where clause.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Gets or sets whether to wrap the filter in parentheses.
        /// </summary>
        bool? WrapInParentheses
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a string representation of the filter.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        /// <returns>The generated text.</returns>
        TokenStream GetFilterTokens(CommandOptions options);
    }
}
