﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Simple.Data.Schema
{
    class ColumnCollection : Collection<Column>
    {
        public ColumnCollection()
        {
            
        }

        public ColumnCollection(IEnumerable<Column> columns) : base(columns.ToList())
        {
            
        }
        /// <summary>
        /// Finds the column with a name most closely matching the specified column name.
        /// This method will try an exact match first, then a case-insensitve search, then a pluralized or singular version.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>A <see cref="Column"/> if a match is found; otherwise, <c>null</c>.</returns>
        public Column Find(string columnName)
        {
            return FindColumnWithExactName(columnName)
                   ?? FindColumnWithCaseInsensitiveName(columnName);
        }

        private Column FindColumnWithExactName(string columnName)
        {
            try
            {
                return this
                    .Where(c => c.ActualName.Equals(columnName))
                    .SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                throw new AmbiguousObjectNameException(columnName);
            }
        }

        private Column FindColumnWithCaseInsensitiveName(string columnName)
        {
            try
            {
                return this
                    .Where(c => c.ActualName.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    .SingleOrDefault();
            }
            catch (InvalidOperationException)
            {
                throw new AmbiguousObjectNameException(columnName);
            }
        }
    }
}
