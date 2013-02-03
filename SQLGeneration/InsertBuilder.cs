﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SQLGeneration.Parsing;

namespace SQLGeneration
{
    /// <summary>
    /// Builds a string of an insert statement.
    /// </summary>
    public class InsertBuilder : ICommand
    {
        private readonly Table _table;
        private readonly List<Column> _columns;
        private readonly IValueProvider _values;

        /// <summary>
        /// Initializes a new instance of a InsertBuilder.
        /// </summary>
        /// <param name="table">The table being inserted into.</param>
        /// <param name="values">The values to insert into the table.</param>
        public InsertBuilder(Table table, IValueProvider values)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            _table = table;
            _columns = new List<Column>();
            _values = values;
        }

        /// <summary>
        /// Gets the table that is being inserted into.
        /// </summary>
        public Table Table
        {
            get { return _table; }
        }

        /// <summary>
        /// Gets the columns being inserted into.
        /// </summary>
        public IEnumerable<Column> Columns
        {
            get { return new ReadOnlyCollection<Column>(_columns); }
        }

        /// <summary>
        /// Adds the column to the insert statement.
        /// </summary>
        /// <param name="column">The column to add.</param>
        public void AddColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            _columns.Add(column);
        }

        /// <summary>
        /// Removes the column from the insert statement.
        /// </summary>
        /// <param name="column">The column to remove.</param>
        /// <returns>True if the column was removed; otherwise, false.</returns>
        public bool RemoveColumn(Column column)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            return _columns.Remove(column);
        }

        /// <summary>
        /// Gets the list of values or select statement that populates the insert.
        /// </summary>
        public IValueProvider Values
        {
            get { return _values; }
        }

        /// <summary>
        /// Gets the SQL for the insert statement.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        public IEnumerable<string> GetCommandTokens(CommandOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            options = options.Clone();
            options.IsSelect = false;
            options.IsInsert = true;
            options.IsUpdate = false;
            options.IsDelete = false;
            return getCommandToken(options);
        }

        private IEnumerable<string> getCommandToken(CommandOptions options)
        {
            // "INSERT" [ "INTO" ] <Source> [ "(" <ColumnList> ")" ] { "VALUES" "(" <ValueList> ")" | <SubSelect> }
            TokenStream stream = new TokenStream();
            stream.Add("INSERT");
            stream.Add("INTO");
            stream.AddRange(((IRightJoinItem)_table).GetDeclarationTokens(options));
            stream.AddRange(buildColumnList(options));
            if (!_values.IsQuery)
            {
                stream.Add("VALUES");
            }
            stream.AddRange(_values.GetFilterTokens(options));
            return stream;
        }

        private IEnumerable<string> buildColumnList(CommandOptions options)
        {
            using (IEnumerator<Column> enumerator = _columns.GetEnumerator())
            {
                TokenStream stream = new TokenStream();
                if (enumerator.MoveNext())
                {
                    stream.Add("(");
                    stream.AddRange(((IProjectionItem)enumerator.Current).GetProjectionTokens(options));
                    while (enumerator.MoveNext())
                    {
                        stream.Add(",");
                        stream.AddRange(((IProjectionItem)enumerator.Current).GetProjectionTokens(options));
                    }
                    stream.Add(")");
                }
                return stream;
            }
        }
    }
}
