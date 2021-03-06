//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// 指示用于定位查询的搜索模式
    /// </summary>
    public enum SearchMode
	{
        /// <summary>
        /// 向上回溯，在本地存储器搜索，若不存在，则向父存储器搜索。
        /// </summary>
        Up,

        /// <summary>
        /// 在本地存储器搜索对象。
        /// </summary>
        Local
    }
}