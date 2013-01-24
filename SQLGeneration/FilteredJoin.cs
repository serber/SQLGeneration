﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SQLGeneration
{
    /// <summary>
    /// Represents a join that is filtered with an ON expression.
    /// </summary>
    public abstract class FilteredJoin : Join, IFilteredJoin
    {
        private readonly IFilterGroup on;

        /// <summary>
        /// Initializes a new instance of a FilteredJoin.
        /// </summary>
        /// <param name="left">The left hand item in the join.</param>
        /// <param name="right">The right hand item in the join.</param>
        /// <param name="filters">The filters to apply.</param>
        protected FilteredJoin(IJoinItem left, IJoinItem right, IEnumerable<IFilter> filters)
            : base(left, right)
        {
            if (filters == null)
            {
                throw new ArgumentNullException("filters");
            }
            on = new FilterGroup();
            foreach (IFilter filter in filters)
            {
                if (filter == null)
                {
                    throw new ArgumentNullException("filters");
                }
                on.AddFilter(filter);
            }
        }

        /// <summary>
        /// Gets the filters by which the left and right hand items are joined.
        /// </summary>
        public IEnumerable<IFilter> On
        {
            get
            {
                return on.Filters;
            }
        }

        /// <summary>
        /// Adds a condition by which the items are joined.
        /// </summary>
        /// <param name="filter">The join condition.</param>
        public void AddFilter(IFilter filter)
        {
            on.AddFilter(filter);
        }

        /// <summary>
        /// Removes a condition by which the items are joined.
        /// </summary>
        /// <param name="filter">The join condition.</param>
        /// <returns>True if the filter was removed; otherwise, false.</returns>
        public bool RemoveFilter(IFilter filter)
        {
            return on.RemoveFilter(filter);
        }

        /// <summary>
        /// Gets the ON expression for the join.
        /// </summary>
        /// <returns>The generated text.</returns>
        protected override string GetOnExpression(BuilderContext context)
        {
            StringBuilder result = new StringBuilder("ON ");
            BuilderContext clone = context.Clone();
            clone.Options.OneFilterPerLine = false;
            clone.Options.IndentFilters = false;
            result.Append(on.GetFilterText(clone));
            return result.ToString();
        }
    }
}
