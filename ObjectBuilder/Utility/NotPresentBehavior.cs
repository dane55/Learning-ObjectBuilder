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
    /// ����δ�ҵ�ʱ����Ϊ����
    /// </summary>
    public enum NotPresentBehavior
    {
        /// <summary>
        /// �����ö���
        /// </summary>
        CreateNew,

        /// <summary>
        /// ����һ��nullֵ
        /// </summary>
        ReturnNull,

        /// <summary>
        /// ��һ��<see cref="DependencyMissingException"/>�쳣
        /// </summary>
        Throw,
    }
}
