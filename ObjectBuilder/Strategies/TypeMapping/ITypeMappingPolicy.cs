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
    /// ����Ĳ���Ϊ <see cref="TypeMappingStrategy"/>�������� <see cref="IBuilderPolicy"/> ��ӳ�䴦���������
    /// </summary>
    public interface ITypeMappingPolicy : IBuilderPolicy
    {
        /// <summary>
        /// ӳ��һ��[����/ID]
        /// </summary>
        /// <param name="incomingTypeIDPair">������[��/ID]</param>
        /// <returns>�µ�[��/ID]</returns>
        DependencyResolutionLocatorKey Map(DependencyResolutionLocatorKey incomingTypeIDPair);
    }
}