﻿using System;
using SQLGeneration.Properties;

namespace SQLGeneration
{
    /// <summary>
    /// Adds a parameter to a command.
    /// </summary>
    public class Parameter : IParameter
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of a Parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        public Parameter(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Resources.BlankParameterName, "name");
            }
            _name = name;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// Gets or sets an alias for the parameter.
        /// </summary>
        public string Alias
        {
            get;
            set;
        }

        string IProjectionItem.GetFullText(BuilderContext context)
        {
            return _name;
        }

        string IFilterItem.GetFilterItemText(BuilderContext context)
        {
            return _name;
        }

        string IGroupByItem.GetGroupByItemText(BuilderContext context)
        {
            return _name;
        }
    }
}
