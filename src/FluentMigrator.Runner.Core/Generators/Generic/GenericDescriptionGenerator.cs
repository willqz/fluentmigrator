﻿#region License
//
// Copyright (c) 2018, Fluent Migrator Project
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System.Collections.Generic;

namespace FluentMigrator.Runner.Generators.Generic
{
    /// <summary>
    /// Base class to generate descriptions for tables/classes
    /// </summary>
    public abstract class GenericDescriptionGenerator : IDescriptionGenerator
    {
        protected abstract string GenerateTableDescription(
            string schemaName, string tableName, string tableDescription);
        protected abstract string GenerateColumnDescription(
            string schemaName, string tableName, string columnName, string columnDescription);

        public virtual IEnumerable<string> GenerateDescriptionStatements(Expressions.CreateTableExpression expression)
        {
            var statements = new List<string>();

            if (!string.IsNullOrEmpty(expression.TableDescription))
                statements.Add(GenerateTableDescription(expression.SchemaName, expression.TableName, expression.TableDescription));

            foreach (var column in expression.Columns)
            {
                if (string.IsNullOrEmpty(column.ColumnDescription))
                    continue;

                statements.Add(GenerateColumnDescription(
                    expression.SchemaName,
                    expression.TableName,
                    column.Name,
                    column.ColumnDescription));
            }

            return statements;
        }

        public virtual string GenerateDescriptionStatement(Expressions.AlterTableExpression expression)
        {
            if (string.IsNullOrEmpty(expression.TableDescription))
                return string.Empty;

            return GenerateTableDescription(
                expression.SchemaName, expression.TableName, expression.TableDescription);
        }

        public virtual string GenerateDescriptionStatement(Expressions.CreateColumnExpression expression)
        {
            if (string.IsNullOrEmpty(expression.Column.ColumnDescription))
                return string.Empty;

            return GenerateColumnDescription(
                expression.SchemaName, expression.TableName, expression.Column.Name, expression.Column.ColumnDescription);
        }

        public virtual string GenerateDescriptionStatement(Expressions.AlterColumnExpression expression)
        {
            if (string.IsNullOrEmpty(expression.Column.ColumnDescription))
                return string.Empty;

            return GenerateColumnDescription(expression.SchemaName, expression.TableName, expression.Column.Name, expression.Column.ColumnDescription);
        }
    }
}
