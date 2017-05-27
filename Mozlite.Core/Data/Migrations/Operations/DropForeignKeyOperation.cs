﻿using Mozlite.Data.Metadata;

namespace Mozlite.Data.Migrations.Operations
{
    /// <summary>
    /// 删除外键操作。
    /// </summary>
    public class DropForeignKeyOperation : MigrationOperation
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; [param: NotNull] set; }
        
        /// <summary>
        /// 表格。
        /// </summary>
        public virtual ITable Table { get; [param: NotNull] set; }
    }
}
